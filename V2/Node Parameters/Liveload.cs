using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public static class Liveload
    {
        public static double MLLmax(this Node n)
        {           
            return Math.Max(1.25 * n.MTmax, 0.75 * 1.25 * n.MTmax + n.MLmax);
        }

        public static double MLLmin(this Node n)
        {
            return Math.Min(1.25 * n.MTmin, 0.75 * 1.25 * n.MTmin + n.MLmin);
        }
        public static double SLLmax(this Node n)
        {
            return Math.Max(1.25 * n.STmax, 0.75 * 1.25 * n.STmax + n.SLmax);
        }

        public static double SLLmin(this Node n)
        {
            return Math.Min(1.25 * n.STmin, 0.75 * 1.25 * n.STmin + n.SLmin);
        }

        public static double TLLmax(this Node n)
        {
            return Math.Max(1.25 * n.TTmax, 0.75 * 1.25 * n.TTmax + n.TLmax);
        }

        public static double TLLmin(this Node n)
        {
            return Math.Min(1.25 * n.TTmin, 0.75 * 1.25 * n.TTmin + n.TLmin);
        }

        public static double MLLfmax(this Node n)
        {
            return 0.8*1.15*n.MTmax;
        }

        public static double MLLfmin(this Node n)
        {
            return 0.8 * 1.15 * n.MTmin;
        }

        public static double SLLfmax(this Node n)
        {
            return 0.8 * 1.15 * n.STmax;
        }

        public static double SLLfmin(this Node n)
        {
            return 0.8 * 1.15 * n.STmin;
        }

        public static double TLLfmax(this Node n)
        {
            return 0.8 * 1.15 * n.TTmax;
        }

        public static double TLLfmin(this Node n)
        {
            return 0.8 * 1.15 * n.TTmin ;
        }

    }
}
