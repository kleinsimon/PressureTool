using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
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
        }

        void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                getAnswer(Port.ReadLine());
            }
            catch (TimeoutException) { }
        }

        private void getAnswer(string Answer)
        {
            //setText(Answer);
            Answer = Answer.Replace(CR, string.Empty);
            Answer = Answer.Replace(LF, string.Empty);

            if (Answer.Contains(NAK))
            {
                lastQuestion = Questions.NULL;
                QuestionSent = QuestionACK = ENQSent = false;
                AwaitingAnswer = false;
                //TODO ERROR
                return;
            }
            else if (QuestionSent && !QuestionACK && Answer.Contains(ACK))
            {
                Port.WriteLine(ENQ);
                QuestionACK=true;
                ENQSent = true;
                return;
            }
            else if (QuestionSent && QuestionACK && ENQSent)
            {
                if (lastQuestion != Questions.COM) AwaitingAnswer = false;
                if (lastQuestion != Questions.NULL)
                {
                    if (Regex.IsMatch(Answer,serialInterface.Answers[lastQuestion].RegexAnswer))
                    {
                        parseAnswer(Answer, lastQuestion);
                        if (lastQuestion != Questions.COM)
                        {
                            QuestionSent = QuestionACK = ENQSent = false;
                            lastQuestion = Questions.NULL;
                        }
                    }
                    else
                    {
                        //TODO Unexpected Answer
                    }
                }
            }
        }

        private void parseAnswer(string Answer, Questions Question)
        {
            string[] res = Answer.Split(',');
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
            }
        }

        private void setText(string text)
        {
            TXTcurPressure.Text += text;
        }

        private void sendQuestion(Questions Question, string[] Parameter = null)
        {
            if (!Port.IsOpen) return;
            OutputBuffer.Enqueue(new KeyValuePair<Questions, string[]>(Question, Parameter));
        }

        private void getPresure_Click(object sender, EventArgs e)
        {
            sendQuestion(Questions.PRX);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Port.Close();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendQuestion(Questions.COM,new string[] {"1"});
        }


        private void ButConnect_CheckedChanged(object sender, EventArgs e)
        {
            if (ButConnect.Checked)
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
                ButConnect.Text = "Connected";
            }
            else
            {
                OutputBuffer.Clear();
                Port.Close();
                Port.Dispose();
                Port = null;
                ButConnect.Text = "Connect";
            }
        }

        private void getStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            sendQuestion(Questions.SPS);
            sendQuestion(Questions.DGS);
            sendQuestion(Questions.UNI);
            sendQuestion(Questions.OFC);
            sendQuestion(Questions.CAL);
            sendQuestion(Questions.PRX);
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
                        // ERROR
                        return;
                    }
                }

                try
                {
                    Port.WriteLine(Question.ToString() + ((Parameter != null) ? "," + String.Join(",", Parameter) : ""));
                    QuestionSent = true;
                    lastQuestion = Question;
                    if (Question == Questions.COM) QuestionSent = QuestionACK = ENQSent = true;
                    AwaitingAnswer = true;
                }
                catch (TimeoutException) { }
            }
        }
    }
}
