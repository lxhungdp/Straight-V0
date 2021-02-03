using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class SecSap
    {
        public SecSap()
        {

        }

        public SecSap(int ID, double A1, double Ix1, double Iy1, double J1, double A2, double Ix2, double Iy2, double J2, double A3, double Ix3, double Iy3, double J3)
        {
            this.ID = ID;
            this.A1 = A1;
            this.Ix1 = Ix1;
            this.Iy1 = Iy1;
            this.J1 = J1;

            this.A2 = A2;
            this.Ix2 = Ix2;
            this.Iy2 = Iy2;
            this.J2 = J2;

            this.A3 = A3;
            this.Ix3 = Ix3;
            this.Iy3 = Iy3;
            this.J3 = J3;
        }
        public int ID
        { get; set; }

        public double A1
        { get; set; }
        public double Ix1
        { get; set; }
        public double Iy1
        { get; set; }
        public double J1
        { get; set; }

        public double A2
        { get; set; }
        public double Ix2
        { get; set; }
        public double Iy2
        { get; set; }
        public double J2
        { get; set; }

        public double A3
        { get; set; }
        public double Ix3
        { get; set; }
        public double Iy3
        { get; set; }
        public double J3
        { get; set; }
    }
}
