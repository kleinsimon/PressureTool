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

    
}
