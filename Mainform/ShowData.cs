using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Provider;

namespace Mainform
{
    public partial class ShowData : Form
    {
        public ShowData()
        {
            InitializeComponent();
        }

        public string Node;
        public int ngirder;
        private void ShowData_Load(object sender, EventArgs e)
        {
            switch (Node)
            {                
                case "NodeC1":
                
                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Lb,A1,Ac,As1,Ah,DeltaV,S,Mlw,Mlo,Mlf,Mlc,Sc_top,Sc_bot,Dc,rt,Sl,fl1,Fcr,Lp,fl,O6Fy,Check_fl,Check_fl_ratio", ngirder, "CCheckCons");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["A1"].HeaderText = "ASteel";
                        gridShowdata.Columns["Ac"].HeaderText = "ABotConcrete";
                        gridShowdata.Columns["As1"].HeaderText = "ADeck";
                        gridShowdata.Columns["Ah"].HeaderText = "AHaunch";
                        gridShowdata.Columns["Sc_top"].HeaderText = "fbu_top";
                        gridShowdata.Columns["Sc_bot"].HeaderText = "fbu_bot";
                        gridShowdata.Columns["Check_fl"].HeaderText = "Checking";
                        gridShowdata.Columns["Check_fl_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeC2":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Rh,Fnc_LB,Fnc_OF,fv_NC,Delta,k_plate,ks,Fcb,Fcv,Fnc_BF,Fnc,Fnt,Slender," +
                    "fbufl_com,RhFyc,Check_Fyc_top,Check_Fyc_top_ratio,fbufl3_com,Check_Fnc_top,Check_Fnc_top_ratio,fbufl_ten,Check_Fnt_top,Check_Fnt_top_ratio," +
                    "fbu_com,Check_Fnc_bot,Check_Fnc_bot_ratio,fbu_ten,Check_Fnt_bot,Check_Fnt_bot_ratio,Vui,k_shear,C,Vn,Check_shear,Check_shear_ratio", ngirder, "CCheckCons");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Fnc_OF"].HeaderText = "Fnc\r\nOpen Flange";
                        gridShowdata.Columns["fv_NC"].HeaderText = "fv";
                        gridShowdata.Columns["Delta"].HeaderText = "Δ";
                        gridShowdata.Columns["k_plate"].HeaderText = "k";
                        gridShowdata.Columns["Fnc_BF"].HeaderText = "Fnc\r\nBox Flange";

                        gridShowdata.Columns["fbufl_com"].HeaderText = "fbu+fl\r\nTop Flange";
                        gridShowdata.Columns["RhFyc"].HeaderText = "ΦfRhFyc";
                        gridShowdata.Columns["Check_Fyc_top"].HeaderText = "Check Fyc\r\nTop Flange";
                        gridShowdata.Columns["Check_Fyc_top_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["fbufl3_com"].HeaderText = "fbu+fl/3\r\nTop Flange";
                        gridShowdata.Columns["Check_Fnc_top"].HeaderText = "Check Fnc\r\nTop Flange";
                        gridShowdata.Columns["Check_Fnc_top_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["fbufl_ten"].HeaderText = "fbu+fl\r\nTop Flange";
                        gridShowdata.Columns["Check_Fnt_top"].HeaderText = "Check Fnt\r\nTop Flange";
                        gridShowdata.Columns["Check_Fnt_top_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["fbu_com"].HeaderText = "fbu\r\nBot Flange";
                        gridShowdata.Columns["Check_Fnc_bot"].HeaderText = "Check Fnc\r\nBottom Flange";
                        gridShowdata.Columns["Check_Fnc_bot_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["fbu_ten"].HeaderText = "fbu\r\nBot Flange";
                        gridShowdata.Columns["Check_Fnt_bot"].HeaderText = "Check Fnt\r\nBottom Flange";
                        gridShowdata.Columns["Check_Fnt_bot_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["k_shear"].HeaderText = "k";
                        gridShowdata.Columns["Check_shear"].HeaderText = "Check Shear";
                        gridShowdata.Columns["Check_shear_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeC3":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Dc,Slender,ds,Psi,k_bend,Fcrw,Check_buckling_top,Check_buckling_top_ratio,Check_buckling_bot,Check_buckling_bot_ratio", ngirder, "CCheckCons");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Psi"].HeaderText = "Ψ";
                        gridShowdata.Columns["k_bend"].HeaderText = "k";
                        gridShowdata.Columns["Check_buckling_top"].HeaderText = "Check Buckling\r\nTop Flange";
                        gridShowdata.Columns["Check_buckling_top_ratio"].HeaderText = "Ratio";
                        gridShowdata.Columns["Check_buckling_bot"].HeaderText = "Check Buckling\r\nBottom Flange";
                        gridShowdata.Columns["Check_buckling_bot_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeC4":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Vu,Vui,d0,Stiffened,k_shear,C,Vp,Vn,Check_shear,Check_shear_ratio", ngirder, "CCheckCons");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["k_shear"].HeaderText = "k";
                        gridShowdata.Columns["Stiffened"].HeaderText = "Classification";
                        gridShowdata.Columns["Check_shear"].HeaderText = "Check Shear";
                        gridShowdata.Columns["Check_shear_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeU1":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Flexure,PNA,Ypna,Dp,Dt_duc,O42Dt,CheckDuctility,CheckDuc_ratio", ngirder, "CCheckULS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["PNA"].HeaderText = "PNA\r\nLocation";
                        gridShowdata.Columns["Dt_duc"].HeaderText = "Dt";
                        gridShowdata.Columns["O42Dt"].HeaderText = "0.42*Dt";
                        gridShowdata.Columns["CheckDuctility"].HeaderText = "Check\r\nDuctility";
                        gridShowdata.Columns["CheckDuc_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeU2":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Flexure,fdeck,fbot,Checkfdeck,Checkfdeck_ratio,Checkfbot,Checkfbot_ratio", ngirder, "CCheckULS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["fdeck"].HeaderText = "Concrete Stress\r\nDeck";
                        gridShowdata.Columns["fbot"].HeaderText = "Concrete Stress\r\nBottom Concrete";
                        gridShowdata.Columns["Checkfdeck"].HeaderText = "Checking\r\nDeck";
                        gridShowdata.Columns["Checkfdeck_ratio"].HeaderText = "Ratio";
                        gridShowdata.Columns["Checkfbot"].HeaderText = "Checking\r\nBottom Concrete";
                        gridShowdata.Columns["Checkfbot_ratio"].HeaderText = "Ratio";

                    }
                    break;

                case "NodeU3":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Flexure,fv,Delta,Rh,Fcb,Fcv,Fnc_Pos,Fnc_Neg,Fnc,Fnt,Mn,Compact," +
                            "Su_top,Check_com_top,Check_com_top_ratio,Check_ten_top,Check_ten_top_ratio,Su_bot,Check_com_bot,Check_com_bot_ratio,Check_ten_bot,Check_ten_bot_ratio," +
                            "Mu,Check_moment,Check_moment_ratio", ngirder, "CCheckULS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Delta"].HeaderText = "Δ";
                        gridShowdata.Columns["Fnc_Pos"].HeaderText = "Fnc\r\nPositive Moment";
                        gridShowdata.Columns["Fnc_Neg"].HeaderText = "Fnc\r\nNegative Moment";
                        gridShowdata.Columns["Compact"].HeaderText = "Classification";

                        gridShowdata.Columns["Su_top"].HeaderText = "fbu\r\nTop Flange";
                        gridShowdata.Columns["Check_com_top"].HeaderText = "Com Checking\r\nTop Flange";
                        gridShowdata.Columns["Check_com_top_ratio"].HeaderText = "Ratio";
                        gridShowdata.Columns["Check_ten_top"].HeaderText = "Ten Checking\r\nTop Flange";
                        gridShowdata.Columns["Check_ten_top_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["Su_bot"].HeaderText = "fbu\r\nBottom Flange";
                        gridShowdata.Columns["Check_com_bot"].HeaderText = "Com Checking\r\nBottom Flange";
                        gridShowdata.Columns["Check_com_bot_ratio"].HeaderText = "Ratio";
                        gridShowdata.Columns["Check_ten_bot"].HeaderText = "Ten Checking\r\nBottom Flange";
                        gridShowdata.Columns["Check_ten_bot_ratio"].HeaderText = "Ratio";

                        gridShowdata.Columns["Check_moment"].HeaderText = "Check Moment";
                        gridShowdata.Columns["Check_moment_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeU4":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Stiffened,d0,k_shear,C,Vu,Vui,Vr,Check_shear,Check_shear_ratio", ngirder, "CCheckULS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Stiffened"].HeaderText = "Classification";
                        gridShowdata.Columns["k_shear"].HeaderText = "k";
                        gridShowdata.Columns["Check_shear"].HeaderText = "Check Shear";
                        gridShowdata.Columns["Check_shear_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeS1":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Ss2_top,Ss2_bot,Rh,RhFytop,RhFybot,Check_topflange,Check_topflange_ratio,Check_botflange,Check_botflange_ratio", ngirder, "CCheckSLS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Ss2_top"].HeaderText = "fbu\r\nTop Flange";
                        gridShowdata.Columns["Ss2_bot"].HeaderText = "fbu\r\nBottom Flange";
                        gridShowdata.Columns["RhFytop"].HeaderText = "0.95RhFyf\r\nTop Flange";
                        gridShowdata.Columns["RhFybot"].HeaderText = "0.95RhFyf\r\nBottom Flange";
                        gridShowdata.Columns["Check_topflange"].HeaderText = "Check ff\r\nTop Flange";
                        gridShowdata.Columns["Check_topflange_ratio"].HeaderText = "Ratio";
                        gridShowdata.Columns["Check_botflange"].HeaderText = "Check ff\r\nBottom Flange";
                        gridShowdata.Columns["Check_botflange_ratio"].HeaderText = "Ratio";
                    }
                    break;

                case "NodeS2":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Ss2_top,Ss2_bot,d,Dc,ds,Psi,k_bend,Fcrw,fc,Check_buckling,Check_buckling_ratio", ngirder, "CCheckSLS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Ss2_top"].HeaderText = "fbu\r\nTop Flange";
                        gridShowdata.Columns["Ss2_bot"].HeaderText = "fbu\r\nBottom Flange";
                        gridShowdata.Columns["Psi"].HeaderText = "Ψ";
                        gridShowdata.Columns["k_bend"].HeaderText = "k";
                        gridShowdata.Columns["Check_buckling"].HeaderText = "Check buckling";
                        gridShowdata.Columns["Check_buckling_ratio"].HeaderText = "Ratio";
                        
                    }
                    break;
                case "NodeS3":

                    {
                        DataTable dt = SQL.getresultdata("Element,Joint,Station,Flexure,M4,Mw,MLLmin,MLLmax,Srebar,fs,O8Fy,Check_fs,Check_fs_ratio", ngirder, "CCheckSLS");
                        gridShowdata.DataSource = dt;
                        gridShowdata.Columns["Check_fs"].HeaderText = "Check fs";
                        gridShowdata.Columns["Check_fs_ratio"].HeaderText = "Ratio";
                        

                    }
                    break;


            }


            Changecolor(gridShowdata);
             
        }

        private void Changecolor(DataGridView a)
        {

            a.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 89, 103);
            a.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(183, 222, 232);
            a.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            a.EnableHeadersVisualStyles = false;
            a.ClearSelection();
            a.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            a.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

            for (int i = 0; i < a.RowCount; i++)
            {
                if (Convert.ToDouble(a.Rows[i].Cells[1].Value) % 2 == 0)
                {
                    a.Rows[i].DefaultCellStyle.BackColor = Color.White;

                }
                else
                {
                    a.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);

                }
            }

            for (int i = 0; i < a.ColumnCount; i++)
            {
                a.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                a.Columns[i].Width = 110;
            }
                

            for (int i = 3; i < a.ColumnCount; i++)
            {
                a.Columns[i].DefaultCellStyle.Format = "0.000";
            }
        }
    }
}
