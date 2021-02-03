using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Shoe
    {       

        public Shoe()
        {

        }


        public Shoe(int Girder, int Support, string Label, int EA, double A, double B, double[] Aspan, double[] Aspacing )
        {
            this.Girder = Girder;
            this.EA = EA;
            this.A = A;
            this.B = B;
            this.Support = Support;
            this.Label = Label;           
           
            this.X = Cumsum(Aspan)[Support - 1];
            this.Y = Cumsum(Aspacing.Skip(1).ToArray())[Girder - 1];

        }
        

        public double D
        { get; set; }

        


        // ---
        public List<double> Cumsum(double [] Aspan)
        {
            double sum = 0;
            List<double> b = new List<double>();
            if (Aspan != null)
            {                
                b = Aspan.Select(p => sum += p).ToList();
                b.Insert(0, 0.0);
            }
            
             return b;
            
        }
               

        //Infor of Shoe
        public int Girder
        { get; set; }

        public int Support
        { get; set; }

        public string Label
        { get; set; }

        public int EA
        { get; set; }

        public double A
        { get; set; }
        public double B
        { get; set; }

        //Info of support
        public int Joint
        { get; set; }

        public double Station
        { get; set; }

        public double X
        {
            get; set;
        }

        public double Y
        {
            get; set;
        }


        public double Z
        {
            get
            {
                return -D / 2;
            }
        }

        public string Type
        {
            get; set;
        }


        //Y, Type, Joint name of 2 shoes (if one shoe, using Y1, Type1, ...)
        public double Y1
        {
            get
            {               
                    return Y - A;
            }
        }

        public double Y2
        {
            get
            {
                  return Y + B;
            }
        }

        public string Type1
        {
            get
            {
                return Type;
            }
        }

        public string Type2
        {
            get
            {
                if (Type == "Fixed")
                    return "LongFixed";
                else if (Type == "TranFixed")
                    return "Free";
                else if (Type == "LongFixed")
                    return "LongFixed";
                else
                    return "Free";
            }
        }

        public string Joint1
        {
            get
            {
                return "S" + (Girder * 10 + Support).ToString() + (EA == 2 ? "-1" : "");
            }
        }

        public string Joint2
        {
            get
            {
                if (EA == 2)
                    return "S" + (Girder * 10 + Support).ToString() + (EA == 2 ? "-2" : "");
                else
                    return "";
            }
        }

       
    }
}
