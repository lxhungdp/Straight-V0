using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public static class Stress
    {
        // Stress due to only Steel
        private static double S1t(this Node n)
        {
            return -n.M1/n.SU1() * 1000000;
        }

        private static double S1b(this Node n)
        {
            return n.M1 / n.SL1() * 1000000;
        }

        // Stress due to Bottom concrete
        private static double S2t(this Node n)
        {
            return -n.M2 / n.SU1() * 1000000;
        }

        private static double S2b(this Node n)
        {
            return n.M2 / n.SL1() * 1000000;
        }

        // Stress due to Deckslab (shorttime and longtime)
        private static double S3t_pos(this Node n)
        {
            return -n.M3 / n.SU1() * 1000000;
        }

        private static double S3b_pos(this Node n)
        {
            return n.M3 / n.SL1() * 1000000;
        }

        private static double S3t_negs(this Node n)
        {
            return -n.M3 / n.SU2s() * 1000000;
        }

        private static double S3b_negs(this Node n)
        {
            return n.M3 / n.SL2s() * 1000000;
        }

        private static double S3t_negl(this Node n)
        {
            return -n.M3 / n.SU2l() * 1000000;
        }

        private static double S3b_negl(this Node n)
        {
            return n.M3 / n.SL2l() * 1000000;
        }

        // Stress due to DC4 (longtime)

        private static double S4t_pos(this Node n)
        {
            return -n.M4 / n.SU3l() * 1000000;
        }

        private static double S4b_pos(this Node n)
        {
            return n.M4 / n.SL3l() * 1000000;
        }

        private static double S4t_neg(this Node n)
        {
            return -n.M4 / n.SU4l() * 1000000;
        }

        private static double S4b_neg(this Node n)
        {
            return n.M4 / n.SL4l() * 1000000;
        }

        // Stress due to DW (longtime)

        private static double Swt_pos(this Node n)
        {
            return -n.Mw / n.SU3l() * 1000000;
        }
        private static double Swb_pos(this Node n)
        {
            return n.Mw / n.SL3l() * 1000000;
        }

        private static double Swt_neg(this Node n)
        {
            return -n.Mw / n.SU4l() * 1000000;
        }
        private static double Swb_neg(this Node n)
        {
            return n.Mw / n.SL4l() * 1000000;
        }

        // Stress due to LL (Short time)

        private static double Slmaxt_pos(this Node n)
        {
            return -n.MLLmax() / n.SU3s() * 1000000;
        }
        private static double Slmaxb_pos(this Node n)
        {
            return n.MLLmax() / n.SL3s() * 1000000;
        }

        private static double Slmint_pos(this Node n)
        {
            return -n.MLLmin() / n.SU3s() * 1000000;
        }
        private static double Slminb_pos(this Node n)
        {
            return n.MLLmin() / n.SL3s() * 1000000;
        }

        private static double Slmaxt_neg(this Node n)
        {
            return -n.MLLmax() / n.SU4s() * 1000000;
        }
        private static double Slmaxb_neg(this Node n)
        {
            return n.MLLmax() / n.SL4s() * 1000000;
        }

        private static double Slmint_neg(this Node n)
        {
            return -n.MLLmin() / n.SU4s() * 1000000;
        }
        private static double Slminb_neg(this Node n)
        {
            return n.MLLmin() / n.SL4s() * 1000000;
        }


        //Calcualtion stress for limit states
        //Classify flexure positive or negative
        public static string Flexure(this Node n)
        {
            return (n.S1t() + n.S2t() + n.S3t_pos())<=0? "Positive":"Negative";
        }

        //Constructibility
        public static double Sc_top(this Node n)
        {
            return n.Flexure() == "Positive"? 1.25*(n.S1t() + n.S2t() + n.S3t_pos())  : 1.25 * (n.S1t() + n.S2t() + n.S3t_negs());
        }

        public static double Sc_bot(this Node n)
        {
            return n.Flexure() == "Positive" ? 1.25 * (n.S1b() + n.S2b() + n.S3b_pos()) : 1.25 * (n.S1b() + n.S2b() + n.S3b_negs());
        }

        //Ultimate limit state
        public static double Su_top(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.25 * (n.S1t() + n.S2t() + n.S3t_pos() + n.S4t_pos()) + 1.5*n.Swt_pos() + 1.8*n.Slmaxt_pos()) : (1.25 * (n.S1t() + n.S2t() + n.S3t_negl() + n.S4t_neg()) + 1.5 * n.Swt_neg() + 1.8 * n.Slmint_neg());
        }

        public static double Su_bot(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.25 * (n.S1b() + n.S2b() + n.S3b_pos() + n.S4b_pos()) + 1.5 * n.Swb_pos() + 1.8 * n.Slmaxb_pos()) : (1.25 * (n.S1b() + n.S2b() + n.S3b_negl() + n.S4b_neg()) + 1.5 * n.Swb_neg() + 1.8 * n.Slminb_neg());
        }

        //Service I limit state
        public static double Ss1_top(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.00 * (n.S1t() + n.S2t() + n.S3t_pos() + n.S4t_pos()) + 1.00 * n.Swt_pos() + 1.00 * n.Slmaxt_pos()) : (1.00 * (n.S1t() + n.S2t() + n.S3t_negl() + n.S4t_neg()) + 1.00 * n.Swt_neg() + 1.00 * n.Slmint_neg());
        }

        public static double Ss1_bot(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.00 * (n.S1b() + n.S2b() + n.S3b_pos() + n.S4b_pos()) + 1.00 * n.Swb_pos() + 1.00 * n.Slmaxb_pos()) : (1.00 * (n.S1b() + n.S2b() + n.S3b_negl() + n.S4b_neg()) + 1.00 * n.Swb_neg() + 1.00 * n.Slminb_neg());
        }

        //Service II limit state
        public static double Ss2_top(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.00 * (n.S1t() + n.S2t() + n.S3t_pos() + n.S4t_pos()) + 1.00 * n.Swt_pos() + 1.30 * n.Slmaxt_pos()) : (1.00 * (n.S1t() + n.S2t() + n.S3t_negl() + n.S4t_neg()) + 1.00 * n.Swt_neg() + 1.30 * n.Slmint_neg());
        }

        public static double Ss2_bot(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.00 * (n.S1b() + n.S2b() + n.S3b_pos() + n.S4b_pos()) + 1.00 * n.Swb_pos() + 1.30 * n.Slmaxb_pos()) : (1.00 * (n.S1b() + n.S2b() + n.S3b_negl() + n.S4b_neg()) + 1.00 * n.Swb_neg() + 1.30 * n.Slminb_neg());
        }

        //Fatigue limit state (for Max and Min liveload)
        public static double Sfmax_top(this Node n)
        {
            return n.Flexure() == "Positive" ? (-n.MLLfmax()/n.SU3s()) * 0.75 * 1000000 : (-n.MLLfmax() / n.SU4s())*0.75*1000000;
        }
        public static double Sfmax_bot(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.MLLfmax() / n.SL3s()) * 0.75 * 1000000 : (n.MLLfmax() / n.SL4s()) * 0.75 * 1000000;
        }
        public static double Sfmin_top(this Node n)
        {
            return n.Flexure() == "Positive" ? (-n.MLLfmin() / n.SU3s()) * 0.75 * 1000000 : (-n.MLLfmin() / n.SU4s()) * 0.75 * 1000000;
        }
        public static double Sfmin_bot(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.MLLfmin() / n.SL3s()) * 0.75 * 1000000 : (n.MLLfmin() / n.SL4s()) * 0.75 * 1000000;
        }
    }
}
