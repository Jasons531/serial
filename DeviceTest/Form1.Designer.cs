namespace DeviceTest
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Serial = new System.IO.Ports.SerialPort(this.components);
            this.tmSerialCheck = new System.Windows.Forms.Timer(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btConnect = new System.Windows.Forms.Button();
            this.tbIPPort = new System.Windows.Forms.TextBox();
            this.tbIPAddr = new System.Windows.Forms.TextBox();
            this.lbIPort = new System.Windows.Forms.Label();
            this.lbIPAd = new System.Windows.Forms.Label();
            this.lbSend = new System.Windows.Forms.Label();
            this.lbQiact = new System.Windows.Forms.Label();
            this.lbcreg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCsq = new System.Windows.Forms.Label();
            this.lbDeviceId = new System.Windows.Forms.Label();
            this.lbSvCnt = new System.Windows.Forms.Label();
            this.lbSoftWare = new System.Windows.Forms.Label();
            this.lbcpin = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lbCcid = new System.Windows.Forms.Label();
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbUpdate = new System.Windows.Forms.TabPage();
            this.btUpdateStatus = new System.Windows.Forms.Button();
            this.cbSerialId = new System.Windows.Forms.ComboBox();
            this.txIpPort = new System.Windows.Forms.TextBox();
            this.txIpAddr = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btUpdateIP = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDeviceId = new System.Windows.Forms.TextBox();
            this.richTextBoxEx1 = new DeviceTest.RichTextBoxEx();
            this.rtbexMsgBox = new DeviceTest.RichTextBoxEx();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tbUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // Serial
            // 
            this.Serial.BaudRate = 115200;
            this.Serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.Serial_DataReceived);
            // 
            // tmSerialCheck
            // 
            this.tmSerialCheck.Interval = 1000;
            this.tmSerialCheck.Tick += new System.EventHandler(this.tmSerialCheck_Tick);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.btConnect);
            this.tabPage1.Controls.Add(this.tbIPPort);
            this.tabPage1.Controls.Add(this.tbIPAddr);
            this.tabPage1.Controls.Add(this.lbIPort);
            this.tabPage1.Controls.Add(this.lbIPAd);
            this.tabPage1.Controls.Add(this.lbSend);
            this.tabPage1.Controls.Add(this.lbQiact);
            this.tabPage1.Controls.Add(this.lbcreg);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lbCsq);
            this.tabPage1.Controls.Add(this.lbDeviceId);
            this.tabPage1.Controls.Add(this.lbSvCnt);
            this.tabPage1.Controls.Add(this.lbSoftWare);
            this.tabPage1.Controls.Add(this.lbcpin);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Controls.Add(this.lbCcid);
            this.tabPage1.Controls.Add(this.cmbSerialPort);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(525, 668);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "BG96 Test";
            // 
            // btConnect
            // 
            this.btConnect.Location = new System.Drawing.Point(352, 31);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(162, 61);
            this.btConnect.TabIndex = 100;
            this.btConnect.Text = "CheckConnect";
            this.btConnect.UseVisualStyleBackColor = true;
            this.btConnect.Click += new System.EventHandler(this.Button1_Click);
            // 
            // tbIPPort
            // 
            this.tbIPPort.Location = new System.Drawing.Point(108, 67);
            this.tbIPPort.Name = "tbIPPort";
            this.tbIPPort.Size = new System.Drawing.Size(209, 25);
            this.tbIPPort.TabIndex = 99;
            this.tbIPPort.Text = "9000";
            // 
            // tbIPAddr
            // 
            this.tbIPAddr.Location = new System.Drawing.Point(108, 31);
            this.tbIPAddr.Name = "tbIPAddr";
            this.tbIPAddr.Size = new System.Drawing.Size(209, 25);
            this.tbIPAddr.TabIndex = 98;
            this.tbIPAddr.Text = "device.nongbotech.com";
            // 
            // lbIPort
            // 
            this.lbIPort.AutoSize = true;
            this.lbIPort.Location = new System.Drawing.Point(17, 70);
            this.lbIPort.Name = "lbIPort";
            this.lbIPort.Size = new System.Drawing.Size(71, 15);
            this.lbIPort.TabIndex = 97;
            this.lbIPort.Text = "IP Port:";
            // 
            // lbIPAd
            // 
            this.lbIPAd.AutoSize = true;
            this.lbIPAd.Location = new System.Drawing.Point(15, 34);
            this.lbIPAd.Name = "lbIPAd";
            this.lbIPAd.Size = new System.Drawing.Size(95, 15);
            this.lbIPAd.TabIndex = 3;
            this.lbIPAd.Text = "IP Address:";
            // 
            // lbSend
            // 
            this.lbSend.BackColor = System.Drawing.Color.DarkOrange;
            this.lbSend.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSend.ForeColor = System.Drawing.Color.White;
            this.lbSend.Location = new System.Drawing.Point(15, 613);
            this.lbSend.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbSend.Name = "lbSend";
            this.lbSend.Size = new System.Drawing.Size(499, 44);
            this.lbSend.TabIndex = 95;
            this.lbSend.Text = "Send Data";
            this.lbSend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbQiact
            // 
            this.lbQiact.BackColor = System.Drawing.Color.DarkOrange;
            this.lbQiact.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbQiact.ForeColor = System.Drawing.Color.White;
            this.lbQiact.Location = new System.Drawing.Point(15, 506);
            this.lbQiact.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbQiact.Name = "lbQiact";
            this.lbQiact.Size = new System.Drawing.Size(499, 44);
            this.lbQiact.TabIndex = 94;
            this.lbQiact.Text = "PDP Context";
            this.lbQiact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbcreg
            // 
            this.lbcreg.BackColor = System.Drawing.Color.DarkOrange;
            this.lbcreg.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcreg.ForeColor = System.Drawing.Color.White;
            this.lbcreg.Location = new System.Drawing.Point(15, 450);
            this.lbcreg.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcreg.Name = "lbcreg";
            this.lbcreg.Size = new System.Drawing.Size(499, 44);
            this.lbcreg.TabIndex = 93;
            this.lbcreg.Text = "CREG";
            this.lbcreg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 92;
            this.label1.Text = "PortId: ";
            // 
            // lbCsq
            // 
            this.lbCsq.BackColor = System.Drawing.Color.DarkOrange;
            this.lbCsq.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCsq.ForeColor = System.Drawing.Color.White;
            this.lbCsq.Location = new System.Drawing.Point(15, 395);
            this.lbCsq.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbCsq.Name = "lbCsq";
            this.lbCsq.Size = new System.Drawing.Size(499, 44);
            this.lbCsq.TabIndex = 91;
            this.lbCsq.Text = "CSQ";
            this.lbCsq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCsq.Click += new System.EventHandler(this.lbCsq_Click);
            this.lbCsq.DoubleClick += new System.EventHandler(this.lbDoubleClick);
            // 
            // lbDeviceId
            // 
            this.lbDeviceId.BackColor = System.Drawing.Color.DarkOrange;
            this.lbDeviceId.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbDeviceId.ForeColor = System.Drawing.Color.White;
            this.lbDeviceId.Location = new System.Drawing.Point(15, 173);
            this.lbDeviceId.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbDeviceId.Name = "lbDeviceId";
            this.lbDeviceId.Size = new System.Drawing.Size(499, 44);
            this.lbDeviceId.TabIndex = 65;
            this.lbDeviceId.Text = "Device ID";
            this.lbDeviceId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbDeviceId.DoubleClick += new System.EventHandler(this.lbDoubleClick);
            // 
            // lbSvCnt
            // 
            this.lbSvCnt.BackColor = System.Drawing.Color.DarkOrange;
            this.lbSvCnt.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSvCnt.ForeColor = System.Drawing.Color.White;
            this.lbSvCnt.Location = new System.Drawing.Point(15, 560);
            this.lbSvCnt.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbSvCnt.Name = "lbSvCnt";
            this.lbSvCnt.Size = new System.Drawing.Size(499, 44);
            this.lbSvCnt.TabIndex = 90;
            this.lbSvCnt.Text = "Connect Server";
            this.lbSvCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSvCnt.DoubleClick += new System.EventHandler(this.lbDoubleClick);
            // 
            // lbSoftWare
            // 
            this.lbSoftWare.BackColor = System.Drawing.Color.DarkOrange;
            this.lbSoftWare.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSoftWare.ForeColor = System.Drawing.Color.White;
            this.lbSoftWare.Location = new System.Drawing.Point(15, 232);
            this.lbSoftWare.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbSoftWare.Name = "lbSoftWare";
            this.lbSoftWare.Size = new System.Drawing.Size(499, 44);
            this.lbSoftWare.TabIndex = 69;
            this.lbSoftWare.Text = "Software version";
            this.lbSoftWare.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSoftWare.DoubleClick += new System.EventHandler(this.lbDoubleClick);
            // 
            // lbcpin
            // 
            this.lbcpin.BackColor = System.Drawing.Color.DarkOrange;
            this.lbcpin.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbcpin.ForeColor = System.Drawing.Color.White;
            this.lbcpin.Location = new System.Drawing.Point(15, 287);
            this.lbcpin.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbcpin.Name = "lbcpin";
            this.lbcpin.Size = new System.Drawing.Size(499, 44);
            this.lbcpin.TabIndex = 67;
            this.lbcpin.Text = "CPIN";
            this.lbcpin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbcpin.DoubleClick += new System.EventHandler(this.lbDoubleClick);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(269, 113);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(245, 56);
            this.btnStart.TabIndex = 64;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lbCcid
            // 
            this.lbCcid.BackColor = System.Drawing.Color.DarkOrange;
            this.lbCcid.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCcid.ForeColor = System.Drawing.Color.White;
            this.lbCcid.Location = new System.Drawing.Point(15, 341);
            this.lbCcid.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lbCcid.Name = "lbCcid";
            this.lbCcid.Size = new System.Drawing.Size(499, 44);
            this.lbCcid.TabIndex = 66;
            this.lbCcid.Text = "QCCID";
            this.lbCcid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCcid.DoubleClick += new System.EventHandler(this.lbDoubleClick);
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(101, 128);
            this.cmbSerialPort.Margin = new System.Windows.Forms.Padding(5);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(159, 23);
            this.cmbSerialPort.TabIndex = 62;
            this.cmbSerialPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbSerialPort_MouseClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tbUpdate);
            this.tabControl1.Location = new System.Drawing.Point(7, 5);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(533, 697);
            this.tabControl1.TabIndex = 0;
            // 
            // tbUpdate
            // 
            this.tbUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.tbUpdate.Controls.Add(this.tbDeviceId);
            this.tbUpdate.Controls.Add(this.label5);
            this.tbUpdate.Controls.Add(this.btUpdateStatus);
            this.tbUpdate.Controls.Add(this.cbSerialId);
            this.tbUpdate.Controls.Add(this.txIpPort);
            this.tbUpdate.Controls.Add(this.txIpAddr);
            this.tbUpdate.Controls.Add(this.label2);
            this.tbUpdate.Controls.Add(this.label3);
            this.tbUpdate.Controls.Add(this.label4);
            this.tbUpdate.Controls.Add(this.btUpdateIP);
            this.tbUpdate.Location = new System.Drawing.Point(4, 25);
            this.tbUpdate.Name = "tbUpdate";
            this.tbUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tbUpdate.Size = new System.Drawing.Size(525, 668);
            this.tbUpdate.TabIndex = 1;
            this.tbUpdate.Text = "UpdateIP";
            // 
            // btUpdateStatus
            // 
            this.btUpdateStatus.Location = new System.Drawing.Point(31, 188);
            this.btUpdateStatus.Name = "btUpdateStatus";
            this.btUpdateStatus.Size = new System.Drawing.Size(487, 38);
            this.btUpdateStatus.TabIndex = 109;
            this.btUpdateStatus.Text = "Update the status";
            this.btUpdateStatus.UseVisualStyleBackColor = true;
            // 
            // cbSerialId
            // 
            this.cbSerialId.FormattingEnabled = true;
            this.cbSerialId.Location = new System.Drawing.Point(121, 36);
            this.cbSerialId.Name = "cbSerialId";
            this.cbSerialId.Size = new System.Drawing.Size(186, 23);
            this.cbSerialId.TabIndex = 108;
            this.cbSerialId.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CbSerialId_MouseClick);
            // 
            // txIpPort
            // 
            this.txIpPort.Location = new System.Drawing.Point(121, 119);
            this.txIpPort.Name = "txIpPort";
            this.txIpPort.Size = new System.Drawing.Size(186, 25);
            this.txIpPort.TabIndex = 107;
            this.txIpPort.Text = "19000";
            // 
            // txIpAddr
            // 
            this.txIpAddr.Location = new System.Drawing.Point(121, 81);
            this.txIpAddr.Name = "txIpAddr";
            this.txIpAddr.Size = new System.Drawing.Size(186, 25);
            this.txIpAddr.TabIndex = 106;
            this.txIpAddr.Text = "3.112.30.114";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 105;
            this.label2.Text = "IP Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 15);
            this.label3.TabIndex = 101;
            this.label3.Text = "IP Address:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 104;
            this.label4.Text = "PortId: ";
            // 
            // btUpdateIP
            // 
            this.btUpdateIP.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUpdateIP.Location = new System.Drawing.Point(326, 36);
            this.btUpdateIP.Margin = new System.Windows.Forms.Padding(4);
            this.btUpdateIP.Name = "btUpdateIP";
            this.btUpdateIP.Size = new System.Drawing.Size(192, 70);
            this.btUpdateIP.TabIndex = 103;
            this.btUpdateIP.Text = "UpdateIP";
            this.btUpdateIP.UseVisualStyleBackColor = true;
            this.btUpdateIP.Click += new System.EventHandler(this.BtUpdateIP_Click_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 110;
            this.label5.Text = "Device Id:";
            // 
            // tbDeviceId
            // 
            this.tbDeviceId.Location = new System.Drawing.Point(121, 157);
            this.tbDeviceId.Name = "tbDeviceId";
            this.tbDeviceId.Size = new System.Drawing.Size(186, 25);
            this.tbDeviceId.TabIndex = 111;
            this.tbDeviceId.Text = "09182011A8D80002";
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.BackColor = System.Drawing.Color.White;
            this.richTextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxEx1.Location = new System.Drawing.Point(999, 1);
            this.richTextBoxEx1.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.ReadOnly = true;
            this.richTextBoxEx1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richTextBoxEx1.Size = new System.Drawing.Size(671, 686);
            this.richTextBoxEx1.TabIndex = 1;
            this.richTextBoxEx1.Text = "\n";
            this.richTextBoxEx1.WordWrap = false;
            // 
            // rtbexMsgBox
            // 
            this.rtbexMsgBox.BackColor = System.Drawing.Color.White;
            this.rtbexMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbexMsgBox.Location = new System.Drawing.Point(544, 2);
            this.rtbexMsgBox.Margin = new System.Windows.Forms.Padding(4);
            this.rtbexMsgBox.Name = "rtbexMsgBox";
            this.rtbexMsgBox.ReadOnly = true;
            this.rtbexMsgBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbexMsgBox.Size = new System.Drawing.Size(425, 686);
            this.rtbexMsgBox.TabIndex = 0;
            this.rtbexMsgBox.Text = "\nv0.1 Test BG96\n";
            this.rtbexMsgBox.WordWrap = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1683, 715);
            this.Controls.Add(this.richTextBoxEx1);
            this.Controls.Add(this.rtbexMsgBox);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BG96 Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tbUpdate.ResumeLayout(false);
            this.tbUpdate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.IO.Ports.SerialPort Serial;
        private System.Windows.Forms.Timer tmSerialCheck;
        private RichTextBoxEx rtbexMsgBox;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCsq;
        private System.Windows.Forms.Label lbDeviceId;
        private System.Windows.Forms.Label lbSvCnt;
        private System.Windows.Forms.Label lbSoftWare;
        private System.Windows.Forms.Label lbcpin;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lbCcid;
        private System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lbcreg;
        private System.Windows.Forms.Label lbQiact;
        private System.Windows.Forms.Label lbSend;
        private System.Windows.Forms.Label lbIPAd;
        private System.Windows.Forms.Label lbIPort;
        private System.Windows.Forms.TextBox tbIPAddr;
        private System.Windows.Forms.TextBox tbIPPort;
        private System.Windows.Forms.Button btConnect;
        private RichTextBoxEx richTextBoxEx1;
        private System.Windows.Forms.TabPage tbUpdate;
        private System.Windows.Forms.ComboBox cbSerialId;
        private System.Windows.Forms.TextBox txIpPort;
        private System.Windows.Forms.TextBox txIpAddr;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btUpdateIP;
        private System.Windows.Forms.Button btUpdateStatus;
        private System.Windows.Forms.TextBox tbDeviceId;
        private System.Windows.Forms.Label label5;
    }
}

