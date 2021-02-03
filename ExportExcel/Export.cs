using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provider;

namespace ExportExcel
{
    public class Export
    {
        
        public static int Node(int girder)
        {
            string Dimfield = "Joint";
            List<Dim> Dim = SQL.getListdata<Dim>(girder, Dimfield, "CNode2");
            return Dim.Count();
        }

        public static List<InputExcel> SectionalData(int girder)
        {
            string field = "Joint, Label, X, ntop, btop, ttop, ctop, D, tw, S, bbot, tbot, cbot, Hc, ts, th, bh, w, bleft, bright, aleft, aright, Leff, bs, ns, d0, nsb, tsb, Hsb, nst, tst, Hst, Srb, Srt, Srbot";
            List<Dim> Dim = SQL.getListdata<Dim>(girder, field, "CNode2");

            field = "Lb, ds, Fytop, Fybot, Fyweb, Sc_top, Sc_bot, YU, YL, DC1, T1, T2, T3, Slender, Dc, S1, S2, S3, M1, M2, M3";
            List<Cons> Cons = SQL.getListdata<Cons>(girder, field, "CCheckCons");

            field = "Fyrtop, Fyrbot, PNA, Ypna, Mp, M4, Mw, MLLmax, MLLmin, STsteel, STbot, STlongtime, STshorttime, SCsteel, SCbot, SClongtime, SCshorttime, Sbot1, Sbot2, Sdeck, T4, TTw, TLLmax, TLLmin, YU, YL, Su_top, Su_bot, S4, Sw, SLLmax, SLLmin";
            List<ULS> ULS = SQL.getListdata<ULS>(girder, field, "CCheckULS");

            field = "Ss2_top, Ss2_bot, Srebar";
            List<SLS> SLS = SQL.getListdata<SLS>(girder, field, "CCheckSLS");

            field = "fDC_top, fDC_bot, Deltaf_top, Deltaf_bot, Vn, SLLfmax, SLLfmin";
            List<FLS> FLS = SQL.getListdata<FLS>(girder, field, "CCheckFLS");

            int n = Dim.Count;
            List<InputExcel> InputExcel = new List<InputExcel>();
            for (int i = 0; i < n; i++)
                InputExcel.Add(new InputExcel(Dim[i], Cons[i], ULS[i], SLS[i], FLS[i]));

            
            return InputExcel;
        }

        public static DataTable SectionalDT()
        {
            DataTable dt = new DataTable();
            DataTable dt1 = SQL.getDataTable("select Es, Fy, fc, Ec from A03Matuse");
            dt.Columns.Add();

            dt.Rows.Add(Convert.ToDouble(dt1.Rows[0]["Es"].ToString())); //Es
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[6]["fc"].ToString())); //fck deck
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[7]["fc"].ToString())); //fck bot
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[6]["Ec"].ToString())); //Ec deck
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[7]["Ec"].ToString())); //Ec bot
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[8]["Fy"].ToString())); //Fy rebar

            dt1 = SQL.getDataTable("select Pforms, ADTT from A04Loading");
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[0]["ADTT"].ToString())); //ADTT
            dt.Rows.Add(Convert.ToDouble(dt1.Rows[0]["Pforms"].ToString())); //Pforms

            return dt;
        }
        

        public static void SectionalFill(int girder, string filename, int col)
        {
            
            Excel.CreateFile(Const.Folderstring + @"\Excel\Sectional Checking.xlsx",  filename);

            //Add row to Cons
            List<int> table = new List<int> { 94, 99, 187, 192, 197, 202, 207, 212, 226, 231, 285, 311 };
            Excel.Addrow(filename, 1, table, Node(girder), col);

            table = new List<int> { 199, 218, 223, 249, 271, 442, 447, 452, 457, 462, 467, 480, 485, 527, 532 };
            Excel.Addrow(filename, 2, table, Node(girder), col);

            //Add to SLS sheet
            table = new List<int> { 42, 103, 118 };
            Excel.Addrow(filename, 3, table, Node(girder), col);

            //Add to FLS sheet
            table = new List<int> { 54, 83 };
            Excel.Addrow(filename, 4, table, Node(girder), col);

            Excel.Fillby1list(filename, 0, SectionalData(girder), 3, 4);
            Excel.Fillbydatatable(filename, 0, SectionalDT(), 2, 2);

            
        }

        public static void SectionalFillAll(int girder, string filename, int col)
        {

            Excel.CreateFile(Const.Folderstring + @"\Excel\Sectional Checking.xlsx", filename);

            List<List<int>> table = new List<List<int>>();
            //Add row to Cons
            List<int> table1 = new List<int> { 94, 99, 187, 192, 197, 202, 207, 212, 226, 231, 285, 311 };
            table.Add(table1);

            table1 = new List<int> { 199, 218, 223, 249, 271, 442, 447, 452, 457, 462, 467, 480, 485, 527, 532 };
            table.Add(table1);

            //Add to SLS sheet
            table1 = new List<int> { 42, 103, 118 };
            table.Add(table1);

            //Add to FLS sheet
            table1 = new List<int> { 54, 83 };
            table.Add(table1);

            List<int> sheet = new List<int>() { 1, 2, 3, 4 };
            Excel.Addmultirow(filename, sheet, table, Node(girder), col);

            //Excel.Fillby1list(filename, 0, SectionalData(girder), 3, 4);
            //Excel.Fillbydatatable(filename, 0, SectionalDT(), 2, 2);
            Excel.FillChecking(filename, 0, SectionalData(girder), 3, 4, SectionalDT(), 2, 2);

        }
    }
}
