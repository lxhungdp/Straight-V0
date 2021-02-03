using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Check_FLS
    {
        private double ADTT, S, S1_top, S2_top, S3_top, S4_top, S1_bot, S2_bot, S3_bot, S4_bot, Sfmax_top, Sfmax_bot, Sfmin_top, Sfmin_bot;
        private Mat Web;
       
        public Check_FLS(Node Node, Sec Sec, Stress Stress, ElmForces Shear, Mat Web, double ADTT)
        {

            this.Element = Sec.Element;
            this.Joint = Sec.Node;
            this.Station = Sec.Station;
            this.Label = Node.Label;
            this.ADTT = ADTT;
            this.S = Node.S;
            this.D = Node.D;
            this.d0 = Node.d0;
            this.Hw = Node.Hw;
            this.tw = Node.tw;
            

            //Force
            this.S1 = Shear.DC1;
            this.S2 = Shear.DC2;
            this.S3 = Shear.DC3;
            this.S4 = Shear.DC4;
            this.Sw = Shear.DW;
            this.SLLfmax = Shear.LLfmax;
            this.SLLfmin = Shear.LLfmin;

            //Stress
            this.Flexure = Stress.Su_top <= 0 ? "Positive" : "Negative";
            this.S1_top = Stress.S1_top;
            this.S2_top = Stress.S2_top;
            this.S3_top = Stress.S3_top_long;
            this.S4_top = Stress.S4_top;
            this.Sfmax_top = 0.75 * Stress.Sfmax_top;
            this.Sfmin_top = 0.75 * Stress.Sfmin_top;

            this.S1_bot = Stress.S1_bot;
            this.S2_bot = Stress.S2_bot;
            this.S3_bot = Stress.S3_bot_long;
            this.S4_bot = Stress.S4_bot;
            this.Sfmax_bot = 0.75 * Stress.Sfmax_bot;
            this.Sfmin_bot = 0.75 * Stress.Sfmin_bot;

            //Material
            this.Web = Web;
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

        public double N
        {
            get
            {
                return 365 * 100 * 1 * ADTT;
            }

        }
        public double DeltaF_stiffener
        {
            get
            {
                if (N <= 2550000)
                    return Math.Pow(2550000 / N, 1.0 / 3) * 82.7;
                else if (N <= 81470000)
                    return Math.Pow(2550000 / N, 1.0 / 5) * 82.7;
                else
                    return 41.4;

            }
        }
        public double DeltaF_cross
        {
            get
            {
                if (N <= 4380000)
                    return Math.Pow(4380000 / N, 1.0 / 3) * 69;
                else if (N <= 140270000)
                    return Math.Pow(4380000 / N, 1.0 / 5) * 69;
                else
                    return 34.5;

            }
        }
        public double DeltaF_stud
        {
            get
            {
                if (N <= 4380000)
                    return Math.Pow(4380000 / N, 1.0 / 3) * 69;
                else if (N <= 140270000)
                    return Math.Pow(4380000 / N, 1.0 / 5) * 69;
                else
                    return 34.5;

            }
        }

        //Table 1
        

        public string Flexure
        {
            get; set;

        }
        public double fDC_top
        {
            get { return S1_top + S2_top + S3_top + S4_top; }
        }

        public double fDC_bot
        {
            get { return S1_bot + S2_bot + S3_bot + S4_bot; }
        }

        public double Deltaf_top
        {
            get { return Math.Abs(Sfmax_top - Sfmin_top) ; }
        }

        public double Deltaf_bot
        {
            get { return Math.Abs(Sfmax_bot - Sfmin_bot) ; }
        }

        // Checking load-induced fatigue
               
        public string Check_stiffener
        {
            get { return Label == "" || Label == "Section Changed" ? "-" : (Math.Max(Deltaf_top, Deltaf_bot) <= DeltaF_stiffener ? "OK" : "NG"); }
        }

        public string Check_cross
        {
            get
            {
                if (Label == "Exterior Support" || Label == "Interior Support" || Label == "Cross Beam")
                {
                    if (fDC_top <= 0 && Math.Abs(fDC_top) >= 2 * Deltaf_top)
                        return "NOT be checked";
                    else
                        return (Deltaf_top <= DeltaF_cross ? "OK" : "NG");
                }
                else
                    return "-";
            }
        }

        public string Check_stud
        {
            get
            {
                if (fDC_top <= 0 && Math.Abs(fDC_top) >= 2 * Deltaf_top)
                    return "NOT be checked";
                else
                    return (Deltaf_top <= DeltaF_stud ? "OK" : "NG");
            }
        }

        //Table 2
        public double d0
        {
            get; set;
        }
        public double D
        {
            get; set;
        }

        public double k_shear
        {
            get { return 5 + 5 / (d0 / D) / (d0 / D); }
        }

        public double Hw
        {
            get; set;
        }
        public double tw
        {
            get; set;
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

        
        public double S1
        {
            get; set;
        }

        public double S2
        {
            get; set;
        }

        public double S3
        {
            get; set;
        }

        public double S4
        {
            get; set;
        }

        public double Sw
        {
            get; set;
        }

        public double SLLfmax
        {
            get; set;
        }

        public double SLLfmin
        {
            get; set;
        }

        // Checking web        

        public double Vu
        {
            get { return (S1 + S2 + S3 + S4 + Sw + 2 * (S1 >= 0 ? SLLfmax : SLLfmin)) / 2.0; }
        }

        public double Vui
        {
            get { return Vu / Math.Cos(S); }
        }

        public string Check_shear
        {
            get { return (Math.Abs(Vui) <= Vn ? "OK" : "NG"); }
        }
    }
}
