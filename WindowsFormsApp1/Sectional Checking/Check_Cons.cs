using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checking
{
    public class Check_Cons
    {
        private string _Label, _Flexure, Flange, Web;
        private double R, ntop, btop, ttop, bbot, tbot, cbot, D, tw, b, w, ts, nst, Hst, tst, nsb, Hsb, tsb, Lb, ns, d0, A1, Ac, As, Ah, S,Hw,
            M1, M2, M3, S1, S2, S3, T1, T2, T3,
            YU1, YL1, YU2s, YL2s, _Sc_top, _Sc_bot, _Sta;
        public Check_Cons(string Label, string Flexure, double R, double ntop, double btop, double ttop, double bbot, double tbot, double cbot, double D, double tw, double b, double w, double ts,
            double nst, double Hst, double tst, double nsb, double Hsb, double tsb, double Lb, double ns, double d0, double A1, double Ac, double As, double Ah, double S, double Hw,
            double M1, double M2, double M3, double S1, double S2, double S3, double T1, double T2, double T3,
            double YU1, double YL1, double YU2s, double YL2s, double Sc_top, double Sc_bot, string Flange, string Web, double Sta)
        {
            this._Label = Label;
            this._Flexure = Flexure;
            this.R = R;
            this.ntop = ntop;
            this.btop = btop;
            this.ttop = ttop;
            this.bbot = bbot;
            this.tbot = tbot;
            this.cbot = cbot;
            this.D = D;
            this.tw = tw;
            this.b = b;
            this.w = w;
            this.ts = ts;
            this.nst = nst;
            this.Hst = Hst;
            this.tst = tst;
            this.nsb = nsb;
            this.Hsb = Hsb;
            this.tsb = tsb;
            this.Lb = Lb;
            this.ns = ns;
            this.d0 = d0;
            this.A1 = A1;
            this.Ac = Ac;
            this.As = As;
            this.Ah = Ah;
            this.S = S;
            this.Hw = Hw;
            this.M1 = M1;
            this.M2 = M2;
            this.M3 = M3;
            this.S1 = S1;
            this.S2 = S2;
            this.S3 = S3;
            this.T1 = T1;
            this.T2 = T2;
            this.T3 = T3;
            this.YU1 = YU1;
            this.YL1 = YL1;
            this.YU2s = YU2s;
            this.YL2s = YL2s;
            this._Sc_top = Sc_top;
            this._Sc_bot = Sc_bot;
            this.Flange = Flange;
            this.Web = Web;
            this._Sta = Sta;
        }

        //Checking constructibility 
        //1. Calculation and check of flange lateral bending stress fl <= 0.6Fy
        // fl1, rt, Fcr, Lp, fl, CheckC_fl 

        public string Label
        {
            get { return _Label; }

        }

        public double Sta
        {
            get { return _Sta; }

        }
        public string Flexure
        {
            get { return _Flexure; }

        }

        public double Sc_top
        {
            get { return _Sc_top; }

        }

        public double Sc_bot
        {
            get { return _Sc_bot; }

        }
        public double DeltaV
        {
            get { return (A1 * Material.Ys + (Ac + As + Ah) * Material.Yc) / 1000000 / 2; }
        }

        public double Mlw
        {
            get { return DeltaV * S * Lb * Lb / 12 / 1000000; }
        }

        public double tana
        {
            get { return (D / (D * S + b)); }
        }

        public double Fcon
        {
            get { return b * ts * Material.Yc; }
        }
        public double Mlo
        {
            get { return 0.5 * Fcon / tana * Lb * Lb / 12 / Math.Pow(10, 12); }
        }

        public double Mlf
        {
            get { return 0.5 * Material.forms / (D / (D * S + b)) * Lb * Lb / 12 / 1000000; }
        }

        public double Mlc
        {
            get { return R > 0 ? (M1 + M2 + M3) * Lb * Lb / 12 / R / D : 0; }
        }

        public double Sl
        {
            get { return ttop * btop * btop / 6.0; }
        }
        public double fl1
        {
            get { return ntop == 1 ? 0 : ((1.25 * Mlw + 1.25 * Mlo + 1.50 * Mlf + 1.50 * Mlc) / Sl * 1000000); }
        }

        public double rt
        {
            get
            {
                double Dc;
                Dc = (Flexure == "Positive" ? YU1 - ttop : YL1 - tbot) / Math.Cos(Math.Atan(S));
                return btop / Math.Pow(12 * (1 + Dc * tw / 3 / btop / ttop), 0.5);
            }
        }

        public double Fcr
        {
            get { return Math.PI * Math.PI * Material.Es / (Lb / rt) / (Lb / rt); }
        }

        public double Lp
        {
            get { return 1.0 * rt * Math.Sqrt(Material.Es / Material.Fy(Flange,ttop)); }
        }

        public double fl
        {
            get { return Flexure == "Positive" ? (Lb <= 1.2 * Lp * Math.Pow(Material.Fy(Flange, ttop) / Math.Abs(Sc_top), 0.5) ? fl1 : (Math.Max(0.85 / (1 - Math.Abs(Sc_top) / Fcr), 1) * fl1)) : fl1; }
        }

        public string Check_fl
        {
            get { return fl <= 0.6 * Material.Fy(Flange, ttop) ? "OK" : "NG"; }
        }

        public string Check_fl_ratio
        {
            get { return fl == 0 ? "Inf" : Math.Round((0.6 * Material.Fy(Flange, ttop) / Math.Abs(fl)),2).ToString(); }
        }

        public double fy06
        {
            get { return 0.6 * Material.Fy(Flange, ttop); }
        }
        //2. Checking flange stress ----------------------------------------------------------
        // Rh, A0_NC, fv, Delta, Fnc_LB, Fnc_LTB, Fnc_OF, tfc, bfc, wrib, nrib, Irib, k, ks, Fcb, Fcv, Fnc_BF, Fnc_Cons, Sc_com, Sc_ten, Dc, k1, Slender, flcom, 
        //fbufl_com, fubfl3_com, fubfl_ten, CheckC_comOF, CheckC_com, Ten_resis, CheckC_ten, Fcrw, CheckC_buckling 

        //2.1. Calculating Rh for Constructibility and ULS


        public double YU
        {
            get { return Flexure == "Positive" ? YU1 : YU2s; }
        }

        public double YL
        {
            get { return Flexure == "Positive" ? YL1 : YL2s; }
        }

        public double Dn
        {
            get { return Math.Max(YU - ttop, YL - tbot); }
        }

        public double Afn
        {
            get { return YU - ttop >= YL - tbot ? ntop * btop * ttop : bbot * tbot; }
        }

        public double fn
        {
            get { return Math.Max(Material.Fy(Flange, ttop), Math.Max(Math.Abs(Sc_top), Math.Abs(Sc_bot))); }
        }

        public double rho
        {
            get { return Math.Min(Material.Fy(Web, tw) / fn, 1.0); }
        }

        public double beta
        {
            get { return 2 * Dn * tw / Afn; }
        }

        public double Rh
        {
            get { return Material.Fy(Web, tw) >= Material.Fy(Flange, ttop) ? 1.0 : (12 + beta * (3 * rho - rho * rho * rho)) / (12 + 2 * beta); }

        }

        //2.1. Calculating Delta

        public double A0_NC
        {
            get { return (w + bbot - 2 * cbot) * (tbot / 2 + D + ttop / 2) / 2.0; }
        }

        public double fv_NC
        {
            get { return 1.25 * (T1 + T2 + T3) / 2 / A0_NC / (Flexure == "Positive" ? ttop : tbot) * 1000000; }
        }

        // Calculating Delta for Cons and ULS limit state
        public double Delta
        {
            get { return Math.Sqrt(1 - fv_NC * fv_NC / Material.Fy(Flange, ttop) / Material.Fy(Flange, ttop)); }
        }

        //2.2. Calcualtion Fnc
        //2.2.1 Calcualtion Fnc for Open flange

        public string BFOFcom
        {
            get { return Flexure == "Positive" ? (ntop == 2 ? "OF" : "BF") : "BF"; }
        }

        public double xf1
        {
            get { return btop / 2 / ttop; }
        }

        public double xpf
        {
            get { return 0.38 * Math.Sqrt(Material.Es / Material.Fy(Flange, ttop)); }
        }

        public double Fyr
        {
            get { return Math.Max(Math.Min(0.7 * Material.Fy(Flange, ttop), Material.Fy(Web, tw)), 0.5 * Material.Fy(Flange, ttop)); }
        }

        public double xrf
        {
            get { return 0.56 * Math.Sqrt(Material.Es / Fyr); }
        }
        public double Fnc_LB
        {
            get
            {
                if (BFOFcom == "OF")
                {
                    if (xf1 <= xpf)
                        return 1.0 * Rh * Material.Fy(Flange, ttop);
                    else
                        return (1 - (1 - Fyr / Rh / Material.Fy(Flange, ttop)) * ((xf1 - xpf) / (xrf - xpf))) * Rh * Material.Fy(Flange, ttop);
                }
                else
                    return 0;
            }
        }

        public double Lr
        {
            get { return Math.PI * rt * Math.Sqrt(Material.Es / Fyr); }
        }


        public double Fnc_LTB
        {
            get
            {
                if (BFOFcom == "OF")
                {
                    if (Lb <= Lp)
                        return 1.0 * Rh * Material.Fy(Flange, ttop);
                    else if (Lb <= Lr)
                        return Math.Min(1 - (1 - Fyr / Rh / Material.Fy(Flange, ttop)) * ((Lb - Lp) / (Lr - Lp)), 1) * Rh * Material.Fy(Flange, ttop);
                    else
                        return Math.Min(Fcr, Rh * Material.Fy(Flange, ttop));
                }
                else
                    return 0;
            }
        }

        public double Fnc_OF
        {
            get { return Math.Min(Fnc_LB, Fnc_LTB); }
        }

        //2.2.2 Calcualtion Fnc for Box flange

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

        public double wrib
        {
            get
            {
                double wrib;
                if (Flexure == "Positive")
                    wrib = nst == 0 ? 0 : ((w - nst * tst) / nst);
                else
                    wrib = nsb == 0 ? 0 : ((bbot - 2 * cbot - nsb * tsb) / nsb);
                return wrib;
            }
        }

        public double Irib
        {
            get { return Flexure == "Positive" ? (tst * Math.Pow(Hst, 3) / 3.0) : (tsb * Math.Pow(Hsb, 3) / 3.0); }
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

        public double xf2
        {
            get { return wrib == 0 ? (bfc / tfc) : (wrib / tfc); }
        }

        public double xr
        {
            get { return 0.95 * Math.Sqrt(Material.Es * k_plate / (Delta - 0.3) / Material.Fy(Flange, ttop)); }
        }

        public double xp
        {
            get { return 0.57 * Math.Sqrt(Material.Es * k_plate / Material.Fy(Flange, ttop) / Delta); }
        }
        public double Fcb
        {
            get
            {
                if (xf2 <= xp)
                    return Rh * Material.Fy(Flange, ttop) * Delta;
                else if (xf2 <= xr)
                    return Rh * Material.Fy(Flange, ttop) * (Delta - (Delta - (Delta - 0.3) / Rh) * ((xf2 - xp) / (xr - xp)));
                else
                    return 0.9 * Material.Es * k_plate / xf2 / xf2;
            }
        }

        public double Fcv
        {
            get
            {
                if (xf2 <= 1.12 * Math.Sqrt(Material.Es * ks / Material.Fy(Flange, ttop)))
                    return 0.58 * Material.Fy(Flange, ttop);
                else if (xf2 <= 1.40 * Math.Sqrt(Material.Es * ks / Material.Fy(Flange, ttop)))
                    return 0.65 * Math.Sqrt(Material.Es * Material.Fy(Flange, ttop) * ks) / xf2;
                else
                    return 0.9 * Material.Es * ks / xf2 / xf2;
            }
        }


        public double Fnc_BF
        {            
            get
            {
                if (BFOFcom == "OF")
                    return 0;
                else
                return Fcb * Math.Sqrt(1 - (fv_NC / Fcv) * (fv_NC / Fcv));
            }
        }

        // Return BF or OF for compression flange
        

        // Return BF or OF for tension flange
        public string BFOFten
        {
            get { return Flexure == "Positive" ? "BF" : (ntop == 2 ? "OF" : "BF"); }
        }

        // Fnc for Cons and ULS
        public double Fnc
        {
            get { return BFOFcom == "OF" ? Fnc_OF : Fnc_BF; }
        }

        //2.3. Classify the slender web or compact/non compact

        public double Sc_com
        {
            get { return Math.Abs(Flexure == "Positive" ? Sc_top : Sc_bot); }
        }

        public double Sc_ten
        {
            get { return Flexure == "Positive" ? Sc_bot : Sc_top; }
        }


        public double Dc
        {
            get { return Flexure == "Positive" ? YU1 - ttop : YL2s - tbot; }

        }


        public string Slender
        {
            get { return 2 * Dc / Math.Cos(Math.Atan(S)) / tw <= 5.7 * Math.Sqrt(Material.Es / Material.Fy(Flange, ttop)) ? "(Non)Compact Web" : "Slender Web"; }
        }

        //2.4. Checking compression stress

        //fbu +fl ≤ ΦfRhFyc
        // fl for compression flange
        public double flcom
        {
            get { return Flexure == "Positive" ? Math.Abs(fl) : 0; }
        }
        // fl for tension flange
        public double flten
        {
            get { return Flexure == "Positive" ? 0 : Math.Abs(fl); }
        }

        // fbu + fl (compression)
        public double fbufl_com
        {
            get { return Sc_com + flcom; }
        }
        public string Check_comOF
        {
            get
            {
                if (flcom == 0 && Slender == "Slender Web")
                    return "-";
                else
                    return Sc_com + flcom <= Rh * Material.Fy(Flange, ttop) ? "OK" : "NG";
            }
        }

        public string Check_comOF_ratio
        {
            get
            {
                if (flcom == 0 && Slender == "Slender Web")
                    return "-";
                else if (Sc_com + flcom == 0)
                    return "Inf";
                else
                    return Math.Round((Rh * Material.Fy(Flange, ttop) / (Sc_com + flcom)),2).ToString();
            }
        }


        //fbu +fl/3 ≤ ΦfFnc and fbu  ≤ ΦfFnc

        // fbu + fl/3 (compression)
        public double fbufl3_com
        {
            get { return Sc_com + flcom / 3.0; }
        }
        public string Check_com
        {
            get { return Sc_com + flcom / 3.0 <= Fnc ? "OK" : "NG"; }
        }

        public string Check_com_ratio
        {
            get { return Sc_com + flcom / 3.0 == 0 ? "Inf" : Math.Round((Fnc / (Sc_com + flcom / 3.0)),2).ToString(); }
        }

        public double Fnt
        {
            get
            {
                if (Flexure == "Positive")
                    return Rh * Material.Fy(Flange, tbot) * Delta;
                else
                {
                    if (ntop == 2)
                        return Rh * Material.Fy(Flange, ttop);
                    else
                        return Rh * Material.Fy(Flange, ttop) * Delta;
                }
            }
        }

        // fbu + fl (tension)
        public double fbufl_ten
        {
            get { return Sc_ten + flten; }
        }
        // fbu + fl ≤ ΦfRhFyt and fbu ≤ ΦfRhFyfΔ
        public string Check_ten
        {
            get { return Sc_ten + flten <= Fnt ? "OK" : "NG"; }
        }

        public string Check_ten_ratio
        {
            get { return Sc_ten + flten == 0 ? "Inf" : Math.Round((Fnt / (Sc_ten + flten)),2).ToString(); }
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
            get { return Math.Min(0.9 * Material.Es * k_bend * tw * tw / Hw / Hw, Math.Min(Rh * Material.Fy(Flange, ttop), Material.Fy(Web, tw) / 0.7)); }
        }

        public string Check_buckling
        {
            get
            {
                if (Slender == "Slender Web")
                    return Sc_com <= Fcrw ? "OK" : "NG";
                else
                    return "-";
            }
        }

        public string Check_buckling_ratio
        {
            get
            {
                if (Slender == "Slender Web")
                    return Sc_com == 0 ? "Inf" : (Fcrw / Sc_com).ToString();
                else
                    return "-";
            }
        }


        //3. Checking Shear
        public double Vu
        {
            get { return (S1 + S2 + S3) * 1.25 / 2.0; }            
        }

        public double Vui
        {
            get { return Vu/ Math.Cos(Math.Atan(S)); }
        }
        // k = shear - buckling coefficient

        public double k_shear
        {
            get { return 5 + 5 / (d0 / D) / (d0 / D); }           
        }

        public double C
        {
            get
            {
                double C1_Cons;
            C1_Cons = Math.Sqrt(Material.Es * k_shear / Material.Fy(Web, tw));
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
            get { return 0.58 * Material.Fy(Web, tw) * Hw * tw / 1000; }
        }

        public double Vn
        {
            get { return C * Vp; }
        }

        public string Check_shear
        {            
            get {return  Math.Abs(Vui) <= Vn ? "OK" : "NG"; }          
        }

        public string Check_shear_ratio
        {
            get { return Vui == 0 ? "Inf" : (Vn / Math.Abs(Vui)).ToString(); }            
        }


        //End of Checking Constructibility ^__^

    }
}
