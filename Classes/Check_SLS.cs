using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Check_SLS
    {
        private double YU3s, YU4s, YL3s, YL4s, ttop, tbot, ntop, btop, bbot, tw, _ttop, _tbot, _tw, Fytop, Fybot, Fyweb, ns, Hw, th, ts, crt, crb, Ix3s, Ix4s;
        private Mat Flange, Web, Rebar;

        public Check_SLS(Node Node, Sec Sec, Stress Stress, ElmForces Moment, List<Mat> Mat)
        {
            this.Element = Sec.Element;
            this.Joint = Sec.Node;
            this.Station = Sec.Station;
            this.Label = Node.Label;
            this.ttop = Node.ttop;
            this.tbot = Node.tbot;
            this.ntop = Node.ntop;
            this.btop = Node.btop;
            this.bbot = Node.bbot;
            this.tw = Node.tw;
            this.ns = Node.ns;
            this.D = Node.D;
            this.Hw = Node.Hw;
            this.th = Node.th;
            this.ts = Node.ts;
            this.crt = Node.crt;
            this.crb = Node.crb;
            
            //Stress
            this.Flexure = Stress.Su_top <= 0 ? "Positive" : "Negative";
            this.Ss2_top = Stress.Ss2_top;
            this.Ss2_bot = Stress.Ss2_bot;

            //Sec
            this.YU3s = Sec.YU3s;
            this.YL3s = Sec.YL3s;
            this.Ix3s = Sec.Ix3s;
            
            this.YU4s = Sec.YU4s;            
            this.YL4s = Sec.YL4s;
            this.Ix4s = Sec.Ix4s;
            

            //Material
            this.Flange = Mat[0];
            this.Web = Mat[1];
            this.Rebar = Mat[2];

            this._ttop = Node.ttop;
            this._tbot = Node.tbot;
            this._tw = Node.tw;
            this.Fytop = Flange.Fys(this._ttop);
            this.Fybot = Flange.Fys(this._tbot);
            this.Fyweb = Web.Fys(this._tw);

            //Forces
            this.M4 = Moment.DC4;
            this.Mw = Moment.DW;
            this.MLLmax = Moment.LLmax;
            this.MLLmin = Moment.LLmin;
        }


        public string Element
        {
            get; set;

        }
        public int Joint
        {
            get; set;

        }
        public double Station
        {
            get; set;

        }
        public string Label
        {
            get; set;

        }



        public string Flexure
        {
            get; set;

        }

        public double Ss2_top
        {
            get; set;

        }
        public double Ss2_bot
        {
            get; set;

        }

        public double YU
        {
            get { return Flexure == "Positive" ? YU3s : YU4s; }
        }

        public double YL
        {
            get { return Flexure == "Positive" ? YL3s : YL4s; }
        }

        public double Dn
        {
            get { return Math.Max(YU - ttop, YL - tbot); }
        }

        public double Afn
        {
            get { return YU - ttop >= YL - tbot ? ntop * btop * ttop : bbot * tbot; }
        }
        public double beta
        {
            get { return 2 * Dn * tw / Afn; }
        }

        public double fn
        {
            get { return Math.Max(Fytop, Math.Max(Math.Abs(Ss2_top), Math.Abs(Ss2_bot))); }
        }
        public double rho
        {
            get { return Math.Min(Fyweb / fn, 1.0); }
        }

        public double Rh
        {
            get { return Fyweb >= Fytop ? 1.0 : (12 + beta * (3 * rho - rho * rho * rho)) / (12 + 2 * beta); }
        }


        //Table 7.3.1. Checking flange stress

        public double RhFytop
        {
            get { return 0.95 * Rh * Fytop; }

        }

        public double RhFybot
        {
            get { return 0.95 * Rh * Fybot; }

        }

        public string Check_topflange
        {
            get
            {
                return Math.Abs(Ss2_top) <= RhFytop ? "OK" : "NG";

            }
        }

        public double Check_topflange_ratio
        {
            get
            {
                return Math.Abs(Ss2_top) == 0 ? 1000000 : RhFytop / Math.Abs(Ss2_top);

            }
        }

        public string Check_botflange
        {
            get
            {
                return Math.Abs(Ss2_bot) <= RhFybot ? "OK" : "NG";

            }
        }

        public double Check_botflange_ratio
        {
            get
            {
                return Math.Abs(Ss2_bot) == 0 ? 1000000 : RhFybot / Math.Abs(Ss2_bot);

            }
        }

        //Check buckling of web
        public double dt
        {
            get { return ttop + D + tbot; }
        }

        public double tfc
        {
            get { return Flexure == "Positive" ? ttop : tbot; }
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

        public double ds
        {
            get
            {
                double ds = 0;
                switch (ns)
                {
                    case 0:
                        ds = 0;
                        break;
                    case 1:
                        ds = 0.2 * D;
                        break;
                    case 2:
                        ds = (0.14 + 0.36) / 2 * D;
                        break;
                }
                return ds;
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

        public double D
        {
            get; set;
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
                return Math.Min(0.9 * Web.Es * k_bend / Math.Pow(Hw / tw, 2), Math.Min(Rh * Fytop, Fyweb / 0.7));
            }
        }
        public double fc
        {
            get
            {
                return Flexure == "Positive" ? Ss2_top : Ss2_bot;
            }
        }

        public string Check_buckling
        {
            get
            {
                return Math.Abs(fc) <= Fcrw ? "OK" : "NG";
            }
        }

        public double Check_buckling_ratio
        {
            get
            {
                return Math.Abs(fc) == 0 ? 1000000 : Fcrw / Math.Abs(fc);
            }
        }


        //Checking stress of rebar
        public double M4
        {
            get; set;
        }
        public double Mw
        {
            get; set;
        }
        public double MLLmin
        {
            get; set;
        }
        public double MLLmax
        {
            get; set;
        }

        public double Srebar
        {
            get { return Flexure == "Positive" ? Ix3s / (tbot + D + th + ts - YL3s - crt) : Ix4s / (tbot + D + th + ts - YL4s - crt); }

        }

        public double fs
        {
            get { return Flexure == "Positive" ? 0.0 : -(M4 + Mw + MLLmin) / Srebar * 1000000; }
        }

        public double O8Fy
        {
            get { return 0.8 * Rebar.Fy; }
        }

        public string Check_fs
        {
            get { return Flexure == "Positive" ? "-" : (Math.Abs(fs) <= 0.8 * Rebar.Fy ? "OK" : "NG"); }
        }

        public double Check_fs_ratio
        {
            get { return Flexure == "Positive" || Math.Abs(fs) == 0 ? 1000000 : 0.8 * Rebar.Fy / Math.Abs(fs); }
        }
    }
}
