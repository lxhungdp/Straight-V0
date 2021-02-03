using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Node
    {
        
        public Node (NodeInput Node, double Dnew, int span, EffectiveWidth EW)
        {            
            this.Joint = Node.Joint;
            
            this.BeamID = Node.BeamID;
            this.Type = Node.Type;
            this.Label = Node.Label;
            this.Haunch = Node.Haunch;
            this.X = Node.X;
            this.Y = Node.Y;
            this.Z = Node.Z;
            this.Restrain = Node.Restrain;
            this.ntop = Node.ntop;
            this.btop = Node.ntop == 2 ? Node.btop : (Node.w + 2 * Node.ctop);
            this.ctop = Node.ctop;
            this.ttop = Node.ttop;            
           
            this.tbot = Node.tbot;
            this.cbot = Node.cbot;

            this.D = span > 1 ? Dnew : Node.D;
            this.tw = Node.tw;
            this.Hc = Node.Hc;

            this.ts = Node.ts;
            this.th = Node.th;
            this.bh = Node.bh;
            this.w = Node.w;            
           
            this.drt = Node.drt;
            this.art = Node.art;
            this.crt = Node.crt;
            this.drb = Node.drb;
            this.arb = Node.arb;
            this.crb = Node.crb;

            this.S = Node.S;
            this.nst = Node.nst;
            this.Hst = Node.Hst;
            this.tst = Node.tst;
            this.nsb = Node.nsb;
            this.Hsb = Node.Hsb;
            this.tsb = Node.tsb;

            this.ns = Node.ns;
            this.Lb = Node.Lb;
            this.d0 = Node.d0;

            this.bleft = EW.bleft;
            this.bright = EW.bright;
            this.aleft = EW.aleft;
            this.aright = EW.aright;
            this.bs = EW.beff;
            this.Leff = EW.Leff;
            this.e = EW.e;

            //Neglect the rebar in bottom concrete
            this.Srbot = 0;

        }
        
        public int Joint   //101 102 103 201 202 203. Maximum 100 node
        { get; set; }

       
        public int BeamID // 1, 2, 3 girder 123, 11 21 31 Stringer
        { get; set; }

        public int Type    //1 - Abu, 2 - Pier, 3 - Cross, 4 Section changed
        { get; set; }    

        public string Label
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
        
        public double bleft
        { get; set; }

        public double bright
        { get; set; }

        public double aleft
        { get; set; }

        public double aright
        { get; set; }
        public double Leff
        { get; set; }
        public double bs
        { get; set; }
        public double e
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

        //Total rebar in bottom concrete
        public double Srbot
        { get; set; }

        //Additional calculate   
        public double bbot
        {
            get { return w - 2 * D * Math.Tan(S) + 2 * cbot; }
        }

        // The length of web along the slope
        public double Hw
        {
            get { return D / Math.Cos(S); }
        }

        public double Ac
        {
            get { return (bbot - 2 * cbot) * Hc + Hc * Math.Tan(S) * Hc; }
        }

        public double Ic
        {
            get { return Hc * Hc * Hc * ((bbot - 2 * cbot) * (bbot - 2 * cbot) + 4 * (bbot - 2 * cbot) * ((bbot - 2 * cbot) + 2 * Hc * S) + ((bbot - 2 * cbot) + 2 * Hc * S) * ((bbot - 2 * cbot) + 2 * Hc * S)) / 36.0 / ((bbot - 2 * cbot) + (bbot - 2 * cbot) + 2 * Hc * S); }
        }

        public double As1
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

        public double Srt
        {
            get { return Math.Floor(bs / art) * 0.25 * Math.PI * drt * drt; }
        }

        public double Srb
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
