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
        public LogOptions(MainForm Window)
        {
            InitializeComponent();
            ParentWindow = Window;
        }

        private bool checkInput (Control cntrl, out string value)
        {
            double trash;
            cntrl.Text = cntrl.Text.Replace('.', ',');
            if (double.TryParse(cntrl.Text, out trash) || cntrl.Text == string.Empty)
            {
                cntrl.BackColor = Color.Red;
                toolTip1.SetToolTip(cntrl, "Keine gültige Eingabe");
                value = cntrl.Text;
                return true;
            }
            else
            {
                value = "";
                return false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dur, minP1, maxP1, minP2, maxP2;
            dur = minP1 = maxP1 = minP2 = maxP2 = "";

            bool valid = true;
            valid = valid && checkInput(InputDuration, out dur);
            valid = valid && checkInput(InputMaxP1, out maxP1);
            valid = valid && checkInput(InputMaxP2, out maxP2);
            valid = valid && checkInput(InputMinP1, out minP1);
            valid = valid && checkInput(InputMinP2, out minP2);


            if (valid)
            {
                ParentWindow.startLogging(
                    (dur == "") ? 0d : double.Parse(dur), 
                    (maxP1 == "") ? double.MaxValue : double.Parse(maxP1), 
                    (maxP2 == "") ? double.MaxValue : double.Parse(maxP2), 
                    (minP1 == "") ? double.MinValue : double.Parse(minP1), 
                    (minP2 == "") ? double.MinValue : double.Parse(minP2)
                    );
            }
            this.Close();
            this.Dispose();
        }


    }
}
