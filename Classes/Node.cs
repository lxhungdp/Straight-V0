using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Node
    {
        public double Label
        { get; set; }
        public double Type
        { get; set; }
        public double BeamID
        { get; set; }
        public double X
        { get; set; }
        public double Y
        { get; set; }
        public double Z
        { get; set; }
        public string Restrain
        { get; set; }

        //Top flange
        
        public double btop
        { get; set; }
        public double ttop
        { get; set; }
        public double ctop
        { get; set; }

        //Bottom flange
        public double bbot
        { get; set; }
        public double tbot
        { get; set; }
        public double cbot
        { get; set; }

        //Web
        public double D
        { get; set; }
        public double tw
        { get; set; }

        //Bottom concrete
        public double Hc
        { get; set; }

        //Deck
        public double ts
        { get; set; }
        public double th
        { get; set; }
        public double bh
        { get; set; }

        //Rebar
        public double drt
        { get; set; }
        public double art
        { get; set; }
        public double crt
        { get; set; }
        public double drb
        { get; set; }
        public double arb
        { get; set; }
        public double crb
        { get; set; }

        //Slope
        public double S
        { get; set; }

        //Rib
        public double nst
        { get; set; }
        public double Hst
        { get; set; }
        public double tst
        { get; set; }
        public double nsb
        { get; set; }
        public double Hsb
        { get; set; }
        public double tsb
        { get; set; }
        public double ns
        { get; set; }
        public double ds1
        { get; set; }
        public double ds2
        { get; set; }

        //
        public double Lp
        { get; set; }
        public double d0
        { get; set; }
    }
}
