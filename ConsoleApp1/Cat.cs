using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    public class Cat
    {
        public string color { get; set; }
        public double leg { get; set; }
        
        public double leg1()
        {
            return leg * 2;
        }
        public Manu manu { get; set; }

        public class Manu
        {
           
            public string A1 { get; set; }
            public double A2 { get; set; }

            public double A3()
            {
                return A2*2;
            }
        }
    }
}
