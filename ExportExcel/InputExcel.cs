using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportExcel
{
    public class InputExcel
    {
        private Cons Cons;
        private ULS ULS;
        private Dim Dim;
        private SLS SLS;
        private FLS FLS;

        public InputExcel(Dim Dim, Cons Cons, ULS ULS, SLS SLS, FLS FLS)
        {
            this.Dim = Dim;
            this.Cons = Cons;
            this.ULS = ULS;
            this.SLS = SLS;
            this.FLS = FLS;
        }
        
        //Dim
        public int Joint { get { return Dim.Joint; } } //Node name
        public string Label { get { return Dim.Label; } } //Station
        public double X { get { return Dim.X; } } //Station
        public double R { get { return Dim.R; } } //Radius

        //Top flange
        public double ntop { get { return Dim.ntop; } }
        public double btop { get { return Dim.btop; } }
        public double ttop { get { return Dim.ttop; } }

        //Bottom flange
        public double bbot { get { return Dim.bbot; } }
        public double tbot { get { return Dim.tbot; } }
        public double cbot { get { return Dim.cbot; } }

        //Web
        public double D { get { return Dim.D; } }
        public double tw { get { return Dim.tw; } }

        public double S { get { return Dim.S; } }

        //Bottom concrete
        public double Hc { get { return Dim.Hc; } }

        //Deck slab
        public double ts { get { return Dim.ts; } }
        public double th { get { return Dim.th; } }
        public double bh { get { return Dim.bh; } }
        public double w { get { return Dim.w; } }
        public double bleft { get { return Dim.bleft; } }
        public double bright { get { return Dim.bright; } }
        public double aleft { get { return Dim.aleft; } }
        public double aright { get { return Dim.aright; } }
        public double Leff { get { return Dim.Leff; } }
        public double bs { get { return Dim.bs; } }

        //Stiffnener
        public double ns { get { return Dim.ns; } }
        public double d0 { get { return Dim.d0; } }

        //Bottom Rib
        public double nsb { get { return Dim.nsb; } }
        public double tsb { get { return Dim.tsb; } }
        public double Hsb { get { return Dim.Hsb; } }

        //Bottom Rib
        public double nst { get { return Dim.nst; } }
        public double tst { get { return Dim.tst; } }
        public double Hst { get { return Dim.Hst; } }

        //Rebar area
        public double Srb { get { return Dim.Srb; } }
        public double Srt { get { return Dim.Srt; } }
        public double Srbot { get { return Dim.Srbot; } }

        //Constructiblity
        public double Lb { get { return Cons.Lb; } }
        public double ds { get { return Cons.ds; } }
        public double Fytop { get { return Cons.Fytop; } }
        public double Fybot { get { return Cons.Fybot; } }
        public double Fyweb { get { return Cons.Fyweb; } }
       
        public double Sc_top { get { return Cons.Sc_top; } }
        public double Sc_bot { get { return Cons.Sc_bot; } }
        public double YU { get { return Cons.YU; } }
        public double YL { get { return Cons.YL; } }
        public double Dc1 { get { return Cons.Dc1; } }
        public double T1 { get { return Cons.T1; } }
        public double T2 { get { return Cons.T2; } }
        public double T3 { get { return Cons.T3; } }
        public string Slender { get { return Cons.Slender == "Slender Web" ? "S" : "C"; } }
        public double Dc { get { return Cons.Dc; } }
        public double S1 { get { return Cons.S1; } }
        public double S2 { get { return Cons.S2; } }
        public double S3 { get { return Cons.S3; } }
        public double M1 { get { return Cons.M1; } }
        public double M2 { get { return Cons.M2; } }
        public double M3 { get { return Cons.M3; } }

        //Ultimate limit state
        public double Fyrtop { get { return ULS.Fyrtop; } }
        public double Fyrbot { get { return ULS.Fyrbot; } }
        public string PNA { get { return ULS.PNA; } }
        public double Ypna { get { return ULS.Ypna; } }
        public double Mp { get { return ULS.Mp; } }
        public double M4 { get { return ULS.M4; } }
        public double Mw { get { return ULS.Mw; } }
        public double MLLmax { get { return ULS.MLLmax; } }
        public double MLLmin { get { return ULS.MLLmin; } }
        public double STsteel { get { return ULS.STsteel; } }
        public double STbot { get { return ULS.STbot; } }
        public double STlongtime { get { return ULS.STlongtime; } }
        public double STshorttime { get { return ULS.STshorttime; } }
        public double SCsteel { get { return ULS.SCsteel; } }
        public double SCbot { get { return ULS.SCbot; } }
        public double SClongtime { get { return ULS.SClongtime; } }
        public double SCshortime { get { return ULS.SCshorttime; } }
        public double Sbot1 { get { return ULS.Sbot1; } }
        public double Sbot2 { get { return ULS.Sbot2; } }
        public double Sdeck { get { return ULS.Sdeck; } }
        public double T4 { get { return ULS.T4; } }
        public double TTw { get { return ULS.TTw; } }
        public double TLLmax { get { return ULS.TLLmax; } }
        public double TLLmin { get { return ULS.TLLmin; } }
        public double YUu { get { return ULS.YU; } }
        public double YLu { get { return ULS.YL; } }
        public double Su_top { get { return ULS.Su_top; } }
        public double Su_bot { get { return ULS.Su_bot; } }
        public double S4 { get { return ULS.S4; } }
        public double Sw { get { return ULS.Sw; } }
        public double SLLmax { get { return ULS.SLLmax; } }
        public double SLLmin { get { return ULS.SLLmin; } }

        //SLS
        public double Ss2_top { get { return SLS.Ss2_top; } }
        public double Ss2_bot { get { return SLS.Ss2_bot; } }
        public double Ss2_Srebar { get { return SLS.Srebar; } }

        //FLS
        public double fDC_top { get { return FLS.fDC_top; } }
        public double fDC_bot { get { return FLS.fDC_bot; } }
        public double Deltaf_top { get { return FLS.Deltaf_top; } }
        public double Deltaf_bot { get { return FLS.Deltaf_bot; } }
        public double Vn { get { return FLS.Vn; } }
        public double SLLfmax { get { return FLS.SLLfmax; } }
        public double SLLfmin { get { return FLS.SLLfmin; } }

    }
}

