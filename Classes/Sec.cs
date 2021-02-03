using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Classes
{
    public class Sec
    {
        
        private double ntop, btop, ttop, ctop, bbot, tbot, cbot, D, tw, Hc, ts, bs, th, bh, b, w, a, drt, art, crt, drb, arb, crb, nsb, Hsb, tsb,
            nst, Hst, tst, S, Hw, Ac, Ic, As1, Is1, Ah, Ih, Srt, Srb, Irt, Irb;

        private double teq, tseq, angle, Iyweb, Iy2web, Iybotrib, Iytoprib, Iycon, Iydeck, Iytop, Iybot, nEb, nEd;
        public Sec(Node Node, double nEb, double nEd, int index, double Station)
        {
            this.Element = "G" + Node.Joint.ToString();
            this.Node = Node.Joint + index ;
            this.Station = Station == 0 ?  Node.X : Station;
            this.ntop = Node.ntop;
            this.btop = Node.btop;
            this.ttop = Node.ttop;
            this.ctop = Node.ctop;

            this.bbot = Node.bbot;
            this.tbot = Node.tbot;
            this.cbot = Node.cbot;

            this.D = Node.D;
            this.tw = Node.tw;
            this.Hc = Node.Hc;
            this.ts = Node.ts;
            this.bs = Node.bs;
            this.th = Node.th;
            this.bh = Node.bh;
            this.b = Math.Max(Node.bleft, Node.bright);
            this.w = Node.w;
            this.a = Math.Max(Node.aleft, Node.aright);

            this.drt = Node.drt;
            this.art = Node.art;
            this.crt = Node.crt;
            this.drb = Node.drb;
            this.arb = Node.arb;
            this.crb = Node.crb;            

            this.nsb = Node.nsb;
            this.Hsb = Node.Hsb;
            this.tsb = Node.tsb;
            this.nst = Node.nst;
            this.Hst = Node.Hst;
            this.tst = Node.tst;

            this.S = Node.S;
            this.Hw = Node.Hw;
            this.Ac = Node.Ac;
            this.Ic = Node.Ic;
            this.As1 = Node.As1;
            this.Is1 = Node.Is1;
            this.Ah = Node.Ah;
            this.Ih = Node.Ih;
            this.Srt = Node.Srt;
            this.Srb = Node.Srb;
            this.Irt = Node.Irt;
            this.Irb = Node.Irb;

            this.nEb = nEb;
            this.nEd = nEd;

            //Assuming teq = 1mm
            this.teq = 1;

            // Determine tseq
            this.tseq = ts * (30702 / (2 * (1 + 1.0 / 6))) / (210000 / (2 * (1 + 0.3)));

            //Determine Iy for one web with exactly equation Yc = [a*b*cos (b^2 + a^2 * sin^2)] / 12
            this.angle = Math.Atan(S);
            this.Iyweb = (Hw * tw / Math.Cos(angle) * Math.Cos(angle) * (Math.Pow(tw / Math.Cos(angle), 2) + Hw * Hw * Math.Pow(Math.Sin(angle), 2))) / 12;
            this.Iy2web = 2 * Iyweb + 2 * Hw * tw * Math.Pow(bbot / 2 - cbot + tw / Math.Cos(angle) / 2 + D / 2 * S, 2);

            // Determine Iy for bottom rib
            if (nsb == 1)
                this.Iybotrib = nsb * 1.0 / 12 * Hsb * tsb * tsb * tsb;
            else if (nsb == 2)
                this.Iybotrib = nsb * 1.0 / 12 * Hsb * tsb * tsb * tsb + 2 * Hsb * tsb * Math.Pow((bbot - 2 * cbot) / 6, 2);
            else if (nsb == 3)
                this.Iybotrib = nsb * 1.0 / 12 * Hsb * tsb * tsb * tsb + 2 * Hsb * tsb * Math.Pow((bbot - 2 * cbot) / 4, 2);
            else if (nsb == 4)
                this.Iybotrib = nsb * 1.0 / 12 * Hsb * tsb * tsb * tsb + 2 * Hsb * tsb * Math.Pow((bbot - 2 * cbot) / 10.0, 2) + 2 * Hsb * tsb * Math.Pow((bbot - 2 * cbot) * 3 / 10.0, 2);
            else if (nsb == 5)
                this.Iybotrib = nsb * 1.0 / 12 * Hsb * tsb * tsb * tsb + 2 * Hsb * tsb * Math.Pow((bbot - 2 * cbot) / 12.0, 2) + 2 * Hsb * tsb * Math.Pow((bbot - 2 * cbot) / 6, 2);
            else
                this.Iybotrib = 0;

            // Determine Iy for top rib
            if (nst == 1)
                this.Iytoprib = nst * 1.0 / 12 * Hst * tst * tst * tst;
            else if (nst == 2)
                this.Iytoprib = nst * 1.0 / 12 * Hst * tst * tst * tst + 2 * Hst * tst * Math.Pow(w / 6, 2);
            else if (nst == 3)
                this.Iytoprib = nst * 1.0 / 12 * Hst * tst * tst * tst + 2 * Hst * tst * Math.Pow(w / 4, 2);
            else if (nst == 4)
                this.Iytoprib = nst * 1.0 / 12 * Hst * tst * tst * tst + 2 * Hst * tst * Math.Pow(w / 10.0, 2) + 2 * Hst * tst * Math.Pow(w * 3 / 10.0, 2);
            else if (nst == 5)
                this.Iytoprib = nst * 1.0 / 12 * Hst * tst * tst * tst + 2 * Hst * tst * Math.Pow(w / 12.0, 2) + 2 * Hst * tst * Math.Pow(w / 6, 2);
            else
                this.Iytoprib = 0;

            // Determine Iy for bottom concrete Iy = h (a+b) (a^2 + b^2) / 48
            this.Iycon = (Hc * ((bbot - 2 * cbot) + (bbot - 2 * cbot) + 2 * Hc * S) * (Math.Pow(bbot - 2 * cbot, 2) + Math.Pow((bbot - 2 * cbot) + 2 * Hc * S, 2))) / 48;

            // Determine Iy for deckslab
            this.Iydeck = 1.0 / 12 * ts * bs * bs * bs;

            // Determine Iy for top flange
            if (ntop == 1)
                this.Iytop = 1.0 / 12 * ttop * btop * btop * btop;
            else
                this.Iytop = 2 * 1.0 / 12 * ttop * btop * btop * btop + 2 * ttop * btop * Math.Pow(w / 2 + ctop - btop / 2, 2);

            //Determine Iy for bottom flange
            this.Iybot = 1.0 / 12 * tbot * bbot * bbot * bbot;


        }



        public string Element
        {
            get; set;

        }
        public int Node
        {
            get; set;

        }  
        public double Station
        {
            get; set;
        }

        // Stage 1: Steel only
        public double A1
        {
            get { return ntop * btop * ttop + bbot * tbot + Hw * tw * 2 + nsb * tsb * Hsb + nst * tst * Hst; }

        }

        public double YL1
        {
            get { return (ntop * btop * ttop * (0.5 * ttop + D + tbot) + 2 * Hw * tw * (0.5 * D + tbot) + bbot * tbot * 0.5 * tbot + nsb * Hsb * tsb * (0.5 * Hsb + tbot) + nst * Hst * tst * (D - 0.5 * Hst + tbot)) / A1; }
        }

        public double YU1
        {
            get { return tbot + D + ttop - YL1; }
        }

        public double Ix1
        {
            get { return 1.0 / 12.0 * ntop * btop * ttop * ttop * ttop + ntop * btop * ttop * (YU1 - ttop / 2) * (YU1 - ttop / 2) + 1.0 / 12.0 / (S * S + 1) * 2 * tw * Hw * Hw * Hw + 2 * Hw * tw * (YL1 - tbot - D / 2) * (YL1 - tbot - D / 2) + 1.0 / 12.0 * bbot * tbot * tbot * tbot + bbot * tbot * (YL1 - tbot / 2) * (YL1 - tbot / 2) + 1.0 / 12.0 * nsb * tsb * Hsb * Hsb * Hsb + nsb * tsb * Hsb * (YL1 - tbot - Hsb / 2) * (YL1 - tbot - Hsb / 2) + 1.0 / 12.0 * nst * tst * Hst * Hst * Hst + nst * tst * Hst * (YU1 - ttop - Hst / 2) * (YU1 - ttop - Hst / 2); }
        }

        public double SL1
        {
            get { return Ix1 / YL1; }
        }

        public double SU1
        {
            get { return Ix1 / YU1; }
        }

        public double Iy1
        {
            get { return Iybot + Iytop + Iy2web + Iytoprib + Iybotrib; }
        }


        public double J1
        {
            get
            {
                if (ntop == 1)
                    return 4 * Math.Pow((bbot - 2 * cbot + D * S) * (tbot / 2 + D + ttop / 2), 2) / (w / ttop + bbot / tbot + 2 * Hw / tw);
                else
                    return 4 * Math.Pow((bbot - 2 * cbot + D * S) * (tbot / 2 + D + teq / 2), 2) / (w / teq + bbot / tbot + 2 * Hw / tw);
            }
        }

        //Calculating for Stage 2-1: Steel + bottom concrete (shorttime)     

        public double A2s
        {
            get { return A1 + Ac / nEb; }
        }

        public double YL2s
        {
            get { return (A1 * YL1 + Ac * (Hc / 2 + tbot) / nEb) / A2s; }
        }

        public double YU2s
        {
            get { return D + tbot + ttop - YL2s; }
        }

        public double Ix2s
        {
            get { return Ix1 + A1 * (YL2s - YL1) * (YL2s - YL1) + Ic / nEb + Ac / nEb * (YL2s - tbot - Hc / 2) * (YL2s - tbot - Hc / 2); }
        }

        public double SL2s
        {
            get { return Ix2s / YL2s; }
        }

        public double SU2s
        {
            get { return Ix2s / YU2s; }
        }

        public double Iy2s
        {
            get { return Iy1 + Iycon / nEb; }
        }

        public double J2s
        {
            get { return J1; }
        }

        //Calculating for Stage 2-2: Steel + bottom concrete (longtime)

        public double A2l
        {
            get { return A1 + Ac / 3.0 / nEb; }
        }

        public double YL2l
        {
            get { return (A1 * YL1 + Ac * (Hc / 2 + tbot) / 3 / nEb) / A2l; }
        }

        public double YU2l
        {
            get { return D + tbot + ttop - YL2l; }
        }

        public double Ix2l
        {
            get { return Ix1 + A1 * (YL2l - YL1) * (YL2l - YL1) + Ic / 3 / nEb + Ac / 3 / nEb * (YL2l - tbot - Hc / 2) * (YL2l - tbot - Hc / 2); }
        }

        public double SL2l
        {
            get { return Ix2l / YL2l; }
        }

        public double SU2l
        {
            get { return Ix2l / YU2l; }
        }
        public double Iy2l
        {
            get { return Iy1 + Iycon / 3.0 / nEb; }
        }

        public double J2l
        {
            get { return J1; }
        }

        //Calculating for Stage 3-1: Steel + Deck (Shorttime) 
        public double A3s
        {
            get { return A1 + (As1 + Ah) / nEd; }
        }

        public double YL3s
        {
            get { return (A1 * YL1 + As1 * (tbot + D + th + ts / 2) / nEd + Ah * (tbot + D + 2 * th / 3.0) / nEd) / A3s; }
        }

        public double YU3s
        {
            get { return D + tbot + ttop - YL3s; }
        }

        public double Ix3s
        {
            get { return Ix1 + A1 * (YL3s - YL1) * (YL3s - YL1) + Is1 / nEd + As1 / nEd * (YU3s - ttop + th + ts / 2.0) * (YU3s - ttop + th + ts / 2) + Ih / nEd + Ah / nEd * (YU3s - ttop + 2 * th / 3) * (YU3s - ttop + 2 * th / 3); }
        }

        public double SL3s
        {
            get { return Ix3s / YL3s; }
        }

        public double SU3s
        {
            get { return Ix3s / YU3s; }
        }
        public double Iy3s
        {
            get { return Iy1 + Iydeck / nEd; }
        }

        public double J3s
        {
            get
            {
                //return tseq;
                return 4 * Math.Pow((bbot - 2 * cbot + D * S) * (tbot / 2 + D + tseq / 2), 2) / (w / tseq + bbot / tbot + 2 * Hw / tw);
            }
        }

        //Calculating for Stage 3-1: Steel + Deck (longtime) 

        public double A3l
        {
            get { return A1 + (As1 + Ah) / 3 / nEd; }
        }

        public double YL3l
        {
            get { return (A1 * YL1 + As1 * (tbot + D + th + ts / 2) / 3 / nEd + Ah * (tbot + D + 2 * th / 3) / 3 / nEd) / A3l; }
        }

        public double YU3l
        {
            get { return D + tbot + ttop - YL3l; }
        }

        public double Ix3l
        {
            get { return Ix1 + A1 * (YL3l - YL1) * (YL3l - YL1) + Is1 / 3 / nEd + As1 / 3 / nEd * (YU3l - ttop + th + ts / 2) * (YU3l - ttop + th + ts / 2) + Ih / 3 / nEd + Ah / 3 / nEd * (YU3l - ttop + 2 * th / 3) * (YU3l - ttop + 2 * th / 3); }
        }

        public double SL3l
        {
            get { return Ix3l / YL3l; }
        }

        public double SU3l
        {
            get { return Ix3l / YU3l; }
        }
        public double Iy3l
        {
            get { return Iy1 + Iydeck / 3 / nEd; }
        }

        public double J3l
        {
            get
            {
                return J3s;
            }
        }

        //Calculating for Stage 4-1: Steel + Bottom concrete + Rebar shortime 


        public double A4s
        {
            get { return A2s + Srt + Srb; }
        }

        public double YL4s
        {
            get { return (A2s * YL2s + Srt * (tbot + D + th + ts - crt) + Srb * (tbot + D + th + crb)) / A4s; }
        }

        public double YU4s
        {
            get { return D + tbot + ttop - YL4s; }
        }

        public double Ix4s
        {
            get { return Ix2s + A2s * (YL4s - YL2s) * (YL4s - YL2s) + Irt + Srt * (YU4s - ttop + th + ts - crt) * (YU4s - ttop + th + ts - crt) + Irb + Srb * (YU4s - ttop + th + crb) * (YU4s - ttop + th + crb); }
        }

        public double SL4s
        {
            get { return Ix4s / YL4s; }
        }

        public double SU4s
        {
            get { return Ix4s / YU4s; }
        }
        public double Iy4s
        {
            get { return Iy3s; }
        }

        public double J4s
        {
            get { return J3s; }
        }

        //Calculating for Stage 4-2: Steel + Bottom concrete + Rebar longtime 

        public double A4l
        {
            get { return A2l + Srt / 3 + Srb / 3; }
        }

        public double YL4l
        {
            get { return (A2l * YL2l + Srt / 3 * (tbot + D + th + ts - crt) + Srb / 3 * (tbot + D + th + crb)) / A4l; }
        }

        public double YU4l
        {
            get { return D + tbot + ttop - YL4l; }
        }

        public double Ix4l
        {
            get { return Ix2l + A2l * (YL4l - YL2l) * (YL4l - YL2l) + Irt / 3 + Srt / 3 * (YU4l - ttop + th + ts - crt) * (YU4l - ttop + th + ts - crt) + Irb / 3 + Srb / 3 * (YU4l - ttop + th + crb) * (YU4l - ttop + th + crb); }
        }

        public double SL4l
        {
            get { return Ix4l / YL4l; }
        }

        public double SU4l
        {
            get { return Ix4l / YU4l; }
        }

        public double Iy4l
        {
            get { return Iy3l; }
        }

        public double J4l
        {
            get { return J3l; }
        }


        //Calculating for Stage 5: Steel + Bottom Concrete + Deck (Shorttime) 
        public double A5s
        {
            get { return A1 + Ac / nEb +  (As1 + Ah) / nEd; }
        }

        public double YL5s
        {
            get { return (A1 * YL1 + Ac * (Hc / 2 + tbot) / nEb + As1 * (tbot + D + th + ts / 2) / nEd + Ah * (tbot + D + 2 * th / 3.0) / nEd) / A5s; }
        }

        public double YU5s
        {
            get { return D + tbot + ttop - YL5s; }
        }

        public double Ix5s
        {
            get { return Ix1 + A1 * (YL5s - YL1) * (YL5s - YL1) + Ic / nEb + Ac / nEb * (YL5s - tbot - Hc / 2) * (YL5s - tbot - Hc / 2) + Is1 / nEd + As1 / nEd * (YU5s - ttop + th + ts / 2.0) * (YU5s - ttop + th + ts / 2) + Ih / nEd + Ah / nEd * (YU5s - ttop + 2 * th / 3) * (YU5s - ttop + 2 * th / 3); }
        }

        public double SL5s
        {
            get { return Ix5s / YL5s; }
        }

        public double SU5s
        {
            get { return Ix5s / YU5s; }
        }
        public double Iy5s
        {
            get { return Iy1 + Iycon / nEb + Iydeck / nEd; }
        }

        public double J5s
        {
            get
            {
                //return tseq;
                return 4 * Math.Pow((bbot - 2 * cbot + D * S) * (tbot / 2 + D + tseq / 2), 2) / (w / tseq + bbot / tbot + 2 * Hw / tw);
            }
        }

       


    }
}
