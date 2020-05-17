using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sectional_Checking
{
    public class Stress
    {
        private string _Label;
        private double A1, I1, YU1, YL1, SU1, SL1, A2s, I2s, YU2s, YL2s, SU2s, SL2s, A2l, I2l, YU2l, YL2l, SU2l, SL2l,
           A3s, I3s, YU3s, YL3s, SU3s, SL3s, A3l, I3l, YU3l, YL3l, SU3l, SL3l,
            A4s, I4s, YU4s, YL4s, SU4s, SL4s, A4l, I4l, YU4l, YL4l, SU4l, SL4l,
            M1, M2, M3, M4, Mw, MLLmax, MLLmin, MLLfmax, MLLfmin;
        public Stress(String Label, double A1, double I1, double YU1, double YL1, double SU1, double SL1, double A2s, double I2s, double YU2s, double YL2s, double SU2s, double SL2s, double A2l, double I2l, double YU2l, double YL2l, double SU2l, double SL2l,
           double A3s, double I3s, double YU3s, double YL3s, double SU3s, double SL3s, double A3l, double I3l, double YU3l, double YL3l, double SU3l, double SL3l,
            double A4s, double I4s, double YU4s, double YL4s, double SU4s, double SL4s, double A4l, double I4l, double YU4l, double YL4l, double SU4l, double SL4l,
            double M1, double M2, double M3, double M4, double Mw, double MLLmax, double MLLmin, double MLLfmax, double MLLfmin)
        {
            this._Label = Label;
            this.A1 = A1;
            this.I1 = I1;
            this.YU1 = YU1;
            this.YL1 = YL1;
            this.SU1 = SU1;
            this.SL1 = SL1;

            this.A2s = A2s;
            this.I2s = I2s;
            this.YU2s = YU2s;
            this.YL2s = YL2s;
            this.SU2s = SU2s;
            this.SL2s = SL2s;

            this.A2l = A2l;
            this.I2l = I2l;
            this.YU2l = YU2l;
            this.YL2l = YL2l;
            this.SU2l = SU2l;
            this.SL2l = SL2l;

            this.A3s = A3s;
            this.I3s = I3s;
            this.YU3s = YU3s;
            this.YL3s = YL3s;
            this.SU3s = SU3s;
            this.SL3s = SL3s;

            this.A3l = A3l;
            this.I3l = I3l;
            this.YU3l = YU3l;
            this.YL3l = YL3l;
            this.SU3l = SU3l;
            this.SL3l = SL3l;

            this.A4s = A4s;
            this.I4s = I4s;
            this.YU4s = YU4s;
            this.YL4s = YL4s;
            this.SU4s = SU4s;
            this.SL4s = SL4s;

            this.A4l = A4l;
            this.I4l = I4l;
            this.YU4l = YU4l;
            this.YL4l = YL4l;
            this.SU4l = SU4l;
            this.SL4l = SL4l;

            this.M1 = M1;
            this.M2 = M2;
            this.M3 = M3;
            this.M4 = M4;
            this.Mw = Mw;
            this.MLLmax = MLLmax;
            this.MLLmin = MLLmin;
            this.MLLfmax = MLLfmax;
            this.MLLfmin = MLLfmin;

        }


        public string Label
        {
            get { return _Label; }

        }


        // Stress due to only Steel
        public double S1_top
        {
            get { return -M1 / SU1 * 1000000; }
        }

        public double S1_bot
        {
            get { return M1 / SL1 * 1000000; }
        }

        // Stress due to Bottom concrete
        public double S2_top
        {
            get { return -M2 / SU1 * 1000000; }
        }

        public double S2_bot
        {
            get { return M2 / SL1 * 1000000; }
        }

        // Stress due to Deckslab (shorttime and longtime)
        public double S3_top_pos
        {
            get { return -M3 / SU1 * 1000000; }
        }

        public double S3_bot_pos
        {
            get { return M3 / SL1 * 1000000; }
        }

        public double S3_top_negs
        {
            get { return -M3 / SU2s * 1000000; }
        }

        public double S3_bot_negs
        {
            get { return M3 / SL2s * 1000000; }
        }

        public double S3_top_negl
        {
            get { return -M3 / SU2l * 1000000; }
        }

        public double S3_bot_negl
        {
            get { return M3 / SL2l * 1000000; }
        }

        // Stress due to DC4 (longtime)

        public double S4_top_pos
        {
            get { return -M4 / SU3l * 1000000; }
        }

        public double S4_bot_pos
        {
            get { return M4 / SL3l * 1000000; }
        }

        public double S4_top_neg
        {
            get { return -M4 / SU4l * 1000000; }
        }

        public double S4_bot_neg
        {
            get { return M4 / SL4l * 1000000; }
        }

        // Stress due to DW (longtime)

        public double Sw_top_pos
        {
            get { return -Mw / SU3l * 1000000; }
        }
        public double Sw_bot_pos
        {
            get { return Mw / SL3l * 1000000; }
        }

        public double Sw_top_neg
        {
            get { return -Mw / SU4l * 1000000; }
        }
        public double Sw_bot_neg
        {
            get { return Mw / SL4l * 1000000; }
        }

        // Stress due to LL (Short time)

        public double Slmax_top_pos
        {
            get { return -MLLmax / SU3s * 1000000; }
        }
        public double Slmax_bot_pos
        {
            get { return MLLmax / SL3s * 1000000; }
        }

        public double Slmin_top_pos
        {
            get { return -MLLmin / SU3s * 1000000; }
        }
        public double Slmin_bot_pos
        {
            get { return MLLmin / SL3s * 1000000; }
        }

        public double Slmax_top_neg
        {
            get { return -MLLmax / SU4s * 1000000; }
        }
        public double Slmaxb_bot_neg
        {
            get { return MLLmax / SL4s * 1000000; }
        }

        public double Slmin_top_neg
        {
            get { return -MLLmin / SU4s * 1000000; }
        }
        public double Slmin_bot_neg
        {
            get { return MLLmin / SL4s * 1000000; }
        }

        //Calcualtion stress for limit states
        //Classify flexure positive or negative
        public string Flexure
        {
            get { return (S1_top + S2_top + S3_top_pos) <= 0 ? "Positive" : "Negative"; }
        }

        //Constructibility
        public double Sc_top
        {
            get { return Flexure == "Positive" ? 1.25 * (S1_top + S2_top + S3_top_pos) : 1.25 * (S1_top + S2_top + S3_top_negs); }
        }

        public double Sc_bot
        {
            get { return Flexure == "Positive" ? 1.25 * (S1_bot + S2_bot + S3_bot_pos) : 1.25 * (S1_bot + S2_bot + S3_bot_negs); }
        }

        //Ultimate limit state
        public double Su_top
        {
            get { return Flexure == "Positive" ? (1.25 * (S1_top + S2_top + S3_top_pos + S4_top_pos) + 1.5 * Sw_top_pos + 1.8 * Slmax_top_pos) : (1.25 * (S1_top + S2_top + S3_top_negl + S4_top_neg) + 1.5 * Sw_top_neg + 1.8 * Slmin_top_neg); }
        }

        public double Su_bot
        {
            get { return Flexure == "Positive" ? (1.25 * (S1_bot + S2_bot + S3_bot_pos + S4_bot_pos) + 1.5 * Sw_bot_pos + 1.8 * Slmax_bot_pos) : (1.25 * (S1_bot + S2_bot + S3_bot_negl + S4_bot_neg) + 1.5 * Sw_bot_neg + 1.8 * Slmin_bot_neg); }
        }

        //Service I limit state

        public double Ss1_top
        {
            get { return Flexure == "Positive" ? (1.00 * (S1_top + S2_top + S3_top_pos + S4_top_pos) + 1.0 * Sw_top_pos + 1.0 * Slmax_top_pos) : (1.00 * (S1_top + S2_top + S3_top_negl + S4_top_neg) + 1.0 * Sw_top_neg + 1.0 * Slmin_top_neg); }
        }

        public double Ss1_bot
        {
            get { return Flexure == "Positive" ? (1.00 * (S1_bot + S2_bot + S3_bot_pos + S4_bot_pos) + 1.0 * Sw_bot_pos + 1.0 * Slmax_bot_pos) : (1.00 * (S1_bot + S2_bot + S3_bot_negl + S4_bot_neg) + 1.0 * Sw_bot_neg + 1.0 * Slmin_bot_neg); }
        }


        //Service II limit state

        public double Ss2_top
        {
            get { return Flexure == "Positive" ? (1.00 * (S1_top + S2_top + S3_top_pos + S4_top_pos) + 1.0 * Sw_top_pos + 1.3 * Slmax_top_pos) : (1.00 * (S1_top + S2_top + S3_top_negl + S4_top_neg) + 1.0 * Sw_top_neg + 1.3 * Slmin_top_neg); }
        }

        public double Ss2_bot
        {
            get { return Flexure == "Positive" ? (1.00 * (S1_bot + S2_bot + S3_bot_pos + S4_bot_pos) + 1.0 * Sw_bot_pos + 1.3 * Slmax_bot_pos) : (1.00 * (S1_bot + S2_bot + S3_bot_negl + S4_bot_neg) + 1.0 * Sw_bot_neg + 1.3 * Slmin_bot_neg); }
        }


        //Fatigue limit state (for Max and Min liveload)
        public double Sfmax_top
        {
            get { return Flexure == "Positive" ? (-MLLfmax / SU3s) * 0.75 * 1000000 : (-MLLfmax / SU4s) * 0.75 * 1000000; }
        }
        public double Sfmax_bot
        {
            get { return Flexure == "Positive" ? (MLLfmax / SL3s) * 0.75 * 1000000 : (MLLfmax / SL4s) * 0.75 * 1000000; }
        }
        public double Sfmin_top
        {
            get { return Flexure == "Positive" ? (-MLLfmin / SU3s) * 0.75 * 1000000 : (-MLLfmin / SU4s) * 0.75 * 1000000; }
        }
        public double Sfmin_bot
        {
            get { return Flexure == "Positive" ? (MLLfmin / SL3s) * 0.75 * 1000000 : (MLLfmin / SL4s) * 0.75 * 1000000; }
        }

    }
}
