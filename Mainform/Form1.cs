using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Mainform.Properties;
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
using Provider;
using Classes;


namespace Mainform
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {

            IniGeneral();
            Inimaterial();
            IniBridge();
            IniLoadings();
            IniOther();
            IniAnalysis();

        }


        private void IniGeneral()
        {
            cbType.SelectedIndex = 0;
            labelEx.Text = "Ex. For a bridges with 3 spans, the lengths are 30m, 40m, 30m, input should be 30+40+30";
           

            labelCode1.Text = "(1) 도로교 설계기준 (한계상태설계법) 해설, (2015) - (사)한국교량및구조공화회∙교량설계핵심기술연구단";
            labelCode2.Text = "(2) 강구조설계기준 및 해설 (하중저항계수법), (2018) - (사)한국강구조학회";
            labelCode3.Text = "(3) AASHTO LRFD, (2017)";


            ShowpageGeneral();

        }

        private void Inimaterial()
        {
            //Tag material
            txtMatname.Text = "Mat1";
            cbMattype.SelectedIndex = 0;
            numWs.Value = 75;
            numEs.Value = 210000;
            numG.Value = 81000;
            numFy.Value = 380;
            numFu.Value = 500;
            numWc.Value = 25;
            numFc.Value = 35;
            pictureMat.Load(Const.Constring + @"\Picture\Mat.PNG");

            DataTable DTMat = Access.getDataTable("Select Name from Mat", con);

            for (int i = 0; i < DTMat.Rows.Count; i++)
            {
                listBox1.Items.Add(DTMat.Rows[i][0].ToString());
            }

            List<string> Mitems = new List<string> { "Flange", "Web", "Diaphragm", "Longitudinal Rib", "Longitudinal Stiffener", "Transverse Stiffener", "Deck", "Bottom Concrete", "Rebar in Deck", "Rebar in Bottom concrete", "Cross Beam", "Stringer", "Splice", "Shear Connector" };
            dgvMat.ColumnCount = 2;
            dgvMat.Columns[0].HeaderText = "No.";
            dgvMat.Columns[1].HeaderText = "Items";
            dgvMat.Columns[0].Width = 50;
            dgvMat.Columns[0].ReadOnly = true;
            dgvMat.Columns[1].ReadOnly = true;
            dgvMat.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvMat.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            for (int i = 0; i < Mitems.Count; i++)
                dgvMat.Rows.Add((i + 1).ToString(), Mitems[i]);

            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            combo.DataSource = listBox1.Items;
            dgvMat.Columns.Add(combo);


            //foreach (string s in listBox1.Items)
            //    combo.Items.Add(s);
            //dgvMat.Columns.Add(combo);

            combo.HeaderText = "Material";
            combo.Name = "Material";
            combo.Width = 100;

        }
        private void IniBridge()
        {
            flowLayoutPanel1.BackColor = Color.FromArgb(33, 115, 70);
            panel1.BackColor = Color.FromArgb(33, 115, 70);
            btGeneral.BackColor = Color.FromArgb(33, 115, 70);
            btBridge.BackColor = Color.FromArgb(33, 115, 70);
            btGeneralD.BackColor = Color.FromArgb(44, 152, 93);
            btGirderD.BackColor = Color.FromArgb(44, 152, 93);
            btHaunch.BackColor = Color.FromArgb(44, 152, 93);
            btStif.BackColor = Color.FromArgb(44, 152, 93);
            btOther.BackColor = Color.FromArgb(44, 152, 93);
            btBack.BackColor = Color.FromArgb(33, 115, 70);
            btApply.BackColor = Color.FromArgb(33, 115, 70);
            btNext.BackColor = Color.FromArgb(33, 115, 70);
            btMaterial.BackColor = Color.FromArgb(33, 115, 70);
            btAnalysis.BackColor = Color.FromArgb(33, 115, 70);
            btLiveLoad.BackColor = Color.FromArgb(33, 115, 70);

            Setgridview(gridTop);
            Setgridview(gridBot);
            Setgridview(gridWeb);

            Setgridview(gridTrib);
            Setgridview(gridBrib);
            
            Setgridview(gridTranstif);

            picSection.Load(Const.Constring + @"\Picture\Section.PNG");
            picCross.Load(Const.Constring + @"\Picture\Cross.PNG");
            picH1.Load(Const.Constring + @"\Picture\H1.PNG");
            picH2.Load(Const.Constring + @"\Picture\H2.PNG");
            picH3.Load(Const.Constring + @"\Picture\H3.PNG");
            picds.Load(Const.Constring + @"\Picture\ds.PNG");




        }

        private void IniLoadings()
        {
            List<string> LLiveload = new List<string> { "KL510", "DB24", "HL93" };
            foreach (string L in LLiveload)
                cbLLiveload.Items.Add(L);
            checkBox2.Checked = false;
            cbLLiveload.Enabled = false;


            dgvLane.ColumnCount = 2;
            dgvLane.RowCount = 5;
            dgvLane.Columns[0].HeaderText = "Number of lane";
            dgvLane.Columns[1].HeaderText = "Multi-lane factor";

            DataTable DTtruck = Access.getDataTable("Select * from Truck", con);
            dgvTruck.DataSource = DTtruck;



            //Loading database to page
            DataTable DTloading = Access.getDataTable("Select * from Loadings", con);
            dgvLane.Rows[0].Cells[1].Value = DTloading.Rows[0]["Lane1"].ToString();
            dgvLane.Rows[1].Cells[1].Value = DTloading.Rows[0]["Lane2"].ToString();
            dgvLane.Rows[2].Cells[1].Value = DTloading.Rows[0]["Lane3"].ToString();
            dgvLane.Rows[3].Cells[1].Value = DTloading.Rows[0]["Lane4"].ToString();
            dgvLane.Rows[4].Cells[1].Value = DTloading.Rows[0]["Lane5"].ToString();

            dgvLane.Rows[0].Cells[0].Value = "1";
            dgvLane.Rows[1].Cells[0].Value = "2";
            dgvLane.Rows[2].Cells[0].Value = "3";
            dgvLane.Rows[3].Cells[0].Value = "4";
            dgvLane.Rows[4].Cells[0].Value = "> 5";
            dgvLane.Columns[0].ReadOnly = true;

            numLane.Value = Convert.ToDecimal(DTloading.Rows[0]["Laneload"]);
            numPe.Value = Convert.ToDecimal(DTloading.Rows[0]["Pedestrian"]);
            numOverload.Value = Convert.ToDecimal(DTloading.Rows[0]["Overload"]);
            numCons.Value = Convert.ToDecimal(DTloading.Rows[0]["Consload"]);
            numPara.Value = Convert.ToDecimal(DTloading.Rows[0]["Paraload"]);

        }


        private void IniOther()
        {
            

            picBar.Load(Const.Constring + @"\Picture\Bar.PNG");
            DataTable DTbar = new DataTable();
            DTbar = Access.getDataTable("Select * from Barrier", con);
            string Barheader = "Left-side barrier,Right-side barrier,Jersey barrier";
            DGV.DTtoGrid(dgvBar, DTbar, Barheader);


        }

        private void IniAnalysis()
        {
            richAnalysis.Text = "To analyze the bridge structure, the frame elements must be divided into segments. The more elements are divided, the more accurate the results will be, but the program execution time will be long." +
                " For this case, the divided segment length of 2m to 5m is recommended. To do that, the main girder elements are divided by the rules as follows";

        }


        private void Setgridview(DataGridView grid)
        {
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(189, 215, 238);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
        }

        private bool isCollapsed;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                btBridge.Image = Resources.Collapse_Arrow_20px;
                panelBP.Height += 10;
                if (panelBP.Size == panelBP.MaximumSize)
                {
                    timer1.Stop();
                    isCollapsed = false;
                }
            }
            else
            {
                btBridge.Image = Resources.Expand_Arrow_20px;
                panelBP.Height -= 10;
                if (panelBP.Size == panelBP.MinimumSize)
                {
                    timer1.Stop();
                    isCollapsed = true;
                }

            }
        }

        private void btBridge_Click(object sender, EventArgs e)
        {
            timer1.Start();

            ShowpageGrid("all", Aspan);

        }



        private void btGeneral_Click(object sender, EventArgs e)
        {
            ShowpageGeneral();


        }


        private void btAnalysis_Click(object sender, EventArgs e)
        {
            ShowpageAnalysis();
        }
        private void btMaterial_Click(object sender, EventArgs e)
        {
            ShowpageMaterial();
        }

        private void btLiveLoad_Click(object sender, EventArgs e)
        {
            ShowpageLoading();
        }

        private void btGeneralD_Click(object sender, EventArgs e)
        {
            ShowpageGrid("", Aspan);
        }

        private void btGirderD_Click(object sender, EventArgs e)
        {
            ShowpageDim("", Aspan);
        }
        private void btHaunch_Click(object sender, EventArgs e)
        {
            ShowpageHaunch("");
        }
        private void btStif_Click(object sender, EventArgs e)
        {
            ShowpageStiff("", Aspan);
        }
        private void btOther_Click(object sender, EventArgs e)
        {
            ShowpageOther("", Aspan);
        }

        //Control tab page
        void showtabpage(List<TabPage> a)
        {
            metroTabControl1.TabPages.Clear();
            foreach (TabPage page in metroTabControl1.TabPages)
                metroTabControl1.TabPages.Remove(page);
            foreach (TabPage a1 in a)
                metroTabControl1.TabPages.Add(a1);
        }


        void ShowpageGeneral()
        {
            List<TabPage> a = new List<TabPage> { pageGeneral };
            showtabpage(a);
            btApply.Text = "Apply";
        }

        void ShowpageMaterial()
        {
            List<TabPage> a = new List<TabPage> { pageMaterial };
            showtabpage(a);
            btApply.Text = "Apply";
        }
        void ShowpageLoading()
        {
            List<TabPage> a = new List<TabPage> { pageLoadings };
            showtabpage(a);
            btApply.Text = "Apply";
        }
        void ShowpageGrid(string a1, double[] a2)
        {
            if (a1 == "all")
            {
                List<TabPage> a = new List<TabPage>();
                if (a2.GetLength(0) > 1)
                    a = new List<TabPage> { pageGrid, pageHaunch, pageDim, pageStiffeners, pageOther };
                else
                    a = new List<TabPage> { pageGrid, pageDim, pageStiffeners, pageOther };

                showtabpage(a);

            }


            btApply.Text = "Apply";
            metroTabControl1.SelectedTab = pageGrid;
        }

        void ShowpageHaunch(string a1)
        {

            if (a1 == "all")
            {
                List<TabPage> a = new List<TabPage> { pageGrid, pageHaunch, pageDim, pageStiffeners, pageOther };
                showtabpage(a);

            }
            metroTabControl1.SelectedTab = pageHaunch;
            btApply.Text = "Apply";


        }

        void ShowpageDim(string a1, double[] a2)
        {
            if (a1 == "all")
            {
                List<TabPage> a = new List<TabPage>();
                if (a2.GetLength(0) > 1)
                    a = new List<TabPage> { pageGrid, pageHaunch, pageDim, pageStiffeners, pageOther };
                else
                    a = new List<TabPage> { pageGrid, pageDim, pageStiffeners, pageOther };
                showtabpage(a);
            }
            metroTabControl1.SelectedTab = pageDim;
            btApply.Text = "Apply";
        }
        void ShowpageStiff(string a1, double[] a2)
        {
            if (a1 == "all")
            {
                List<TabPage> a = new List<TabPage>();
                if (a2.GetLength(0) > 1)
                    a = new List<TabPage> { pageGrid, pageHaunch, pageDim, pageStiffeners, pageOther };
                else
                    a = new List<TabPage> { pageGrid, pageDim, pageStiffeners, pageOther };
                showtabpage(a);
            }

            metroTabControl1.SelectedTab = pageStiffeners;
            btApply.Text = "Apply";
        }

        void ShowpageOther(string a1, double[] a2)
        {
            if (a1 == "all")
            {
                List<TabPage> a = new List<TabPage>();
                if (a2.GetLength(0) > 1)
                    a = new List<TabPage> { pageGrid, pageHaunch, pageDim, pageStiffeners, pageOther };
                else
                    a = new List<TabPage> { pageGrid, pageDim, pageStiffeners, pageOther };
                showtabpage(a);
            }

            metroTabControl1.SelectedTab = pageOther;
            btApply.Text = "Apply";
        }

        void ShowpageAnalysis()
        {
            List<TabPage> a = new List<TabPage> { pageAnalysis };
            showtabpage(a);
            btApply.Text = "Run";
        }




        // Limit the input value in datagridview is only numec

        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // allow only number and dot
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            //only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }




        //Allow only input number and "+"
        private void txtSpan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '+'))
            {
                e.Handled = true;
            }
        }

        //Fill the girdSection
        private void fillAsection()
        {
            DGV.ArraytoGrid(gridSection, Asectiong);

            for (int i = 0; i < Asection.GetLength(1); i++)
            {
                DataGridViewComboBoxCell cbx = new DataGridViewComboBoxCell();
                cbx.Items.Add("Barrier");
                cbx.Items.Add("Liveload");
                cbx.Items.Add("Pedestrian");
                gridSection.Rows[1].Cells[i].Value = null;
                gridSection.Rows[1].Cells[i] = cbx;

                //Set the default value for combobox
                if (Asection[1, i] == 3)
                    gridSection.Rows[1].Cells[i].Value = (cbx.Items[2]).ToString();
                else if (Asection[1, i] == 2)
                    gridSection.Rows[1].Cells[i].Value = (cbx.Items[1]).ToString();
                else
                    gridSection.Rows[1].Cells[i].Value = (cbx.Items[0]).ToString();
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {
                        ShowpageMaterial();
                    }
                    break;

                case "pageMaterial":
                    {
                        ShowpageLoading();

                    }
                    break;

                case "pageLoadings":
                    {
                        ShowpageGrid("all", Aspan);

                        fillAsection();

                    }
                    break;


                case "pageGrid":
                    {
                        if (Aspan.GetLength(0) > 1)
                            ShowpageHaunch("");
                        else
                            ShowpageDim("", Aspan);
                    }
                    break;

                case "pageHaunch":
                    {

                        ShowpageDim("", Aspan);
                    }
                    break;



                case "pageDim":
                    {
                        gridTrib.MultiSelect = false;
                        gridBrib.MultiSelect = false;

                        ShowpageStiff("", Aspan);

                    }
                    break;

                case "pageStiffeners":
                    {
                        ShowpageOther("", Aspan);
                    }
                    break;
                case "pageOther":
                    {
                        ShowpageAnalysis();
                    }
                    break;


            }


        }

        private void btBack_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageAnalysis":
                    {
                        ShowpageOther("all", Aspan);
                    }
                    break;
                case "pageOther":
                    {
                        ShowpageStiff("", Aspan);
                    }
                    break;

                case "pageStiffeners":
                    {

                        ShowpageDim("", Aspan);

                    }
                    break;
                case "pageDim":
                    {
                        if (Aspan.GetLength(0) > 1)
                            ShowpageHaunch("");
                        else
                            ShowpageGrid("", Aspan);
                    }
                    break;

                case "pageHaunch":
                    {

                        ShowpageGrid("", Aspan);
                    }
                    break;

                case "pageGrid":
                    {
                        ShowpageLoading();
                    }
                    break;

                case "pageLoadings":
                    {
                        ShowpageMaterial();
                    }
                    break;

                case "pageMaterial":
                    {
                        ShowpageGeneral();
                    }
                    break;





            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(this.numgirder.Value);
        }


        static string DBstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Const.Constring + @"\PUS1.accdb";
        OleDbConnection con = new OleDbConnection(DBstring);

        double[,] Atranstif;
        double[,] Atranstif_grid;
        double[,] Akframe;        

        double[,] Across;
        double[,] Across_grid;
        double[] Across1;

        double[,] Atran;
        double[,] Asection = new double[2, 3];
        double[,] Asectiong = new double[2, 3];
        

        double[] Aspan;

        DataTable DThaunch, DTCBox;
        int pier; //Number of interior pier;

        double sumspan, sumsec;
        int ngirder;
        List<Node> Node;
        List<KFrame> KFrame = new List<KFrame>();
        

        double[,] Atop; //Atop consider the closed box, has 3 row: 1st length (including closed box section) 2nd: with, 3 rd: thickness        
        double[,] Atop_grid; //Has 5 row: 1st: ID = 1, add ID = 4; 2nd: order to mark color and lock closed box section, 3-5: same Atop2
        double[] Atop1; //Length same 1st row of Atop2, this is help to maintain the sum of length when modify

        double[,] Atrib; //Atrip consider the closed box, has 4 row: 1st length (including closed box section) 2nd: number, 3: depth, 4: thickness
        double[,] Atrib_grid; //Has 6 row: 1st: ID = 1, add ID = 4; 2nd: order to mark color and lock closed box section, 3-6: same Atrib

        double[,] Acon; //Bottom concrete: number of interior pier + 2 rows: 1st rows: length, next rows: depth of concrete, last row: 1 : left, 2 right
        double[,] Acon_grid;
        double[,] Acon1;

        double[,] Abot; //Length and thickness of bottom flange
        double[,] Aweb; //Length and thickness of web        

        double[,] Aribtop; //Has 4 rows: length, nst, Hst, tst
        double[,] Aribbot;


        private void btApply_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {

                        ngirder = Convert.ToInt32(numgirder.Value);

                        // Atran has 3 rows
                        // 1st row: length
                        //2nd row : order of sections - to set color
                        //3rd row: BeamID : main beam 1->5; stringer: 21 ->51
                        Atran = new double[3, ngirder + 1];
                        for (int i = 0; i < Atran.GetLength(1); i++)
                        {
                            Atran[0, i] = 2000;
                            if (i == 0 || i == Atran.GetLength(1) - 1)
                            {
                                Atran[1, i] = 0;
                                Atran[2, i] = 0;
                            }
                            else
                            {
                                Atran[1, i] = i;
                                Atran[2, i] = i;

                            }

                        }
                        gridTran.DataSource = null;
                        DGV.ArraytoGrid_tran(gridTran, Atran);
                        Deco(gridTran, Atran);


                        Aspan = Array.ConvertAll(txtSpan.Text.ToString().Split('+'), Double.Parse);
                        for (int i = 0; i < Aspan.GetLength(0); i++)
                            Aspan[i] = Aspan[i] * 1000;
                        sumspan = Aspan.Sum();


                        Abot = new double[2, 1] { { sumspan }, { 16 } };                        
                        DGV.ArraytoGrid(gridBot, Abot);

                        Aweb = new double[2, 1] { { sumspan }, { 12 }};                        
                        DGV.ArraytoGrid(gridWeb, Aweb);

                        Aribtop = new double[4, 1] { { sumspan }, { 0 }, { 0 }, { 0 } };                        
                        DGV.ArraytoGrid(gridTrib, Aribtop);

                        Aribbot = new double[4, 1] { { sumspan }, { 2 }, { 160 }, { 16 } };                        
                        DGV.ArraytoGrid(gridBrib, Aribbot);                       


                        //Default value of haunch
                        if (Aspan.GetLength(0) > 1)
                        {
                            pier = Aspan.GetLength(0) - 1;

                            //Haunch
                            DThaunch = new DataTable();
                            DThaunch.Columns.Add("L1");
                            DThaunch.Columns.Add("L2");
                            DThaunch.Columns.Add("L3");
                            DThaunch.Columns.Add("H1");
                            DThaunch.Columns.Add("H2");
                            DThaunch.Columns.Add("H3");
                            string Hauheader = "Support #1";

                            for (int i = 0; i < pier; i++)
                            {
                                if (i > 0)
                                    Hauheader = Hauheader + ",Support #" + (i + 1).ToString();

                                //Default value
                                if (Aspan[i] > 17500 && Aspan[i+1] > 17500)
                                    DThaunch.Rows.Add(15000, 5000, 15000, 2000, 2500, 2000);
                                else
                                    DThaunch.Rows.Add(0, 0, 0, 2000, 2000, 2000);

                            }
                            DGV.DTtoGrid(dgvHaunch, DThaunch, Hauheader);
                            //dgvHaunch.DataSource = DThaunch;
                            Chart.Haunch(Aspan, DThaunch, chartHaunch);

                            //Closed box section
                            DTCBox = new DataTable();
                            DTCBox.Columns.Add("L1");
                            DTCBox.Columns.Add("L2");


                            for (int i = 0; i < pier; i++)
                            {
                                if (i > 0)
                                    Hauheader = Hauheader + ",Support #" + (i + 1).ToString();
                                if (Aspan[i] > 17500 && Aspan[i + 1] > 5000)
                                    DTCBox.Rows.Add(5000, 5000);
                                else
                                    DTCBox.Rows.Add(0, 0);

                            }
                            DGV.DTtoGrid(dgvCBox, DTCBox, Hauheader);

                            //Bottom Concrete
                            //Create Acon
                            Acon = new double[pier + 2, 2];
                            Acon[0, 0] = 5000;
                            Acon[0, 1] = 5000;
                            for (int i = 0; i < pier; i++)
                            {
                                Acon[i + 1, 0] = 500;
                                Acon[i + 1, 1] = 500;
                            }
                            Acon[pier + 1, 0] = 1;
                            Acon[pier + 1, 1] = 2;

                            gridBCon.DataSource = null;
                            DGV.Acontogrid(gridBCon, Acon);

                            //Transfer DTCBox to Array
                            DThaunch = DGV.GridtoDT(dgvCBox);
                            Atop = Matrix.Atop_CBox(DThaunch, Aspan, 3);
                            panelToprib.Visible = true;
                            panelD.Visible = false;
                        }    
                        else
                        {
                            Atop = new double[3, 1] { { sumspan }, { 600 }, { 16 } } ;
                            DGV.ArraytoGrid(gridTop, Atop);

                            Atop_grid = new double[5, 1];
                            Atop1 = new double[1];
                            for (int i = 0; i < 1; i++)
                            {
                                Atop_grid[0, i] = 1; //Set all ID = 1
                                Atop_grid[1, i] = i + 1; //Set order 1-2-3
                                Atop_grid[2, i] = Atop[0, i]; //Set length
                                Atop1[i] = Atop[0, i];
                            }
                            Deco(gridTop, Atop_grid);

                            //Create Atrib and Atrib_grid
                            
                            Atrib = new double[4, 1] { { sumspan }, { 0 }, { 0 }, { 0 } };

                            //Fill to gridTrib dgv
                            DGV.ArraytoGrid(gridTrib, Atrib);

                            //Create Atrib_grid with 6 row
                            Atrib_grid = new double[6, Atrib.GetLength(1)];
                            for (int i = 0; i < Atrib.GetLength(1); i++)
                            {
                                Atrib_grid[0, i] = 1; //Set all ID = 1
                                Atrib_grid[1, i] = i + 1; //Set order 1-2-3
                                Atrib_grid[2, i] = Atrib[0, i]; //Set length

                            }
                            Deco(gridTrib, Atrib_grid);
                            panelToprib.Visible = false;
                            panelD.Visible = true;

                        }



                        // Convert 1D array Across and Across_grid
                        //Description about Across_grid
                        //Across_grid has 3 rows: 
                        //1st row = Type: 1 abut, 2 pier, 3 cross, 4 section changed
                        // 2nd row = order of section, to set different color for different sections
                        // 3rd row = length

                        Across_grid = new double[3, Aspan.GetLength(0)];
                        Across = new double[1, Aspan.GetLength(0)];
                        for (int i = 0; i < Aspan.GetLength(0); i++)
                        {
                            Across_grid[2, i] = Aspan[i];
                            Across_grid[0, i] = 2.0;
                            Across_grid[1, i] = i + 1;
                            Across[0, i] = Aspan[i];
                        }
                        Across_grid[0, 0] = 1.0;

                        gridCross.DataSource = null;

                        DGV.ArraytoGrid(gridCross, Across);

                        Deco(gridCross, Across_grid);

                        Asection = new double[2, 3];
                        // 1 = Barrier, 2 = Liveload; 3 = Pedestrian
                        for (int i = 0; i < Asection.GetLength(1); i++)
                        {
                            if (i == 0 || i == Asection.GetLength(1) - 1)
                                Asection[1, i] = 1;
                            else
                                Asection[1, i] = 2;
                        }


                        //Hide and show btHaunch
                        if (Aspan.GetLength(0) > 1)
                        {
                            btHaunch.Visible = true;
                            panelBP.MaximumSize = new Size(panelBP.Width, 294);
                        }
                        else
                        {
                            btHaunch.Visible = false;
                            panelBP.MaximumSize = new Size(panelBP.Width, 294 - 43);
                        }

                        
                        // Generate List of grid bridge
                        Node = Matrix.GenerateNode(Across_grid, Atran, ngirder);

                        // Plot to the chart
                        Chart.Bridgegrid(Node, gridchart);

                    }
                    break;

                case "pageMaterial":
                    {
                        DataTable DTMat = Access.getDataTable("Select * from Mat", con);

                        //Save all material from Mat to Listmat
                        List<Mat> Listmat = new List<Mat>();
                        Listmat = (from DataRow dr in DTMat.Rows
                                   select new Mat()
                                   {
                                       Name = dr["Name"].ToString(),
                                       Type = dr["Type"].ToString(),
                                       Ws = Convert.ToDouble(dr["Ws"]),
                                       Es = Convert.ToDouble(dr["Es"]),
                                       G = Convert.ToDouble(dr["G"]),
                                       Fy = Convert.ToDouble(dr["Fy"]),
                                       Fu = Convert.ToDouble(dr["Fu"]),
                                       Wc = Convert.ToDouble(dr["Wc"]),
                                       fc = Convert.ToDouble(dr["fc"]),
                                       Ec = Convert.ToDouble(dr["Ec"])
                                   }).ToList();


                        //Add assigned material to Mat1 database
                        List<Mat> Items = new List<Mat>();
                        Mat Mat1 = new Mat();
                        for (int i = 0; i < dgvMat.RowCount; i++)
                        {
                            if (dgvMat.Rows[i].Cells[2].Value != null)
                            {
                                Mat1 = new Mat();
                                Mat1.Name = dgvMat.Rows[i].Cells[1].Value.ToString();
                                Mat1.Type = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Type).FirstOrDefault();
                                Mat1.Ws = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Ws).FirstOrDefault();
                                Mat1.Es = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Es).FirstOrDefault();
                                Mat1.G = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.G).FirstOrDefault();
                                Mat1.Fy = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Fy).FirstOrDefault();
                                Mat1.Fu = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Fu).FirstOrDefault();
                                Mat1.Wc = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Wc).FirstOrDefault();
                                Mat1.fc = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.fc).FirstOrDefault();
                                Mat1.Ec = Listmat.Where(p => p.Name == dgvMat.Rows[i].Cells[2].Value.ToString()).Select(p => p.Ec).FirstOrDefault();
                                Items.Add(Mat1);
                            }
                        }

                        Access.delTable("Mat1", con);
                        if (Items.Count > 0)
                        {
                            Access.writeList(Items, "Mat1", con, "All");
                        }


                    }
                    break;

                case "pageLoadings":
                    {

                        List<List<double>> truck = new List<List<double>>();
                        List<double> truck1 = new List<double>();

                        for (int i = 0; i < dgvTruck.Rows.Count - 1; i++)
                        {
                            truck1 = new List<double>();
                            truck1.Add(Convert.ToDouble(dgvTruck.Rows[i].Cells[0].Value.ToString()));
                            truck1.Add(Convert.ToDouble(dgvTruck.Rows[i].Cells[1].Value.ToString()));
                            truck.Add(truck1);
                        }

                        Access.writetruck(truck, "Truck", con);

                        DataTable Loadings = new DataTable();
                        Loadings.Columns.Add("Laneload");
                        Loadings.Columns.Add("Pedestrian");
                        Loadings.Columns.Add("Overload");
                        Loadings.Columns.Add("Consload");
                        Loadings.Columns.Add("Paraload");
                        Loadings.Columns.Add("Lane1");
                        Loadings.Columns.Add("Lane2");
                        Loadings.Columns.Add("Lane3");
                        Loadings.Columns.Add("Lane4");
                        Loadings.Columns.Add("Lane5");


                        Loadings.Rows.Add(numLane.Value, numPe.Value, numOverload.Value, numCons.Value, numPara.Value, dgvLane.Rows[0].Cells[1].Value,
                            dgvLane.Rows[1].Cells[1].Value, dgvLane.Rows[2].Cells[1].Value, dgvLane.Rows[3].Cells[1].Value, dgvLane.Rows[4].Cells[1].Value);

                        Access.writeDataTable(Loadings, "Laneload,Pedestrian,Overload,Consload,Paraload,Lane1,Lane2,Lane3,Lane4,Lane5", "Loadings", con);

                    }
                    break;

                case "pageGrid":
                    {
                        // Generate List of grid bridge
                        Node = Matrix.GenerateNode(Across_grid, Atran, ngirder);


                        //Write to Database

                        Access.writeList(Node, "Node", con, "All");

                        // Plot to the chart
                        Chart.Bridgegrid(Node, gridchart);

                        //Fill the transverse stiffener grid
                        //CAN NOT use = for 2 matrices

                        //Atranstif has one row: length
                        //Atranstif_grid has 3 rows: 
                        //1st: type: (1 - abu, 2 - pier, 3 - crossbearm, 4 section changed, 5 long stiff, 6 transtiff)
                        //2nd: order : 1 2 3
                        //3rd: Length

                        Atranstif = (double[,])Across.Clone();

                        Atranstif_grid = new double[Across_grid.GetLength(0), Across_grid.GetLength(1)];

                        Across1 = new double[Across.GetLength(1)];
                        for (int i = 0; i < Across_grid.GetLength(1); i++)
                        {
                            Atranstif_grid[0, i] = Across_grid[0, i];
                            Atranstif_grid[1, i] = i + 1;
                            Atranstif_grid[2, i] = Across_grid[2, i];

                            Across1[i] = Across[0, i]; //Help to maintain the sum length of one section
                        }



                        DGV.ArraytoGrid(gridTranstif, Atranstif);

                        for (int i = 0; i < Asection.GetLength(1); i++)
                            Asection[1, i] = Asectiong[1, i];

                        //Add to gridCrossbeam
                        string Crossheader = "Exterior-Support Crossbeam";
                        List<int> type = Node.Select(p => p.Type).ToList();
                        int ncross = 1;

                        if (type.IndexOf(2) != -1)
                        {
                            Crossheader = Crossheader + ",Interior - Support Crossbeam";
                            ncross = ncross + 1;
                        }
                            
                        if (type.IndexOf(3) != -1)
                        {
                            Crossheader = Crossheader + ",General Crossbeam";
                            ncross = ncross + 1;
                        }
                            
                        
                        if (Node.Max(p => p.BeamID) > 10)
                        {
                            Crossheader = Crossheader + ",Stringer";
                            ncross = ncross + 1;
                        }

                        //Generate the initial value of dimension
                         List<Crossbeam> Crossbeam = new List<Crossbeam>();
                        for (int i = 0; i < ncross; i++)
                            Crossbeam.Add(new Crossbeam(10, 300, 10, 300, 1200, 12, 1));
                        Access.writeList(Crossbeam, "Crossbeam", con, "All");

                        DataTable DTcross = new DataTable();
                        DTcross = Access.getDataTable("Select * from Crossbeam", con);
                        //dgvCross.DataSource = DTcross;
                       
                        DGV.DTtoGrid(gridCrossbeam, DTcross, Crossheader);


                    }
                    break;

                case "pageHaunch":
                    {
                        Node = Node.Where(p => p.Type < 4).ToList();
                        //Save to DThaunch again             
                        DThaunch = DGV.GridtoDT(dgvHaunch);
                        Chart.Haunch(Aspan, DThaunch, chartHaunch);

                        //Add haunch to database Node
                        Haunch Haunch = new Haunch(Aspan, DThaunch);
                        Node = Matrix.Addnode(Node, Haunch.Harray, "Haunch", 4, "btop");

                        //Save to DTCbox again
                        
                        DTCBox = DGV.GridtoDT(dgvCBox);
                        Node = Matrix.Addnode(Node, Haunch.Closedbox(Aspan, DTCBox), "ntop", 4, "Haunch");
                        

                        //Bottom concrete
                        Acon_grid = DGV.GridtoArray(gridBCon);
                        Acon = Matrix.Update_con(Acon, Acon_grid);
                        Acon1 = Haunch.Bottomcon(Aspan, Acon);
                        Node = Matrix.Addnode(Node, Acon1, "Hc", 4, "Haunch,ntop");
                        Access.writeList(Node, "Node", con, "All");

                        //Atop has 3 rows
                        //1st is length, seperate by closed box section at pier
                        //2nd is with of flange
                        //3rd is thickness of top flange
                        Atop = Matrix.Atop_CBox(DTCBox, Aspan, 3);

                        //Fill to gridTop DGV
                        DGV.ArraytoGrid(gridTop, Atop);

                        //Create Atop_grid with 5 row
                        //1st: type: All number, if add more => number 4 => this help when modify length
                        //2nd: order : 1 2 3 => this help mark by color and lock the closed - box section
                        Atop_grid = new double[5, Atop.GetLength(1)];
                        Atop1 = new double[Atop.GetLength(1)];
                        for (int i = 0; i < Atop.GetLength(1); i++)
                        {
                            Atop_grid[0, i] = 1; //Set all ID = 1
                            Atop_grid[1, i] = i + 1; //Set order 1-2-3
                            Atop_grid[2, i] = Atop[0, i]; //Set length
                            Atop1[i] = Atop[0, i];
                        }
                        Deco(gridTop, Atop_grid);

                        //Create Atrib and Atrib_grid
                        Atrib = Matrix.Atop_CBox(DTCBox, Aspan, 4);

                        //Fill to gridTrib dgv
                        DGV.ArraytoGrid(gridTrib, Atrib);

                        //Create Atrib_grid with 6 row
                        Atrib_grid = new double[6, Atrib.GetLength(1)];
                        for (int i = 0; i < Atrib.GetLength(1); i++)
                        {
                            Atrib_grid[0, i] = 1; //Set all ID = 1
                            Atrib_grid[1, i] = i + 1; //Set order 1-2-3
                            Atrib_grid[2, i] = Atrib[0, i]; //Set length

                        }
                        Deco(gridTrib, Atrib_grid);


                    }
                    break;

                case "pageDim":
                    {
                        //Select node without type 5 again, this is need to reset the Type 4 - section when hit Apply again
                        Node = Node.Where(p => p.Type <5 ).ToList();

                        Atop = DGV.GridtoArray(gridTop);
                        Node = Matrix.Addnode(Node, Atop, "btop,ttop", 5, "Haunch,ntop,Hc");

                        Abot = DGV.GridtoArray(gridBot);
                        Node = Matrix.Addnode(Node, Abot, "tbot", 5, "Haunch,ntop,Hc,btop,ttop");

                        Aweb = DGV.GridtoArray(gridWeb);
                        Node = Matrix.Addnode(Node, Aweb, "tw", 5, "Haunch,ntop,Hc,btop,ttop,tbot");

                        double [] Asec = new double[14];
                       
                        Asec[0] = (double)numts.Value;
                        Asec[1] = (double)numbh.Value;
                        Asec[2] = (double)numth.Value;
                        Asec[3] = (double)numdrt.Value;
                        Asec[4] = (double)numart.Value;
                        Asec[5] = (double)numcrt.Value;
                        Asec[6] = (double)numdrb.Value;
                        Asec[7] = (double)numarb.Value;
                        Asec[8] = (double)numcrb.Value;
                        Asec[9] = radioRa.Checked == Enabled ? Convert.ToDouble(numSr.Value) : Math.Tan(Convert.ToDouble(numSd.Value) * Math.PI / 180.0);
                        Asec[10] = (double)numw.Value;
                        Asec[11] = (double)numD.Value;
                        Asec[12] = (double)numcbot.Value;
                        Asec[13] = (double)numctop.Value;
                        Node = Matrix.Add1prop(Node, Asec, "ts,th,bh,drt,art,crt,drb,arb,crb,S,w,D,cbot,ctop");

                        Access.writeList(Node, "Node", con, "All");

                    }
                    break;


                case "pageStiffeners":
                    {
                        //Select node without type 5 again

                        Node = Node.Where(p => p.Type < 6).ToList();

                        Aribtop = DGV.GridtoArray(gridTrib);
                        Node = Matrix.Addnode(Node, Aribtop, "nst,Hst,tst", 6, "Haunch,ntop,Hc,btop,ttop,tbot,ts,th,bh,drt,art,crt,drb,arb,crb,S,w,D,cbot,ctop");

                        Aribbot = DGV.GridtoArray(gridBrib);
                        Node = Matrix.Addnode(Node, Aribbot, "nsb,Hsb,tsb", 6, "Haunch,ntop,Hc,btop,ttop,tbot,ts,th,bh,drt,art,crt,drb,arb,crb,S,w,D,cbot,ctop,nst,Hst,tst");

                        Atranstif = DGV.GridtoArray(gridTranstif);
                        Node = Matrix.Addd0(Node, Atranstif, "d0");

                        double[] Ans = new double[1] { (double)numns.Value };
                        Node = Matrix.Add1prop(Node, Ans, "ns");

                        //Write to DB
                        Access.writeList(Node, "Node", con, "All");

                        //Fill to dgvKframe
                        Akframe = Matrix.Arrcumulate(Atranstif);

                        
                        List<Node> Node123 = Node.Where(p => (p.Type == 1 || p.Type == 2 || p.Type == 3) && p.BeamID == 1).ToList();
                        for (int i = 0; i < Node123.Count; i++)
                            KFrame.Add(new KFrame(Node123[i].X / 1000, false, Node123[i].Label));
                        for (int i = 0; i < Akframe.GetLength(1); i++)
                            if (Node123.Select(p => p.X).ToList().IndexOf(Akframe[0, i]) == -1)
                                KFrame.Add(new KFrame(Akframe[0, i] / 1000, false, ""));
                        KFrame = KFrame.OrderBy(p => p.Station).ToList();
                        gridKframe.DataSource = KFrame;

                        //Deco the gridKframe
                        for (int i = 0; i < KFrame.Count; i++)
                        {
                            if (KFrame[i].Description == "Exterior Support" || KFrame[i].Description == "Interior Support")
                            {
                                gridKframe.Rows[i].DefaultCellStyle.BackColor = Color.Beige;
                                gridKframe.Rows[i].Cells["Location"].ReadOnly = false;
                            }                        
                                
                        }

                        
                    }
                    break;

               

                case "pageOther":
                    {
                        //Write crossbeam to DB
                        List<Crossbeam> Crossbeam = new List<Crossbeam>();
                        for (int i = 0; i < gridCrossbeam.RowCount; i++)
                            Crossbeam.Add(new Crossbeam(Convert.ToDouble(gridCrossbeam.Rows[i].Cells[0].Value), Convert.ToDouble(gridCrossbeam.Rows[i].Cells[1].Value), Convert.ToDouble(gridCrossbeam.Rows[i].Cells[2].Value),
                                Convert.ToDouble(gridCrossbeam.Rows[i].Cells[3].Value), Convert.ToDouble(gridCrossbeam.Rows[i].Cells[4].Value), Convert.ToDouble(gridCrossbeam.Rows[i].Cells[5].Value),
                                Convert.ToDouble(gridCrossbeam.Rows[i].Cells[6].Value)));
                        Access.writeList(Crossbeam, "Crossbeam", con, "All");


                        //Deco the gridKframe
                        for (int i = 0; i < KFrame.Count; i++)
                        {
                            if (Convert.ToBoolean(gridKframe.Rows[i].Cells[1].Value) && KFrame[i].Description == "")                            
                                KFrame[i].Location = true; 
                            else                            
                                KFrame[i].Location = false;
                               
                                                          
                        }

                        Node = Node.Where(p => p.Type < 7).ToList();
                        Node = Matrix.AddKframe(Node, KFrame);
                        Access.writeList(Node, "Node", con, "All");

                       
                        //MessageBox.Show(K.Count.ToString());

                        
                    }
                    break;


                case "pageAnalysis":
                    {
                        //Do for Node
                        List<int> kindex = new List<int>();
                        
                        if (checkSChanged.Checked == false)
                        {
                            if (kindex.IndexOf(5) == -1 && kindex.IndexOf(6) == -1)
                                kindex.AddRange(new List<int> { 5, 6 });
                        }
                        else
                        {
                            if (kindex.IndexOf(5) != -1 && kindex.IndexOf(6) != -1)
                            {
                                kindex.Remove(5);
                                kindex.Remove(6);
                            }                               
                        }

                        if (checkKframe.Checked == false)
                        {
                            if (kindex.IndexOf(7) == -1)
                                kindex.Add(7);
                        }
                        else
                        {
                            if (kindex.IndexOf(7) != -1)                            
                                kindex.Remove(7); 
                        }

                        Node = Node.Where(p => p.Type < 8).ToList();
                        Access.writeList(Node, "Node", con, "All");

                        List<Node> Node1 = new List<Node>();
                        Node1 = Node;
                        for (int i = 0; i < kindex.Count; i++)
                            Node1 = Node1.Where(p => p.Type != kindex[i]).ToList();
                        List<Node> Node2 = new List<Node>();
                        Node2 = Matrix.Selection(Node1, (double)numseg1.Value, (double)numseg2.Value);
                        Access.writeList(Node2, "Node2", con, "All");

                    }
                    break;


            }
        }


        public void Deco(DataGridView dgv, double[,] arr)
        {
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (arr[1, i] == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(146, 205, 220);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(146, 205, 220);
                }
                else if (arr[1, i] % 2 == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                }

                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;

                }
            }

            if (dgv == gridTop)
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    if (arr[1, i] % 2 == 0)
                    {
                        dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);
                        dgv.Rows[1].Cells[i].ReadOnly = true;
                        dgv.Rows[1].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }

                    else
                    {
                        dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;

                    }

                    if (arr[0, i] == 1)
                    {
                        dgv.Rows[0].Cells[i].ReadOnly = true;
                        dgv.Rows[0].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    }

                }
            }

            else if (dgv == gridTrib)
            {

                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dgv.Columns[i].ReadOnly = false;
                    dgv.Columns[i].DefaultCellStyle.ForeColor = DefaultForeColor;

                    if (arr[1, i] % 2 == 0)
                    {
                        dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;

                        if (arr[0, i] == 1)
                        {
                            dgv.Rows[0].Cells[i].ReadOnly = true;
                            dgv.Rows[0].Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.Red };

                        }


                    }

                    else
                    {
                        dgv.Columns[i].ReadOnly = true;
                        dgv.Columns[i].DefaultCellStyle.ForeColor = Color.Red;

                        dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);
                        dgv.EnableHeadersVisualStyles = false;
                        dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(217, 217, 217);

                    }

                }
            }

        }

        int index;
        DataGridView SelectedDGV;
        private void grid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if ((e.Button != MouseButtons.Right) || !(sender is DataGridView a) || e.ColumnIndex == -1)
                return;

            contextMenuStrip1.Show(Cursor.Position);            
                index = e.ColumnIndex;

            if (a == gridBCon)
            {
                divideTool.Enabled = false;
                addTool.Enabled = true;

                if (Acon[Acon.GetLength(0) - 1, index] == 1 || Acon[Acon.GetLength(0) - 1, index] == 2)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;

            }
            else if (a == gridCross)
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (Across_grid[0, index] == 1 || Across_grid[0, index] == 2)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }
            else if (a == gridTranstif)
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (Atranstif_grid[0, index] == 1 || Atranstif_grid[0, index] == 2 || Atranstif_grid[0, index] == 3)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }
            else if (a == gridTop)
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (Atop_grid[0, index] == 1)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }

            else if (a == gridTran)
            {
                divideTool.Enabled = false;
                if (Atran[2, index] < 10)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;

                if (Atran[2, index] == 0)
                    addTool.Enabled = false;
                else
                    addTool.Enabled = true;
            }
            else if (a == gridSection)
            {
                divideTool.Enabled = false;
                if (index == a.ColumnCount - 1)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }

            else if (a == gridTrib)
            {

                deleteTool.Enabled = true;
                addTool.Enabled = true;
                divideTool.Enabled = true;

                if (Atrib_grid[1, index] % 2 == 1)
                {
                    deleteTool.Enabled = false;
                    addTool.Enabled = false;
                    divideTool.Enabled = false;
                }
                else
                {
                    if (Atrib_grid[0, index] == 1)
                        deleteTool.Enabled = false;
                    else
                    {
                        deleteTool.Enabled = true;
                        addTool.Enabled = true;
                        divideTool.Enabled = true;
                    }
                }

            }

            else
            {
                divideTool.Enabled = true;
                addTool.Enabled = true;
                if (index == a.ColumnCount - 1)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }

            SelectedDGV = a;
        }



        private void addTool_Click(object sender, EventArgs e)
        {
            ndiv = 2;
            if (SelectedDGV == gridBCon)
            {
                Acon = Matrix.Seperate_con(Acon, index);
                DGV.Acontogrid(gridBCon, Acon);
            }
            
            
            else if (SelectedDGV == gridTop)
            {
                Atop = Matrix.Seperate_top(Atop, index, ndiv);
                Atop_grid = Matrix.Seperate_top(Atop_grid, index, ndiv);
                DGV.ArraytoGrid(gridTop, Atop);
                Deco(gridTop, Atop_grid);
            }

            else if (SelectedDGV == gridBot)
            {
                Abot = Matrix.Seperate(Abot, index, ndiv);
                DGV.ArraytoGrid(gridBot, Abot);
            }
            else if (SelectedDGV == gridWeb)
            {
                Aweb = Matrix.Seperate(Aweb, index, ndiv);
                DGV.ArraytoGrid(gridWeb, Aweb);
            }

            else if (SelectedDGV == gridCross)
            {

                Across = Matrix.Seperate_cross(Across, index, ndiv);
                Across_grid = Matrix.Seperate_cross(Across_grid, index, ndiv);
                DGV.ArraytoGrid(gridCross, Across);
                Deco(SelectedDGV, Across_grid);
            }
            else if (SelectedDGV == gridTranstif)
            {
                Atranstif = Matrix.Seperate_transtif(Atranstif, index, ndiv);
                Atranstif_grid = Matrix.Seperate_transtif(Atranstif_grid, index, ndiv);
                DGV.ArraytoGrid(gridTranstif, Atranstif);
                Deco(SelectedDGV, Atranstif_grid);
            }
            else if (SelectedDGV == gridTran)
            {
                Atran = Matrix.Seperate_tran(Atran, index);
                DGV.ArraytoGrid_tran(gridTran, Atran);
                Deco(SelectedDGV, Atran);

            }
            else if (SelectedDGV == gridTrib)
            {
                Atrib = Matrix.Seperate_top(Atrib, index, ndiv);
                Atrib_grid = Matrix.Seperate_top(Atrib_grid, index, ndiv);
                DGV.ArraytoGrid(gridTrib, Atrib);
                Deco(gridTrib, Atrib_grid);


            }
            else if (SelectedDGV == gridBrib)
            {
                Aribbot = Matrix.Seperate(Aribbot, index, ndiv);
                DGV.ArraytoGrid(gridBrib, Aribbot);
            }
           

            else if (SelectedDGV == gridSection)
            {
                Asection = Matrix.Seperate(Asection, index, ndiv);
                Asectiong = (double[,])Asection.Clone();
                fillAsection();
            }

            if (SelectedDGV.Columns.Count > 10)
                foreach (DataGridViewColumn c in SelectedDGV.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    c.MinimumWidth = 80;
                    SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32 + 19;
                }
            SelectedDGV.ClearSelection();
        }

        private void grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;

            if (sender == gridBCon)
            {
                //Acon_grid = DGV.GridtoArray(gridBCon);
                //Acon = Matrix.Update_con(Acon, Acon_grid);
                //DGV.Acontogrid(gridBCon, Acon);
            }

            else if (sender == gridBot)
            {
                Abot = DGV.GridtoArray(gridBot);
                Abot = Matrix.Update(Abot, sumspan);
                DGV.ArraytoGrid(gridBot, Abot);
                gridBot.MultiSelect = false;
            }
            else if (sender == gridWeb)
            {
                Aweb = DGV.GridtoArray(gridWeb);
                Aweb = Matrix.Update(Aweb, sumspan);
                DGV.ArraytoGrid(gridWeb, Aweb);
                gridWeb.MultiSelect = false;
            }

            else if (sender == gridCross)
            {
                Across = DGV.GridtoArray(gridCross);
                for (int i = 0; i < Across.GetLength(1); i++)
                    Across_grid[2, i] = Across[0, i];
                Across_grid = Matrix.Update_cross(Across_grid, Aspan);
                for (int i = 0; i < Across.GetLength(1); i++)
                    Across[0, i] = Across_grid[2, i];
                DGV.ArraytoGrid(gridCross, Across);
                gridCross.MultiSelect = false;
            }
            else if (sender == gridTop)
            {
                Atop = DGV.GridtoArray(gridTop);
                for (int i = 0; i < Atop.GetLength(1); i++)
                    for (int j = 0; j < Atop.GetLength(0); j++)
                        Atop_grid[j + 2, i] = Atop[j, i];

                Atop_grid = Matrix.Update_transtif(Atop_grid, Atop1); //??ok
                for (int i = 0; i < Atop.GetLength(1); i++)
                    for (int j = 0; j < Atop.GetLength(0); j++)
                        Atop[j, i] = Atop_grid[j + 2, i];

                DGV.ArraytoGrid(gridTop, Atop);
                gridTranstif.MultiSelect = false;
            }
            else if (sender == gridTranstif)
            {
                Atranstif = DGV.GridtoArray(gridTranstif);
                for (int i = 0; i < Atranstif.GetLength(1); i++)
                    Atranstif_grid[2, i] = Atranstif[0, i];

                Atranstif_grid = Matrix.Update_transtif(Atranstif_grid, Across1); //??ok
                for (int i = 0; i < Atranstif.GetLength(1); i++)
                    Atranstif[0, i] = Atranstif_grid[2, i];
                DGV.ArraytoGrid(gridTranstif, Atranstif);
                gridTranstif.MultiSelect = false;
            }
            else if (sender == gridTran)
            {
                Atran = DGV.GridtoArray_tran(gridTran, Atran);

                // Change component of section
                sumsec = 0; //Width of bridge
                //Sum of width
                for (int i = 0; i < Atran.GetLength(1); i++)
                {
                    sumsec += Atran[0, i];
                }
                ////Update to gridSection


                if (sumsec <= 500)
                    Asection[0, 0] = sumsec;
                else if (sumsec <= 1000)
                {
                    Asection[0, 0] = 500;
                    Asection[0, Asection.GetLength(1) - 1] = sumsec - 500;
                }
                else
                {
                    Asection[0, 0] = 500;
                    Asection[0, Asection.GetLength(1) - 1] = 500;
                    for (int i = 1; i < Asection.GetLength(1) - 1; i++)
                        Asection[0, i] = (sumsec - 1000) / (Asection.GetLength(1) - 2);
                }

                for (int i = 0; i < Asection.GetLength(1); i++)
                    Asectiong[0, i] = Asection[0, i];

                fillAsection();
            }

            else if (sender == gridSection)
            {
                if (e.RowIndex == 0)
                {
                    Asectiong = DGV.GridtoArray_sec(gridSection);
                    Asectiong = Matrix.Update(Asectiong, sumsec);
                    for (int i = 0; i < Asectiong.GetLength(1); i++)
                        Asection[0, i] = Asectiong[0, i];
                    fillAsection();
                }
                else
                {
                    for (int i = 0; i < Asection.GetLength(1); i++)
                    {
                        if (gridSection.Rows[1].Cells[i].Value.ToString().Contains("Barrier"))
                            Asectiong[1, i] = 1;
                        else if (gridSection.Rows[1].Cells[i].Value.ToString().Contains("Liveload"))
                            Asectiong[1, i] = 2;
                        else
                            Asectiong[1, i] = 3;

                    }
                }


                //fillAsection();

            }
            else if (sender == gridTrib)
            {
                Atrib = DGV.GridtoArray(gridTrib);
                for (int i = 0; i < Atrib.GetLength(1); i++)
                    for (int j = 0; j < Atrib.GetLength(0); j++)
                        Atrib_grid[j + 2, i] = Atrib[j, i];

                Atrib_grid = Matrix.Update_transtif(Atrib_grid, Atop1); //??ok
                for (int i = 0; i < Atrib.GetLength(1); i++)
                    for (int j = 0; j < Atrib.GetLength(0); j++)
                        Atrib[j, i] = Atrib_grid[j + 2, i];

                DGV.ArraytoGrid(gridTrib, Atrib);
                Deco(gridTrib, Atrib_grid);
            }
            else if (sender == gridBrib)
            {
                Aribbot = DGV.GridtoArray(gridBrib);
                Aribbot = Matrix.Update(Aribbot, sumspan);
                DGV.ArraytoGrid(gridBrib, Aribbot);
                gridBrib.MultiSelect = false;
            }
            
            //gridTop.Rows[row+1].Cells[col].Selected = true;
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {

            if (SelectedDGV == gridBCon)
            {
                Acon = Matrix.Combine_con(Acon, index);
                DGV.Acontogrid(gridBCon, Acon);
            }
            
            else if (SelectedDGV == gridTop)
            {
                Atop = Matrix.Combine_top(Atop, index);
                Atop_grid = Matrix.Combine_top(Atop_grid, index);
                DGV.ArraytoGrid(gridTop, Atop);
                Deco(SelectedDGV, Atop_grid);

            }
            else if (SelectedDGV == gridBot)
            {
                Abot = Matrix.Combine(Abot, index);
                DGV.ArraytoGrid(gridBot, Abot);

            }
            else if (SelectedDGV == gridWeb)
            {
                Aweb = Matrix.Combine(Aweb, index);
                DGV.ArraytoGrid(gridWeb, Aweb);

            }

            else if (SelectedDGV == gridCross)
            {
                Across = Matrix.Combine_cross(Across, index);
                Across_grid = Matrix.Combine_cross(Across_grid, index);
                DGV.ArraytoGrid(gridCross, Across);
                Deco(SelectedDGV, Across_grid);
            }

            else if (SelectedDGV == gridTranstif)
            {
                Atranstif = Matrix.Combine_cross(Atranstif, index);
                Atranstif_grid = Matrix.Combine_cross(Atranstif_grid, index);
                DGV.ArraytoGrid(gridTranstif, Atranstif);
                Deco(SelectedDGV, Atranstif_grid);
            }
            else if (SelectedDGV == gridTran)
            {
                Atran = Matrix.Combine_tran(Atran, index);
                DGV.ArraytoGrid_tran(gridTran, Atran);
                Deco(SelectedDGV, Atran);
            }
            else if (SelectedDGV == gridTrib)
            {
                Atrib = Matrix.Combine_top(Atrib, index);
                Atrib_grid = Matrix.Combine_top(Atrib_grid, index);
                DGV.ArraytoGrid(gridTrib, Atrib);
                Deco(SelectedDGV, Atrib_grid);


            }
            else if (SelectedDGV == gridBrib)
            {
                Aribbot = Matrix.Combine(Aribbot, index);
                DGV.ArraytoGrid(gridBrib, Aribbot);


            }
            
            else if (SelectedDGV == gridSection)
            {
                Asection = Matrix.Combine(Asection, index);
                Asectiong = (double[,])Asection.Clone();
                fillAsection();
            }

            if (SelectedDGV.Columns.Count <= 10)
                foreach (DataGridViewColumn c in SelectedDGV.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32;
                }
            SelectedDGV.ClearSelection();



        }

        int ndiv;
        private void divideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAddmore f = new fAddmore();
            if (f.ShowDialog() == DialogResult.OK)
            {
                ndiv = f.ndiv;
                if (SelectedDGV == gridTop)
                {
                    Atop = Matrix.Seperate_top(Atop, index, ndiv);
                    Atop_grid = Matrix.Seperate_top(Atop_grid, index, ndiv);
                    DGV.ArraytoGrid(gridTop, Atop);
                    Deco(gridTop, Atop_grid);
                }

                else if (SelectedDGV == gridBot)
                {
                    Abot = Matrix.Seperate(Abot, index, ndiv);
                    DGV.ArraytoGrid(gridBot, Abot);
                }
                else if (SelectedDGV == gridWeb)
                {
                    Aweb = Matrix.Seperate(Aweb, index, ndiv);
                    DGV.ArraytoGrid(gridWeb, Aweb);
                }

                else if (SelectedDGV == gridCross)
                {
                    Across = Matrix.Seperate_cross(Across, index, ndiv);
                    Across_grid = Matrix.Seperate_cross(Across_grid, index, ndiv);
                    DGV.ArraytoGrid(gridCross, Across);
                    Deco(SelectedDGV, Across_grid);
                }
                else if (SelectedDGV == gridTranstif)
                {
                    Atranstif = Matrix.Seperate_transtif(Atranstif, index, ndiv);
                    Atranstif_grid = Matrix.Seperate_transtif(Atranstif_grid, index, ndiv);
                    DGV.ArraytoGrid(gridTranstif, Atranstif);
                    Deco(SelectedDGV, Atranstif_grid);
                }
                else if (SelectedDGV == gridTran)
                {
                    Atran = Matrix.Seperate_tran(Atran, index);
                    DGV.ArraytoGrid_tran(gridTran, Atran);
                    Deco(SelectedDGV, Atran);

                }
                else if (SelectedDGV == gridTrib)
                {
                    Atrib = Matrix.Seperate_top(Atrib, index, ndiv);
                    Atrib_grid = Matrix.Seperate_top(Atrib_grid, index, ndiv);
                    DGV.ArraytoGrid(gridTrib, Atrib);
                    Deco(gridTrib, Atrib_grid);

                }
                else if (SelectedDGV == gridBrib)
                {
                    Aribbot = Matrix.Seperate(Aribbot, index, ndiv);
                    DGV.ArraytoGrid(gridBrib, Aribbot);
                }
               

                if (SelectedDGV.Columns.Count > 10)
                    foreach (DataGridViewColumn c in SelectedDGV.Columns)
                    {
                        c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        c.MinimumWidth = 80;
                        SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32 + 19;
                    }
                SelectedDGV.ClearSelection();



            }
        }

        string Scheck;
        private void gridchart_DataClick(object sender, ChartPoint chartPoint)
        {
            var asPixels = gridchart.Base.ConvertToPixels(chartPoint.AsPoint());
            fRestrain f = new fRestrain();
            f.Location = new Point(Cursor.Position.X - f.Size.Width / 2, Cursor.Position.Y - f.Size.Height);
            if (f.ShowDialog() == DialogResult.OK)
            {
                Scheck = f.Scheck;
                if (Scheck != "" && Scheck != null)
                    Node.Where(p => p.X == chartPoint.X && p.Y == chartPoint.Y).ToList().ForEach(i => i.Restrain = Scheck);

                Chart.Bridgegrid(Node, gridchart);
                // MessageBox.Show(Scheck);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMattype.SelectedIndex == 0)
            {
                groupSteel1.Visible = false;
                groupSteel2.Visible = false;
                groupSteel3.Visible = false;
                checkSteel.Enabled = false;
                comboSteel.Enabled = false;
                groupConcrete.Visible = true;
                checkSteel.Checked = false;

            }

            else if (cbMattype.SelectedIndex == 1)
            {
                groupSteel1.Visible = true;
                groupSteel2.Visible = true;
                groupSteel3.Visible = false;
                groupConcrete.Visible = false;
                checkSteel.Enabled = true;

            }
            else
            {
                groupSteel1.Visible = false;
                groupSteel2.Visible = false;
                groupSteel3.Visible = false;
                checkSteel.Enabled = false;
                comboSteel.Enabled = false;
                groupConcrete.Visible = false;
                checkSteel.Checked = false;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                double fcm = Convert.ToDouble(numFc.Value <= 40 ? (numFc.Value + 4.00M) : (numFc.Value >= 60 ? (numFc.Value + 6.00M) : (numFc.Value * 1.1M)));
                numEc.Value = Convert.ToDecimal(0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcm, (1 / 3.0)));

            }
        }


        private void checkSteel_CheckedChanged(object sender, EventArgs e)
        {
            if (checkSteel.Checked == true)
            {
                comboSteel.Enabled = true;

                groupSteel2.Visible = false;
                groupSteel3.Visible = true;
                BindingSource bs = new BindingSource();
                bs.DataSource = new List<string> { "SS235", "SS275", "SM275", "SM355", "SM420", "SM460", "HSB380", "HSB460", "HSB690" };
                comboSteel.DataSource = bs;
            }

            else
            {
                comboSteel.Enabled = false;

                groupSteel2.Visible = true;
                groupSteel3.Visible = false;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Mat Mat1 = new Mat();
            Mat1.Name = txtMatname.Text;
            Mat1.Type = cbMattype.Text;
            Mat1.Ws = Convert.ToDouble(numWs.Value);
            Mat1.Es = Convert.ToDouble(numEs.Value);
            Mat1.G = Convert.ToDouble(numG.Value);
            Mat1.Fy = Convert.ToDouble(numFy.Value);
            Mat1.Fu = Convert.ToDouble(numFu.Value);
            Mat1.Wc = Convert.ToDouble(numWc.Value);
            Mat1.fc = Convert.ToDouble(numFc.Value);
            Mat1.Ec = Convert.ToDouble(numEc.Value);

            try
            {
                Access.writemat(Mat1, "Mat", con);
            }
            catch
            {
                MessageBox.Show("Error: Duplicate Name");
            }

            DataTable DTMat = Access.getDataTable("Select Name from Mat", con);

            listBox1.Items.Clear();
            for (int i = 0; i < DTMat.Rows.Count; i++)
            {
                listBox1.Items.Add(DTMat.Rows[i][0].ToString());
            }

            dgvMat.Invalidate();
            dgvMat.Refresh();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMatname.Text = listBox1.SelectedItem.ToString();
            DataTable DTMat = Access.getDataTable("Select * from Mat", con);
            List<Mat> Listmat = new List<Mat>();
            Listmat = (from DataRow dr in DTMat.Rows
                       select new Mat()
                       {
                           Name = dr["Name"].ToString(),
                           Type = dr["Type"].ToString(),
                           Ws = Convert.ToDouble(dr["Ws"]),
                           Es = Convert.ToDouble(dr["Es"]),
                           G = Convert.ToDouble(dr["G"]),
                           Fy = Convert.ToDouble(dr["Fy"]),
                           Fu = Convert.ToDouble(dr["Fu"]),
                           Wc = Convert.ToDouble(dr["Wc"]),
                           fc = Convert.ToDouble(dr["fc"]),
                           Ec = Convert.ToDouble(dr["Ec"])
                       }).ToList();

            string a = Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Type).FirstOrDefault();
            cbMattype.SelectedIndex = a == "Concrete" ? 0 : 1;
            numWs.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Ws).FirstOrDefault());
            numEs.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Es).FirstOrDefault());
            numG.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.G).FirstOrDefault());
            numFy.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Fy).FirstOrDefault());
            numFu.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Fu).FirstOrDefault());
            numWc.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Wc).FirstOrDefault());
            numFc.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.fc).FirstOrDefault());
            numEc.Value = Convert.ToDecimal(Listmat.Where(p => p.Name == listBox1.SelectedItem.ToString()).Select(p => p.Ec).FirstOrDefault());

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Access.delmat(listBox1.SelectedItem.ToString(), con);
                DataTable DTMat = Access.getDataTable("Select Name from Mat", con);
                listBox1.Items.Clear();
                for (int i = 0; i < DTMat.Rows.Count; i++)
                {
                    listBox1.Items.Add(DTMat.Rows[i][0].ToString());
                }
            }
            try
            {
                dgvMat.Invalidate();
                dgvMat.Refresh();
            }
            catch
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mat Mat1 = new Mat();
            Mat1.Name = txtMatname.Text;
            Mat1.Type = cbMattype.Text;
            Mat1.Ws = Convert.ToDouble(numWs.Value);
            Mat1.Es = Convert.ToDouble(numEs.Value);
            Mat1.G = Convert.ToDouble(numG.Value);
            Mat1.Fy = Convert.ToDouble(numFy.Value);
            Mat1.Fu = Convert.ToDouble(numFu.Value);
            Mat1.Wc = Convert.ToDouble(numWc.Value);
            Mat1.fc = Convert.ToDouble(numFc.Value);
            Mat1.Ec = Convert.ToDouble(numEc.Value);
            Access.upadatemat(Mat1, con);
        }

        private void numFc_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                double fcm = Convert.ToDouble(numFc.Value <= 40 ? (numFc.Value + 4.00M) : (numFc.Value >= 60 ? (numFc.Value + 6.00M) : (numFc.Value * 1.1M)));
                numEc.Value = Convert.ToDecimal(0.077 * Math.Pow(2500, 1.5) * Math.Pow(fcm, (1 / 3.0)));

            }
        }



        // One click to select combobox

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender == gridSection)
                this.gridSection.MouseDown += this.HandleDataGridViewMouseDown;
            else if (sender == dgvMat)
            {
                try
                {
                    this.dgvMat.MouseDown += this.HandleDataGridViewMouseDown;
                }
                catch
                {

                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == false)
                cbLLiveload.Enabled = false;
            else
                cbLLiveload.Enabled = true;
        }

        private void cbLLiveload_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLLiveload.SelectedIndex == 0)
            {
                //Define KL510;
                DataTable DTTruck = new DataTable();
                DTTruck.Columns.Add("Coor");
                DTTruck.Columns.Add("Aload");
                DTTruck.Rows.Add(0, 48);
                DTTruck.Rows.Add(3.6, 135);
                DTTruck.Rows.Add(4.8, 135);
                DTTruck.Rows.Add(12, 192);

                dgvTruck.DataSource = DTTruck;

                numLane.Value = 12.7M;

            }
        }

       

        private void dgvHaunch_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 1; i < pier; i++)
                dgvHaunch.Rows[i].Cells[3].Value = dgvHaunch.Rows[i - 1].Cells[5].Value;
        }



        private void HandleDataGridViewMouseDown(object sender, MouseEventArgs e)
        {
            // See where the click is occurring
            if (sender == gridSection)
            {
                DataGridView.HitTestInfo info = this.gridSection.HitTest(e.X, e.Y);

                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    switch (info.RowIndex)
                    {
                        case 1:
                            this.gridSection.CurrentCell =
                                this.gridSection.Rows[info.RowIndex].Cells[info.ColumnIndex];
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (sender == dgvMat)
            {
                DataGridView.HitTestInfo info = this.dgvMat.HitTest(e.X, e.Y);

                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    switch (info.ColumnIndex)
                    {
                        // Add and remove case statements as necessary depending on
                        // which columns have ComboBoxes in them.

                        case 1: // Column index 1
                        case 2: // Column index 2
                            this.dgvMat.CurrentCell =
                                this.dgvMat.Rows[info.RowIndex].Cells[info.ColumnIndex];
                            break;
                        default:
                            break;
                    }
                }

            }


        }


    }


}
