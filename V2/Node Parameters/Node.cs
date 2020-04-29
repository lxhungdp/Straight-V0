using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public class Node
    {
        // Input dimension
        public string Label { get; set; } //Node name
        public double Sta { get; set; } //Station
        public double R { get; set; } //Radius

        //Top flange
        public double ntop { get; set; }
        public double btop { get; set; }
        public double ttop { get; set; }
        public double ctop { get; set; }
        
        //Bottom flange
        public double bbot { get; set; }
        public double tbot { get; set; }
        public double cbot { get; set; }
        
        //Web
        public double D { get; set; }
        public double tw { get; set; }

        //Bottom concrete
        public double Hc { get; set; }

        //Deck slab
        public double ts { get; set; }
        public double bs { get; set; }
        public double th { get; set; }
        public double bh { get; set; }
        public double b { get; set; }
        public double w { get; set; }
        public double a { get; set; }

        //Top layer of rebar in slab
        public double drt { get; set; }
        public double art { get; set; }
        public double crt { get; set; }

        //Bottom layer of rebar in slab
        public double drb { get; set; }
        public double arb { get; set; }
        public double crb { get; set; }

        //Total area of rebar in bottom concrete
        public double srb { get; set; }

        //Bottom Rib
        public double nsb { get; set; }
        public double Hsb { get; set; }
        public double tsb { get; set; }

        //Bottom Rib
        public double nst { get; set; }
        public double Hst { get; set; }
        public double tst { get; set; }

        

        //ds, d0 and Lb
        public double ds { get; set; }
        public double d0 { get; set; }
        public double Lb { get; set; }

        //Moment, Shear and Torsion

        public double M1 { get; set; }
        public double M2 { get; set; }
        public double M3 { get; set; }
        public double M4 { get; set; }
        public double Mw { get; set; }
        public double MTmax { get; set; }
        public double MTmin { get; set; }
        public double MLmax { get; set; }
        public double MLmin { get; set; }

        public double S1 { get; set; }
        public double S2 { get; set; }
        public double S3 { get; set; }
        public double S4 { get; set; }
        public double Sw { get; set; }
        public double STmax { get; set; }
        public double STmin { get; set; }
        public double SLmax { get; set; }
        public double SLmin { get; set; }

        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T3 { get; set; }
        public double T4 { get; set; }
        public double Tw { get; set; }
        public double TTmax { get; set; }
        public double TTmin { get; set; }
        public double TLmax { get; set; }
        public double TLmin { get; set; }
    }
}
