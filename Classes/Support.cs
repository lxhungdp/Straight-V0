using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Support
    {
        public Support(int Joint, double X, double Y, double Z, string Type)
        {
            this.Joint = Joint;
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.Type = Type;
        }

        public int Joint
        { get; set; }
        public double X
        { get; set; }

        public double Y
        { get; set; }
        public double Z
        { get; set; }

        public string Type
        { get; set; }

    }
}
