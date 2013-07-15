namespace PressureTool
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PanelHideButton = new System.Windows.Forms.PictureBox();
            this.TXTmes1Offset = new System.Windows.Forms.Label();
            this.TXTmes1Calib = new System.Windows.Forms.Label();
            this.TXTmes1Degas = new System.Windows.Forms.Label();
            this.txtMes1On = new System.Windows.Forms.Label();
            this.TXTError = new System.Windows.Forms.Label();
            this.TXTmes1SP2 = new System.Windows.Forms.Label();
            this.TXTmes1SP1 = new System.Windows.Forms.Label();
            this.TXTUnit = new System.Windows.Forms.Label();
            this.TXTcurPressureExp = new System.Windows.Forms.Label();
            this.TXTcurPressure = new System.Windows.Forms.Label();
            this.BUTChart = new System.Windows.Forms.CheckBox();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ButLogStart = new System.Windows.Forms.CheckBox();
            this.ButLog = new System.Windows.Forms.Button();
            this.ButConnect = new System.Windows.Forms.CheckBox();
            this.getStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.Asker = new System.ComponentModel.BackgroundWorker();
            this.AskerTimer = new System.Windows.Forms.Timer(this.components);
            this.getStatus = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ConnectionWatchDog = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelHideButton)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PanelHideButton);
            this.splitContainer1.Panel1.Controls.Add(this.TXTmes1Offset);
            this.splitContainer1.Panel1.Controls.Add(this.TXTmes1Calib);
            this.splitContainer1.Panel1.Controls.Add(this.TXTmes1Degas);
            this.splitContainer1.Panel1.Controls.Add(this.txtMes1On);
            this.splitContainer1.Panel1.Controls.Add(this.TXTError);
            this.splitContainer1.Panel1.Controls.Add(this.TXTmes1SP2);
            this.splitContainer1.Panel1.Controls.Add(this.TXTmes1SP1);
            this.splitContainer1.Panel1.Controls.Add(this.TXTUnit);
            this.splitContainer1.Panel1.Controls.Add(this.TXTcurPressureExp);
            this.splitContainer1.Panel1.Controls.Add(this.TXTcurPressure);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BUTChart);
            this.splitContainer1.Panel2.Controls.Add(this.ConfigButton);
            this.splitContainer1.Panel2.Controls.Add(this.textBox1);
            this.splitContainer1.Panel2.Controls.Add(this.ButLogStart);
            this.splitContainer1.Panel2.Controls.Add(this.ButLog);
            this.splitContainer1.Panel2.Controls.Add(this.ButConnect);
            this.splitContainer1.Size = new System.Drawing.Size(350, 358);
            this.splitContainer1.SplitterDistance = 177;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // PanelHideButton
            // 
            this.PanelHideButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelHideButton.Location = new System.Drawing.Point(0, 154);
            this.PanelHideButton.Name = "PanelHideButton";
            this.PanelHideButton.Size = new System.Drawing.Size(350, 23);
            this.PanelHideButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.PanelHideButton.TabIndex = 31;
            this.PanelHideButton.TabStop = false;
            this.PanelHideButton.Click += new System.EventHandler(this.PanelHideButton_Click);
            // 
            // TXTmes1Offset
            // 
            this.TXTmes1Offset.BackColor = System.Drawing.Color.Black;
            this.TXTmes1Offset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1Offset.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1Offset.Location = new System.Drawing.Point(195, 132);
            this.TXTmes1Offset.Name = "TXTmes1Offset";
            this.TXTmes1Offset.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1Offset.TabIndex = 29;
            this.TXTmes1Offset.Text = "OFS";
            // 
            // TXTmes1Calib
            // 
            this.TXTmes1Calib.BackColor = System.Drawing.Color.Black;
            this.TXTmes1Calib.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1Calib.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1Calib.Location = new System.Drawing.Point(147, 132);
            this.TXTmes1Calib.Name = "TXTmes1Calib";
            this.TXTmes1Calib.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1Calib.TabIndex = 28;
            this.TXTmes1Calib.Text = "CAL";
            // 
            // TXTmes1Degas
            // 
            this.TXTmes1Degas.BackColor = System.Drawing.Color.Black;
            this.TXTmes1Degas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1Degas.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1Degas.Location = new System.Drawing.Point(99, 132);
            this.TXTmes1Degas.Name = "TXTmes1Degas";
            this.TXTmes1Degas.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1Degas.TabIndex = 27;
            this.TXTmes1Degas.Text = "DEG";
            // 
            // txtMes1On
            // 
            this.txtMes1On.BackColor = System.Drawing.Color.Black;
            this.txtMes1On.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMes1On.ForeColor = System.Drawing.Color.Yellow;
            this.txtMes1On.Location = new System.Drawing.Point(64, 132);
            this.txtMes1On.Name = "txtMes1On";
            this.txtMes1On.Size = new System.Drawing.Size(33, 19);
            this.txtMes1On.TabIndex = 26;
            this.txtMes1On.Text = "ON";
            // 
            // TXTError
            // 
            this.TXTError.BackColor = System.Drawing.Color.Black;
            this.TXTError.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TXTError.Location = new System.Drawing.Point(12, 12);
            this.TXTError.Name = "TXTError";
            this.TXTError.Size = new System.Drawing.Size(57, 19);
            this.TXTError.TabIndex = 25;
            this.TXTError.Text = "Error";
            // 
            // TXTmes1SP2
            // 
            this.TXTmes1SP2.BackColor = System.Drawing.Color.Black;
            this.TXTmes1SP2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TXTmes1SP2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1SP2.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1SP2.Location = new System.Drawing.Point(291, 132);
            this.TXTmes1SP2.Name = "TXTmes1SP2";
            this.TXTmes1SP2.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1SP2.TabIndex = 24;
            this.TXTmes1SP2.Text = "SP2";
            this.TXTmes1SP2.Click += new System.EventHandler(this.TXTmes1SP1_Click);
            // 
            // TXTmes1SP1
            // 
            this.TXTmes1SP1.BackColor = System.Drawing.Color.Black;
            this.TXTmes1SP1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TXTmes1SP1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1SP1.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1SP1.Location = new System.Drawing.Point(243, 132);
            this.TXTmes1SP1.Name = "TXTmes1SP1";
            this.TXTmes1SP1.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1SP1.TabIndex = 23;
            this.TXTmes1SP1.Text = "SP1";
            this.TXTmes1SP1.Click += new System.EventHandler(this.TXTmes1SP1_Click);
            // 
            // TXTUnit
            // 
            this.TXTUnit.BackColor = System.Drawing.Color.Black;
            this.TXTUnit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TXTUnit.Location = new System.Drawing.Point(293, 12);
            this.TXTUnit.Name = "TXTUnit";
            this.TXTUnit.Size = new System.Drawing.Size(46, 19);
            this.TXTUnit.TabIndex = 22;
            this.TXTUnit.Text = "mbar";
            // 
            // TXTcurPressureExp
            // 
            this.TXTcurPressureExp.BackColor = System.Drawing.Color.Black;
            this.TXTcurPressureExp.Font = new System.Drawing.Font("Arial", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTcurPressureExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TXTcurPressureExp.Location = new System.Drawing.Point(199, 12);
            this.TXTcurPressureExp.Name = "TXTcurPressureExp";
            this.TXTcurPressureExp.Size = new System.Drawing.Size(99, 41);
            this.TXTcurPressureExp.TabIndex = 21;
            this.TXTcurPressureExp.Text = "E-3";
            this.TXTcurPressureExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TXTcurPressure
            // 
            this.TXTcurPressure.BackColor = System.Drawing.Color.Black;
            this.TXTcurPressure.Font = new System.Drawing.Font("Arial", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTcurPressure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TXTcurPressure.Location = new System.Drawing.Point(3, 53);
            this.TXTcurPressure.Name = "TXTcurPressure";
            this.TXTcurPressure.Size = new System.Drawing.Size(347, 74);
            this.TXTcurPressure.TabIndex = 20;
            this.TXTcurPressure.Text = "8.8888";
            this.TXTcurPressure.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BUTChart
            // 
            this.BUTChart.Appearance = System.Windows.Forms.Appearance.Button;
            this.BUTChart.Image = ((System.Drawing.Image)(resources.GetObject("BUTChart.Image")));
            this.BUTChart.Location = new System.Drawing.Point(266, 143);
            this.BUTChart.Name = "BUTChart";
            this.BUTChart.Size = new System.Drawing.Size(32, 23);
            this.BUTChart.TabIndex = 43;
            this.BUTChart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BUTChart.UseVisualStyleBackColor = true;
            this.BUTChart.CheckedChanged += new System.EventHandler(this.BUTChart_CheckedChanged);
            // 
            // ConfigButton
            // 
            this.ConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("ConfigButton.Image")));
            this.ConfigButton.Location = new System.Drawing.Point(314, 143);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(27, 23);
            this.ConfigButton.TabIndex = 42;
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(12, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(248, 160);
            this.textBox1.TabIndex = 41;
            // 
            // ButLogStart
            // 
            this.ButLogStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButLogStart.Enabled = false;
            this.ButLogStart.Image = global::PressureTool.Properties.Resources.RecordHS;
            this.ButLogStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButLogStart.Location = new System.Drawing.Point(266, 64);
            this.ButLogStart.Name = "ButLogStart";
            this.ButLogStart.Size = new System.Drawing.Size(75, 23);
            this.ButLogStart.TabIndex = 40;
            this.ButLogStart.Text = "Log...";
            this.ButLogStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ButLogStart.UseVisualStyleBackColor = true;
            this.ButLogStart.Click += new System.EventHandler(this.ButLogStart_Clicked);
            // 
            // ButLog
            // 
            this.ButLog.BackColor = System.Drawing.Color.Transparent;
            this.ButLog.Image = global::PressureTool.Properties.Resources.openfolderHS;
            this.ButLog.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButLog.Location = new System.Drawing.Point(266, 35);
            this.ButLog.Name = "ButLog";
            this.ButLog.Size = new System.Drawing.Size(75, 23);
            this.ButLog.TabIndex = 39;
            this.ButLog.Text = "Logfile";
            this.ButLog.UseVisualStyleBackColor = false;
            this.ButLog.Click += new System.EventHandler(this.ButLog_Click);
            // 
            // ButConnect
            // 
            this.ButConnect.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButConnect.Location = new System.Drawing.Point(266, 6);
            this.ButConnect.Name = "ButConnect";
            this.ButConnect.Size = new System.Drawing.Size(75, 23);
            this.ButConnect.TabIndex = 0;
            this.ButConnect.Text = "Connect";
            this.ButConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ButConnect.UseVisualStyleBackColor = true;
            this.ButConnect.Click += new System.EventHandler(this.ButConnect_Clicked);
            // 
            // getStatusTimer
            // 
            this.getStatusTimer.Interval = 10000;
            this.getStatusTimer.Tick += new System.EventHandler(this.getStatusTimer_Tick);
            // 
            // Asker
            // 
            this.Asker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Asker_DoWork);
            // 
            // AskerTimer
            // 
            this.AskerTimer.Interval = 5000;
            this.AskerTimer.Tick += new System.EventHandler(this.AskerTimer_Tick);
            // 
            // getStatus
            // 
            this.getStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getStatus_DoWork);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "txt-Datei|*.txt";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // ConnectionWatchDog
            // 
            this.ConnectionWatchDog.Interval = 1000;
            this.ConnectionWatchDog.Tick += new System.EventHandler(this.ConnectionWatchDog_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(350, 358);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "PressureTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelHideButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label TXTmes1Offset;
        private System.Windows.Forms.Label TXTmes1Calib;
        private System.Windows.Forms.Label TXTmes1Degas;
        private System.Windows.Forms.Label txtMes1On;
        private System.Windows.Forms.Label TXTError;
        private System.Windows.Forms.Label TXTmes1SP2;
        private System.Windows.Forms.Label TXTmes1SP1;
        private System.Windows.Forms.Label TXTUnit;
        private System.Windows.Forms.Label TXTcurPressureExp;
        private System.Windows.Forms.Label TXTcurPressure;
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ButLogStart;
        private System.Windows.Forms.Button ButLog;
        private System.Windows.Forms.CheckBox ButConnect;
        private System.Windows.Forms.Timer getStatusTimer;
        private System.ComponentModel.BackgroundWorker Asker;
        private System.Windows.Forms.Timer AskerTimer;
        private System.ComponentModel.BackgroundWorker getStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PictureBox PanelHideButton;
        private System.Windows.Forms.Timer ConnectionWatchDog;
        private System.Windows.Forms.CheckBox BUTChart;


    }
}

