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
using System.Windows.Forms.DataVisualization.Charting;

namespace DeviceTest
{
    public partial class form485Mogu : Form
    {
        public form485Mogu()
        {
            InitializeComponent();
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
            Action act1 = new Action(() =>
            {
                lb.BackColor = Color.Green;
            });
            lb.BeginInvoke(act1);
        }

        public void lbSetFail(Label lb)
        {
            Action act1 = new Action(() =>
            {
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
            Action act1 = new Action(() =>
            {
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
            lbReset(lbSensorId);
            lbReset(lbWendu);
            lbReset(lbShidu);
            lbReset(lbLux);
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
            if(cmbTestMode.SelectedItem==null)
            cmbTestMode.SelectedItem = "生产测试";
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


        #region 图表显示数据
        private void lbMouseClick(object sender, MouseEventArgs e)
        {
            Label lb = sender as Label;
            if (lb == lbWendu)
            {
                chartData.Series["温度"].Enabled = !chartData.Series["温度"].Enabled;
            }
            else if (lb == lbShidu)
            {
                chartData.Series["湿度"].Enabled = !chartData.Series["湿度"].Enabled;
            }
            else if (lb == lbLux)
            {
                chartData.Series["光照"].Enabled = !chartData.Series["光照"].Enabled;
            }
            else
            {

            }
            double Max = double.MinValue;
            double Min = double.MaxValue;
            foreach (Series se in chartData.Series)
            {
                if (se.Enabled)
                {
                    foreach (DataPoint p in se.Points)
                    {
                        if (Max < p.YValues[0])
                        {
                            Max = p.YValues[0];
                        }
                        if (Min > p.YValues[0])
                        {
                            Min = p.YValues[0];
                        }
                    }
                }
            }
            /* 确保有数据 */
            if (Max > Min)
            {
                double step = (Max - Min) / 10;
                //step = step > 1 ? step : 0.01;
                chartData.ChartAreas[0].AxisY.LabelStyle.Format = step > 1 ? "N0" : "N2";
                chartData.ChartAreas[0].AxisY.IntervalOffset = step;
                chartData.ChartAreas[0].AxisY.IsLabelAutoFit = false;
                chartData.ChartAreas[0].AxisY.Maximum = Max + step;
                chartData.ChartAreas[0].AxisY.Minimum = Min - step;
            }
        }

        private void SetData(double[] Value)
        {
            Action act = new Action(() =>
            {
                chartData.ChartAreas["ChartArea1"].AxisY.IsLabelAutoFit = true;
                if (chartData.Series["湿度"].Points.Count > 20 - 1)
                {
                    chartData.Series["温度"].Points.Clear();
                    chartData.Series["湿度"].Points.Clear();
                    chartData.Series["光照"].Points.Clear();
                }

                chartData.Series["温度"].Points.AddY(Value[0]);
                chartData.Series["湿度"].Points.AddY(Value[1]);
                chartData.Series["光照"].Points.AddY(Value[2]);
            });
            chartData.BeginInvoke(act);
        }

        private void chartMouseMove(object sender, MouseEventArgs e)
        {
            Chart chart = sender as Chart;
            //ShowMsgLn(chart.ChartAreas[0].AxisX.GetPosition(1).ToString());
            //if(chart.ChartAreas[0].CursorX.Position. )
            
            if (chart != null)
            {
                chart.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(e.X,e.Y), true);
                int PointIndex = (int)chart.ChartAreas[0].CursorX.Position-1;
                string str = "";
                for (int i = 0; i < chart.Series.Count(); i++)
                {
                    if (chart.Series[i].Enabled)
                    {
                        double value;
                        if (PointIndex < chart.Series[i].Points.Count && PointIndex > -1)
                        {
                            value = chart.Series[i].Points[PointIndex].YValues[0];
                            str += chart.Series[i].Name + ":" + value.ToString() + "\r\n";
                        }
                    }
                }
                if (str != "")
                {
                    str.Remove(str.Length - 2, 2);
                    /* 置顶显示 */
                    lbShowData.Visible = true;
                    lbShowData.Text = str;

                    lbShowData.Parent = chart.Parent;
                    lbShowData.BringToFront();

                    int ShowX = e.X + chart.Location.X + 5;
                    int ShowY = e.Y + chart.Location.Y + 5;

                    if (e.X + 5 + lbShowData.Size.Width >= chart.Size.Width)
                    {
                        ShowX -= lbShowData.Size.Width;
                        ShowX -= 10;
                    }
                    if (e.Y + 5 + lbShowData.Size.Height >= chart.Size.Height)
                    {
                        ShowY -= lbShowData.Size.Height;
                        ShowY -= 10;
                    }

                    lbShowData.Location = new Point(ShowX, ShowY);
                }
                else {
                    lbShowData.Visible = false;
                }
            }
        }
        private void chartMouseLeave(object sender, EventArgs e)
        {
            /* 隐藏显示 */
            lbShowData.Visible = false;
            var chart = sender as Chart;
            chart.ChartAreas[0].CursorX.Position = -1;
        }
        #endregion


        #region 检测任务
        string g_TestMode = "";
        Thread g_Task = null;
        private void btnExPortStart_Click(object sender, EventArgs e)
        {
            if (btnExPortStart.Tag == null)
            {
                try
                {
                    string portName = this.cmbSerialPort.SelectedItem as string;
                    if (portName == null || portName.Equals(""))
                        throw new Exception("The serial port is not available");
                    Serial.PortName = portName;
                    Serial.BaudRate = 9600;


                    g_TestMode = cmbTestMode.SelectedItem as string;

                    cmbSerialPort.Enabled = false;
                    cmbTestMode.Enabled = false;
                    tmSerialCheck.Enabled = true;
                    btnExPortStart.Text = "Stop";
                    btnExPortStart.Tag = true;
                    ResetAllLable();
                    chartData.Series["温度"].Points.Clear();
                    chartData.Series["湿度"].Points.Clear();
                    chartData.Series["光照"].Points.Clear();
                }
                catch (Exception ex)
                {
                    tmSerialCheck.Enabled = false;
                    cmbSerialPort.Enabled = true;
                    cmbTestMode.Enabled = true;
                    btnExPortStart.Text = "启动";
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
                btnExPortStart.Text = "启动";
                btnExPortStart.Tag = null;

                Serial.Close();
                if(g_Task!=null)
                {
                    g_Task.Abort();
                    g_Task = null;
                }
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
                if (g_Task != null)
                {
                    g_Task.Abort();
                }
                g_Task = new Thread(MoguTestTask) { IsBackground = true };
                g_Task.Start(g_TestMode);

            }
            catch (Exception ex)
            {
                ShowMsgLn("异常1:" + ex.Message);
                ResetAllLable();
                Action act = new Action(() =>
                {
                    chartData.Series["温度"].Points.Clear();
                    chartData.Series["湿度"].Points.Clear();
                    chartData.Series["光照"].Points.Clear();
                });
                chartData.BeginInvoke(act);
            }
        }

        #region 蘑菇操作
        private void ExcuteOne()
        {
            double[] Data = GetData();
            if (Data != null && Data.Count() == 3)
            {
                lbSetPass(lbWendu);
                lbSetText(lbWendu, Data[0].ToString("f2"));
                lbSetPass(lbShidu);
                lbSetText(lbShidu, Data[1].ToString("f2"));
                lbSetPass(lbLux);
                lbSetText(lbLux, Data[2].ToString("f2"));

                SetData(Data);
            }
            else
            {
                lbSetFail(lbWendu);
                lbSetFail(lbShidu);
                lbSetFail(lbLux);
                lbSetText(lbWendu, "读取失败");
                lbSetText(lbShidu, "读取失败");
                lbSetText(lbLux, "读取失败");
                return;
            }
            /* 保存结果 */
        }
        private void MoguTestTask(object Arg)
        {
            try
            {
                string Mode = Arg as string;
                if (GetAddr() != 0xFD)
                {
                    ShowMsgLn("连接蘑菇失败");
                }
                else
                {
                    ShowMsgLn("连接蘑菇成功");
                    Thread.Sleep(1000);
                    string ID = GetID();
                    if (ID != "")
                    {
                        lbSetPass(lbSensorId);
                        lbSetText(lbSensorId, ID);
                    }
                    else
                    {
                        lbSetFail(lbSensorId);
                        lbSetText(lbSensorId, "ID读取失败");
                    }
                    if (Mode == "生产测试")
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            /* 执行一次 */
                            ExcuteOne();
                            Thread.Sleep(500);
                        }
                    }
                    else if (Mode == "老化测试")
                    {
                        while (true)
                        {
                            ExcuteOne();
                            Thread.Sleep(1000);
                        }
                    }else
                    {
                        ShowMsgLn("不支持的测试模式");
                    }
                }
            }
            catch(ThreadAbortException)
            {
                return;
            }
            catch (Exception ex)
            {
                ShowMsgLn("异常:" + ex.Message);
                return;
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
        private byte[] prvGetData(string SendCmd)
        {
            try
            {
                byte[] TxData = ToByteArray(SendCmd);

                ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
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
                        return RxData;
                    }
                    else
                    {
                        ShowMsgLn("校验不通过,检查内部电阻等元件是否正确");
                        return null;
                    }
                }
                else
                {
                    ShowMsgLn("接收超时");
                    return null;
                }
            }
            catch (Exception ex)
            {
                ShowMsgLn("异常：" + ex.Message);
                return null;
            }
        }
        private int GetAddr()
        {
            byte [] Data = prvGetData("fe030400000000f53c");
            if (Data[0] == 0xfe || Data[1] == 0x03 || Data[2] == 0x04)
            {
                return Data[3];
            }
            else
            {
                return -1;
            }
        }
        private string GetID()
        {
            string ID = "";
            byte[] Data = prvGetData("FD0380FB0001C807");
            if (Data != null)
            {
                if (Data[0] == 0xfD || Data[1] == 0x03 || Data[2] == 0x02)
                {
                    ID += Data[3].ToString("X2");
                    ID += Data[4].ToString("X2");
                }
            }
            Data = prvGetData("FD0380FC000179C6");
            if (Data != null)
            {
                if (Data[0] == 0xfD || Data[1] == 0x03 || Data[2] == 0x02)
                {
                    ID += Data[3].ToString("X2");
                    ID += Data[4].ToString("X2");
                }
            }
            Data = prvGetData("FD0380FD00012806");
            if (Data != null)
            {
                if (Data[0] == 0xfD || Data[1] == 0x03 || Data[2] == 0x02)
                {
                    ID += Data[3].ToString("X2");
                    ID += Data[4].ToString("X2");
                }
            }
            if (ID.Length != 12)
                return "";
            return ID;
        }
        private double[] GetData()
        {
            try
            {
                string text = "FD03000000045035";
                byte[] TxData = ToByteArray(text);
                List<double> ret = new List<double>();

                ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
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
                    //FD 03 08 09 C3 1E D2 00 00 16 14 2F 21 
                    byte[] crc = CRC.CRC16(RxData);
                    if (crc[0] == 0 && crc[1] == 0)
                    {
                        if (RxData[0] == 0xFD && RxData.Length == 13)
                        {
                            int iVal = (int)((RxData[3] << 8) | RxData[4]);
                            double fValue = iVal / 100.0;
                            ret.Add(fValue);
                            iVal = (int)((RxData[5] << 8) | RxData[6]);
                            fValue = iVal / 100.0;
                            ret.Add(fValue);
                            iVal = (int)((RxData[7] << 24) | (RxData[8] << 16) | (RxData[9] << 8) | RxData[10]);
                            fValue = iVal / 100.0;
                            ret.Add(fValue);
                        }
                        else
                        {
                            ShowMsgLn("数据接收错误");
                        }
                    }
                    else
                    {
                        ShowMsgLn("校验不通过,检查内部电阻等元件是否正确");
                    }
                }
                else
                {
                    ShowMsgLn("接收超时");
                }
                return ret.ToArray();
            }
            catch (Exception ex)
            {
                ShowMsgLn("异常：" + ex.Message);
                return null;
            }
        }
        #endregion

        #endregion

        private void form485Mogu_Load(object sender, EventArgs e)
        {
            lbFirstConfig(lbSensorId);
            lbFirstConfig(lbWendu);
            lbFirstConfig(lbShidu);
            lbFirstConfig(lbLux);
        }

        private void lbSensorId_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(lbSensorId.Text);
            ShowMsgLn("已复制到剪切板:"+ lbSensorId.Text);
        }
    }
}
