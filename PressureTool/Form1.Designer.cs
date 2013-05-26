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
            this.TXTcurPressure = new System.Windows.Forms.TextBox();
            this.TXTcurPressureExp = new System.Windows.Forms.TextBox();
            this.TXTUnit = new System.Windows.Forms.TextBox();
            this.TXTmes1SP1 = new System.Windows.Forms.TextBox();
            this.TXTmes1SP2 = new System.Windows.Forms.TextBox();
            this.TXTError = new System.Windows.Forms.TextBox();
            this.txtMes1On = new System.Windows.Forms.TextBox();
            this.TXTmes1Degas = new System.Windows.Forms.TextBox();
            this.TXTmes1Calib = new System.Windows.Forms.TextBox();
            this.TXTmes1Offset = new System.Windows.Forms.TextBox();
            this.BoxBaud = new System.Windows.Forms.ComboBox();
            this.BoxComPorts = new System.Windows.Forms.ComboBox();
            this.ButConnect = new System.Windows.Forms.CheckBox();
            this.getStatus = new System.ComponentModel.BackgroundWorker();
            this.Asker = new System.ComponentModel.BackgroundWorker();
            this.getStatusTimer = new System.Windows.Forms.Timer(this.components);
            this.AskerTimer = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ButLog = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TXTcurPressure
            // 
            this.TXTcurPressure.BackColor = System.Drawing.Color.Black;
            this.TXTcurPressure.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTcurPressure.Font = new System.Drawing.Font("Arial", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTcurPressure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TXTcurPressure.Location = new System.Drawing.Point(12, 46);
            this.TXTcurPressure.Name = "TXTcurPressure";
            this.TXTcurPressure.ReadOnly = true;
            this.TXTcurPressure.Size = new System.Drawing.Size(244, 74);
            this.TXTcurPressure.TabIndex = 0;
            this.TXTcurPressure.Text = "1.0000";
            this.TXTcurPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TXTcurPressureExp
            // 
            this.TXTcurPressureExp.BackColor = System.Drawing.Color.Black;
            this.TXTcurPressureExp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTcurPressureExp.Font = new System.Drawing.Font("Arial", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTcurPressureExp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TXTcurPressureExp.Location = new System.Drawing.Point(181, 12);
            this.TXTcurPressureExp.Name = "TXTcurPressureExp";
            this.TXTcurPressureExp.ReadOnly = true;
            this.TXTcurPressureExp.Size = new System.Drawing.Size(96, 41);
            this.TXTcurPressureExp.TabIndex = 1;
            this.TXTcurPressureExp.Text = "E-3";
            this.TXTcurPressureExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TXTUnit
            // 
            this.TXTUnit.BackColor = System.Drawing.Color.Black;
            this.TXTUnit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTUnit.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TXTUnit.Location = new System.Drawing.Point(293, 12);
            this.TXTUnit.Name = "TXTUnit";
            this.TXTUnit.ReadOnly = true;
            this.TXTUnit.Size = new System.Drawing.Size(46, 19);
            this.TXTUnit.TabIndex = 2;
            this.TXTUnit.Text = "mbar";
            // 
            // TXTmes1SP1
            // 
            this.TXTmes1SP1.BackColor = System.Drawing.Color.Black;
            this.TXTmes1SP1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTmes1SP1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1SP1.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1SP1.Location = new System.Drawing.Point(243, 132);
            this.TXTmes1SP1.Name = "TXTmes1SP1";
            this.TXTmes1SP1.ReadOnly = true;
            this.TXTmes1SP1.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1SP1.TabIndex = 3;
            this.TXTmes1SP1.Text = "SP1";
            // 
            // TXTmes1SP2
            // 
            this.TXTmes1SP2.BackColor = System.Drawing.Color.Black;
            this.TXTmes1SP2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTmes1SP2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1SP2.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1SP2.Location = new System.Drawing.Point(291, 132);
            this.TXTmes1SP2.Name = "TXTmes1SP2";
            this.TXTmes1SP2.ReadOnly = true;
            this.TXTmes1SP2.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1SP2.TabIndex = 4;
            this.TXTmes1SP2.Text = "SP2";
            // 
            // TXTError
            // 
            this.TXTError.BackColor = System.Drawing.Color.Black;
            this.TXTError.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTError.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTError.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.TXTError.Location = new System.Drawing.Point(12, 12);
            this.TXTError.Name = "TXTError";
            this.TXTError.ReadOnly = true;
            this.TXTError.Size = new System.Drawing.Size(46, 19);
            this.TXTError.TabIndex = 5;
            this.TXTError.Text = "Error";
            // 
            // txtMes1On
            // 
            this.txtMes1On.BackColor = System.Drawing.Color.Black;
            this.txtMes1On.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMes1On.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMes1On.ForeColor = System.Drawing.Color.Yellow;
            this.txtMes1On.Location = new System.Drawing.Point(64, 132);
            this.txtMes1On.Name = "txtMes1On";
            this.txtMes1On.ReadOnly = true;
            this.txtMes1On.Size = new System.Drawing.Size(33, 19);
            this.txtMes1On.TabIndex = 6;
            this.txtMes1On.Text = "ON";
            // 
            // TXTmes1Degas
            // 
            this.TXTmes1Degas.BackColor = System.Drawing.Color.Black;
            this.TXTmes1Degas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTmes1Degas.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1Degas.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1Degas.Location = new System.Drawing.Point(99, 132);
            this.TXTmes1Degas.Name = "TXTmes1Degas";
            this.TXTmes1Degas.ReadOnly = true;
            this.TXTmes1Degas.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1Degas.TabIndex = 7;
            this.TXTmes1Degas.Text = "DEG";
            // 
            // TXTmes1Calib
            // 
            this.TXTmes1Calib.BackColor = System.Drawing.Color.Black;
            this.TXTmes1Calib.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTmes1Calib.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1Calib.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1Calib.Location = new System.Drawing.Point(147, 132);
            this.TXTmes1Calib.Name = "TXTmes1Calib";
            this.TXTmes1Calib.ReadOnly = true;
            this.TXTmes1Calib.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1Calib.TabIndex = 8;
            this.TXTmes1Calib.Text = "CAL";
            // 
            // TXTmes1Offset
            // 
            this.TXTmes1Offset.BackColor = System.Drawing.Color.Black;
            this.TXTmes1Offset.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TXTmes1Offset.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXTmes1Offset.ForeColor = System.Drawing.Color.Yellow;
            this.TXTmes1Offset.Location = new System.Drawing.Point(195, 132);
            this.TXTmes1Offset.Name = "TXTmes1Offset";
            this.TXTmes1Offset.ReadOnly = true;
            this.TXTmes1Offset.Size = new System.Drawing.Size(46, 19);
            this.TXTmes1Offset.TabIndex = 9;
            this.TXTmes1Offset.Text = "OFS";
            // 
            // BoxBaud
            // 
            this.BoxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BoxBaud.FormattingEnabled = true;
            this.BoxBaud.Location = new System.Drawing.Point(135, 171);
            this.BoxBaud.Name = "BoxBaud";
            this.BoxBaud.Size = new System.Drawing.Size(121, 21);
            this.BoxBaud.TabIndex = 11;
            // 
            // BoxComPorts
            // 
            this.BoxComPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BoxComPorts.FormattingEnabled = true;
            this.BoxComPorts.Location = new System.Drawing.Point(8, 171);
            this.BoxComPorts.Name = "BoxComPorts";
            this.BoxComPorts.Size = new System.Drawing.Size(121, 21);
            this.BoxComPorts.TabIndex = 12;
            // 
            // ButConnect
            // 
            this.ButConnect.Appearance = System.Windows.Forms.Appearance.Button;
            this.ButConnect.Location = new System.Drawing.Point(262, 169);
            this.ButConnect.Name = "ButConnect";
            this.ButConnect.Size = new System.Drawing.Size(75, 23);
            this.ButConnect.TabIndex = 0;
            this.ButConnect.Text = "Connect";
            this.ButConnect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ButConnect.UseVisualStyleBackColor = true;
            this.ButConnect.CheckedChanged += new System.EventHandler(this.ButConnect_CheckedChanged);
            // 
            // getStatus
            // 
            this.getStatus.DoWork += new System.ComponentModel.DoWorkEventHandler(this.getStatus_DoWork);
            // 
            // Asker
            // 
            this.Asker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Asker_DoWork);
            // 
            // getStatusTimer
            // 
            this.getStatusTimer.Interval = 10000;
            this.getStatusTimer.Tick += new System.EventHandler(this.getStatusTimer_Tick);
            // 
            // AskerTimer
            // 
            this.AskerTimer.Interval = 3;
            this.AskerTimer.Tick += new System.EventHandler(this.AskerTimer_Tick);
            // 
            // ButLog
            // 
            this.ButLog.Location = new System.Drawing.Point(260, 275);
            this.ButLog.Name = "ButLog";
            this.ButLog.Size = new System.Drawing.Size(75, 23);
            this.ButLog.TabIndex = 14;
            this.ButLog.Text = "Logdatei";
            this.ButLog.UseVisualStyleBackColor = true;
            this.ButLog.Click += new System.EventHandler(this.ButLog_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "txt-Datei|*.txt";
            this.saveFileDialog1.RestoreDirectory = true;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(260, 304);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 23);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Start Log";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(8, 198);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(248, 131);
            this.textBox1.TabIndex = 17;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(351, 339);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.ButLog);
            this.Controls.Add(this.ButConnect);
            this.Controls.Add(this.BoxComPorts);
            this.Controls.Add(this.BoxBaud);
            this.Controls.Add(this.TXTmes1Offset);
            this.Controls.Add(this.TXTmes1Calib);
            this.Controls.Add(this.TXTmes1Degas);
            this.Controls.Add(this.txtMes1On);
            this.Controls.Add(this.TXTError);
            this.Controls.Add(this.TXTmes1SP2);
            this.Controls.Add(this.TXTmes1SP1);
            this.Controls.Add(this.TXTUnit);
            this.Controls.Add(this.TXTcurPressureExp);
            this.Controls.Add(this.TXTcurPressure);
            this.Name = "MainForm";
            this.Text = "PressureTool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TXTcurPressure;
        private System.Windows.Forms.TextBox TXTcurPressureExp;
        private System.Windows.Forms.TextBox TXTUnit;
        private System.Windows.Forms.TextBox TXTmes1SP1;
        private System.Windows.Forms.TextBox TXTmes1SP2;
        private System.Windows.Forms.TextBox TXTError;
        private System.Windows.Forms.TextBox txtMes1On;
        private System.Windows.Forms.TextBox TXTmes1Degas;
        private System.Windows.Forms.TextBox TXTmes1Calib;
        private System.Windows.Forms.TextBox TXTmes1Offset;
        private System.Windows.Forms.ComboBox BoxBaud;
        private System.Windows.Forms.ComboBox BoxComPorts;
        private System.Windows.Forms.CheckBox ButConnect;
        private System.ComponentModel.BackgroundWorker getStatus;
        private System.ComponentModel.BackgroundWorker Asker;
        private System.Windows.Forms.Timer getStatusTimer;
        private System.Windows.Forms.Timer AskerTimer;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button ButLog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;

    }
}

