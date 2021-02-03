using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Check_ULS
    {
        private Mat Flange, Web, Deck, Botcon, Rib, Rebar;
        private double As1, nsb, Hsb, tsb, nst, Hst, tst, Srb, Srt, Srbot, Ac, crb, crt, Hc, SL1, SU1, SU2l, SL2l, SU3l, SL3l, SU3s, SL3s, SU4s, SL4s, SU4l, SL4l,
            _ttop, _tbot, _tw, Fytop, Fybot, Fyweb, YL2s, YL3s, YL4s, Ix2s, Ix3s, Ix4s, nEd, nEb, YU3s, YU4s, ns;

        public Check_ULS(Node Node, Sec Sec, Stress Stress, ElmForces Moment, ElmForces Torsion, ElmForces Shear, List<Mat> Mat)
        {
            this.Element = Sec.Element;
            this.Joint = Sec.Node;
            this.Station = Sec.Station;
            this.Label = Node.Label;

            this.ntop = Node.ntop;
            this.btop = Node.btop;
            this.bbot = Node.bbot;
            this.cbot = Node.cbot;
            this.w = Node.w;
            this.As1 = Node.As1;
            this.nsb = Node.nsb;
            this.Hsb = Node.Hsb;
            this.tsb = Node.tsb;
            this.nst = Node.nst;
            this.Hst = Node.Hst;
            this.tst = Node.tst;
            this.Srb = Node.Srb;
            this.Srt = Node.Srt;
            this.Srbot = Node.Srbot;
            this.Ac = Node.Ac;
            this.crb = Node.crb;
            this.crt = Node.crt;
            this.Hc = Node.Hc;
            this.ttop = Node.ttop;
            this.tbot = Node.tbot;
            this.D = Node.D;
            this.th = Node.th;
            this.ts = Node.ts;
            this.R = 0; //Assume for straight bridge
            this.ns = Node.ns;
            this.Dtw = Node.Hw / Node.tw;
            this.a = Math.Max(Node.aleft, Node.aright) - Node.w;
            this.b = Math.Max(Node.bleft, Node.bright) - Node.w / 2;
            this.S = Node.S;
            this.tw = Node.tw;
            this.d0 = Node.d0;
            this.Hw = Node.Hw;

            //Material
            this.Flange = Mat[0];
            this.Web = Mat[1];
            this.Deck = Mat[2];
            this.Botcon = Mat[3];
            this.Rib = Mat[4];            
            this.Rebar = Mat[5];

            this._ttop = Node.ttop;
            this._tbot = Node.tbot;
            this._tw = Node.tw;
            this.Fytop = Flange.Fys(this._ttop);
            this.Fybot = Flange.Fys(this._tbot);
            this.Fyweb = Web.Fys(this._tw);

            this.nEd = Flange.Es / Deck.Ec;
            this.nEb = Flange.Es / Botcon.Ec;

            //Force
            this.M1 = Moment.DC1;
            this.M2 = Moment.DC2;
            this.M3 = Moment.DC3;
            this.M4 = Moment.DC4;
            this.Mw = Moment.DW;
            this.MLLmax = Moment.LLmax;
            this.MLLmin = Moment.LLmin;

            this.T1 = Torsion.DC1;
            this.T2 = Torsion.DC2;
            this.T3 = Torsion.DC3;
            this.T4 = Torsion.DC4;
            this.TTw = Torsion.DW;
            this.TLLmax = Torsion.LLmax;
            this.TLLmin = Torsion.LLmin;

            this.S1 = Shear.DC1;
            this.S2 = Shear.DC2;
            this.S3 = Shear.DC3;
            this.S4 = Shear.DC4;
            this.Sw = Shear.DW;
            this.SLLmax = Shear.LLmax;
            this.SLLmin = Shear.LLmin;

            //Sectional Properties
            this.SL1 = Sec.SL1;
            this.SU1 = Sec.SU1;

            this.Ix2s = Sec.Ix2s;
            this.YL2s = Sec.YL2s;
            this.SU2l = Sec.SU2l;
            this.SL2l = Sec.SL2l;

            this.SU3l = Sec.SU3l;
            this.SL3l = Sec.SL3l;

            this.SU4l = Sec.SU4l;
            this.SL4l = Sec.SL4l;

            this.Ix3s = Sec.Ix3s;
            this.YU3s = Sec.YU3s;
            this.YL3s = Sec.YL3s;
            this.SU3s = Sec.SU3s;
            this.SL3s = Sec.SL3s;

            this.Ix4s = Sec.Ix4s;
            this.YU4s = Sec.YU4s;
            this.YL4s = Sec.YL4s;
            this.SU4s = Sec.SU4s;
            this.SL4s = Sec.SL4s;




            //Stress
            this.Flexure = Stress.Su_top <= 0 ? "Positive" : "Negative";
            this.Su_top = Stress.Su_top;
            this.Su_bot = Stress.Su_bot;


        }

        //Table 1

        public string Element
        {
            get; set;

        }
        public int Joint
        {
            get; set;

        }
        public double Station
        {
            get; set;

        }
        public string Label
        {
            get; set;

        }



        //1. Determine the location of PNA
        public double Ptop
        {
            get { return ntop * btop * ttop * Fytop / 1000; }
        }

        public double Pw
        {
            get { return Hw * tw * 2 * Fyweb / 1000; }
        }

        public double Pbot
        {
            get { return bbot * tbot * Fybot / 1000; }
        }


        public double Ps
        {
            get { return 0.85 * Deck.fc * As1 / 1000; }
        }
        public double Fyrbot
        {
            get { return Rib.Fys(tsb); }
        }

        public double Psb
        {
            get { return nsb * tsb * Hsb * Fyrbot / 1000; }
        }

        public double Fyrtop
        {
            get { return Rib.Fys(tst); }
        }

        public double Pst
        {
            get { return nst * tst * Hst * Fyrtop / 1000; }
        }

        public double Prb
        {
            get { return Srb * Rebar.Fy / 1000; }
        }

        public double Prt
        {
            get { return Srt * Rebar.Fy / 1000; }
        }

        //Assume area of bottom concrete rebar = 0
        public double Prbot
        {
            get { return Srbot * Rebar.Fy / 1000; }
        }

        public double Pcom
        {
            get { return Ac * 0.85 * Botcon.fc / 1000; }
        }

        public string PNA
        {
            get
            {
                string Location;
                if (Flexure == "Positive")
                {
                    if (Pbot + Psb + (D - 2 * Hst) / D * Pw + Prbot >= Pst + Ptop + Ps + Prb + Prt)
                        Location = "1";
                    else if (Pbot + Psb + Pw + Prbot + Pst >= Ptop + Ps + Prb + Prt)
                        Location = "a";
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop >= Ps + Prb + Prt)
                        Location = "2";
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop >= ((ts - crb) / ts) * Ps + Prb + Prt)
                        Location = "3";
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop + Prb >= ((ts - crb) / ts) * Ps + Prt)
                        Location = "4";
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop + Prb >= (crt / ts) * Ps + Prt)
                        Location = "5";
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop + Prb + Prt >= (crt / ts) * Ps)
                        Location = "6";
                    else
                        Location = "7";
                }
                else
                {
                    if (Pbot + Psb + Hsb / Hc * Pcom + Hsb / D * Pw >= (D - Hsb) * Pw / D + Pst + Ptop + Prb + Prt)
                        Location = "b";
                    else if (Pbot + Psb + Pcom + Hc / D * Pw >= (D - Hc) * Pw / D + Pst + Ptop + Prb + Prt)
                        Location = "c";
                    else if (Pbot + Psb + Prbot + Pcom + (D - Hst) / D * Pw >= Pst + Ptop + Prb + Prt)
                        Location = "8";
                    else if (Pbot + Psb + Prbot + Pcom + Pw + Pst >= Ptop + Prb + Prt)
                        Location = "d";
                    else
                        Location = "9";
                }
                return Location;
            }
        }

        public double Ypna
        {
            get
            {
                double Ypna = 0;
                switch (PNA)
                {
                    case "1":
                        {
                            Ypna = D / 2 * ((Pbot + Psb + Prbot - Pst - Ptop - Ps - Prb - Prt) / Pw + 1);
                        }
                        break;
                    case "a":
                        {
                            Ypna = (Pbot + Psb + Prbot + Pw + Pst - Ptop - Ps - Prb - Prt) / (2 / D * Pw + 2 / Hst * Pst);
                        }
                        break;
                    case "2":
                        {
                            Ypna = ttop / 2 * ((Pbot + Psb + Prbot + Pw + Pst - Ps - Prb - Prt) / Ptop + 1);
                        }
                        break;
                    case "3":
                        {
                            Ypna = ts * (Pbot + Psb + Prbot + Pw + Pst + Ptop - Prb - Prt) / Ps;
                        }
                        break;
                    case "4":
                        {
                            Ypna = ts - crb;
                        }
                        break;
                    case "5":
                        {
                            Ypna = ts * (Pbot + Psb + Prbot + Pw + Pst + Ptop + Prb - Prt) / Ps;
                        }
                        break;
                    case "6":
                        {
                            Ypna = crt;
                        }
                        break;
                    case "7":
                        {
                            Ypna = ts * (Pbot + Psb + Prbot + Pw + Pst + Ptop + Prb + Prt) / Ps;
                        }
                        break;
                    case "b":
                        {
                            Ypna = (Pbot + Pw + D / Hc * Pcom + (2 * D / Hsb - 1) * Psb - Ptop - Pst - Prb - Prt) / (2 / Hsb * Psb + Pcom / Hc + 2 / D * Pw);
                        }
                        break;
                    case "c":
                        {
                            Ypna = (Pbot + Pw + Psb + D / Hc * Pcom - Ptop - Pst - Prb - Prt) / (Pcom / Hc + 2 / D * Pw);
                        }
                        break;
                    case "8":
                        {
                            Ypna = D / 2 * ((Pbot + Psb + Prbot + Pcom - Pst - Ptop - Prb - Prt) / Pw + 1);
                        }
                        break;
                    case "d":
                        {
                            Ypna = (Pbot + Psb + Prbot + Pcom + Pst + Pw - Ptop - Prb - Prt) / (2 / Hst * Pst + 2 / D * Pw);
                        }
                        break;
                    case "9":
                        {
                            Ypna = ttop / 2 * ((Pbot + Psb + Prbot + Pcom + Pst + Pw - Prb - Prt) / Ptop + 1);
                        }
                        break;

                }
                return Ypna;
            }
        }

        public double Mp
        {
            get
            {
                double dtop, dbot, dsb, dst, ds, drb, drt, dr, dw, dcom;
                double Mp = 0;
                double Y = Ypna;
                switch (PNA)
                {

                    case "1":
                        {
                            
                            dtop = Y + ttop / 2;
                            dbot = D - Y + tbot / 2;
                            dsb = D - Y - Hsb / 2;
                            dst = Y - Hst / 2;
                            ds = Y + th + ts / 2;
                            drb = Y + th + crb;
                            drt = Y + th + ts - crt;
                            dr = D - Y - Hc / 2;
                            Mp = (Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Ps * ds + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "a":
                        {
                            dtop = Y + ttop / 2;
                            dbot = D - Y + tbot / 2;
                            dsb = D - Y - Hsb / 2;
                            ds = Y + th + ts / 2;
                            drb = Y + th + crb;
                            drt = Y + th + ts - crt;
                            dr = D - Y - Hc / 2;
                            Mp = (Pst / 2 / Hst * (Y * Y + (Hst - Y) * (Hst - Y)) + Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + Ptop * dtop + Psb * dsb + Pbot * dbot + Ps * ds + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "2":
                        {
                            dbot = ttop - Y + D + tbot / 2;
                            dw = ttop - Y + D / 2;
                            dsb = ttop - Y + D - Hsb / 2;
                            dst = ttop - Y + Hst / 2;
                            ds = ts / 2 + th - ttop + Y;
                            drb = crb + th - ttop + Y;
                            drt = ts - crt + th - ttop + Y;
                            dr = ttop - Y + D - Hc / 2;
                            Mp = (Ptop / 2 / ttop * (Y * Y + (ttop - Y) * (ttop - Y)) + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Ps * ds + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "3":
                        {
                            dtop = ts + th - Y - ttop / 2;
                            dbot = ts + th - Y + D + tbot / 2;
                            dw = ts + th - Y + D / 2;
                            dsb = ts + th - Y + D - Hsb / 2;
                            dst = ts + th - Y + Hst / 2;
                            drb = crb - (ts - Y);
                            drt = Y - crt;
                            dr = ts + th - Y + D - Hc / 2;
                            Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prb * drb + Prt * drt + Prbot * dr) / 1000;
                        }
                        break;
                    case "4":
                        {
                            dtop = ts + th - Y - ttop / 2;
                            dbot = ts + th - Y + D + tbot / 2;
                            dw = ts + th - Y + D / 2;
                            dsb = ts + th - Y + D - Hsb / 2;
                            dst = ts + th - Y + Hst / 2;
                            drt = Y - crt;
                            dr = ts + th - Y + D - Hc / 2;
                            Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prt * drt + Prbot * dr) / 1000;
                        }
                        break;
                    case "5":
                        {
                            dtop = ts + th - Y - ttop / 2;
                            dbot = ts + th - Y + D + tbot / 2;
                            dw = ts + th - Y + D / 2;
                            dsb = ts + th - Y + D - Hsb / 2;
                            dst = ts + th - Y + Hst / 2;
                            drb = ts - Y - crb;
                            drt = Y - crt;
                            dr = ts + th - Y + D - Hc / 2;
                            Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prt * drt + Prb * drb + Prbot * dr) / 1000;
                        }
                        break;
                    case "6":
                        {
                            dtop = ts + th - Y - ttop / 2;
                            dbot = ts + th - Y + D + tbot / 2;
                            dw = ts + th - Y + D / 2;
                            dsb = ts + th - Y + D - Hsb / 2;
                            dst = ts + th - Y + Hst / 2;
                            drb = ts - Y - crb;
                            dr = ts + th - Y + D - Hc / 2;
                            Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prb * drb + Prbot * dr) / 1000;
                        }
                        break;
                    case "7":
                        {
                            dtop = ts + th - Y - ttop / 2;
                            dbot = ts + th - Y + D + tbot / 2;
                            dw = ts + th - Y + D / 2;
                            dsb = ts + th - Y + D - Hsb / 2;
                            dst = ts + th - Y + Hst / 2;
                            drb = ts - Y - crb;
                            drt = Y - crt;
                            dr = ts + th - Y + D - Hc / 2;
                            Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prb * drb + Prt * drt + Prbot * dr) / 1000;
                        }
                        break;
                    case "b":
                        {
                            dbot = D - Y + tbot / 2;
                            dtop = Y + ttop / 2;
                            dst = Y - Hst / 2;
                            drb = Y + th + crb;
                            drt = Y + th + ts - crt;
                            dr = D - Y - Hc / 2;
                            Mp = (Psb / 2 / Hsb * ((D - Y) * (D - Y) + (Hsb - D + Y) * (Hsb - D + Y)) + Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + (D - Y) * (D - Y) / 2 / Hc * Pcom + Pbot * dbot + Pst * dst + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "c":
                        {
                            dbot = D - Y + tbot / 2;
                            dtop = Y + ttop / 2;
                            dst = Y - Hst / 2;
                            dsb = D - Y - Hsb / 2;
                            drb = Y + th + crb;
                            drt = Y + th + ts - crt;
                            dr = D - Y - Hc / 2;
                            Mp = (Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + (D - Y) * (D - Y) / 2 / Hc * Pcom + Pbot * dbot + Psb * dsb + Pst * dst + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "8":
                        {
                            dbot = D - Y + tbot / 2;
                            dtop = Y + ttop / 2;
                            dst = Y - Hst / 2;
                            dsb = D - Y - Hsb / 2;
                            drb = Y + th + crb;
                            drt = Y + th + ts - crt;
                            dr = D - Y - Hc / 2;
                            dcom = D - Y - Hc / 2;
                            Mp = (Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + Pcom * dcom + Pbot * dbot + Psb * dsb + Pst * dst + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "d":
                        {
                            dbot = D - Y + tbot / 2;
                            dtop = Y + ttop / 2;
                            dsb = D - Y - Hsb / 2;
                            drb = Y + th + crb;
                            drt = Y + th + ts - crt;
                            dr = D - Y - Hc / 2;
                            dcom = D - Y - Hc / 2;
                            Mp = (Pst / 2 / Hst * (Y * Y + (Hst - Y) * (Hst - Y)) + Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + Pcom * dcom + Pbot * dbot + Psb * dsb + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;
                    case "9":
                        {
                            dbot = ttop - Y + D + tbot / 2;
                            dw = ttop - Y + D / 2;
                            dsb = ttop - Y + D - Hsb / 2;
                            dst = ttop - Y + Hst / 2;
                            ds = ts / 2 + th - ttop + Y;
                            drb = crb + th - ttop + Y;
                            drt = ts - crt + th - ttop + Y;
                            dr = ttop - Y + D - Hc / 2;
                            dcom = ttop - Y + D - Hc / 2;
                            Mp = (Ptop / 2 / ttop * (Y * Y + (ttop - Y) * (ttop - Y)) + Pbot * dbot + Pcom * dcom + Psb * dsb + Pw * dw + Pst * dst + Prb * drb + Prt * drt + Prbot * dr) / 1000;

                        }
                        break;

                }
                return Mp;
            }
        }

        
        //Table 2.1

        public double M1
        {
            get; set;
        }
        public double M2
        {
            get; set;
        }
        public double M3
        {
            get; set;
        }
        public double M4
        {
            get; set;
        }
        public double Mw
        {
            get; set;
        }

        public double MD1
        {
            get { return 1.25 * (M1 + M2); }
        }
        public double MD2
        {
            get { return 1.25 * M3; }
        }
        public double MD3
        {
            get { return 1.25 * M4 + 1.5 * Mw; }
        }

        //Table 2.2
        public double STsteel
        {
            get { return Flexure == "Positive" ? SL1 : SU1; }
        }

        public double STbot
        {
            get { return Flexure == "Positive" ? SL1 : SU2l; }
        }

        public double STlongtime
        {
            get { return Flexure == "Positive" ? SL3l : SU4l; }
        }

        public double STshorttime
        {
            get { return Flexure == "Positive" ? SL3s : SU4s; }
        }

        public double Mad_t
        {
            get
            {
                if (Flexure == "Positive")
                    return (Fybot / 1000000 - MD1 / STsteel - MD2 / STbot - MD3 / STlongtime) * STshorttime;
                else
                    return -(Fytop / 1000000 + MD1 / STsteel + MD2 / STbot + MD3 / STlongtime) * STshorttime;
            }
        }

        public double SCsteel
        {
            get { return Flexure == "Positive" ? SU1 : SL1; }
        }

        public double SCbot
        {
            get { return Flexure == "Positive" ? SU1 : SL2l; }
        }

        public double SClongtime
        {
            get { return Flexure == "Positive" ? SU3l : SL4l; }
        }

        public double SCshorttime
        {
            get { return Flexure == "Positive" ? SU3s : SL4s; }
        }


        public double Mad_c
        {
            get
            {
                if (Flexure == "Positive")
                    return (Fytop / 1000000 - MD1 / SCsteel - MD2 / SCbot - MD3 / SClongtime) * SCshorttime;
                else
                    return -(Fybot / 1000000 + MD1 / SCsteel + MD2 / SCbot + MD3 / SClongtime) * SCshorttime;
            }
        }

        public double Mad
        {
            get { return Math.Min(Math.Abs(Mad_c), Math.Abs(Mad_t)); }
        }

        public double My
        {
            get { return Math.Abs(MD1 + MD2 + MD3) + Mad; }
        }

        public double Compare_Mp
        {
            get { return (My - Mp) / Mp; }
        }

        //Table 3

        public string Flexure
        {
            get; set;

        }

        //3. Checking Ductility Requirement

        public double Dp
        {
            get
            {
                double Dp = 0;
                switch (PNA)
                {
                    case "1":
                    case "a":
                        Dp = ts + th + Ypna;
                        break;
                    case "8":
                    case "b":
                    case "c":
                    case "d":
                        Dp = D - Ypna;
                        break;
                    case "2":
                        Dp = ts + th - ttop + Ypna;
                        break;
                    case "9":
                        Dp = ttop - Ypna + D;
                        break;
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                        Dp = Ypna;
                        break;
                }
                return Dp;
            }
        }

        public double ttop
        {
            get; set;
        }

        public double tbot
        {
            get; set;
        }
        public double D
        {
            get; set;
        }
        public double th
        {
            get; set;
        }
        public double ts
        {
            get; set;
        }

        public double Dt_duc
        {
            get { return Flexure == "Positive" ? (tbot + D + th + ts) : (ttop + D); }
        }

        public double O42Dt
        {
            get { return 0.42 * Dt_duc; }
        }

        public string CheckDuctility
        {
            get
            {
                if (Flexure != "Positive" && Hc == 0)
                    return "-";
                else
                    return Dp <= 0.42 * Dt_duc ? "OK" : "NG";
            }
        }

        public double CheckDuc_ratio
        {
            get
            {
                if (Flexure != "Positive" && Hc == 0)
                    return 1000000;
                else
                    return 0.42 * Dt_duc / Dp;
            }
        }

        //Table 4
        //4. Checking stress of concrete

        public double MLLmin
        {
            get; set;
        }
        public double MLLmax
        {
            get; set;
        }
        public double Sbot1
        {
            get { return Ix2s / (YL2s - tbot); }
        }

        public double Sbot2
        {
            get { return Ix4s / (YL4s - tbot); }
        }


        public double Sdeck
        {
            get { return Ix3s / (tbot + D + th + ts - YL3s); }
        }


        public double fdeck
        {
            get { return Flexure == "Positive" ? (1.25 * M4 + 1.5 * Mw + 1.8 * MLLmax) / Sdeck / nEd * 1000000 : 0; }
        }
        public double O6fcdeck
        {
            get { return 0.6 * Deck.fc; }
        }

        public double fbot
        {

            get { return Flexure == "Positive" || Hc == 0 ? 0 : (-1.25 * M3 / Sbot1 - (1.25 * M4 + 1.5 * Mw + 1.8 * MLLmin) / Sbot2) / nEb * 1000000; }
        }

        public double O6fcbot
        {
            get { return 0.6 * Botcon.fc; }
        }

        public string Checkfdeck
        {
            get { return Flexure == "Positive" ? (fdeck <= 0.6 * Deck.fc ? "OK" : "NG") : "-"; }
        }

        public double Checkfdeck_ratio
        {
            get { return (Flexure == "Positive" && fdeck != 0) ? 0.6 * Deck.fc / fdeck  : 1000000; }
        }

        public string Checkfbot
        {

            get { return Flexure == "Positive" || Hc == 0 ? "-" : (fbot <= 0.6 * Botcon.fc ? "OK" : "NG"); }
        }

        public double Checkfbot_ratio
        {

            get { return (Flexure == "Positive" || Hc == 0 || fbot == 0) ? 1000000 : 0.6 * Botcon.fc / fbot; }
        }

        //Table 5



        //5. Flexure Checking
        //Calculating Delta
        public double ntop
        {
            get; set;
        }
        public double btop
        {
            get; set;
        }
        public double bbot
        {
            get; set;
        }
        public double cbot
        {
            get; set;
        }
        public double w
        {
            get; set;
        }

        public double A0_NC
        {
            get { return (w + bbot - 2 * cbot) * (tbot / 2 + D + ttop / 2) / 2.0; }
        }
        public double A0_C
        {
            get { return (w + bbot - 2 * cbot) * (tbot / 2 + D + th + ts / 2) / 2.0; }
        }

        //Table 6
        public double T1
        {
            get; set;
        }
        public double T2
        {
            get; set;
        }
        public double T3
        {
            get; set;
        }
        public double T4
        {
            get; set;
        }
        public double TTw
        {
            get; set;
        }
        public double TLLmax
        {
            get; set;
        }
        public double TLLmin
        {
            get; set;
        }

        public double fv_NC
        {

            get { return 1.25 * (T1 + T2 + T3) / 2 / A0_NC / (Flexure == "Positive" ? ttop : tbot) * 1000000; }
        }
        public double fv_C
        {

            get { return (1.25 * T4 + 1.5 * TTw + 1.8 * (1.25 * T4 + 1.5 * TTw >= 0 ? TLLmax : TLLmin)) / 2 / A0_C / (Flexure == "Positive" ? ttop : tbot) * 1000000; }
        }

        public double fv
        {

            get { return fv_NC + fv_C; }
        }

        public double Delta
        {
            get
            {
                if ((1 - 3 * fv * fv / Fytop / Fytop) < 0)
                    return 0;
                else
                    return Math.Sqrt(1 - 3 * fv * fv / Fytop / Fytop);
            }
        }

        //Table 7
        //Calcualating Rh
        public double YU
        {
            get { return Flexure == "Positive" ? YU3s : YU4s; }
        }

        public double YL
        {
            get { return Flexure == "Positive" ? YL3s : YL4s; }
        }

        public double Dn
        {
            get { return Math.Max(YU - ttop, YL - tbot); }
        }

        public double Afn
        {
            get { return YU - ttop >= YL - tbot ? ntop * btop * ttop : bbot * tbot; }
        }

        public double fn
        {
            get { return Math.Max(Fytop, Math.Max(Math.Abs(Su_top), Math.Abs(Su_bot))); }
        }
        public double rho
        {
            get { return Math.Min(Fyweb / fn, 1.0); }
        }
        public double beta
        {
            get { return 2 * Dn * tw / Afn; }
        }

        public double Rh
        {
            get { return Fyweb >= Fytop ? 1.0 : (12 + beta * (3 * rho - rho * rho * rho)) / (12 + 2 * beta); }
        }

        //Table 8
        //Calcualation Fcb, Fcv
        public double wrib
        {
            get
            {
                double wrib;
                if (Flexure == "Positive")
                    wrib = nst == 0 ? 0 : ((w - nst * tst) / (nst + 1));
                else
                    wrib = nsb == 0 ? 0 : ((bbot - 2 * cbot - nsb * tsb) / (nsb + 1));
                return wrib;
            }
        }
        public double tfc
        {
            get { return Flexure == "Positive" ? ttop : tbot; }
        }

        public double bfc
        {
            get { return Flexure == "Positive" ? btop : bbot; }
        }

        public double xf2
        {
            get { return wrib == 0 ? (bfc / tfc) : (wrib / tfc); }
        }
        public double Irib
        {
            get { return Flexure == "Positive" ? (tst * Math.Pow(Hst, 3) / 3.0) : (tsb * Math.Pow(Hsb, 3) / 3.0); }
        }
        public double k_plate
        {
            get
            {
                double nrib = Flexure == "Positive" ? nst : nsb;
                double k;
                if (nrib == 0)
                    k = 4.0;
                else if (nrib == 1)
                    k = Math.Min(Math.Max(Math.Pow(8 * Irib / (wrib * tfc * tfc * tfc), 1 / 3.0), 1.0), 4.0);
                else
                    k = Math.Min(Math.Max(Math.Pow(0.894 * Irib / (wrib * tfc * tfc * tfc), 1 / 3.0), 1.0), 4.0);

                return k;
            }
        }

        public double ks
        {
            get
            {
                double nrib = Flexure == "Positive" ? nst : nsb;
                double ks;
                if (nrib == 0)
                    ks = 5.34;
                else
                    ks = Math.Min((5.34 + 2.84 * Math.Pow(Irib / wrib / tfc / tfc / tfc, 1.0 / 3.0)) / Math.Pow((nrib + 1), 2), 5.34);
                return ks;
            }
        }



        public double xp
        {
            get
            {
                if (Delta == 0)
                    return 10 ^ 6;
                else
                    return 0.57 * Math.Sqrt(Flange.Es * k_plate / Fybot / Delta);
            }
        }

        public double xr
        {
            get
            {
                if (Delta <= 0.3)
                    return 10 ^ 6;
                else
                    return 0.95 * Math.Sqrt(Flange.Es * k_plate / (Delta - 0.3) / Fybot);
            }
        }

        public double Fcb
        {
            get
            {
                if (xf2 <= xp)
                    return Rh * Fybot * Delta;
                else if (xf2 <= xr)
                    return Rh * Fybot * (Delta - (Delta - (Delta - 0.3) / Rh) * ((xf2 - xp) / (xr - xp)));
                else
                    return 0.9 * Flange.Es * k_plate / xf2 / xf2;
            }
        }

        public double Fcv
        {
            get
            {
                if (xf2 <= 1.12 * Math.Sqrt(Flange.Es * ks / Fytop))
                    return 0.58 * Fytop;
                else if (xf2 <= 1.40 * Math.Sqrt(Flange.Es * ks / Fytop))
                    return 0.65 * Math.Sqrt(Flange.Es * Fytop * ks) / xf2;
                else
                    return 0.9 * Flange.Es * ks / xf2 / xf2;
            }
        }

        //Table 9
        //Calcualtion Fnc, Fnt
        public string OFBFtop
        { get { return ntop == 2 ? "OF" : "BF"; } }

        public double Fnc_Pos
        {
            get
            {
                if (Flexure == "Positive")
                    return ntop == 2 ? Rh * Fytop : Rh * Fytop * Delta;
                else
                    return 0;
            }
        }

        public double Fnc_Neg
        {
            get
            {
                if (Flexure == "Positive")
                    return 0;
                else
                {
                    if (fv > Fcv)
                        return 0;
                    else
                        return Fcb * Math.Sqrt(1 - (fv / Fcv) * (fv / Fcv));
                }

            }
        }

        public double Fnc
        {
            get { return Flexure == "Positive" ? Fnc_Pos : Fnc_Neg; }
        }

        public double Fnt
        {
            get
            {
                if (Flexure == "Positive")
                    return Rh * Fybot * Delta;
                else
                {
                    if (ntop == 2)
                        return Rh * Fytop;
                    else
                        return Rh * Fytop * Delta;
                }
            }
        }

        // Calculation Mn

        public double Dt
        {
            get { return Flexure == "Positive" ? (tbot + D + th + ts) : (ttop + D + tbot); }
        }
        public double RhMy13
        {
            get { return 1.3 * Rh * My; }
        }

        public double Mn
        {
            get { return Math.Min((Dp <= 0.1 * Dt ? Mp : Mp * (1.07 - 0.7 * Dp / Dt)), 1.3 * Rh * My); }
        }

        //Table 10
        //Clasification compact or concompact section
        public double R
        {
            get; set;
        }

        public double ds
        {
            get
            {
                double ds = 0;
                switch (ns)
                {
                    case 0:
                        ds = 0;
                        break;
                    case 1:
                        ds = 0.2 * D;
                        break;
                    case 2:
                        ds = (0.14 + 0.36) / 2 * D;
                        break;
                }
                return ds;
            }
        }

        public double Dtw
        {
            get; set;
        }
        public double a
        {
            get; set;
        }

        public double b
        {
            get; set;
        }
        public double S
        {
            get; set;
        }

        public double Dcp
        {
            get
            {
                double Dcp = 0;
                switch (PNA)
                {
                    case "1":
                    case "a":
                        Dcp = Ypna;
                        break;
                    case "8":
                    case "b":
                    case "c":
                    case "d":
                        Dcp = D - Ypna;
                        break;
                    case "2":
                    case "9":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                        Dcp = 0;
                        break;
                }
                return Dcp / Math.Cos(S);
            }
        }

        public double tw
        {
            get; set;
        }

        public string Compact
        {
            get
            {
                string Compact;
                if (Flexure == "Positive")
                {
                    if (R == 0)
                    {
                        if (Fytop <= 455 && (Hw / tw <= (ns == 0 ? 1000 : 150)) && Fyweb <= 455 && 0.8 * w <= a && 1.2 * w >= a && S <= 0.25 && b <= Math.Min(a, 1800) && 2 * Dcp / tw <= 3.76 * Math.Sqrt(Flange.Es / Fytop))
                            Compact = "Compact";
                        else
                            Compact = "Noncompact";
                    }
                    else
                        Compact = "Noncompact";
                }
                else
                    Compact = "-";

                return Compact;
            }
        }


        //Checking flexure
        //Table 11
        public double Su_top
        {
            get; set;

        }
        public double Su_bot
        {
            get; set;

        }

        

        public string Check_com_top
        {
            get
            {
                if (Compact == "Compact" || Su_top >=0 )
                    return "-";
                else
                    return Math.Abs(Su_top) <= Fnc ? "OK" : "NG";

            }
        }
        public double Check_com_top_ratio
        {
            get
            {
                if (Compact == "Compact" || Su_top >= 0)
                    return 1000000;
                else
                    return Fnc / Math.Abs(Su_top);

            }
        }

        public string Check_ten_top
        {
            get
            {
                if (Compact == "Compact" || Su_top <= 0)
                    return "-";
                else
                    return Math.Abs(Su_top) <= Fnt ? "OK" : "NG";
            }
        }

        public double Check_ten_top_ratio
        {
            get
            {
                if (Compact == "Compact" || Su_top <= 0)
                    return 1000000;
                else
                    return Fnt / Math.Abs(Su_top);
            }
        }


        public string Check_com_bot
        {
            get
            {
                if (Compact == "Compact" || Su_bot >= 0)
                    return "-";
                else
                    return Math.Abs(Su_bot) <= Fnc ? "OK" : "NG";

            }
        }
        public double Check_com_bot_ratio
        {
            get
            {
                if (Compact == "Compact" || Su_bot >= 0)
                    return 1000000;
                else
                    return Fnc / Math.Abs(Su_bot);

            }
        }

        public string Check_ten_bot
        {
            get
            {
                if (Compact == "Compact" || Su_bot <= 0)
                    return "-";
                else
                    return Math.Abs(Su_bot) <= Fnt ? "OK" : "NG";
            }
        }

        public double Check_ten_bot_ratio
        {
            get
            {
                if (Compact == "Compact" || Su_bot <= 0)
                    return 1000000;
                else
                    return Fnt / Math.Abs(Su_bot);
            }
        }



        public double Mu
        {
            get { return 1.25 * (M1 + M2 + M3 + M4) + 1.5 * Mw + 1.8 * (Flexure == "Positive" ? MLLmax : MLLmin); }
        }

        public string Check_moment
        {
            get
            {
                if (Compact == "Compact")
                    return Math.Abs(Mu) <= Mn ? "OK" : "NG";
                else
                    return "-";
            }
        }

        public double Check_moment_ratio
        {
            get
            {
                if (Compact == "Compact" && Math.Abs(Mu) !=0)
                    return Mn / Math.Abs(Mu);
                else
                    return 1000000;
            }
        }

        //6. Flexure Shear

        //Table 12
        public double d0
        {
            get; set;
        }


        public string Stiffened
        {
            get
            {
                if ((d0 <= 3 * D && ns == 0) || (d0 <= 1.5 * D && ns != 0))
                    return "Stiffened";
                else
                    return "Unstiffened";
            }
        }

        public double Hw
        {
            get; set;
        }

        public double Vp
        {
            get { return 0.58 * Fyweb * Hw * tw / 1000; }
        }

        public double k_shear
        {
            get { return Stiffened == "Stiffened" ? (5 + 5 / (d0 / D) / (d0 / D)) : 5.0; }
        }

        public double EKFyw
        {
            get { return Math.Sqrt(Flange.Es * k_shear / Fyweb); }
        }

        public double C
        {
            get
            {
                double C1_ULS;
                C1_ULS = Math.Sqrt(Flange.Es * k_shear / Fyweb);
                if (Hw / tw <= 1.12 * C1_ULS)
                    return 1.0;
                else if (Hw / tw <= 1.40 * C1_ULS)
                    return 1.12 / (Hw / tw) * C1_ULS;
                else
                    return 1.57 * C1_ULS * C1_ULS / (Hw / tw) / (Hw / tw);
            }
        }

        public double Vn
        {
            get
            {
                if (Stiffened == "Stiffened")
                {
                    if (2 * Hw * tw / (btop * ttop * ntop + tbot * bbot) <= 2.5)
                        return Vp * (C + 0.87 * (1 - C) / Math.Sqrt(1 + d0 * d0 / D / D));
                    else
                        return Vp * (C + 0.87 * (1 - C) / Math.Sqrt(1 + d0 * d0 / D / D + d0 / D));
                }
                else
                    return C * Vp;
            }

        }

        //Table 13
        public double S1
        {
            get; set;
        }

        public double S2
        {
            get; set;
        }

        public double S3
        {
            get; set;
        }

        public double S4
        {
            get; set;
        }

        public double Sw
        {
            get; set;
        }

        public double SLLmax
        {
            get; set;
        }

        public double SLLmin
        {
            get; set;
        }


        public double Vu
        {
            get { return (1.25 * (S1 + S2 + S3 + S4) + 1.5 * Sw + 1.8 * ((1.25 * (S1 + S2 + S3 + S4) + 1.5 * Sw) >= 0 ? SLLmax : SLLmin)) / 2.0; }

        }

        public double angle
        {
            get { return S * 180 / Math.PI; }

        }

        public double Vui
        {
            get { return Vu / Math.Cos(S); }
        }

        public double Vr
        {
            get { return Vn; }
        }

        public string Check_shear
        {
            get { return Math.Abs(Vui) <= Vn ? "OK" : "NG"; }

        }

        public double Check_shear_ratio
        {
            get { return Vui == 0 ? 1000000 : Vn / Math.Abs(Vui); }
        }
    }
}
