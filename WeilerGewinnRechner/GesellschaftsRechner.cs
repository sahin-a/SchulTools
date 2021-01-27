using System;
using System.Collections.Generic;
using System.Text;
using WeilerGewinnRechner.enums;
using WeilerGewinnRechner.model;

namespace WeilerGewinnRechner
{
    public class GesellschaftsRechner
    {
        private Gesellschaft Gesellschaft { get; set; }
        private Gesellschafter SelectedGesellschafter { get; set; }
        private int GesellschafterAnzahl { get; set; }

        public GesellschaftsRechner(Gesellschaft gesellschaft)
        {
            this.Gesellschaft = gesellschaft;
            this.SelectedGesellschafter = 
                this.Gesellschaft.Gesellschafter.Find(gesellschafter => gesellschafter.IsSelected);
            this.GesellschafterAnzahl = this.Gesellschaft.Gesellschafter.Count;
        }

        public List<CalcResult> Calc()
        {
            bool isVerlust = this.Gesellschaft.IsVerlust;

            switch (Gesellschaft.GesellschaftsForm)
            {
                case GesellschaftsForm.KG:
                    return isVerlust ? VerlustOHG() : GewinnOHG();
                case GesellschaftsForm.GMBH:
                    return isVerlust ? VerlustGMBH() : GewinnGMBH();
                case GesellschaftsForm.OHG:
                    return isVerlust ? VerlustOHG() : GewinnOHG();
            }

            return null;
        }

        private List<CalcResult> GewinnGMBH()
        {
            List<CalcResult> calcResults = new List<CalcResult>();

            List<double> prozente = new List<double>();
            this.Gesellschaft.Gesellschafter.ForEach(gesellschaft =>
            {
                double prozentAnteil = gesellschaft.Kapital / this.Gesellschaft.GesamtKapital;
                double gewinn = this.Gesellschaft.Gewinn * prozentAnteil;

                calcResults.Add(new CalcResult(gesellschaft, gewinn, false));
            });

            return calcResults;
        }

        private List<CalcResult> VerlustGMBH()
        {
            List<CalcResult> calcResults = new List<CalcResult>();

            return calcResults;
        }

        private List<CalcResult> GewinnOHG()
        {
            List<CalcResult> calcResults = new List<CalcResult>();

            double gewinn = this.Gesellschaft.Gewinn;
            int gesellschafterAnzahl = this.GesellschafterAnzahl;

            List<double> zusatzAnteile = new List<double>();
            double summeZusatzAnteile = 0;
            this.Gesellschaft.Gesellschafter.ForEach(gesellschafter =>
            {
                double zusatzAnteilDurchKapital = gesellschafter.Kapital * Gesellschaft.GewinnVerzinsung;
                zusatzAnteile.Add(zusatzAnteilDurchKapital);

                summeZusatzAnteile += zusatzAnteilDurchKapital;
            });


            double gewinnMitAbzuegen = Gesellschaft.Gewinn - summeZusatzAnteile;

            for (int i = 0; i < this.GesellschafterAnzahl; i++)
            {
                Gesellschafter gesellschafter = this.Gesellschaft.Gesellschafter[i];
                double gewinnGesellschafter = (gewinnMitAbzuegen / this.GesellschafterAnzahl) + zusatzAnteile[i];

                calcResults.Add(new CalcResult(gesellschafter, gewinnGesellschafter, false));
            }

            return calcResults;
        }

        private List<CalcResult> VerlustOHG()
        {
            List<CalcResult> calcResults = new List<CalcResult>();
            double verlust = this.Gesellschaft.Gewinn / this.Gesellschaft.Gesellschafter.Count;

            this.Gesellschaft.Gesellschafter.ForEach(gesellschafter =>
            {
                calcResults.Add(new CalcResult(gesellschafter, verlust, true));
            });

            return calcResults;
        }
    }
}
