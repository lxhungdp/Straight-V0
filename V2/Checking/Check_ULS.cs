using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public static class Check_ULS
    {
        //1. Determine the location of PNA
        public static double Ptop(this Node n)
        {
            return n.ntop * n.btop * n.ttop * Material.Fyf / 1000;
        }

        public static double Pbot(this Node n)
        {
            return n.bbot * n.tbot * Material.Fyf / 1000;
        }

        public static double Pw(this Node n)
        {
            return n.Hw() * n.tw * 2 * Material.Fyw / 1000;
        }

        public static double Ps(this Node n)
        {
            return 0.85 * Material.fckd * n.As() / 1000 ;
        }

        public static double Psb(this Node n)
        {
            return n.nsb * n.tsb * n.Hsb * Material.Fyf / 1000;
        }

        public static double Pst(this Node n)
        {
            return n.nst * n.tst * n.Hst * Material.Fyf / 1000;
        }

        public static double Prb(this Node n)
        {
            return n.Arb() * Material.Fyb / 1000;
        }

        public static double Prt(this Node n)
        {
            return n.Art() * Material.Fyb / 1000;
        }

        public static double Prbot(this Node n)
        {
            return n.srb * Material.Fyb / 1000;
        }

        public static double Pcom(this Node n)
        {
            return n.Ac() * 0.85 * Material.fckb / 1000;
        }

        
        public static Tuple<string, double, double> PNA(this Node n)
        {
            
            string Location ;           
            double dtop, dbot, dsb, dst, ds, drb, drt, dr, dw, dcom, Y, Mp;

            if (n.Flexure() == "Positive")
            {
                if (n.Pbot() + n.Psb() + (n.D - 2 * n.Hst) / n.D * n.Pw() + n.Prbot() >= n.Pst() + n.Ptop() + n.Ps() + n.Prb() + n.Prt())
                {
                    Location = "1";
                    Y = n.D / 2 * ((n.Pbot() + n.Psb() + n.Prbot() - n.Pst() - n.Ptop() - n.Ps() - n.Prb() - n.Prt()) / n.Pw() + 1);
                    dtop = Y + n.ttop / 2;
                    dbot = n.D - Y + n.tbot / 2;
                    dsb = n.D - Y - n.Hsb / 2;
                    dst = Y - n.Hst / 2;
                    ds = Y + n.th + n.ts / 2;
                    drb = Y + n.th + n.crb;
                    drt = Y + n.th + n.ts - n.crt;
                    dr = n.D - Y - n.Hc / 2;
                    Mp = (n.Pw() / 2 / n.D * (Y * Y + (n.D - Y) * (n.D - Y)) + n.Ptop() * dtop + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Ps() * ds + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr)/1000;

                }
                else if (n.Pbot() + n.Psb() + n.Pw() + n.Prbot() + n.Pst()  >= n.Ptop() + n.Ps() + n.Prb() + n.Prt())
                {
                    Location = "a";
                    Y = (n.Pbot() + n.Psb() + n.Prbot() + n.Pw() + n.Pst() - n.Ptop() - n.Ps() - n.Prb() - n.Prt()) / (2/n.D*n.Pw() + 2/n.Hst*n.Pst());
                    dtop = Y + n.ttop / 2;
                    dbot = n.D - Y + n.tbot / 2;
                    dsb = n.D - Y - n.Hsb / 2;                    
                    ds = Y + n.th + n.ts / 2;
                    drb = Y + n.th + n.crb;
                    drt = Y + n.th + n.ts - n.crt;
                    dr = n.D - Y - n.Hc / 2;
                    Mp = (n.Pst() / 2 / n.Hst * (Y * Y + (n.Hst - Y) * (n.Hst - Y)) + n.Pw() / 2 / n.D * (Y * Y + (n.D - Y) * (n.D - Y)) + n.Ptop()*dtop + n.Psb() * dsb + n.Pbot() * dbot + n.Ps() * ds + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Pw() + n.Prbot() + n.Pst() + n.Ptop() >= n.Ps() + n.Prb() + n.Prt())
                {
                    Location = "2";
                    Y = n.ttop / 2 * ((n.Pbot() + n.Psb() + n.Prbot() + n.Pw() + n.Pst() - n.Ps() - n.Prb() - n.Prt()) / n.Ptop() + 1);
                    dbot = n.ttop - Y + n.D + n.tbot / 2;
                    dw = n.ttop - Y + n.D / 2;
                    dsb = n.ttop - Y + n.D - n.Hsb / 2;
                    dst = n.ttop - Y + n.Hst / 2;
                    ds = n.ts / 2 + n.th - n.ttop + Y;
                    drb = n.crb + n.th - n.ttop + Y;
                    drt = n.ts - n.crt + n.th - n.ttop + Y;
                    dr = n.ttop - Y + n.D - n.Hc / 2;
                    Mp = (n.Ptop() / 2 / n.ttop * (Y * Y + (n.ttop - Y) * (n.ttop - Y)) + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Pw() * dw + n.Ps() * ds + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Pw() + n.Prbot() + n.Pst() + n.Ptop() >= ((n.ts - n.crb)/n.ts)*n.Ps() + n.Prb() + n.Prt())
                {
                    Location = "3";
                    Y = n.ts * (n.Pbot() + n.Psb() + n.Prbot() + n.Pw() + n.Pst() + n.Ptop() - n.Prb() - n.Prt()) / n.Ps();
                    dtop = n.ts + n.th - Y - n.ttop / 2;
                    dbot = n.ts + n.th - Y + n.D + n.tbot / 2;
                    dw = n.ts + n.th - Y + n.D / 2;
                    dsb = n.ts + n.th - Y + n.D - n.Hsb / 2;
                    dst = n.ts + n.th - Y + n.Hst / 2;                    
                    drb = n.crb - (n.ts - Y);
                    drt = Y - n.crt;
                    dr = n.ts + n.th - Y + n.D - n.Hc / 2;
                    Mp = (Y*Y/2/n.ts*n.Ps() + n.Ptop() * dtop + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Pw() * dw + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Pw() + n.Prbot() + n.Pst() + n.Ptop() + n.Prb() >= ((n.ts - n.crb) / n.ts) * n.Ps() + n.Prt())
                {
                    Location = "4";
                    Y = n.ts - n.crb;
                    dtop = n.ts + n.th - Y - n.ttop / 2;
                    dbot = n.ts + n.th - Y + n.D + n.tbot / 2;
                    dw = n.ts + n.th - Y + n.D / 2;
                    dsb = n.ts + n.th - Y + n.D - n.Hsb / 2;
                    dst = n.ts + n.th - Y + n.Hst / 2;                    
                    drt = Y - n.crt;
                    dr = n.ts + n.th - Y + n.D - n.Hc / 2;
                    Mp = (Y * Y / 2 / n.ts * n.Ps() + n.Ptop() * dtop + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Pw() * dw +  n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Pw() + n.Prbot() + n.Pst() + n.Ptop() + n.Prb() >= (n.crt / n.ts) * n.Ps() + n.Prt())
                {
                    Location = "5";
                    Y = n.ts * (n.Pbot() + n.Psb() + n.Prbot() + n.Pw() + n.Pst() + n.Ptop() + n.Prb() - n.Prt()) / n.Ps();
                    dtop = n.ts + n.th - Y - n.ttop / 2;
                    dbot = n.ts + n.th - Y + n.D + n.tbot / 2;
                    dw = n.ts + n.th - Y + n.D / 2;
                    dsb = n.ts + n.th - Y + n.D - n.Hsb / 2;
                    dst = n.ts + n.th - Y + n.Hst / 2;
                    drb = n.ts - Y - n.crb;
                    drt = Y - n.crt;
                    dr = n.ts + n.th - Y + n.D - n.Hc / 2;
                    Mp = (Y * Y / 2 / n.ts * n.Ps() + n.Ptop() * dtop + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Pw() * dw + n.Prt() * drt + n.Prb()*drb + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Pw() + n.Prbot() + n.Pst() + n.Ptop() + n.Prb() + n.Prt() >= (n.crt / n.ts) * n.Ps() )
                {
                    Location = "6";
                    Y = n.crt;
                    dtop = n.ts + n.th - Y - n.ttop / 2;
                    dbot = n.ts + n.th - Y + n.D + n.tbot / 2;
                    dw = n.ts + n.th - Y + n.D / 2;
                    dsb = n.ts + n.th - Y + n.D - n.Hsb / 2;
                    dst = n.ts + n.th - Y + n.Hst / 2;
                    drb = n.ts - Y - n.crb;                    
                    dr = n.ts + n.th - Y + n.D - n.Hc / 2;
                    Mp = (Y * Y / 2 / n.ts * n.Ps() + n.Ptop() * dtop + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Pw() * dw + n.Prb() * drb + n.Prbot() * dr) / 1000;
                }
                else
                {
                    Location = "7";
                    Y = n.ts * (n.Pbot() + n.Psb() + n.Prbot() + n.Pw() + n.Pst() + n.Ptop() + n.Prb() + n.Prt()) / n.Ps();
                    dtop = n.ts + n.th - Y - n.ttop / 2;
                    dbot = n.ts + n.th - Y + n.D + n.tbot / 2;
                    dw = n.ts + n.th - Y + n.D / 2;
                    dsb = n.ts + n.th - Y + n.D - n.Hsb / 2;
                    dst = n.ts + n.th - Y + n.Hst / 2;
                    drb = n.ts - Y - n.crb;
                    drt = Y - n.crt;
                    dr = n.ts + n.th - Y + n.D - n.Hc / 2;
                    Mp = (Y * Y / 2 / n.ts * n.Ps() + n.Ptop() * dtop + n.Psb() * dsb + n.Pst() * dst + n.Pbot() * dbot + n.Pw() * dw + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }

            }
            else
            {
                if (n.Pbot() + n.Psb() + n.Hsb/n.Hc * n.Pcom() + n.Hsb / n.D * n.Pw() >= (n.D - n.Hsb)*n.Pw()/n.D + n.Pst() + n.Ptop() + n.Prb() + n.Prt() )
                {
                    Location = "b";
                    Y = (n.Pbot() + n.Pw() + n.D / n.Hc * n.Pcom() + (2 * n.D / n.Hsb - 1) * n.Psb() - n.Ptop() - n.Pst() - n.Prb() - n.Prt()) / (2/n.Hsb*n.Psb() + n.Pcom()/n.Hc + 2/n.D * n.Pw());
                    dbot = n.D - Y + n.tbot/2;
                    dtop = Y + n.ttop / 2;
                    dst = Y - n.Hst / 2;
                    drb = Y + n.th + n.crb;
                    drt = Y + n.th + n.ts - n.crt;
                    dr = n.D - Y - n.Hc / 2;                    
                    Mp = (n.Psb() / 2 / n.Hsb *((n.D - Y)* (n.D - Y) + (n.Hsb - n.D + Y)* (n.Hsb - n.D + Y)) + n.Pw()/2/n.D*(Y*Y + (n.D - Y)* (n.D - Y)) + (n.D - Y)* (n.D - Y) /2 / n.Hc * n.Pcom() + n.Pbot()*dbot + n.Pst()*dst + n.Ptop()*dtop + n.Prb()*drb + n.Prt()*drt + n.Prbot()*dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Pcom() + n.Hc / n.D * n.Pw() >= (n.D - n.Hc) * n.Pw() / n.D + n.Pst() + n.Ptop() + n.Prb() + n.Prt())
                {
                    Location = "c";
                    Y = (n.Pbot() + n.Pw() + n.Psb() + n.D / n.Hc * n.Pcom()  - n.Ptop() - n.Pst() - n.Prb() - n.Prt()) / ( n.Pcom() / n.Hc + 2 / n.D * n.Pw());
                    dbot = n.D - Y + n.tbot / 2;
                    dtop = Y + n.ttop / 2;
                    dst = Y - n.Hst / 2;
                    dsb = n.D - Y - n.Hsb / 2;
                    drb = Y + n.th + n.crb;
                    drt = Y + n.th + n.ts - n.crt;
                    dr = n.D - Y - n.Hc / 2;
                    Mp = (n.Pw() / 2 / n.D * (Y * Y + (n.D - Y) * (n.D - Y)) + (n.D - Y) * (n.D - Y) / 2 / n.Hc * n.Pcom() + n.Pbot() * dbot + n.Psb()*dsb + n.Pst() * dst + n.Ptop() * dtop + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Prbot() + n.Pcom() + (n.D - n.Hst) / n.D * n.Pw() >= n.Pst() + n.Ptop() + n.Prb() + n.Prt())
                {
                    Location = "8";
                    Y = n.D / 2 * ( (n.Pbot() + n.Psb() + n.Prbot() + n.Pcom() - n.Pst() - n.Ptop() - n.Prb() - n.Prt()) / n.Pw()+ 1);
                    dbot = n.D - Y + n.tbot / 2;
                    dtop = Y + n.ttop / 2;
                    dst = Y - n.Hst / 2;
                    dsb = n.D - Y - n.Hsb / 2;
                    drb = Y + n.th + n.crb;
                    drt = Y + n.th + n.ts - n.crt;
                    dr = n.D - Y - n.Hc / 2;
                    dcom = n.D - Y - n.Hc / 2;
                    Mp = (n.Pw() / 2 / n.D * (Y * Y + (n.D - Y) * (n.D - Y)) + n.Pcom()*dcom + n.Pbot() * dbot + n.Psb() * dsb + n.Pst() * dst + n.Ptop() * dtop + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else if (n.Pbot() + n.Psb() + n.Prbot() + n.Pcom() +n.Pw() + n.Pst() >= n.Ptop() + n.Prb() + n.Prt())
                {
                    Location = "d";
                    Y = (n.Pbot() + n.Psb() + n.Prbot() + n.Pcom() + n.Pst() + n.Pw() - n.Ptop() - n.Prb() - n.Prt())/(2/n.Hst*n.Pst() + 2/n.D*n.Pw()) ;
                    dbot = n.D - Y + n.tbot / 2;
                    dtop = Y + n.ttop / 2;                    
                    dsb = n.D - Y - n.Hsb / 2;
                    drb = Y + n.th + n.crb;
                    drt = Y + n.th + n.ts - n.crt;
                    dr = n.D - Y - n.Hc / 2;
                    dcom = n.D - Y - n.Hc / 2;
                    Mp =(n.Pst()/2/n.Hst*(Y*Y + (n.Hst - Y)* (n.Hst - Y)) + n.Pw() / 2 / n.D * (Y * Y + (n.D - Y) * (n.D - Y)) + n.Pcom() * dcom + n.Pbot() * dbot + n.Psb() * dsb + n.Ptop() * dtop + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
                else
                {
                    Location = "9";
                    Y = n.ttop / 2 * ( (n.Pbot() + n.Psb() + n.Prbot() + n.Pcom() + n.Pst() + n.Pw() - n.Prb() - n.Prt() ) / n.Ptop()+ 1);
                    dbot = n.ttop - Y + n.D + n.tbot / 2;
                    dw = n.ttop - Y + n.D / 2;
                    dsb = n.ttop - Y + n.D - n.Hsb / 2;
                    dst = n.ttop - Y + n.Hst / 2;
                    ds = n.ts / 2 + n.th - n.ttop + Y;
                    drb = n.crb + n.th - n.ttop + Y;
                    drt = n.ts - n.crt + n.th - n.ttop + Y;
                    dr = n.ttop - Y + n.D - n.Hc / 2;
                    dcom = n.ttop - Y + n.D - n.Hc / 2;
                    Mp = (n.Ptop() / 2 / n.ttop * (Y * Y + (n.ttop - Y)* (n.ttop - Y)) + n.Pbot()*dbot + n.Pcom()*dcom + n.Psb() * dsb + n.Pw()*dw + n.Pst()*dst + n.Prb() * drb + n.Prt() * drt + n.Prbot() * dr) / 1000;
                }
            }

            //var tuple = new Tuple<string, double, double>(Location, Y, Mp);
            //return tuple;
            return Tuple.Create(Location, Y, Mp);
        }

        //2. Determine My

        public static double MD1(this Node n)
        {
            return 1.25*(n.M1 + n.M2);
        }
        public static double MD2(this Node n)
        {
            return 1.25 * n.M3;
        }
        public static double MD3(this Node n)
        {
            return 1.25 * n.M4 + 1.5*n.Mw;
        }

        public static double STsteel(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SL1() : n.SU1();
        }

        public static double STbot(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SL1() : n.SU2l();
        }

        public static double STlongtime(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SL3l() : n.SU4l();
        }

        public static double STshorttime(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SL3s() : n.SU4s();
        }

        public static double SCsteel(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SU1() : n.SL1();
        }

        public static double SCbot(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SU1() : n.SL2l();
        }

        public static double SClongtime(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SU3l() : n.SL4l();
        }

        public static double SCshorttime(this Node n)
        {
            return n.Flexure() == "Positive" ? n.SU3s() : n.SL4s();
        }

        public static double Mad_t(this Node n)
        {
            if (n.Flexure() == "Positive")
                return (Material.Fyf / 1000000 - n.MD1() / n.STsteel() - n.MD2() / n.STbot() - n.MD3() / n.STlongtime() )*n.STshorttime();
            else
                return -(Material.Fyf / 1000000 + n.MD1() / n.STsteel() + n.MD2() / n.STbot() + n.MD3() / n.STlongtime()) * n.STshorttime();
        }

        public static double Mad_c(this Node n)
        {
            if (n.Flexure() == "Positive")
                return (Material.Fyf / 1000000 - n.MD1() / n.SCsteel() - n.MD2() / n.SCbot() - n.MD3() / n.SClongtime()) * n.SCshorttime();
            else
                return -(Material.Fyf / 1000000 + n.MD1() / n.SCsteel() + n.MD2() / n.SCbot() + n.MD3() / n.SClongtime()) * n.SCshorttime();
        }

        public static double Mad(this Node n)
        {
            return Math.Min(Math.Abs(n.Mad_c()), Math.Abs(n.Mad_t()));
        }

        public static double My(this Node n)
        {
            return Math.Abs(n.MD1() + n.MD2() + n.MD3()) + n.Mad();
        }

        public static double Compare_Mp(this Node n)
        {
            return (n.My() - n.PNA().Item3) / n.PNA().Item3;
        }

        //3. Checking Ductility Requirement

        public static double Dp(this Node n)
        {
            double Dp = 0;
            switch (n.PNA().Item1)
            {
                case "1":
                case "a":
                    Dp = n.ts + n.th + n.PNA().Item2;
                    break;
                case "8":
                case "b":
                case "c":
                case "d":
                    Dp = n.D - n.PNA().Item2;
                    break;
                case "2":
                    Dp = n.ts + n.th - n.ttop + n.PNA().Item2;
                    break;
                case "9":
                    Dp = n.ttop - n.PNA().Item2 + n.D;
                    break;
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                    Dp = n.PNA().Item2;
                    break;
            }
            return Dp;
        }

        public static double Dt_duc(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.tbot + n.D + n.th + n.ts) : (n.ttop + n.D);
        }

        public static string CheckDuctility(this Node n)
        {
            string CheckDuctility;
            if (n.Flexure() != "Positive" && n.Hc == 0)
                CheckDuctility = "-";
            else
                CheckDuctility = n.Dp() <= 0.42 * n.Dt_duc() ? "OK" : "NG";

            return CheckDuctility;
        }


        //4. Checking stress of concrete
        public static double Sdeck(this Node n)
        {
            return n.I3s() / (n.tbot + n.D + n.th + n.ts - n.YL3s());
        }

        public static double Sbot1(this Node n)
        {
            return n.I2s() / (n.YL2s() - n.tbot);
        }

        public static double Sbot2(this Node n)
        {
            return n.I4s() / (n.YL4s() - n.tbot);
        }

        public static double fdeck(this Node n)
        {
            return n.Flexure() == "Positive" ? (1.25*n.M4 + 1.5*n.Mw + 1.8*n.MLLmax()) / n.Sdeck() / Material.nEd() * 1000000 : 0;
        }

        public static double fbot(this Node n)
        {

            return n.Flexure() == "Positive" || n.Hc == 0 ? 0 : (-1.25 * n.M3 / n.Sbot1()  - (1.25 * n.M4 + 1.5 * n.Mw + 1.8 * n.MLLmin()) / n.Sbot2()) / Material.nEb() * 1000000;
        }

        public static string Checkfdeck(this Node n)
        {
            return n.Flexure() == "Positive" ? (n.fdeck() <= 0.6*Material.fckd ? "OK" : "NG") : "-";
        }

        public static string Checkfbot(this Node n)
        {

            return n.Flexure() == "Positive" || n.Hc == 0 ? "-" : (n.fbot() <= 0.6 * Material.fckb ? "OK" : "NG");
        }

        //5. Flexure Checking

        public static double A0_C(this Node n)
        {

            return  (n.w + n.bbot - 2 * n.cbot) * (n.tbot/2 + n.D + n.th + n.ts/2) / 2.0;
        }

        public static double fv_C(this Node n)
        {

            return (1.25 * n.T4 + 1.5 * n.Tw + 1.8 * (1.25 * n.T4 + 1.5 * n.Tw >=0 ? n.TLLmax() : n.TLLmin())) / 2 / n.A0_C() / (n.Flexure() == "Positive" ? n.ttop : n.tbot) * 1000000;
        }

        public static double fv(this Node n)
        {

            return n.fv_NC() + n.fv_C();
        }

        public static double Fnc_Pos(this Node n)
        {
            if (n.Flexure() == "Positive")
                return n.ntop == 2 ? n.Rh().Item2 * Material.Fyf : n.Rh().Item2 * Material.Fyf * n.Delta().Item2;
            else
                return 0;
        }

        public static double Fnc_Neg(this Node n)
        {
            if (n.Flexure() == "Positive")
                return 0;
            else
                return n.Fcb().Item2 * Math.Sqrt(1 - (n.fv() / n.Fcv()) * (n.fv() / n.Fcv()));
        }

        public static double Dt(this Node n)
        {
            return  n.Flexure() == "Positive" ? (n.tbot + n.D + n.th + n.ts) : (n.ttop + n.D + n.tbot);
        }

        public static double Mn(this Node n)
        {
            return Math.Min((n.Dp() <= 0.1 * n.Dt() ? n.PNA().Item3 : n.PNA().Item3 * (1.07 - 0.7 * n.Dp() / n.Dt())),1.3 * n.Rh().Item2*n.My());
        }

        public static double Dcp(this Node n)
        {
            double Dcp = 0;
            switch (n.PNA().Item1)
            {
                case "1":
                case "a":
                    Dcp = n.PNA().Item2;
                    break;
                case "8":
                case "b":
                case "c":
                case "d":
                    Dcp = n.D - n.PNA().Item2;
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
            return Dcp / Math.Cos(Math.Atan(n.S()));
        }

        //Classification compact or noncompact section
                
        public static string Compact(this Node n)
        {
            string Compact;
            if (n.Flexure() == "Positive")
            {
                if (n.R == 0)
                {
                    if (Material.Fyf <= 455 && (n.Hw() / n.tw <= (n.ds == 0 ? 1000 : 150) ) && Material.Fyw <= 455 && 0.8*n.w <= n.a && 1.2*n.w >= n.a && n.S() <= 0.25 && n.b <= Math.Min(n.a,1800) && 2*n.Dcp() / n.tw <= 3.76 * Math.Sqrt(Material.Es / Material.Fyf))
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

        //Checking flexure

        
        public static string CheckU_com(this Node n)
        {
            if (n.Compact() == "Noncompact")
                return Math.Abs(Math.Min(n.Su_bot(), n.Su_top())) <= n.Fnc().Item2 ? "OK" : "NG";
            else
                return "-";
        }
        public static string CheckU_com_ratio(this Node n)
        {
            if (n.Compact() == "Noncompact")
                return Math.Abs(Math.Min(n.Su_bot(), n.Su_top())) == 0 ? "inf" : (n.Fnc().Item2 / Math.Abs(Math.Min(n.Su_bot(), n.Su_top()))).ToString();
            else
                return "-";
        }

        public static string CheckU_ten(this Node n)
        {
            if (n.Compact() == "Noncompact")
                return Math.Abs(Math.Max(n.Su_bot(), n.Su_top())) <= n.Fnt().Item2 ? "OK" : "NG";
            else
                return "-";
        }

        public static string CheckU_ten_ratio(this Node n)
        {
            if (n.Compact() == "Noncompact")
                return Math.Abs(Math.Max(n.Su_bot(), n.Su_top())) == 0 ? "Inf" : (n.Fnt().Item2 / Math.Abs(Math.Max(n.Su_bot(), n.Su_top()))).ToString();
            else
                return "-";
        }

        public static double Mu(this Node n)
        {
            return 1.25*(n.M1 + n.M2 + n.M3 + n.M4) + 1.5*n.Mw + 1.8*(n.Flexure() == "Positive" ? n.MLLmax() : n.MLLmin()) ;
        }

        public static string CheckU_moment(this Node n)
        {
            if (n.Compact() != "Noncompact")
                return Math.Abs(n.Mu()) <= n.Mn() ? "OK" : "NG";
            else
                return "-";
        }

        public static string CheckU_moment_ratio(this Node n)
        {
            if (n.Compact() != "Noncompact")
                return Math.Abs(n.Mu()) == 0 ? "Inf" : (n.Mn()/ Math.Abs(n.Mu())).ToString();
            else
                return "-";
        }

        //6. Flexure Shear

        public static string Stiffened(this Node n)
        {
            if ((n.d0 <= 3 * n.D && n.ds == 0) || (n.d0 <= 1.5 * n.D && n.ds != 0))
                return "Stiffened";
            else
                return "Unstiffened";
        }
                

    }
}
