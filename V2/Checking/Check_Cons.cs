using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public static class Check_Cons
    {
        //Checking constructibility 
        //1. Calculation and check of flange lateral bending stress fl <= 0.6Fy
        // fl1, rt, Fcr, Lp, fl, CheckC_fl 

      
        public static double DeltaV(this Node n)
        {
           return (n.A1() * Material.Ys + (n.Ac() + n.As() + n.Ah()) * Material.Yc) / 1000000 / 2;
        }

        public static double Mlw(this Node n)
        {
            return n.DeltaV() * n.S() * n.Lb * n.Lb / 12 / 1000000;
        }

        public static double tana(this Node n)
        {
            return (n.D / (n.D * n.S() + n.b));
        }

        public static double Fcon(this Node n)
        {
            return n.b * n.ts * Material.Yc;
        }
        public static double Mlo(this Node n)
        {
            return 0.5 *  n.Fcon()/ n.tana()* n.Lb * n.Lb / 12 / Math.Pow(10, 12);
        }

        public static double Mlf(this Node n)
        {
            return 0.5 * Material.forms / (n.D / (n.D * n.S() + n.b)) * n.Lb * n.Lb / 12 / 1000000;
        }

        public static double Mlc(this Node n)
        {
            return n.R > 0 ? (n.M1 + n.M2 + n.M3) * n.Lb * n.Lb / 12 / n.R / n.D : 0;
        }

        public static double Sl(this Node n)
        {
            return n.ttop * n.btop * n.btop / 6.0;
        }
        public static double fl1(this Node n)
        {
           return n.ntop == 1 ? 0 : ((1.25 * n.Mlw() + 1.25 * n.Mlo() + 1.50 * n.Mlf() + 1.50 * n.Mlc()) / n.Sl() * 1000000);
        }

        public static double rt(this Node n)
        {
            double Dc;
            Dc = (n.Flexure() == "Positive" ? n.YU1() - n.ttop : n.YL1() - n.tbot) / Math.Cos(Math.Atan(n.S()));
            return n.btop / Math.Pow(12 * (1 + Dc * n.tw / 3 / n.btop / n.ttop), 0.5);
        }

        public static double Fcr(this Node n)
        {
            return Math.PI * Math.PI * Material.Es / (n.Lb / n.rt()) / (n.Lb / n.rt());
        }

        public static double Lp(this Node n)
        {
            return 1.0 * n.rt() * Math.Sqrt(Material.Es / Material.Fyf);
        }

        public static double fl(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.Lb <= 1.2 * n.Lp() * Math.Pow(Material.Fyf / Math.Abs(n.Sc_top()), 0.5) ? n.fl1() : (Math.Max(0.85/(1-Math.Abs(n.Sc_top())/n.Fcr()),1)*n.fl1())) : n.fl1();
        }

        public static string CheckC_fl(this Node n)
        {
            return n.fl() <= 0.6 * Material.Fyf ? "OK" : "NG";
        }

        public static string CheckC_fl_ratio(this Node n)
        {
            return n.fl() == 0 ? "Inf" : (0.6 * Material.Fyf / Math.Abs(n.fl())).ToString();
        }

        //2. Checking flange stress ----------------------------------------------------------
        // Rh, A0_NC, fv, Delta, Fnc_LB, Fnc_LTB, Fnc_OF, tfc, bfc, wrib, nrib, Irib, k, ks, Fcb, Fcv, Fnc_BF, Fnc_Cons, Sc_com, Sc_ten, Dc, k1, Slender, flcom, 
        //fbufl_com, fubfl3_com, fubfl_ten, CheckC_comOF, CheckC_com, Ten_resis, CheckC_ten, Fcrw, CheckC_buckling 

        //2.1. Calculating Rh for Constructibility and ULS


        public static Tuple <double, double> YU(this Node n)
        {
            double YU_Cons, YU_ULS;
            YU_Cons=  n.Flexure() == "Positive" ? n.YU1() : n.YU2s();
            YU_ULS = n.Flexure() == "Positive" ? n.YU3s() : n.YU4s();
            return Tuple.Create(YU_Cons, YU_ULS);
        }

        public static Tuple<double, double> YL(this Node n)
        {
            double YL_Cons, YL_ULS;
            YL_Cons = n.Flexure() == "Positive" ? n.YL1() : n.YL2s();
            YL_ULS = n.Flexure() == "Positive" ? n.YL3s() : n.YL4s();
            return Tuple.Create(YL_Cons, YL_ULS);            
        }

        public static Tuple<double, double> Dn(this Node n)
        {
            double Dn_Cons, Dn_ULS;
            Dn_Cons = Math.Max(n.YU().Item1 - n.ttop, n.YL().Item1 - n.tbot);
            Dn_ULS = Math.Max(n.YU().Item2 - n.ttop, n.YL().Item2 - n.tbot);
            return Tuple.Create(Dn_Cons, Dn_ULS);
        }

        public static Tuple<double, double> Afn(this Node n)
        {
            double Afn_Cons, Afn_ULS;
            Afn_Cons = n.YU().Item1 - n.ttop >= n.YL().Item1 - n.tbot ? n.ntop * n.btop * n.ttop : n.bbot * n.tbot;
            Afn_ULS = n.YU().Item2 - n.ttop >= n.YL().Item2 - n.tbot ? n.ntop * n.btop * n.ttop : n.bbot * n.tbot;
            return Tuple.Create(Afn_Cons, Afn_ULS);            
        }

        public static Tuple<double, double> fn(this Node n)
        {
            double fn_Cons, fn_ULS;
            fn_Cons = Math.Max(Material.Fyf, Math.Max(Math.Abs(n.Sc_top()), Math.Abs(n.Sc_bot())));
            fn_ULS = Math.Max(Material.Fyf, Math.Max(Math.Abs(n.Su_top()), Math.Abs(n.Su_bot())));
            return Tuple.Create(fn_Cons, fn_ULS);
        }

        public static Tuple<double, double> rho(this Node n)
        {
            double rho_Cons, rho_ULS;
            rho_Cons = Math.Min(Material.Fyw / n.fn().Item1, 1.0);
            rho_ULS = Math.Min(Material.Fyw / n.fn().Item2, 1.0);
            return Tuple.Create(rho_Cons, rho_ULS);
        }

        public static Tuple<double, double> beta(this Node n)
        {
            double beta_Cons, beta_ULS;
            beta_Cons = 2 * n.Dn().Item1 * n.tw / n.Afn().Item1;
            beta_ULS = 2 * n.Dn().Item2 * n.tw / n.Afn().Item2;
            return Tuple.Create(beta_Cons, beta_ULS);            
        }

        public static Tuple<double, double> Rh(this Node n)
        {
            double Rh_Cons, Rh_ULS;
            Rh_Cons = Material.Fyw >= Material.Fyf ? 1.0 : (12 + n.beta().Item1 * (3 * n.rho().Item1 - n.rho().Item1 * n.rho().Item1 * n.rho().Item1)) / (12 + 2 * n.beta().Item1);
            Rh_ULS = Material.Fyw >= Material.Fyf ? 1.0 : (12 + n.beta().Item2 * (3 * n.rho().Item2 - n.rho().Item2 * n.rho().Item2 * n.rho().Item2)) / (12 + 2 * n.beta().Item2);
            return Tuple.Create(Rh_Cons, Rh_ULS);            
        }

        //2.1. Calculating Delta

        public static double A0_NC(this Node n)
        {
            return (n.w + n.bbot - 2 * n.cbot) * (n.tbot/2 + n.D + n.ttop/2) / 2.0;
        }
         
        public static double fv_NC(this Node n)
        {            
            return 1.25 * (n.T1 + n.T2 + n.T3) / 2 / n.A0_NC() / (n.Flexure() == "Positive" ? n.ttop : n.tbot) * 1000000;
        }

        // Calculating Delta for Cons and ULS limit state
        public static Tuple<double, double> Delta(this Node n)
        {
            double Delta_Cons, Delta_ULS;
            Delta_Cons= Math.Sqrt(1 - n.fv_NC()*n.fv_NC() / Material.Fyf / Material.Fyf);
            Delta_ULS = Math.Sqrt(1 - n.fv() * n.fv() / Material.Fyf / Material.Fyf);
            return Tuple.Create(Delta_Cons, Delta_Cons);
        }

        //2.2. Calcualtion Fnc
        //2.2.1 Calcualtion Fnc for Open flange

        public static double xf1(this Node n)
        {
            return n.btop / 2 / n.ttop;
        }

        public static double xpf(this Node n)
        {
            return 0.38 * Math.Sqrt(Material.Es / Material.Fyf);
        }

        public static double xrf(this Node n)
        {
            return 0.56 * Math.Sqrt(Material.Es / Material.Fyr());
        }
        public static double Fnc_LB(this Node n)
        {            
            if (n.xf1() <= n.xpf())
                return 1.0 * n.Rh().Item1 * Material.Fyf;
            else
                return (1 - (1 - Material.Fyr()/n.Rh().Item1/Material.Fyf)*((n.xf1() - n.xpf())/(n.xrf() - n.xpf()))) * n.Rh().Item1 * Material.Fyf;            
        }

        public static double Lr(this Node n)
        {
            return Math.PI * n.rt() * Math.Sqrt(Material.Es / Material.Fyr());
        }


        public static double Fnc_LTB(this Node n)
        {                   
            if (n.Lb <= n.Lp())
               return 1.0 * n.Rh().Item1 * Material.Fyf;
            else if (n.Lb <= n.Lr())
               return Math.Min(1 - (1 - Material.Fyr() / n.Rh().Item1 / Material.Fyf) * ((n.Lb - n.Lp()) / (n.Lr() - n.Lp())), 1) * n.Rh().Item1 * Material.Fyf;
            else
                return Math.Min(n.Fcr(), n.Rh().Item1 * Material.Fyf);            
        }

        public static double Fnc_OF(this Node n)
        {
            return Math.Min(n.Fnc_LB(), n.Fnc_LTB());
        }

        //2.2.2 Calcualtion Fnc for Box flange

        public static double tfc(this Node n)
        {
            return n.Flexure() == "Positive" ? n.ttop : n.tbot;
        }

        public static double bfc(this Node n)
        {
            return n.Flexure() == "Positive" ? n.btop : n.bbot;
        }

        public static double nrib(this Node n)
        {
            return n.Flexure() == "Positive" ? n.nst : n.nsb;
        }

        public static double wrib(this Node n)
        {
            double wrib;
            if (n.Flexure() == "Positive")
                wrib = n.nst == 0 ? 0 : ((n.w - n.nst * n.tst) / n.nst);
            else
                wrib = n.nsb == 0 ? 0 : ((n.bbot - 2 * n.cbot - n.nsb * n.tsb) / n.nsb);
            return wrib;
        }

        public static double Irib(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.tst * Math.Pow(n.Hst, 3) / 3.0) : (n.tsb * Math.Pow(n.Hsb, 3) / 3.0);
        }

        // k = plate - buckling coefficient for uniform normal stress
        public static double k_plate(this Node n)
        {
            double k;  
            if (n.nrib() == 0)
                k = 4.0;
            else if (n.nrib() == 1)
                k = Math.Min(Math.Max(Math.Pow(8 * n.Irib() / (n.wrib() * n.tfc() * n.tfc() * n.tfc()),1/3.0), 1.0), 4.0);
            else
                k = Math.Min(Math.Max(Math.Pow(0.894 * n.Irib() / (n.wrib() * n.tfc() * n.tfc() * n.tfc()), 1 / 3.0), 1.0), 4.0);

            return k;
        }

        //ks = plate - buckling coefficient for shear stress
        public static double ks(this Node n)
        {
            double ks;
            if (n.nrib() == 0)
                ks = 5.34;
            else
               ks = Math.Min((5.34 + 2.84*Math.Pow(n.Irib()/n.wrib()/n.tfc()/n.tfc()/n.tfc(),1.0/3.0))/Math.Pow((n.nrib() +1),2), 5.34);  
            return ks;
        }

        public static double xf2(this Node n)
        {
           return n.wrib() == 0 ? (n.bfc() / n.tfc()) : (n.wrib() / n.tfc());
        }

        public static Tuple<double, double> xr(this Node n)
        {
            double xr_Cons, xr_ULS;
            xr_Cons = 0.95 * Math.Sqrt(Material.Es * n.k_plate() / (n.Delta().Item1 - 0.3) / Material.Fyf);
            xr_ULS = 0.95 * Math.Sqrt(Material.Es * n.k_plate() / (n.Delta().Item2 - 0.3) / Material.Fyf);
            return Tuple.Create(xr_Cons, xr_ULS);
        }

        public static Tuple<double, double> xp(this Node n)
        {
            double xp_Cons, xp_ULS;
            xp_Cons = 0.57 * Math.Sqrt(Material.Es * n.k_plate() / Material.Fyf / n.Delta().Item1);
            xp_ULS = 0.57 * Math.Sqrt(Material.Es * n.k_plate() / Material.Fyf / n.Delta().Item2);
            return Tuple.Create(xp_Cons, xp_ULS); 
        }
        public static Tuple<double, double> Fcb(this Node n)
        {
            double Fcb_Cons, Fcb_ULS;

            if (n.xf2() <= n.xp().Item1)            
                Fcb_Cons = n.Rh().Item1 * Material.Fyf * n.Delta().Item1;                                
            else if (n.xf2() <= n.xr().Item1)            
                Fcb_Cons = n.Rh().Item1 * Material.Fyf * (n.Delta().Item1 - (n.Delta().Item1 - (n.Delta().Item1 - 0.3) / n.Rh().Item1) * ((n.xf2() - n.xp().Item1) / (n.xr().Item1 - n.xp().Item1)));                             
            else            
                Fcb_Cons = 0.9 * Material.Es * n.k_plate() / n.xf2() / n.xf2();               
            
            if (n.xf2() <= n.xp().Item2)                
                Fcb_ULS = n.Rh().Item2 * Material.Fyf * n.Delta().Item2;            
            else if (n.xf2() <= n.xr().Item2)            
                Fcb_ULS = n.Rh().Item2 * Material.Fyf * (n.Delta().Item2 - (n.Delta().Item2 - (n.Delta().Item2 - 0.3) / n.Rh().Item2) * ((n.xf2() - n.xp().Item2) / (n.xr().Item2 - n.xp().Item2)));           
            else
                Fcb_ULS = 0.9 * Material.Es * n.k_plate() / n.xf2() / n.xf2();
            
            return Tuple.Create(Fcb_Cons, Fcb_ULS);
        }

        public static double Fcv(this Node n)
        {  
            if (n.xf2() <= 1.12 * Math.Sqrt(Material.Es * n.ks() / Material.Fyf))
                return 0.58 * Material.Fyf;
            else if (n.xf2() <= 1.40 * Math.Sqrt(Material.Es * n.ks() / Material.Fyf))
                return 0.65 * Math.Sqrt(Material.Es * Material.Fyf * n.ks()) / n.xf2();
            else
                return 0.9 * Material.Es * n.ks() / n.xf2() / n.xf2();           
        }




        public static double Fnc_BF(this Node n)
        {
            return n.Fcb().Item1 * Math.Sqrt( 1 - (n.fv_NC()/n.Fcv())*(n.fv_NC() / n.Fcv()));
        }

        // Return BF or OF for compression flange
        public static string BFOFcom(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.ntop == 2 ? "OF" : "BF") : "BF";
        }

        // Return BF or OF for tension flange
        public static string BFOFten(this Node n)
        {
            return n.Flexure() == "Positive" ? "BF" : (n.ntop == 2 ? "OF" : "BF");
        }

        // Fnc for Cons and ULS
        public static Tuple <double, double> Fnc(this Node n)
        {
            double Fnc_Cons, Fnc_ULS;

            Fnc_Cons = n.Flexure() == "Positive" ? (n.ntop == 2 ? n.Fnc_OF() : n.Fnc_BF()) : n.Fnc_BF();
            Fnc_ULS = n.Flexure() == "Positive" ? n.Fnc_Pos() : n.Fnc_Neg();

            return Tuple.Create(Fnc_Cons, Fnc_ULS);
        }

        //2.3. Classify the slender web or compact/non compact

        public static double Sc_com(this Node n)
        {
            return Math.Abs(n.Flexure() == "Positive" ? n.Sc_top() : n.Sc_bot());
        }

        public static double Sc_ten(this Node n)
        {
            return n.Flexure() == "Positive" ? n.Sc_bot() : n.Sc_top();
        }


        public static double Dc(this Node n)
        {
            return n.Flexure() == "Positive" ? n.YU1() - n.ttop : n.YL2s() - n.tbot;
                      
        }

        
        public static string Slender(this Node n)
        {
            return 2*n.Dc() /Math.Cos(Math.Atan(n.S())) / n.tw <= 5.7*Math.Sqrt(Material.Es/Material.Fyf) ? "(Non)Compact Web" : "Slender Web";
        }

        //2.4. Checking compression stress

        //fbu +fl ≤ ΦfRhFyc
        // fl for compression flange
        public static double flcom(this Node n)
        {
            return n.Flexure() == "Positive" ? Math.Abs(n.fl()) : 0;
        }
        // fl for tension flange
        public static double flten(this Node n)
        {
            return n.Flexure() == "Positive" ? 0 : Math.Abs(n.fl());
        }

        // fbu + fl (compression)
        public static double fbufl_com(this Node n)
        {
            return n.Sc_com() + n.flcom();
        }
        public static string CheckC_comOF(this Node n)
        {
            if (n.flcom() == 0 && n.Slender() == "Slender Web")
                return "NOT be checked";
            else
                return n.Sc_com() + n.flcom() <= n.Rh().Item1 * Material.Fyf ? "OK" : "NG";
        }

        public static string CheckC_comOF_ratio(this Node n)
        {
            if (n.flcom() == 0 && n.Slender() == "Slender Web")
                return "NOT be checked";
            else if (n.Sc_com() + n.flcom() == 0)
                return "Inf";
            else
                return (n.Rh().Item1 * Material.Fyf / (n.Sc_com() + n.flcom())).ToString();
        }


        //fbu +fl/3 ≤ ΦfFnc and fbu  ≤ ΦfFnc

        // fbu + fl/3 (compression)
        public static double fbufl3_com(this Node n)
        {
            return n.Sc_com() + n.flcom() / 3.0;
        }
        public static string CheckC_com(this Node n)
        {
            return n.Sc_com() + n.flcom() / 3.0 <= n.Fnc().Item1 ? "OK" : "NG";
        }

        public static string CheckC_com_ratio(this Node n)
        {
            return n.Sc_com() + n.flcom() / 3.0 == 0 ? "Inf" : (n.Fnc().Item1 / (n.Sc_com() + n.flcom() / 3.0)).ToString();
        }

        public static Tuple<double, double> Fnt(this Node n)
        {
            double Fnt_Cons, Fnt_ULS;

            if (n.Flexure() != "Positive" && n.ntop == 2)
            {
                Fnt_Cons = n.Rh().Item1 * Material.Fyf;
                Fnt_ULS = n.Rh().Item2 * Material.Fyf;
            }                
            else
            {
                Fnt_Cons = n.Rh().Item1 * Material.Fyf * n.Delta().Item1;
                Fnt_ULS = n.Rh().Item2 * Material.Fyf * n.Delta().Item2;
            }
            return Tuple.Create(Fnt_Cons, Fnt_ULS);
        }
        // fbu + fl (tension)
        public static double fbufl_ten(this Node n)
        {
            return n.Sc_ten() + n.flten();
        }
        // fbu + fl ≤ ΦfRhFyt and fbu ≤ ΦfRhFyfΔ
        public static string CheckC_ten(this Node n)
        {
            return n.Sc_ten() + n.flten() <= n.Fnt().Item1 ? "OK" : "NG";
        }

        public static string CheckC_ten_ratio(this Node n)
        {
            return n.Sc_ten() + n.flten() == 0 ? "Inf" : (n.Fnt().Item1/(n.Sc_ten() + n.flten())).ToString();
        }

        //3. Checking web Bend-Buckling Resistance for slender web

        // k = bend_buckling coefficient
        public static double k_bend(this Node n)
        {
            double k;
            if (n.Sc_top() <= 0 && n.Sc_bot() <= 0)
                k = 7.2;
            else
            {
                if (n.ds == 0)
                    k = 9 / (n.Dc() / n.D) / (n.Dc() / n.D);
                else if (n.ds / n.Dc() >= 0.4)
                    k = Math.Max(5.17 / (n.ds / n.D) / (n.ds / n.D), 9 / (n.Dc() / n.D) / (n.Dc() / n.D));
                else
                    k = 11.64 / Math.Pow((n.Dc() - n.ds) / n.D, 2);
            }
            return k;
        }


        public static double Fcrw(this Node n)
        {
            return Math.Min(0.9 * Material.Es * n.k_bend() * n.tw * n.tw / n.Hw() / n.Hw(), Math.Min(n.Rh().Item1 * Material.Fyf, Material.Fyw / 0.7));
        }

        public static string CheckC_buckling(this Node n)
        {
            if (n.Slender() == "Slender Web")
                return n.Sc_com() <= n.Fcrw() ? "OK" : "NG";
            else
                return "NOT be checked";
        }

        public static string CheckC_buckling_ratio(this Node n)
        {
            if (n.Slender() == "Slender Web")
                return n.Sc_com() == 0 ? "Inf" : (n.Fcrw()/ n.Sc_com()).ToString();
            else
                return "NOT be checked";
        }

        //3. Checking Shear
        public static Tuple<double,double> Vu(this Node n)
        {
            double Vu_Cons, Vu_ULS;
            Vu_Cons = (n.S1 + n.S2 + n.S3)*1.25 / 2.0;
            Vu_ULS = (1.25 * (n.S1 + n.S2 + n.S3 + n.S4) + 1.5 * n.Sw + 1.8 * ((1.25 * (n.S1 + n.S2 + n.S3 + n.S4) + 1.5 * n.Sw) >= 0 ? n.SLLmax() : n.SLLmin())) / 2.0;
            return Tuple.Create(Vu_Cons, Vu_ULS);
        }

        public static Tuple<double, double> Vui(this Node n)
        {
            double Vui_Cons, Vui_ULS;
            Vui_Cons = n.Vu().Item1 / Math.Cos(Math.Atan(n.S()));
            Vui_ULS = n.Vu().Item2 / Math.Cos(Math.Atan(n.S()));

            return Tuple.Create(Vui_Cons, Vui_ULS);
            
        }
        // k = shear - buckling coefficient

        public static Tuple<double,double> k_shear(this Node n)
        {
            double k_shear_Cons, k_shear_ULS;
            k_shear_Cons = 5 + 5 / (n.d0 / n.D) / (n.d0 / n.D);
            k_shear_ULS = n.Stiffened() == "Stiffened" ? (5 + 5 / (n.d0 / n.D) / (n.d0 / n.D)) : 5.0;
            return Tuple.Create(k_shear_Cons, k_shear_ULS);
        }

        public static Tuple<double, double> C(this Node n)
        {
            double C1_Cons, C1_ULS, C_Cons, C_ULS;            
            C1_Cons = Math.Sqrt(Material.Es * n.k_shear().Item1 / Material.Fyw);
            if (n.Hw() / n.tw <= 1.12 * C1_Cons)
                C_Cons = 1.0;
            else if (n.Hw() / n.tw <= 1.40 * C1_Cons)
                C_Cons = 1.12 / (n.Hw() / n.tw) * C1_Cons;
            else
                C_Cons = 1.57 * C1_Cons * C1_Cons / (n.Hw() / n.tw) / (n.Hw() / n.tw);

            C1_ULS = Math.Sqrt(Material.Es * n.k_shear().Item2 / Material.Fyw);
            if (n.Hw() / n.tw <= 1.12 * C1_ULS)
                C_ULS = 1.0;
            else if (n.Hw() / n.tw <= 1.40 * C1_ULS)
                C_ULS = 1.12 / (n.Hw() / n.tw) * C1_ULS;
            else
                C_ULS = 1.57 * C1_ULS * C1_ULS / (n.Hw() / n.tw) / (n.Hw() / n.tw);
            return Tuple.Create(C_Cons, C_ULS);
        }

        public static double Vp(this Node n)
        {            
            return 0.58 * Material.Fyw * n.Hw() * n.tw / 1000;
        }

        public static Tuple<double, double> Vn(this Node n)
        {
            double Vn_Cons, Vn_ULS;

            Vn_Cons = n.C().Item1 * n.Vp();

            if (n.Stiffened() == "Stiffened")
            {
                if (2 * n.Hw() * n.tw / (n.btop * n.ttop * n.ntop + n.tbot * n.bbot) <= 2.5)
                    Vn_ULS = n.Vp() * (n.C().Item2 + 0.87*(1 - n.C().Item2) / Math.Sqrt( 1 + n.d0 * n.d0 / n.D / n.D));
                else
                    Vn_ULS = n.Vp() * (n.C().Item2 + 0.87 * (1 - n.C().Item2) / Math.Sqrt(1 + n.d0 * n.d0 / n.D / n.D + n.d0 / n.D));
            }
            else
                Vn_ULS = n.C().Item2 * n.Vp();

            return Tuple.Create(Vn_Cons, Vn_ULS);
        }

        public static Tuple<string,string> Check_shear(this Node n)
        {
            string Check_shear_Cons, Check_shear_ULS;
            Check_shear_Cons = Math.Abs(n.Vui().Item1) <= n.Vn().Item1 ? "OK" : "NG";
            Check_shear_ULS = Math.Abs(n.Vui().Item2) <= n.Vn().Item2 ? "OK" : "NG";

            return Tuple.Create(Check_shear_Cons, Check_shear_ULS);
        }

        public static Tuple<string, string> Check_shear_ratio(this Node n)
        {
            string Check_shear_ratio_Cons, Check_shear_ratio_ULS;
            Check_shear_ratio_Cons = n.Vui().Item1 == 0 ? "Inf" : (n.Vn().Item1 / Math.Abs(n.Vui().Item1)).ToString();
            Check_shear_ratio_ULS = n.Vui().Item2 == 0 ? "Inf" : (n.Vn().Item2 / Math.Abs(n.Vui().Item2)).ToString();

            return Tuple.Create(Check_shear_ratio_Cons, Check_shear_ratio_ULS);            
        }
         
        

    }



}
