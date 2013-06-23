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
    public partial class PressureChart : Form
    {
        private MainForm MainWindow = null;
        public bool Pause = false;

        public PressureChart(MainForm Parent)
        {
            MainWindow = Parent;
            InitializeComponent();
        }

        public void AddToChart(DateTime Time, double Value)
        {
            if (!this.Visible || Pause) return;
            chart1.Series[0].Points.AddXY(Time, Value);
            chart1.Update();
        }

        public void clear()
        {
            chart1.Series[0].Points.Clear();
            chart1.Update();
        }

        private void PressureChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
            MainWindow.onChartClosed();
        }
    }
}
