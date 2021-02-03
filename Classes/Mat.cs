using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Mat
    {
        public Mat()
        {

        }
        public Mat(string Name, string Type, double Ws, double Es, double G, double Fy, double Fu, double Wc, double fc, double Ec, string Lib)
        {
            this.Name = Name;
            this.Type = Type;
            this.Ws = Ws;
            this.Es = Es;
            this.G = G;
            this.Fy = Fy;
            this.Fu = Fu;
            this.Wc = Wc;
            this.fc = fc;
            this.Ec = Ec;
            this.Lib = Lib;

        }
        public string Name
        { get; set; }

        public string Type
        { get; set; }

        //Steel
        public double Ws
        { get; set; }

        public double Es
        { get; set; }

        public double G
        { get; set; }

        public double Fy
        { get; set; }

        public double Fu
        { get; set; }

        //Concrete
        public double Wc
        { get; set; }

        public double fc
        { get; set; }

        public double Ec
        { get; set; }

        public string Lib
        { get; set; }

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

        public double Fus()
        {            
            return Lib == "" ? Fu : ListofFy.Where(t => t.Item1 == Lib).First().Item7;
        }

        public double Fys(double tw)
        {
            if (Lib == "")
                return Fy;
            else
            {
                if (tw <= 16)
                    return ListofFy.Where(t => t.Item1 == Lib).First().Item2;
                else if (tw <= 40)
                    return ListofFy.Where(t => t.Item1 == Lib).First().Item3;
                else if (tw <= 75)
                    return ListofFy.Where(t => t.Item1 == Lib).First().Item4;
                else if (tw <= 100)
                    return ListofFy.Where(t => t.Item1 == Lib).First().Item5;
                else
                    return ListofFy.Where(t => t.Item1 == Lib).First().Item6;
            }
            
        }
    }
}
