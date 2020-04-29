using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checking
{
    public class Sec
    {
        private string _Label;
        private double ntop, btop, ttop, ctop, bbot, tbot, cbot, D, tw, Hc, ts, bs, th, bh, b, w, a, drt, art, crt, drb, arb, crb, srb, nsb, Hsb, tsb, 
            nst, Hst, tst, S, Hw, Ac, Ic, As, Is1, Ah, Ih, Art, Arb, Irt, Irb;
        public Sec(string Label, double ntop, double btop, double ttop, double ctop, double bbot, double tbot, double cbot, double D, double tw, double Hc, double ts, double bs, double th,
            double bh, double b, double w, double a, double drt, double art, double crt, double drb, double arb, double crb, double srb, double nsb, double Hsb,
            double tsb, double nst, double Hst, double tst, double S, double Hw, double Ac, double Ic, double As, double Is1, double Ah, double Ih, double Art, double Arb, double Irt, double Irb)
        {
            this._Label = Label;
            this.ntop = ntop;
            this.btop = btop;
            this.ttop = ttop;
            this.ctop = ctop;

            this.bbot = bbot;
            this.tbot = tbot;
            this.cbot = cbot;

            this.D = D;
            this.tw = tw;
            this.Hc = Hc;
            this.ts = ts;
            this.bs = bs;
            this.th = th;
            this.bh = bh;
            this.b = b;
            this.w = w;
            this.a = a;

            this.drt = drt;
            this.art = art;
            this.crt = crt;
            this.drb = drb;
            this.arb = arb;
            this.crb = crb;

            this.srb = srb;

            this.nsb = nsb;
            this.Hsb = Hsb;
            this.tsb = tsb;
            this.nst = nst;
            this.Hst = Hst;
            this.tst = tst;

            this.S = S;
            this.Hw = Hw;
            this.Ac = Ac;
            this.Ic = Ic;
            this.As = As;
            this.Is1 = Is1;
            this.Ah = Ah;
            this.Ih = Ih;
            this.Art = Art;
            this.Arb = Arb;
            this.Irt = Irt;
            this.Irb = Irb;
        }

                

        public string Label
        {
            get { return _Label; }

        }

        // Stage 1: Steel only
        public double A1
        {
            get { return ntop * btop * ttop + bbot * tbot + Hw * tw * 2 + nsb * tsb * Hsb + nst * tst * Hst; }
            
        }

        public double YL1
        {
            get { return (ntop * btop * ttop * (0.5 * ttop + D + tbot) + 2 * Hw * tw * (0.5 * D + tbot) + bbot * tbot * 0.5 * tbot + nsb * Hsb * tsb * (0.5 * Hsb + tbot) + nst * Hst * tst * (D - 0.5 * Hst + tbot)) / A1; }
        }

        public double YU1
        {
            get { return tbot + D + ttop - YL1; }
        }

        public double I1
        {
            get { return 1 / 12.0 * ntop * btop * ttop * ttop * ttop + ntop * btop * ttop * (YU1 - ttop / 2) * (YU1 - ttop / 2) + 1 / 12.0 / (S * S + 1) * 2 * tw * Hw * Hw * Hw + 2 * Hw * tw * (YL1 - tbot - D / 2) * (YL1 - tbot - D / 2) + 1 / 12.0 * bbot * tbot * tbot * tbot + bbot * tbot * (YL1 - tbot / 2) * (YL1 - tbot / 2) + 1 / 12.0 * nsb * tsb * Hsb * Hsb * Hsb + nsb * tsb * Hsb * (YL1 - tbot - Hsb / 2) * (YL1 - tbot - Hsb / 2) + 1 / 12.0 * nst * tst * Hst * Hst * Hst + nst * tst * Hst * (YU1 - ttop - Hst / 2) * (YU1 - ttop - Hst / 2); }
        }

        public double SL1
        {
            get { return I1 / YL1; }
        }

        public double SU1
        {
            get { return I1 / YU1; }
        }


        //Calculating for Stage 2-1: Steel + bottom concrete (shorttime)     
        
        public double A2s
        {
            get { return A1 + Ac / Material.nEb; }
        }

        public double YL2s
        {
            get { return (A1 * YL1 + Ac * (Hc / 2 + tbot) / Material.nEb) / A2s; }
        }

        public double YU2s
        {
            get { return D + tbot + ttop - YL2s; }
        }

        public  double I2s
        {
            get { return I1 + A1 * (YL2s - YL1) * (YL2s - YL1) + Ic / Material.nEb + Ac / Material.nEb * (YL2s - tbot - Hc / 2) * (YL2s - tbot - Hc / 2); }
        }

        public double SL2s
        {
            get { return I2s / YL2s; }
        }

        public double SU2s
        {
            get { return I2s /YU2s; }
        }

        //Calculating for Stage 2-2: Steel + bottom concrete (longtime)

        public double A2l
        {
            get { return A1 + Ac / 3.0 / Material.nEb; }
        }

        public double YL2l
        {
            get { return (A1 * YL1 + Ac * (Hc / 2 + tbot) / 3 / Material.nEb) / A2l; }
        }

        public double YU2l
        {
            get { return D + tbot + ttop - YL2l; }
        }

        public double I2l
        {
            get { return I1 + A1 * (YL2l - YL1) * (YL2l - YL1) + Ic / 3 / Material.nEb + Ac / 3 / Material.nEb * (YL2l - tbot - Hc / 2) * (YL2l - tbot - Hc / 2); }
        }

        public double SL2l
        {
            get { return I2l / YL2l; }
        }

        public double SU2l
        {
            get { return I2l / YU2l; }
        }

        //Calculating for Stage 3-1: Steel + Deck (Shorttime) 
        public double A3s
        {
            get { return A1 + (As + Ah) / Material.nEd; }
        }

        public double YL3s
        {
            get { return (A1 * YL1 + As * (tbot + D + th + ts / 2) / Material.nEd + Ah * (tbot + D + 2 * th / 3.0) / Material.nEd) / A3s; }
        }

        public  double YU3s
        {
            get { return D + tbot + ttop - YL3s; }
        }

        public double I3s
        {
            get { return I1 + A1 * (YL3s - YL1) * (YL3s - YL1) + Is1 / Material.nEd + As / Material.nEd * (YU3s - ttop + th + ts / 2.0) * (YU3s - ttop + th + ts / 2) + Ih / Material.nEd + Ah / Material.nEd * (YU3s - ttop + 2*th/3) * (YU3s - ttop + 2 * th / 3); }
        }

        public double SL3s
        {
            get { return I3s / YL3s; }
        }

        public double SU3s
        {
            get { return I3s / YU3s; }
        }

        //Calculating for Stage 3-1: Steel + Deck (longtime) 

        public double A3l
        {
            get { return A1 + (As + Ah) / 3 / Material.nEd; }
        }

        public double YL3l
        {
            get { return (A1 * YL1 + As * (tbot + D + th + ts / 2) / 3 / Material.nEd + Ah * (tbot + D + 2 * th / 3) / 3 / Material.nEd) / A3l; }
        }

        public double YU3l
        {
            get { return D + tbot + ttop - YL3l; }
        }

        public double I3l
        {
            get { return I1 + A1 * (YL3l - YL1) * (YL3l - YL1) + Is1 / 3 / Material.nEd + As / 3 / Material.nEd * (YU3l - ttop + th + ts / 2) * (YU3l - ttop + th + ts / 2) + Ih/3 / Material.nEd + Ah/3 / Material.nEd * (YU3l - ttop + 2 * th / 3) * (YU3l - ttop + 2 * th / 3); }
        }

        public double SL3l
        {
            get { return I3l / YL3l; }
        }

        public double SU3l
        {
            get { return I3l / YU3l; }
        }

        //Calculating for Stage 4-1: Steel + Bottom concrete + Rebar shortime 
       

        public double A4s
        {
            get { return A2s + Art + Arb; }
        }

        public double YL4s
        {
            get { return (A2s * YL2s + Art * (tbot + D + th + ts - crt) + Arb * (tbot + D + th + crb)) / A4s; }
        }

        public double YU4s
        {
            get { return D + tbot + ttop - YL4s; }
        }

        public double I4s
        {
            get { return I2s + A2s * (YL4s - YL2s) * (YL4s - YL2s) + Irt + Art * (YU4s - ttop + th + ts - crt) * (YU4s - ttop + th + ts - crt) + Irb + Arb * (YU4s - ttop + th + crb) * (YU4s - ttop + th + crb); }
        }

        public double SL4s
        {
            get { return I4s / YL4s; }
        }

        public double SU4s
        {
            get { return I4s / YU4s; }
        }

        //Calculating for Stage 4-2: Steel + Bottom concrete + Rebar longtime 

        public double A4l
        {
            get { return A2l + Art / 3 + Arb / 3; }
        }

        public double YL4l
        {
            get { return (A2l * YL2l + Art / 3 * (tbot + D + th + ts - crt) + Arb / 3 * (tbot + D + th + crb)) / A4l; }
        }

        public double YU4l
        {
            get { return D + tbot + ttop - YL4l; }
        }

        public double I4l
        {
            get { return I2l + A2l * (YL4l - YL2l) * (YL4l - YL2l) + Irt / 3 + Art / 3 * (YU4l - ttop + th + ts - crt) * (YU4l - ttop + th + ts - crt) + Irb / 3 + Arb / 3 * (YU4l - ttop + th + crb) * (YU4l - ttop + th + crb); }
        }

        public double SL4l
        {
            get { return I4l / YL4l; }
        }

        public double SU4l
        {
            get { return I4l / YU4l; }
        }

        


    }
}
