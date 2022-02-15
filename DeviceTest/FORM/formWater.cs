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
    public partial class formWater : Form
    {
        public formWater()
        {
            InitializeComponent();
        }

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
            if (lb == lbShuiYa)
            {
                chartData.Series["水压"].Enabled = !chartData.Series["水压"].Enabled;
            }
            else if (lb == lbLiuSu)
            {
                chartData.Series["流速"].Enabled = !chartData.Series["流速"].Enabled;
            }
            else if (lb == lbZongLiuLiang)
            {
                chartData.Series["总流量"].Enabled = !chartData.Series["总流量"].Enabled;
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
                if (chartData.Series["水压"].Points.Count > 20 - 1)
                {
                    chartData.Series["水压"].Points.RemoveAt(0);
                    chartData.Series["流速"].Points.RemoveAt(0);
                    chartData.Series["总流量"].Points.RemoveAt(0);
                }

                chartData.Series["水压"].Points.AddY(Value[0]);
                chartData.Series["流速"].Points.AddY(Value[1]);
                chartData.Series["总流量"].Points.AddY(Value[2]);
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
                chart.ChartAreas[0].CursorX.SetCursorPixelPosition(new PointF(e.X, e.Y), true);
                int PointIndex = (int)chart.ChartAreas[0].CursorX.Position - 1;
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
                else
                {
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
            lbReset(lbShuiYa);
            lbReset(lbLiuSu);
            lbReset(lbZongLiuLiang);
        }

        #endregion
        private void formWater_Load(object sender, EventArgs e)
        {
            lbFirstConfig(lbShuiYa);
            lbFirstConfig(lbLiuSu);
            lbFirstConfig(lbZongLiuLiang);
        }
        #region 检测任务
        Thread g_Task = null;
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Tag == null)
            {
                try
                {
                    string portName = this.cmbSerialPort.SelectedItem as string;
                    if (portName == null || portName.Equals(""))
                        throw new Exception("串口不可用");
                    Serial.PortName = portName;
                    Serial.BaudRate = 9600;

                    btnResetAll.Enabled = false;
                    cmbSerialPort.Enabled = false;
                    tmSerialCheck.Enabled = true;
                    btnStart.Text = "停止";
                    btnStart.Tag = true;
                    ResetAllLable();
                    chartData.Series["水压"].Points.Clear();
                    chartData.Series["流速"].Points.Clear();
                    chartData.Series["总流量"].Points.Clear();
                }
                catch (Exception ex)
                {
                    btnResetAll.Enabled = true;
                    tmSerialCheck.Enabled = false;
                    cmbSerialPort.Enabled = true;
                    btnStart.Text = "启动";
                    btnStart.Tag = null;

                    Serial.Close();
                    ShowMsgLn(ex.Message);
                }
            }
            else
            {
                btnResetAll.Enabled = true;
                tmSerialCheck.Enabled = false;
                cmbSerialPort.Enabled = true;
                btnStart.Text = "启动";
                btnStart.Tag = null;

                Serial.Close();
                if (g_Task != null)
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
                    throw new Exception("串口不可用");
                }

                if (Serial.IsOpen)
                    Serial.Close();
                Serial.Open();
                if (g_Task != null)
                {
                    g_Task.Abort();
                }
                g_Task = new Thread(SensorTask) { IsBackground = true };
                g_Task.Start();

            }
            catch (Exception ex)
            {
                ShowMsgLn("异常1:" + ex.Message);
                ResetAllLable();
                Action act = new Action(() =>
                {
                    chartData.Series["水压"].Points.Clear();
                    chartData.Series["流速"].Points.Clear();
                    chartData.Series["总流量"].Points.Clear();
                });
                chartData.BeginInvoke(act);
            }
        }

        #region 传感器操作
        private void ExcuteOne(int Addr)
        {
            double[] Data = GetData(Addr);
            if (Data != null && Data.Count() == 3)
            {
                lbSetPass(lbShuiYa);
                lbSetText(lbShuiYa, Data[0].ToString("f2"));
                lbSetPass(lbLiuSu);
                lbSetText(lbLiuSu, Data[1].ToString("f2"));
                lbSetPass(lbZongLiuLiang);
                lbSetText(lbZongLiuLiang, Data[2].ToString("f2"));

                SetData(Data);
            }
            else
            {
                lbSetFail(lbShuiYa);
                lbSetFail(lbLiuSu);
                lbSetFail(lbZongLiuLiang);
                lbSetText(lbShuiYa, "读取失败");
                lbSetText(lbLiuSu, "读取失败");
                lbSetText(lbZongLiuLiang, "读取失败");
                return;
            }
            /* 保存结果 */
        }
        private void SensorTask()
        {
            try
            {
                int Addr = GetAddr();
                ShowMsgLn("接入地址："+Addr.ToString("X2"));
                if (Addr == 0xFC)
                {
                    ShowMsgLn("水压水流传感器");
                }

                while(true)
                {
                    /* 执行一次 */
                    ExcuteOne(Addr);
                    Thread.Sleep(500);
                }
                    
            }
            catch (ThreadAbortException)
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
        private byte[] prvGetData(string SendCmd,int TimeoutMS = 1000)
        {
            try
            {
                byte[] TxData = ToByteArray(SendCmd);

                ShowMsgLn("485Tx:" + BitConverter.ToString(TxData).Replace("-", " "));
                Serial.ReadExisting();
                Serial.Write(TxData, 0, TxData.Length);
                int TimeOut = 0;
                /*  */
                while (Serial.BytesToRead == 0 && TimeOut < TimeoutMS / 10)
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
            byte[] Data = prvGetData("fe030400000000f53c");
            if (Data != null && (Data[0] == 0xfe || Data[1] == 0x03 || Data[2] == 0x04))
            {
                return Data[3];
            }
            else
            {
                return -1;
            }
        }
        private double[] GetData(int Addr)
        {
            try
            {
                string text = Addr.ToString("X2") + "0300000006";
                byte[] TxData = ToByteArray(text);
                byte[] crc16 = CRC.CRC16(TxData);
                byte[] Cmd = new byte[TxData.Length + 2];
                Array.Copy(TxData, 0, Cmd, 0, TxData.Length);
                Cmd[TxData.Length] = crc16[1];
                Cmd[TxData.Length+1] = crc16[0];

                byte[] RxData = prvGetData(BitConverter.ToString(Cmd).Replace("-", ""),3000);

                List<double> ret = new List<double>();
                if(RxData!=null)
                { 
                    int iVal = 0;
                    iVal |= RxData[3] << 24;
                    iVal |= RxData[4] << 16;
                    iVal |= RxData[5] << 8;
                    iVal |= RxData[6] << 0;
                    ret.Add(iVal);
                    iVal = 0;
                    iVal |= RxData[7] << 24;
                    iVal |= RxData[8] << 16;
                    iVal |= RxData[9] << 8;
                    iVal |= RxData[10] << 0;
                    ret.Add(iVal);
                    iVal = 0;
                    iVal |= RxData[11] << 24;
                    iVal |= RxData[12] << 16;
                    iVal |= RxData[13] << 8;
                    iVal |= RxData[14] << 0;
                    ret.Add(iVal);
                }
                else
                {
                    ShowMsgLn("接收超时");
                    return null;
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

        private void btnResetAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Serial.IsOpen)
                    Serial.Close();
                string portName = this.cmbSerialPort.SelectedItem as string;
                if (portName == null || portName.Equals(""))
                    throw new Exception("串口不可用");
                Serial.PortName = portName;
                Serial.BaudRate = 9600;
                Serial.Open();
                int Addr = GetAddr();
                if (Addr != -1)
                {
                    string text = Addr.ToString("X2") + "0500000001";
                    byte[] TxData = ToByteArray(text);
                    byte[] crc16 = CRC.CRC16(TxData);
                    byte[] Cmd = new byte[TxData.Length + 2];
                    Array.Copy(TxData, 0, Cmd, 0, TxData.Length);
                    Cmd[TxData.Length] = crc16[1];
                    Cmd[TxData.Length + 1] = crc16[0];

                    byte[] RxData = prvGetData(BitConverter.ToString(Cmd).Replace("-", ""), 3000);
                    if (RxData!=null)
                    {
                        ShowMsgLn("恢复出厂成功");
                    }
                    else
                    {
                        ShowMsgLn("恢复出厂失败");
                    }
                }
                else
                {
                    ShowMsgLn("传感器连接失败");
                }
                Serial.Close();
            }
            catch(Exception ex)
            {
                ShowMsgLn("恢复出厂失败:"+ ex.Message);
                Serial.Close();
            }
        }
    }
}
