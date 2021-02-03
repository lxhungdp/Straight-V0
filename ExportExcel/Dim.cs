using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportExcel
{
    public class Dim
    {
        public int Joint { get; set; } //Node name
        public string Label { get; set; } //Station
        public double X { get; set; } //Station
        public double R { get; set; } //Radius
        
        //Top flange
        public double ntop { get; set; }
        public double btop { get; set; }
        public double ttop { get; set; }

        //Bottom flange
        public double bbot { get; set; }
        public double tbot { get; set; }
        public double cbot { get; set; }
        //Web
        public double D { get; set; }
        public double tw { get; set; }

        public double S { get; set; }

        //Bottom concrete
        public double Hc { get; set; }

        //Deck slab
        public double ts { get; set; }        
        public double th { get; set; }
        public double bh { get; set; }
        public double w { get; set; }
        public double bleft { get; set; }
        public double bright { get; set; }
        public double aleft { get; set; }
        public double aright { get; set; }
        public double Leff { get; set; }
        public double bs { get; set; }

        //Stiffnener
        public double ns { get; set; }
        public double d0 { get; set; }

        //Bottom Rib
        public double nsb { get; set; }
        public double Hsb { get; set; }
        public double tsb { get; set; }

        //Bottom Rib
        public double nst { get; set; }
        public double Hst { get; set; }
        public double tst { get; set; }

        //Top layer of rebar in slab
        public double drt { get; set; }
        public double art { get; set; }
        public double crt { get; set; }
        public double Srt { get; set; }

        //Bottom layer of rebar in slab
        public double drb { get; set; }
        public double arb { get; set; }
        public double crb { get; set; }
        public double Srb { get; set; }

        //Bottom concrete area
        public double Srbot { get; set; }
        


        

    }
}
