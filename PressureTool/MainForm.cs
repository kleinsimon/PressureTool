using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace PressureTool
{
    public partial class MainForm : Form
    {
        public const string ACK = "\u0006";
        public const string ENQ = "\u0005";
        public const string NAK = "\u0015";
        public const string CR = "\u000d";
        public const string LF = "\u000a";

        public string LogSpeed = "1";
        public string DisplaySpeed = "1";
        public bool connectOnStart = false;

        private int oldHeight;
        public int debugLevel = 1;

        private DataLogger Log;
        private bool _logging = false;
        private bool logging
        {
            get
            {
                return _logging;
            }
            set
            {
                _logging = value;
                if (value)
                {
                    ButLogStart.Text = "Logging";
                    ButLogStart.BackColor = Color.Green;
                    ButLogStart.Image = Properties.Resources.Gear;
                    ButLogStart.Checked = true;
                }
                else
                {
                    ButLogStart.Image = Properties.Resources.RecordHS;
                    ButLogStart.Text = "Log...";
                    ButLogStart.BackColor = Color.Transparent;
                    ButLogStart.Checked = false;
                }
            }
        }

        private bool AnswerRecieved = false;

        private Questions lastQuestion = Questions.NULL;
        private Queue<KeyValuePair<Questions, string[]>> OutputBuffer = new Queue<KeyValuePair<Questions, string[]>>();
        public System.Globalization.NumberFormatInfo NumberFormat = null;

        public bool _continue = true;
        private bool QuestionSent = false;
        private bool QuestionACK = false;
        private bool ENQSent = false;
        private bool AwaitingAnswer = false;
        private PressureChart ChartWindow;
        private relais RelaisWindow;
        private LogOptions LogOptionWindow;
        private SerialPort Port = new SerialPort();

        public MainForm()
        {
            InitializeComponent();
            Log = new DataLogger(this);
            NumberFormat = (System.Globalization.NumberFormatInfo) System.Globalization.CultureInfo.InstalledUICulture.NumberFormat.Clone();
            NumberFormat.NumberDecimalSeparator = ".";
            NumberFormat.NumberGroupSeparator = "";

            //ChartWindow.Show();
            BoxComPorts.DataSource = SerialPort.GetPortNames();
            BoxBaud.DataSource = new int[] { 9600, 19200, 38400 };

            try
            {
                BoxBaud.Text = Properties.Settings.Default.BaudRate;
                BoxComPorts.Text= Properties.Settings.Default.ComPort;
                DisplaySpeed = Properties.Settings.Default.DisplaySpeed;
                LogSpeed = Properties.Settings.Default.LogSpeed;
                connectOnStart = Properties.Settings.Default.ConnectOnStart;
            }
            catch { }

            if (connectOnStart)
                ConnectionWatchDog.Start();
        }

        private void getAnswer(string Answer)
        {
            Answer = Answer.Replace(CR, string.Empty);
            Answer = Answer.Replace(LF, string.Empty);

            if (Answer.Contains(NAK))
            {
                debugMSG("NAK recieved", 3);
                lastQuestion = Questions.NULL;
                QuestionSent = QuestionACK = ENQSent = false;
                AwaitingAnswer = false;
                if (!Asker.IsBusy) Asker.RunWorkerAsync();
                return;
            }
            else if (Answer.Contains(ACK))
            {
                debugMSG("ACK Recieved", 3);
                if (lastQuestion != Questions.COM)
                {
                    Port.WriteLine(ENQ);
                    debugMSG("ENQ Sent", 3);
                    QuestionACK = true;
                    ENQSent = true;
                    return;
                }
            }
            else if (QuestionSent && QuestionACK && ENQSent)
            {
                 AwaitingAnswer = false;
                if (lastQuestion != Questions.NULL)
                {
                    if (Regex.IsMatch(Answer, serialInterface.Answers[lastQuestion].RegexAnswer))
                    {
                        parseAnswer(Answer, lastQuestion);
                        debugMSG("REC:\t" + Answer, 3);
                        if (lastQuestion != Questions.COM)
                        {
                            QuestionSent = QuestionACK = ENQSent = false;
                            lastQuestion = Questions.NULL;
                        }
                    }
                    else
                    {
                        debugMSG("Unexpected Answer: " + Answer, 1);
                    }
                }
                else
                {
                    debugMSG("Answer to no Question: " + Answer, 1);
                    if (!Asker.IsBusy) Asker.RunWorkerAsync();
                }
            }
        }

        private void parseAnswer(string Answer, Questions Question)
        {
            string[] res = Answer.Split(',');
            AnswerRecieved = true;
            if (!Asker.IsBusy) Asker.RunWorkerAsync();
            switch (Question)
            {
                case Questions.PRX:
                    txtMes1On.ForeColor = (res[0] == "0") ? Color.Yellow : Color.Gray;
                    //txtMes2On.Visible = (res[0] == "0") ? true : false;
                    string[] P = res[1].Split('E');
                    TXTcurPressure.Text = P[0];
                    TXTcurPressureExp.Text = P[1];
                        break;
                
                case Questions.SPS:
                        TXTmes1SP1.ForeColor = (res[0] == "1") ? Color.Yellow : Color.Gray;
                        TXTmes1SP2.ForeColor = (res[1] == "1") ? Color.Yellow : Color.Gray;
                        break;

                case Questions.DGS:
                        TXTmes1Degas.ForeColor = (res[0] == "1") ? Color.Yellow : Color.Gray;
                        break;

                case Questions.UNI:
                    TXTUnit.Text = (res[0] == "0") ? "mbar" : (res[0] == "1") ? "Torr" : "Pa";
                        break;

                case Questions.OFC:
                        TXTmes1Offset.ForeColor = (res[0] == "0") ? Color.Gray : Color.Yellow;
                        break;

                case Questions.CAL:
                        TXTmes1Calib.ForeColor = (res[0] != "1.000") ? Color.Yellow : Color.Gray;
                        break;

                case Questions.COM:
                            txtMes1On.Visible = (res[0] == "0") ? true : false;
                            string[] Pc = res[1].Split('E');
                            TXTcurPressure.Text = Pc[0];
                            TXTcurPressureExp.Text = Pc[1];
                            //Log.addLine(DateTime.Now.ToString() + "\t" + res[1] + "\t" + res[3]);
                            Log.addValues(DateTime.Now, double.Parse(res[1].Replace('.', ',')), double.Parse(res[3].Replace('.', ',')));
                            if (ChartWindow != null)
                            {
                                try
                                {
                                    BeginInvoke(new Action(() =>
                                    {
                                        ChartWindow.AddToChart(DateTime.Now, double.Parse(res[1].Replace('.', ',')));
                                    }));
                                }
                                catch { }
                            }
                        break;

                case Questions.ERR:
                    TXTError.Visible = (res[0] == "0000") ? false : true;
                    toolTip1.SetToolTip(TXTError,(res[0]=="1000") ? "ERROR" : (res[0]=="0100") ? "Hardware nicht installiert" : (res[0]=="0010") ? "Unerlaubter Parameter" : (res[0]=="0001") ? "Falsche Syntax" : "");
                        break;

                case Questions.SP1:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 1);
                        break;

                case Questions.SP2:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 2);
                        break;

                case Questions.SP3:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 3);
                        break;

                case Questions.SP4:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 4);
                        break;

                default:
                        debugMSG("Answer not implemented: " + Question.ToString(), 1);
                        break;
            }
        }

        public void sendQuestion(Questions Question, string[] Parameter = null)
        {
            if (!Port.IsOpen) return;
            OutputBuffer.Enqueue(new KeyValuePair<Questions, string[]>(Question, Parameter));

            if (!Asker.IsBusy) Asker.RunWorkerAsync();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ComPort = BoxComPorts.Text;
            Properties.Settings.Default.BaudRate = BoxBaud.Text;
            Properties.Settings.Default.DisplaySpeed = DisplaySpeed;
            Properties.Settings.Default.LogSpeed = LogSpeed;
            Properties.Settings.Default.ConnectOnStart = connectOnStart;
            Properties.Settings.Default.Save();
            sendQuestion(Questions.PRX);
            disconnect();
        }

        void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            debugMSG(e.EventType.ToString(), 3);
            try
            {
                getAnswer(Port.ReadLine());
            }
            catch (TimeoutException)
            {
                onTimeout();
            }
        }

        private void startChart()
        {
            if (ChartWindow == null)
                ChartWindow = new PressureChart(this);
            ChartWindow.Show();
            ChartWindow.Pause = false;
            
        }

        private void connect()
        {
            if (Port.IsOpen) return;
            try
            {
                //Port = new SerialPort();
                Port.PortName = BoxComPorts.Text;
                Port.BaudRate = int.Parse(BoxBaud.Text);
                Port.Parity = Parity.None;
                Port.StopBits = StopBits.One;
                Port.DataBits = 8;
                Port.Handshake = Handshake.None;
                Port.Open();
                Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                Port.ErrorReceived += new SerialErrorReceivedEventHandler(Port_ErrorReceived);
                Port.ReadTimeout = 2000;
                Port.WriteTimeout = 2000;
                ButConnect.Text = "Connected";
                ButConnect.BackColor = Color.Green;
                debugMSG("Connected to " + Port.PortName + " with baud " + Port.BaudRate.ToString(), 1);
                AskerTimer.Start();
                getStatusTimer.Start();
                getStatus.RunWorkerAsync();
                ConnectionWatchDog.Start();
                //togglePanel();
            }
            catch
            {
                ButLogStart.Checked = false;
                debugMSG("Port could not be opened", 1);
                ButConnect.Checked = false;
            }
        }

        private void disconnect()
        {
            try
            {
                OutputBuffer.Clear();
                Port.Close();
                Port.Dispose();
                ButConnect.Text = "Connect";
                ButConnect.BackColor = Color.Transparent;
                ButConnect.Checked = false;
                debugMSG("Disconnected", 1);
                getStatusTimer.Stop();
                AskerTimer.Stop();
                ConnectionWatchDog.Stop();
            }
            catch { debugMSG("Port could not be closed", 1); }
        }

        private void onTimeout()
        {
            debugMSG("Timeout recieving Answer", 1);
            disconnect();
        }

        private void ButConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (ButConnect.Checked)
            {
                ConnectionWatchDog.Start();
            }
            else
            {
                ConnectionWatchDog.Stop();
                disconnect();
            }
        }

        public void onChartClosed()
        {
            BUTChart.Checked = false;
        }

        void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            debugMSG(e.EventType.ToString(), 1);
        }

        private void getStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            debugMSG("Getting Status", 3);
            if (logging) return;
            if (OutputBuffer.Count > 0) return;
            sendQuestion(Questions.SPS);
            sendQuestion(Questions.SPS);
            sendQuestion(Questions.DGS);
            sendQuestion(Questions.UNI);
            sendQuestion(Questions.OFC);
            sendQuestion(Questions.CAL);
            sendQuestion(Questions.ERR);
            sendQuestion(Questions.COM, new string[] { DisplaySpeed });
        }

        private void Asker_DoWork(object sender, DoWorkEventArgs e)
        {           
            if (OutputBuffer.Count > 0 && !AwaitingAnswer && Port.IsOpen)
            {
                KeyValuePair<Questions, string[]> CurCommand = OutputBuffer.Dequeue();
                Questions Question = CurCommand.Key;
                string[] Parameter = CurCommand.Value;

                QuestionSent = QuestionACK = ENQSent = false;
                QuestionOptions Opts = serialInterface.Answers[Question];
                if (Opts.numParameter > 0 && Parameter != null)
                {
                    if (Parameter.Length != Opts.numParameter)
                    {
                        debugMSG("Parameter count misfit", 1);
                        return;
                    }
                }

                try
                {
                    Port.WriteLine(Question.ToString() + ((Parameter != null) ? "," + String.Join(",", Parameter) : ""));
                    debugMSG("Send: " + Question.ToString() + ((Parameter != null) ? "," + String.Join(",", Parameter) : ""), 2);
                    QuestionSent = true;
                    lastQuestion = Question;
                    if (Question == Questions.COM) QuestionSent = QuestionACK = ENQSent = true;
                    AwaitingAnswer = true;
                }
                catch (TimeoutException) {
                    onTimeout();
                }
            }
        }

        private void debugMSG(string Message, int Level)
        {
            if (Level <= debugLevel)
            {
                BeginInvoke(new Action(() =>
                {
                    textBox1.Text += Message + Environment.NewLine;

                    textBox1.SelectionStart = textBox1.Text.Length;
                    textBox1.ScrollToCaret();
                }));
            }
        }

        private void getStatusTimer_Tick(object sender, EventArgs e)
        {
            if (logging) return;
            if (!AnswerRecieved) onTimeout();
            if (!getStatus.IsBusy) getStatus.RunWorkerAsync();
        }

        private void AskerTimer_Tick(object sender, EventArgs e)
        {
            //if (logging) return;
            if (!Asker.IsBusy) Asker.RunWorkerAsync();
        }

        private void ButLog_Click(object sender, EventArgs e)
        {
            if (logging) return;
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            debugMSG("Log File set to " + saveFileDialog1.FileName, 1);
            Log.logFile = saveFileDialog1.FileName;
            ButLog.Image = Properties.Resources.OK;
            ButLogStart.Enabled = true;
        }

        private void ButLogStart_Clicked(object sender, EventArgs e)
        {
            if (!Port.IsOpen) return;
            if (!logging)
            {
                LogOptionWindow = new LogOptions(this);
                LogOptionWindow.ShowDialog();
            }
            else
            {
                onStopLogging("Logging stopped by user");
            }
        }

        public void startLogging(TimeSpan duration, double minP1, double maxP1, double minP2, double maxP2)
        {
            if (Log.StartLogging(TXTUnit.Text, duration, minP1, maxP1, minP2, maxP2))
            {
                debugMSG("Logfile created, logging...", 1);
                logging = true;
            }
            else
            {
                debugMSG("Logfile " + Log.logFile + " could not be created", 1);
                logging = false;
                return;
            }

            startChart();
            ChartWindow.clear();
            OutputBuffer.Clear();
            sendQuestion(Questions.COM, new string[] { LogSpeed });
        }

        private void stopLogging()
        {
            Log.stopLogging();
            logging = false;
        }

        public void onStopLogging(string Reason = "")
        {
            stopLogging();
            debugMSG("Logging stopped. Reaseon: " + Reason, 0);
            try
            {
                ChartWindow.Pause = true;
            }
            catch { }
        }

        private void PanelHideButton_Click(object sender, EventArgs e)
        {
            togglePanel();
        }

        private void togglePanel()
        {
            this.SuspendLayout();
            SuspendUpdate.Suspend(this);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            if (!splitContainer1.Panel2Collapsed)
            {
                int height = splitContainer1.Panel1.Height;
                oldHeight = splitContainer1.Height;
                splitContainer1.Height = height;
                splitContainer1.Panel2Collapsed = true;
                PanelHideButton.Image = Properties.Resources.down;
            }
            else
            {
                splitContainer1.Panel2Collapsed = false;
                splitContainer1.Height = oldHeight;
                PanelHideButton.Image = Properties.Resources.up;
            }
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            SuspendUpdate.Resume(this);
            this.ResumeLayout();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (splitContainer1.Panel2Collapsed)
            {
                PanelHideButton.Image = Properties.Resources.down;
            }
            else
            {
                PanelHideButton.Image = Properties.Resources.up;
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            Settings bla = new Settings(this);
            bla.Show();
        }

        private void ConnectionWatchDog_Tick(object sender, EventArgs e)
        {
            if (!Port.IsOpen)
            {
                connect();
            }
        }

        private void BUTChart_CheckedChanged(object sender, EventArgs e)
        {
            if (BUTChart.Checked)
            {
                startChart();
                BUTChart.BackColor = Color.Yellow;
            }
            else
            {
                ChartWindow.Close();
                BUTChart.BackColor = Color.Transparent;
            }
        }

        private void TXTmes1SP1_Click(object sender, EventArgs e)
        {
            RelaisWindow = new relais(this);
            RelaisWindow.Show();
        }

    }

    public static class SuspendUpdate
    {
        private const int WM_SETREDRAW = 0x000B;

        public static void Suspend(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        public static void Resume(Control control)
        {
            // Create a C "true" boolean as an IntPtr
            IntPtr wparam = new IntPtr(1);
            Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgResumeUpdate);

            control.Invalidate();
        }
    }
}
