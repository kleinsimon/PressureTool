﻿using System;
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
    /// Provides a Form for changing some Settings
    /// </summary>
    public partial class Settings : Form
    {
        MainForm ParentWindow;

        //Possible Values
        Dictionary<string, string> SpeedValues = new Dictionary<string, string>() 
        { 
            {"0", "1 ms"},
            {"1", "1 s"},
            {"2", "1 min"},
        };

        Dictionary<string, bool> BoolValues = new Dictionary<string, bool>() 
        { 
            {"Off", false},
            {"On", true},
        };

        Dictionary<int, string> Verbosity = new Dictionary<int, string>() 
        { 
            {0, "Nothing"},
            {1, "Some"},
            {2, "More"},
            {3, "All"},
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="Parent">The parent Window</param>
        public Settings(MainForm Parent)
        {
            ParentWindow = Parent;
            InitializeComponent();

            InputDisplaySpeed.DisplayMember = "Value";
            InputDisplaySpeed.ValueMember = "Key";
            InputDisplaySpeed.DataSource = new BindingSource(SpeedValues, null);
            InputDisplaySpeed.DataBindings.Add("SelectedValue", ParentWindow.DisplaySpeed, null, true, DataSourceUpdateMode.Never);

            InputLogSpeed.DisplayMember = "Value";
            InputLogSpeed.ValueMember = "Key";
            InputLogSpeed.DataSource = new BindingSource(SpeedValues, null);
            InputLogSpeed.DataBindings.Add("SelectedValue", ParentWindow.LogSpeed, null, true, DataSourceUpdateMode.Never);

            InputConnectOnStart.DisplayMember = "Key";
            InputConnectOnStart.ValueMember = "Value";
            InputConnectOnStart.DataSource = new BindingSource(BoolValues, null);
            InputConnectOnStart.DataBindings.Add("SelectedValue", ParentWindow.connectOnStart, null, true, DataSourceUpdateMode.Never);

            InputLogLevel.DisplayMember = "Value";
            InputLogLevel.ValueMember = "Key";
            InputLogLevel.DataSource = new BindingSource(Verbosity, null);
            InputLogLevel.DataBindings.Add("SelectedValue", ParentWindow.debugLevel, null, true, DataSourceUpdateMode.Never);

            InputRefresh.DataBindings.Add("Text", ParentWindow.refreshSpeed, null, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Handles the Click event of the saveSettingsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            ParentWindow.DisplaySpeed = (string)InputDisplaySpeed.SelectedValue;
            ParentWindow.LogSpeed = (string)InputLogSpeed.SelectedValue;
            ParentWindow.connectOnStart = (bool)InputConnectOnStart.SelectedValue;
            ParentWindow.debugLevel = (int)InputLogLevel.SelectedValue;
            ParentWindow.refreshSpeed = InputRefresh.Text;
            this.Close();
        }
    }
}
