using System;
using System.Collections.Generic;
using System.Text;

namespace WeilerGewinnRechner.model
{
    public class CalcResult
    {
        public Gesellschafter Gesellschafter { get; set; }

        public double Gewinn { get; set; }

        public bool IsVerlust { get; set; }

        public string Note { get; set; }

        public CalcResult(Gesellschafter gesellschafter, double gewinn, bool isVerlust, string note = "")
        {
            this.Gesellschafter = gesellschafter;
            this.Gewinn = gewinn;
            this.IsVerlust = isVerlust;
            this.Note = note;
        }
    }
}
