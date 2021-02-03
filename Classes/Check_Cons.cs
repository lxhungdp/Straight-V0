using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Check_Cons
    {
        private double Ws, Wc, b, ts, Pforms, YU1, YL1, YU2s, YL2s, _ttop, _tbot, _tw;        
        private Mat Flange, Web;
        public Check_Cons(Node Node, Sec Sec, Stress Stress, ElmForces Moment, ElmForces Torsion, ElmForces Shear, Mat Flange, Mat Web, double Pforms)
        {
            //Dim
            this.Element = Sec.Element;            
            this.Joint = Sec.Node;
            this.Station = Sec.Station;
            this.Label = Node.Label;
            this.Lb = Node.Lb;
            this.A1 = Sec.A1;
            this.Ac = Node.Ac;
            this.As1 = Node.As1;
            this.Ah = Node.Ah;
            this.b = Math.Max(Math.Max(Node.bleft, Node.bright), Math.Max(Node.aleft / 2, Node.aright / 2)) - Node.w / 2;
            this.ts = Node.ts;
            this.S = Node.S;
            this.ttop = Node.ttop;
            this.tbot = Node.tbot;
            this.ntop = Node.ntop;
            this.btop = Node.btop;
            this.bbot = Node.bbot;
            this.tw = Node.tw;
            this.D = Node.D;
            this.cbot = Node.cbot;
            this.w = Node.w;
            this.Hst = Node.Hst;
            this.tst = Node.tst;
            this.nst = Node.nst;
            this.Hsb = Node.Hsb;
            this.tsb = Node.tsb;
            this.nsb = Node.nsb;
            this.ns = Node.ns;
            this.d0 = Node.d0;
            this.Hw = Node.Hw;
            this.R = 0;

            //Material
            this.Ws = Flange.Ws;
            this.Wc = Flange.Wc;
            this.Flange = Flange;
            this.Web = Web;
            this._ttop = Node.ttop;
            this._tbot = Node.tbot;
            this._tw = Node.tw;
            this.Fytop = Flange.Fys(this._ttop);
            this.Fybot = Flange.Fys(this._tbot);
            this.Fyweb = Web.Fys(this._tw);

            //Loading
            this.Pforms = Pforms;

            //Forces
            this.M1 = Moment.DC1;
            this.M2 = Moment.DC2;
            this.M3 = Moment.DC3;
            this.T1 = Torsion.DC1;
            this.T2 = Torsion.DC2;
            this.T3 = Torsion.DC3;
            this.S1 = Shear.DC1;
            this.S2 = Shear.DC2;
            this.S3 = Shear.DC3;

            //Stress
            this.Sc_top = Stress.Sc_top;
            this.Sc_bot = Stress.Sc_bot;

            //Sec
            this.YU1 = Sec.YU1;
            this.YL1 = Sec.YL1;
            this.YU2s = Sec.YU2s;
            this.YL2s = Sec.YL2s;
            
            
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

        public double Lb
        {
            get; set;
        }

        public double A1
        {
            get; set;

        }

        public double Ac
        {
            get; set;

        }

        public double As1
        {
            get; set;

        }

        public double Ah
        {
            get; set;

        }

        public double DeltaV
        {
            get { return (A1 * Ws + (Ac + As1 + Ah) * Wc) / 1000000 / 2; }
        }
        public double S
        {
            get; set;

        }

        public double Mlw
        {
            get { return DeltaV * Math.Tan(S) * Lb * Lb / 12 / 1000000; }
        }

        public double tana
        {
            get { return (D / (D * S + b)); }
        }

        public double Fcon
        {
            get { return b * ts * Wc / 1000000; }
        }
        public double Mlo
        {
            get { return 0.5 * Fcon / tana * Lb * Lb / 12 / Math.Pow(10, 6); }
        }

        public double Mlf
        {
            get { return 0.5 * Pforms / (D / (D * S + b)) * Lb * Lb / 12 / 1000000; }
        }

        public double R
        {
            get; set;
        }
        public double M1
        {
            get; set;
        }
        public double M2
        {
            get; set;
        }
        public double M3
        {
            get; set;
        }

        public double Mlc
        {
            get { return R > 0 ? (M1 + M2 + M3) * Lb * Lb / 12 / R / D : 0; }
        }

        //Table 2
        public double Fytop
        {
            get; set;
        }
        public double Fybot
        {
            get; set;
        }

        public double Fyweb
        {
            get; set;
        }


        public double Sc_top
        {
            get; set;
        }
        public double Sc_bot
        {
            get; set;
        }

        public string Flexure
        {
            get { return Sc_top <= 0 ? "Positive" : "Negative" ; }

        }
        public double Dc1
        {
            get
            {
                return (Flexure == "Positive" ? YU1 - ttop : YL1 - tbot) / Math.Cos(Math.Atan(S));
            }
        }

        public double rt
        {
            get
            {
                return btop / Math.Pow(12 * (1 + Dc1 * tw / 3 / btop / ttop), 0.5);
            }
        }

        public double Sl
        {
            get { return ttop * btop * btop / 6.0; }
        }

        public double fl1
        {
            get { return ntop == 1 ? 0 : ((1.25 * Mlw + 1.25 * Mlo + 1.50 * Mlf + 1.50 * Mlc) / Sl * 1000000); }
        }


        public double Fcr
        {
            get { return Math.PI * Math.PI * Flange.Es / (Lb / rt) / (Lb / rt); }
        }


        public double Lp
        {
            get { return 1.0 * rt * Math.Sqrt(Flange.Es / Fytop); }
        }

        public double fl
        {
            get { return Flexure == "Positive" ? (Lb <= 1.2 * Lp * Math.Pow(Fytop / Math.Abs(Sc_top), 0.5) ? fl1 : (Math.Max(0.85 / (1 - Math.Abs(Sc_top) / Fcr), 1) * fl1)) : fl1; }
        }

        public double O6Fy
        {
            get { return 0.6 * Fytop; }
        }

        public string Check_fl
        {
            get { return fl <= 0.6 * Fytop ? "OK" : "NG"; }
        }

        public double Check_fl_ratio
        {
            get { return fl == 0 ? 1000000 : Math.Round(0.6 * Fytop / Math.Abs(fl), 2); }
        }

        //2. Checking flange stress ----------------------------------------------------------
        // Rh, A0_NC, fv, Delta, Fnc_LB, Fnc_LTB, Fnc_OF, tfc, bfc, wrib, nrib, Irib, k, ks, Fcb, Fcv, Fnc_BF, Fnc_Cons, Sc_com, Sc_ten, Dc, k1, Slender, flcom, 
        //fbufl_com, fubfl3_com, fubfl_ten, CheckC_comOF, CheckC_com, Ten_resis, CheckC_ten, Fcrw, CheckC_buckling 

        //2.1. Calculating Rh for Constructibility and ULS
        //Table 3

        public double YU
        {
            get { return Flexure == "Positive" ? YU1 : YU2s; }
        }

        public double YL
        {
            get { return Flexure == "Positive" ? YL1 : YL2s; }
        }

        public double ttop
        {
            get; set;
        }

        public double tbot
        {
            get; set;
        }

        public double Dn
        {
            get { return Math.Max(YU - ttop, YL - tbot); }
        }

        public double ntop
        {
            get; set;
        }

        public double btop
        {
            get; set;
        }

        public double bbot
        {
            get; set;
        }

        public double Afn
        {
            get { return YU - ttop >= YL - tbot ? ntop * btop * ttop : bbot * tbot; }
        }

        public double fn
        {
            get { return Math.Max(Fytop, Math.Max(Math.Abs(Sc_top), Math.Abs(Sc_bot))); }
        }

        public double rho
        {
            get { return Math.Min(Web.Fys(tw) / fn, 1.0); }
        }

        public double tw
        {
            get; set;
        }

        public double beta
        {
            get { return 2 * Dn * tw / Afn; }
        }

        public double Rh
        {
            get { return Web.Fys(tw) >= Fytop ? 1.0 : (12 + beta * (3 * rho - rho * rho * rho)) / (12 + 2 * beta); }

        }

        //2.1. Calculating Delta
        //Table 4
        public string BFOFcom
        {
            get { return Flexure == "Positive" ? (ntop == 2 ? "OF" : "BF") : "BF"; }
        }

        public string BFOFten
        {
            get { return Flexure == "Positive" ? "BF" : (ntop == 2 ? "OF" : "BF"); }
        }

        //2.2. Calcualtion Fnc
        //2.2.1 Calcualtion Fnc for Open flange
        public double Fyr
        {
            get { return Math.Max(Math.Min(0.7 * Fytop, Web.Fys(tw)), 0.5 * Fytop); }
        }

        public double xf1
        {
            get { return btop / 2 / ttop; }
        }

        public double xpf
        {
            get { return 0.38 * Math.Sqrt(Flange.Es / Fytop); }
        }
        public double xrf
        {
            get { return 0.56 * Math.Sqrt(Flange.Es / Fyr); }
        }

        public double Fnc_LB
        {
            get
            {
                if (xf1 <= xpf)
                    return 1.0 * Rh * Fytop;
                else
                    return (1 - (1 - Fyr / Rh / Fytop) * ((xf1 - xpf) / (xrf - xpf))) * Rh * Fytop;
            }
        }
        public double Lr
        {
            get { return Math.PI * rt * Math.Sqrt(Flange.Es / Fyr); }
        }

        public double Fnc_LTB
        {
            get
            {
                if (Lb <= Lp)
                    return 1.0 * Rh * Fytop;
                else if (Lb <= Lr)
                    return Math.Min(1 - (1 - Fyr / Rh / Fytop) * ((Lb - Lp) / (Lr - Lp)), 1) * Rh * Fytop;
                else
                    return Math.Min(Fcr, Rh * Fytop);
            }
        }

        public double Fnc_OF
        {
            get { return Math.Min(Fnc_LB, Fnc_LTB); }
        }

        //Table 5
        public double cbot
        {
            get; set;
        }

        public double w
        {
            get; set;
        }
        public double D
        {
            get; set;
        }
        public double A0_NC
        {
            get { return (w + bbot - 2 * cbot) * (tbot / 2 + D + ttop / 2) / 2.0; }
        }

        public double T1
        {
            get; set;
        }

        public double T2
        {
            get; set;
        }

        public double T3
        {
            get; set;
        }

        public double fv_NC
        {
            get { return 1.25 * (T1 + T2 + T3) / 2 / A0_NC / (Flexure == "Positive" ? ttop : tbot) * 1000000; }
        }

        // Calculating Delta for Cons and ULS limit state
        public double Delta
        {
            get { return Math.Sqrt(1 - fv_NC * fv_NC / Fytop / Fytop); }
        }


        //2.2.2 Calcualtion Fnc for Box flange
        //Table 6
        public double Hsb
        {
            get; set;
        }
        public double tsb
        {
            get; set;
        }
        public double nsb
        {
            get; set;
        }
        public double Hst
        {
            get; set;
        }
        public double tst
        {
            get; set;
        }
        public double nst
        {
            get; set;
        }

        public double Irib
        {
            get { return Flexure == "Positive" ? (tst * Math.Pow(Hst, 3) / 3.0) : (tsb * Math.Pow(Hsb, 3) / 3.0); }
        }

        public double wrib
        {
            get
            {
                double wrib;
                if (Flexure == "Positive")
                    wrib = nst == 0 ? 0 : ((w - nst * tst) / (nst + 1));
                else
                    wrib = nsb == 0 ? 0 : ((bbot - 2 * cbot - nsb * tsb) / (nsb + 1));
                return wrib;
            }
        }

        public double tfc
        {
            get { return Flexure == "Positive" ? ttop : tbot; }
        }

        public double bfc
        {
            get { return Flexure == "Positive" ? btop : bbot; }
        }

        public double nrib
        {
            get { return Flexure == "Positive" ? nst : nsb; }
        }


        // k = plate - buckling coefficient for uniform normal stress
        public double k_plate
        {
            get
            {
                double k;
                if (nrib == 0)
                    k = 4.0;
                else if (nrib == 1)
                    k = Math.Min(Math.Max(Math.Pow(8 * Irib / (wrib * tfc * tfc * tfc), 1 / 3.0), 1.0), 4.0);
                else
                    k = Math.Min(Math.Max(Math.Pow(0.894 * Irib / (wrib * tfc * tfc * tfc), 1 / 3.0), 1.0), 4.0);

                return k;
            }
        }

        //ks = plate - buckling coefficient for shear stress
        public double ks
        {
            get
            {
                double ks;
                if (nrib == 0)
                    ks = 5.34;
                else
                    ks = Math.Min((5.34 + 2.84 * Math.Pow(Irib / wrib / tfc / tfc / tfc, 1.0 / 3.0)) / Math.Pow((nrib + 1), 2), 5.34);
                return ks;
            }
        }

        //Table 7

        public double xf2
        {
            get { return wrib == 0 ? (bfc / tfc) : (wrib / tfc); }
        }

        public double xr
        {
            get { return 0.95 * Math.Sqrt(Flange.Es * k_plate / (Delta - 0.3) / Fytop); }
        }

        public double xp
        {
            get { return 0.57 * Math.Sqrt(Flange.Es * k_plate / Fytop / Delta); }
        }
        public double Fcb
        {
            get
            {
                if (xf2 <= xp)
                    return Rh * Fytop * Delta;
                else if (xf2 <= xr)
                    return Rh * Fytop * (Delta - (Delta - (Delta - 0.3) / Rh) * ((xf2 - xp) / (xr - xp)));
                else
                    return 0.9 * Flange.Es * k_plate / xf2 / xf2;
            }
        }

        public double Fcv
        {
            get
            {
                if (xf2 <= 1.12 * Math.Sqrt(Flange.Es * ks / Fytop))
                    return 0.58 * Fytop;
                else if (xf2 <= 1.40 * Math.Sqrt(Flange.Es * ks / Fytop))
                    return 0.65 * Math.Sqrt(Flange.Es * Fytop * ks) / xf2;
                else
                    return 0.9 * Flange.Es * ks / xf2 / xf2;
            }
        }


        public double Fnc_BF
        {
            get { return Fcb * Math.Sqrt(1 - (fv_NC / Fcv) * (fv_NC / Fcv)); }
        }


        // Determine Fnc and Fnt
        
        public double Fnc
        {
            get { return Flexure == "Positive" ? (ntop == 2 ? Fnc_OF : Fnc_BF) : Fnc_BF; }
        }

        public double Fyt
        {
            get { return Flexure == "Positive" ? Fybot : Fytop; }
        }

        public double RhFyt
        {
            get { return Rh * Fyt; }
        }

        public double RhFytDelta
        {
            get { return Rh * Fyt * Delta; }
        }

        public double Fnt
        {
            get { return BFOFten == "BF" ? RhFytDelta : RhFyt; }
        }

        //Table 8


        //2.3. Classify the slender web or compact/non compact
        //Table 10;
        public double ns
        { get; set; }


        public double Dc
        {
            get { return Flexure == "Positive" ? YU1 - ttop : YL2s - tbot; }

        }

        public string Slender
        {
            get { return 2 * Dc / Math.Cos(Math.Atan(S)) / tw <= 5.7 * Math.Sqrt(Flange.Es / Fytop) ? "(Non)Compact Web" : "Slender Web"; }
        }

        //3. Checking web Bend-Buckling Resistance for slender web

        // k = bend_buckling coefficient

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
                double Sc_com = Math.Abs(Flexure == "Positive" ? Sc_top : Sc_bot);
                double Sc_ten = Flexure == "Positive" ? Sc_bot : Sc_top;

                if (Sc_com == 0)
                    return 0;
                else
                    return -Sc_ten / Sc_com;
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
            get { return Math.Min(0.9 * Flange.Es * k_bend * tw * tw / Hw / Hw, Math.Min(Rh * Fytop, Fyweb / 0.7)); }

        }

        public string Check_buckling_top
        {
            get
            {
                if (Slender != "Slender Web" || Sc_top >= 0)
                    return "-";
                else
                    return -Sc_top <= Fcrw ? "OK" : "NG";
               
            }
        }

        public double Check_buckling_top_ratio
        {
            get
            {
                if (Slender != "Slender Web" || Sc_top >= 0)
                    return 1000000;
                else
                    return -Fcrw / Sc_top;
                
            }
        }

        public string Check_buckling_bot
        {
            get
            {
                if (Slender != "Slender Web" || Sc_bot >= 0)
                    return "-";
                else
                    return -Sc_bot <= Fcrw ? "OK" : "NG";
                
            }
        }

        public double Check_buckling_bot_ratio
        {
            get
            {
                if (Slender != "Slender Web" || Sc_bot >= 0)
                    return 1000000;
                else
                    return -Fcrw / Sc_bot;
               
            }
        }

        //Table 8

        //2.4. Checking Top flange stress

        //fbu +fl ≤ ΦfRhFyc


        public double fbufl_com
        {
            get
            {
                return Sc_top >= 0 ? 0 : -Sc_top + Math.Abs(fl);
            }
        }

        public double RhFyc
        {
            get { return Rh * Fytop; }
        }

        public string Check_Fyc_top
        {
            get
            {
                if (fbufl_com == 0 || ( fl == 0 && Slender == "Slender Web"))
                    return "-";
                else
                    return fbufl_com <= RhFyc ? "OK" : "NG";
            }
        }

        public double Check_Fyc_top_ratio
        {
            get
            {
                if (fbufl_com == 0 || (fl == 0 && Slender == "Slender Web"))
                    return 1000000;
                else
                    return RhFyc / fbufl_com ;
            }
        }

        //fbu + fl/3 ≤ ΦfFnc
        public double fbufl3_com
        {
            get
            {
                return Sc_top >= 0 ? 0 : -Sc_top + Math.Abs(fl) / 3;
            }
        }
               

        public string Check_Fnc_top
        {
            get
            {
                if (fbufl3_com == 0)
                    return "-";
                else
                    return fbufl3_com <= Fnc ? "OK" : "NG";
            }
        }

        public double Check_Fnc_top_ratio
        {
            get
            {
                if (fbufl3_com == 0)
                    return 1000000;
                else
                    return Fnc / fbufl3_com;
            }
        }
        //fbu + fl ≤ ΦfFnt

        public double fbufl_ten
        {
            get
            {
                return Sc_top >= 0 ? Sc_top + Math.Abs(fl) : 0;
            }
        }
               

        public string Check_Fnt_top
        {
            get
            {
                if (fbufl_ten == 0)
                    return "-";
                else
                    return fbufl_ten <= Fnt ? "OK" : "NG";
            }
        }

        public double Check_Fnt_top_ratio
        {
            get
            {
                if (fbufl_ten == 0)
                    return 1000000;
                else
                    return Fnt / fbufl_ten;
            }
        }

        //2.4. Checking bottom flange stress


        //fbu ≤ ΦfFnc
        public double fbu_com
        {
            get
            {
                return Sc_bot >= 0 ? 0 : -Sc_bot;
            }
        }
       
        public string Check_Fnc_bot
        {
            get
            {
                if (fbu_com == 0)
                    return "-";
                else
                    return fbu_com <= Fnc ? "OK" : "NG";
            }
        }

        public double Check_Fnc_bot_ratio
        {
            get
            {
                if (fbu_com == 0)
                    return 1000000;
                else
                    return Fnc / fbu_com ;
            }
        }

        //fbu ≤ ΦfFnt

        public double fbu_ten
        {
            get
            {
                return Sc_bot >= 0 ? Sc_bot : 0;
            }
        }       

        public string Check_Fnt_bot
        {
            get
            {
                if (fbu_ten == 0)
                    return "-";
                else
                    return fbu_ten <= Fnt ? "OK" : "NG";
            }
        }

        public double Check_Fnt_bot_ratio
        {
            get
            {
                if (fbu_ten == 0)
                    return 1000000;
                else
                    return Fnt / fbu_ten ;
            }
        }

        //Table 11
        //3. Checking Shear
        public double S1
        { get; set; }
        public double S2
        { get; set; }
        public double S3
        { get; set; }


        public double Vu
        {
            get { return (S1 + S2 + S3) * 1.25 / 2.0; }
        }

        public double cosp
        {
            get { return Math.Cos(Math.Atan(S)); }
        }

        public double Vui
        {
            get { return Vu / Math.Cos(S); }
        }
        // k = shear - buckling coefficient

        public double d0
        {
            get; set;
        }

        public string Stiffened
        {
            get
            {
                if ((d0 <= 3 * D && ns == 0) || (d0 <= 1.5 * D && ns != 0))
                    return "Stiffened";
                else
                    return "Unstiffened";
            }
        }
               

        public double Hw
        {
            get; set;
        }

        public double k_shear
        {
            get { return Stiffened == "Stiffened" ? (5 + 5 / (d0 / D) / (d0 / D)) : 5.0; }
        }

        public double C
        {
            get
            {
                double C1_Cons;
                C1_Cons = Math.Sqrt(Web.Es * k_shear / Web.Fys(tw));
                if (Hw / tw <= 1.12 * C1_Cons)
                    return 1.0;
                else if (Hw / tw <= 1.40 * C1_Cons)
                    return 1.12 / (Hw / tw) * C1_Cons;
                else
                    return 1.57 * C1_Cons * C1_Cons / (Hw / tw) / (Hw / tw);
            }
        }

        public double Vp
        {
            get { return 0.58 * Web.Fys(tw) * Hw * tw / 1000; }
        }

        public double Vn
        {
            get { return C * Vp; }
        }

        public string Check_shear
        {
            get { return Math.Abs(Vui) <= Vn ? "OK" : "NG"; }
        }

        public double Check_shear_ratio
        {
            get { return Vui == 0 ? 1000000 : Math.Round(Vn / Math.Abs(Vui), 2); }
        }


    }
}
