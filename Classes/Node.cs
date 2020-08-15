using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Node
    {
        public Node ShallowCopy()
        {
            return (Node)this.MemberwiseClone();
        }
        
        public double Joint   //101 102 103 201 202 203. Maximum 100 node
        { get; set; }
        
        public double Type    //1 - Abu, 2 - Pier, 3 - Cross, 4 Section changed
        { get; set; }    

        public string Label
        { get; set; }
        public double BeamID // 1, 2, 3 girder 123, 11 21 31 Stringer
        { get; set; }
        public double Haunch // 0 - constant web depth, 1 - variable web depth
        { get; set; }

        public double X
        { get; set; }
        public double Y
        { get; set; }
        public double Z
        { get; set; }
        public string Restrain //Fixed, LongFixed, TranFixed, Free
        { get; set; }

        //Top flange

        public double ntop
        { get; set; }
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
        public double w
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
       

        //
        public double Lb
        { get; set; }
        public double d0
        { get; set; }
    }
}
