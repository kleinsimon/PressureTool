using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace PressureTool
{
    public partial class MainForm : Form
    {
        //Protocol Specialchars
        public const string ACK = "\u0006";
        public const string ENQ = "\u0005";
        public const string NAK = "\u0015";
        public const string CR = "\u000d";
        public const string LF = "\u000a";

        //Settings Variables
        public string DisplaySpeed = "1";
        public int debugLevel = 1;
        public string refreshSpeed
        {
            get
            {
                return (getStatusTimer.Interval / 1000).ToString();
            }
            set
            {
                double tmp;
                if (double.TryParse(value, out tmp))
                {
                    getStatusTimer.Interval = (int) (tmp * 1000);
                }
            }
        }
        public bool connectOnStart = false;

        //State Variables
        private int oldHeight;
        private DataLogger Log;
        //logging status
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

                    txtMes1On.Visible = false;
                    TXTError.Visible = false;
                    TXTmes1Calib.Visible = false;
                    TXTmes1Degas.Visible = false;
                    TXTmes1Offset.Visible = false;
                    TXTmes1SP1.Visible = false;
                    TXTmes1SP2.Visible = false;
                }
                else
                {
                    ButLogStart.Image = Properties.Resources.RecordHS;
                    ButLogStart.Text = "Log...";
                    ButLogStart.BackColor = Color.Transparent;
                    ButLogStart.Checked = false;

                    txtMes1On.Visible = true;
                    TXTError.Visible = true;
                    TXTmes1Calib.Visible = true;
                    TXTmes1Degas.Visible = true;
                    TXTmes1Offset.Visible = true;
                    TXTmes1SP1.Visible = true;
                    TXTmes1SP2.Visible = true;
                }
            }
        }
        // connection status, keeps UI up to date
        private bool _connected = false;
        private bool connected
        {
            get
            {
                return _connected;
            }
            set
            {
                _connected = value;
                if (value)
                {
                    ButConnect.Text = "Connected";
                    ButConnect.BackColor = Color.Green;
                    ButConnect.Checked = true;
                }
                else
                {
                    ButConnect.Text = "Connect";
                    ButConnect.BackColor = Color.Transparent;
                    ButConnect.Checked = false;
                }
            }
        }
        private bool AnswerRecieved = false;
        //stores last question to proper parse the next answer
        private Questions lastQuestion = Questions.NULL;
        //keeps outgoing messages
        private Queue<KeyValuePair<Questions, string[]>> OutputBuffer = new Queue<KeyValuePair<Questions, string[]>>();
        private SerialPort Port = new SerialPort();
        public NumberFormatInfo NumberFormat = null;
        private bool QuestionSent = false;
        private bool QuestionACK = false;
        private bool ENQSent = false;
        private bool AwaitingAnswer = false;

        //Other Forms
        private PressureChart ChartWindow;
        private relais RelaisWindow;
        private LogOptions LogOptionWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Log = new DataLogger(this);
            NumberFormat = (NumberFormatInfo) CultureInfo.InstalledUICulture.NumberFormat.Clone();
            NumberFormat.NumberDecimalSeparator = ".";
            NumberFormat.NumberGroupSeparator = "";



            try
            {
                DisplaySpeed = Properties.Settings.Default.DisplaySpeed;
                connectOnStart = Properties.Settings.Default.ConnectOnStart;
                refreshSpeed = Properties.Settings.Default.RefreshSpeed;
            }
            catch { }

            if (connectOnStart)
                ConnectionWatchDog.Start();
        }

        /// <summary>
        /// Gets the answer.
        /// </summary>
        /// <param name="Answer">The answer.</param>
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

        /// <summary>
        /// Parses the answer.
        /// </summary>
        /// <param name="Answer">The answer.</param>
        /// <param name="Question">The question.</param>
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
                        //Log.addValues(DateTime.Now, double.Parse(res[1].Replace('.', ',')), double.Parse(res[3].Replace('.', ',')));
                        Log.addValues(DateTime.Now, double.Parse(res[1],NumberFormat), double.Parse(res[3],NumberFormat));
                        if (ChartWindow != null)
                        {
                            try
                            {
                                BeginInvoke(new Action(() =>
                                {
                                    ChartWindow.AddToChart(DateTime.Now, double.Parse(res[1], NumberFormat));
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
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 1, NumberFormat);
                    break;

                case Questions.SP2:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 2, NumberFormat);
                    break;

                case Questions.SP3:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 3, NumberFormat);
                    break;

                case Questions.SP4:
                        if (RelaisWindow == null) return;
                        if (!RelaisWindow.Visible) return;
                        RelaisWindow.displayStatus(res[0], res[1], res[2], 4, NumberFormat);
                    break;

                case Questions.IOT:
                    if (RelaisWindow == null) return;
                    if (!RelaisWindow.Visible) return;
                    RelaisWindow.displayIOT(res[0], res[1]);
                    break;

                default:
                    debugMSG("Answer not implemented: " + Question.ToString(), 1);
                        break;
            }
        }

        /// <summary>
        /// Sends the question.
        /// </summary>
        /// <param name="Question">The question.</param>
        /// <param name="Parameter">The parameters as a String-Array</param>
        public void sendQuestion(Questions Question, string[] Parameter = null)
        {
            debugMSG("Sendig Question: " + Question.ToString() + "," + ((Parameter != null) ? string.Join(",", Parameter) : ""), 3);
            if (!Port.IsOpen) return;
            OutputBuffer.Enqueue(new KeyValuePair<Questions, string[]>(Question, Parameter));

            if (!Asker.IsBusy) Asker.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.DisplaySpeed = DisplaySpeed;
            Properties.Settings.Default.ConnectOnStart = connectOnStart;
            Properties.Settings.Default.RefreshSpeed = refreshSpeed;
            Properties.Settings.Default.Save();
            sendQuestion(Questions.PRX);
            disconnect();
        }

        /// <summary>
        /// Handles the DataReceived event of the Serial-Port.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SerialDataReceivedEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Starts the chart.
        /// </summary>
        private void startChart()
        {
            if (ChartWindow == null)
                ChartWindow = new PressureChart(this);
            ChartWindow.Show();
            ChartWindow.Pause = false;
            
        }

        /// <summary>
        /// Connects this instance to the Serial Port Set in Port and starts the Connection Watchdog and the AskerTimer and getStatus Timer.
        /// </summary>
        private void connect()
        {
            if (Port.IsOpen) return;
            try
            {
                //Port = new SerialPort();
                Port.PortName = Properties.Settings.Default.ComPort;
                Port.BaudRate = int.Parse(Properties.Settings.Default.BaudRate);
                Port.Parity = Parity.None;
                Port.StopBits = StopBits.One;
                Port.DataBits = 8;
                Port.Handshake = Handshake.None;
                Port.Open();
                Port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                Port.ErrorReceived += new SerialErrorReceivedEventHandler(Port_ErrorReceived);
                Port.ReadTimeout = 2000;
                Port.WriteTimeout = 2000;
                debugMSG("Connected to " + Port.PortName + " with baud " + Port.BaudRate.ToString(), 1);
                AskerTimer.Start();
                getStatusTimer.Start();
                getStatus.RunWorkerAsync();
                ConnectionWatchDog.Start();
                connected = true;
                //togglePanel();
            }
            catch
            {
                debugMSG("Port could not be opened", 1);
                connected = false;
            }
        }

        /// <summary>
        /// Disconnects the Serial Port and stops the Connection Watchdog.
        /// </summary>
        private void disconnect()
        {
            try
            {
                OutputBuffer.Clear();
                Port.Close();
                Port.Dispose();
                debugMSG("Disconnected", 1);
                getStatusTimer.Stop();
                AskerTimer.Stop();
                ConnectionWatchDog.Stop();
                connected = false;
            }
            catch { debugMSG("Port could not be closed", 1); }
        }

        /// <summary>
        /// Callback for Timeout
        /// </summary>
        private void onTimeout()
        {
            debugMSG("Timeout recieving Answer", 1);
            disconnect();
        }

        /// <summary>
        /// Handles the CheckedChanged event of the ButConnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButConnect_Clicked(object sender, EventArgs e)
        {
            if (!ConnectionWatchDog.Enabled)
            {
                ConnectionWatchDog.Start();
            }
            else
            {
                ConnectionWatchDog.Stop();
                disconnect();
            }
        }

        /// <summary>
        /// Run when the chart is closed.
        /// </summary>
        public void onChartClosed()
        {
            BUTChart.Checked = false;
        }

        /// <summary>
        /// Handles the ErrorReceived event of the Serial Port.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SerialErrorReceivedEventArgs"/> instance containing the event data.</param>
        void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            debugMSG(e.EventType.ToString(), 1);
        }

        /// <summary>
        /// Updates the Device Status by sending questions
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the next Question in the Queue
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DoWorkEventArgs"/> instance containing the event data.</param>
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
                    //debugMSG("Send: " + Question.ToString() + ((Parameter != null) ? "," + String.Join(",", Parameter) : ""), 2);
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

        /// <summary>
        /// Adds a debug Message to the output control
        /// </summary>
        /// <param name="Message">The message.</param>
        /// <param name="Level">Priority of the Message (1=high)</param>
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

        /// <summary>
        /// Handles the Tick event of the getStatusTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void getStatusTimer_Tick(object sender, EventArgs e)
        {
            if (logging) return;
            if (!AnswerRecieved) onTimeout();
            if (!getStatus.IsBusy) getStatus.RunWorkerAsync();
        }

        /// <summary>
        /// Handles the Tick event of the AskerTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AskerTimer_Tick(object sender, EventArgs e)
        {
            if (!Asker.IsBusy) Asker.RunWorkerAsync();
        }

        /// <summary>
        /// Asks for the logging file location
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButLog_Click(object sender, EventArgs e)
        {
            if (logging) return;
            saveFileDialog1.ShowDialog();
        }

        /// <summary>
        /// Sets the File for Logging
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            debugMSG("Log File set to " + saveFileDialog1.FileName, 1);
            Log.logFile = saveFileDialog1.FileName;
            ButLog.Image = Properties.Resources.OK;
            ButLogStart.Enabled = true;
        }

        /// <summary>
        /// Opens the Options Window for logging
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButLogStart_Clicked(object sender, EventArgs e)
        {
            //if (!Port.IsOpen) return;
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

        /// <summary>
        /// Error if Port not Open
        /// </summary>
        private void onErrorPortNotOpen()
        {
            debugMSG("Not connected.... aborting", 1);
        }

        /// <summary>
        /// Starts logging with Options
        /// </summary>
        /// <param name="duration">Duration of logging</param>
        /// <param name="minP1">Lower Pressure Limit on Channel 1</param>
        /// <param name="maxP1">Upper Pressure Limit on Channel 1</param>
        /// <param name="minP2">Lower Pressure Limit on Channel 2</param>
        /// <param name="maxP2">Upper Pressure Limit on Channel 2</param>
        public void startLogging(TimeSpan duration, double minP1, double maxP1, double minP2, double maxP2, string logspeed)
        {
            if (!Port.IsOpen)
            {
                onErrorPortNotOpen();
                return;
            }
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
            sendQuestion(Questions.COM, new string[] { logspeed });
        }

        /// <summary>
        /// Stops the logging.
        /// </summary>
        private void stopLogging()
        {
            Log.stopLogging();
            logging = false;
        }

        /// <summary>
        /// Callback when logging stopped
        /// </summary>
        /// <param name="Reason">The reason.</param>
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

        /// <summary>
        /// Handles the Click event of the PanelHideButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PanelHideButton_Click(object sender, EventArgs e)
        {
            togglePanel();
        }

        /// <summary>
        /// Hides / Shows the lower Part of the Window
        /// </summary>
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

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the ConfigButton control. Shows the Config Form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ConfigButton_Click(object sender, EventArgs e)
        {
            Settings bla = new Settings(this);
            bla.Show();
        }

        /// <summary>
        /// This Method is Called by the Connection Watchdog. It reconnects if the connection is lost
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ConnectionWatchDog_Tick(object sender, EventArgs e)
        {
            if (!Port.IsOpen)
            {
                connect();
            }
        }

        /// <summary>
        /// Shows / Hides the Chart Form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Shows the Relais Form
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TXTmes1SP1_Click(object sender, EventArgs e)
        {
            if (logging) return;
            RelaisWindow = new relais(this);
            RelaisWindow.Show();
        }
    }
}
