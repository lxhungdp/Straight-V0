using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using Tools;

namespace Checking
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            string constring = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + constring + @"\PUS1.accdb";
            
            IDbConnection connection = new OleDbConnection(constring);
            OleDbConnection con = new OleDbConnection(constring);

            //Adding all dimension to Dim
            string inputstr = "select * from Dimension";
            List<Dim> Dim = connection.Query<Dim>(inputstr).ToList();

            //Adding material to Material
            DataTable Mattb = new DataTable();
            Mattb = Access.getDataTable("Select * from Material", con);
            Material.Flange = (Mattb.Rows[0]["Flange"]).ToString();
            Material.Web = (Mattb.Rows[0]["Web"]).ToString();
            Material.Es = Convert.ToDouble(Mattb.Rows[0]["Es"]);
            Material.fckd = Convert.ToDouble(Mattb.Rows[0]["fckd"]);
            Material.fckb = Convert.ToDouble(Mattb.Rows[0]["fckb"]);
            Material.Fyb = Convert.ToDouble(Mattb.Rows[0]["Fyb"]);
            Material.Esb = Convert.ToDouble(Mattb.Rows[0]["Esb"]);
            Material.Ys = Convert.ToDouble(Mattb.Rows[0]["Ys"]);
            Material.Yc = Convert.ToDouble(Mattb.Rows[0]["Yc"]);
            Material.forms = Convert.ToDouble(Mattb.Rows[0]["forms"]);


            //Calculate Sectional Properties and write to Sec table database
            List<Sec> Sec = new List<Sec>();

            for (int i = 0; i < Dim.Count(); i++)
                Sec.Add(new Sec(Dim[i].Label, Dim[i].ntop, Dim[i].btop, Dim[i].ttop, Dim[i].ctop, Dim[i].bbot, Dim[i].tbot, Dim[i].cbot, Dim[i].D, Dim[i].tw, Dim[i].Hc, Dim[i].ts, Dim[i].bs, Dim[i].th,
            Dim[i].bh, Dim[i].b, Dim[i].w, Dim[i].a, Dim[i].drt, Dim[i].art, Dim[i].crt, Dim[i].drb, Dim[i].arb, Dim[i].crb, Dim[i].srb, Dim[i].nsb, Dim[i].Hsb,
            Dim[i].tsb, Dim[i].nst, Dim[i].Hst, Dim[i].tst, Dim[i].S, Dim[i].Hw, Dim[i].Ac, Dim[i].Ic, Dim[i].As, Dim[i].Is1, Dim[i].Ah, Dim[i].Ih, Dim[i].Art, Dim[i].Arb, Dim[i].Irt, Dim[i].Irb));

            Access.writeList(Sec, "Sec", con, "All");

            //Adding all MST to MST
            inputstr = "select * from MST";
            List<MST> MST = connection.Query<MST>(inputstr).ToList();


            //Calculate Stress and write to Stress table database
            List<Stress> Stress = new List<Stress>();

            for (int i = 0; i < Dim.Count(); i++)
                Stress.Add(new Stress(Dim[i].Label, Sec[i].A1, Sec[i].I1, Sec[i].YU1, Sec[i].YL1, Sec[i].SU1, Sec[i].SL1, Sec[i].A2s, Sec[i].I2s, Sec[i].YU2s, Sec[i].YL2s, Sec[i].SU2s, Sec[i].SL2s, Sec[i].A2l, Sec[i].I2l, Sec[i].YU2l, Sec[i].YL2l, Sec[i].SU2l, Sec[i].SL2l,
            Sec[i].A3s, Sec[i].I3s, Sec[i].YU3s, Sec[i].YL3s, Sec[i].SU3s, Sec[i].SL3s, Sec[i].A3l, Sec[i].I3l, Sec[i].YU3l, Sec[i].YL3l, Sec[i].SU3l, Sec[i].SL3l,
            Sec[i].A4s, Sec[i].I4s, Sec[i].YU4s, Sec[i].YL4s, Sec[i].SU4s, Sec[i].SL4s, Sec[i].A4l, Sec[i].I4l, Sec[i].YU4l, Sec[i].YL4l, Sec[i].SU4l, Sec[i].SL4l,
            MST[i].M1, MST[i].M2, MST[i].M3, MST[i].M4, MST[i].Mw, MST[i].MLLmax, MST[i].MLLmin, MST[i].MLLfmax, MST[i].MLLfmin));

            Access.writeList(Stress, "Stress", con, "Label,Flexure,Sc_top,Sc_bot,Su_top,Su_bot,Ss1_top,Ss1_bot,Ss2_top,Ss2_bot,Sfmax_top,Sfmax_bot,Sfmin_top,Sfmin_bot");

            //Checking constructibility and wite to database
            List<Check_Cons> Check_Cons = new List<Check_Cons>();

            for (int i = 0; i < Dim.Count(); i++)
                Check_Cons.Add(new Check_Cons(Dim[i].Label, Stress[i].Flexure, Dim[i].R, Dim[i].ntop, Dim[i].btop, Dim[i].ttop, Dim[i].bbot, Dim[i].tbot, Dim[i].cbot, Dim[i].D,
                Dim[i].tw, Dim[i].b, Dim[i].w, Dim[i].ts, Dim[i].nst, Dim[i].Hst, Dim[i].tst, Dim[i].nsb, Dim[i].Hsb, Dim[i].tsb, Dim[i].Lb, Dim[i].ns, Dim[i].d0, Sec[i].A1,
                Dim[i].Ac, Dim[i].As, Dim[i].Ah, Dim[i].S, Dim[i].Hw,
                MST[i].M1, MST[i].M2, MST[i].M3, MST[i].S1, MST[i].S2, MST[i].S3, MST[i].T1, MST[i].T2, MST[i].T3,
                Sec[i].YU1, Sec[i].YL1, Sec[i].YU2s, Sec[i].YL2s, Stress[i].Sc_top, Stress[i].Sc_bot,Material.Flange,Material.Web,Dim[i].Sta));

            Access.writeList(Check_Cons, "Check_Cons", con, "Label,Sta,Flexure,Sc_com,Sc_ten,Mlw,Mlo,Mlf,Mlc,rt,fl1,Fcr,Lp,fl,fy06,Check_fl,Check_fl_ratio,Rh,A0_NC,fv_NC,Delta,Fnc_LB,Fnc_LTB,Fnc_OF,k_plate,ks" +
                ",Fcb,Fcv,Fnc_BF,Fnc,Dc,Slender,fbufl_com,Check_comOF,fbufl3_com,Check_com,fbufl_ten,Fnt,Check_ten,Fcrw,Check_buckling,Vui,Vn,Check_shear,Sc_top,Sc_bot");

            //Checking ULS and wite to database
            List<Check_ULS> Check_ULS = new List<Check_ULS>();

            for (int i = 0; i < Dim.Count(); i++)
                Check_ULS.Add(new Check_ULS(Dim[i].Label, Stress[i].Flexure, Dim[i].R, Dim[i].ntop, Dim[i].btop, Dim[i].ttop, Dim[i].bbot, Dim[i].tbot, Dim[i].cbot, Dim[i].w, Dim[i].a, Dim[i].b, Dim[i].D, Dim[i].tw, Dim[i].nsb, Dim[i].tsb,
            Dim[i].Hsb, Dim[i].nst, Dim[i].tst, Dim[i].Hst, Dim[i].d0, Dim[i].ns, Dim[i].Arb, Dim[i].Art, Dim[i].srb, Dim[i].Ac, Dim[i].th, Dim[i].ts, Dim[i].crt, Dim[i].crb, Dim[i].Hc, Dim[i].S, Dim[i].Hw, Dim[i].As,
            MST[i].M1, MST[i].M2, MST[i].M3, MST[i].M4, MST[i].Mw, MST[i].MLLmax, MST[i].MLLmin, MST[i].T4, MST[i].Tw, MST[i].TLLmax, MST[i].TLLmin, MST[i].S1, MST[i].S2, MST[i].S3, MST[i].S4, MST[i].Sw, MST[i].SLLmax, MST[i].SLLmin,
            Sec[i].SU1, Sec[i].SL1, Sec[i].I2s, Sec[i].YL2s, Sec[i].SL2s, Sec[i].SU2l, Sec[i].SL2l, Sec[i].I3s, Sec[i].YU3s, Sec[i].YL3s, Sec[i].SU3s, Sec[i].SL3s, Sec[i].SU3l, Sec[i].SL3l, Sec[i].I4s, Sec[i].YU4s, Sec[i].YL4s,
            Sec[i].SU4s, Sec[i].SL4s, Sec[i].SU4l, Sec[i].SL4l, Check_Cons[i].fv_NC,
             Stress[i].Su_bot, Stress[i].Su_top, Check_Cons[i].xf2, Check_Cons[i].k_plate, Check_Cons[i].Fcv, Check_Cons[i].Vp,Material.Flange,Material.Web));

            Access.writeList(Check_ULS, "Check_ULS", con, "Label,Ptop,Pw,Pbot,Ps,Psb,Pst,Prb,Prt,Prbot,Pcom,PNA,Ypna,Mp,My,Compare_Mp,Dp,Dt,CheckDuctility,fdeck,fbot" +
                ",Checkfdeck,Checkfbot,Fcb,Fcv,Fnc,Fnt,Mn,Dcp,Compact,Check_com,Check_ten,Check_moment,C,Vn,Vu,Check_shear");

            //Checking SLS and wite to database
            List<Check_SLS> Check_SLS = new List<Check_SLS>();

            for (int i = 0; i < Dim.Count(); i++)
                Check_SLS.Add(new Check_SLS(Dim[i].Label, Stress[i].Flexure, Check_ULS[i].Compact, Stress[i].Ss2_top, Stress[i].Ss2_bot, Check_ULS[i].Rh, MST[i].M4, MST[i].Mw, MST[i].MLLmin, Check_ULS[i].Sdeck, Check_ULS[i].Sbot1, Check_ULS[i].Sbot2, Dim[i].Hc,
            Dim[i].ttop, Dim[i].tbot, Dim[i].D, Check_Cons[i].tfc, Dim[i].ns, Dim[i].Hw, Dim[i].tw, Dim[i].th, Dim[i].ts, Dim[i].crt, Sec[i].I3s, Sec[i].YL3s, Sec[i].I4s, Sec[i].YL4s, Material.Flange, Material.Web, Check_Cons[i].ds));

            Access.writeList(Check_SLS, "Check_SLS", con, "Label,Flexure,Compact,Ss2_top,Ss2_bot,RhFy,Check_flange,Check_flange_ratio," +
                "Dc,k_bend,Fcrw,fc,Check_buckling,Srebar,fs,Check_fs");

            //Checking FLS and wite to database
            double ADTT = 1500;
            List<Check_FLS> Check_FLS = new List<Check_FLS>();
            for (int i = 0; i < Dim.Count(); i++)            
                
                Check_FLS.Add(new Check_FLS(Dim[i].Label, Stress[i].Flexure, Stress[i].S1_top, Stress[i].S1_bot, Stress[i].S2_top, Stress[i].S2_bot, Stress[i].S3_top_pos, Stress[i].S3_bot_pos, Stress[i].S3_top_negl, Stress[i].S3_bot_negl,
            Stress[i].S4_top_pos, Stress[i].S4_bot_pos, Stress[i].S4_top_neg, Stress[i].S4_bot_neg, Stress[i].Sfmax_top, Stress[i].Sfmin_top, Stress[i].Sfmax_bot, Stress[i].Sfmin_bot, Dim[i].Type, Check_Cons[i].Vn,
            MST[i].S1, MST[i].S2, MST[i].S3, MST[i].S4, MST[i].Sw, MST[i].SLLfmax, MST[i].SLLfmin, Dim[i].S,MST[i].MLLfmax, MST[i].MLLfmin,ADTT));

            Access.writeList(Check_FLS, "Check_FLS", con, "Label,Flexure,fDC_top,fDC_bot,Deltaf_top,Deltaf_bot,Check_stiffener,Check_cross,Check_stud,Vcr,Vui,Check_shear,MLLfmax,MLLfmin,SLLfmax,SLLfmin");

                       
            var bs = new BindingSource();
            bs.DataSource = Stress;
            dataGridView1.DataSource = bs;

        }

        private void constructibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCons Cons_Form = new frmCons();
            Cons_Form.ShowDialog();

        }
    }

    

}

