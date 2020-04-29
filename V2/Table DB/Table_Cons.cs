using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public class Table_Cons
    {
        
        public string Label { get; set; }

        public string Flexure { get; set; }
        public double Sc_com { get; set; }
        public double Sc_ten { get; set; }
        public double rt { get; set; }
        public double fl1 { get; set; }
        public double Fcr { get; set; }
        public double Lp { get; set; }
        public double fl { get;  set; }
        public string CheckC_fl { get;  set; }
        public double Rh { get;  set; }
        public double A0_NC { get;  set; }
        public double fv_NC { get;  set; }
        public double Delta { get;  set; }
        public double Fnc_LB { get;  set; }
        public double k_plate { get;  set; }
        public double ks { get; internal set; }
        public object fbufl_com { get;  set; }
        public double fbufl3_com { get;  set; }
        public double fbufl_ten { get;  set; }
        public double Fnc_LTB { get;  set; }
        public double Fnc_OF { get;  set; }
        public double Fcv { get; internal set; }
        public double Fnc_BF { get;  set; }
        public double Fcb { get;  set; }
        public double Dc { get;  set; }
        public string Slender { get;  set; }
        public string CheckC_comOF { get;  set; }
        public string CheckC_com { get;  set; }
        public double Fnt { get;  set; }
        public string CheckC_ten { get;  set; }
        public string CheckC_buckling { get;  set; }
        public string Check_shear { get;  set; }
        public double Vui { get;  set; }
        public double Fcrw { get;  set; }
        public double Vn { get;  set; }
    }
}
