using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Deployment.Application;
using System.Net;
using System.Net.Sockets;

namespace DeviceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeSerialPorts();
            InitializeSerialId();
            ShowForm();
            //Size = new Size(750, 592);
        }
        #region 护眼黑(未完成)
        private void GetAllControl(Control parent, ref List<Control> CtrlList)
        {
            foreach (Control ctl in parent.Controls)
            {
                if (ctl.HasChildren)
                {
                    CtrlList.Add(ctl);
                    GetAllControl(ctl, ref CtrlList);
                }
                else
                {
                    CtrlList.Add(ctl);
                }
            }
        }
        private void prvHuYanHei()
        {
            List<Control> CtrlList = new List<Control>();
            GetAllControl(this, ref CtrlList);
            Control[] Ctrls = CtrlList.ToArray();
            foreach (Control ctl in Ctrls)
            {
                if (!ctl.HasChildren)
                {
                    ctl.BackColor = Color.Black;
                    ctl.ForeColor = Color.FromArgb(0, 192, 0);
                }
                if (ctl is DataGridView)
                {
                    DataGridView dgv = (DataGridView)ctl;
                    foreach (DataGridViewColumn clm in dgv.Columns)
                    {
                        clm.DefaultCellStyle.BackColor = Color.Black;
                        clm.DefaultCellStyle.ForeColor = Color.FromArgb(0, 192, 0);
                    }
                }
            }
        }
        #endregion

        #region 界面
        formPgmCfg g_cfgForm = new formPgmCfg();
        formExpandHub g_expForm = new formExpandHub();
        form485Mogu g_485MoguForm = new form485Mogu();
        formWater g_WaterForm = new formWater();
        private void ShowForm()
        {
            g_cfgForm.TopLevel = false;
            //g_cfgForm.Parent = tabControl1.TabPages[1];
            g_cfgForm.MsgBox = rtbexMsgBox;
            g_cfgForm.Show();

            g_expForm.TopLevel = false;
            //g_expForm.Parent = tabControl1.TabPages[2];
            g_expForm.Show();

            g_485MoguForm.TopLevel = false;
            //g_485MoguForm.Parent = tabControl1.TabPages[3];
            g_485MoguForm.Show();

            g_WaterForm.TopLevel = false;
            //g_WaterForm.Parent = tabControl1.TabPages[4];
            g_WaterForm.Show();
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
        /**** 更新IP串口 ****/
        private void InitializeSerialId()
        {
            this.cbSerialId.Items.Clear();
            string[] names = System.IO.Ports.SerialPort.GetPortNames();
            if (names.Length != 0)
            {
                this.cbSerialId.Items.AddRange(names);
            }
        }

        private void cmbSerialPort_MouseClick(object sender, MouseEventArgs e)
        {
            InitializeSerialPorts();
            cmbSerialPort.BackColor = System.Drawing.SystemColors.Window;
            //if (cmbStartMode.SelectedIndex < 0)
            //{
            //    cmbStartMode.SelectedItem = "测试设备";
            //}
        }
        #endregion

        #region 启动方式

        #region 烧录任务
        string g_portName;
        int g_StartMode;
        Thread g_thProgram;
        Thread g_thProgramTest;

        public void UpdateIp(string text)
        {
            if (text == "OK")
            {
                Action act1 = new Action(() => {
                    btUpdateStatus.BackColor = Color.Green;
                    btUpdateStatus.Text = "Server update successful";
                });
                btUpdateStatus.BeginInvoke(act1);               
            }
            else
            {
                Action act1 = new Action(() => {
                    btUpdateStatus.BackColor = Color.Red;
                    btUpdateStatus.Text = "Server update failed";
                });
                btUpdateStatus.BeginInvoke(act1);
            }           
        }

        private bool Program()
        {
            List<FirmwareInfomation> firmwares;
            g_cfgForm.InitWidget(txIpAddr, txIpPort, tbDeviceId);
            firmwares = g_cfgForm.GetNextDataToWrite();

            SaveDeviceIdInfo(tbDeviceId.Text, "\r\n"+"IP Address: " + txIpAddr.Text+"\r\n");
            SaveDeviceIdInfo(tbDeviceId.Text, "IP Port: " + txIpPort.Text + "\r\n");
            //else
            //{
            //    if( g_ProDeviceId !=null && g_ProDeviceId!="" && g_ProDeviceId.Length == 16)
            //    {
            //        firmwares = g_cfgForm.GetNewFirmwares(g_ProDeviceId);
            //    }
            //    else
            //    {
            //        firmwares = null;
            //    }
            //}

            if (firmwares == null)
            {
                ShowMsg("无可烧录数据\r\n");
                return false;
            }
            /* 重新插拔的时候需要延时1s,给COM口初始化 */
            Thread.Sleep(1000);
            isp.Init(g_portName, 115200);
            if (!isp.EnterBooloader(ISPProgramerL071.InitialType.DTR_LOW_REBOOT_RTS_HIGH_ENTERBOOTLOADER))
            {
                throw new Exception("初始化失败\r\n");
            }
            if (!isp.ReadInfo())
            {
                throw new Exception("读取芯片信息失败\r\n");
            }
            //if (g_cfgForm.g_xProDataType == formPgmCfg.ProDataType.All)
            //{
            //    EraseChip();
            //}

            if (!isp.EnterBooloader(ISPProgramerL071.InitialType.DTR_LOW_REBOOT_RTS_HIGH_ENTERBOOTLOADER))
            {
                throw new Exception("初始化失败\r\n");
            }

            foreach (FirmwareInfomation item in firmwares)
            {
                ShowMsg("开始烧录" + item.Name + "\r\n");
                bool ret = true;
                if (isp.ChipType == ISPProgramerL071.DensityType.L07X_DENSITY)
                {
                    ret = isp.WriteFlashL072(item.BaseAddress, item.Data);
                }
                else if (isp.ChipType == ISPProgramerL071.DensityType.F207_DENSITY)
                {
                    ret = isp.WriteFlashF207(item.BaseAddress, item.Data);
                }
                else
                {
                    ShowMsg("不支持的芯片\r\n");
                    break;
                }
                if (!ret)
                {
                    ShowMsg("写入出错\r\n");
                    UpdateIp("Fail");
                    SaveDeviceIdInfo(tbDeviceId.Text, "Server update Fail");
                    break;
                }
            }
            UpdateIp("OK");
            ShowMsg("Server update successful");
            SaveDeviceIdInfo(tbDeviceId.Text, "Server update successful\r\n");
            isp.Close();
            return true;
        }

        ISPProgramerL071 isp = new ISPProgramerL071();

        private void ProgramTask()
        {
            try
            {
                ShowMsg("ProgramTask\r\n");
                Program();
                isp.Close();
                g_StartMode = 0;
                tmSerialCheck.Enabled = false;
            }
            catch (Exception ex)
            {
                isp.Close();
                ShowMsg("异常:" + ex.Message + "\r\n");
                ShowMsg("烧录失败，重新烧录需重新配置\r\n");
                UpdateIp("Fail");
                ShowMsg("Update failed\r\n");
                ShowMsg("Please make sure, whether the device is on!!!\r\n");
                SaveDeviceIdInfo(tbDeviceId.Text, "Update failed\r\n");
                g_StartMode = 0;
                tmSerialCheck.Enabled = false;
            }
        }
        #endregion

        #region 烧录测试任务
        private void ProgramTestTask()
        {
            try
            {
                ShowMsg("ProgramTestTask\r\n");
                if (Program())
                {
                    Thread.Sleep(2000);//防止立马打开串口导致boot程序运行失败
                    if (!Serial.IsOpen)
                        Serial.Open();
                    if (g_cfgForm.DeviceType == "04")
                    {
                        Serial.RtsEnable = true;
                        Thread.Sleep(100);
                        Serial.DtrEnable = true;
                        Thread.Sleep(100);
                        Serial.DtrEnable = false;
                        Thread.Sleep(100);
                        g_RxFifo.Clear();
                        Serial.Write("SelfCheck\n");
                    }
                    else
                    {
                        Serial.RtsEnable = true;
                        Thread.Sleep(100);
                        Serial.RtsEnable = false;
                    }

                    ShowMsg("开始检测设备\r\n");
                }
                else
                {
                    ShowMsg("烧录失败，重新烧录需重新配置\r\n");
                }
            }
            catch (Exception ex)
            {
                Serial.Close();
                isp.Close();
                ShowMsg("异常:" + ex.Message + "\r\n");
            }
        }
        #endregion
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Tag == null)
            {
                try
                {
                    //g_StartMode = cmbStartMode.SelectedIndex;
                    g_StartMode = 0;
                    if (g_StartMode < 0)
                    {
                        //throw new Exception("请选择启动模式");
                        ShowMsg("默认测试设备\r\n");
                        g_StartMode = 0;
                        //cmbStartMode.SelectedIndex = 0;
                    }

                    if (g_StartMode != 0)
                    {
                        if (!g_cfgForm.IsCfgDone)
                        {
                            ShowMsg("未配置\r\n");
                            return;
                        }
                    }

                    cmbSerialPort.Enabled = false;
                    //cmbStartMode.Enabled = false;
                    tmSerialCheck.Enabled = true;
                    btnStart.Text = "Stop";
                    btnStart.Tag = true;
                    //if(TextIsDefaultInputOrNull(txtCsqValue))
                    //{
                    //    ShowMsg("未输入信号阈值，默认值为25\r\n");
                    //    txtCsqValue.Text = "25";
                    //}
                }
                catch (Exception ex)
                {
                    tmSerialCheck.Enabled = false;
                    cmbSerialPort.Enabled = true;
                    //cmbStartMode.Enabled = true;
                    btnStart.Text = "Start";
                    btnStart.Tag = null;
                    Serial.Close();
                    ShowMsg(ex.Message + "\r\n");
                }
            }
            else
            {
                tmSerialCheck.Enabled = false;
                cmbSerialPort.Enabled = true;
                //cmbStartMode.Enabled = true;
                btnStart.Text = "Start";
                btnStart.Tag = null;
                Serial.Close();
                ResetAllData();
                ResetAllLable();
                g_RxFifo.Clear();

            }
        }
        private void tmSerialCheck_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Serial.IsOpen)
                {
                    if (g_ShowMsg != null)
                        ShowMsg(g_ShowMsg + "\r\n");
                    return;
                }

                if (g_StartMode != 1)
                {
                    g_portName = this.cmbSerialPort.SelectedItem as string;

                    if (g_portName == null || g_portName.Equals(""))
                        throw new Exception("The serial port is not available");
                    Serial.PortName = g_portName;
                    string[] names = SerialPort.GetPortNames();
                    if (!names.Contains(g_portName))
                        throw new Exception("The serial port is not available");
                }                   

                /* 仅测试 */
                if (g_StartMode == 0)
                {
                    try
                    {
                        Serial.Open();
                        if (g_cfgForm.DeviceType == "04")
                        {
                            Serial.RtsEnable = true;
                            Thread.Sleep(100);
                            Serial.DtrEnable = true;
                            Thread.Sleep(100);
                            Serial.DtrEnable = false;
                            Thread.Sleep(100);
                            g_RxFifo.Clear();
                            Serial.Write("SelfCheck\n");
                        }
                        else
                        {
                            Serial.RtsEnable = true;
                            Thread.Sleep(100);
                            Serial.RtsEnable = false;
                        }
                        ShowMsg("\r\nIf there is no output information, please check whether the device is on\r\n");
                    }
                    catch (Exception ex)
                    {
                        ShowMsg(ex.Message + "\r\n");
                    }
                }
                else
                {
                    //if (g_ProDeviceId == null || g_ProDeviceId.Length != 16)
                    //{
                    //    ShowMsgLn("未知设备号，请扫码");
                    //    return;
                    //}
                }
                /* 仅烧录 */
                if (g_StartMode == 1)
                {
                    g_portName = this.cbSerialId.SelectedItem as string;
                    Serial.PortName = g_portName;
                    string[] names = SerialPort.GetPortNames();
                    if (g_portName == null || g_portName.Equals(""))
                        throw new Exception("The serial port is not available");

                    if (g_thProgram == null)
                    {
                        g_thProgram = new Thread(ProgramTask);
                        g_thProgram.IsBackground = true;
                        g_thProgram.Start();
                    }
                    else
                    {
                        if (!g_thProgram.IsAlive)
                        {
                           ShowMsg("任务执行完毕，拔出USB即可\r\n");
                        }
                    }
                }
                /* 烧录后测试 */
                else if (g_StartMode == 2)
                {
                    if (g_thProgramTest == null)
                    {
                        g_thProgramTest = new Thread(ProgramTestTask);
                        g_thProgramTest.IsBackground = true;
                        g_thProgramTest.Start();
                    }
                    else
                    {
                        if (!g_thProgramTest.IsAlive)
                        {
                            ShowMsg("任务执行完毕，拔出USB即可\r\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMsg("error:" + ex.Message + "\r\n");
                tmSerialCheck.Enabled = false;
                ResetAllLable();
                ResetAllData();
            }
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

        #region 保存信息
        public void SaveDeviceInfo(string text)
        {
            string path = "";
            path = Application.StartupPath + "\\" + DateTime.Now.ToString("yy-MM-dd") + "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = path + DateTime.Now.ToString("yy-MM-dd") + ".txt";

            StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("GBK"));
            writer.Write(text);
            writer.Flush();
            writer.Close();
        }

        public void SaveDeviceIdInfo(string DeviceId, string text)
        {
            string path = "";
            path = Application.StartupPath + "\\" + DateTime.Now.ToString("yy-MM-dd") + "\\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = path + DeviceId + "_update_log.txt";

            StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("GBK"));
            writer.Write(text);
            writer.Flush();
            writer.Close();
        }

        #endregion

        #region 设备信息状态

        string g_ShowMsg;
        string g_DeviceId;
        string g_SoftWare;
        string g_HardWare;
        string g_Ccid;
        string g_Imei;
        string g_Creg;
        bool g_CheckSucceed;
        bool g_CheckDone;

        private void ResetData()
        {
            g_ShowMsg = null;
            g_DeviceId = null;
            g_SoftWare = null;
            g_HardWare = null;
            g_Ccid = null;
            g_Imei = null;
            g_Creg = null;
            g_CheckSucceed = true;
            g_CheckDone = false;
        }
        private void CheckLine(string FlameData)
        {
            /* 必须先删除回车符 */
            FlameData = FlameData.Replace("\r", "").Replace("\n", "");
            ShowMsg(FlameData + "\r\n");
            if(g_CheckDone)
            {
                return;
            }
            try
            {
                string CheckStr = "id:";

                CheckStr = "ID:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {

                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    g_DeviceId = Content;
                    lbSetText(lbDeviceId, Content);
                    if (g_DeviceId.Length == 16)
                    {
                        lbSetPass(lbDeviceId);

                        string gps = Content.Substring(7, 1);
                        if (gps != "1")
                        {
                            //lbSetPass(lbGpsState);
                            //lbSetText(lbGpsState, "无定位");
                        }

                        string DeviceID = g_DeviceId.Substring(0, 2);
                        /* mini1 */
                        if (DeviceID == "00")
                        {
                            //lbSetText(lbSensorCheck1, "空气温度");
                            //lbSetText(lbSensorCheck2, "空气湿度");
                            //lbSetText(lbSensorCheck3, "光照度");
                            //lbSetText(lbSensorCheck4, "无此接口");
                            //lbSetText(lbSensorCheck5, "无此接口");
                            //lbSetText(lbSensorCheck6, "无此接口");
                            //lbSetPass(lbSensorCheck4);
                            //lbSetPass(lbSensorCheck5);
                            //lbSetPass(lbSensorCheck6);
                        }
                        /* mini2 */
                        else if (DeviceID == "10")
                        {
                            //lbSetText(lbSensorCheck1, "空气温度");
                            //lbSetText(lbSensorCheck2, "空气湿度");
                            //lbSetText(lbSensorCheck3, "光照度");
                            //lbSetText(lbSensorCheck4, "无此接口");
                            //lbSetText(lbSensorCheck5, "无此接口");
                            //lbSetText(lbSensorCheck6, "无此接口");
                            //lbSetPass(lbSensorCheck4);
                            //lbSetPass(lbSensorCheck5);
                            //lbSetPass(lbSensorCheck6);
                        }
                        /* pro1 */
                        else if (DeviceID == "01")
                        {
                            //lbSetText(lbSensorCheck1, "温湿度");
                            //lbSetText(lbSensorCheck2, "光照度");
                            //lbSetText(lbSensorCheck3, "485接口");
                            //lbSetText(lbSensorCheck4, "无此接口");
                            //lbSetText(lbSensorCheck5, "无此接口");
                            //lbSetText(lbSensorCheck6, "无此接口");
                            //lbSetText(lbSaveData, "无保存");
                            //lbSetPass(lbSaveData);
                            //lbSetPass(lbSensorCheck4);
                            //lbSetPass(lbSensorCheck5);
                            //lbSetPass(lbSensorCheck6);
                        }
                        /* pro2 */
                        else if (DeviceID == "09")
                        {
                            //lbSetText(lbSensorCheck1, "接口A2");
                            //lbSetText(lbSensorCheck2, "接口A1");
                            //lbSetText(lbSensorCheck3, "接口B");
                            //lbSetText(lbSensorCheck4, "无此接口");
                            //lbSetText(lbSensorCheck5, "无此接口");
                            //lbSetText(lbSensorCheck6, "无此接口");
                            //lbSetPass(lbSensorCheck4);
                            //lbSetPass(lbSensorCheck5);
                            //lbSetPass(lbSensorCheck6);
                        }
                        /* plus */
                        else if (DeviceID == "02")
                        {
                            //lbSetText(lbSensorCheck1, "接口A0");
                            //lbSetText(lbSensorCheck2, "接口A1");
                            //lbSetText(lbSensorCheck3, "接口A2");
                            //lbSetText(lbSensorCheck4, "接口A3");
                            //lbSetText(lbSensorCheck5, "接口A4");
                            //lbSetText(lbSensorCheck6, "接口A5");
                        }
                        /* pro2水产 */
                        else if (DeviceID == "11")
                        {
                            //lbSetText(lbSensorCheck1, "接口A0");
                            //lbSetText(lbSensorCheck2, "接口A1");
                            //lbSetText(lbSensorCheck3, "舵机");
                            //lbSetText(lbSensorCheck4, "无此接口");
                            //lbSetText(lbSensorCheck5, "无此接口");
                            //lbSetText(lbSensorCheck6, "无此接口");
                            //lbSetPass(lbSensorCheck4);
                            //lbSetPass(lbSensorCheck5);
                            //lbSetPass(lbSensorCheck6);
                        }
                        else if (DeviceID == "04")
                        {
                            lbSetText(lbcpin, "空");
                            lbSetText(lbCcid, "空");
                            lbSetPass(lbcpin);
                            lbSetPass(lbCcid);
                            lbSetPass(lbcreg);
                        }
                    }
                    else
                    {
                        lbSetFail(lbDeviceId);
                    }
                }

                #region lora水阀
                CheckStr = "Btn:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    if (Content == "Up")
                    {
                        ShowMsgLn("按钮未按下，请手动按下");
                    }
                    else if (Content == "Down")
                    {
                        ShowMsgLn("按钮已按下，请释放(切莫长按)");
                    }
                    else if (Content == "Ok")
                    {
                        //lbSetPass(lbSensorCheck1);
                    }
                }
                CheckStr = "Lm400:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    if (Content == "Ok")
                    {
                        //lbSetPass(lbSensorCheck4);
                    }
                    else
                    {
                        //lbSetFail(lbSensorCheck4);
                    }
                }

                CheckStr = "Send:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    if (Content == "Ok")
                    {
                        //lbSetPass(lbSensorCheck5);
                    }
                    else
                    {
                        //lbSetFail(lbSensorCheck5);
                    }
                }

                CheckStr = "Ack:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Ok")
                    //{
                    //    lbSetPass(lbSensorCheck6);
                    //}
                    //else
                    //{
                    //    lbSetFail(lbSensorCheck6);
                    //}
                }
                CheckStr = "Valve:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Open")
                    //{
                    //    ShowMsgLn("请确认水阀是否打开...");
                    //    lbSetPass(lbSensorCheck2);
                    //}
                    //else if (Content == "Close")
                    //{
                    //    ShowMsgLn("请确认水阀是否关闭...");
                    //    lbSetPass(lbSensorCheck3);
                    //}
                }
                #endregion

                #region mini1-mini2
                CheckStr = "Temp:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    if (Content == "Err")
                    {
                        //lbSetFail(lbSensorCheck1);
                    }
                    else
                    {
                        //lbSetPass(lbSensorCheck1);
                        //lbSetText(lbSensorCheck1, Content);
                    }
                }

                CheckStr = "Humi:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Err")
                    //{
                    //    lbSetFail(lbSensorCheck2);
                    //}
                    //else
                    //{
                    //    lbSetPass(lbSensorCheck2);
                    //    lbSetText(lbSensorCheck2, Content);
                    //}
                }

                CheckStr = "Lux:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Err")
                    //{
                    //    lbSetFail(lbSensorCheck3);
                    //}
                    //else
                    //{
                    //    lbSetPass(lbSensorCheck3);
                    //    lbSetText(lbSensorCheck3, Content);
                    //}
                }
                #endregion

                #region Pro2-Plus
                CheckStr = "Port";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        CheckStr = "Port";
                        CheckStr = CheckStr + i.ToString() + ":";
                        if (FlameData.IndexOf(CheckStr) == 0)
                        {
                            Label temp = null;
                            //switch(i)
                            //{
                            //    case 0:
                            //        temp = lbSensorCheck1;
                            //        break;
                            //    case 1:
                            //        temp = lbSensorCheck2;
                            //        break;
                            //    case 2:
                            //        temp = lbSensorCheck3;
                            //        break;
                            //    case 3:
                            //        temp = lbSensorCheck4;
                            //        break;
                            //    case 4:
                            //        temp = lbSensorCheck5;
                            //        break;
                            //    case 5:
                            //        temp = lbSensorCheck6;
                            //        break;
                            //    default:break;
                            //}
                            if (temp != null)
                            {
                                string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);

                                if (Content == "ff")
                                {
                                    lbSetFail(temp);
                                }
                                //else if (Content == "0")
                                //{
                                //    lbSetText(temp, "拓展盒");
                                //    lbSetPass(temp);
                                //}
                                else
                                {
                                    lbSetPass(temp);
                                    lbSetText(temp, "地址:" + Content);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Pro1
                CheckStr = "T&H:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Err")
                    //{
                    //    lbSetFail(lbSensorCheck1);
                    //}
                    //else
                    //{
                    //    lbSetPass(lbSensorCheck1);
                    //    lbSetText(lbSensorCheck1, Content);
                    //}
                }

                CheckStr = "ProLux:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Err")
                    //{
                    //    lbSetFail(lbSensorCheck2);
                    //}
                    //else
                    //{
                    //    lbSetPass(lbSensorCheck2);
                    //    lbSetText(lbSensorCheck2, Content);
                    //}
                }
                /* 485接口在Pro2的检测里面做了 */
                #endregion

                #region Pro2水产
                CheckStr = "Motor:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Ok")
                    //{
                    //    lbSetPass(lbSensorCheck3);
                    //}
                    //else
                    //{
                    //    lbSetFail(lbSensorCheck3);
                    //}
                }
                #endregion
                #region 通用

                CheckStr = "SW:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    g_SoftWare = Content;
                    lbSetPass(lbSoftWare);
                    lbSetText(lbSoftWare, Content);
                }

                CheckStr = "HW:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    g_HardWare = Content;
                    //lbSetPass(lbHardWare);
                    //lbSetText(lbHardWare, Content);
                }

                CheckStr = "Bat:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content.IndexOf("Ok") == 0)
                    //{
                    //    lbSetPass(lbChargeState);
                    //    lbSetText(lbChargeState, "充电完成");
                    //}
                    //else if (Content.IndexOf("Ing") == 0)
                    //{
                    //    lbSetPass(lbChargeState);
                    //    lbSetText(lbChargeState, "正在充电");
                    //}
                    //else
                    //{
                    //    lbSetFail(lbChargeState);
                    //    lbSetText(lbChargeState, "充电错误");
                    //}
                }

                CheckStr = "CgV:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //lbSetPass(lbChargeV);
                    //lbSetText(lbChargeV, Content + "V");
                }

                CheckStr = "BatV:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //lbSetPass(lbBatV);
                    //lbSetText(lbBatV, Content + "V");
                }

                CheckStr = "BatP:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    double value = Convert.ToDouble(Content);
                    //if (value > 0)
                    //{
                    //    lbSetPass(lbBatPercent);
                    //}
                    //else
                    //{
                    //    lbSetFail(lbBatPercent);
                    //}
                    //lbSetText(lbBatPercent, "电量" + Content);
                }

                CheckStr = "Spi:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Err")
                    //{
                    //    lbSetFail(lbSaveData);
                    //}
                    //else
                    //{
                    //    lbSetPass(lbSaveData);
                    //}
                }

                CheckStr = "Gps:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    //if (Content == "Ing" || Content == "Ok" || Content == "None")
                    //{
                    //    lbSetPass(lbGpsState);
                    //}
                    //else
                    //{
                    //    lbSetFail(lbGpsState);
                    //}
                    //lbSetText(lbGpsState, CheckStr + Content);
                }

                CheckStr = "Sim:Fail";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    lbSetFail(lbCcid);
                    lbSetFail(lbcpin);
                    lbSetFail(lbSvCnt);
                    lbSetFail(lbCsq);
                    lbSetFail(lbcreg);
                }

                CheckStr = "+CPIN:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    lbSetPass(lbcpin);
                    lbSetText(lbcpin, "+CPIN: " + Content);
                }

                CheckStr = "+CME ERROR:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                   
                    lbSetFail(lbcpin);
                    if (FlameData.Length - CheckStr.Length < 4)
                    {
                        lbSetText(lbcpin, "SIM not inserted");
                    }
                    else
                    {
                        lbSetText(lbcpin, Content);
                    }                  
                }

                CheckStr = "+QCCID:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    g_Ccid = Content;
                    lbSetPass(lbCcid);
                    lbSetText(lbCcid, Content);
                }

                CheckStr = "+CREG:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    g_Creg = Content;

                    int Creg = Convert.ToInt32(Content.Replace(" 0,", ""));

                    if (Creg == 1)
                    {
                        lbSetPass(lbcreg);
                    }
                    else
                    {
                        lbSetFail(lbcreg);
                    }                   
                    lbSetText(lbcreg, "+CREG:" + Content);
                }

                CheckStr = "Connect:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    if (Content == "Ok")
                    {
                        lbSetPass(lbSvCnt);
                        lbSetText(lbSvCnt, "Connect Server Success");
                    }
                    else
                    {
                        lbSetFail(lbSvCnt);
                        lbSetText(lbSvCnt, "Connect Server Fail");
                    }
                }


                CheckStr = "csq:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    if (Content == "Fail")
                    {
                        lbSetText(lbCsq, "无法获取");
                        lbSetFail(lbCsq);
                    }
                    else
                    {
                        int Csq = Convert.ToInt32(Content.Replace(" ", ""));

                        if (Csq == 99 || Csq == 0)
                        {
                            lbSetFail(lbCsq);
                        }
                        else if(Csq > 0)
                        {
                            lbSetPass(lbCsq);
                        }                      
                        lbSetText(lbCsq, "CSQ: " + Csq.ToString());
                    }
                }                

                CheckStr = "+QIACT: ";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);            
                    lbSetPass(lbQiact);
                    lbSetText(lbQiact, Content);                    
                }

                CheckStr = "ReadDataErr:";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    string Content = FlameData.Substring(CheckStr.Length, FlameData.Length - CheckStr.Length);
                    int Port = Convert.ToInt32(Content.Replace(" ",""));
                    Label temp = null;
                    
                    if (temp != null)
                    {
                        lbSetText(temp,"读失败");
                        lbSetFail(temp);
                    }
                }             

                CheckStr = "Ack OK";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    lbSetPass(lbSend);
                    lbSend.BackColor = Color.Green;
                    lbSetText(lbSend, "Send Success");
                }

                CheckStr = "CheckDone";
                if (FlameData.IndexOf(CheckStr) == 0)
                {
                    //g_ShowMsg = "已检测完成";
                    ShowMsg("The device has send data to the server\r\n");
                    g_CheckDone = true;
                    CheckLableInit();
                    DateTime.Now.ToString("HH:mm:ss ");
                    string info = DateTime.Now.ToString("HH-mm-ss  ") + g_DeviceId + "    " + g_SoftWare + "    " + g_HardWare + "    " + g_Imei + "    " + g_Ccid + "    " + g_Creg;
                    if (g_CheckSucceed)
                        info = info + "    PASS\r\n";
                    else
                        info = info + "    FAIL\r\n";
                    SaveDeviceInfo(info);                   
                }
                #endregion
            }
            catch (Exception ex)
            {
                ShowMsg("异常3:" + ex.Message + "\r\n");
            }
        }
        private void CheckLableInit()
        {
            lbInitToFail(lbDeviceId);
            lbInitToFail(lbSoftWare);
            //lbInitToFail(lbHardWare);
            lbInitToFail(lbCcid);
            lbInitToFail(lbcpin);
            lbInitToFail(lbcreg); 
            lbInitToFail(lbQiact);
            lbInitToFail(lbSend);  

            //lbInitToFail(lbChargeState);
            //lbInitToFail(lbChargeV);
            //lbInitToFail(lbGpsState);
            //lbInitToFail(lbBatV);
            //lbInitToFail(lbBatPercent);
            //lbInitToFail(lbSensorCheck1);
            //lbInitToFail(lbSensorCheck2);
            //lbInitToFail(lbSensorCheck3);
            //lbInitToFail(lbSensorCheck4);
            //lbInitToFail(lbSensorCheck5);
            //lbInitToFail(lbSensorCheck6);
            //lbInitToFail(lbSaveData);
            lbInitToFail(lbSvCnt);
            lbInitToFail(lbCsq);
        }
        #endregion

        #region 接收检测
        FiFo g_RxFifo = new FiFo(1024);
        /// <summary>
        /// 数据接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Serial_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort Serial = (SerialPort)sender;
            int n = Serial.BytesToRead;
            byte[] buffer = new byte[n];
            Serial.Read(buffer, 0, n);
            g_RxFifo.Push(buffer);
            SaveDeviceInfo(Encoding.GetEncoding("GB2312").GetString(buffer));
            while (true)
            {
                string LineStr = g_RxFifo.PopLine();
                
                if (LineStr != null)
                {
                    CheckLine(LineStr);
                }
                else
                {
                    break;
                }
            }
        }
        #endregion
        
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
                object a = new object[] { lb.BackColor, lb.Text};
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
            g_CheckSucceed = false;
            lb.BeginInvoke(act1);
        }

        public void lbInitToFail(Label lb)
        {
            object[] a = lb.Tag as object[];
            if (lb.BackColor == (Color)a[0]){
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

        #endregion

        #region 自动更新
        private void CheckUpdate()
        {
            if (ApplicationDeployment.IsNetworkDeployed == true)
            {
                ApplicationDeployment Version = ApplicationDeployment.CurrentDeployment;
                Text = Text + "  v" + Version.CurrentVersion.ToString().Substring(0, 3);
                if (Version.CheckForUpdate() == true)
                {
                    if (MessageBox.Show("检测到有新的版本可以进行更新,现在需要更新吗?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Version.Update();
                        MessageBox.Show("更新完成");
                        Application.Restart();
                    }
                }
            }
            else
            {
                Text = Text + "  非正式发布版本";
            }
        }
        #endregion

        #region 初始配置
        private void Form1_Load(object sender, EventArgs e)
        {
            CheckUpdate();

            lbFirstConfig(lbDeviceId);
            lbFirstConfig(lbSoftWare);
            //lbFirstConfig(lbHardWare);
            lbFirstConfig(lbCcid);
            lbFirstConfig(lbcpin); 
            lbFirstConfig(lbcreg); 
            lbFirstConfig(lbQiact); 
            lbFirstConfig(lbSend);

            //lbFirstConfig(lbChargeState);
            //lbFirstConfig(lbChargeV);
            //lbFirstConfig(lbGpsState);
            //lbFirstConfig(lbBatV);
            //lbFirstConfig(lbBatPercent);
            //lbFirstConfig(lbSensorCheck1);
            //lbFirstConfig(lbSensorCheck2);
            //lbFirstConfig(lbSensorCheck3);
            //lbFirstConfig(lbSensorCheck4);
            //lbFirstConfig(lbSensorCheck5);
            //lbFirstConfig(lbSensorCheck6);
            //lbFirstConfig(lbSaveData);
            lbFirstConfig(lbSvCnt);
            lbFirstConfig(lbCsq);
            //TextFirstCfg(txtCsqValue);
            ProgressBarInit();

            richTextBoxEx1.ShowText("\r\nFile save path：" + Application.StartupPath + "\r\n");
            richTextBoxEx1.ShowText("\r\nSerial Tool operation instructions: \r\n");
            richTextBoxEx1.ShowText("1. Check the connection of  NBI server  before testing!!!\r\n");
            richTextBoxEx1.ShowText("Click the \"checkconnect\" button. \r\n");            
            richTextBoxEx1.ShowText("\r\nIf the NBI server connection is good, the massage--“Server Connect Success!!!” \r\n");
            richTextBoxEx1.ShowText("--will be displayed in the left window. \r\n");
            richTextBoxEx1.ShowText("\r\nIf not , the massage will be “Server Connect fail!!!”\r\n");
            richTextBoxEx1.ShowText("\r\n2. Start BG96 module test.\r\n");
            richTextBoxEx1.ShowText("Make sure the NBI server connection is good, then connect the device to one PC, \r\n");
            richTextBoxEx1.ShowText("select the right PortId and push down the power switch to turn the device on.\r\n");
            richTextBoxEx1.ShowText("Click the \"Start\"bottom, and the test begins\r\n");
            richTextBoxEx1.ShowText("\r\nPlease keep the device running during the test!!!\r\n");
            richTextBoxEx1.ShowText("\r\nThe test will check several module functions as been shown on the left.\r\n");
            richTextBoxEx1.ShowText("If a function is normal, the corresponding label will turn green, otherwise it will turn red.\r\n");
            richTextBoxEx1.ShowText("\r\nThe detail of each function and its output messages are described below:\r\n");
            richTextBoxEx1.ShowText("\r\nCPIN: Check SIM Card Status, If success, it prints “+CPIN: READY”,and the label turns green.\r\n");
            richTextBoxEx1.ShowText("If not, it prints “SIM not inserted”,and the label turns red. Check that whether the SIM card is installed correctly \r\n");

            richTextBoxEx1.ShowText("\r\nQCCID: Get SIM Card Information,  If success, it prints the SIM Card Number,and the label turns green\r\n");
            richTextBoxEx1.ShowText("If not, it prints the SIM Card Number,and the label turns red\r\n");

            richTextBoxEx1.ShowText("\r\nCSQ: Get BG96 Module RSSI, If success, it prints the  RSSI Number,and the label turns green\r\n");
            richTextBoxEx1.ShowText("If not, it prints “99”,and the label turns red.Check that whether the antenna is installed correctly\n");

            richTextBoxEx1.ShowText("\r\nCREG:  Initialize the BG96 and Check the network register status, If the BG96 is successfully Initialized,\r\n");
            richTextBoxEx1.ShowText("the label will turn green,otherwise the label will turn red\r\n");
            richTextBoxEx1.ShowText("The device will keep trying initialize the BG96 till succeed, please keep it running and do not turn it down .\r\n"); 

            richTextBoxEx1.ShowText("\r\nPDP Context: checkthe query context status,If the query context is enabled, the network IP address will be Printed and\r\n");
            richTextBoxEx1.ShowText("the label will turn green,otherwise the label will turn red,and need to check that the network APN is correct\r\n");

            richTextBoxEx1.ShowText("\r\nConnect Server: connect the NBI server. \r\n");
            richTextBoxEx1.ShowText("If the device connect to the NBI server successfully, the label will  turn green, otherwise the label will turn red\r\n");

            richTextBoxEx1.ShowText("\r\nSend Data: Sending data to the NBI server. The test is completed and the device enters sleep mode after\r\n");
            richTextBoxEx1.ShowText("the data has been uploaded to the NBI server successfully.\r\n");

            ShowMsgLn("File save path：" + Application.StartupPath);
            ShowMsgLn("\r\nAttention: ");
            ShowMsgLn("Serial Tool operation instructions: ");             
            ShowMsgLn("Test the NBI server connection before testing the device!!!");
            ShowMsgLn("Click the checkconnect button \r\n");
            ShowMsgLn("If successful, the output server connection was successful \r\n");            
        }
        #endregion
        
        #region 复位
        private void ResetAllLable()
        {
            lbReset(lbDeviceId);
            lbReset(lbSoftWare);
            //lbReset(lbHardWare);
            lbReset(lbCcid);
            lbReset(lbcpin);
            lbReset(lbcreg); 
            lbReset(lbQiact);
            lbReset(lbSend);

            //lbReset(lbChargeState);
            //lbReset(lbChargeV);
            //lbReset(lbBatV);
            //lbReset(lbBatPercent);
            //lbReset(lbGpsState);
            //lbReset(lbSensorCheck1);
            //lbReset(lbSensorCheck2);
            //lbReset(lbSensorCheck3);
            //lbReset(lbSensorCheck4);
            //lbReset(lbSensorCheck5);
            //lbReset(lbSensorCheck6);
            //lbReset(lbSaveData);
            lbReset(lbSvCnt);
            lbReset(lbCsq);
        }

        private void ResetAllData()
        {
            if (g_thProgram != null)
            {
                g_thProgram.Abort();
                g_thProgram = null;
            }
            if (g_thProgramTest != null)
            {
                g_thProgramTest.Abort();
                g_thProgramTest = null;
            }
            g_ProDeviceId = "";
            ResetData();
        }
        #endregion

        #region 进度条
        private void ProgressBarInit()
        {
            this.isp.ProgressChanged += this.ProgressChanged;
        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //if (this.progressBar.InvokeRequired)
            //{
            //    this.progressBar.BeginInvoke(new Action(() => this.progressBar.Value = e.ProgressPercentage));
            //}
            //else
            //{
            //    this.progressBar.Value = e.ProgressPercentage;
            //}

            if (e.UserState != null && e.UserState is string)
            {
                string str = e.UserState as string;
                ShowMsg(str);
            }
        }
        #endregion
        
        #region 擦除设备
        private bool ReadProtect()
        {
            if (Serial.IsOpen)
                Serial.Close();

            if (!isp.EnterBooloader(ISPProgramerL071.InitialType.DTR_LOW_REBOOT_RTS_HIGH_ENTERBOOTLOADER))
            {
                ShowMsg("串口初始化失败\r\n");
                return false;
            }
            else
            {
                if (isp.SendCommand(0x82))
                {
                    ShowMsg("上锁成功\r\n");
                }
                else
                {
                    ShowMsg("上锁失败\r\n");
                    return false;
                }
            }
            Thread.Sleep(100);
            isp.Close();
            return true;
        }

        private bool ReadUnProtect()
        {
            if (Serial.IsOpen)
                Serial.Close();

            if (!isp.EnterBooloader(ISPProgramerL071.InitialType.DTR_LOW_REBOOT_RTS_HIGH_ENTERBOOTLOADER))
            {
                ShowMsg("串口初始化失败\r\n");
                return false;
            }
            if (isp.SendCommand(0x92))
            {
                ShowMsg("解锁成功\r\n");
            }
            else
            {
                ShowMsg("解锁失败\r\n");
                return false;
            }
            Thread.Sleep(100);//不能删除此等待
            isp.Close();
            return true;
        }

        private void EraseChip()
        {
            if (Serial.IsOpen)
                Serial.Close();

            ShowMsg("擦除芯片\r\n");
            if (this.isp.ChipType == ISPProgramerL071.DensityType.L07X_DENSITY)
            {
                Thread.Sleep(100);
                if (ReadUnProtect())
                {
                    Thread.Sleep(100);
                    if (ReadProtect())
                    {
                        Thread.Sleep(100);
                        if (ReadUnProtect())
                        {
                            isp.Close();
                            ShowMsg("擦除成功\r\n");
                        }
                    }
                }
            }
            else if (this.isp.ChipType == ISPProgramerL071.DensityType.F207_DENSITY)
            {
                ShowMsg("需要等待20秒\r\n");
                if (this.isp.EraseChip())
                {
                    ShowMsg("擦除成功\r\n");
                }
            }
            else
            {
                throw new Exception("芯片类型错误\r\n");
            }
        }
        #endregion
        

        #region 信号强度阈值

        private void txtCsqValue_Leave(object sender, EventArgs e)
        {
            txtLeave(sender,e);
            //txtCsqValue.Visible = false;
        }

        private void lbCsq_Click(object sender, EventArgs e)
        {
            //txtCsqValue.Visible = true;
        }


        #endregion

        #region 扫码烧录
        public bool IsHexadecimal(string str)
        {
            
            string PATTERN = "^[0-9A-Fa-f]+$";
           return System.Text.RegularExpressions.Regex.IsMatch(str, PATTERN);
            
        }
        string g_Input="";
        string g_ProDeviceId="";

        public ManualResetEvent TimeoutObject { get; private set; }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    //g_Input = "http://we.qq.com/d/AQAJ7eC_4C6ncCN0x-2pkqcYjaJOInlJDjlSJr8R#{"physical_sn":"0914021121770090"}
                    g_Input = "http://we.qq.com/d/AQAJ7eC_wdL6gfy0LW6ZUgjmONDTaza3BlvjfHAN#{\"physical_sn\":\"10090110527B0002\"}";
                    //ShowMsgLn("Input:" + g_Input);
                    string [] InputArray = g_Input.Split('\"');
                    if (InputArray != null && InputArray.Count() == 5)
                    {
                        ResetAllLable();
                        ResetAllData();
                        g_ProDeviceId = InputArray[3];
                        ShowMsgLn("得到设备号:" + g_ProDeviceId);

                        if (g_ProDeviceId.Length != 16)
                        {
                            g_ProDeviceId = "";
                            ShowMsgLn("设备号长度不足");
                        }
                        else
                        {
                            if (!IsHexadecimal(g_ProDeviceId))
                            {
                                g_ProDeviceId = "";
                                ShowMsgLn("设备号格式错误");
                            }
                        }
                        g_Input = "";
                    }
                }
                else
                {
                    g_Input += e.KeyChar.ToString();
                }
            }catch(Exception ex)
            {
                ShowMsgLn(ex.Message);
            }

        }
        #endregion
        

        private void lbDoubleClick(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            Clipboard.SetText(lb.Text);
            ShowMsgLn("已复制到剪切板:" + lb.Text);
        }

        private void ProgressBar_Click(object sender, EventArgs e)
        {

        }

        public int GetSubStringCount(string str, string substring)
        {
            int count = 0;
            while (true)
            {
                int index = str.IndexOf(substring);
                if (index > -1)
                {
                    count++;
                    str = str.Substring(index + substring.Length);
                }
                else
                {
                    return count;
                }
            }
        }
        public bool ConnectServer(string ip, string port)
        {
            if (GetSubStringCount(ip, ".") != 3)
                return false;
            TimeoutObject = new ManualResetEvent(false);
            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint endpoint = new IPEndPoint(ipaddress, int.Parse(port));

            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            sck.BeginConnect(endpoint, new AsyncCallback(CallBackMethod), null);
            if (TimeoutObject.WaitOne(1500, false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CallBackMethod(IAsyncResult asyncresult)
        {
            TimeoutObject.Set();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ipStr = tbIPAddr.Text.Replace(" ", "");
                if (tbIPAddr.Text == "")
                {
                    ShowMsgLn("IP Adress Error!!!");
                }
                IPAddress ip;
                if (!IPAddress.TryParse(ipStr, out ip))
                {
                    string domain = ipStr.Replace("http://", "").Replace("https://", "").Trim();
                    IPHostEntry hostEntry = Dns.GetHostEntry(domain);
                    IPEndPoint ipEndPoint = new IPEndPoint(hostEntry.AddressList[0], 0);
                    ipStr = ipEndPoint.Address.ToString();
                }

                if (ConnectServer(ipStr, tbIPPort.Text))
                {
                    ShowMsgLn("Server Connect Sucess!!!");
                    SaveDeviceInfo("Server Connect Sucess!!!\r\n");
                    btConnect.BackColor = Color.Green;
                }
                else
                {
                    ShowMsgLn("Server Connect Fail!!!");
                    SaveDeviceInfo("Server Connect Fail!!!\r\n");
                    btConnect.BackColor = Color.Red;
                }

            }
            catch (Exception ex)
            {
                ShowMsgLn("Server Connect Fail!!!");
                SaveDeviceInfo("Server Connect Fail!!!\r\n");
            }
        }

        private void CbSerialId_MouseClick(object sender, MouseEventArgs e)
        {
            InitializeSerialId();
            cbSerialId.BackColor = System.Drawing.SystemColors.Window;
        }

        private void BtUpdateIP_Click_1(object sender, EventArgs e)
        {
            g_StartMode = 1;
            tmSerialCheck.Enabled = true;
            ShowMsg("Start updating the server address\r\n");
            ResetAllData();
            btUpdateStatus.BackColor = System.Drawing.SystemColors.Window;
            btUpdateStatus.Text = "Update Status";
        }
    }
}
