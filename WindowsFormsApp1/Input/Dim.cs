using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checking
{
    public class Dim
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
        public double ns { get; set; }
        public double d0 { get; set; }
        public double Lb { get; set; }

        public string Type { get; set; }

        //Additional calculate
        // The slope

        public double S
        {
            get {return (w - (bbot - (2 * cbot))) / 2.0 / D; }
        }
        
        // The length of web along the slope
        public double Hw
        {
            get { return Math.Sqrt(D * D + D * S * D * S); }
        }

        public double Ac
        {
            get { return (bbot - 2 * cbot) * Hc + Hc * S * Hc; }
        }

        public double Ic
        {
            get { return Hc * Hc * Hc * ((bbot - 2 * cbot) * (bbot - 2 * cbot) + 4 * (bbot - 2 * cbot) * ((bbot - 2 * cbot) + 2 * Hc * S) + ((bbot - 2 * cbot) + 2 * Hc * S) * ((bbot - 2 * cbot) + 2 * Hc * S)) / 36.0 / ((bbot - 2 * cbot) + (bbot - 2 * cbot) + 2 * Hc * S); }
        }

        public double As
        {
            get { return bs * ts; }
        }

        public double Is1
        {
            get { return bs * ts * ts * ts / 12.0; }
        }

        public double Ah
        {
            get { return 2 * bh * th; }
        }

        public double Ih
        {
            get { return 4 * bh * th * th * th / 36.0; }
        }

        public double Art
        {
            get { return Math.Floor(bs / art) * 0.25 * Math.PI * drt * drt; }
        }

        public double Arb
        {
            get { return Math.Floor(bs / arb) * 0.25 * Math.PI * drb * drb; }
        }

        public double Irt
        {
            get { return Math.Floor(bs / art) * Math.PI * drt * drt * drt * drt / 64.0; }
        }

        public double Irb
        {
            get { return Math.Floor(bs / arb) * Math.PI * drb * drb * drb * drb / 64.0; }
        }


    }
}
