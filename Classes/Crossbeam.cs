using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Crossbeam
    {
        public Crossbeam(double ttop, double btop, double tbot, double bbot, double D, double tw, double nw)
        {
            this.ttop = ttop;
            this.btop = btop;
            this.tbot = tbot;
            this.bbot = bbot;
            this.D = D;
            this.tw = tw;
            this.nw = nw;

        }
        public double ttop
        {
            get; set;
        }

        public double btop
        {
            get; set;
        }
        public double tbot
        {
            get; set;
        }
        public double bbot
        {
            get; set;
        }
        public double D
        {
            get; set;
        }
        public double tw
        {
            get; set;
        }

        public double nw
        {
            get; set;
        }
    }
}
