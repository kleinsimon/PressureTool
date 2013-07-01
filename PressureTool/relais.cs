using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private BindingList<RelData> Rel = new BindingList<RelData>();
        private Dictionary<relStatus, RelData> RelTable = new Dictionary<relStatus, RelData>();
        private relStatus[] RVal = (relStatus[])Enum.GetValues(typeof(relStatus));
        private bool _override = false;

        private bool Override
        {
            get {
                return _override;
            }
            set
            {
                if (value)
                {
                    _override = true;
                    InputOverride.Checked = true;
                    //InputSetStatus.Enabled = true;
                    dataGridView2.ReadOnly = false;
                }
                else
                {
                    _override = false;
                    InputOverride.Checked = false;
                    //InputSetStatus.Enabled = false;
                    dataGridView2.ReadOnly = true;
                }
            }
        }

        public relais(MainForm Parent)
        {
            MainWindow = Parent;
            InitializeComponent();
            Override = false;

            SP.Add(new SpData(1));
            SP.Add(new SpData(2));
            SP.Add(new SpData(3));
            SP.Add(new SpData(4));

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = SP;

            for (int i = 0; i < RVal.Length; i++)
            {
                RelData tmp = new RelData(i, RVal[i]);
                Rel.Add(tmp);
                RelTable.Add(RVal[i], tmp);
            }

            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = Rel;


            readStatus();
        }

        public void readStatus()
        {
            MainWindow.sendQuestion(Questions.SP1);
            MainWindow.sendQuestion(Questions.SP2);
            MainWindow.sendQuestion(Questions.SP3);
            MainWindow.sendQuestion(Questions.SP4);

            MainWindow.sendQuestion(Questions.IOT);
        }

        public void displayStatus(string channel, string low, string high, int SPx, NumberFormatInfo NumberFormat)
        {
            double _low, _high;

            if (!double.TryParse(low, NumberStyles.Any, NumberFormat,  out _low) || !double.TryParse(high, out _high))
                return;

            SP[SPx - 1].Channel = channel;
            SP[SPx - 1].Lower = _low;
            SP[SPx - 1].Upper = _high;
        }

        public void displayIOT(string TestStatus, string RelaisStatus)
        {
            int Status;
            if (!int.TryParse(RelaisStatus, System.Globalization.NumberStyles.HexNumber, CultureInfo.CurrentCulture, out Status))
            {
                return;
            }
            if (TestStatus == "0")
            {
                Override = false;
            }
            else
            {
                Override = true;
            }

            for (int i = 0; i < RVal.Length; i++)
            {
                RelTable[RVal[i]].Status = FlagsHelper.IsSet(Status, (int)RVal[i]);
            }
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


        private void InputOverride_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void InputOverride_Click(object sender, EventArgs e)
        {
            if (!Override)
            {
                DialogResult res = MessageBox.Show("Attention! Do you realy want to override the relais status? This can cause damage to connected systems!", "Override Relais status", MessageBoxButtons.YesNoCancel);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    Override = true;
                }
                else
                {
                    Override = false;
                }
            }
            else
            {
                Override = false;
            }
        }

        private void InputSetStatus_Click(object sender, EventArgs e)
        {
            int tmp = 00;
            for (int i = 0; i < RVal.Length; i++)
            {
                if (RelTable[RVal[i]].Status == true)
                    FlagsHelper.Set(ref tmp, (int)RVal[i]);
            }

            string yVal = tmp.ToString("X02");
            string xVal = (Override) ? "1" : "0";
            MainWindow.sendQuestion(Questions.IOT, new string[] { xVal,  yVal});
        }
    }

    public class SpData : INotifyPropertyChanged
    {
        private string _number;
        private string _channel;
        private double _low;
        private double _high;
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
        public double Lower
        {
            get { return this._low; }
            set
            {
                _low = value;
                NotifyPropertyChanged("Low");
            }
        }
        public double Upper
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

    public class RelData : INotifyPropertyChanged
    {
        private int number;
        private relStatus _name;
        private bool? _status = null;
        private Dictionary<relStatus, string> Translate = new Dictionary<relStatus, string>()
        {
            //{relStatus.ALL_OFF, "All Off"},
            //{relStatus.ALL_ON, "All"},
            {relStatus.REL_CH1_ON, "Relais Channel 1"},
            {relStatus.REL_CH2_ON, "Relais Channel 2"},
            {relStatus.REL_ERR_ON, "Relais Error"},
            {relStatus.REL_SP1_ON, "Relais SP 1"},
            {relStatus.REL_SP2_ON, "Relais SP 2"},
            {relStatus.REL_SP3_ON, "Relais SP 3"},
            {relStatus.REL_SP4_ON, "Relais SP 4"},
        };

        private Dictionary<string, relStatus> ReTranslate = new Dictionary<string, relStatus>()
        {
            //{relStatus.ALL_OFF, "All Off"},
            //{relStatus.ALL_ON, "All"},
            {"Relais Channel 1", relStatus.REL_CH1_ON},
            {"Relais Channel 2", relStatus.REL_CH2_ON},
            {"Relais Error", relStatus.REL_ERR_ON},
            {"Relais SP 1", relStatus.REL_SP1_ON},
            {"Relais SP 2", relStatus.REL_SP2_ON},
            {"Relais SP 3", relStatus.REL_SP3_ON},
            {"Relais SP 4", relStatus.REL_SP4_ON},
        };

        public string Name
        {
            get { return Translate[this._name]; }
            set
            {
                _name = ReTranslate[value];
                NotifyPropertyChanged("Name");
            }
        }
        
        public bool? Status
        {
            get { return this._status; }
            set
            {
                _status = value;
                NotifyPropertyChanged("Status");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RelData(int No, relStatus id)
        {
            number = No;
            _name = id;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public enum relStatus
    {
        //ALL_OFF = 0x00,
        REL_SP1_ON = 0x01,
        REL_SP2_ON = 0x02,
        REL_SP3_ON = 0x04,
        REL_SP4_ON = 0x08,
        REL_CH1_ON = 0x10,
        REL_CH2_ON = 0x20,
        REL_ERR_ON = 0x40,
        //ALL_ON = 0x7F,
    }

    public static class FlagsHelper
    {
        public static bool IsSet<T>(T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            return (flagsValue & flagValue) != 0;
        }

        public static void Set<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue | flagValue);
        }

        public static void Unset<T>(ref T flags, T flag) where T : struct
        {
            int flagsValue = (int)(object)flags;
            int flagValue = (int)(object)flag;

            flags = (T)(object)(flagsValue & (~flagValue));
        }
    }
}
