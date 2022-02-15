namespace DeviceTest
{
    partial class formWater
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lbShowData = new System.Windows.Forms.Label();
            this.chartData = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.Serial = new System.IO.Ports.SerialPort(this.components);
            this.tmSerialCheck = new System.Windows.Forms.Timer(this.components);
            this.lbZongLiuLiang = new System.Windows.Forms.Label();
            this.lbLiuSu = new System.Windows.Forms.Label();
            this.lbShuiYa = new System.Windows.Forms.Label();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.rtbexMsgBox = new DeviceTest.RichTextBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).BeginInit();
            this.SuspendLayout();
            // 
            // lbShowData
            // 
            this.lbShowData.AutoSize = true;
            this.lbShowData.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShowData.Location = new System.Drawing.Point(148, 174);
            this.lbShowData.Name = "lbShowData";
            this.lbShowData.Size = new System.Drawing.Size(89, 19);
            this.lbShowData.TabIndex = 119;
            this.lbShowData.Text = "ShowData";
            this.lbShowData.Visible = false;
            // 
            // chartData
            // 
            chartArea6.AxisX.MajorGrid.Enabled = false;
            chartArea6.AxisY.MajorGrid.Enabled = false;
            chartArea6.BackColor = System.Drawing.Color.Moccasin;
            chartArea6.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea6.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            chartArea6.Name = "ChartArea1";
            this.chartData.ChartAreas.Add(chartArea6);
            legend6.Enabled = false;
            legend6.Name = "Legend1";
            this.chartData.Legends.Add(legend6);
            this.chartData.Location = new System.Drawing.Point(9, 105);
            this.chartData.Name = "chartData";
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series16.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            series16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            series16.LabelToolTip = "#VAL{D2}L";
            series16.Legend = "Legend1";
            series16.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series16.Name = "水压";
            series16.YValuesPerPoint = 2;
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series17.Legend = "Legend1";
            series17.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Square;
            series17.Name = "流速";
            series18.ChartArea = "ChartArea1";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series18.Legend = "Legend1";
            series18.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Star5;
            series18.Name = "总流量";
            this.chartData.Series.Add(series16);
            this.chartData.Series.Add(series17);
            this.chartData.Series.Add(series18);
            this.chartData.Size = new System.Drawing.Size(378, 187);
            this.chartData.TabIndex = 118;
            this.chartData.Text = "chart1";
            this.chartData.MouseLeave += new System.EventHandler(this.chartMouseLeave);
            this.chartData.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartMouseMove);
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(10, 4);
            this.cmbSerialPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(378, 20);
            this.cmbSerialPort.TabIndex = 116;
            this.cmbSerialPort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbSerialPort_MouseClick);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("宋体", 18F);
            this.btnStart.Location = new System.Drawing.Point(8, 29);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(185, 35);
            this.btnStart.TabIndex = 115;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
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
            // lbZongLiuLiang
            // 
            this.lbZongLiuLiang.BackColor = System.Drawing.Color.DarkOrange;
            this.lbZongLiuLiang.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbZongLiuLiang.ForeColor = System.Drawing.Color.White;
            this.lbZongLiuLiang.Location = new System.Drawing.Point(267, 67);
            this.lbZongLiuLiang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbZongLiuLiang.Name = "lbZongLiuLiang";
            this.lbZongLiuLiang.Size = new System.Drawing.Size(121, 35);
            this.lbZongLiuLiang.TabIndex = 122;
            this.lbZongLiuLiang.Text = "总流量";
            this.lbZongLiuLiang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbZongLiuLiang.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMouseClick);
            // 
            // lbLiuSu
            // 
            this.lbLiuSu.BackColor = System.Drawing.Color.DarkOrange;
            this.lbLiuSu.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbLiuSu.ForeColor = System.Drawing.Color.White;
            this.lbLiuSu.Location = new System.Drawing.Point(138, 67);
            this.lbLiuSu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLiuSu.Name = "lbLiuSu";
            this.lbLiuSu.Size = new System.Drawing.Size(121, 35);
            this.lbLiuSu.TabIndex = 121;
            this.lbLiuSu.Text = "流速";
            this.lbLiuSu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbLiuSu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMouseClick);
            // 
            // lbShuiYa
            // 
            this.lbShuiYa.BackColor = System.Drawing.Color.DarkOrange;
            this.lbShuiYa.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbShuiYa.ForeColor = System.Drawing.Color.White;
            this.lbShuiYa.Location = new System.Drawing.Point(9, 67);
            this.lbShuiYa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbShuiYa.Name = "lbShuiYa";
            this.lbShuiYa.Size = new System.Drawing.Size(121, 35);
            this.lbShuiYa.TabIndex = 120;
            this.lbShuiYa.Text = "水压";
            this.lbShuiYa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbShuiYa.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbMouseClick);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Font = new System.Drawing.Font("宋体", 18F);
            this.btnResetAll.Location = new System.Drawing.Point(201, 29);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(187, 35);
            this.btnResetAll.TabIndex = 124;
            this.btnResetAll.Text = "恢复出厂";
            this.btnResetAll.UseVisualStyleBackColor = true;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // rtbexMsgBox
            // 
            this.rtbexMsgBox.BackColor = System.Drawing.Color.White;
            this.rtbexMsgBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbexMsgBox.Location = new System.Drawing.Point(8, 298);
            this.rtbexMsgBox.Name = "rtbexMsgBox";
            this.rtbexMsgBox.ReadOnly = true;
            this.rtbexMsgBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtbexMsgBox.Size = new System.Drawing.Size(380, 170);
            this.rtbexMsgBox.TabIndex = 123;
            this.rtbexMsgBox.Text = "只支持将变送器接入485转串口工具\n";
            this.rtbexMsgBox.WordWrap = false;
            // 
            // formWater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 477);
            this.Controls.Add(this.btnResetAll);
            this.Controls.Add(this.lbShowData);
            this.Controls.Add(this.chartData);
            this.Controls.Add(this.cmbSerialPort);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.rtbexMsgBox);
            this.Controls.Add(this.lbZongLiuLiang);
            this.Controls.Add(this.lbLiuSu);
            this.Controls.Add(this.lbShuiYa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "formWater";
            this.Text = "formWater";
            this.Load += new System.EventHandler(this.formWater_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbShowData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartData;
        private System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.Button btnStart;
        private System.IO.Ports.SerialPort Serial;
        private System.Windows.Forms.Timer tmSerialCheck;
        private DeviceTest.RichTextBoxEx rtbexMsgBox;
        private System.Windows.Forms.Label lbZongLiuLiang;
        private System.Windows.Forms.Label lbLiuSu;
        private System.Windows.Forms.Label lbShuiYa;
        private System.Windows.Forms.Button btnResetAll;
    }
}