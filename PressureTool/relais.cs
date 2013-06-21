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
    public partial class relais : Form
    {
        private MainForm MainWindow;
        private BindingList<SpData> SP = new BindingList<SpData>();

        public relais(MainForm Parent)
        {
            MainWindow = Parent;
            InitializeComponent();

            SP.Add(new SpData(1));
            SP.Add(new SpData(2));
            SP.Add(new SpData(3));
            SP.Add(new SpData(4));

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = SP;

            readStatus();
        }

        public void readStatus()
        {
            MainWindow.sendQuestion(Questions.SP1);
            MainWindow.sendQuestion(Questions.SP2);
            MainWindow.sendQuestion(Questions.SP3);
            MainWindow.sendQuestion(Questions.SP4);
        }

        public void displayStatus(string channel, string low, string high, int SPx)
        {
            SP[SPx - 1].Channel = channel;
            SP[SPx - 1].Lower = low;
            SP[SPx - 1].Upper = high;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.FormattingApplied = true;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }

    public class SpData : INotifyPropertyChanged
    {
        private string _number;
        private string _channel;
        private string _low;
        private string _high;
        private bool _edit = false;

        public string Number
        {
            get { return this._number; }
            set
            {
                _number = value;
                NotifyPropertyChanged("Number");
            }
        }
        public string Channel
        {
            get { return this._channel; }
            set
            {
                _channel = value;
                NotifyPropertyChanged("Channel");
            }
        }
        public string Lower
        {
            get { return this._low; }
            set
            {
                _low = value;
                NotifyPropertyChanged("Low");
            }
        }
        public string Upper
        {
            get { return this._high; }
            set
            {
                _high = value;
                NotifyPropertyChanged("High");
            }
        }
        public bool Edit
        {
            get { return this._edit; }
            set
            {
                _edit = value;
                NotifyPropertyChanged("Edit");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SpData(int No)
        {
            Number = No.ToString();
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
