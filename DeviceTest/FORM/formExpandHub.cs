using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceTest
{
    public partial class formExpandHub : Form
    {
        public formExpandHub()
        {
            InitializeComponent();
        }

        private void formExpandHub_Load(object sender, EventArgs e)
        {
            lbFirstConfig(lbExPort1);
            lbFirstConfig(lbExPort2);
            lbFirstConfig(lbExPort3);
            lbFirstConfig(lbExPort4);
            lbFirstConfig(lbExPort5);
        }

        #region txtBox相关操作
        enum TextErr
        {
            eInputErr = 0x00,
            eLengthErr,
        };
        private void txtEnter(object sender, EventArgs e)
        {
            TextBox temp = (TextBox)sender;
            temp.Text = "";

            if (temp.InvokeRequired)
                temp.BeginInvoke(new Action(() => temp.BackColor = System.Drawing.SystemColors.Window));
            else
                temp.BackColor = System.Drawing.SystemColors.Window; ;
        }

        private void txtLeave(object sender, EventArgs e)
        {

        }

        double Text2Double(TextBox txtbox)
        {
            TextCheckDefaultInput(txtbox);
            string str = txtbox.Text.Replace(" ", "");
            try
            {
                return Convert.ToDouble(str);
            }
            catch
            {
                setTextError(txtbox, TextErr.eInputErr);
                return 0;
            }
        }
        int Text2Int(TextBox txtbox)
        {
            TextCheckDefaultInput(txtbox);
            string str = txtbox.Text.Replace(" ", "");
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                setTextError(txtbox, TextErr.eInputErr);
                return 0;
            }
        }

        byte Text2Byte(TextBox txtbox)
        {
            TextCheckDefaultInput(txtbox);
            string str = txtbox.Text.Replace(" ", "");
            if (str.Length != 2)
            {
                setTextError(txtbox, TextErr.eLengthErr);
                return 0;
            }
            else
            {
                try
                {
                    return Convert.ToByte(str, 16);
                }
                catch
                {
                    setTextError(txtbox, TextErr.eInputErr);
                    return 0;
                }
            }

        }
        void setTextError(TextBox txtbox, TextErr err)
        {
            if (txtbox.InvokeRequired)
                txtbox.BeginInvoke(new Action(() => txtbox.BackColor = Color.Red));
            else
                txtbox.BackColor = Color.Red;

            if (err == TextErr.eInputErr)
                throw new Exception("错误！" + txtbox.Tag + " 输入错误\r\n");
            else if ((err == TextErr.eLengthErr))
                throw new Exception("错误！" + txtbox.Tag + " 长度错误\r\n");
            else
                throw new Exception("错误！" + txtbox.Tag + " 未知错误\r\n");

        }

        void TextFirstCfg(TextBox txtbox)
        {
            if (txtbox.Tag == null)
                txtbox.Tag = txtbox.Text;
        }
        byte[] Text2HexArray(TextBox txtbox)
        {
            return txtbox.Text.Replace(" ", "").ToByteArray();
        }

        void TextCheckDefaultInput(TextBox txtbox)
        {
            string DefaultInput = txtbox.Tag as string;
            string input = txtbox.Text.Replace(" ", "");
            if (DefaultInput == input || input == "")
            {
                if (txtbox.InvokeRequired)
                    txtbox.BeginInvoke(new Action(() => txtbox.BackColor = Color.Yellow));
                else
                    txtbox.BackColor = Color.Yellow;

                throw new Exception("警告！" + txtbox.Tag + " 未输入数据\r\n");
            }
        }

        bool TextIsDefaultInputOrNull(TextBox txtbox)
        {
            string DefaultInput = txtbox.Tag as string;
            string input = txtbox.Text.Replace(" ", "");
            if (DefaultInput == input || input == "")
            {
                return true;
            }
            return false;
        }

        #endregion

        #region lable相关操作

        private void lbFirstConfig(Label lb)
        {
            if (lb.Tag == null)
            {
                object a = new object[] { lb.BackColor, lb.Text };
                lb.Tag = a;
            }
        }
        public void lbSetPass(Label lb)
        {
            Action act1 = new Action(() => {
                lb.BackColor = Color.Green;
            });
            lb.BeginInvoke(act1);
        }

        public void lbSetFail(Label lb)
        {
            Action act1 = new Action(() => {
                lb.BackColor = Color.Red;
            });
            lb.BeginInvoke(act1);
        }

        public void lbInitToFail(Label lb)
        {
            object[] a = lb.Tag as object[];
            if (lb.BackColor == (Color)a[0])
            {
                lbSetFail(lb);
            }
        }

        public void lbSetText(Label lb, string text)
        {
            Action act1 = new Action(() => {
                lb.Text = text;
            });
            lb.BeginInvoke(act1);
        }

        public void lbReset(Label lb)
        {
            if (lb.Tag != null)
            {
                object[] a = lb.Tag as object[];
                Action act1 = new Action(() =>
                {
                        lb.BackColor = (Color)a[0];
                        lb.ForeColor = Color.White;

                    lb.Text = a[1] as string;
                });
                lb.BeginInvoke(act1);
            }
        }
        private void ResetAllLable()
        {
            lbReset(lbExPort1);
            lbReset(lbExPort2);
            lbReset(lbExPort3);
            lbReset(lbExPort4);
            lbReset(lbExPort5);
        }

        #endregion

        #region USB插拔自动识别COM口
        private void InitializeSerialPorts()
        {
            this.cmbSerialPort.Items.Clear();
            string[] names = System.IO.Ports.SerialPort.GetPortNames();
            if (names.Length != 0)
            {
                this.cmbSerialPort.Items.AddRange(names);
            }
        }

        private void cmbSerialPort_MouseClick(object sender, MouseEventArgs e)
        {
            InitializeSerialPorts();
            cmbSerialPort.BackColor = System.Drawing.SystemColors.Window;
            cmbTestMode.SelectedItem = "识别地址";
        }
        #endregion

        #region 调试日志
        public void ShowMsg(string text)
        {
            rtbexMsgBox.ShowText(text);
        }

        public void ShowMsgLn(string text)
        {
            text = text + "\r\n";
            ShowMsg(text);
        }

        #endregion

        #region 检测任务
        string g_TestMode;
        private void btnExPortStart_Click(object sender, EventArgs e)
        {
            if (btnExPortStart.Tag == null)
            {
                try
                {
                    string portName = this.cmbSerialPort.SelectedItem as string;
                    g_TestMode = cmbTestMode.SelectedItem as string;
                    if (portName == null || portName.Equals(""))
                        throw new Exception("The serial port is not available");
                    Serial.PortName = portName;
                    Serial.BaudRate = 9600;

                    cmbSerialPort.Enabled = false;
                    cmbTestMode.Enabled = false;
                    tmSerialCheck.Enabled = true;
                    btnExPortStart.Text = "Stop";
                    btnExPortStart.Tag = true;
                    
                }
                catch (Exception ex)
                {
                    tmSerialCheck.Enabled = false;
                    cmbSerialPort.Enabled = true;
                    cmbTestMode.Enabled = true;
                    btnExPortStart.Text = "Start";
                    btnExPortStart.Tag = null;
                    
                    Serial.Close();
                    ShowMsgLn(ex.Message);
                }
            }
            else
            {
                tmSerialCheck.Enabled = false;
                cmbSerialPort.Enabled = true;
                cmbTestMode.Enabled = true;
                btnExPortStart.Text = "Start";
                btnExPortStart.Tag = null;

                Serial.Close();
                ResetAllLable();
            }
        }

        private void tmSerialCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Serial.IsOpen)
                {
                    return;
                }

                string[] names = SerialPort.GetPortNames();
                if (!names.Contains(cmbSerialPort.SelectedItem as string))
                {
                    Serial.Close();
                    throw new Exception("The serial port is not available");
                }

                if (Serial.IsOpen)
                    Serial.Close();
                Serial.Open();
                
                new Thread(ExportTestTask) { IsBackground = true }.Start(g_TestMode);
            }
            catch (Exception ex)
            {
                ShowMsgLn("异常1:" + ex.Message);
                ResetAllLable();
            }
        }

        #endregion

        #region 拓展盒操作
        private void SetExportState(int index,bool IsPass,string Text)
        {
            Label lb = null;
            switch (index)
            {
                case 0:
                    lb = lbExPort1;
                    break;
                case 1:
                    lb = lbExPort2;
                    break;
                case 2:
                    lb = lbExPort3;
                    break;
                case 3:
                    lb = lbExPort4;
                    break;
                case 4:
                    lb = lbExPort5;
                    break;
                default:
                    lb = null;
                    break;
            }
            if (lb != null)
            {
                if (IsPass)
                {
                    lbSetPass(lb);
                }
                else
                {
                    lbSetFail(lb);
                }
                lbSetText(lb, Text);
            }
        }
        private void ExportTestTask(object Arg)
        {
            try
            {
                string Mode = Arg as string;
                PowerOff();
                if (GetAddr() != 0x00)
                {
                    ShowMsgLn("连接拓展盒失败");
                    lbSetFail(lbExPort1);
                    lbSetFail(lbExPort2);
                    lbSetFail(lbExPort3);
                    lbSetFail(lbExPort4);
                    lbSetFail(lbExPort5);
                }
                else
                {
                    ShowMsgLn("连接拓展盒成功");
                    if (Mode == "接触检测")
                    {
                        while (true)
                        {
                            int cnt = GetConnect();
                            prvCheckGetConnect(cnt);
                        }
                    }
                    else if (Mode == "识别地址")
                    {
                        int cnt = GetConnect();
                        prvCheckGetConnectAddr(cnt);
                    }
                    else
                    {
                        ShowMsgLn("不支持的测试模式:" + Mode);
                    }
                }
                PowerOff();
                /* 保存结果 */
            }
            catch
            {
                return;
            }

        }
        private void prvCheckGetConnect(int cnt)
        {
            if (cnt == -1 || cnt == 0)
            {
                ShowMsgLn("未发现传感器接入");
                for (int i = 0; i < 5; i++)
                {
                    SetExportState(i, false, "未接入传感器");
                }
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                int Index = i;
                if (i == 4)
                    Index = 6;
                if ((cnt & (0x01 << Index)) > 0)
                {
                    SetExportState(i, true, "识别到插入");
                }
                else
                {
                    SetExportState(i, false, "未识别到插入");
                }
            }
        }
        private void prvCheckGetConnectAddr(int cnt)
        {
            if (cnt == -1 || cnt == 0)
            {
                ShowMsgLn("未发现传感器接入");
                for (int i = 0; i < 5; i++)
                {
                    SetExportState(i, false, "未接入传感器");
                }
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                int Index = i;
                if (i == 4)
                    Index = 6;
                if ((cnt & (0x01 << Index)) > 0)
                {
                    if (!PowerOn(i))
                    {
                        ShowMsgLn("打开电源失败");
                        SetExportState(i, false, "打开电源失败");
                        continue;
                    }
                    else
                    {
                        ShowMsgLn("打开电源成功");
                        Thread.Sleep(200);
                    }
                    int Addr = GetAddr();
                    if (Addr == -1)
                    {
                        SetExportState(i, false, "地址获取失败");
                    }
                    else
                    {
                        SetExportState(i, true, "地址:" + Addr.ToString("X2"));
                    }
                }
                else
                {
                    SetExportState(i, false, "未接入传感器");
                }
            }
        }

        public byte[] ToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString = "0" + hexString;
            byte[] returnBytes = new byte[hexString.Length / 2];
            try
            {
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            catch (Exception e)
            {
                MessageBox.Show("请输入符合要求的16进制数据，注意不需要输入0x\r\n");
            }

            return returnBytes;
        }//将16进制数据转换为字节数组
        private bool PowerOff()
        {;
            //0005000100 08 00
            string text = "00050001000000";
            byte[] TxData = ToByteArray(text);
            byte[] CR16 = CRC.CRC16(TxData);
            text += CR16[1].ToString("X2");
            text += CR16[0].ToString("X2");
            TxData = ToByteArray(text);

            ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
            if (!Serial.IsOpen)
            {
                Serial.Open();
            }
            Serial.ReadExisting();
            Serial.Write(TxData, 0, TxData.Length);
            int TimeOut = 0;
            /*  */
            while (Serial.BytesToRead == 0 && TimeOut < 100)
            {
                Thread.Sleep(10);
                TimeOut++;
            }

            if (TimeOut < 100)
            {
                Thread.Sleep(50);
                byte[] RxData = new byte[Serial.BytesToRead];
                Serial.Read(RxData, 0, Serial.BytesToRead);
                ShowMsgLn("485Rx:" + BitConverter.ToString(RxData).Replace("-", " "));
                if (Enumerable.SequenceEqual(TxData, RxData))
                {
                    return true;
                }
                else
                {
                    ShowMsgLn("接收数据错误");
                }
            }
            else
            {
                ShowMsgLn("接收超时");
            }

            return false;
        }
        private bool PowerOn(byte BinSet)
        {
            string text = "0005000100" + BinSet.ToString("X2") + "00";
            byte[] TxData = ToByteArray(text);
            byte[] CR16 = CRC.CRC16(TxData);
            text += CR16[1].ToString("X2");
            text += CR16[0].ToString("X2");
            TxData = ToByteArray(text);

            ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));

            if (!Serial.IsOpen)
            {
                Serial.Open();
            }
            Serial.ReadExisting();
            Serial.Write(TxData, 0, TxData.Length);
            int TimeOut = 0;
            /*  */
            while (Serial.BytesToRead == 0 && TimeOut < 100)
            {
                Thread.Sleep(10);
                TimeOut++;
            }

            if (TimeOut < 100)
            {
                Thread.Sleep(50);
                byte[] RxData = new byte[Serial.BytesToRead];
                Serial.Read(RxData, 0, Serial.BytesToRead);
                ShowMsgLn("485Rx:" + BitConverter.ToString(RxData).Replace("-", " "));
                if (Enumerable.SequenceEqual(TxData, RxData))
                {
                    return true;
                }
                else
                {
                    ShowMsgLn("接收数据错误");
                }
            }
            else
            {
                ShowMsgLn("接收超时");
            }

            return false;
        }
        private bool PowerOn(int Index )
        {
            int PowerIndex;
            if (Index == 4)
            {
                Index = 6;
            }
            PowerIndex = (0x01 << Index);
            //0005000100 08 00
            string text = "0005000100" + PowerIndex.ToString("X2")+"00";
            byte[] TxData = ToByteArray(text);
            byte[] CR16 = CRC.CRC16(TxData);
            text += CR16[1].ToString("X2");
            text += CR16[0].ToString("X2");
            TxData = ToByteArray(text);

            ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
            if (!Serial.IsOpen)
            {
                Serial.Open();
            }
            Serial.ReadExisting();
            Serial.Write(TxData, 0, TxData.Length);
            int TimeOut = 0;
            /*  */
            while (Serial.BytesToRead == 0 && TimeOut < 100)
            {
                Thread.Sleep(10);
                TimeOut++;
            }

            if (TimeOut < 100)
            {
                Thread.Sleep(50);
                byte[] RxData = new byte[Serial.BytesToRead];
                Serial.Read(RxData, 0, Serial.BytesToRead);
                ShowMsgLn("485Rx:" + BitConverter.ToString(RxData).Replace("-", " "));
                if (Enumerable.SequenceEqual(TxData, RxData))
                {
                    return true;
                }
                else
                {
                    ShowMsgLn("接收数据错误");
                }
            }
            else
            {
                ShowMsgLn("接收超时");
            }

            return false;
        }
        private int GetAddr()
        {
            string text = "fe030400000000f53c";
            byte[] TxData = ToByteArray(text);

            ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
            if (!Serial.IsOpen)
            {
                Serial.Open();
            }
            Serial.ReadExisting();
            Serial.Write(TxData, 0, TxData.Length);
            int TimeOut = 0;
            /*  */
            while (Serial.BytesToRead == 0 && TimeOut < 100)
            {
                Thread.Sleep(10);
                TimeOut++;
            }

            if (TimeOut < 100)
            {
                Thread.Sleep(50);
                byte[] RxData = new byte[Serial.BytesToRead];
                Serial.Read(RxData, 0, Serial.BytesToRead);
                ShowMsgLn("485Rx:" + BitConverter.ToString(RxData).Replace("-", " "));
                byte[] crc = CRC.CRC16(RxData);
                if (crc[0] == 0 && crc[1] == 0)
                {
                    if (RxData[0] == 0xfe || RxData[1] == 0x03 || RxData[2] == 0x04)
                    {
                        return RxData[3];
                    }
                    else
                    {
                        ShowMsgLn("接入的传感不符合要求");
                        return -1;
                    }
                }
                else
                {
                    ShowMsgLn("校验不通过,检查内部电阻等元件是否正确");
                    return -1;
                }
            }
            else
            {
                ShowMsgLn("接收超时");
                return -1;
            }
        }

        private int GetConnect()
        {
            string text = "fe030400000000f53c";
            byte[] TxData = ToByteArray(text);

            ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
            if (!Serial.IsOpen)
            {
                Serial.Open();
            }
            Serial.ReadExisting();
            Serial.Write(TxData, 0, TxData.Length);
            int TimeOut = 0;
            /*  */
            while (Serial.BytesToRead == 0 && TimeOut < 100)
            {
                Thread.Sleep(10);
                TimeOut++;
            }

            if (TimeOut < 100)
            {
                Thread.Sleep(50);
                byte[] RxData = new byte[Serial.BytesToRead];
                Serial.Read(RxData, 0, Serial.BytesToRead);
                ShowMsgLn("485Rx:" + BitConverter.ToString(RxData).Replace("-", " "));
                byte[] crc = CRC.CRC16(RxData);
                if (crc[0] == 0 && crc[1] == 0)
                {
                    if (RxData[0] == 0xfe || RxData[1] == 0x03 || RxData[2] == 0x04)
                    {
                        return RxData[4];
                    }
                    else
                    {
                        ShowMsgLn("接入的传感不符合要求");
                        return -1;
                    }
                }
                else
                {
                    ShowMsgLn("校验不通过,检查内部电阻等元件是否正确");
                    return -1;
                }
            }
            else
            {
                ShowMsgLn("接收超时");
                return -1;
            }
        }
        #endregion

        #region 内部测试

        byte g_PowOnBin = 0;
        private void btnPowerOnClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int Index = 0;
            if (btn == btnPowerOn1)
            {
                Index = 0;
            }
            else if (btn == btnPowerOn2)
            {
                Index = 1;
            }
            else if (btn == btnPowerOn3)
            {
                Index = 2;
            }
            else if (btn == btnPowerOn4)
            {
                Index = 3;
            }
            else if (btn == btnPowerOn5)
            {
                Index = 6;
            }

            if (btn.Tag == null)
            {
                g_PowOnBin = (byte)(g_PowOnBin | (0x01 << Index));
            }
            else
            {
                g_PowOnBin = (byte)(g_PowOnBin & ~(0x01 << Index));
            }

            if (PowerOn(g_PowOnBin))
            {

                if (btn.Tag == null)
                {
                    btn.BackColor = Color.Green;
                    btn.Tag = true;
                }
                else
                {
                    btn.BackColor = btn.Parent.BackColor;
                    btn.Tag = null;
                }
            }else
            {
                ShowMsgLn("操作失败");
            }
        }
        private void prvGetAddrClick()
        {
            Serial.Open();
            int Addr = GetAddr();
            if (Addr > -1)
            {
                ShowMsgLn("得到地址:" + Addr.ToString("X2"));
            }
            Serial.Close();
        }
        private void btnGetAddr_Click(object sender, EventArgs e)
        {
            try
            {
                if (Serial.IsOpen)
                {
                    Serial.Close();
                }
                string portName = this.cmbSerialPort.SelectedItem as string;
                if (portName == null || portName.Equals(""))
                    throw new Exception("The serial port is not available");
                Serial.PortName = portName;
                Serial.BaudRate = 9600;
                new Thread(prvGetAddrClick) { IsBackground = true}.Start();
            }
            catch (Exception ex)
            {
                Serial.Close();
                ShowMsgLn(ex.Message);
            }
        }

        #endregion

        private void cmbTestMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbTestMode.SelectedItem as string == "内部使用")
            {
                btnGetAddr.Visible = !btnGetAddr.Visible;
                btnPowerOn1.Visible = !btnPowerOn1.Visible;
                btnPowerOn2.Visible = !btnPowerOn2.Visible;
                btnPowerOn3.Visible = !btnPowerOn3.Visible;
                btnPowerOn4.Visible = !btnPowerOn4.Visible;
                btnPowerOn5.Visible = !btnPowerOn5.Visible;
            }else
            {
                btnGetAddr.Visible = false;
                btnPowerOn1.Visible = false;
                btnPowerOn2.Visible = false;
                btnPowerOn3.Visible = false;
                btnPowerOn4.Visible = false;
                btnPowerOn5.Visible = false;
            }
        }
    }
}
