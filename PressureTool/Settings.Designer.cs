namespace PressureTool
{
    partial class Settings
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
            this.InputDisplaySpeed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.InputLogSpeed = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.InputConnectOnStart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.InputLogLevel = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // InputDisplaySpeed
            // 
            this.InputDisplaySpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputDisplaySpeed.FormattingEnabled = true;
            this.InputDisplaySpeed.Location = new System.Drawing.Point(137, 6);
            this.InputDisplaySpeed.Name = "InputDisplaySpeed";
            this.InputDisplaySpeed.Size = new System.Drawing.Size(91, 21);
            this.InputDisplaySpeed.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Display Interval";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Log Interval";
            // 
            // InputLogSpeed
            // 
            this.InputLogSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputLogSpeed.FormattingEnabled = true;
            this.InputLogSpeed.Location = new System.Drawing.Point(137, 33);
            this.InputLogSpeed.Name = "InputLogSpeed";
            this.InputLogSpeed.Size = new System.Drawing.Size(91, 21);
            this.InputLogSpeed.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(86, 123);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Connect on Start";
            // 
            // InputConnectOnStart
            // 
            this.InputConnectOnStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputConnectOnStart.FormattingEnabled = true;
            this.InputConnectOnStart.Location = new System.Drawing.Point(137, 60);
            this.InputConnectOnStart.Name = "InputConnectOnStart";
            this.InputConnectOnStart.Size = new System.Drawing.Size(91, 21);
            this.InputConnectOnStart.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Verbosity";
            // 
            // InputLogLevel
            // 
            this.InputLogLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputLogLevel.FormattingEnabled = true;
            this.InputLogLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.InputLogLevel.Location = new System.Drawing.Point(137, 87);
            this.InputLogLevel.Name = "InputLogLevel";
            this.InputLogLevel.Size = new System.Drawing.Size(91, 21);
            this.InputLogLevel.TabIndex = 7;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 158);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputLogLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InputConnectOnStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InputLogSpeed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputDisplaySpeed);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox InputDisplaySpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox InputLogSpeed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox InputConnectOnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox InputLogLevel;
    }
}