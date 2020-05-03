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
using System.Windows;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using OfficeOpenXml;
using Provider;

namespace Sectional_Checking
{
    public partial class frmCons : Form
    {
        public frmCons()
        {
            InitializeComponent();
        }

        private void frmCons_Load(object sender, EventArgs e)
        {
            string constring = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + constring + @"\PUS1.accdb";
            OleDbConnection con = new OleDbConnection(constring);

            // Fill the Datagridview and chart for Tab 1

            Cons1_Load(con);
            
        }

        private void Cons1_Load(OleDbConnection con)
        {

            DataTable dt = new DataTable();
            dt = Access.getDataTable("select Label, Sta,Flexure, Mlw,Mlo,Mlf,Mlc,fl,fy06, Check_fl, Check_fl_ratio from Check_Cons", con);

            dtgCons1.DataSource = dt;
            var toFormat = new string[] { "Mlw", "Mlo", "Mlf", "Mlc", "fl" };
            foreach (string col in toFormat)
                this.dtgCons1.Columns[col].DefaultCellStyle.Format = "0.##";

            this.dtgCons1.EnableHeadersVisualStyles = false;
            this.dtgCons1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 192);


            var x = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<double>("Sta")).ToList();
            var y1 = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<double>("fl")).ToList();
            var y2 = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<double>("fy06")).ToList();


            ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

            for (int i = 0; i < x.Count; i++)
            {
                List1Points.Add(new ObservablePoint
                {
                    X = x[i],
                    Y = y1[i]
                });
            }

            ChartValues<ObservablePoint> List2Points = new ChartValues<ObservablePoint>();

            for (int i = 0; i < x.Count; i++)
            {
                List2Points.Add(new ObservablePoint
                {
                    X = x[i],
                    Y = y2[i]
                });
            }

            ChartCons1.Series = new SeriesCollection
            {
                new LineSeries
                    {
                        Title = "fl",
                        Values = List1Points,
                        LineSmoothness = 0
                    },


                new LineSeries
                    {
                        Title = "0.6Fy",
                         Values = List2Points,
                         LineSmoothness = 0
                    },

            };

            ChartCons1.AxisY.Add(new Axis
            {
                MinValue = 0,
                Title = "Stress (MPa)",
                Separator = new Separator
                {
                    Step = 50,
                    IsEnabled = true
                }
            });
            ChartCons1.AxisX.Add(new Axis
            {
                Title = "Length (m)",
                Separator = new Separator
                {
                    Step = 10,
                    IsEnabled = true
                }
            });
            ChartCons1.LegendLocation = LegendLocation.Right;
            ChartCons1.DefaultLegend.Visibility = Visibility.Visible;

            string picstr = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string picstr1 = picstr + @"\Picture\flbig.png";
            pictureBox1.Image = Image.FromFile(picstr1);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            string filestr = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            filestr = filestr + @"\Excel\v1.xlsx";
            var tableorder = new int[] { 97, 102, 196, 201, 207, 213, 219, 234, 240, 293, 318 };
            int node = 62;
            //var filllocation = new int[,] { {1,2 },{1,5 }, { 2, 3 }, { 3, 15 } , { 4, 11 } , { 5, 11 } };
            var filllocation = new int[,] { { 1, 2 } };
            string constring = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            constring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + constring + @"\PUS1.accdb";
            OleDbConnection con = new OleDbConnection(constring);
            DataTable filldata = Access.getDataTable("select Sta, Sc_top, Sc_bot, Mlw, Mlo, Mlf from Check_Cons", con);


            Excel.fillwithdt(filestr, tableorder, node, filllocation, filldata);
        }
    }
}
