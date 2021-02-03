using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathWorks.MATLAB.NET.Arrays;
using MathNet.Numerics.Data.Matlab;
using MathNet.Numerics.LinearAlgebra;
using System.Data;
using System.Windows.Forms;

namespace Provider
{
    public class Const
    {
        public static string Folderstring
        {
             get { return Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName; }            
          
        }

        public static string Sap2000
        {
            get { return Folderstring + @"\Sap2000\"; }
        }


        //Write mat to mat file to check
        public static void WriteMat2(MWNumericArray a, string path, string name)
        {            
            double[,] arr = (double[,])((MWNumericArray)a).ToArray(MWArrayComponent.Real);
            var d1 = CreateMatrix.DenseOfArray(arr);
            string pathname = path + name + ".mat";
            MatlabWriter.Write(pathname, d1, name);
        }

        public static void WriteMat1(double[,] arr, string path, string name)
        {            
            var d1 = CreateMatrix.DenseOfArray(arr);
            string pathname = path + name + ".mat";
            MatlabWriter.Write(pathname, d1, name);
        }

        public static void Fillcheckfl(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {
            Charttop.Visible = true;
            Chartbot.Visible = false;
            DataTable dt = SQL.getresultdata("Station,fl,O6Fy,Check_fl_ratio", girder, "CCheckCons");
            DataRow[] dr = dt.Select("[Check_fl_ratio] = MIN([Check_fl_ratio])");

            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][3].ToString()), 2);

            tb.Text = "Checking Eq: fl ≤  0.6Fy \r\n";
            tb.Text += "Smallest ratio = " + ratio.ToString() + "\r\n";
            tb.Text += "At Station = " + sta.ToString() +"mm";
                        
            Chart.Checkfl(dt, Charttop);
        }

        public static void FillcheckCstress(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = true;
            DataTable dt = SQL.getresultdata("Station,Check_Fnc_top_ratio,Check_Fnt_top_ratio,Check_Fnc_bot_ratio,Check_Fnt_bot_ratio", girder, "CCheckCons");
            
            DataRow[] dr = dt.Select("[Check_Fnc_top_ratio] = MIN([Check_Fnc_top_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);

            tb.Text = "Compression stress : fbu + fl/3 ≤ Φf*Fnc \r\n";
            tb.Text += "Top flange: smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() +  "mm \r\n";

            dr = dt.Select("[Check_Fnc_bot_ratio] = MIN([Check_Fnc_bot_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][3].ToString()), 2);
            
            tb.Text += "Bottom flange: smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_Fnt_top_ratio] = MIN([Check_Fnt_top_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][2].ToString()), 2);

            tb.Text += "Tension stress : fbu + fl ≤ Φf*Fnt \r\n";
            tb.Text += "Top flange: smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_Fnt_bot_ratio] = MIN([Check_Fnt_bot_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][4].ToString()), 2);
            
            tb.Text += "Bottom flange: smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm";

            dt = SQL.getresultdata("Station,Sc_top,Sc_bot,fl,Fnc,Fnt,fbufl3_com,fbufl_ten", girder, "CCheckCons");
            Chart.CheckCons(dt, Charttop, Chartbot);
            

        }

        public static void FillcheckFcrw(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = true;

            DataTable dt = SQL.getresultdata("Station,Check_buckling_top_ratio,Check_buckling_bot_ratio", girder, "CCheckCons");
            DataRow[] dr = dt.Select("[Check_buckling_top_ratio] = MIN([Check_buckling_top_ratio])");

            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);
            
            tb.Text = "Checking Eq : fbu ≤ Φf* Fcrw \r\n";
            tb.Text += "Top flange: smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_buckling_bot_ratio] = MIN([Check_buckling_bot_ratio])");

            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][2].ToString()), 2);
            
            tb.Text += "Bottom flange: smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dt = SQL.getresultdata("Station,Sc_top,Sc_bot,Fcrw", girder, "CCheckCons");
            Chart.CheckFcrw(dt, Charttop, Chartbot);            

        }

        public static void FillcheckCVui(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = false;
            
            DataTable dt = SQL.getresultdata("Station,Vui,Vn,Check_shear_ratio", girder, "CCheckCons");

            DataRow[] dr = dt.Select("[Check_shear_ratio] = MIN([Check_shear_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][3].ToString()), 2);

            tb.Text = "Checking Eq : Vui ≤ Φv*Vcr \r\n";
            tb.Text += "Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            Chart.CheckCShear(dt, Charttop);

        }

        public static void FillcheckDuc(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = false;

            DataTable dt = SQL.getresultdata("Station,Dp,O42Dt,CheckDuctility,CheckDuc_ratio", girder, "CCheckULS");

            DataRow[] dr = dt.Select("[CheckDuc_ratio] = MIN([CheckDuc_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][4].ToString()), 2);

            tb.Text = "Checking Eq : Dp ≤ 0.42*Dt \r\n";
            tb.Text += "Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            Chart.CheckDuc(dt, Charttop);

        }

        public static void FillcheckConcrete(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = true;

            DataTable dt = SQL.getresultdata("Station,Checkfdeck_ratio,Checkfbot_ratio", girder, "CCheckULS");

            DataRow[] dr = dt.Select("[Checkfdeck_ratio] = MIN([Checkfdeck_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);

            tb.Text = "Checking Eq : Concrete compression stress ≤ 0.6*f'c \r\n";
            tb.Text += "Deck : Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Checkfbot_ratio] = MIN([Checkfbot_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][2].ToString()), 2);

            tb.Text += "Bottom concrete : Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dt = SQL.getresultdata("Station,fdeck,O6fcdeck,fbot,O6fcbot,Checkfdeck,Checkfbot", girder, "CCheckULS");
            Chart.CheckConcrete(dt, Charttop, Chartbot);

        }

        public static void FillcheckUstress(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = true;

            DataTable dt = SQL.getresultdata("Station,Check_com_top_ratio,Check_ten_top_ratio,Check_com_bot_ratio,Check_ten_bot_ratio", girder, "CCheckULS");

            DataRow[] dr = dt.Select("[Check_com_top_ratio] = MIN([Check_com_top_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);

            tb.Text = "Compression stress : fbu ≤ Φf*Fnc \r\n";
            tb.Text += "Top flange : Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_com_bot_ratio] = MIN([Check_com_bot_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][3].ToString()), 2);
            tb.Text += "Bottom flange : Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_ten_top_ratio] = MIN([Check_ten_top_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][2].ToString()), 2);
            tb.Text += "Tension stress : fbu ≤ Φf*Fnt \r\n";
            tb.Text += "Top flange : Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_ten_bot_ratio] = MIN([Check_ten_bot_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][4].ToString()), 2);            
            tb.Text += "Bottom flange : Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dt = SQL.getresultdata("Station,Compact,Su_top,Su_bot,Fnc,Fnt", girder, "CCheckULS");
            Chart.CheckUstress(dt, Charttop, Chartbot);

        }

        public static void FillcheckUVui(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = false;

            DataTable dt = SQL.getresultdata("Station,Vui,Vr,Check_shear_ratio", girder, "CCheckULS");

            DataRow[] dr = dt.Select("[Check_shear_ratio] = MIN([Check_shear_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][3].ToString()), 2);

            tb.Text = "Checking Eq : Vui ≤ Φv*Vn \r\n";
            tb.Text += "Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            Chart.CheckCShear(dt, Charttop);

        }

        public static void FillcheckSstress(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = true;

            DataTable dt = SQL.getresultdata("Station,Check_topflange_ratio,Check_botflange_ratio", girder, "CCheckSLS");

            DataRow[] dr = dt.Select("[Check_topflange_ratio] = MIN([Check_topflange_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);

            tb.Text = "Checking Eq : ff ≤ 0.95*Rh*Fyf \r\n";
            tb.Text += "Top flange: Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dr = dt.Select("[Check_botflange_ratio] = MIN([Check_botflange_ratio])");
            sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            ratio = Math.Round(Convert.ToDouble(dr[0][2].ToString()), 2);

            tb.Text += "Bottom flange: Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dt = SQL.getresultdata("Station,Ss2_top,Ss2_bot,RhFytop,RhFybot", girder, "CCheckSLS");
            Chart.CheckSstress(dt, Charttop, Chartbot);

        }

        public static void FillcheckSFcrw(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = false;

            DataTable dt = SQL.getresultdata("Station,Check_buckling_ratio", girder, "CCheckSLS");

            DataRow[] dr = dt.Select("[Check_buckling_ratio] = MIN([Check_buckling_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);

            tb.Text = "Checking Eq : fc ≤ Fcrw \r\n";
            tb.Text += "Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";           

            dt = SQL.getresultdata("Station,fc,Fcrw", girder, "CCheckSLS");
            Chart.CheckSFcrw(dt, Charttop, Chartbot);

        }

        public static void FillcheckSFs(int girder, TextBox tb, LiveCharts.WinForms.CartesianChart Charttop, LiveCharts.WinForms.CartesianChart Chartbot)
        {

            Charttop.Visible = true;
            Chartbot.Visible = false;

            DataTable dt = SQL.getresultdata("Station,Check_fs_ratio", girder, "CCheckSLS");

            DataRow[] dr = dt.Select("[Check_fs_ratio] = MIN([Check_fs_ratio])");
            double sta = Math.Round(Convert.ToDouble(dr[0][0].ToString()), 2);
            double ratio = Math.Round(Convert.ToDouble(dr[0][1].ToString()), 2);

            tb.Text = "Checking Eq : fs ≤ 0.8Fy \r\n";
            tb.Text += "Smallest ratio = " + ratio.ToString() + ", at Station = " + sta.ToString() + "mm \r\n";

            dt = SQL.getresultdata("Station,fs,O8Fy,Check_fs", girder, "CCheckSLS");
            Chart.CheckSFy(dt, Charttop, Chartbot);

        }
    }
}      
