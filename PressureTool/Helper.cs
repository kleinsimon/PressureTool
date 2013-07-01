using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace PressureTool
{
    public enum Questions
    {
        ADC,
        BAU,
        COM,
        CAL,
        DCD,
        DGS,
        DIC,
        DIS,
        EEP,
        EPR,
        ERR,
        FIL,
        FSR,
        IOT,
        LOC,
        OFC,
        OFD,
        PNR,
        PR1,
        PR2,
        PRX,
        PUC,
        RAM,
        RES,
        RST,
        SAV,
        SC1,
        SC2,
        SCT,
        SEN,
        SP1,
        SP2,
        SP3,
        SP4,
        SPS,
        TID,
        TKB,
        TLC,
        UNI,
        WDT,
        NULL
    }

    class QuestionOptions
    {
        public int numParameter = 0;
        public string RegexAnswer = "";

        public QuestionOptions(string RegexEpxectedAnswer, int NumberOfParameters)
        {
            numParameter = NumberOfParameters;
            RegexAnswer = RegexEpxectedAnswer;
        }
    }

    static class serialInterface
    { 
        static public Dictionary<Questions, QuestionOptions> Answers = new Dictionary<Questions, QuestionOptions>()
        {
            { Questions.ADC, new QuestionOptions(@"\d\d?\.\d{4},\d\d?\.\d{4},\d\d?\.\d{4},\d\d?\.\d{4}", 0) },
            { Questions.BAU, new QuestionOptions(@"\d", 1) },
            { Questions.COM, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2},\d,[ +-]?\d\.\d{4}E[ +-]?\d{2}", 1) },
            { Questions.CAL, new QuestionOptions(@"\d\.\d{3},\d\.\d{3}", 2) },
            { Questions.DCD, new QuestionOptions(@"\d", 1) },
            { Questions.DGS, new QuestionOptions(@"\d,\d", 2) },
            { Questions.DIC, new QuestionOptions(@"\d", 1) },
            { Questions.DIS, new QuestionOptions(@"\d", 1) },
            { Questions.EEP, new QuestionOptions(@".+", 0) },
            { Questions.EPR, new QuestionOptions(@".+,[0-9a-fA-F]+", 0) },
            { Questions.ERR, new QuestionOptions(@"\d{4}", 0) },
            { Questions.FIL, new QuestionOptions(@"\d,\d", 2) },
            { Questions.FSR, new QuestionOptions(@"\d,\d", 2) },
            { Questions.IOT, new QuestionOptions(@"\d,[0-9a-fA-F]{2}", 2) },
            { Questions.LOC, new QuestionOptions(@"\d", 1) },
            { Questions.OFC, new QuestionOptions(@"\d,\d", 2) },
            { Questions.OFD, new QuestionOptions(@"[ +-]?\d\.\d{4}E[ +-]?\d{2},[ +-]?\d\.\d{4}E[ +-]?\d{2}", 2) },
            { Questions.PNR, new QuestionOptions(@".+", 0) },
            { Questions.PR1, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2}", 0) },
            { Questions.PR2, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2}", 0) },
            { Questions.PRX, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2},\d,[ +-]?\d\.\d{4}E[ +-]?\d{2}", 0) },
            { Questions.PUC, new QuestionOptions(@"\d,\d", 2) },
            { Questions.RAM, new QuestionOptions(@"\d+", 0) },
            { Questions.RES, new QuestionOptions(@"(\d+,?)+", 1) },
            { Questions.RST, new QuestionOptions(@".*", 0) },
            { Questions.SAV, new QuestionOptions(@".*", 1) },
            { Questions.SC1, new QuestionOptions(@"\d,\d,[ +-]?\d\.\d+E[ +-]?\d{2},[ +-]?\d\.\d+dE[ +-]?\d{2}", 4) },
            { Questions.SC2, new QuestionOptions(@"\d,\d,[ +-]?\d\.\d+E[ +-]?\d{2},[ +-]?\d\.\d+dE[ +-]?\d{2}", 4) },
            { Questions.SCT, new QuestionOptions(@"\d", 1) },
            { Questions.SEN, new QuestionOptions(@"\d,\d", 2) },
            { Questions.SP1, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2},[ +-]?\d\.\d{4}E[ +-]?\d{2}", 3) },
            { Questions.SP2, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2},[ +-]?\d\.\d{4}E[ +-]?\d{2}", 3) },
            { Questions.SP3, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2},[ +-]?\d\.\d{4}E[ +-]?\d{2}", 3) },
            { Questions.SP4, new QuestionOptions(@"\d,[ +-]?\d\.\d{4}E[ +-]?\d{2},[ +-]?\d\.\d{4}E[ +-]?\d{2}", 3) },
            { Questions.SPS, new QuestionOptions(@"\d,\d,\d,\d", 0) },
            { Questions.TID, new QuestionOptions(@"\w+,\w+", 0) },
            { Questions.TKB, new QuestionOptions(@"\d{4}", 0) },
            { Questions.TLC, new QuestionOptions(@"\d", 1) },
            { Questions.UNI, new QuestionOptions(@"\d", 1) },
            { Questions.WDT, new QuestionOptions(@"\d", 1) },
        };
    }

    class DataLogger
    {
        public bool logging = false;
        public string logFile;
        private TextWriter logFileWriter;
        private DateTime startTime;
        private DateTime endTime;
        private double minP1;
        private double maxP1;
        private double minP2;
        private double maxP2;
        private MainForm MainWindow;

        public DataLogger(MainForm Window)
        {
            MainWindow = Window;
        }

        public bool StartLogging(string Unit, TimeSpan duration, double minPressure1, double maxPressure1, double minPressure2, double maxPressure2)
        {
            if (logFile == string.Empty) return false;

            try
            {
                logging = true;
                logFileWriter = new StreamWriter(logFile);
                logFileWriter.WriteLine("Time\tPressure1\tPressure2");
                logFileWriter.WriteLine("None\t" + Unit + "\t" + Unit);

                startTime = DateTime.Now;

                if (duration == TimeSpan.Zero)
                    endTime = DateTime.MaxValue;
                else
                    endTime = startTime.Add(duration);
                
                minP1 = minPressure1;
                maxP1 = maxPressure1;
                minP2 = minPressure2;
                maxP2 = maxPressure2;
                return true;
            }
            catch {
                return false;
            }
        }

        public void addLine(string Line)
        {
            if (!logging || logFileWriter == null) return;
            logFileWriter.WriteLine(Line);
            logFileWriter.Flush();
        }

        public void addValues(DateTime Time, double p1, double p2)
        {
            if (!logging || logFileWriter == null) return;

            if (Time.CompareTo(endTime) > 0)
            {
                TimeSpan duration = endTime - startTime;
                MainWindow.onStopLogging("Duration of " + duration.ToString() + " reached");
                return;
            }
            if (p1 > maxP1 || p1 < minP1)
            {
                MainWindow.onStopLogging("Pressure P1=" + p1.ToString("0.0000E+00") + " exceeded limit: " + minP1.ToString("0.0000E+00") + " / " + maxP1.ToString("0.0000E+00"));
                return;
            }
            if (p2 > maxP2 || p2 < minP2)
            {
                MainWindow.onStopLogging("Pressure P2=" + p2.ToString("0.0000E+00") + " exceeded limit: " + minP2.ToString("0.0000E+00") + " / " + maxP2.ToString("0.0000E+00"));
                return;
            }

            logFileWriter.WriteLine("{0}\t{1:0.0000#E+00}\t{2:0.0000#E+00}", Time, p1, p2);
            logFileWriter.Flush();
        }

        public void stopLogging()
        {
            logging = false;
            logFileWriter.Flush();
            logFileWriter.Close();
            logFileWriter.Dispose();
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
