using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Material
    {
        public static string Flange { get; set; }

        public static string Web { get; set; }

        // Determine Fu
        // Determine Fy       

        private static List<Tuple<string, double, double, double, double, double, double>> ListofFy = new List<Tuple<string, double, double, double, double, double, double>>
        {
            Tuple.Create( "SS235", 235.0, 225.0, 205.0, 205.0, 195.0, 330.0 ),
            Tuple.Create( "SS275", 275.0, 265.0, 245.0, 245.0, 235.0, 410.0 ),
            Tuple.Create( "SM275", 275.0, 265.0, 255.0, 245.0, 235.0, 410.0 ),
            Tuple.Create( "SMA275", 275.0, 265.0, 255.0, 245.0, 235.0, 410.0 ),
            Tuple.Create( "SM355", 355.0, 345.0, 335.0, 325.0, 305.0, 490.0 ),
            Tuple.Create( "SAM355", 355.0, 345.0, 335.0, 325.0, 305.0, 490.0 ),
            Tuple.Create( "SM420", 420.0, 410.0, 400.0, 390.0, 380.0, 520.0 ),
            Tuple.Create( "SM460", 460.0, 450.0, 430.0, 420.0, 420.0, 570.0 ),
            Tuple.Create( "SAM460", 460.0, 450.0, 430.0, 420.0, 420.0, 570.0 ),
            Tuple.Create( "HSB380", 380.0, 380.0, 380.0, 380.0, 380.0, 500.0 ),
            Tuple.Create( "HSB460", 460.0, 460.0, 460.0, 460.0, 460.0, 600.0 ),
            Tuple.Create( "HSB690", 690.0, 690.0, 690.0, 690.0, 690.0, 800.0 ),
        };

        public static double Fu(string a)
        {
            return ListofFy.Where(t => t.Item1 == a).First().Item7;
        }

        public static double Fy(string a, double tw)
        {
            if (tw <= 16)
                return ListofFy.Where(t => t.Item1 == a).First().Item2;
            else if (tw <= 40)
                return ListofFy.Where(t => t.Item1 == a).First().Item3;
            else if (tw <= 75)
                return ListofFy.Where(t => t.Item1 == a).First().Item4;
            else if (tw <= 100)
                return ListofFy.Where(t => t.Item1 == a).First().Item5;
            else
                return ListofFy.Where(t => t.Item1 == a).First().Item6;
        }


        public static double Es { get; set; }

        public static double fckd { get; set; }

        public static double fckb { get; set; }

        public static double Fyb { get; set; }

        public static double Esb { get; set; }


        private static Dictionary<double, double> fcktofcm = new Dictionary<double, double>()
        {
            { 18, 22 }, { 21, 25 }, { 24, 28 }, { 27, 31 }, { 30, 34 }, { 35, 39 }, { 40, 44 }, { 50, 56 }, { 60, 66 }, { 70, 76 }, { 80, 86 }, { 90, 96 }
        };

        public static double Ecb
        {
            get { return 0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcktofcm[fckb], (1 / 3.0)); }
        }

        public static double Ecd
        {
            get { return 0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcktofcm[fckd], (1 / 3.0)); }
        }

        public static double nEd
        {
            get { return Es / Ecd; }
        }

        public static double nEb
        {
            get { return Es / Ecb; }
        }



        public static double frd
        {
            get { return 0.63 * Math.Sqrt(fckd); }
        }

        public static double frb
        {
            get { return 0.63 * Math.Sqrt(fckb); }
        }
        // Weight density of steel (kN/m3)
        public static double Ys { get; set; }

        // Weight density of concrete (kN/m3)
        public static double Yc { get; set; }

        // Distribution loading in constructibility (kN/m)
        public static double forms { get; set; }
    }
}
