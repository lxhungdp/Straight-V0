using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;

namespace V2
{
    public partial class Form1 : Form
    {
        string constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Google Drive\PUS Program\PUS\Database\PUS1.accdb";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Generate the connection      
            IDbConnection connection = new OleDbConnection(constring);
            OleDbConnection con = new OleDbConnection(constring);

            //Adding all dimension to Node
            string inputstr = "select Label, Sta, R, ntop, btop, ttop, ctop, bbot, tbot, cbot, D, tw, Hc, ts, bs, th, bh, b, w, a, drt, art, crt, drb, arb, crb, srb, nsb, Hsb, tsb, nst, Hst, tst, ds, d0, Lb from Dimension";
            List<Node> Node = connection.Query<Node>(inputstr).ToList();

            //Adding material to Material
            DataTable Mattb = new DataTable();
            Mattb = DB.DBtoDT("Select * from Material", con);
            Material.Fyf = Convert.ToDouble(Mattb.Rows[0]["Fyf"]);
            Material.Fuf = Convert.ToDouble(Mattb.Rows[0]["Fuf"]);
            Material.Fyw = Convert.ToDouble(Mattb.Rows[0]["Fyw"]);
            Material.Fuw = Convert.ToDouble(Mattb.Rows[0]["Fuw"]);
            Material.Es = Convert.ToDouble(Mattb.Rows[0]["Es"]);
            Material.fckd = Convert.ToDouble(Mattb.Rows[0]["fckd"]);
            Material.fckb = Convert.ToDouble(Mattb.Rows[0]["fckb"]);
            Material.Fyb = Convert.ToDouble(Mattb.Rows[0]["Fyb"]);
            Material.Esb = Convert.ToDouble(Mattb.Rows[0]["Esb"]);
            Material.Ys = Convert.ToDouble(Mattb.Rows[0]["Ys"]);
            Material.Yc = Convert.ToDouble(Mattb.Rows[0]["Yc"]);
            Material.forms = Convert.ToDouble(Mattb.Rows[0]["forms"]);


            //Adding Moment, Shear and Torsion from Database to object Node
            DataTable MST = new DataTable();
            MST = DB.DBtoDT("Select * from MST", con);
            for (int i = 0; i < Node.Count; i++)
            {
                Node[i].M1 = Convert.ToDouble(MST.Rows[i]["M1"]);
                Node[i].M2 = Convert.ToDouble(MST.Rows[i]["M2"]);
                Node[i].M3 = Convert.ToDouble(MST.Rows[i]["M3"]);
                Node[i].M4 = Convert.ToDouble(MST.Rows[i]["M4"]);
                Node[i].Mw = Convert.ToDouble(MST.Rows[i]["Mw"]);
                Node[i].MTmax = Convert.ToDouble(MST.Rows[i]["MTmax"]);
                Node[i].MTmin = Convert.ToDouble(MST.Rows[i]["MTmin"]);
                Node[i].MLmax = Convert.ToDouble(MST.Rows[i]["MLmax"]);
                Node[i].MLmin = Convert.ToDouble(MST.Rows[i]["MLmin"]);

                Node[i].S1 = Convert.ToDouble(MST.Rows[i]["S1"]);
                Node[i].S2 = Convert.ToDouble(MST.Rows[i]["S2"]);
                Node[i].S3 = Convert.ToDouble(MST.Rows[i]["S3"]);
                Node[i].S4 = Convert.ToDouble(MST.Rows[i]["S4"]);
                Node[i].Sw = Convert.ToDouble(MST.Rows[i]["Sw"]);
                Node[i].STmax = Convert.ToDouble(MST.Rows[i]["STmax"]);
                Node[i].STmin = Convert.ToDouble(MST.Rows[i]["STmin"]);
                Node[i].SLmax = Convert.ToDouble(MST.Rows[i]["SLmax"]);
                Node[i].SLmin = Convert.ToDouble(MST.Rows[i]["SLmin"]);

                Node[i].T1 = Convert.ToDouble(MST.Rows[i]["T1"]);
                Node[i].T2 = Convert.ToDouble(MST.Rows[i]["T2"]);
                Node[i].T3 = Convert.ToDouble(MST.Rows[i]["T3"]);
                Node[i].T4 = Convert.ToDouble(MST.Rows[i]["T4"]);
                Node[i].Tw = Convert.ToDouble(MST.Rows[i]["Tw"]);
                Node[i].TTmax = Convert.ToDouble(MST.Rows[i]["TTmax"]);
                Node[i].TTmin = Convert.ToDouble(MST.Rows[i]["TTmin"]);
                Node[i].TLmax = Convert.ToDouble(MST.Rows[i]["TLmax"]);
                Node[i].TLmin = Convert.ToDouble(MST.Rows[i]["TLmin"]);

            }

            // Write Sectional Properties to Table Sec
            List<Table_Sec> Table_Sec = new List<Table_Sec>();
            Table_Sec = Node.Select(p => new Table_Sec
            {
                Label = p.Label,
                A1 = p.A1(),
                I1 = p.I1(),
                YU1 = p.YU1(),
                YL1 = p.YL1(),
                SU1 = p.SU1(),
                SL1 = p.SL1(),
                A2s = p.A2s(),
                I2s = p.I2s(),
                YU2s = p.YU2s(),
                YL2s = p.YL2s(),
                SU2s = p.SU2s(),
                SL2s = p.SL2s(),
                A2l = p.A2l(),
                I2l = p.I2l(),
                YU2l = p.YU2l(),
                YL2l = p.YL2l(),
                SU2l = p.SU2l(),
                SL2l = p.SL2l(),
                A3s = p.A3s(),
                I3s = p.I3s(),
                YU3s = p.YU3s(),
                YL3s = p.YL3s(),
                SU3s = p.SU3s(),
                SL3s = p.SL3s(),
                A3l = p.A3l(),
                I3l = p.I3l(),
                YU3l = p.YU3l(),
                YL3l = p.YL3l(),
                SU3l = p.SU3l(),
                SL3l = p.SL3l(),
                A4s = p.A4s(),
                I4s = p.I4s(),
                YU4s = p.YU4s(),
                YL4s = p.YL4s(),
                SU4s = p.SU4s(),
                SL4s = p.SL4s(),
                A4l = p.A4l(),
                I4l = p.I4l(),
                YU4l = p.YU4l(),
                YL4l = p.YL4l(),
                SU4l = p.SU4l(),
                SL4l = p.SL4l(),
                A5s = p.A5s(),
                I5s = p.I5s(),
                YU5s = p.YU5s(),
                YL5s = p.YL5s(),
                SU5s = p.SU5s(),
                SL5s = p.SL5s(),
                A5l = p.A5l(),
                I5l = p.I5l(),
                YU5l = p.YU5l(),
                YL5l = p.YL5l(),
                SU5l = p.SU5l(),
                SL5l = p.SL5l()
            }).ToList();            
            DB.ListtoDB(Table_Sec, "Sec", con);

            // Write Stress to Table Stress
            List<Table_Stress> Table_Stress = new List<Table_Stress>();
            Table_Stress = Node.Select(p => new Table_Stress
            {
                Label = p.Label,
                Flexure = p.Flexure(),
                Sc_top = p.Sc_top(),
                Sc_bot = p.Sc_bot(),
                Su_top = p.Su_top(),
                Su_bot = p.Su_bot(),
                Ss1_top = p.Ss1_top(),
                Ss1_bot = p.Ss1_bot(),
                Ss2_top = p.Ss2_top(),
                Ss2_bot = p.Ss2_bot(),
                Sfmax_top = p.Sfmax_top(),
                Sfmax_bot = p.Sfmax_bot(),
                Sfmin_top = p.Sfmin_top(),
                Sfmin_bot = p.Sfmin_bot()
            }).ToList();
            DB.ListtoDB(Table_Stress, "Stress", con);

            // Write Cons to Table Cons
            List<Table_Cons> Table_Cons = new List<Table_Cons>();

            Table_Cons = Node.Select(p => new Table_Cons
            {
                Label = p.Label,
                Flexure = p.Flexure(),
                Sc_com = p.Sc_com(),
                Sc_ten = p.Sc_ten(),
                rt = p.rt(),
                fl1 = p.fl1(),
                Fcr = p.Fcr(),
                Lp = p.Lp(),
                fl = p.fl(),
                CheckC_fl = p.CheckC_fl(),
                Rh = p.Rh().Item1,
                A0_NC = p.A0_NC(),
                fv_NC = p.fv_NC(),
                Delta = p.Delta().Item1,
                Fnc_LB = p.Fnc_LB(),
                Fnc_LTB = p.Fnc_LTB(),
                Fnc_OF = p.Fnc_OF(),
                k_plate = p.k_plate(),
                ks = p.ks(),
                Fcb = p.Fcb().Item1,
                Fcv = p.Fcv(),
                Fnc_BF = p.Fnc().Item1,
                Dc = p.Dc(),
                Slender = p.Slender(),
                fbufl_com = p.fbufl_com(),
                CheckC_comOF = p.CheckC_comOF(),
                fbufl3_com = p.fbufl3_com(),
                CheckC_com = p.CheckC_com(),
                fbufl_ten = p.fbufl_ten(),
                Fnt = p.Fnt().Item1,
                CheckC_ten = p.CheckC_ten(),
                Fcrw = p.Fcrw(),
                CheckC_buckling = p.CheckC_buckling(),
                Vui = p.Vui().Item1,
                Vn = p.Vn().Item1,
                Check_shear = p.Check_shear().Item1
            }).ToList();
            DB.ListtoDB(Table_Cons, "Check_Cons", con);

            // Write ULS to Table ULS
            List<Table_ULS> Table_ULS = new List<Table_ULS>();

            Table_ULS = Node.Select(p => new Table_ULS
            {
                Label = p.Label,
                Ptop = p.Ptop(),
                Pw = p.Pw(),
                Pbot = p.Pbot(),
                Ps = p.Ps(),
                Psb = p.Psb(),
                Pst = p.Pst(),
                Prb = p.Prb(),
                Prt = p.Prt(),
                Prbot = p.Prbot(),
                Pcom = p.Pcom(),
                PNA = p.PNA().Item1,
                Ypna = p.PNA().Item2,
                Mp = p.PNA().Item3,
                My = p.My(),
                Compare_Mp = p.Compare_Mp(),
                Dp = p.Dp(),
                Dt = p.Dt(),
                CheckDuctility = p.CheckDuctility(),
                fdeck = p.fdeck(),
                fbot = p.fbot(),
                Checkfdeck = p.Checkfdeck(),
                Checkfbot = p.Checkfbot(),
                Fcb = p.Fcb().Item2,
                Fcv = p.Fcv(),
                Fnc = p.Fnc().Item2,
                Fnt = p.Fnt().Item2,
                Mn = p.Mn(),
                Dcp = p.Dcp(),
                Compact = p.Compact(),
                CheckU_com = p.CheckU_com(),
                CheckU_ten = p.CheckU_ten(),
                C = p.C().Item2,
                Vn = p.Vn().Item2,
                Vu = p.Vu().Item2,
                Check_shear = p.Check_shear().Item2
            }).ToList();
            DB.ListtoDB(Table_ULS, "Check_ULS", con);


            //MessageBox.Show(Node[0].GetType().GetProperties().ToString());

            var bs = new BindingSource();
            bs.DataSource = Table_Cons;
            dataGridView1.DataSource = bs;
        }
             
    }
}
