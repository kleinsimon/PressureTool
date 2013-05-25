using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
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
        static private AnswerFormat ExpectedAnswer = null;

        static public bool _continue = true;
        static private bool QuestionSent = false;
        static private bool QuestionACK = false;
        static private bool ENQSent = false;

        static private SerialPort Port = new SerialPort();
        private Thread ReadThreat = new Thread(Read);

        public MainForm()
        {
            InitializeComponent();
        }

        private static void parseAnswer(string Answer)
        {
            Answer = Answer.Replace(CR, string.Empty);
            Answer = Answer.Replace(LF, string.Empty);

            if (Answer.Contains(NAK))
            {
                ExpectedAnswer = null;
                QuestionSent = QuestionACK = ENQSent = false;
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
                if (!(ExpectedAnswer == null))
                {
                    if (ExpectedAnswer.Matches(Answer))
                    {
                        QuestionSent = QuestionACK = ENQSent = false;
                        //TODO Answer
                    }
                    else
                    {
                        //TODO Unexpected Answer
                    }
                }
            }
        }

        public static void Read()
        {
            while (_continue)
            {
                try
                {
                    string message = Port.ReadLine();
                    parseAnswer(message);
                }
                catch (TimeoutException) { }
            }
        }

        private void sendQuestion(Questions Question, string[] Parameter = null)
        {
            QuestionOptions Opts = serialInterface.Answers[Question];
            if (Opts.numParameter > 0)
            {
                if (Parameter == null)
                {
                    // ERROR
                    return;
                }
                else if ( Parameter.Length != Opts.numParameter)
                {
                    // ERROR
                    return;
                }
            }

            ExpectedAnswer = new AnswerFormat(Opts.RegexAnswer);
            try
            {
                Port.WriteLine(Question.ToString());
                QuestionSent = true;
            }
            catch (TimeoutException) { }
        }

        private void getPresure_Click(object sender, EventArgs e)
        {
            sendQuestion(Questions.PRX);
        }
    }
}
