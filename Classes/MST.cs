using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class MST
    {
        public string Label { get; set; }
        public double M1 { get; set; }
        public double M2 { get; set; }
        public double M3 { get; set; }
        public double M4 { get; set; }
        public double Mw { get; set; }
        public double MTmax { get; set; }
        public double MTmin { get; set; }
        public double MLmax { get; set; }
        public double MLmin { get; set; }

        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
        public double Sw { get; set; }
        public double STmax { get; set; }
        public double STmin { get; set; }
        public double SLmax { get; set; }
        public double SLmin { get; set; }

        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T3 { get; set; }
        public double T4 { get; set; }
        public double Tw { get; set; }
        public double TTmax { get; set; }
        public double TTmin { get; set; }

        public double TLmax { get; set; }
        public double TLmin { get; set; }


        public double MLLmax
        {
            get { return Math.Max(1.25 * MTmax, 0.75 * 1.25 * MTmax + MLmax); }
        }

        public double MLLmin
        {
            get { return Math.Min(1.25 * MTmin, 0.75 * 1.25 * MTmin + MLmin); }
        }
        public double SLLmax
        {
            get { return Math.Max(1.25 * STmax, 0.75 * 1.25 * STmax + SLmax); }
        }

        public double SLLmin
        {
            get { return Math.Min(1.25 * STmin, 0.75 * 1.25 * STmin + SLmin); }
        }

        public double TLLmax
        {
            get { return Math.Max(1.25 * TTmax, 0.75 * 1.25 * TTmax + TLmax); }
        }

        public double TLLmin
        {
            get { return Math.Min(1.25 * TTmin, 0.75 * 1.25 * TTmin + TLmin); }
        }

        public double MLLfmax
        {
            get { return 0.8 * 1.15 * MTmax; }
        }

        public double MLLfmin
        {
            get { return 0.8 * 1.15 * MTmin; }
        }

        public double SLLfmax
        {
            get { return 0.8 * 1.15 * STmax; }
        }

        public double SLLfmin
        {
            get { return 0.8 * 1.15 * STmin; }
        }

        public double TLLfmax
        {
            get { return 0.8 * 1.15 * TTmax; }
        }

        public double TLLfmin
        {
            get { return 0.8 * 1.15 * TTmin; }
        }
    }
}
