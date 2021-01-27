using System;
using System.Collections.Generic;
using System.Text;
using WeilerGewinnRechner.enums;

namespace WeilerGewinnRechner.model
{
    public class Gesellschaft
    {
        public GesellschaftsForm GesellschaftsForm { get; set; }

        public List<Gesellschafter> Gesellschafter { get; set; }

        public double Gewinn { get; set; }

        public double GewinnVerzinsung { get; set; }

        public double GesamtKapital { get; set; }

        public bool IsVerlust { get; set; }

        public Gesellschaft(GesellschaftsForm gesellschaftsForm, List<Gesellschafter> gesellschafter, double gewinn, double verzinsung, bool isVerlust)
        {
            this.GesellschaftsForm = gesellschaftsForm;
            this.Gesellschafter = gesellschafter;
            this.Gewinn = gewinn;
            this.GewinnVerzinsung = verzinsung;
            this.IsVerlust = isVerlust;

            gesellschafter.ForEach(gesellschafter => this.GesamtKapital += gesellschafter.Kapital);
        }
    }
}
