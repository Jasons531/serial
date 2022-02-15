using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DeviceTest
{
    public partial class formPgmCfg : Form
    {
        public enum ProDataType
        {
            None,
            OnlyFile,
            OnlyCfg,
            All,
            
        }
        public formPgmCfg()
        {
            InitializeComponent();
        }
        public RichTextBoxEx MsgBox;
        public string DeviceType;
        public List<string> g_DevIdList;
        public int StartIndex;
        public bool IsCfgDone;
        public ProDataType g_xProDataType ;

        public void InitWidget(TextBox _txtIpAddr, TextBox _txtIpPort, TextBox _tbDeviceId)
        {
            txtIpAddr = _txtIpAddr;
            txtIpPort = _txtIpPort;
            tbDeviceId = _tbDeviceId;
        }

        private void DataInit()
        {
            g_DevIdList = new List<string>() ;
            StartIndex = 0;
            DeviceType = null;
            //g_xProDataType = ProDataType.None;
            IsCfgDone = false;
        }

        #region 导入设备号
        //private System.Data.DataTable ImportExcel(string path)
        //{
        //    DataSet ds = new DataSet();
        //    string strConn = "";
        //    if (Path.GetExtension(path) == ".xls")
        //    {
        //        strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", path);
        //    }
        //    else
        //    {
        //        strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", path);
        //    }
        //    using (var oledbConn = new OleDbConnection(strConn))
        //    {
        //        oledbConn.Open();
        //        var sheetName = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new[] { null, null, null, "Table" });
        //        var sheet = new string[sheetName.Rows.Count];
        //        for (int i = 0, j = sheetName.Rows.Count; i < j; i++)
        //        {
        //            sheet[i] = sheetName.Rows[i]["TABLE_NAME"].ToString();
        //        }
        //        var adapter = new OleDbDataAdapter(string.Format("select * from [{0}]", sheet[0]), oledbConn);
        //        adapter.Fill(ds);
        //    }
        //    return ds.Tables[0];
        //}
        private void btnInportPro2Device_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "txt文件|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                var file = File.Open(openFileDialog1.FileName, FileMode.Open);
                List<string> txt = new List<string>();
                using (var stream = new StreamReader(file))
                {
                    while (!stream.EndOfStream)
                    {
                        txt.Add(stream.ReadLine());
                    }
                }
                dgvPro2Device.Rows.Clear();
                dgvPro2Device.Rows.Add(txt.Count);

                string[] IDAry = txt.ToArray();
                for (int i = 0; i < txt.Count; i++)
                {
                    dgvPro2Device.Rows[i].Cells[0].Value = false;
                    dgvPro2Device.Rows[i].Cells[1].Value = IDAry[i];
                }

                for (int i = 0; i < dgvPro2Device.Columns.Count; i++)
                {
                    dgvPro2Device.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvPro2Device.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }



                //filePath = openFileDialog1.FileName;
                //System.Data.DataTable dt = ImportExcel(filePath);
                //dgvPro2Device.DataSource = dt;
                //for (int i = 0; i < dgvPro2Device.Columns.Count; i++)
                //{
                //    dgvPro2Device.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //    dgvPro2Device.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //}
            }
        }

        #endregion

        #region 确认配置
        
        private void btnPro2ProgramCfgCorfirm_Click(object sender, EventArgs e)
        {
            DataInit();
            try
            {
                if(btnPro2ProgramCfgCorfirm.Tag != null)
                {
                    cmbDeviceTypeHub.Enabled = true;
                    cmbProDataTypeHub.Enabled = true;
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    btnPro2ProgramCfgCorfirm.Tag = null;
                    btnPro2ProgramCfgCorfirm.Text = "确认配置";
                    return;
                }

                if (cmbDeviceTypeHub.SelectedIndex > -1)
                {
                    string str = cmbDeviceTypeHub.SelectedItem as string;
                    string[] strAry = str.Split('-');
                    DeviceType = strAry[1];
                    
                }
                else
                {
                    ShowMsgLn("请选择设备类型");
                    return;
                }

                for (int i = 0; i < dgvPro2Device.Rows.Count; i++)
                {
                    if (dgvPro2Device.Rows[i].Cells[0].Value != null && (bool)dgvPro2Device.Rows[i].Cells[0].Value)
                    {

                        string DeviceId = dgvPro2Device.Rows[i].Cells[1].Value as string;
                        if (DeviceId == null)
                        {
                            ShowMsgLn("错误设备号,行号:" + i.ToString() );
                            return;
                        }
                        if (DeviceId.IndexOf(DeviceType) != 0)
                        {
                            ShowMsgLn("错误设备号,行号:" + i.ToString());
                            ShowMsgLn("设备类型错误:" + DeviceType + "  " + DeviceId);
                            return;
                        }
                        g_DevIdList.Add(DeviceId);
                    }
                    
                }
                if (g_xProDataType == ProDataType.OnlyCfg || g_xProDataType == ProDataType.All)
                {
                    if (g_DevIdList.Count <= 0)
                    {
                        ShowMsgLn("无设备号，请重新配置");
                        return;
                    }
                    if (txtHardWare.Enabled)
                    {
                        TextCheckDefaultInput(txtHardWare);
                    }
                    if (txtIpAddr.Enabled)
                    {
                        TextCheckDefaultInput(txtIpAddr);
                    }
                    if (txtIpPort.Enabled)
                    {
                        TextCheckDefaultInput(txtIpPort);
                    }
                }
                if (g_xProDataType == ProDataType.OnlyFile || g_xProDataType == ProDataType.All)
                {
                    if (txtBootFlie.Enabled)
                    {
                        if (!txtBootFlie.Text.EndsWith(".hex"))
                        {
                            setTextError(txtBootFlie, TextErr.eInputErr);
                            return;
                        }
                    }
                    if (txtAppFlie.Enabled)
                    {
                        if (!txtAppFlie.Text.EndsWith(".hex"))
                        {
                            setTextError(txtAppFlie, TextErr.eInputErr);
                            return;
                        }
                    }
                }
                ShowMsgLn("配置成功");
                cmbDeviceTypeHub.Enabled = false;
                cmbProDataTypeHub.Enabled = false;
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;

                btnPro2ProgramCfgCorfirm.Tag = true;
                btnPro2ProgramCfgCorfirm.Text = "重新配置";
                IsCfgDone = true;
            }
            catch(Exception ex)
            {
                ShowMsgLn(ex.Message);
                IsCfgDone = false;
            }
        }
        #endregion

        #region 读取烧录数据


        public List<FirmwareInfomation> GetNewFirmwares(string DeviceId)
        {
            if( g_xProDataType == ProDataType.None)
            {
                ShowMsgLn("配置错误");
                return null;
            }
            List<FirmwareInfomation> firmwares = new List<FirmwareInfomation>();
            //if (g_xProDataType == ProDataType.OnlyFile || g_xProDataType == ProDataType.All)
            //{
            //    FirmwareInfomation xItem;
            //    if (DeviceType != "04")
            //    {
            //        xItem = ReadFile(txtBootFlie.Text);
            //        if (xItem.Error != null)
            //        {
            //            throw new Exception(xItem.Error.Message + "\r\n");
            //        }
            //        firmwares.Add(xItem);
            //    }

            //    xItem = ReadFile(txtAppFlie.Text);
            //    if (xItem.Error != null)
            //    {
            //        throw new Exception(xItem.Error.Message + "\r\n");
            //    }
            //    firmwares.Add(xItem);
            //}

            //if (g_xProDataType == ProDataType.OnlyCfg || g_xProDataType == ProDataType.All)
            {
                //if (DeviceId.IndexOf(DeviceType) != 0)
                //{
                //    ShowMsgLn("设备类型错误:" + DeviceType + "  " + DeviceId);
                //    return null;
                //}

                /* 确保每次烧录完都重新获取传感器数据 */
                FirmwareInfomation customRow = new FirmwareInfomation();
                customRow.BaseAddress = 0x0801fd10;
                customRow.Name = "标识位";
                customRow.Data = new byte[] { 0, 0, 0, 0x10 };
                firmwares.Add(customRow);

                //写入自定义数据
                customRow.BaseAddress = 0x801ffc0;
                customRow.Name = "设备号";
                //DeviceId = "09182011A8D80002"; 
                customRow.Data = Encoding.ASCII.GetBytes(tbDeviceId.Text.Replace(" ", "") + "\0");
                firmwares.Add(customRow);
                if (DeviceType != "04")
                {
                    customRow.BaseAddress = 0x801ff90;
                    customRow.Name = "IP地址";
                    customRow.Data = Encoding.ASCII.GetBytes(txtIpAddr.Text.Replace(" ", "") + "\0");
                    firmwares.Add(customRow);

                    customRow.BaseAddress = 0x801ffb0;
                    customRow.Name = "IP端口";
                    customRow.Data = Encoding.ASCII.GetBytes(txtIpPort.Text.Replace(" ", "") + "\0");
                    firmwares.Add(customRow);
                }
                customRow.BaseAddress = 0x801ff60;
                customRow.Name = "硬件版本";
                customRow.Data = Encoding.ASCII.GetBytes("YC.DZ.P00020.10+"+ "\0");
                firmwares.Add(customRow);
            }
            return firmwares;
        }
        public List<FirmwareInfomation> GetNextDataToWrite()
        {
            //if (g_xProDataType == ProDataType.OnlyFile)
            {
                return GetNewFirmwares("");
            }
            if (g_DevIdList != null && g_DevIdList.Count > 0) {
                string DeviceId = g_DevIdList[0];
                g_DevIdList.RemoveAt(0);
                return GetNewFirmwares(DeviceId);
            }
            throw new Exception("无设备号，请重新配置");
        }

        private FirmwareInfomation ReadFile(string name)
        {
            FirmwareInfomation result = new FirmwareInfomation();
            if (name.EndsWith(".hex"))
            {
                result = FilesOperator.ReadHex(name);
            }
            else
            {
                result.Error = new Exception("读取文件:" + name + " 失败");
            }
            return result;
        }
        private void txtBootFlie_DoubleClick(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "hex文件|*.hex";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtbox.Text = fd.FileName;
            }
        }

        private void txtAppFlie_DoubleClick(object sender, EventArgs e)
        {
            TextBox txtbox = sender as TextBox;
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "hex文件|*.hex";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtbox.Text = fd.FileName;
            }
        }

        #endregion

        private void dgvPro2Device_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }

        #region 调试输出
        public void ShowMsgLn(string text)
        {
            if (MsgBox != null)
            {
                MsgBox.ShowText(text + "\r\n");
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
            TextBox temp = (TextBox)sender;
            if (temp.Tag != null && temp.Text.Trim() == "")
                temp.Text = temp.Tag as string;
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

        #endregion

        private void pro2烧录配置_Load(object sender, EventArgs e)
        {
            TextFirstCfg(txtHardWare);
            TextFirstCfg(txtIpAddr);
            TextFirstCfg(txtIpPort);
            TextFirstCfg(txtBootFlie);
            TextFirstCfg(txtAppFlie);

            cmbProDataTypeHub.SelectedIndex = 0;
            cmbServerIp.SelectedIndex = 2;
        }

        private void pro2烧录配置_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* 清空数据 */
            DataInit();
            /* 仅隐藏，不关闭 */
            Visible = false;
            /* 取消关闭 */
            e.Cancel = true;
        }

        private void cmbServerIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbServerIp.SelectedIndex == 0)
            {
                txtIpAddr.Text = "120.77.213.80";
                txtIpPort.Text = "3099";
            }
            else if (cmbServerIp.SelectedIndex == 1)
            {
                txtIpAddr.Text = "47.106.217.146";
                txtIpPort.Text = "8900";
            }
            else if (cmbServerIp.SelectedIndex == 2)
            {
                txtIpAddr.Text = "device.nongbotech.com";//"47.107.26.146";
                txtIpPort.Text = "9000";
            }
            else
            {

            }
            ShowMsgLn(txtIpAddr.Text );
            ShowMsgLn(txtIpPort.Text);
        }

        private void cmbDeviceTypeHub_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = cmbDeviceTypeHub.SelectedItem as string;
            string [] strAry = str.Split( '-');
            string Hardware = strAry[0];
            string ID = strAry[1];
            ShowMsgLn(Hardware );
            ShowMsgLn(ID );

            txtHardWare.Text = Hardware;
            txtBootFlie.Enabled = !(ID == "04");
            txtIpAddr.Enabled = !(ID == "04");
            txtIpPort.Enabled = !(ID == "04");
            cmbServerIp.Enabled = !(ID == "04");
        }

        private void cmbProDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProDataTypeHub.Text == "全烧录")
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                g_xProDataType = ProDataType.All;
            }
            else if (cmbProDataTypeHub.Text == "只烧录文件")
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = false;
                g_xProDataType = ProDataType.OnlyFile;
            }
            else if (cmbProDataTypeHub.Text == "只烧录配置")
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
                g_xProDataType = ProDataType.OnlyCfg;
            }
            else if (cmbProDataTypeHub.Text == "不烧录")
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                g_xProDataType = ProDataType.None;
            }
        }
        #region 数据右键菜单
        private void prvDelRows(object sender)
        {
            DataGridView dgv = sender as DataGridView;

            List<DataGridViewRow> RemoveRows = new List<DataGridViewRow>();
            foreach (DataGridViewRow xRow in dgv.Rows)
            {
                foreach (DataGridViewCell cell in dgv.SelectedCells)
                {
                    if (xRow.Index == cell.RowIndex)
                    {
                        if (!RemoveRows.Contains(xRow))
                        {
                            RemoveRows.Add(xRow);
                        }
                    }
                }
            }
            foreach (DataGridViewRow xRow in RemoveRows)
            {
                if (dgv.NewRowIndex != xRow.Index)
                {
                    dgv.Rows.Remove(xRow);
                }
                else
                {
                    ShowMsgLn("新行不可删除");
                }
            }
        }
        private void rcmnDgv_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            DataGridView dgv = (DataGridView)this.rcmnDgv.SourceControl;
            switch (e.ClickedItem.Text)
            {
                case "删除选中行":
                    prvDelRows(dgv);
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}
