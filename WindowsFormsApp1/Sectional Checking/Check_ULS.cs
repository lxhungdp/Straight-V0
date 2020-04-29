using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checking
{
    public class Check_ULS
    {
        private string _Label, _Flexure, Flange, Web;
        private double R, ntop, btop, ttop, bbot, tbot, cbot, w,a,b, D, tw, nsb, tsb, Hsb, nst, tst, Hst, d0, ns, Arb, Art, srb, Ac, th, ts, crt, crb, Hc,S, Hw, As,
            M1, M2, M3, M4, Mw, MLLmax, MLLmin, T4, Tw, TLLmax, TLLmin,S1,S2,S3,S4,Sw,SLLmax,SLLmin,
            SU1, SL1, I2s, YL2s, SL2s, SU2l, SL2l, I3s, YU3s, YL3s, SU3s, SL3s, SU3l, SL3l, I4s, YU4s, YL4s, SU4s, SL4s, SU4l, SL4l, _fv_NC,
            _Su_bot, _Su_top, _xf2,_k_plate, _Fcv, _Vp, _Ypna, _Mp;         
            
        public Check_ULS(string Label, string Flexure, double R, double ntop, double btop, double ttop, double bbot, double tbot, double cbot, double w,double a,double b, double D, double tw, double nsb, double tsb,
            double Hsb, double nst, double tst, double Hst, double d0, double ns, double Arb, double Art, double srb, double Ac, double th, double ts, double crt, double crb, double Hc,double S, double Hw, double As,
            double M1, double M2, double M3, double M4, double Mw, double MLLmax, double MLLmin, double T4, double Tw, double TLLmax, double TLLmin, double S1, double S2, double S3, double S4, double Sw, double SLLmax, double SLLmin,
            double SU1, double SL1, double I2s, double YL2s, double SL2s, double SU2l, double SL2l, double I3s, double YU3s, double YL3s, double SU3s, double SL3s, double SU3l, double SL3l, double I4s, double YU4s, double YL4s,
            double SU4s, double SL4s, double SU4l, double SL4l, double fv_NC,
            double Su_bot, double Su_top,  double xf2, double k_plate, double Fcv, double Vp, string Flange, string Web)
        {
            this._Label = Label;
            this._Flexure = Flexure;
            this.R = R;
            this.ntop = ntop;
            this.btop = btop;
            this.ttop = ttop;
            this.bbot = bbot;
            this.tbot = tbot;
            this.cbot = cbot;
            this.w = w;
            this.a = a;
            this.b = b;
            this.Hw = Hw;
            this.tw = tw;
            this.nsb = nsb;
            this.tsb = tsb;
            this.Hsb = Hsb;
            this.nst = nst;
            this.tst = tst;
            this.Hst = Hst;
            this.d0 = d0;
            this.ns = ns;
            this.Arb = Arb;
            this.Art = Art;
            this.srb = srb;
            this.Ac = Ac;
            this.th = th;
            this.ts = ts;
            this.crt = crt;
            this.crb = crb;
            this.Hc = Hc;
            this.S = S;
            this.D = D;
            this.M1 = M1;
            this.M2 = M2;
            this.M3 = M3;
            this.M4 = M4;
            this.Mw = Mw;
            this.MLLmax = MLLmax;
            this.MLLmin = MLLmin;
            this.T4 = T4;
            this.Tw = Tw;
            this.TLLmax = TLLmax;
            this.TLLmin = TLLmin;
            this.S1 = S1;
            this.S2 = S2;
            this.S3 = S3;
            this.S4 = S4;
            this.Sw = Sw;
            this.SLLmax = SLLmax;
            this.SLLmin = SLLmin;
            this.SU1 = SU1;
            this.SL1 = SL1;
            this.SL2s = SL2s;
            this.SU2l = SU2l;
            this.SL2l = SL2l;
            this.SU3l = SU3l;
            this.SL3l = SL3l;
            this.SU4l = SU4l;
            this.SL4l = SL4l;
            this.SU3s = SU3s;
            this.SL3s = SL3s;
            this.SU4s = SU4s;
            this.SL4s = SL4s;
            this.I3s = I3s;
            this.I2s = I2s;
            this.YL2s = YL2s;
            this.YL3s = YL3s;
            this.YL4s = YL4s;
            this.I4s = I4s;
            this.YU3s = YU3s;
            this.YU4s = YU4s;
            this._fv_NC = fv_NC;
            this._Su_bot = Su_bot;
            this._Su_top = Su_top;
            this.As = As;
            this._xf2 = xf2;
            this._k_plate = k_plate;
            this._Fcv = Fcv;
            this._Vp = Vp;
            this.Flange = Flange;
            this.Web = Web;
        }

        
        public string Label
        {
            get { return _Label; }

        }

        public string Flexure
        {
            get { return _Flexure; }

        }
        public double Su_top
        {
            get { return _Su_top; }

        }
        public double Su_bot
        {
            get { return _Su_bot; }

        }

        //1. Determine the location of PNA
        public double Ptop
        {
            get { return ntop * btop * ttop * Material.Fy(Flange,ttop) / 1000; }
        }

        public double Pbot
        {
            get { return bbot * tbot * Material.Fy(Flange, tbot) / 1000; }
        }

        public double Pw
        {
            get { return Hw * tw * 2 * Material.Fy(Web, tw) / 1000; }
        }

        public double Ps
        {
            get { return 0.85 * Material.fckd * As / 1000; }
        }

        public double Psb
        {
            get { return nsb * tsb * Hsb * Material.Fy(Flange, tsb) / 1000; }
        }

        public double Pst
        {
            get { return nst * tst * Hst * Material.Fy(Flange, tst) / 1000; }
        }

        public double Prb
        {
            get { return Arb * Material.Fyb / 1000; }
        }

        public double Prt
        {
            get { return Art * Material.Fyb / 1000; }
        }

        public double Prbot
        {
            get { return srb * Material.Fyb / 1000; }
        }

        public double Pcom
        {
            get { return Ac * 0.85 * Material.fckb / 1000; }
        }

        public string PNA
        {
            get
            {
                string Location;
                double dtop, dbot, dsb, dst, ds, drb, drt, dr, dw, dcom, Y, Mp;

                if (Flexure == "Positive")
                {
                    if (Pbot + Psb + (D - 2 * Hst) / D * Pw + Prbot >= Pst + Ptop + Ps + Prb + Prt)
                    {
                        Location = "1";
                        Y = D / 2 * ((Pbot + Psb + Prbot - Pst - Ptop - Ps - Prb - Prt) / Pw + 1);
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
                    else if (Pbot + Psb + Pw + Prbot + Pst >= Ptop + Ps + Prb + Prt)
                    {
                        Location = "a";
                        Y = (Pbot + Psb + Prbot + Pw + Pst - Ptop - Ps - Prb - Prt) / (2 / D * Pw + 2 / Hst * Pst);
                        dtop = Y + ttop / 2;
                        dbot = D - Y + tbot / 2;
                        dsb = D - Y - Hsb / 2;
                        ds = Y + th + ts / 2;
                        drb = Y + th + crb;
                        drt = Y + th + ts - crt;
                        dr = D - Y - Hc / 2;
                        Mp = (Pst / 2 / Hst * (Y * Y + (Hst - Y) * (Hst - Y)) + Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + Ptop * dtop + Psb * dsb + Pbot * dbot + Ps * ds + Prb * drb + Prt * drt + Prbot * dr) / 1000;
                    }
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop >= Ps + Prb + Prt)
                    {
                        Location = "2";
                        Y = ttop / 2 * ((Pbot + Psb + Prbot + Pw + Pst - Ps - Prb - Prt) / Ptop + 1);
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
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop >= ((ts - crb) / ts) * Ps + Prb + Prt)
                    {
                        Location = "3";
                        Y = ts * (Pbot + Psb + Prbot + Pw + Pst + Ptop - Prb - Prt) / Ps;
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
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop + Prb >= ((ts - crb) / ts) * Ps + Prt)
                    {
                        Location = "4";
                        Y = ts - crb;
                        dtop = ts + th - Y - ttop / 2;
                        dbot = ts + th - Y + D + tbot / 2;
                        dw = ts + th - Y + D / 2;
                        dsb = ts + th - Y + D - Hsb / 2;
                        dst = ts + th - Y + Hst / 2;
                        drt = Y - crt;
                        dr = ts + th - Y + D - Hc / 2;
                        Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prt * drt + Prbot * dr) / 1000;
                    }
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop + Prb >= (crt / ts) * Ps + Prt)
                    {
                        Location = "5";
                        Y = ts * (Pbot + Psb + Prbot + Pw + Pst + Ptop + Prb - Prt) / Ps;
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
                    else if (Pbot + Psb + Pw + Prbot + Pst + Ptop + Prb + Prt >= (crt / ts) * Ps)
                    {
                        Location = "6";
                        Y = crt;
                        dtop = ts + th - Y - ttop / 2;
                        dbot = ts + th - Y + D + tbot / 2;
                        dw = ts + th - Y + D / 2;
                        dsb = ts + th - Y + D - Hsb / 2;
                        dst = ts + th - Y + Hst / 2;
                        drb = ts - Y - crb;
                        dr = ts + th - Y + D - Hc / 2;
                        Mp = (Y * Y / 2 / ts * Ps + Ptop * dtop + Psb * dsb + Pst * dst + Pbot * dbot + Pw * dw + Prb * drb + Prbot * dr) / 1000;
                    }
                    else
                    {
                        Location = "7";
                        Y = ts * (Pbot + Psb + Prbot + Pw + Pst + Ptop + Prb + Prt) / Ps;
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

                }
                else
                {
                    if (Pbot + Psb + Hsb / Hc * Pcom + Hsb / D * Pw >= (D - Hsb) * Pw / D + Pst + Ptop + Prb + Prt)
                    {
                        Location = "b";
                        Y = (Pbot + Pw + D / Hc * Pcom + (2 * D / Hsb - 1) * Psb - Ptop - Pst - Prb - Prt) / (2 / Hsb * Psb + Pcom / Hc + 2 / D * Pw);
                        dbot = D - Y + tbot / 2;
                        dtop = Y + ttop / 2;
                        dst = Y - Hst / 2;
                        drb = Y + th + crb;
                        drt = Y + th + ts - crt;
                        dr = D - Y - Hc / 2;
                        Mp = (Psb / 2 / Hsb * ((D - Y) * (D - Y) + (Hsb - D + Y) * (Hsb - D + Y)) + Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + (D - Y) * (D - Y) / 2 / Hc * Pcom + Pbot * dbot + Pst * dst + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;
                    }
                    else if (Pbot + Psb + Pcom + Hc / D * Pw >= (D - Hc) * Pw / D + Pst + Ptop + Prb + Prt)
                    {
                        Location = "c";
                        Y = (Pbot + Pw + Psb + D / Hc * Pcom - Ptop - Pst - Prb - Prt) / (Pcom / Hc + 2 / D * Pw);
                        dbot = D - Y + tbot / 2;
                        dtop = Y + ttop / 2;
                        dst = Y - Hst / 2;
                        dsb = D - Y - Hsb / 2;
                        drb = Y + th + crb;
                        drt = Y + th + ts - crt;
                        dr = D - Y - Hc / 2;
                        Mp = (Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + (D - Y) * (D - Y) / 2 / Hc * Pcom + Pbot * dbot + Psb * dsb + Pst * dst + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;
                    }
                    else if (Pbot + Psb + Prbot + Pcom + (D - Hst) / D * Pw >= Pst + Ptop + Prb + Prt)
                    {
                        Location = "8";
                        Y = D / 2 * ((Pbot + Psb + Prbot + Pcom - Pst - Ptop - Prb - Prt) / Pw + 1);
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
                    else if (Pbot + Psb + Prbot + Pcom + Pw + Pst >= Ptop + Prb + Prt)
                    {
                        Location = "d";
                        Y = (Pbot + Psb + Prbot + Pcom + Pst + Pw - Ptop - Prb - Prt) / (2 / Hst * Pst + 2 / D * Pw);
                        dbot = D - Y + tbot / 2;
                        dtop = Y + ttop / 2;
                        dsb = D - Y - Hsb / 2;
                        drb = Y + th + crb;
                        drt = Y + th + ts - crt;
                        dr = D - Y - Hc / 2;
                        dcom = D - Y - Hc / 2;
                        Mp = (Pst / 2 / Hst * (Y * Y + (Hst - Y) * (Hst - Y)) + Pw / 2 / D * (Y * Y + (D - Y) * (D - Y)) + Pcom * dcom + Pbot * dbot + Psb * dsb + Ptop * dtop + Prb * drb + Prt * drt + Prbot * dr) / 1000;
                    }
                    else
                    {
                        Location = "9";
                        Y = ttop / 2 * ((Pbot + Psb + Prbot + Pcom + Pst + Pw - Prb - Prt) / Ptop + 1);
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
                }

                this._Ypna = Y;
                this._Mp = Mp;
                return Location;
            }
        }

        public double Ypna
        {
            get { return _Ypna; }
        }

        public double Mp
        {
            get { return _Mp; }
        }

        //2. Determine My

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

        public double Mad_t
        {
            get
            {
                if (Flexure == "Positive")
                    return (Material.Fy(Flange,tbot) / 1000000 - MD1 / STsteel - MD2 / STbot - MD3 / STlongtime) * STshorttime;
                else
                    return -(Material.Fy(Flange, ttop) / 1000000 + MD1 / STsteel + MD2 / STbot + MD3 / STlongtime) * STshorttime;
            }
        }

        public double Mad_c
        {
            get
            {
                if (Flexure == "Positive")
                    return (Material.Fy(Flange, ttop) / 1000000 - MD1 / SCsteel - MD2 / SCbot - MD3 / SClongtime) * SCshorttime;
                else
                    return -(Material.Fy(Flange, tbot) / 1000000 + MD1 / SCsteel + MD2 / SCbot + MD3 / SClongtime) * SCshorttime;
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

        public double Dt_duc
        {
            get { return Flexure == "Positive" ? (tbot + D + th + ts) : (ttop + D); }
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

        //4. Checking stress of concrete
        public double Sdeck
        {
            get { return I3s / (tbot + D + th + ts - YL3s); }
        }

        public double Sbot1
        {
            get { return I2s / (YL2s - tbot); }
        }

        public double Sbot2
        {
            get { return I4s / (YL4s - tbot); }
        }

        public double fdeck
        {
            get { return Flexure == "Positive" ? (1.25 * M4 + 1.5 * Mw + 1.8 * MLLmax) / Sdeck / Material.nEd * 1000000 : 0; }
        }

        public double fbot
        {

            get { return Flexure == "Positive" || Hc == 0 ? 0 : (-1.25 * M3 / Sbot1 - (1.25 * M4 + 1.5 * Mw + 1.8 * MLLmin) / Sbot2) / Material.nEb * 1000000; }
        }

        public string Checkfdeck
        {
            get { return Flexure == "Positive" ? (fdeck <= 0.6 * Material.fckd ? "OK" : "NG") : "-"; }
        }

        public string Checkfbot
        {

            get { return Flexure == "Positive" || Hc == 0 ? "-" : (fbot <= 0.6 * Material.fckb ? "OK" : "NG"); }
        }

        //5. Flexure Checking
        //Calculating Delta
        public double A0_C
        {
            get { return (w + bbot - 2 * cbot) * (tbot / 2 + D + th + ts / 2) / 2.0; }
        }
        public double fv_NC
        {

            get { return _fv_NC; }
        }
        public double fv_C
        {

            get { return (1.25 * T4 + 1.5 * Tw + 1.8 * (1.25 * T4 + 1.5 * Tw >= 0 ? TLLmax : TLLmin)) / 2 / A0_C / (Flexure == "Positive" ? ttop : tbot) * 1000000; }
        }

        public double fv
        {

            get { return fv_NC + fv_C; }
        }

        public double Delta
        {
            get { return Math.Sqrt(1 - fv * fv / Material.Fy(Flange,ttop) / Material.Fy(Flange, ttop)); }
        }

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
            get { return Math.Max(Material.Fy(Flange, ttop), Math.Max(Math.Abs(Su_top), Math.Abs(Su_bot))); }            
        }
        public double rho
        {
            get { return Math.Min(Material.Fy(Web, tw) / fn, 1.0); }            
        }
        public double beta
        {
            get { return 2 * Dn * tw / Afn; }            
        }

        public double Rh
        {
            get { return Material.Fy(Web, tw) >= Material.Fy(Flange, ttop) ? 1.0 : (12 + beta * (3 * rho - rho * rho * rho)) / (12 + 2 * beta); }            
        }

        //Calcualation Fcb, Fcv
        public double k_plate
        {
            get { return _k_plate; }
        }
        public double xf2
        {
            get { return _xf2; }
        }
        public double xr
        {
            get { return 0.95 * Math.Sqrt(Material.Es * k_plate / (Delta - 0.3) / Material.Fy(Flange, tbot)); }            
        }

        public double xp
        {
            get { return 0.57 * Math.Sqrt(Material.Es * k_plate / Material.Fy(Flange, tbot) / Delta); }            
        }
        public double Fcb
        {
            get
            {
                if (xf2 <= xp)
                    return Rh * Material.Fy(Flange, tbot) * Delta;
                else if (xf2 <= xr)
                    return Rh * Material.Fy(Flange, tbot) * (Delta - (Delta - (Delta - 0.3) / Rh) * ((xf2 - xp) / (xr - xp)));
                else
                    return 0.9 * Material.Es * k_plate / xf2 / xf2;
            }
        }

        public double Fcv
        {
            get { return _Fcv; }          
        }

        //Calcualtion Fnc, Fnt
        public double Fnc_Pos
        {
            get
            {
                if (Flexure == "Positive")
                    return ntop == 2 ? Rh * Material.Fy(Flange, ttop) : Rh * Material.Fy(Flange, ttop) * Delta;
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
                    return Fcb * Math.Sqrt(1 - (fv / Fcv) * (fv / Fcv));
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
                if (Flexure != "Positive" && ntop == 2)
                    return Rh * Material.Fy(Flange, tbot);
                else
                    return Rh * Material.Fy(Flange, ttop) * Delta;
            }

        }

        // Calculation Mn

        public double Dt
        {
            get { return Flexure == "Positive" ? (tbot + D + th + ts) : (ttop + D + tbot); }
        }

        public double Mn
        {
            get { return Math.Min((Dp <= 0.1 * Dt ? Mp : Mp * (1.07 - 0.7 * Dp / Dt)), 1.3 * Rh * My); }
        }

        //Clasification compact or concompact section
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
                return Dcp / Math.Cos(Math.Atan(S));
            }
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
                        if (Material.Fy(Flange, ttop) <= 455 && (Hw / tw <= (ns == 0 ? 1000 : 150)) && Material.Fy(Web, tw) <= 455 && 0.8 * w <= a && 1.2 * w >= a && S <= 0.25 && b <= Math.Min(a, 1800) && 2 * Dcp / tw <= 3.76 * Math.Sqrt(Material.Es / Material.Fy(Flange, ttop)))
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


        public string Check_com
        {
            get
            {
                if (Compact == "Noncompact")
                    return Math.Abs(Math.Min(Su_bot, Su_top)) <= Fnc ? "OK" : "NG";
                else
                    return "-";
            }
        }
        public string Check_com_ratio
        {
            get
            {
                if (Compact == "Noncompact")
                    return Math.Abs(Math.Min(Su_bot, Su_top)) == 0 ? "inf" : (Fnc / Math.Abs(Math.Min(Su_bot, Su_top))).ToString();
                else
                    return "-";
            }
        }

        public string Check_ten
        {
            get
            {
                if (Compact == "Noncompact")
                    return Math.Abs(Math.Max(Su_bot, Su_top)) <= Fnt ? "OK" : "NG";
                else
                    return "-";
            }
        }

        public string Check_ten_ratio
        {
            get
            {
                if (Compact == "Noncompact")
                    return Math.Abs(Math.Max(Su_bot, Su_top)) == 0 ? "Inf" : (Fnt / Math.Abs(Math.Max(Su_bot, Su_top))).ToString();
                else
                    return "-";
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

        public string Check_moment_ratio
        {
            get
            {
                if (Compact == "Compact")
                    return Math.Abs(Mu) == 0 ? "Inf" : (Mn / Math.Abs(Mu)).ToString();
                else
                    return "-";
            }
        }

        //6. Flexure Shear

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


        public double Vu
        {
            get { return (1.25 * (S1 + S2 + S3 + S4) + 1.5 * Sw + 1.8 * ((1.25 * (S1 + S2 + S3 + S4) + 1.5 * Sw) >= 0 ? SLLmax : SLLmin)) / 2.0; }
            
        }

        public double Vui
        {
            get { return Vu / Math.Cos(Math.Atan(S)); }
        }
        // k = shear - buckling coefficient

        public double k_shear
        {
            get { return Stiffened == "Stiffened" ? (5 + 5 / (d0 / D) / (d0 / D)) : 5.0; }            
        }

        public double C
        {
            get
            {
                double C1_ULS;
                C1_ULS = Math.Sqrt(Material.Es * k_shear / Material.Fy(Web, tw));
                if (Hw / tw <= 1.12 * C1_ULS)
                    return 1.0;
                else if (Hw / tw <= 1.40 * C1_ULS)
                    return 1.12 / (Hw / tw) * C1_ULS;
                else
                    return 1.57 * C1_ULS * C1_ULS / (Hw / tw) / (Hw / tw);
            }
        }

        public double Vp
        {
            get { return _Vp; }

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

        public string Check_shear
        {
            get { return Math.Abs(Vui) <= Vn ? "OK" : "NG"; }
           
        }

        public string Check_shear_ratio
        {
            get { return Vui == 0 ? "Inf" : (Vn / Math.Abs(Vui)).ToString(); }            
        }

        // End of checking ULS

    }
}
