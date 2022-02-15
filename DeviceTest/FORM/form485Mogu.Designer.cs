namespace DeviceTest
{
    partial class form485Mogu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.cmbTestMode = new System.Windows.Forms.ComboBox();
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.btnExPortStart = new System.Windows.Forms.Button();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lbShowData = new System.Windows.Forms.Label();
            this.Serial = new System.IO.Ports.SerialPort(this.components);
            this.tmSerialCheck = new System.Windows.Forms.Timer(this.components);
            this.lbWendu = new System.Windows.Forms.Label();
            this.lbShidu = new System.Windows.Forms.Label();
            this.lbLux = new System.Windows.Forms.Label();
            this.rtbexMsgBox = new DeviceTest.RichTextBoxEx();
            this.lbSensorId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbTestMode
            // 
            this.cmbTestMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTestMode.FormattingEnabled = true;
            this.cmbTestMode.Items.AddRange(new object[] {
            "老化测试",
            "生产测试"});
            this.cmbTestMode.Location = new System.Drawing.Point(8, 31);
            this.cmbTestMode.Name = "cmbTestMode";
            this.cmbTestMode.Size = new System.Drawing.Size(120, 20);
            this.cmbTestMode.TabIndex = 106;
            this.cmbTestMode.Text = "选择测试模式";
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(8, 6);
            this.cmbSerialPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(120, 20);
            this.cmbSerialPort.TabIndex = 105;
            this.cmbSerialPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbSerialPort_MouseClick);
            // 
            // btnExPortStart
            // 
            this.btnExPortStart.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExPortStart.Location = new System.Drawing.Point(135, 6);
            this.btnExPortStart.Name = "btnExPortStart";
            this.btnExPortStart.Size = new System.Drawing.Size(251, 45);
            this.btnExPortStart.TabIndex = 104;
            this.btnExPortStart.Text = "启动";
            this.btnExPortStart.UseVisualStyleBackColor = true;
            this.btnExPortStart.Click += new System.EventHandler(this.btnExPortStart_Click);
            // 
            // chartData
            // 
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.BackColor = System.Drawing.Color.Moccasin;
            chartArea2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            chartArea2.Name = "ChartArea1";
            this.chartData.ChartAreas.Add(chartArea2);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.chartData.Legends.Add(legend2);
            this.chartData.Location = new System.Drawing.Point(8, 134);
            this.chartData.Name = "chartData";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series4.Legend = "Legend1";
            series4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series4.Name = "温度";
            series4.YValuesPerPoint = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Legend = "Legend1";
            series5.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series5.Name = "湿度";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
            series6.Name = "光照";
            this.chartData.Series.Add(series4);
            this.chartData.Series.Add(series5);
            this.chartData.Series.Add(series6);
            this.chartData.Size = new System.Drawing.Size(378, 174);
            this.chartData.TabIndex = 108;
            this.chartData.Text = "chart1";
            this.chartData.MouseLeave += new System.EventHandler(this.chartMouseLeave);
            this.chartData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartMouseMove);
            // 
            // lbShowData
            // 
            this.lbShowData.AutoSize = true;
            this.lbShowData.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShowData.Location = new System.Drawing.Point(146, 171);
            this.lbShowData.Name = "lbShowData";
            this.lbShowData.Size = new System.Drawing.Size(89, 19);
            this.lbShowData.TabIndex = 109;
            this.lbShowData.Text = "ShowData";
            this.lbShowData.Visible = false;
            // 
            // Serial
            // 
            this.Serial.BaudRate = 115200;
            // 
            // tmSerialCheck
            // 
            this.tmSerialCheck.Interval = 1000;
            this.tmSerialCheck.Tick += new System.EventHandler(this.tmSerialCheck_Tick);
            // 
            // lbWendu
            // 
            this.lbWendu.BackColor = System.Drawing.Color.DarkOrange;
            this.lbWendu.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWendu.ForeColor = System.Drawing.Color.White;
            this.lbWendu.Location = new System.Drawing.Point(7, 96);
            this.lbWendu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWendu.Name = "lbWendu";
            this.lbWendu.Size = new System.Drawing.Size(121, 35);
            this.lbWendu.TabIndex = 110;
            this.lbWendu.Text = "温度";
            this.lbWendu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbWendu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMouseClick);
            // 
            // lbShidu
            // 
            this.lbShidu.BackColor = System.Drawing.Color.DarkOrange;
            this.lbShidu.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShidu.ForeColor = System.Drawing.Color.White;
            this.lbShidu.Location = new System.Drawing.Point(136, 96);
            this.lbShidu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbShidu.Name = "lbShidu";
            this.lbShidu.Size = new System.Drawing.Size(121, 35);
            this.lbShidu.TabIndex = 111;
            this.lbShidu.Text = "湿度";
            this.lbShidu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbShidu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMouseClick);
            // 
            // lbLux
            // 
            this.lbLux.BackColor = System.Drawing.Color.DarkOrange;
            this.lbLux.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLux.ForeColor = System.Drawing.Color.White;
            this.lbLux.Location = new System.Drawing.Point(265, 96);
            this.lbLux.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLux.Name = "lbLux";
            this.lbLux.Size = new System.Drawing.Size(121, 35);
            this.lbLux.TabIndex = 112;
            this.lbLux.Text = "光照度";
            this.lbLux.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLux.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMouseClick);
            // 
            // rtbexMsgBox
            // 
            this.rtbexMsgBox.BackColor = System.Drawing.Color.White;
            this.rtbexMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbexMsgBox.Location = new System.Drawing.Point(8, 314);
            this.rtbexMsgBox.Name = "rtbexMsgBox";
            this.rtbexMsgBox.ReadOnly = true;
            this.rtbexMsgBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbexMsgBox.Size = new System.Drawing.Size(378, 151);
            this.rtbexMsgBox.TabIndex = 113;
            this.rtbexMsgBox.Text = "仅支持直接将蘑菇接到串口转485工具上\n双击传感器ID标签可以复制值";
            this.rtbexMsgBox.WordWrap = false;
            // 
            // lbSensorId
            // 
            this.lbSensorId.BackColor = System.Drawing.Color.DarkOrange;
            this.lbSensorId.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSensorId.ForeColor = System.Drawing.Color.White;
            this.lbSensorId.Location = new System.Drawing.Point(7, 56);
            this.lbSensorId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSensorId.Name = "lbSensorId";
            this.lbSensorId.Size = new System.Drawing.Size(379, 35);
            this.lbSensorId.TabIndex = 114;
            this.lbSensorId.Text = "传感器ID";
            this.lbSensorId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbSensorId.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbSensorId_MouseDoubleClick);
            // 
            // form485Mogu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 477);
            this.Controls.Add(this.lbSensorId);
            this.Controls.Add(this.rtbexMsgBox);
            this.Controls.Add(this.lbLux);
            this.Controls.Add(this.lbShidu);
            this.Controls.Add(this.lbWendu);
            this.Controls.Add(this.lbShowData);
            this.Controls.Add(this.chartData);
            this.Controls.Add(this.cmbTestMode);
            this.Controls.Add(this.cmbSerialPort);
            this.Controls.Add(this.btnExPortStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "form485Mogu";
            this.Text = "form485Mogu";
            this.Load += new System.EventHandler(this.form485Mogu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbTestMode;
        private System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.Button btnExPortStart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private System.Windows.Forms.Label lbShowData;
        private System.IO.Ports.SerialPort Serial;
        private System.Windows.Forms.Timer tmSerialCheck;
        private System.Windows.Forms.Label lbWendu;
        private System.Windows.Forms.Label lbShidu;
        private System.Windows.Forms.Label lbLux;
        private RichTextBoxEx rtbexMsgBox;
        private System.Windows.Forms.Label lbSensorId;
    }
}