using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public class Table_ULS
    {
        public string Label { get; set; }
        
        public double Ptop { get; set; }
        public double Pw { get; set; }
        public double Pbot { get; set; }       
        public double Ps { get; set; }
        public double Psb { get; set; }
        public double Pst { get; set; }
        public double Prb { get; set; }
        public double Prt { get; set; }
        public double Prbot { get; set; }
        public double Pcom { get; set; }
        public string PNA { get; set; }
        public double Ypna { get; set; }
        public double Mp { get; set; }

        public double My { get; set; }
        public double Compare_Mp { get; set; }
        public double Dp { get; set; }
        public double Dt { get; set; }

        public string CheckDuctility { get; set; }
        public double fdeck { get; set; }
        public double fbot { get; set; }
        public string Checkfdeck { get; set; }

        public string Checkfbot { get; set; }
        public double Fcb { get; set; }

        public double Fcv { get; set; }
        public double Fnc { get; set; }
        public double Fnt { get; set; }
        public double Mn { get; set; }
        public double Dcp { get; set; }
        public string Compact { get; set; }
        public string CheckU_com { get; set; }
        public string CheckU_ten { get; set; }
        public string CheckU_moment { get; set; }
        public double C { get; set; }
        public double Vn { get; set; }
        public double Vu { get; set; }
        public string Check_shear { get; set; }
    }
}
