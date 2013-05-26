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
        private int debugLevel = 1;
        private bool logging = false;
        private string logFile;
        private TextWriter logFileWriter;
        private bool AnswerRecieved = false;

        private double _cP;
        private double currentPressure
        {
            get
            {
                return _cP;
            }
            set
            {
                _cP = value;
            }
        }

        private Questions lastQuestion = Questions.NULL;
        private Queue<KeyValuePair<Questions, string[]>> OutputBuffer = new Queue<KeyValuePair<Questions, string[]>>();

        public bool _continue = true;
        private bool QuestionSent = false;
        private bool QuestionACK = false;
        private bool ENQSent = false;
        private bool AwaitingAnswer = false;

        private SerialPort Port;
        //private Thread ReadThreat = new Thread(Read
        

        public MainForm()
        {
            InitializeComponent();

            BoxComPorts.DataSource = SerialPort.GetPortNames();
            BoxBaud.DataSource = new int[] { 9600, 19200, 38400 };

            try
            {
                BoxBaud.Text = Properties.Settings.Default.BaudRate;
                BoxComPorts.Text= Properties.Settings.Default.ComPort;
            }
            catch { }
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
                }
            }
        }

        private void parseAnswer(string Answer, Questions Question)
        {
            string[] res = Answer.Split(',');
            AnswerRecieved = true;
            switch (Question)
            {
                case Questions.PRX:
                    txtMes1On.Visible = (res[0] == "0") ? true : false;
                    //txtMes2On.Visible = (res[0] == "0") ? true : false;
                    string[] P = res[1].Split('E');
                    TXTcurPressure.Text = P[0];
                    TXTcurPressureExp.Text = P[1];
                        break;
                
                case Questions.SPS:
                    TXTmes1SP1.Visible = (res[0] == "1") ? true : false;
                    TXTmes1SP2.Visible = (res[1] == "1") ? true : false;
                        break;

                case Questions.DGS:
                    TXTmes1Degas.Visible= (res[0] == "1") ? true : false;
                        break;

                case Questions.UNI:
                    TXTUnit.Text = (res[0] == "0") ? "mbar" : (res[0] == "1") ? "Torr" : "Pa";
                        break;

                case Questions.OFC:
                    TXTmes1Offset.Visible = (res[0] == "0") ? false : true;
                        break;

                case Questions.CAL:
                    TXTmes1Calib.Visible = (res[0] != "1.000") ? true : false;
                        break;

                case Questions.COM:
                            txtMes1On.Visible = (res[0] == "0") ? true : false;
                            string[] Pc = res[1].Split('E');
                            TXTcurPressure.Text = Pc[0];
                            TXTcurPressureExp.Text = Pc[1];
                            if (logging && logFileWriter != null)
                            {
                                logFileWriter.WriteLine(DateTime.Now.ToString() + "\t" + res[1] + "\t" + res[3]);
                                logFileWriter.Flush();
                            }
                        break;

                case Questions.ERR:
                    TXTError.Visible = (res[0] == "0000") ? false : true;
                    toolTip1.SetToolTip(TXTError,(res[0]=="1000") ? "ERROR" : (res[0]=="0100") ? "Hardware nicht installiert" : (res[0]=="0010") ? "Unerlaubter Parameter" : (res[0]=="0001") ? "Falsche Syntax" : "");
                        break;

                default:
                        debugMSG("Answer not implemented: " + Question.ToString(), 1);
                        break;
            }
        }

        private void sendQuestion(Questions Question, string[] Parameter = null)
        {
            if (Port == null) return;
            if (!Port.IsOpen) return;
            OutputBuffer.Enqueue(new KeyValuePair<Questions, string[]>(Question, Parameter));
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ComPort = BoxComPorts.Text;
            Properties.Settings.Default.BaudRate = BoxBaud.Text;
            Properties.Settings.Default.Save();
            disconnect();
        }

        void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                getAnswer(Port.ReadLine());
            }
            catch (TimeoutException)
            {
                onTimeout();
            }
        }

        private void connect()
        {
            try
            {
                Port = new SerialPort();
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
                getStatusTimer.Start();
                getStatus.RunWorkerAsync();
                AskerTimer.Start();
            }
            catch
            {
                checkBox1.Checked = false;
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
                Port = null;
                ButConnect.Text = "Connect";
                ButConnect.BackColor = Color.Transparent;
                debugMSG("Disconnected", 1);
                getStatusTimer.Stop();
                AskerTimer.Stop();
            }
            catch { }
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
                connect();
            }
            else
            {
                disconnect();
            }
        }

        void Port_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            debugMSG(e.EventType.ToString(), 1);
        }

        private void getStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            if (logging) return;
            if (OutputBuffer.Count > 0) return;
            sendQuestion(Questions.SPS);
            sendQuestion(Questions.SPS);
            sendQuestion(Questions.DGS);
            sendQuestion(Questions.UNI);
            sendQuestion(Questions.OFC);
            sendQuestion(Questions.CAL);
            //sendQuestion(Questions.PRX);
            sendQuestion(Questions.ERR);
            sendQuestion(Questions.COM, new string[] { "0" });
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
            else if (!Port.IsOpen)
            {
                connect();
            }
        }

        private void debugMSG(string Message, int Level)
        {
            if (Level <= debugLevel)
            {
                textBox1.Text += Message + Environment.NewLine;

                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
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
            logFile = saveFileDialog1.FileName;
            ButLog.Image = Properties.Resources.OK;
            checkBox1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (logFile == string.Empty)
                {
                    checkBox1.Checked = false;
                    return;
                }
                OutputBuffer.Clear();
                sendQuestion(Questions.COM, new string[] { "1" });
                logging = true;
                logFileWriter = new StreamWriter(logFile);
                logFileWriter.WriteLine("Time\tPressure");
                logFileWriter.WriteLine("None\t" + TXTUnit.Text);
                debugMSG("Logfile created, logging...", 1);
                checkBox1.Text = "Logging";
                checkBox1.BackColor = Color.Green;
                checkBox1.Image = Properties.Resources.Gear;
            }
            else if (logging)
            {
                checkBox1.Image = Properties.Resources.RecordHS;
                checkBox1.Text = "Log...";
                checkBox1.BackColor = Color.Transparent;
                logging = false;
                logFileWriter.Close();
                logFileWriter.Dispose();
            }
        }
    }
}
