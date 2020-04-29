using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public static class Sectional_Properties
    {
        
        public static double S (this Node n)
        {
            // The slope of web
            return (n.w - (n.bbot - 2 * n.cbot)) / 2.0 / n.D;
        }

        public static double Hw(this Node n)
        {
            //The length of web along the slope
            return Math.Sqrt(n.D * n.D + n.D * n.S() * n.D * n.S());
        }

        // Calculating for Stage 1: Steel only
        public static double A1(this Node n)
        {            
            return n.ntop * n.btop * n.ttop + n.bbot * n.tbot + n.Hw() * n.tw * 2 + n.nsb * n.tsb * n.Hsb + n.nst * n.tst * n.Hst;
        }

        public static double YL1(this Node n)
        {
            return (n.ntop * n.btop * n.ttop * (0.5 * n.ttop + n.D + n.tbot) + 2 * n.Hw() * n.tw * (0.5 * n.D + n.tbot) + n.bbot * n.tbot * 0.5 * n.tbot + n.nsb * n.Hsb * n.tsb * (0.5 * n.Hsb + n.tbot) + n.bbot * n.tbot * 0.5 * n.tbot + n.nst * n.Hst * n.tst * (n.D - 0.5 * n.Hst + n.tbot)) / n.A1();
        }

        public static double YU1(this Node n)
        {
            return n.tbot + n.D + n.ttop - n.YL1();
        }

        public static double I1(this Node n)
        {
            return 1 / 12.0 * n.ntop * n.btop * n.ttop * n.ttop * n.ttop + n.ntop * n.btop * n.ttop * (n.YU1() - n.ttop / 2) * (n.YU1() - n.ttop / 2) + 1 / 12.0 / (n.S() * n.S() + 1) * 2 * n.tw * n.Hw() * n.Hw() * n.Hw() + 2 * n.Hw() * n.tw * (n.YU1() - n.ttop - n.D / 2) * (n.YU1() - n.ttop - n.D / 2) + 1 / 12.0 * n.bbot * n.tbot * n.tbot * n.tbot + n.bbot * n.tbot * (n.YL1() - n.tbot / 2) * (n.YL1() - n.tbot / 2) + 1 / 12.0 * n.nsb * n.tsb * n.Hsb * n.Hsb * n.Hsb + n.nsb * n.tsb * n.Hsb * (n.YL1() - n.tbot - n.Hsb / 2) * (n.YL1() - n.tbot - n.Hsb / 2) + 1 / 12.0 * n.nst * n.tst * n.Hst * n.Hst * n.Hst + n.nst * n.tst * n.Hst * (n.YU1() - n.ttop - n.Hst / 2) * (n.YU1() - n.ttop - n.Hst / 2);
        }

        public static double SL1(this Node n)
        {
            return n.I1() / n.YL1();
        }

        public static double SU1(this Node n)
        {
            return n.I1() / n.YU1();
        }

        //Calculating for Stage 2-1: Steel + bottom concrete (shorttime)

        
        public static double Ac(this Node n)
        {
            return (n.bbot - 2 * n.cbot) * n.Hc + n.Hc * n.S() * n.Hc;
        }

        public static double Ic(this Node n)
        {
            return n.Hc * n.Hc * n.Hc * ((n.bbot - 2 * n.cbot) * (n.bbot - 2 * n.cbot) + 4 * (n.bbot - 2 * n.cbot) * ((n.bbot - 2 * n.cbot) + 2 * n.Hc * n.S()) + ((n.bbot - 2 * n.cbot) + 2 * n.Hc * n.S()) * ((n.bbot - 2 * n.cbot) + 2 * n.Hc * n.S())) / 36.0 / ((n.bbot - 2 * n.cbot) + (n.bbot - 2 * n.cbot) + 2 * n.Hc * n.S());
        }

       
        public static double A2s(this Node n)
        {
            return n.A1() + n.Ac() / Material.nEb();
        }

        public static double YL2s(this Node n)
        {
            return (n.A1() * n.YL1() + n.Ac() * (n.Hc / 2 + n.tbot) / Material.nEb()) / n.A2s();
        }

        public static double YU2s(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL2s();
        }

        public static double I2s(this Node n)
        {
            return n.I1() + n.A1() * (n.YL2s() - n.YL1()) * (n.YL2s() - n.YL1()) + n.Ic() / Material.nEb() + n.Ac() / Material.nEb() * (n.YL2s() - n.tbot - n.Hc / 2) * (n.YL2s() - n.tbot - n.Hc / 2);
        }

        public static double SL2s(this Node n)
        {
            return n.I2s() / n.YL2s();
        }

        public static double SU2s(this Node n)
        {
            return n.I2s() / n.YU2s();
        }

        //Calculating for Stage 2-2: Steel + bottom concrete (longtime)

        public static double A2l(this Node n)
        {
            return n.A1() + n.Ac() / 3.0 / Material.nEb();
        }

        public static double YL2l(this Node n)
        {
            return (n.A1() * n.YL1() + n.Ac() * (n.Hc / 2 + n.tbot) / 3 / Material.nEb()) / n.A2l();
        }

        public static double YU2l(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL2l();
        }

        public static double I2l(this Node n)
        {
            return n.I1() + n.A1() * (n.YL2l() - n.YL1()) * (n.YL2l() - n.YL1()) + n.Ic() / 3 / Material.nEb() + n.Ac() / 3 / Material.nEb() * (n.YL2l() - n.tbot - n.Hc / 2) * (n.YL2l() - n.tbot - n.Hc / 2);
        }

        public static double SL2l(this Node n)
        {
            return n.I2l() / n.YL2l();
        }

        public static double SU2l(this Node n)
        {
            return n.I2l() / n.YU2l();
        }

        //Calculating for Stage 3-1: Steel + Deck (Shorttime) 
        public static double As(this Node n)
        {
            return n.bs * n.ts;
        }

        public static double Is1(this Node n)
        {
            return n.bs * n.ts * n.ts * n.ts / 12.0;
        }

        public static double Ah(this Node n)
        {
            return 2 * n.bh * n.th;
        }

        public static double Ih(this Node n)
        {
            return 4 * n.bh * n.th * n.th * n.th / 36.0;
        }

        public static double A3s(this Node n)
        {
            return n.A1() + (n.As() + n.Ah()) / Material.nEd();
        }

        public static double YL3s(this Node n)
        {
            return (n.A1() * n.YL1() + n.As() * (n.tbot + n.D + n.th + n.ts / 2) / Material.nEd() + n.Ah() * (n.tbot + n.D + 2 * n.th / 3.0) / Material.nEd()) / n.A3s();
        }

        public static double YU3s(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL3s();
        }

        public static double I3s(this Node n)
        {
            return n.I1() + n.A1() * (n.YL3s() - n.YL1()) * (n.YL3s() - n.YL1()) + n.Is1() / Material.nEd() + n.As() / Material.nEd() * (n.YU3s() - n.ttop + n.th + n.ts / 2.0) * (n.YU3s() - n.ttop + n.th + n.ts / 2);
        }

        public static double SL3s(this Node n)
        {
            return n.I3s() / n.YL3s();
        }

        public static double SU3s(this Node n)
        {
            return n.I3s() / n.YU3s();
        }

        //Calculating for Stage 3-1: Steel + Deck (longtime) 

        public static double A3l(this Node n)
        {
            return n.A1() + (n.As() + n.Ah()) / 3/ Material.nEd();
        }

        public static double YL3l(this Node n)
        {
            return (n.A1() * n.YL1() + n.As() * (n.tbot + n.D + n.th + n.ts / 2) / 3 / Material.nEd() + n.Ah() * (n.tbot + n.D + 2 * n.th / 3) / 3 / Material.nEd()) / n.A3l();
        }

        public static double YU3l(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL3l();
        }

        public static double I3l(this Node n)
        {
            return n.I1() + n.A1() * (n.YL3l() - n.YL1()) * (n.YL3l() - n.YL1()) + n.Is1() / 3 / Material.nEd() + n.As() / 3 / Material.nEd() * (n.YU3l() - n.ttop + n.th + n.ts / 2) * (n.YU3l() - n.ttop + n.th + n.ts / 2);
        }

        public static double SL3l(this Node n)
        {
            return n.I3l() / n.YL3l();
        }

        public static double SU3l(this Node n)
        {
            return n.I3l() / n.YU3l();
        }

        //Calculating for Stage 4-1: Steel + Bottom concrete + Rebar shortime 
        public static double Art(this Node n)
        {
            return Math.Floor(n.bs / n.art) * 0.25 * 3.14159 * n.drt * n.drt;
        }

        public static double Arb(this Node n)
        {
            return Math.Floor(n.bs / n.arb) * 0.25 * 3.14159 * n.drb * n.drb;
        }

        public static double Irt(this Node n)
        {
            return Math.Floor(n.bs / n.art) * 3.14159 * n.drt * n.drt * n.drt * n.drt / 64.0;
        }

        public static double Irb(this Node n)
        {
            return Math.Floor(n.bs / n.arb) * 3.14159 * n.drb * n.drb * n.drb * n.drb / 64.0;
        }

        public static double A4s(this Node n)
        {
            return n.A2s() + n.Art() + n.Arb();
        }

        public static double YL4s(this Node n)
        {
            return (n.A2s() * n.YL2s() + n.Art() * (n.tbot + n.D + n.th + n.ts - n.crt) + n.Arb() * (n.tbot + n.D + n.th + n.crb)) / n.A4s();
        }

        public static double YU4s(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL4s();
        }

        public static double I4s(this Node n)
        {
            return n.I2s() + n.A2s() * (n.YL4s() - n.YL2s()) * (n.YL4s() - n.YL2s()) + n.Irt() + n.Art() * (n.YU4s() - n.ttop + n.th + n.ts - n.crt) * (n.YU4s() - n.ttop + n.th + n.ts - n.crt) + n.Irb() + n.Arb() * (n.YU4s() - n.ttop + n.th + n.crb) * (n.YU4s() - n.ttop + n.th + n.crb);
        }

        public static double SL4s(this Node n)
        {
            return n.I4s() / n.YL4s();
        }

        public static double SU4s(this Node n)
        {
            return n.I4s() / n.YU4s();
        }

        //Calculating for Stage 4-2: Steel + Bottom concrete + Rebar longtime 

        public static double A4l(this Node n)
        {
            return n.A2l() + n.Art() / 3 + n.Arb() / 3;
        }

        public static double YL4l(this Node n)
        {
            return (n.A2l() * n.YL2l() + n.Art() / 3 * (n.tbot + n.D + n.th + n.ts - n.crt) + n.Arb() / 3 * (n.tbot + n.D + n.th + n.crb)) / n.A4l();
        }

        public static double YU4l(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL4l();
        }

        public static double I4l(this Node n)
        {
            return n.I2l() + n.A2l() * (n.YL4l() - n.YL2l()) * (n.YL4l() - n.YL2l()) + n.Irt() / 3 + n.Art() / 3 * (n.YU4l() - n.ttop + n.th + n.ts - n.crt) * (n.YU4l() - n.ttop + n.th + n.ts - n.crt) + n.Irb() / 3 + n.Arb() / 3 * (n.YU4l() - n.ttop + n.th + n.crb) * (n.YU4l() - n.ttop + n.th + n.crb);
        }

        public static double SL4l(this Node n)
        {
            return n.I4l() / n.YL4l();
        }

        public static double SU4l(this Node n)
        {
            return n.I4l() / n.YU4l();
        }

        //Calculating for Stage 5-1: Steel + Bottom concrete + rebar + Deckslab Shortime
        public static double A5s(this Node n)
        {
            return n.A4s() + n.As() / Material.nEd() + n.Ah() / Material.nEd();
        }

        public static double YL5s(this Node n)
        {
            return (n.A4s() * n.YL4s() + n.As() / Material.nEd() * (n.tbot + n.D + n.th + n.ts / 2) + n.Ah() / Material.nEd() * (n.tbot + n.D + 2 * n.th / 3)) / n.A5s();
        }

        public static double YU5s(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL5s();
        }

        public static double I5s(this Node n)
        {
            return n.I4s() + n.A4s() * (n.YL5s() - n.YL4s()) * (n.YL5s() - n.YL4s()) + n.Is1() / Material.nEd() + n.As() / Material.nEd() * (n.YU5s() - n.ttop + n.th + n.ts / 2) * (n.YU5s() - n.ttop + n.th + n.ts / 2) + n.Ih() / Material.nEd() + n.Ah() / Material.nEd() * (n.YU5s() - n.ttop + 2 * n.th / 3.0) * (n.YU5s() - n.ttop + 2 * n.th / 3.0);
        }

        public static double SL5s(this Node n)
        {
            return n.I5s() / n.YL5s();
        }

        public static double SU5s(this Node n)
        {
            return n.I5s() / n.YU5s();
        }

        //Calculating for Stage 5-2: Steel + Bottom concrete + rebar + Deckslab Longtime 
        public static double A5l(this Node n)
        {
            return n.A4l() + n.As() / 3 / Material.nEd() + n.Ah() / 3 / Material.nEd();
        }

        public static double YL5l(this Node n)
        {
            return (n.A4l() * n.YL4l() + n.As() / 3 / Material.nEd() * (n.tbot + n.D + n.th + n.ts / 2) + n.Ah() / 3 / Material.nEd() * (n.tbot + n.D + 2 * n.th / 3)) / n.A5l();
        }

        public static double YU5l(this Node n)
        {
            return n.D + n.tbot + n.ttop - n.YL5l();
        }

        public static double I5l(this Node n)
        {
            return n.I4l() + n.A4l() * (n.YL5l() - n.YL4l()) * (n.YL5l() - n.YL4l()) + n.Is1() / 3 / Material.nEd() + n.As() / 3 / Material.nEd() * (n.YU5l() - n.ttop + n.th + n.ts / 2) * (n.YU5l() - n.ttop + n.th + n.ts / 2) + n.Ih() / 3 / Material.nEd() + n.Ah() / 3 / Material.nEd() * (n.YU5l() - n.ttop + 2 * n.th / 3.0) * (n.YU5l() - n.ttop + 2 * n.th / 3.0);
        }

        public static double SL5l(this Node n)
        {
            return n.I5l() / n.YL5l();
        }

        public static double SU5l(this Node n)
        {
            return n.I5l() / n.YU5l();
        }
               

    }

}
