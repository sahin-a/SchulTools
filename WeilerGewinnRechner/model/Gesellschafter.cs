using System;
using System.Collections.Generic;
using System.Text;
using WeilerGewinnRechner.enums;

namespace WeilerGewinnRechner.model
{
    public class Gesellschafter
    {
        public bool IsSelected { get; set; }

        private int kapital;
        public int Kapital { get
            {
                return kapital;
            }
            set
            {
                kapital = Int32.Parse(value.ToString());
            }
        }

        public Gesellschafter(int kapital)
        {
            this.Kapital = kapital;
        }
    }
}
