﻿namespace PressureTool
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
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.InputConnectOnStart = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.InputLogLevel = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InputRefresh = new System.Windows.Forms.TextBox();
            this.BoxComPorts = new System.Windows.Forms.ComboBox();
            this.BoxBaud = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InputDisplaySpeed
            // 
            this.InputDisplaySpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputDisplaySpeed.FormattingEnabled = true;
            this.InputDisplaySpeed.Location = new System.Drawing.Point(137, 68);
            this.InputDisplaySpeed.Name = "InputDisplaySpeed";
            this.InputDisplaySpeed.Size = new System.Drawing.Size(91, 21);
            this.InputDisplaySpeed.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Display Interval";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(78, 177);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsButton.TabIndex = 4;
            this.SaveSettingsButton.Text = "OK";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Connect on Start";
            // 
            // InputConnectOnStart
            // 
            this.InputConnectOnStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputConnectOnStart.FormattingEnabled = true;
            this.InputConnectOnStart.Location = new System.Drawing.Point(137, 95);
            this.InputConnectOnStart.Name = "InputConnectOnStart";
            this.InputConnectOnStart.Size = new System.Drawing.Size(91, 21);
            this.InputConnectOnStart.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 125);
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
            this.InputLogLevel.Location = new System.Drawing.Point(137, 122);
            this.InputLogLevel.Name = "InputLogLevel";
            this.InputLogLevel.Size = new System.Drawing.Size(91, 21);
            this.InputLogLevel.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Status Refresh [s]";
            // 
            // InputRefresh
            // 
            this.InputRefresh.Location = new System.Drawing.Point(137, 149);
            this.InputRefresh.Name = "InputRefresh";
            this.InputRefresh.Size = new System.Drawing.Size(91, 20);
            this.InputRefresh.TabIndex = 11;
            // 
            // BoxComPorts
            // 
            this.BoxComPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BoxComPorts.FormattingEnabled = true;
            this.BoxComPorts.Location = new System.Drawing.Point(137, 14);
            this.BoxComPorts.Name = "BoxComPorts";
            this.BoxComPorts.Size = new System.Drawing.Size(91, 21);
            this.BoxComPorts.TabIndex = 40;
            // 
            // BoxBaud
            // 
            this.BoxBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BoxBaud.FormattingEnabled = true;
            this.BoxBaud.Location = new System.Drawing.Point(137, 41);
            this.BoxBaud.Name = "BoxBaud";
            this.BoxBaud.Size = new System.Drawing.Size(91, 21);
            this.BoxBaud.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Serial-Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Baud-Rate";
            // 
            // Settings
            // 
            this.AcceptButton = this.SaveSettingsButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 212);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BoxComPorts);
            this.Controls.Add(this.BoxBaud);
            this.Controls.Add(this.InputRefresh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputLogLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InputConnectOnStart);
            this.Controls.Add(this.SaveSettingsButton);
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
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox InputConnectOnStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox InputLogLevel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InputRefresh;
        private System.Windows.Forms.ComboBox BoxComPorts;
        private System.Windows.Forms.ComboBox BoxBaud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
    }
}