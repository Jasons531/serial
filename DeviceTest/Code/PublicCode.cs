using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace DeviceTest
{ 
    class PublicCode
    {
    }

    public static class DataHelper
    {
        public static bool IsEmpty(this byte[] data)
        {
            if (data == null)
            {
                return true;
            }
            foreach (byte b in data)
            {
                if (b != 0x00)    //L07X 系列Flash初始化是0x00不是0xff
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsEmpty(this byte[] data, long startOffset, long length)
        {
            if (data == null)
            {
                return true;
            }
            if (startOffset + length > data.LongLength)
            {
                throw new IndexOutOfRangeException();
            }
            for (long i = startOffset; i < startOffset + length; i++)
            {
                if (data[i] != 0x00)    //L07X 系列Flash初始化是0x00不是0xff
                {
                    return false;
                }
            }
            return true;
        }

        public static void FillEmpty(this byte[] data)
        {
            for (long i = 0; i < data.LongLength; i++)
            {
                data[i] = 0x00;
            }
        }

        public static byte[] GetEffectiveData(this byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            long length;
            for (length = data.LongLength - 1; length > 0 && data[length] == 0xff; length--) ;
            if (length == 0)
            {
                return null;
            }
            else if (length == data.LongLength - 1)
            {
                return data;
            }
            else
            {
                byte[] result = new byte[length + 1];
                Array.Copy(data, 0, result, 0, length + 1);
                return result;
            }
        }

        /// <summary>
        /// 反转数组里面的元素
        /// </summary>
        /// <param name="data">待转换数组，转换完成该数组本身会被反转</param>
        /// <returns>返回反转后的数组也就是原始数据本身</returns>
        public static TResult[] Reverse<TResult>(this TResult[] data)
        {
            Array.Reverse(data);
            return data;
        }

        public static byte[] ToByteArray(this string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = "0" + hexString;
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public static byte[] ToAddressArray(this UInt32 addr)
        {
            byte[] address = BitConverter.GetBytes(addr);
            Array.Reverse(address);
            return address;
        }
    }

    public struct DeviceOperation
    {
        public DateTime Time { get; set; }
        public string UniqueID { get; set; }
        public string OptionByte { get; set; }
        public ISPProgramerL071.DensityType ChipType { get; set; }
        public float BootloaderVer { get; set; }
        public string File1 { get; set; }
        public string File2 { get; set; }
        public string File3 { get; set; }
        public string FirmwareVer { get; set; }
        public string SerialID { get; set; }
    }

    public struct FirmwareInfomation
    {
        public string Name { get; set; }
        public UInt32 BaseAddress { get; set; }
        public byte[] Data { get; set; }
        public Exception Error { set; get; }
    }
}
