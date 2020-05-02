﻿using LiveCharts;
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


namespace Mainform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Change color
            Setinitialize();


            //MessageBox.Show(a.GetLength(0).ToString());


            //MessageBox.Show(b[1, 1].ToString());
        }


        //Change background color
        private void Setinitialize()
        {
            flowLayoutPanel1.BackColor = Color.FromArgb(33, 115, 70);
            panel1.BackColor = Color.FromArgb(33, 115, 70);
            btGeneral.BackColor = Color.FromArgb(33, 115, 70);
            btBridge.BackColor = Color.FromArgb(33, 115, 70);
            button3.BackColor = Color.FromArgb(44, 152, 93);
            button4.BackColor = Color.FromArgb(44, 152, 93);
            button5.BackColor = Color.FromArgb(44, 152, 93);
            button6.BackColor = Color.FromArgb(44, 152, 93);
            button7.BackColor = Color.FromArgb(44, 152, 93);
            button8.BackColor = Color.FromArgb(44, 152, 93);
            button9.BackColor = Color.FromArgb(44, 152, 93);
            btBack.BackColor = Color.FromArgb(33, 115, 70);
            btApply.BackColor = Color.FromArgb(33, 115, 70);
            btNext.BackColor = Color.FromArgb(33, 115, 70);
            btAnalysis.BackColor = Color.FromArgb(33, 115, 70);

            gridAB.RowCount = 1;
            gridAB.Rows[0].Height = 30;
            cbType.SelectedIndex = 0;
            labelEx.Text = "Ex. For a bridges with 3 spans, the lengths are 30m, 40m, 30m, and distance from end-beam to";
            labelEx2.Text = "support point is 0.5m, input string should be: 0.5+30+40+30+0.5";

            labelCode1.Text = "(1) 도로교 설계기준 (한계상태설계법) 해설, (2015) - (사)한국교량및구조공화회∙교량설계핵심기술연구단";
            labelCode2.Text = "(2) 강구조설계기준 및 해설 (하중저항계수법), (2018) - (사)한국강구조학회";
            labelCode3.Text = "(3) AASHTO LRFD, (2017)";


            metroTabControl1.SelectedTab = pageGeneral;

            Setgridview(gridTop);
            Setgridview(gridBot);
            Setgridview(gridWeb);
            Setgridview(gridCon);

            pictureBox1.Load(@"C:\Home2\Picture\Section.PNG");
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


            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);

        }

        private void btGeneral_Click(object sender, EventArgs e)
        {

            TabPage[] a = { pageGeneral };
            showtabpage(a);


        }

        private void button1_Click(object sender, EventArgs e)
        {

            TabPage[] a = { pageAnalysis };
            showtabpage(a);



        }


        void showtabpage(TabPage[] a)
        {
            metroTabControl1.TabPages.Clear();
            foreach (TabPage page in metroTabControl1.TabPages)
                metroTabControl1.TabPages.Remove(page);
            foreach (TabPage a1 in a)
                metroTabControl1.TabPages.Add(a1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSteel;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageDeck;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageStiffeners;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageCross;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSectional;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageSupports;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
            showtabpage(a);
            metroTabControl1.SelectedTab = pageProp;
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

        private void gridAB_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (gridAB.CurrentCell.ColumnIndex == 0 || gridAB.CurrentCell.ColumnIndex == 1 || gridAB.CurrentCell.ColumnIndex == 2 || gridAB.CurrentCell.ColumnIndex == 3 || gridAB.CurrentCell.ColumnIndex == 4 || gridAB.CurrentCell.ColumnIndex == 5 || gridAB.CurrentCell.ColumnIndex == 6) //Desired Column
            {

                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                }
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

        private void btNext_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {
                        TabPage[] a = { pageCross };
                        showtabpage(a);

                    }
                    break;


                case "pageCross":
                    {
                        TabPage[] a = { pageSteel, pageDeck, pageStiffeners, pageCross, pageSectional, pageSupports, pageProp };
                        showtabpage(a);
                        metroTabControl1.SelectedTab = pageSteel;
                    }
                    break;



                case "pageSteel":
                    {
                        metroTabControl1.SelectedTab = pageDeck;


                    }
                    break;

            }


        }

        private void btBack_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageCross":
                    {
                        TabPage[] a = { pageGeneral };
                        showtabpage(a);
                    }
                    break;
                case "pageSteel":
                    {
                        TabPage[] a = { pageCross };
                        showtabpage(a);
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


        double[,] Atop = new double[3, 1];
        double[,] Abot = new double[3, 1];
        double[,] Aweb = new double[3, 1];
        double[,] Acon = new double[2, 1];
        double[,] Across;
        double[,] Across_grid;
        
        double[,] Atran;        

        double[] Aspan;
        double sumspan;
        int ngirder;
        private void btApply_Click(object sender, EventArgs e)
        {
            switch (metroTabControl1.SelectedTab.Name)
            {
                case "pageGeneral":
                    {

                        ngirder = Convert.ToInt32(numgirder.Value);
                       
                        Atran = new double[3, ngirder + 1];
                        for (int i = 0; i < Atran.GetLength(1); i++)
                        {
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
                        sumspan = Aspan.Sum();

                        Atop = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Atop[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridTop, Atop);

                        Abot = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Abot[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridBot, Abot);

                        Aweb = new double[3, 1] { { 0 }, { 0 }, { 0 } };
                        Aweb[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridWeb, Aweb);

                        Acon = new double[2, 1] { { 0 }, { 0 } };
                        Acon[0, 0] = sumspan;
                        DGV.ArraytoGrid(gridCon, Acon);

                        // Convert 1D array Arr_span to 2D_Array Arr_cross
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

                    }
                    break;
                case "pageCross":
                    {
                        double[] Longcu = new double[Across_grid.GetLength(1) + 1];
                        Longcu[0] = 0;
                        for (int i = 1; i < Across_grid.GetLength(1) + 1; i++ )
                        {
                            Longcu[i] = 0;
                            for (int j = 0; j < i; j++)
                                Longcu[i] = Across_grid[2, j] + Longcu[i];                                                   
                        }

                        double[] Trancu = new double[Atran.GetLength(1) -1];
                        
                        Trancu[0] = 0;
                        for (int i = 1; i < Atran.GetLength(1) - 1; i++)
                        {
                            Trancu[i] = 0;
                            for (int j = 0; j < i; j++)
                                Trancu[i] = Atran[0, j+1] + Trancu[i];
                        }

                        List<Node> Node = new List<Node>();
                        int k = 1;
                        for (int i = 0; i < Trancu.GetLength(0); i++)
                        {
                            for (int j = 0; j < Longcu.GetLength(0); j++)
                            {
                                Node a = new Node();
                                a.Type = j == Longcu.GetLength(0) - 1 ? 1 : Across_grid[0, j];
                                a.BeamID = i == Trancu.GetLength(0) - 1 ? ngirder : Atran[2, i + 1];
                                a.X = Longcu[j];
                                a.Y = Trancu[i];
                                a.Z = 0;
                                a.Label = a.BeamID < 10 ? a.BeamID * 100 + j + 1 : (ngirder + k) * 100 + j+1;
                                Node.Add(a);
                            }
                            k = Atran[2, i + 1] > 10 ? k + 1 : k;
                        }
                                                
                        Node = Node.OrderBy(n => n.Label).ToList();  
                        //List<Node> Node = Trancu.Select(p => new Node() { X = p}).ToList(); 
                        
                        Tools.Access.writeList(Node, "Node",con, "All");

                        // Plot to the chart

                        List<ChartValues<ObservablePoint>> GirderPoint = new List<ChartValues<ObservablePoint>>();

                        for (int j = 0; j < ngirder; j ++)
                        {
                            ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();
                            
                            for (int i = 0; i < Longcu.GetLength(0); i++)
                            {
                                List1Points.Add(new ObservablePoint
                                {
                                    X = Node[j* (Longcu.GetLength(0)) + i].X,
                                    Y = Node[j * (Longcu.GetLength(0)) + i].Y
                                });
                            }
                            GirderPoint.Add(List1Points);
                        }

                        cartesianChart1.Series = new SeriesCollection();
                        
                        for (int i = 0; i< GirderPoint.Count; i++)
                        {
                            cartesianChart1.Series.Add(new LineSeries
                            {
                                Title = "Girder " + (i+1).ToString(),
                                Values = GirderPoint[i],
                                LineSmoothness = 0,
                                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                                Fill = System.Windows.Media.Brushes.Transparent,
                                //StrokeThickness = 4,
                            });
                        }

                        List<ChartValues<ObservablePoint>> StringerPoint = new List<ChartValues<ObservablePoint>>();

                        for (int j = 0; j < Trancu.GetLength(0) - ngirder; j++)
                        {
                            ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                            for (int i = 0; i < Longcu.GetLength(0); i++)
                            {
                                List1Points.Add(new ObservablePoint
                                {
                                    X = Node[ngirder * Longcu.GetLength(0) + j * Longcu.GetLength(0) + i].X,
                                    Y = Node[ngirder * Longcu.GetLength(0) + j * Longcu.GetLength(0) + i].Y
                                });
                            }
                            StringerPoint.Add(List1Points);
                        }

                        for (int i = 0; i < StringerPoint.Count; i++)
                        {
                            cartesianChart1.Series.Add(new LineSeries
                            {
                                Title = "Girder " + (i + 1).ToString(),
                                Values = StringerPoint[i],
                                LineSmoothness = 0,
                                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                                Fill = System.Windows.Media.Brushes.Transparent,
                                StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 },
                                //StrokeThickness = 4,
                            });
                        }

                        cartesianChart1.AxisY = new AxesCollection();
                        cartesianChart1.AxisY.Add(new Axis
                        {
                            MinValue = 0,
                            Separator = new Separator
                            {
                                IsEnabled = false
                            },
                            ShowLabels = false

                        }); ; ;
                        
                        cartesianChart1.AxisX = new AxesCollection();
                        cartesianChart1.AxisX.Add(new Axis
                        {

                            Separator = new Separator
                            {
                                IsEnabled = false
                            },
                            ShowLabels = false
                        });


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
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.White;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.White;
                }
                else if (arr[1, i] % 2 == 0)
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(221, 235, 247);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(221, 235, 247);
                }

                else
                {
                    dgv.Columns[i].DefaultCellStyle.BackColor = Color.FromArgb(226, 239, 218);
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.Columns[i].HeaderCell.Style.BackColor = Color.FromArgb(226, 239, 218);

                }
            }


        }

        int index;
        DataGridView SelectedDGV;
        private void grid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {

            if ((e.Button != MouseButtons.Right) || !(sender is DataGridView a))
                return;

            contextMenuStrip1.Show(Cursor.Position);
            index = e.ColumnIndex;

            if (a == gridCross)
            {
                if (Across_grid[0, index] == 1 || Across_grid[0, index] == 2)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }

            else if (a == gridTran)
            {
                if (Atran[2, index] < 10)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;

                if (Atran[2, index] == 0)
                    addTool.Enabled = false;
                else
                    addTool.Enabled = true;
            }

            else
            {
                if (index == a.ColumnCount - 1)
                    deleteTool.Enabled = false;
                else
                    deleteTool.Enabled = true;
            }
            SelectedDGV = a;
        }



        private void addTool_Click(object sender, EventArgs e)
        {

            if (SelectedDGV == gridTop)
            {
                Atop = Matrix.Seperate(Atop, index);
                DGV.ArraytoGrid(gridTop, Atop);
            }

            else if (SelectedDGV == gridBot)
            {
                Abot = Matrix.Seperate(Abot, index);
                DGV.ArraytoGrid(gridBot, Abot);
            }
            else if (SelectedDGV == gridWeb)
            {
                Aweb = Matrix.Seperate(Aweb, index);
                DGV.ArraytoGrid(gridWeb, Aweb);
            }
            else if (SelectedDGV == gridCon)
            {
                Acon = Matrix.Seperate(Acon, index);
                DGV.ArraytoGrid(gridCon, Acon);
            }
            else if (SelectedDGV == gridCross)
            {
                Across = Matrix.Seperate_cross(Across, index);
                Across_grid = Matrix.Seperate_cross(Across_grid, index);
                DGV.ArraytoGrid(gridCross, Across);
                Deco(SelectedDGV, Across_grid);
            }
            else if (SelectedDGV == gridTran)
            {               
                Atran = Matrix.Seperate_tran(Atran, index);
                DGV.ArraytoGrid_tran(gridTran, Atran);
                Deco(SelectedDGV, Atran);

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
            if (sender == gridTop)
            {
                Atop = DGV.GridtoArray(gridTop);
                Atop = Matrix.Update(Atop, sumspan);
                DGV.ArraytoGrid(gridTop, Atop);
                gridTop.MultiSelect = false;
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
            else if (sender == gridCon)
            {
                Acon = DGV.GridtoArray(gridCon);
                Acon = Matrix.Update(Acon, sumspan);
                DGV.ArraytoGrid(gridCon, Acon);
                gridCon.MultiSelect = false;
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
            else if (sender == gridTran)
            {
                Atran = DGV.GridtoArray_tran(gridTran,Atran);
            }
            //gridTop.Rows[row+1].Cells[col].Selected = true;
        }

        private void deleteTool_Click(object sender, EventArgs e)
        {

            if (SelectedDGV == gridTop)
            {
                Atop = Matrix.Combine(Atop, index);
                DGV.ArraytoGrid(gridTop, Atop);


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
            else if (SelectedDGV == gridCon)
            {
                Acon = Matrix.Combine(Acon, index);
                DGV.ArraytoGrid(gridCon, Acon);

            }
            else if (SelectedDGV == gridCross)
            {
                Across = Matrix.Combine_cross(Across, index);
                Across_grid = Matrix.Combine_cross(Across_grid, index);
                DGV.ArraytoGrid(gridCross, Across);
                Deco(SelectedDGV, Across_grid);
            }

            else if (SelectedDGV == gridTran)
            {
                Atran = Matrix.Combine_tran(Atran, index);
                DGV.ArraytoGrid_tran(gridTran, Atran);
                Deco(SelectedDGV, Atran);
            }

            if (SelectedDGV.Columns.Count <= 10)
                foreach (DataGridViewColumn c in SelectedDGV.Columns)
                {
                    c.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    SelectedDGV.Height = SelectedDGV.Rows.Count * 23 + 32;
                }
            SelectedDGV.ClearSelection();



        }




        //OK
    }


}
