using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PressureTool
{
    public partial class LogOptions : Form
    {
        private MainForm ParentWindow;
        private Color Default;
        public LogOptions(MainForm Window)
        {
            InitializeComponent();
            ParentWindow = Window;
            Default = InputDuration.BackColor;
        }

        private bool checkInput (Control cntrl, out string value, Type valType )
        {
            //cntrl.Text = cntrl.Text.Replace('.', ',');

            try
            {
                if (cntrl.Text != "") 
                    TypeDescriptor.GetConverter(valType).ConvertFromString(cntrl.Text);
                cntrl.BackColor = Default;
                toolTip1.SetToolTip(cntrl, "");
                value = cntrl.Text;
                return true;
            }
            catch
            {
                value = "";
                cntrl.BackColor = Color.Red;
                toolTip1.SetToolTip(cntrl, "No valid input... " + valType.Name + " expected");
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dur, minP1, maxP1, minP2, maxP2;
            dur = minP1 = maxP1 = minP2 = maxP2 = "";

            bool valid = true;
            valid &= checkInput(InputDuration, out dur, typeof(TimeSpan));
            valid &= checkInput(InputMaxP1, out maxP1, typeof(double));
            valid &= checkInput(InputMaxP2, out maxP2, typeof(double));
            valid &= checkInput(InputMinP1, out minP1, typeof(double));
            valid &= checkInput(InputMinP2, out minP2, typeof(double));

            if (valid)
            {

                ParentWindow.startLogging(
                    (dur == "" || dur == "0") ? TimeSpan.Zero : TimeSpan.Parse(dur),
                    (minP1 == "") ? double.MinValue : double.Parse(minP1),
                    (maxP1 == "") ? double.MaxValue : double.Parse(maxP1),
                    (minP2 == "") ? double.MinValue : double.Parse(minP2),
                    (maxP2 == "") ? double.MaxValue : double.Parse(maxP2)
                    );

                this.Close();
                this.Dispose();
            }
            else
            {
                return;
            }

        }
    }
}
