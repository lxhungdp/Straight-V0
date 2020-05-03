using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Sectional_Checking
{
    public class Check_SLS
    {
        private string _Label, _Flexure, _Compact, Flange, Web;
        private double _Ss2_top, _Ss2_bot, _Rh, _M4, _Mw, _MLLmin, _Sdeck, _Sbot1, _Sbot2, Hc, ttop, tbot, D, tfc, ns,
            Hw, tw, th, ts, crt, I3s, YL3s, I4s, YL4s, ds;

        public Check_SLS(string Label, string Flexure, string Compact, double Ss2_top, double Ss2_bot, double Rh, double M4, double Mw, double MLLmin, double Sdeck, double Sbot1, double Sbot2, double Hc,
            double ttop, double tbot, double D, double tfc, double ns,
            double Hw, double tw, double th, double ts, double crt, double I3s, double YL3s, double I4s, double YL4s, string Flange, string Web, double ds)
        {
            this._Label = Label;
            this._Flexure = Flexure;
            this._Compact = Compact;
            this._Ss2_bot = Ss2_bot;
            this._Ss2_top = Ss2_top;
            this._Rh = Rh;
            this._M4 = M4;
            this._Mw = Mw;
            this._MLLmin = MLLmin;
            this._Sdeck = Sdeck;
            this._Sbot1 = Sbot1;
            this._Sbot2 = Sbot2;
            this.Hc = Hc;
            this.ttop = ttop;
            this.tbot = tbot;
            this.D = D;
            this.tfc = tfc;
            this.ns = ns;
            this.Hw = Hw;
            this.tw = tw;
            this.th = th;
            this.ts = ts;
            this.crt = crt;
            this.I3s = I3s;
            this.YL3s = YL3s;
            this.I4s = I4s;
            this.YL4s = YL4s;
            this.Flange = Flange;
            this.Web = Web;
            this.ds = ds;

        }


        public string Label
        {
            get { return _Label; }

        }

        public string Flexure
        {
            get { return _Flexure; }

        }
        public string Compact
        {
            get { return _Compact; }

        }
        public double Ss2_top
        {
            get { return _Ss2_top; }

        }
        public double Ss2_bot
        {
            get { return _Ss2_bot; }

        }
        public double Rh
        {
            get { return _Rh; }

        }

        //Table 7.3.1. Checking flange stress

        public double RhFy
        {
            get { return 0.95 * Rh * Material.Fy(Flange, ttop); }

        }

        public string Check_flange
        {
            get
            {
                if (Compact == "Compact" && Flexure == "Positive")
                    return Math.Max(Math.Abs(Ss2_bot), Math.Abs(Ss2_top)) <= RhFy ? "OK" : "NG";
                else
                    return "-";
            }
        }

        public string Check_flange_ratio
        {
            get
            {
                if (Compact == "Compact" && Flexure == "Positive")
                    return Math.Max(Math.Abs(Ss2_bot), Math.Abs(Ss2_top)) == 0 ? "Inf" : (RhFy / Math.Max(Math.Abs(Ss2_bot), Math.Abs(Ss2_top))).ToString();
                else
                    return "-";
            }
        }


        //Check buckling of web




        public double fc
        {
            get
            {
                return Flexure == "Positive" ? Ss2_top : Ss2_bot;
            }
        }

        public double dt
        {
            get { return ttop + D + tbot; }
        }

        public double Dc
        {
            get
            {
                if (Math.Abs(Ss2_bot) + Math.Abs(Ss2_top) == 0)
                    return D;
                else
                    return (Flexure == "Positive" ? Math.Abs(Ss2_top) : Math.Abs(Ss2_bot)) / (Math.Abs(Ss2_bot) + Math.Abs(Ss2_top)) * dt - tfc;
            }
        }


        public double Psi
        {
            get
            {
                if (Ss2_bot == 0 || Ss2_top == 0)
                    return 0;
                else
                    return Flexure == "Positive" ? Ss2_bot / Ss2_top : Ss2_top / Ss2_bot;
            }
        }

        public double k_bend
        {
            get
            {
                double k = 0;
                switch (ns)
                {
                    case 0:
                        k = 9 / (Dc / D) / (Dc / D);
                        break;
                    case 1:
                        {
                            if (ds / Dc >= 0.4)
                                k = Math.Max(5.17 / (ds / D) / (ds / D), 9 / (Dc / D) / (Dc / D));
                            else
                                k = 11.64 / Math.Pow((Dc - ds) / D, 2);
                        }
                        break;

                    case 2:
                        {
                            if (Psi >= -1)
                                k = 247.8 * Math.Pow((ds / Dc), 1.8) * Math.Pow((1 - Psi), 2.7);
                            else
                                k = 247.8 * Math.Pow((1 - Psi), 0.32);
                        }
                        break;
                }
                return k;
            }

        }



        public double Fcrw
        {
            get
            {
                return Math.Min(0.9 * Material.Es * k_bend / Math.Pow(Hw / tw, 2), Math.Min(Rh * Material.Fy(Flange, ttop), Material.Fy(Web, tw) / 0.7));
            }
        }


        public string Check_buckling
        {
            get
            {
                return Math.Abs(fc) <= Fcrw ? "OK" : "NG";
            }
        }

        //Checking stress of rebar
        public double Srebar
        {
            get { return Flexure == "Positive" ? I3s / (tbot + D + th + ts - YL3s - crt) : I4s / (tbot + D + th + ts - YL4s - crt); }

        }

        public double M4
        {
            get { return _M4; }
        }

        public double Mw
        {
            get { return _Mw; }
        }
        public double MLLmin
        {
            get { return _MLLmin; }
        }

        public double fs
        {
            get { return Flexure == "Positive" ? 0.0 : -(M4 + Mw + MLLmin) / Srebar * 1000000; }
        }

        public string Check_fs
        {
            get { return Flexure == "Positive" ? "-" : (Math.Abs(fs) <= 0.8 * Material.Fyb ? "OK" : "NG"); }
        }

    }
}
