namespace PressureTool
{
    partial class LogOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.InputDuration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.InputMinP1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.InputMaxP1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.InputMaxP2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.InputMinP2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Logging duration (DD:)HH:MM (Empty or Zero for unlimited)";
            // 
            // InputDuration
            // 
            this.InputDuration.Location = new System.Drawing.Point(152, 27);
            this.InputDuration.Name = "InputDuration";
            this.InputDuration.Size = new System.Drawing.Size(55, 20);
            this.InputDuration.TabIndex = 1;
            this.InputDuration.Text = "00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Break Condition Channel 1 (leave empty for none)";
            // 
            // InputMinP1
            // 
            this.InputMinP1.Location = new System.Drawing.Point(109, 80);
            this.InputMinP1.Name = "InputMinP1";
            this.InputMinP1.Size = new System.Drawing.Size(55, 20);
            this.InputMinP1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Min Pressure";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Max Pressure";
            // 
            // InputMaxP1
            // 
            this.InputMaxP1.Location = new System.Drawing.Point(247, 80);
            this.InputMaxP1.Name = "InputMaxP1";
            this.InputMaxP1.Size = new System.Drawing.Size(55, 20);
            this.InputMaxP1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Max Pressure";
            // 
            // InputMaxP2
            // 
            this.InputMaxP2.Location = new System.Drawing.Point(247, 129);
            this.InputMaxP2.Name = "InputMaxP2";
            this.InputMaxP2.Size = new System.Drawing.Size(55, 20);
            this.InputMaxP2.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 132);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Min Pressure";
            // 
            // InputMinP2
            // 
            this.InputMinP2.Location = new System.Drawing.Point(109, 129);
            this.InputMinP2.Name = "InputMinP2";
            this.InputMinP2.Size = new System.Drawing.Size(55, 20);
            this.InputMinP2.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Break Condition Channel 2 (leave empty for none)";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(132, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LogOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 189);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.InputMaxP2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.InputMinP2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InputMaxP1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.InputMinP1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InputDuration);
            this.Controls.Add(this.label1);
            this.Name = "LogOptions";
            this.Text = "LogOptions";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InputDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox InputMinP1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox InputMaxP1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox InputMaxP2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox InputMinP2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}