using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Crossbeam
    {
        public Crossbeam(string type, double ttop, double btop, double tbot, double bbot, double D, double tw, double nw)
        {
            this.ttop = ttop;
            this.btop = btop;
            this.tbot = tbot;
            this.bbot = bbot;
            this.D = D;
            this.tw = tw;
            this.nw = nw;
            this.type = type;

        }
        public string type
        {
            get; set;
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

        public double Area()
        {
            return btop * ttop + bbot * tbot + D * tw * nw;
        }

        public double Ix()
        {
            double Itop = btop * Math.Pow(ttop, 3) / 12;
            double Ibot = bbot * Math.Pow(tbot, 3) / 12;
            double Iweb = nw * tw * Math.Pow(D, 3) / 12;

            double Atop = btop * ttop;
            double Abot = bbot * tbot;
            double Aweb = nw * D * tw;

            double Ytop = ttop / 2 + D + tbot;
            double Ybot = tbot / 2;
            double Yweb = D / 2 + tbot;

            double YL = (Atop * Ytop + Aweb * Yweb + Abot * Ybot) / (Atop + Abot + Aweb);

            return Itop + Ibot + Iweb + Atop * (YL - Ytop) * (YL - Ytop) + Abot * (YL - Ybot) * (YL - Ybot) + Aweb * (YL - Yweb) * (YL - Yweb);
        }

        public double Iy()
        {
            double Iweb;
            if (nw == 1)
                Iweb = D * Math.Pow(tw, 3) / 12;
            else
                Iweb = 2 * D * Math.Pow(tw, 3) / 12 + 2 * D * tw * Math.Pow(btop / 2 - tw / 2  , 2);

            double Itop = ttop * Math.Pow(btop, 3) / 12;
            double Ibot = tbot * Math.Pow(bbot, 3) / 12;

            return Itop + Ibot + Iweb ;
        }

        public double J()
        {
            if (nw == 1)
                return (btop * Math.Pow(ttop, 3) + bbot * Math.Pow(tbot, 3) + (D - ttop - tbot) * Math.Pow(tw, 3)) / 3;
            else
            {
                double a = Math.Max(btop, D + tbot + ttop);
                double b = Math.Min(btop, D + tbot + ttop);
                return a * Math.Pow(b, 3) * (1.0 / 3.0 - 0.21 * b / a * (1 - Math.Pow(b, 4) / 12 / Math.Pow(a, 4)));
            }                

        }

    }
}
