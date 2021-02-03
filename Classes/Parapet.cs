using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Parapet
    {
        public Parapet(string type, double H1, double H2, double H3, double B1, double B2, double B3, double e)
        {
            this.type = type;
            this.H1 = H1;
            this.H2 = H2;
            this.H3 = H3;
            this.B1 = B1;
            this.B2 = B2;
            this.B3 = B3;
            this.e = e;

        }
        public string type
        {
            get; set;
        }
        public double H1
        {
            get; set;
        }

        public double H2
        {
            get; set;
        }
        public double H3
        {
            get; set;
        }
        public double B1
        {
            get; set;
        }
        public double B2
        {
            get; set;
        }
        public double B3
        {
            get; set;
        }

        public double e
        {
            get; set;
        }

        public double Area()
        {
            if (type == "Jersey barrier")
                return B1 * H1 + B2 * H1 + (B1 + 2 * B2) * H2 + B3 * H2 + (B1 + 2 * B2 + 2 * B3) * H3;
            else
                return B1 * H1 + B2 * H1 / 2 + (B1 + B2) * H2 + B3 * H2 / 2 + (B1 + B2 + B3) * H3;
        }

        public double Ecc()
        {
            if (type == "Jersey barrier")
                return B1 / 2 + B2 + B3;
            else
            {
                double Ar1 = B1 * H1;
                double Ar2 = B1 * H1 / 2;
                double Ar3 = (B1 + B2) * H2;
                double Ar4 = B3 * H2 / 2;
                double Ar5 = (B1 + B2 + B3) * H3;
                return (Ar1 * (B1 / 2) + Ar2 * (B1 + B2 / 3) + Ar3 * (B1 / 2 + B2 / 2) + Ar4 * (B1 + B2 + B3 / 3) + Ar5 * (B1 / 2 + B2 / 2 + B3 / 2)) / (Ar1 + Ar2 + Ar3 + Ar4 + Ar5);
            }                
        }


    }
}
