namespace DeviceTest
{
    partial class formExpandHub
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
            this.btnExPortStart = new System.Windows.Forms.Button();
            this.lbExPort5 = new System.Windows.Forms.Label();
            this.lbExPort4 = new System.Windows.Forms.Label();
            this.lbExPort3 = new System.Windows.Forms.Label();
            this.lbExPort2 = new System.Windows.Forms.Label();
            this.lbExPort1 = new System.Windows.Forms.Label();
            this.tmSerialCheck = new System.Windows.Forms.Timer(this.components);
            this.Serial = new System.IO.Ports.SerialPort(this.components);
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.cmbTestMode = new System.Windows.Forms.ComboBox();
            this.btnPowerOn5 = new System.Windows.Forms.Button();
            this.btnPowerOn1 = new System.Windows.Forms.Button();
            this.btnPowerOn4 = new System.Windows.Forms.Button();
            this.btnPowerOn2 = new System.Windows.Forms.Button();
            this.btnPowerOn3 = new System.Windows.Forms.Button();
            this.btnGetAddr = new System.Windows.Forms.Button();
            this.rtbexMsgBox = new DeviceTest.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // btnExPortStart
            // 
            this.btnExPortStart.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExPortStart.Location = new System.Drawing.Point(138, 216);
            this.btnExPortStart.Name = "btnExPortStart";
            this.btnExPortStart.Size = new System.Drawing.Size(245, 45);
            this.btnExPortStart.TabIndex = 100;
            this.btnExPortStart.Text = "启动";
            this.btnExPortStart.UseVisualStyleBackColor = true;
            this.btnExPortStart.Click += new System.EventHandler(this.btnExPortStart_Click);
            // 
            // lbExPort5
            // 
            this.lbExPort5.BackColor = System.Drawing.Color.DarkOrange;
            this.lbExPort5.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExPort5.ForeColor = System.Drawing.Color.White;
            this.lbExPort5.Location = new System.Drawing.Point(7, 174);
            this.lbExPort5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExPort5.Name = "lbExPort5";
            this.lbExPort5.Size = new System.Drawing.Size(376, 35);
            this.lbExPort5.TabIndex = 99;
            this.lbExPort5.Text = "拓展口5";
            this.lbExPort5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbExPort4
            // 
            this.lbExPort4.BackColor = System.Drawing.Color.DarkOrange;
            this.lbExPort4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExPort4.ForeColor = System.Drawing.Color.White;
            this.lbExPort4.Location = new System.Drawing.Point(7, 133);
            this.lbExPort4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExPort4.Name = "lbExPort4";
            this.lbExPort4.Size = new System.Drawing.Size(376, 35);
            this.lbExPort4.TabIndex = 98;
            this.lbExPort4.Text = "拓展口4";
            this.lbExPort4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbExPort3
            // 
            this.lbExPort3.BackColor = System.Drawing.Color.DarkOrange;
            this.lbExPort3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExPort3.ForeColor = System.Drawing.Color.White;
            this.lbExPort3.Location = new System.Drawing.Point(7, 92);
            this.lbExPort3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExPort3.Name = "lbExPort3";
            this.lbExPort3.Size = new System.Drawing.Size(376, 35);
            this.lbExPort3.TabIndex = 97;
            this.lbExPort3.Text = "拓展口3";
            this.lbExPort3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbExPort2
            // 
            this.lbExPort2.BackColor = System.Drawing.Color.DarkOrange;
            this.lbExPort2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExPort2.ForeColor = System.Drawing.Color.White;
            this.lbExPort2.Location = new System.Drawing.Point(7, 51);
            this.lbExPort2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExPort2.Name = "lbExPort2";
            this.lbExPort2.Size = new System.Drawing.Size(376, 35);
            this.lbExPort2.TabIndex = 96;
            this.lbExPort2.Text = "拓展口2";
            this.lbExPort2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbExPort1
            // 
            this.lbExPort1.BackColor = System.Drawing.Color.DarkOrange;
            this.lbExPort1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbExPort1.ForeColor = System.Drawing.Color.White;
            this.lbExPort1.Location = new System.Drawing.Point(7, 9);
            this.lbExPort1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExPort1.Name = "lbExPort1";
            this.lbExPort1.Size = new System.Drawing.Size(376, 35);
            this.lbExPort1.TabIndex = 95;
            this.lbExPort1.Text = "拓展口1";
            this.lbExPort1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmSerialCheck
            // 
            this.tmSerialCheck.Interval = 1000;
            this.tmSerialCheck.Tick += new System.EventHandler(this.tmSerialCheck_Tick);
            // 
            // Serial
            // 
            this.Serial.BaudRate = 115200;
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(8, 216);
            this.cmbSerialPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(120, 20);
            this.cmbSerialPort.TabIndex = 101;
            this.cmbSerialPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbSerialPort_MouseClick);
            // 
            // cmbTestMode
            // 
            this.cmbTestMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTestMode.FormattingEnabled = true;
            this.cmbTestMode.Items.AddRange(new object[] {
            "接触检测",
            "识别地址",
            "内部使用"});
            this.cmbTestMode.Location = new System.Drawing.Point(8, 241);
            this.cmbTestMode.Name = "cmbTestMode";
            this.cmbTestMode.Size = new System.Drawing.Size(120, 20);
            this.cmbTestMode.TabIndex = 102;
            this.cmbTestMode.Text = "选择测试模式";
            this.cmbTestMode.SelectedIndexChanged += new System.EventHandler(this.cmbTestMode_SelectedIndexChanged);
            // 
            // btnPowerOn5
            // 
            this.btnPowerOn5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPowerOn5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPowerOn5.Location = new System.Drawing.Point(283, 174);
            this.btnPowerOn5.Name = "btnPowerOn5";
            this.btnPowerOn5.Size = new System.Drawing.Size(100, 35);
            this.btnPowerOn5.TabIndex = 104;
            this.btnPowerOn5.Text = "电源";
            this.btnPowerOn5.UseVisualStyleBackColor = false;
            this.btnPowerOn5.Visible = false;
            this.btnPowerOn5.Click += new System.EventHandler(this.btnPowerOnClick);
            // 
            // btnPowerOn1
            // 
            this.btnPowerOn1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPowerOn1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPowerOn1.Location = new System.Drawing.Point(283, 9);
            this.btnPowerOn1.Name = "btnPowerOn1";
            this.btnPowerOn1.Size = new System.Drawing.Size(100, 35);
            this.btnPowerOn1.TabIndex = 105;
            this.btnPowerOn1.Text = "电源";
            this.btnPowerOn1.UseVisualStyleBackColor = false;
            this.btnPowerOn1.Visible = false;
            this.btnPowerOn1.Click += new System.EventHandler(this.btnPowerOnClick);
            // 
            // btnPowerOn4
            // 
            this.btnPowerOn4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPowerOn4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPowerOn4.Location = new System.Drawing.Point(283, 133);
            this.btnPowerOn4.Name = "btnPowerOn4";
            this.btnPowerOn4.Size = new System.Drawing.Size(100, 35);
            this.btnPowerOn4.TabIndex = 106;
            this.btnPowerOn4.Text = "电源";
            this.btnPowerOn4.UseVisualStyleBackColor = false;
            this.btnPowerOn4.Visible = false;
            this.btnPowerOn4.Click += new System.EventHandler(this.btnPowerOnClick);
            // 
            // btnPowerOn2
            // 
            this.btnPowerOn2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPowerOn2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPowerOn2.Location = new System.Drawing.Point(283, 51);
            this.btnPowerOn2.Name = "btnPowerOn2";
            this.btnPowerOn2.Size = new System.Drawing.Size(100, 35);
            this.btnPowerOn2.TabIndex = 106;
            this.btnPowerOn2.Text = "电源";
            this.btnPowerOn2.UseVisualStyleBackColor = false;
            this.btnPowerOn2.Visible = false;
            this.btnPowerOn2.Click += new System.EventHandler(this.btnPowerOnClick);
            // 
            // btnPowerOn3
            // 
            this.btnPowerOn3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPowerOn3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPowerOn3.Location = new System.Drawing.Point(283, 92);
            this.btnPowerOn3.Name = "btnPowerOn3";
            this.btnPowerOn3.Size = new System.Drawing.Size(100, 35);
            this.btnPowerOn3.TabIndex = 107;
            this.btnPowerOn3.Text = "电源";
            this.btnPowerOn3.UseVisualStyleBackColor = false;
            this.btnPowerOn3.Visible = false;
            this.btnPowerOn3.Click += new System.EventHandler(this.btnPowerOnClick);
            // 
            // btnGetAddr
            // 
            this.btnGetAddr.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetAddr.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnGetAddr.Location = new System.Drawing.Point(283, 216);
            this.btnGetAddr.Name = "btnGetAddr";
            this.btnGetAddr.Size = new System.Drawing.Size(100, 45);
            this.btnGetAddr.TabIndex = 108;
            this.btnGetAddr.Text = "获取地址";
            this.btnGetAddr.UseVisualStyleBackColor = false;
            this.btnGetAddr.Visible = false;
            this.btnGetAddr.Click += new System.EventHandler(this.btnGetAddr_Click);
            // 
            // rtbexMsgBox
            // 
            this.rtbexMsgBox.BackColor = System.Drawing.Color.White;
            this.rtbexMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbexMsgBox.Location = new System.Drawing.Point(8, 267);
            this.rtbexMsgBox.Name = "rtbexMsgBox";
            this.rtbexMsgBox.ReadOnly = true;
            this.rtbexMsgBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbexMsgBox.Size = new System.Drawing.Size(375, 198);
            this.rtbexMsgBox.TabIndex = 109;
            this.rtbexMsgBox.Text = "仅支持直接将拓展盒接到串口转485工具上\n\n支持接入485设备类型：\n土壤温湿度、蘑菇、土壤EC、土壤PH、土壤温度、叶面温度\n\n\n\n";
            this.rtbexMsgBox.WordWrap = false;
            // 
            // 拓展盒
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 477);
            this.Controls.Add(this.btnGetAddr);
            this.Controls.Add(this.btnPowerOn3);
            this.Controls.Add(this.btnPowerOn2);
            this.Controls.Add(this.btnPowerOn4);
            this.Controls.Add(this.btnPowerOn1);
            this.Controls.Add(this.btnPowerOn5);
            this.Controls.Add(this.rtbexMsgBox);
            this.Controls.Add(this.cmbTestMode);
            this.Controls.Add(this.cmbSerialPort);
            this.Controls.Add(this.btnExPortStart);
            this.Controls.Add(this.lbExPort5);
            this.Controls.Add(this.lbExPort4);
            this.Controls.Add(this.lbExPort3);
            this.Controls.Add(this.lbExPort2);
            this.Controls.Add(this.lbExPort1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "拓展盒";
            this.Text = "拓展盒";
            this.Load += new System.EventHandler(this.formExpandHub_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExPortStart;
        private System.Windows.Forms.Label lbExPort5;
        private System.Windows.Forms.Label lbExPort4;
        private System.Windows.Forms.Label lbExPort3;
        private System.Windows.Forms.Label lbExPort2;
        private System.Windows.Forms.Label lbExPort1;
        private System.Windows.Forms.Timer tmSerialCheck;
        private System.IO.Ports.SerialPort Serial;
        private System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.ComboBox cmbTestMode;
        private System.Windows.Forms.Button btnPowerOn5;
        private System.Windows.Forms.Button btnPowerOn1;
        private System.Windows.Forms.Button btnPowerOn4;
        private System.Windows.Forms.Button btnPowerOn2;
        private System.Windows.Forms.Button btnPowerOn3;
        private System.Windows.Forms.Button btnGetAddr;
        private RichTextBoxEx rtbexMsgBox;
    }
}