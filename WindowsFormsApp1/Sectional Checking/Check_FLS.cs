using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checking
{
    public class Check_FLS
    {
        private string _Label, _Flexure, Type;
        private double S1_top, S1_bot, S2_top, S2_bot, S3_top_pos, S3_bot_pos, S3_top_negl, S3_bot_negl, S4_top_pos, S4_bot_pos, S4_top_neg, S4_bot_neg, Sfmax_top, Sfmin_top, Sfmax_bot, Sfmin_bot,
            Vn, S1, S2, S3, S4, Sw, S, _MLLfmax, _MLLfmin, _SLLfmax, _SLLfmin, ADTT;

        public Check_FLS(string Label, string Flexure, double S1_top, double S1_bot, double S2_top, double S2_bot, double S3_top_pos, double S3_bot_pos, double S3_top_negl, double S3_bot_negl,
            double S4_top_pos, double S4_bot_pos, double S4_top_neg, double S4_bot_neg, double Sfmax_top, double Sfmin_top, double Sfmax_bot, double Sfmin_bot, string Type, double Vn,
            double S1, double S2, double S3, double S4, double Sw, double SLLfmax, double SLLfmin, double S, double MLLfmax, double MLLfmin, double ADTT)
        {
            this._Label = Label;
            this._Flexure = Flexure;
            this.S1_top = S1_top;
            this.S1_bot = S1_bot;
            this.S2_top = S2_top;
            this.S2_bot = S2_bot;
            this.S3_top_pos = S3_top_pos;
            this.S3_bot_pos = S3_bot_pos;
            this.S3_top_negl = S3_top_negl;
            this.S3_bot_negl = S3_bot_negl;
            this.S4_top_pos = S4_top_pos;
            this.S4_bot_pos = S4_bot_pos;
            this.S4_top_neg = S4_top_neg;
            this.S4_bot_neg = S4_bot_neg;
            this.Sfmax_top = Sfmax_top;
            this.Sfmax_bot = Sfmax_bot;
            this.Sfmin_top = Sfmin_top;
            this.Sfmin_bot = Sfmin_bot;
            this.Vn = Vn;
            this.S1 = S1;
            this.S2 = S2;
            this.S3 = S3;
            this.S4 = S4;
            this.Sw = Sw;
            this._SLLfmax = SLLfmax;
            this._SLLfmin = SLLfmin;
            this.S = S;
            this.Type = Type;
            this._MLLfmax = MLLfmax;
            this._MLLfmin = MLLfmin;
            this.ADTT = ADTT;


        }


        public string Label
        {
            get { return _Label; }

        }

        public string Flexure
        {
            get { return _Flexure; }

        }

        public double MLLfmax
        {
            get { return _MLLfmax; }

        }
        public double MLLfmin
        {
            get { return _MLLfmin; }

        }

        public double SLLfmax
        {
            get { return _SLLfmax; }

        }
        public double SLLfmin
        {
            get { return _SLLfmin; }

        }

        public double fDC_top
        {
            get { return (Flexure == "Positive" ? S1_top + S2_top + S3_top_pos + S4_top_pos : S1_top + S2_top + S3_top_negl + S4_top_neg); }
        }

        public double fDC_bot
        {
            get { return (Flexure == "Positive" ? S1_bot + S2_bot + S3_bot_pos + S4_bot_pos : S1_bot + S2_bot + S3_bot_negl + S4_bot_neg); }
        }

        public double Deltaf_top
        {
            get { return Math.Abs(Sfmax_top - Sfmin_top); }
        }

        public double Deltaf_bot
        {
            get { return Math.Abs(Sfmax_bot - Sfmin_bot); }
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

        // Checking load-induced fatigue
        public string Check_stiffener
        {
            get { return Math.Max(Deltaf_top, Deltaf_bot) <= DeltaF_stiffener ? "OK" : "NG"; }
        }

        public string Check_cross
        {
            get
            {
                if (Type == "Cross")
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

        // Checking web

        public double Vcr
        {
            get { return Vn; }
        }

        public double Vu
        {
            get { return (S1 + S2 + S3 + S4 + Sw + 2 * 0.75 * (S1 >= 0 ? SLLfmax : SLLfmin)) / 2.0; }
        }

        public double Vui
        {
            get { return Vu / Math.Cos(Math.Atan(S)); }
        }

        public string Check_shear
        {
            get { return (Math.Abs(Vui) <= Vcr ? "OK" : "NG"); }
        }


        //End
    }
}
