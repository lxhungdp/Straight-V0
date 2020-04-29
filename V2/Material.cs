using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public class Material
    {
        public static double Fyf { get; set; }

        public static double Fuf { get; set; }

        public static double Fyw { get; set; }

        public static double Fuw { get; set; }

        public static double Es { get; set; }

        public static double fckd { get; set; }

        public static double fckb { get; set; }

        public static double Fyb { get; set; }

        public static double Esb { get; set; }

        private static double fcktofcm(double fck)
        {
            double[,] a = { { 18, 22 }, { 21, 25 }, { 24, 28 }, { 27, 31 }, { 30, 34 }, { 35, 39 }, { 40, 44 }, { 50, 56 }, { 60, 66 }, { 70, 76 }, { 80, 86 }, { 90, 96 } };
            double fcm = new double();
            for (int i = 0; i < a.GetLength(0); i++)
                if (a[i, 0] >= fck)
                {
                    fcm = a[i, 1];
                    break;
                }
            return fcm;
        }

        public static double Ecb()
        {
            return 0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcktofcm(fckb), (1 / 3.0));
        }

        public static double Ecd()
        {
            return 0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcktofcm(fckd), (1 / 3.0));
        }

        public static double nEd()
        {
            return Es / Ecd();
        }

        public static double nEb()
        {
            return Es / Ecb();
        }

        public static double Fyr()
        {
            return Math.Max(Math.Min(0.7 * Fyf, Fyw),0.5*Fyf);
        }

        // Weight density of steel (kN/m3)
        public static double Ys { get; set; }
        
        // Weight density of concrete (kN/m3)
        public static double Yc { get; set; }
        
        // Distribution loading in constructibility (kN/m)
        public static double forms { get; set; }
    }
}
