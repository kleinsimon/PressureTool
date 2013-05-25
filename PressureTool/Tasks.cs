using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PressureTool
{
    enum Questions
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
        WDT
    }

    struct QuestionOptions
    {
        public int numParameter = 0;
        public bool Continous = false;
        public string RegexAnswer = "";

        public QuestionOptions(string RegexEpxectedAnswer, int NumberOfParameters, bool ContinousAnswer)
        {
            numParameter = NumberOfParameters;
            Continous = ContinousAnswer;
            RegexAnswer = RegexEpxectedAnswer;
        }
    }

    static struct serialInterface
    { 
        static public Dictionary<Questions, QuestionOptions> Answers = new Dictionary<Questions, QuestionOptions>()
        {
            { Questions.ADC, new QuestionOptions("", 0, false) },
            { Questions.BAU, new QuestionOptions("", 0, false) },
            { Questions.COM, new QuestionOptions("", 0, false) },
            { Questions.CAL, new QuestionOptions("", 0, false) },
            { Questions.DCD, new QuestionOptions("", 0, false) },
            { Questions.DGS, new QuestionOptions("", 0, false) },
            { Questions.DIC, new QuestionOptions("", 0, false) },
            { Questions.DIS, new QuestionOptions("", 0, false) },
            { Questions.EEP, new QuestionOptions("", 0, false) },
            { Questions.EPR, new QuestionOptions("", 0, false) },
            { Questions.ERR, new QuestionOptions("", 0, false) },
            { Questions.FIL, new QuestionOptions("", 0, false) },
            { Questions.FSR, new QuestionOptions("", 0, false) },
            { Questions.IOT, new QuestionOptions("", 0, false) },
            { Questions.LOC, new QuestionOptions("", 0, false) },
            { Questions.OFC, new QuestionOptions("", 0, false) },
            { Questions.OFD, new QuestionOptions("", 0, false) },
            { Questions.PNR, new QuestionOptions("", 0, false) },
            { Questions.PR1, new QuestionOptions("", 0, false) },
            { Questions.PR2, new QuestionOptions("", 0, false) },
            { Questions.PRX, new QuestionOptions(@"\d,[ +-]\d\.\d\d\d\dE[ +-]\d\d,\d,[ +-]\d\.\d\d\d\dE[ +-]\d\d", 0, false) },
            { Questions.PUC, new QuestionOptions("", 0, false) },
            { Questions.RAM, new QuestionOptions("", 0, false) },
            { Questions.RES, new QuestionOptions("", 0, false) },
            { Questions.RST, new QuestionOptions("", 0, false) },
            { Questions.SAV, new QuestionOptions("", 0, false) },
            { Questions.SC1, new QuestionOptions("", 0, false) },
            { Questions.SC2, new QuestionOptions("", 0, false) },
            { Questions.SCT, new QuestionOptions("", 0, false) },
            { Questions.SEN, new QuestionOptions("", 0, false) },
            { Questions.SP1, new QuestionOptions("", 0, false) },
            { Questions.SP2, new QuestionOptions("", 0, false) },
            { Questions.SP3, new QuestionOptions("", 0, false) },
            { Questions.SP4, new QuestionOptions("", 0, false) },
            { Questions.SPS, new QuestionOptions("", 0, false) },
            { Questions.TID, new QuestionOptions("", 0, false) },
            { Questions.TKB, new QuestionOptions("", 0, false) },
            { Questions.TLC, new QuestionOptions("", 0, false) },
            { Questions.UNI, new QuestionOptions("", 0, false) },
            { Questions.WDT, new QuestionOptions("", 0, false) },
        };
    }

    class AnswerFormat
    {
        string _f;
        public AnswerFormat(string RegexFormat)
        {
            _f = RegexFormat;
        }

        public bool Matches(string Answer)
        {
            Regex rx = new Regex(_f);
            return (rx.IsMatch(Answer)) ? true : false;
        }
    }
}
