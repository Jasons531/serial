using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace DeviceTest
{
    public class ISPProgramerL071
    {
        //可在相关芯片的stm32f030xc.h头文件中可找到 第500行左右
        //public const UInt32 FLASH_BASE_ADDR = 0x08000000;
        //private const UInt32 UNIQUE_ID_ADDR = 0x1FFFF7AC;
        //private const UInt32 OPTION_ADDR = 0x1FFFF800;
        //private const UInt32 CHIP_SIZE_ADDR = 0x1FFFF7CC;

        ////可在相关芯片的stm32l071xx.h头文件中可找到 第590行左右
        //public const UInt32 FLASH_BASE_ADDR = 0x08000000;
        //private const UInt32 UNIQUE_ID_ADDR = 0x1FF80050;
        //private const UInt32 OPTION_ADDR = 0x1FF80000;
        //private const UInt32 CHIP_SIZE_ADDR = 0x1FF8007C;

        //可在相关芯片的stm32l072xx.h头文件中可找到 第690行左右
        public const UInt32 FLASH_BASE_ADDR = 0x08000000;
        private const UInt32 UNIQUE_ID_ADDR = 0x1FF80050;
        private const UInt32 OPTION_ADDR = 0x1FF80000;
        private const UInt32 CHIP_SIZE_ADDR = 0x1FF8007C;

        private const byte ACK_REPLY = 0x79;
        private const byte NACK_REPLY = 0x1f;
        private const byte INIT_COMMAND = 0x7f;
        private const byte INDENT_COMMAND = 0x00;
        private const byte GET_VER_COMMAND = 0x01;
        private const byte GET_ID_COMMAND = 0x02;
        private const byte READ_MEM_COMMAND = 0x11;
        private const byte GO_COMMAND = 0x21;
        private const byte WRITE_MEM_COMMAND = 0x31;
        private const byte ERASE_MEM_COMMAND = 0x43;
        private const byte EXTERN_ERASE_MEM_COMMAND = 0x44;
        private const byte WRITE_PROTECT_COMMAND = 0x63;
        private const byte WRITE_UNPROTECT_COMMAND = 0x73;
        private const byte READOUT_PROTECT_COMMAND = 0x82;
        private const byte READOUT_UNPROTECT_COMMAND = 0x92;

        private const int TIMEOUT_50MS = 50;
        private const int TIMEOUT_200MS = 200;
        private const int TIMEOUT_500MS = 500;
        private const int TIMEOUT_1000MS = 1000;
        private const int TIMEOUT_2000MS = 2000;

        public enum InitialType
        {
            NOT_USE_RTS_DRT = 0,
            DTR_LOW_REBOOT = 1,
            DTR_LOW_REBOOT_RTS_LOW_ENTERBOOTLOADER = 2,
            DTR_LOW_REBOOT_RTS_HIGH_ENTERBOOTLOADER = 3,
            DTR_HIGH_REBOOT = 4,
            DTR_HIGH_REBOOT_RTS_LOW_ENTERBOOTLOADER = 5,
            DTR_HIGH_REBOOT_RTS_HIGH_ENTERBOOTLOADER = 6,
            RTS_LOW_REBOOT = 7,
            RTS_LOW_REBOOT_DTR_LOW_ENTERBOOTLOADER = 8,
            RTS_LOW_REBOOT_DTR_HIGH_ENTERBOOTLOADER = 9,
            RTS_HIGH_REBOOT = 10,
            RTS_HIGH_REBOOT_DTR_LOW_ENTERBOOTLOADER = 11,
            RTS_HIGH_REBOOT_DTR_HIGH_ENTERBOOTLOADER = 12
        };
        public enum DensityType
        {
            F103_LOW_DENSITY = 0x12,
            F103_MEDIUM_DENSITY = 0x10,
            F103_HIGH_DENSITY = 0x14,
            F103_XL_DENSITY = 0x30,
            F103_CONNECTIVITY_DENSITY = 0x18,
            F030XC_DENSITY = 0x42,
            L07X_DENSITY = 0x47,
            F207_DENSITY = 0x11,
            UNKNOW_DENSITY = NACK_REPLY
        };

        public string UniqueID { get; private set; }
        public string OptionByte { get; private set; }
        public float BootloaderVer { get; private set; }
        public DensityType ChipType { get; private set; }
        public UInt32 ChipSize { get; private set; }
        public UInt32 PageSize { get; private set; }
        public string PortName { get { return port.PortName; } }
        public int PortBaudRate { get { return port.BaudRate; } }
        public SerialPort port = new SerialPort();
        public event ProgressChangedEventHandler ProgressChanged = null;
        private bool cancelPending = false;
        public ISPProgramerL071() { }

        private void ReportProgress(int pec, object o)
        {
            if (cancelPending == true)
            {
                cancelPending = false;
                throw new Exception("操作被用户取消\r\n");
            }
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(pec, o));
            }
        }

        private void ReportProgress(int pec)
        {
            ReportProgress(pec, null);
        }

        public void CancelWrok()
        {
            cancelPending = true;
        }

        #region 初始化
        public bool Init(string portName, int baudRate)
        {
            port.WriteTimeout = TIMEOUT_200MS;
            port.ReadTimeout = TIMEOUT_200MS;
            try
            {
                port.Close();
                port.PortName = portName;
                port.BaudRate = baudRate;
                port.DataBits = 8;
                port.Parity = Parity.Even;
                port.StopBits = StopBits.One;
                port.Open();
            }
            catch (Exception)
            {
                return false;
            }
            if (!port.IsOpen)
            {
                return false;
            }
            return true;
        }
        public bool Init(string portName, int baudRate, InitialType index)
        {
            if (!Init(portName, baudRate))
                return false;
            return EnterBooloader(index);
        }

        public bool EnterBooloader(InitialType index)
        {
            if (!port.IsOpen)
                port.Open();
            int type = (int)index - 1;
            //F030XXC 此代码与烧录器硬件有关
            //复位一次，重新进入bootloader
            //port.DtrEnable = false;
            //port.RtsEnable = true;
            //port.DtrEnable = false;
            //port.RtsEnable = false;
            //Thread.Sleep(100);

            if (type >= 0)
            {
                bool rtsReboot = (type / 6) > 0;
                int enterBootloader = type % 3;
                bool rebootLevel = ((type % 6) / 3) > 0;
                if (rtsReboot)
                {
                    port.RtsEnable = rebootLevel;
                    Thread.Sleep(5);
                }
                else
                {
                    port.DtrEnable = rebootLevel;
                    Thread.Sleep(5);
                }
                if (enterBootloader > 0)
                {
                    if (rtsReboot)
                    {
                        port.DtrEnable = enterBootloader > 1;
                        Thread.Sleep(5);
                    }
                    else
                    {
                        port.RtsEnable = enterBootloader > 1;
                        Thread.Sleep(5);
                    }
                }
                if (rtsReboot)
                {
                    port.RtsEnable = !rebootLevel;
                    Thread.Sleep(5);
                }
                else
                {
                    port.DtrEnable = !rebootLevel;
                    Thread.Sleep(5);
                }
            }

            ///*
            // RTS    DTR    BOOT    RST     
            //  1      1      1       1
            //  1      0      1       0
            //  0      1      0       1
            //  0      0      0       1
            //*/

            ///* 默认打开时RTS和DTR都是0 */
            ///*  BOOT = 1 RST = 0 复位*/
            //port.RtsEnable = true;
            //port.DtrEnable = false;
            //Thread.Sleep(10);
            ///*  BOOT = 1 RST = 1 释放复位*/
            //port.DtrEnable = true;
            //port.RtsEnable = true;
            ///* 给时间STM32进入BOOT */
            //Thread.Sleep(10);

            /* 防止误碰 */
            port.RtsEnable = false;
            port.DtrEnable = false;

            for (int i = 0; i < 3; i++)
            {
                port.Write(new byte[] { INIT_COMMAND }, 0, 1);
                int time = 0;
                while (port.BytesToRead <= 0 && time < 10)
                {
                    time++;
                    Thread.Sleep(5);
                }
                if (port.BytesToRead > 0)
                {
                    int firstbyte = port.ReadByte();
                    if ((firstbyte == ACK_REPLY || firstbyte == NACK_REPLY))
                    {
                        return true;
                    }
                    return false;
                }
            }
            Close();
            return false;
        }

        public bool ReadInfo()
        {
            ChipType = ReadDensity();
            if (ChipType == DensityType.L07X_DENSITY)
            {
                OptionByte = ReadOptionByte();
                UniqueID = "L07X芯片无法用Bootloader读取UID";
            }
            else if (ChipType == DensityType.F207_DENSITY)
            {
                OptionByte = "F207无法读取选项字节";
                UniqueID = "F207芯片无法用Bootloader读取UID";
            }
            BootloaderVer = ReadBootloaderVer();
            ChipSize = PageSize * 1024;//目前使用的芯片Flash为128K
            if (ChipType == (DensityType)0x79 || ChipSize <= 0)
                return false;
            return true;
        }
        public void Close()
        {
            if (port.IsOpen)
            {
                port.Close();
            }
        }
        #endregion

        #region 串口命令

        public bool SendCommand(byte data)
        {
            try
            {
                port.Write(new byte[] { data }, 0, 1);
                port.Write(new byte[] { (byte)(data ^ 0xff) }, 0, 1);

                port.ReadTimeout = 1000;
                int time = 0;
                while (port.BytesToRead <= 0 && time < 100)
                {
                    time++;
                    Thread.Sleep(1);
                }
                if (port.BytesToRead > 0)
                {
                    int firstbyte = port.ReadByte();
                    if (firstbyte == ACK_REPLY)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SendCommand(byte[] data, int timeout)
        {
            port.ReadExisting();//清空接收缓存区
            byte bcc = 0;
            for (int i = 0; i < data.Length; i++)
            {
                port.Write(data, i, 1);
                bcc ^= data[i];
            }
            port.Write(new byte[] { bcc }, 0, 1);
            port.ReadTimeout = timeout;
            int time = 0;
            while (port.BytesToRead <= 0 && time < 100)
            {
                time++;
                Thread.Sleep(1);
            }
            try
            {
                if (port.BytesToRead > 0)
                {
                    int firstbyte = port.ReadByte();
                    if (firstbyte == ACK_REPLY)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 功能函数
        public bool StartProgram(UInt32 address)
        {
            return Go(address.ToAddressArray());
        }

        public DensityType ReadDensity()
        {
            DensityType pid = (DensityType)GetPID(); ;
            switch (pid)
            {
                case DensityType.F103_LOW_DENSITY:
                case DensityType.F103_MEDIUM_DENSITY:
                    PageSize = 1024;
                    break;
                case DensityType.F103_HIGH_DENSITY:
                case DensityType.F103_XL_DENSITY:
                case DensityType.F103_CONNECTIVITY_DENSITY:
                    PageSize = 2048;
                    break;
                case DensityType.F030XC_DENSITY:    //F030XC页大小为2048 0x800
                    PageSize = 2048;
                    break;
                case DensityType.L07X_DENSITY:    //L071页大小为128 128
                    PageSize = 128;
                    break;
                case DensityType.F207_DENSITY:
                    PageSize = 256;
                    break;
                default:
                    PageSize = 1024;
                    break;
            }
            return pid;
        }

        public float ReadBootloaderVer()
        {
            byte readVer = GetBootLoaderVer();
            if (readVer != NACK_REPLY)
            {
                return ((readVer & 0xf0) >> 4) + (float)0.1 * (readVer & 0xf);
            }
            return NACK_REPLY;
        }

        public UInt16 ReadChipSize()
        {
            byte[] data = Read(CHIP_SIZE_ADDR.ToAddressArray(), 1);
            if (data != null)
            {
                UInt16 size = BitConverter.ToUInt16(data, 0);
                ChipSize = (UInt32)size * 1024;
                return size;
            }
            return 0;
        }

        public string ReadUinqueID(UInt32 unique_id_addr)
        {
            byte[] s = Read(unique_id_addr.ToAddressArray(), 11);
            if (s != null)
            {
                return BitConverter.ToString(s).Replace("-", "");
            }
            return null;
        }

        public string ReadOptionByte()
        {
            //byte[] s = Read(OPTION_ADDR.ToAddressArray(), 15);//F103、F030的选项字节有16位
            byte[] s = Read(OPTION_ADDR.ToAddressArray(), 19);//L071、L072的选项字节有20位
            if (s != null)
            {
                return BitConverter.ToString(s).Replace("-", "");
            }
            return null;
        }
        #endregion

        #region flash操作

        /// <summary>从指定地址读取指定长度数据</summary>
        /// <param name="addr">起始读取地址，应不小于FLASH_BASE_ADDR且小于FLASH_BASE_ADDR+ChipSize</param>
        /// <param name="length">读取长度，addr + length应不大于FLASH_BASE_ADDR + ChipSize</param>
        /// <returns>读取成功返回读取到的数据，否则返回null</returns>
        public byte[] ReadFlash(UInt32 addr, UInt32 length)
        {
            if (addr + length > FLASH_BASE_ADDR + ChipSize)
            {
                return null;
            }
            byte[] data = new byte[length];
            UInt32 offset = 0;
            while (length > 0)
            {
                UInt32 len = Math.Min(0x100, length);
                byte[] d = new byte[len];
                d = Read(addr.ToAddressArray(), (byte)(len - 1));
                if (d == null)
                {
                    return null;
                }
                Array.Copy(d, 0, data, offset, len);
                offset += len;
                length -= len;
                addr += len;
                ReportProgress((int)((offset * 100) / (length + offset)));
                //ReportProgress((int)((offset * 100) / (length + offset)), offset.ToString("x8")+"\r\n");
            }
            return data;
        }

        /// <summary>在指定flash地址写入数据，仅覆盖写入地址范围内的数据</summary>
        /// <param name="address">写入起始地址，应为4的倍数且不小于FLASH_BASE_ADDR但小于FLASH_BASE_ADDR+ChipSize</param>
        /// <param name="startRemainData">写入数据，长度加上address应小于FLASH_BASE_ADDR+ChipSize</param>
        /// <returns>写入成功返回true，写入错误返回false</returns>
        public bool WriteFlashL072(UInt32 address, byte[] data)
        {
            UInt32 length = (UInt32)data.Length;
            if (address < FLASH_BASE_ADDR || address + length > FLASH_BASE_ADDR + ChipSize || address % 4 != 0)
            {
                throw new ArgumentOutOfRangeException("地址或数据长度非法\r\n");
            }

            if (length % 4 != 0)
            {
                List<byte> temp = new List<byte>();
                temp.AddRange(data);
                byte[] tale = Read((address + length).ToAddressArray(), (byte)(3 - length % 4));
                if (tale != null)
                {
                    temp.AddRange(tale);
                    data = temp.ToArray();
                    length = (UInt32)data.Length;
                }
            }

            UInt32 startOffset = address - FLASH_BASE_ADDR;         //数据的首地址
            UInt16 startPage = (UInt16)(startOffset / PageSize);    //数据的开始页

            UInt32 taleOffset = startOffset + length;                       //数据的尾部地址
            UInt16 talePage = (UInt16)((startOffset + length) / PageSize);  //数据的尾部地址所在的页，判断数据是否跨页
            UInt32 taleRemain = taleOffset % PageSize;                      //尾部页偏移

            List<UInt16> pagesToErase = new List<UInt16>();         //要擦的页
            UInt16 page = startPage;

            if (length > PageSize)//写入的是烧录文件
            {
                for (page = startPage; page <= talePage; page++)        //先擦除数据
                {
                    pagesToErase.Add(page);
                }
                if (pagesToErase.Count != 0)
                {
                    if (BootloaderVer < (float)3)//L07X 系列都不会小于3
                        return false;
                    else
                    {
                        List<ushort> pagesToErase16 = new List<ushort>();
                        foreach (var item in pagesToErase)
                        {
                            pagesToErase16.Add(item);
                        }
                        ReportProgress(0, "擦除flash\r\n");
                        if (!ExternErase(pagesToErase16.ToArray()))    //擦除所有页
                        {
                            ReportProgress(0, "flash擦除失败\r\n");
                            return false;
                        }
                    }
                }
                ReportProgress(0, "开始写入文件数据\r\n");
                UInt32 offset = 0;
                while (length > 0)
                {
                    UInt32 inPageAddr = address - FLASH_BASE_ADDR + offset;
                    byte len = length > 0x80 ? (byte)0x7f : (byte)(length - 1);
                    byte[] wData = new byte[len + 1];
                    Array.Copy(data, offset, wData, 0, len + 1);
                    if (!Write((address + offset).ToAddressArray(), wData))
                    {
                        return false;
                    }

                    byte[] d = ReadFlash(address + offset, (uint)wData.Length);
                    bool ret = (d != null && BitConverter.ToString(d).Equals(BitConverter.ToString(wData)));
                    if (ret == false)
                    {
                        ReportProgress(50, "校验失败\r\n");
                        return true;
                    }
                    offset += (UInt32)(1 + len);
                    length -= (UInt32)(1 + len);
                    ReportProgress((int)((offset * 100) / (length + offset)));
                }
                ReportProgress(100, "写入并校验成功\r\n");
                return true;

            }
            else//写入的是自定义数据，自定义数据不能大于1页大小
            {
                ReportProgress(0, "开始写入自定义数据\r\n");
                UInt32 add = FLASH_BASE_ADDR + startPage * PageSize;         //读数据
                UInt32 PageCount = (UInt32)(talePage - startPage + 1);

                byte[] FlashData = new byte[PageCount * PageSize];
                for (UInt32 i = 0; i < PageCount; i++)
                {
                    UInt32 AddTemp = add + i * PageSize;
                    byte[] temp = Read(AddTemp.ToAddressArray(), 0x80 - 1);
                    Array.Copy(temp, 0, FlashData, i * PageSize, temp.Length);
                }
                //修改数据
                for (int i = 0; i < PageCount * PageSize; i++)
                {
                    if (address == add + i)
                    {
                        Array.Copy(data, 0, FlashData, i, data.Length);
                        break;
                    }
                }
                if (BootloaderVer < (float)3)//L07X 系列都不会小于3
                    return false;
                else
                {
                    List<ushort> pagesToErase16 = new List<ushort>();
                    for (page = startPage; page <= talePage; page++)        //先擦除数据
                    {
                        pagesToErase16.Add(page);
                    }
                    ReportProgress(0, "擦除flash\r\n");
                    if (!ExternErase(pagesToErase16.ToArray()))    //擦除所有页
                    {
                        ReportProgress(0, "flash擦除失败\r\n");
                        return false;
                    }
                }
                UInt32 offset = 0;
                length = (UInt32)FlashData.Length;
                while (length > 0)
                {
                    UInt32 inPageAddr = add - FLASH_BASE_ADDR + offset;
                    byte len = length > 0x80 ? (byte)0x7f : (byte)(length - 1);
                    byte[] wData = new byte[len + 1];
                    Array.Copy(FlashData, offset, wData, 0, len + 1);
                    if (!Write((add + offset).ToAddressArray(), wData))
                    {
                        return false;
                    }

                    byte[] d = ReadFlash(add + offset, (uint)wData.Length);
                    bool ret = (d != null && BitConverter.ToString(d).Equals(BitConverter.ToString(wData)));
                    if (ret == false)
                    {
                        ReportProgress(50, "校验失败\r\n");
                        return true;
                    }
                    offset += (UInt32)(1 + len);
                    length -= (UInt32)(1 + len);
                    ReportProgress((int)((offset * 100) / (length + offset)));
                }
                ReportProgress(100, "写入并校验成功\r\n");
                return true;
            }

        }


        public bool WriteFlashF207(UInt32 address, byte[] data)
        {
            UInt32 length = (UInt32)data.Length;
            if (address < FLASH_BASE_ADDR || address + length > FLASH_BASE_ADDR + ChipSize || address % 4 != 0)
            {
                throw new ArgumentOutOfRangeException("地址或数据长度非法\r\n");
            }

            if (length % 4 != 0)
            {
                List<byte> temp = new List<byte>();
                temp.AddRange(data);
                byte[] tale = Read((address + length).ToAddressArray(), (byte)(3 - length % 4));
                if (tale != null)
                {
                    temp.AddRange(tale);
                    data = temp.ToArray();
                    length = (UInt32)data.Length;
                }
            }

            if (length > PageSize)//写入的是烧录文件
            {
                ReportProgress(0, "开始写入文件数据\r\n");
                UInt32 offset = 0;
                while (length > 0)
                {
                    byte len = length > 0x100 ? (byte)0xFF : (byte)(length - 1);
                    byte[] wData = new byte[len + 1];
                    Array.Copy(data, offset, wData, 0, len + 1);
                    if (!Write((address + offset).ToAddressArray(), wData))
                    {
                        return false;
                    }
                    byte[] d = ReadFlash(address + offset, (uint)wData.Length);
                    bool ret = (d != null && BitConverter.ToString(d).Equals(BitConverter.ToString(wData)));
                    if (ret == false)
                    {
                        ReportProgress(50, "校验失败\r\n");
                        return true;
                    }
                    offset += (UInt32)(1 + len);
                    length -= (UInt32)(1 + len);

                    ReportProgress((int)((offset * 100) / (length + offset)));

                }
                ReportProgress(100, "写入并校验成功\r\n");
                return true;

            }
            else//写入的是自定义数据，自定义数据不能大于1页大小
            {
                ReportProgress(0, "开始写入自定义数据\r\n");
                if (BootloaderVer < (float)3)//L07X 系列都不会小于3
                    return false;
                if (!Write(address.ToAddressArray(), data))
                {
                    return false;
                }
                byte[] d = ReadFlash(address, (uint)data.Length);
                bool ret = (d != null && BitConverter.ToString(d).Equals(BitConverter.ToString(data)));
                if (ret == false)
                {
                    ReportProgress(50, "校验失败\r\n");
                    return true;
                }
                ReportProgress(100, "写入并校验成功\r\n");
                return true;
            }
        }

        /// <summary>擦除整个芯片</summary>
        /// <returns>擦除结果</returns>
        public bool EraseChip()
        {
            if (BootloaderVer > (float)2.2)
            {
                return ExternErase(0xffff);
            }
            else
            {
                return Erase(0xff);
            }
        }

        /// <summary>从指定地址开始擦除指定长度空间，页擦除后会写回不在擦除范围的数据，和WriteMem存在嵌套调用</summary>
        /// <param name="address">起始地址，必须为4的倍数</param>
        /// <param name="length">擦除长度，必须为4的倍数</param>
        /// <returns>擦除成功返回true，擦除失败返回false</returns>
        public bool EraseMem(UInt32 address, UInt32 length)
        {
            UInt32 offset = 0;
            if (length % 4 != 0)
            {
                length += (4 - length % 4);
            }
            ///161125 修正偏移和结束条件计算错误导致数据溢出无限擦除情况
            ///<exception>计算错误可能导致无限擦除</exception>
            while (offset < length)
            {
                UInt32 offsetAddr = address - FLASH_BASE_ADDR + offset;
                byte page = (byte)(offsetAddr / PageSize);
                UInt32 inPageOffset = offsetAddr % PageSize;
                UInt32 inPageRemain = PageSize - inPageOffset;
                UInt32 remain = length - offset;
                byte[] frontData = null;
                byte[] behindData = null;

                if (inPageOffset > 0)
                {
                    frontData = ReadFlash(FLASH_BASE_ADDR + page * PageSize, inPageOffset);
                    if (frontData.IsEmpty())
                    {
                        frontData = null;
                    }
                }
                if (remain < inPageRemain)
                {
                    behindData = ReadFlash(address + length, inPageRemain - remain);
                    if (behindData.IsEmpty())
                    {
                        behindData = null;
                    }
                    offset += remain;
                }
                else
                {
                    offset += inPageRemain;
                }
                if (!Erase(page))
                {
                    return false;
                }
                if (frontData != null)
                {
                    WriteMem(FLASH_BASE_ADDR + page * PageSize, frontData);
                }
                if (behindData != null)
                {
                    WriteMem(address + length, behindData);
                }
            }

            return true;
        }

        /// <summary>在指定flash地址写入数据，仅覆盖写入地址范围内的数据，和EraseMem存在嵌套调用</summary>
        /// <param name="address">写入起始地址，应为4的倍数且不小于FLASH_BASE_ADDR但小于FLASH_BASE_ADDR+ChipSize</param>
        /// <param name="startRemainData">写入数据，长度加上address应小于FLASH_BASE_ADDR+ChipSize</param>
        /// <returns>写入成功返回true，写入错误返回false</returns>
        public bool WriteMem(UInt32 address, byte[] data)
        {
            UInt32 length = (UInt32)data.Length;
            if (address < FLASH_BASE_ADDR || address + length > FLASH_BASE_ADDR + ChipSize || address % 4 != 0)
            {
                return false;
            }

            if (length % 4 != 0)
            {
                byte[] temp = new byte[length + 4 - length % 4];
                byte[] tale = ReadFlash((UInt32)(address + length), 4 - length % 4);
                if (tale != null)
                {
                    Array.Copy(data, temp, length);
                    Array.Copy(tale, 0, temp, length, tale.Length);
                    data = temp;
                    length = (UInt32)data.Length;
                }
            }

            ReportProgress(0, "检查flash\r\n");

            if (!ReadFlash(address, length).IsEmpty())
            {
                ReportProgress(0, "擦除flash\r\n");
                if (!EraseMem(address, length))
                {
                    ReportProgress(100, "擦除失败\r\n");
                    return false;
                }
                else
                {
                    ReportProgress(100, "擦除成功\r\n");
                }
            }

            UInt32 offset = 0;

            ReportProgress(0, "开始写入...\r\n");

            while (length > 0)
            {
                UInt32 inPageAddr = address - FLASH_BASE_ADDR + offset;
                byte page = (byte)((inPageAddr) / PageSize);
                byte len = length > 0x80 ? (byte)0x80 : (byte)(length - 1);
                byte[] addr = BitConverter.GetBytes(address + offset);
                Array.Reverse(addr);
                byte[] wData = new byte[len + 1];
                Array.Copy(data, offset, wData, 0, len + 1);
                if (!Write(addr, wData))
                {
                    return false;
                }
                offset += (UInt32)(1 + len);
                length -= (UInt32)(1 + len);

                ReportProgress((int)((offset * 100) / (length + offset)));
            }
            ReportProgress(100, "写入完成\r\n");
            return true;
        }
        #endregion

        #region 基础指令操作
        /// <summary>
        /// 擦除指定的页面，用于2.2版本以及以下的Bootloader
        /// </summary>
        /// <param name="pages">待擦除的多个页面，数量应不大于0xff</param>
        /// <returns>擦除成功返回true，擦除失败返回false。具有写保护的扇区擦除不会报错</returns>
        private bool Erase(byte[] pages)
        {
            if (SendCommand(ERASE_MEM_COMMAND))
            {
                byte[] data = new byte[pages.Length + 1];
                data[0] = (byte)(pages.Length - 1);
                Array.Copy(pages, 0, data, 1, pages.Length);
                return SendCommand(data, TIMEOUT_500MS * (pages.Length / 16 + 1));       //每增加16k擦除空间增加500ms等待超时时间
            }
            return false;
        }

        /// <summary>
        /// 擦除特定页面，用于2.2版本以及以下的Bootloader
        /// </summary>
        /// <param name="index">
        /// 0xFF:全部擦除
        /// other:擦除指定一个页面
        /// </param>
        /// <returns>擦除成功返回true，擦除失败返回false。具有写保护的扇区擦除不会报错</returns>
        private bool Erase(byte index)
        {
            if (SendCommand(ERASE_MEM_COMMAND))
            {
                if (index == 0xFF)
                {
                    return SendCommand(index);
                }
                else
                {
                    return SendCommand(new byte[] { 0, index }, TIMEOUT_1000MS);
                }
            }
            return false;
        }

        /// <summary>
        /// 扩展特殊擦除指令，仅支持v3.0及以上 bootloader，用来代替Erase的全部擦除功能，未测试
        /// </summary>
        /// <param name="command">
        /// 双字节命令，MSB在前，调用者需自行保障数据正确
        /// 0xFFFF:全部擦除；
        /// 0xFFFE:存储区1批量擦除
        /// 0xFFFD:存储区2批量擦除
        /// </param>
        /// <returns>擦除成功返回true，擦除失败返回false。具有写保护的扇区擦除不会报错</returns>
        public bool ExternErase(UInt16 command)
        {
            if (SendCommand(EXTERN_ERASE_MEM_COMMAND))
            {
                SendCommand(new byte[] { (byte)(command >> 8), (byte)(command & 0xff) }, 20000);
                /* F207 全擦除需要等待20秒才返回 */
                port.ReadTimeout = 20000;
                int temp = port.BytesToRead;
                int sleepcount = 0;
                while (true)
                {
                    temp = port.BytesToRead;
                    if (temp > 0)
                    {
                        for (int i = 0; i < temp; i++)
                        {
                            byte Reply = (byte)port.ReadByte();
                            ReportProgress(0, Reply.ToString("x2") + " ");
                            if (Reply == 0x79)
                            {
                                return true;
                            }
                            else if (Reply == 0x1f)
                            {
                                return false;
                            }
                            else
                            {
                                ;
                            }
                        }
                    }
                    sleepcount++;
                    Thread.Sleep(1000);
                    //15s内没有再返回数据，说明接收到1帧数据了
                    if (sleepcount == 20)
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// 扩展擦除指令，仅支持v3.0及以上bootloader，用来代替Erase，未测试
        /// </summary>
        /// <param name="pagesToErase">待擦除起始页面，双字节，msb在前</param>
        /// <returns>擦除成功返回true，擦除失败返回false。具有写保护的扇区擦除不会报错</returns>
        public bool ExternErase(UInt16[] pages)
        {
            if (SendCommand(EXTERN_ERASE_MEM_COMMAND))
            {
                byte bcc = 0;
                byte[] data = new byte[pages.Length * 2 + 2];
                data[0] = (byte)((pages.Length - 1) >> 8);
                data[1] = (byte)((pages.Length - 1) & 0xff);
                //data[0] = (byte)(pages.Length >> 8);
                //data[1] = (byte)(pages.Length & 0xff);

                for (int i = 1; i < pages.Length + 1; i++)
                {
                    data[i * 2] = (byte)(pages[i - 1] >> 8);
                    data[i * 2 + 1] = (byte)(pages[i - 1] & 0xff);
                }

                for (int i = 0; i < data.Length; i++)
                {
                    port.Write(data, i, 1);
                    bcc ^= data[i];
                }

                port.Write(new byte[] { bcc }, 0, 1);

                port.ReadTimeout = Math.Min(2000, Math.Max(pages.Length * 5, 100));
                Thread.Sleep(Math.Min(2000, Math.Max(pages.Length * 5, 100)));//100ms太短，读不到数据
                if (port.BytesToRead > 0)
                {
                    byte a = (byte)port.ReadByte();
                    if (a == ACK_REPLY)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 向地址读取写入指定数据
        /// </summary>
        /// <param name="addr">4字节地址，msb在前，必须为4的倍数对齐，否则写入失败</param>
        /// <param name="data">data长度应不大于0x80否则会写入失败，且必须为4的倍数对齐，否则虽然不会报错但是末尾会被未知数据填充</param>
        /// <returns></returns>
        private bool Write(byte[] addr, byte[] data)
        {
            if (SendCommand(WRITE_MEM_COMMAND))
            {
                if (SendCommand(addr, TIMEOUT_200MS))
                {
                    byte[] wData = new byte[data.Length + 1];
                    wData[0] = (byte)(data.Length - 1);
                    Array.Copy(data, 0, wData, 1, data.Length);
                    return SendCommand(wData, TIMEOUT_200MS);
                }

            }
            return false;
        }

        /// <summary>
        /// 从指定地址读取指定长度数据
        /// </summary>
        /// <param name="addr">4字节地址，msb在前</param>
        /// <param name="length">读取长度为length+1</param>
        /// <returns></returns>
        public byte[] Read(byte[] addr, byte length)
        {
            port.ReadExisting();
            if (SendCommand(READ_MEM_COMMAND)) //发送读指令
            {
                if (SendCommand(addr, TIMEOUT_200MS))  //发送读地址
                {

                    if (SendCommand(length))   //发送读长度
                    {
                        byte[] data = new byte[length + 1];
                        int retryCount = 0;
                        for (int i = 0; i <= length && retryCount < 1000; i++)
                        {
                            if (port.BytesToRead == 0)
                            {
                                i--;
                                retryCount++;
                                Thread.Sleep(1);
                                continue;
                            }
                            data[i] = (byte)port.ReadByte();
                            //ReportProgress(0, data[i].ToString("x2") + " ");
                        }
                        if (port.BytesToRead != 0)
                        {
                            port.ReadExisting();
                            return null;
                        }
                        if (retryCount == 0xfff)
                        {
                            return null;
                        }
                        return data;
                    }
                    else
                    {
                        ReportProgress(0, "读长度失败\r\n");
                    }
                }
                else
                {
                    ReportProgress(0, "读地址失败\r\n");
                }
            }
            else
            {
                ReportProgress(0, "读指令失败\r\n");
            }

            return null;
        }

        /// <summary>
        /// 获得双字节芯片PID
        /// </summary>
        /// <returns>如读取成功返回PID低字节数据，高字节固定为0x04，否则返回NACK_REPLY</returns>
        public byte GetPID()
        {
            if (SendCommand(GET_ID_COMMAND))
            {
                //F030会返回：79 01 04 42 79 
                //第一个79在SendCommand()里面被接收
                //01表示字节数为 01+1=2字节 
                //数据1：04 
                //数据2：42 
                //ACK回复 79
                //将数据1、2拼接起来就是PID：0x0442,PID可以在AN2606"器件相关的自举程序参数(Device-dependent bootloader parameters)"章节参看
                //L071会返回：79 01 04 47 79 
                //第一个79在SendCommand()里面被接收
                //01表示字节数为 01+1=2字节 
                //数据1：04 
                //数据2：47 
                //ACK回复 79
                //将数据1、2拼接起来就是PID：0x0442,PID可以在AN2606"器件相关的自举程序参数(Device-dependent bootloader parameters)"章节参看

                //F207：01 04 11 79
                int ReplyCount = port.BytesToRead;
                int[] data = new int[ReplyCount];
                for (int i = 0; i < ReplyCount; i++)
                {
                    data[i] = port.ReadByte();
                    ReportProgress(0, data[i].ToString("x2") + " ");
                }
                if (ReplyCount != 4)
                    return NACK_REPLY;
                byte low, high, count;
                count = (byte)data[0];
                high = (byte)data[1];
                low = (byte)data[2];
                if (count == 0x1 && (byte)data[3] == ACK_REPLY && high == 0x04 && ReplyCount == 4)
                {
                    return low;
                }
            }
            return NACK_REPLY;
        }
        /// <summary>
        /// 获得双字节芯片PID
        /// </summary>
        /// <returns>如读取成功返回PID低字节数据，高字节固定为0x04，否则返回NACK_REPLY</returns>
        public bool GetCommand()
        {
            return SendCommand(INDENT_COMMAND);
        }
        /// <summary>
        /// 获得芯片内部自举Bootloader程序版本号
        /// </summary>
        /// <returns>读取成功返回一个字节版本号，高4bit为大版本号，低4bit为小版本号，否则返回NACK_REPLY</returns>
        public byte GetBootLoaderVer()
        {
            if (SendCommand(GET_VER_COMMAND))
            {
                //F030会返回：79 31 00 00 79
                //第一个79在SendCommand()里面被接收
                //31表示版本号为3.1 
                //选项字节1：00 
                //选项字节2：00 
                //ACK回复 79

                //L071、F207会返回：79 31 00 00 79
                //第一个79在SendCommand()里面被接收
                //31表示版本号为3.1 
                //选项字节1：00 
                //选项字节2：00 
                //ACK回复 79

                int ReplyCount = port.BytesToRead;
                int[] data = new int[ReplyCount];
                for (int i = 0; i < ReplyCount; i++)
                {
                    data[i] = port.ReadByte();
                    ReportProgress(0, data[i].ToString("x2") + " ");
                }
                if (ReplyCount != 4)
                    return NACK_REPLY;
                byte op1, op2, ver;
                ver = (byte)data[0];
                op1 = (byte)data[1];
                op2 = (byte)data[2];
                if ((byte)data[3] == ACK_REPLY && op1 == 0x0 && op2 == 0x0 && ReplyCount == 4)
                {
                    return ver;
                }
            }
            return NACK_REPLY;
        }

        /// <summary>
        /// 从指定地址运行程序
        /// </summary>
        /// <param name="addr">4字节程序地址，msb在前</param>
        /// <returns>成功返回true，否则返回false</returns>
        private bool Go(byte[] addr)
        {
            if (SendCommand(GO_COMMAND))
            {
                return SendCommand(addr, TIMEOUT_200MS);
            }
            return false;
        }

        //todo:读写保护功能待测试
        /// <summary>
        /// 对指定页面执行写保护操作
        /// </summary>
        /// <param name="pageCount">待保护页面，数量应小于0xff</param>
        /// <returns>执行成功返回true，否则返回false，页面无效不会返回错误</returns>  
        public bool WriteProtect(byte[] pages)
        {
            if (SendCommand(WRITE_PROTECT_COMMAND))
            {
                if (SendCommand((byte)pages.Length))
                {
                    byte[] data = new byte[pages.Length + 1];
                    data[0] = (byte)(pages.Length - 1);
                    Array.Copy(pages, 0, data, 1, pages.Length);
                    return SendCommand(data, TIMEOUT_200MS);
                }
            }
            return false;
        }

        /// <summary>
        /// 取消芯片写保护，操作成功之后芯片会重启
        /// </summary>
        /// <returns>取消成功返回true，否则返回false</returns>
        public bool WriteUnProtect()
        {
            if (SendCommand(WRITE_UNPROTECT_COMMAND))
            {
                int retryCount = 0;
                while (port.BytesToRead < 1 && retryCount++ < 0xfff) ;
                return (port.ReadByte() == ACK_REPLY);
            }
            return false;
        }

        /// <summary>
        /// 对芯片执行读保护操作，操作成功之后芯片会重启
        /// </summary>
        /// <returns>操作成功返回true，否则返回false</returns>
        public bool ReadOutProtect()
        {
            if (SendCommand(READOUT_PROTECT_COMMAND))
            {
                int retryCount = 0;
                while (port.BytesToRead < 1 && retryCount++ < 0xfff) ;
                return (port.ReadByte() == ACK_REPLY);
            }
            return false;
        }

        /// <summary>
        /// 取消芯片读保护功能.！！注意取消读保护会擦除整个芯片
        /// </summary>
        /// <returns>操作成功返回true，否则返回false</returns>
        public bool ReadOutUnProtect()
        {
            if (SendCommand(READOUT_UNPROTECT_COMMAND))
            {
                int retryCount = 0;
                while (port.BytesToRead < 1 && retryCount++ < 0xfff) ;
                return (port.ReadByte() == ACK_REPLY);
            }
            return false;
        }
        #endregion
    }
}
