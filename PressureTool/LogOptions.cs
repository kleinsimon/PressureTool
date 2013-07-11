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
    /// <summary>
    /// Provides a Form to set Logging Options
    /// </summary>
    /// 

    public partial class LogOptions : Form
    {
        Dictionary<string, string> SpeedValues = new Dictionary<string, string>() 
        { 
            {"0", "1 ms"},
            {"1", "1 s"},
            {"2", "1 min"},
        };

        private MainForm ParentWindow;
        private Color Default;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogOptions"/> class.
        /// </summary>
        /// <param name="Window">The Parent Window</param>
        public LogOptions(MainForm Window)
        {
            InitializeComponent();
            ParentWindow = Window;
            Default = InputDuration.BackColor;

            InputLogSpeed.DisplayMember = "Value";
            InputLogSpeed.ValueMember = "Key";
            InputLogSpeed.DataSource = new BindingSource(SpeedValues, null);
            InputLogSpeed.SelectedValue = Properties.Settings.Default.LogSpeed;
        }

        /// <summary>
        /// Checks the Content of a Control for validity. Returns the true if the Text-Property may be parsed in the given Type, also passes out the Text-Property.
        /// </summary>
        /// <param name="cntrl">The Control to be checked</param>
        /// <param name="value">Gives bacl the Value of the Text-Property</param>
        /// <param name="valType">Type to be parsed to</param>
        /// <returns></returns>
        private bool checkInput (Control cntrl, out string value, Type valType )
        {
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

        /// <summary>
        /// Handles the Click event of the startLoggingButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void startLoggingButton_Click(object sender, EventArgs e)
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
                    (maxP2 == "") ? double.MaxValue : double.Parse(maxP2),
                    (string) InputLogSpeed.SelectedValue
                    );

                Properties.Settings.Default.LogSpeed = (string) InputLogSpeed.SelectedValue;
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
