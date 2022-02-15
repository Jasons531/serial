namespace DeviceTest
{
    partial class formPgmCfg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.rcmnDgv = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DelRows = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnInportPro2Device = new System.Windows.Forms.Button();
            this.DevId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPro = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvPro2Device = new System.Windows.Forms.DataGridView();
            this.txtIpPort = new System.Windows.Forms.TextBox();
            this.txtIpAddr = new System.Windows.Forms.TextBox();
            this.cmbServerIp = new System.Windows.Forms.ComboBox();
            this.txtHardWare = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAppFlie = new System.Windows.Forms.TextBox();
            this.txtBootFlie = new System.Windows.Forms.TextBox();
            this.cmbProDataTypeHub = new System.Windows.Forms.ComboBox();
            this.cmbDeviceTypeHub = new System.Windows.Forms.ComboBox();
            this.btnPro2ProgramCfgCorfirm = new System.Windows.Forms.Button();
            this.rcmnDgv.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPro2Device)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rcmnDgv
            // 
            this.rcmnDgv.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.rcmnDgv.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DelRows});
            this.rcmnDgv.Name = "rcmnDgv";
            this.rcmnDgv.Size = new System.Drawing.Size(154, 28);
            this.rcmnDgv.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.rcmnDgv_ItemClicked);
            // 
            // DelRows
            // 
            this.DelRows.Name = "DelRows";
            this.DelRows.Size = new System.Drawing.Size(153, 24);
            this.DelRows.Text = "删除选中行";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPro2Device);
            this.groupBox2.Controls.Add(this.txtHardWare);
            this.groupBox2.Controls.Add(this.txtIpAddr);
            this.groupBox2.Controls.Add(this.btnInportPro2Device);
            this.groupBox2.Controls.Add(this.txtIpPort);
            this.groupBox2.Controls.Add(this.cmbServerIp);
            this.groupBox2.Location = new System.Drawing.Point(7, 200);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(513, 391);
            this.groupBox2.TabIndex = 119;
            this.groupBox2.TabStop = false;
            // 
            // btnInportPro2Device
            // 
            this.btnInportPro2Device.Location = new System.Drawing.Point(7, 355);
            this.btnInportPro2Device.Margin = new System.Windows.Forms.Padding(4);
            this.btnInportPro2Device.Name = "btnInportPro2Device";
            this.btnInportPro2Device.Size = new System.Drawing.Size(497, 29);
            this.btnInportPro2Device.TabIndex = 4;
            this.btnInportPro2Device.Text = "导入设备号";
            this.btnInportPro2Device.UseVisualStyleBackColor = true;
            this.btnInportPro2Device.Click += new System.EventHandler(this.btnInportPro2Device_Click);
            // 
            // DevId
            // 
            this.DevId.HeaderText = "设备号";
            this.DevId.MinimumWidth = 6;
            this.DevId.Name = "DevId";
            this.DevId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DevId.Width = 330;
            // 
            // IsPro
            // 
            this.IsPro.HeaderText = "";
            this.IsPro.MinimumWidth = 6;
            this.IsPro.Name = "IsPro";
            this.IsPro.Width = 21;
            // 
            // dgvPro2Device
            // 
            this.dgvPro2Device.AllowUserToResizeColumns = false;
            this.dgvPro2Device.AllowUserToResizeRows = false;
            this.dgvPro2Device.BackgroundColor = System.Drawing.Color.White;
            this.dgvPro2Device.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPro2Device.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvPro2Device.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPro2Device.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsPro,
            this.DevId});
            this.dgvPro2Device.ContextMenuStrip = this.rcmnDgv;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPro2Device.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPro2Device.Location = new System.Drawing.Point(8, 151);
            this.dgvPro2Device.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPro2Device.Name = "dgvPro2Device";
            this.dgvPro2Device.RowHeadersVisible = false;
            this.dgvPro2Device.RowHeadersWidth = 51;
            this.dgvPro2Device.RowTemplate.Height = 23;
            this.dgvPro2Device.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPro2Device.Size = new System.Drawing.Size(496, 196);
            this.dgvPro2Device.TabIndex = 3;
            this.dgvPro2Device.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvPro2Device_RowStateChanged);
            // 
            // txtIpPort
            // 
            this.txtIpPort.Font = new System.Drawing.Font("宋体", 9F);
            this.txtIpPort.Location = new System.Drawing.Point(7, 118);
            this.txtIpPort.Margin = new System.Windows.Forms.Padding(4);
            this.txtIpPort.Name = "txtIpPort";
            this.txtIpPort.Size = new System.Drawing.Size(496, 25);
            this.txtIpPort.TabIndex = 2;
            this.txtIpPort.Text = "IP端口";
            this.txtIpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIpPort.Enter += new System.EventHandler(this.txtEnter);
            this.txtIpPort.Leave += new System.EventHandler(this.txtLeave);
            // 
            // txtIpAddr
            // 
            this.txtIpAddr.Font = new System.Drawing.Font("宋体", 9F);
            this.txtIpAddr.Location = new System.Drawing.Point(7, 84);
            this.txtIpAddr.Margin = new System.Windows.Forms.Padding(4);
            this.txtIpAddr.Name = "txtIpAddr";
            this.txtIpAddr.Size = new System.Drawing.Size(496, 25);
            this.txtIpAddr.TabIndex = 1;
            this.txtIpAddr.Text = "IP地址";
            this.txtIpAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIpAddr.Enter += new System.EventHandler(this.txtEnter);
            this.txtIpAddr.Leave += new System.EventHandler(this.txtLeave);
            // 
            // cmbServerIp
            // 
            this.cmbServerIp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerIp.Font = new System.Drawing.Font("宋体", 9F);
            this.cmbServerIp.Items.AddRange(new object[] {
            "旧服务器",
            "新服务器(测试)",
            "新服务器(正式)"});
            this.cmbServerIp.Location = new System.Drawing.Point(7, 51);
            this.cmbServerIp.Margin = new System.Windows.Forms.Padding(4);
            this.cmbServerIp.Name = "cmbServerIp";
            this.cmbServerIp.Size = new System.Drawing.Size(496, 23);
            this.cmbServerIp.TabIndex = 112;
            this.cmbServerIp.SelectedIndexChanged += new System.EventHandler(this.cmbServerIp_SelectedIndexChanged);
            // 
            // txtHardWare
            // 
            this.txtHardWare.Font = new System.Drawing.Font("宋体", 9F);
            this.txtHardWare.Location = new System.Drawing.Point(7, 18);
            this.txtHardWare.Margin = new System.Windows.Forms.Padding(4);
            this.txtHardWare.Name = "txtHardWare";
            this.txtHardWare.Size = new System.Drawing.Size(496, 25);
            this.txtHardWare.TabIndex = 0;
            this.txtHardWare.Text = "硬件版本(仅供参考,具体请核对丝印)";
            this.txtHardWare.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHardWare.Enter += new System.EventHandler(this.txtEnter);
            this.txtHardWare.Leave += new System.EventHandler(this.txtLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAppFlie);
            this.groupBox1.Controls.Add(this.txtBootFlie);
            this.groupBox1.Location = new System.Drawing.Point(7, 119);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(513, 80);
            this.groupBox1.TabIndex = 113;
            this.groupBox1.TabStop = false;
            // 
            // txtAppFlie
            // 
            this.txtAppFlie.BackColor = System.Drawing.SystemColors.Window;
            this.txtAppFlie.Font = new System.Drawing.Font("宋体", 9F);
            this.txtAppFlie.Location = new System.Drawing.Point(8, 46);
            this.txtAppFlie.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppFlie.Name = "txtAppFlie";
            this.txtAppFlie.ReadOnly = true;
            this.txtAppFlie.Size = new System.Drawing.Size(496, 25);
            this.txtAppFlie.TabIndex = 114;
            this.txtAppFlie.Text = "双击可选择APP文件...";
            this.txtAppFlie.DoubleClick += new System.EventHandler(this.txtAppFlie_DoubleClick);
            // 
            // txtBootFlie
            // 
            this.txtBootFlie.BackColor = System.Drawing.SystemColors.Window;
            this.txtBootFlie.Font = new System.Drawing.Font("宋体", 9F);
            this.txtBootFlie.Location = new System.Drawing.Point(8, 15);
            this.txtBootFlie.Margin = new System.Windows.Forms.Padding(4);
            this.txtBootFlie.Name = "txtBootFlie";
            this.txtBootFlie.ReadOnly = true;
            this.txtBootFlie.Size = new System.Drawing.Size(496, 25);
            this.txtBootFlie.TabIndex = 113;
            this.txtBootFlie.Text = "双击可选择BOOT文件...";
            this.txtBootFlie.DoubleClick += new System.EventHandler(this.txtBootFlie_DoubleClick);
            // 
            // cmbProDataTypeHub
            // 
            this.cmbProDataTypeHub.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProDataTypeHub.Font = new System.Drawing.Font("宋体", 9F);
            this.cmbProDataTypeHub.Items.AddRange(new object[] {
            "全烧录",
            "只烧录文件",
            "只烧录配置",
            "不烧录"});
            this.cmbProDataTypeHub.Location = new System.Drawing.Point(7, 96);
            this.cmbProDataTypeHub.Margin = new System.Windows.Forms.Padding(4);
            this.cmbProDataTypeHub.Name = "cmbProDataTypeHub";
            this.cmbProDataTypeHub.Size = new System.Drawing.Size(512, 23);
            this.cmbProDataTypeHub.TabIndex = 118;
            this.cmbProDataTypeHub.SelectedIndexChanged += new System.EventHandler(this.cmbProDataType_SelectedIndexChanged);
            // 
            // cmbDeviceTypeHub
            // 
            this.cmbDeviceTypeHub.Font = new System.Drawing.Font("宋体", 9F);
            this.cmbDeviceTypeHub.Items.AddRange(new object[] {
            "YC.DZ.P00014.S.42-01-(pro1农场)",
            "YC.DZ.P00014.S.42(E1)-07-(pro1气象站)",
            "YC.DZ.P00020.e2-09-(pro2旧-18年贴板)",
            "YC.DZ.P00020.10-09-(pro2新-19年贴板)",
            "YC.DZ.P00020.10(E1)-11-(pro2水产)",
            "YC.DZ.P00001.30-00-(mini1)",
            "YC.DZ.P01901.10-10-(mini2)",
            "YC.DZ.P00201.e3-02-(plus农场)",
            "YC.DZ.P00410.e2-04-(lora水阀)"});
            this.cmbDeviceTypeHub.Location = new System.Drawing.Point(7, 66);
            this.cmbDeviceTypeHub.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDeviceTypeHub.Name = "cmbDeviceTypeHub";
            this.cmbDeviceTypeHub.Size = new System.Drawing.Size(512, 23);
            this.cmbDeviceTypeHub.TabIndex = 115;
            this.cmbDeviceTypeHub.Text = "选择设备类型";
            this.cmbDeviceTypeHub.SelectedIndexChanged += new System.EventHandler(this.cmbDeviceTypeHub_SelectedIndexChanged);
            // 
            // btnPro2ProgramCfgCorfirm
            // 
            this.btnPro2ProgramCfgCorfirm.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPro2ProgramCfgCorfirm.Location = new System.Drawing.Point(7, 4);
            this.btnPro2ProgramCfgCorfirm.Margin = new System.Windows.Forms.Padding(4);
            this.btnPro2ProgramCfgCorfirm.Name = "btnPro2ProgramCfgCorfirm";
            this.btnPro2ProgramCfgCorfirm.Size = new System.Drawing.Size(513, 56);
            this.btnPro2ProgramCfgCorfirm.TabIndex = 78;
            this.btnPro2ProgramCfgCorfirm.Text = "确认配置";
            this.btnPro2ProgramCfgCorfirm.UseVisualStyleBackColor = true;
            this.btnPro2ProgramCfgCorfirm.Click += new System.EventHandler(this.btnPro2ProgramCfgCorfirm_Click);
            // 
            // formPgmCfg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 596);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmbProDataTypeHub);
            this.Controls.Add(this.btnPro2ProgramCfgCorfirm);
            this.Controls.Add(this.cmbDeviceTypeHub);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formPgmCfg";
            this.Text = "烧录配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.pro2烧录配置_FormClosing);
            this.Load += new System.EventHandler(this.pro2烧录配置_Load);
            this.rcmnDgv.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPro2Device)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip rcmnDgv;
        private System.Windows.Forms.ToolStripMenuItem DelRows;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvPro2Device;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsPro;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevId;
        private System.Windows.Forms.TextBox txtHardWare;
        private System.Windows.Forms.TextBox txtIpAddr;
        private System.Windows.Forms.Button btnInportPro2Device;
        private System.Windows.Forms.TextBox txtIpPort;
        private System.Windows.Forms.TextBox tbDeviceId;
        private System.Windows.Forms.ComboBox cmbServerIp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAppFlie;
        private System.Windows.Forms.TextBox txtBootFlie;
        private System.Windows.Forms.ComboBox cmbProDataTypeHub;
        private System.Windows.Forms.ComboBox cmbDeviceTypeHub;
        private System.Windows.Forms.Button btnPro2ProgramCfgCorfirm;
    }
}