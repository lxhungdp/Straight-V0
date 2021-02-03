using Classes;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Provider
{
    public class Chart
    {

        public static void Bridgegrid(List<NodeInput> grid, LiveCharts.WinForms.CartesianChart gridchart)
        {

            double maxy = grid.Max(p => p.Y);
            double maxx = grid.Max(p => p.X);

            var maingirder = grid.Where(p => (p.Type == 1 || p.Type == 2) && p.BeamID < 10).ToList();
            var firstgirder = grid.Where(p => (p.Type == 1 || p.Type == 2) && p.BeamID == 1).ToList();
            int ngirder = maingirder.Count / firstgirder.Count;
            int longcount = maingirder.Count / ngirder;



            // Plot Main girder
            List<ChartValues<ObservablePoint>> GirderPoint = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < ngirder; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < longcount; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = maingirder[j * longcount + i].X,
                        Y = maingirder[j * longcount + i].Y
                    });
                }
                GirderPoint.Add(List1Points);
            }

            gridchart.Series = new SeriesCollection();

            for (int i = 0; i < GirderPoint.Count; i++)
            {
                gridchart.Series.Add(new LineSeries
                {
                    Title = "Girder " + (i + 1).ToString(),
                    Values = GirderPoint[i],
                    LineSmoothness = 0,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null,
                    StrokeThickness = 4,
                    LabelPoint = p => "",
                }); ;
            }

            var stringer = grid.Where(p => (p.Type == 1 || p.Type == 2) && p.BeamID > 10).ToList();
            int nstringer = stringer.Count / firstgirder.Count;

            //Plot stringer
            List<ChartValues<ObservablePoint>> StringerPoint = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < nstringer; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < longcount; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = stringer[j * longcount + i].X,
                        Y = stringer[j * longcount + i].Y
                    });
                }
                StringerPoint.Add(List1Points);
            }

            for (int i = 0; i < StringerPoint.Count; i++)
            {
                gridchart.Series.Add(new LineSeries
                {
                    Title = "Stringer " + (i + 1).ToString(),
                    Values = StringerPoint[i],
                    LineSmoothness = 0,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 },
                    PointGeometry = null,
                    //StrokeThickness = 4,
                    LabelPoint = p => "",

                });
            }

            //Plot Cross beam at Pier and Abu
            List<ChartValues<ObservablePoint>> Crossmain = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < longcount; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < ngirder; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = maingirder[i * longcount + j].X,
                        Y = maingirder[i * longcount + j].Y
                    });
                }
                Crossmain.Add(List1Points);
            }

            for (int i = 0; i < Crossmain.Count; i++)
            {
                gridchart.Series.Add(new LineSeries
                {
                    Title = "Cross " + (i + 1).ToString(),
                    Values = Crossmain[i],
                    LineSmoothness = 0,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null,
                    StrokeThickness = 4,
                    LabelPoint = p => "",
                });
            }


            //Plot the rest crossbeam
            var restcross = grid.Where(p => (p.Type == 3 && p.BeamID < 10)).ToList();
            int nrcross = restcross.Count / ngirder;
            List<ChartValues<ObservablePoint>> Crossrest = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < nrcross; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < ngirder; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = restcross[i * nrcross + j].X,
                        Y = restcross[i * nrcross + j].Y
                    });
                }
                Crossrest.Add(List1Points);
            }

            for (int i = 0; i < Crossrest.Count; i++)
            {
                gridchart.Series.Add(new LineSeries
                {
                    Title = "General cross " + (i + 1).ToString(),
                    Values = Crossrest[i],
                    LineSmoothness = 0,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 },
                    PointGeometry = null,
                    LabelPoint = p => "",
                });
            }

            //Plot the scatter support
            //Fixed

            var Supportfix = grid.Where(p => p.Restrain == "Fixed").ToList();

            ChartValues<ObservablePoint> Listfix = new ChartValues<ObservablePoint>();

            for (int i = 0; i < Supportfix.Count; i++)
            {
                Listfix.Add(new ObservablePoint
                {
                    X = Supportfix[i].X,
                    Y = Supportfix[i].Y
                });
            }


            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Fixed",
                Values = Listfix,
                PointGeometry = DefaultGeometries.Triangle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 15,
                LabelPoint = p => "",

            });

            //Plot the scatter support
            //LongFixed

            var Supportlong = grid.Where(p => p.Restrain == "LongFixed").ToList();

            ChartValues<ObservablePoint> Listlong = new ChartValues<ObservablePoint>();

            for (int i = 0; i < Supportlong.Count; i++)
            {
                Listlong.Add(new ObservablePoint
                {
                    X = Supportlong[i].X,
                    Y = Supportlong[i].Y
                });
            }


            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Fixed in longitudinal direction",
                Values = Listlong,
                PointGeometry = DefaultGeometries.Triangle,
                StrokeThickness = 3,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),

                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)),
                MinPointShapeDiameter = 15,
                LabelPoint = p => "",
            });

            //Tranfixed

            var Supporttran = grid.Where(p => p.Restrain == "TranFixed").ToList();

            ChartValues<ObservablePoint> Listtran = new ChartValues<ObservablePoint>();

            for (int i = 0; i < Supporttran.Count; i++)
            {
                Listtran.Add(new ObservablePoint
                {
                    X = Supporttran[i].X,
                    Y = Supporttran[i].Y
                });
            }


            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Fixed in transverse direction",
                Values = Listtran,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 15,

                LabelPoint = p => "",
            });

            //Free

            var Supportfree = grid.Where(p => p.Restrain == "Free").ToList();

            ChartValues<ObservablePoint> Listfree = new ChartValues<ObservablePoint>();

            for (int i = 0; i < Supportfree.Count; i++)
            {
                Listfree.Add(new ObservablePoint
                {
                    X = Supportfree[i].X,
                    Y = Supportfree[i].Y
                });
            }


            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Free",
                Values = Listfree,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 3,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),

                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)),
                MinPointShapeDiameter = 15,
                LabelPoint = p => "",
            });

            gridchart.AxisY = new AxesCollection();
            gridchart.AxisY.Add(new Axis
            {
                MinValue = 0 - maxy * 0.03,
                MaxValue = maxy == 0 ? 10 : maxy * 1.03,
                Separator = new Separator
                {
                    IsEnabled = false
                },
                ShowLabels = false

            });

            gridchart.AxisX = new AxesCollection();
            gridchart.AxisX.Add(new Axis
            {
                MinValue = 0 - maxx * 0.03,
                MaxValue = maxx == 0 ? 10 : maxx * 1.03,
                Separator = new Separator
                {
                    IsEnabled = false
                },
                ShowLabels = false
            });

            //gridchart.LegendLocation = LegendLocation.Right;
        }

        public static void byList(List<double> Dw, List<double> Lx, LiveCharts.WinForms.CartesianChart gridchart)
        {

            ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

            for (int i = 0; i < Dw.Count; i++)
            {
                List1Points.Add(new ObservablePoint
                {
                    X = Lx[i],
                    Y = -Dw[i]
                });
            }


            gridchart.Series = new SeriesCollection
            {
                new LineSeries
                    {
                        Title = "",
                        Values = List1Points,
                        LineSmoothness = 0,                        
                        //Fill = System.Windows.Media.Brushes.Transparent,
                        PointGeometry = null,
                         LabelPoint = p => "",
                    },

            };

            gridchart.AxisY = new AxesCollection();
            gridchart.AxisY.Add(new Axis
            {

                Separator = new Separator
                {
                    IsEnabled = false
                },
                ShowLabels = false

            });

            gridchart.AxisX = new AxesCollection();
            gridchart.AxisX.Add(new Axis
            {

                Separator = new Separator
                {
                    IsEnabled = false
                },
                ShowLabels = false
            });
        }

        public static void Haunch(double[] Aspan, double[,] DThaunch, LiveCharts.WinForms.CartesianChart gridchart)
        {
            if (DThaunch.GetLength(0) > 0)
            {
                Haunch Haunch = new Haunch(Aspan, DThaunch);
                Haunch.Sta = Haunch.Point;

                List<double> Lx = Haunch.Point;
                for (int i = 0; i < Haunch.Point.Count - 2; i++)
                {
                    if (Haunch.Dw[i + 1] != Haunch.Dw[i])
                        for (int j = 1; j < 20; j++)
                            Lx.Add(Haunch.Point[i] + (Haunch.Point[i + 1] - Haunch.Point[i]) / 20 * j);
                }

                Lx.Sort();
                Haunch.Sta = Lx;

                List<double> Ly = Haunch.Dw;
                Lx.Add(Lx.Max());
                Ly.Add(0);

                Lx.Add(0);
                Ly.Add(0);

                Lx.Add(0);
                Ly.Add(Ly[0]);

                //Plot the girder
                Chart.byList(Ly, Lx, gridchart);

                //Determine list of station for support
                double[] b = new double[Aspan.GetLength(0) + 1];

                b[0] = 0;
                for (int i = 1; i < Aspan.GetLength(0) + 1; i++)
                {
                    b[i] = 0;
                    for (int j = 0; j < i; j++)
                        b[i] = Aspan[j] + b[i];
                }
                List<double> SSup = new List<double>(b);
                Haunch.Sta = SSup;

                ChartValues<ObservablePoint> List2Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < Haunch.Dw.Count; i++)
                {
                    List2Points.Add(new ObservablePoint
                    {
                        X = SSup[i],
                        Y = -Haunch.Dw[i] - Haunch.Dw.Max() / 7
                    });
                }

                gridchart.Series.Add(new ScatterSeries
                {
                    Title = "Support",
                    Values = List2Points,
                    PointGeometry = DefaultGeometries.Circle,
                    StrokeThickness = 3,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),

                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255)),
                    MinPointShapeDiameter = 15,
                    LabelPoint = p => "",
                });



                gridchart.AxisY = new AxesCollection();
                gridchart.AxisY.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false

                });

                gridchart.AxisX = new AxesCollection();
                gridchart.AxisX.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false
                });
            }
        }

        public static void Forces(List<ElmPrint> Force, LiveCharts.WinForms.CartesianChart gridchart, List<string> type, int typeofforce)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            for (int i = 0; i < type.Count; i++)
            {
                ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();

                for (int j = 0; j < Force.Count; j++)
                {
                    PropertyInfo prop = Force[j].GetType().GetProperty(type[i]);
                    var p = prop.GetValue(Force[j]);

                    LPoint1.Add(new ObservablePoint
                    {
                        X = Force[j].Station,
                        Y = Convert.ToDouble(p),
                    });
                }
                LPoint.Add(LPoint1);
            }

            gridchart.Series = new SeriesCollection();

            for (int i = 0; i < type.Count; i++)
            {
                gridchart.Series.Add(new LineSeries
                {
                    Title = type[i],
                    Values = LPoint[i],
                    LineSmoothness = 0,
                    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null,
                    StrokeThickness = 2,
                    LabelPoint = p => "",
                });
            }

            string Ylabel = "";
            if (typeofforce == 0)
                Ylabel = "Moment (kNm)";
            else if (typeofforce == 1)
                Ylabel = "Shear (kN)";
            else if (typeofforce == 2)
                Ylabel = "Torsion (kNm)";
            else if (typeofforce == 3)
                Ylabel = "Deflection (m)";
            else
                Ylabel = "Reaction (kN)";

            if (type.Count != 0)
            {
                gridchart.AxisY = new AxesCollection();

                gridchart.AxisY.Add(new Axis
                {
                    Title = Ylabel,
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                });

                gridchart.AxisX = new AxesCollection();
                gridchart.AxisX.Add(new Axis
                {
                    Title = "Length in mm",
                    MaxValue = Force.Max(p => p.Station),
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),
                    Separator = new Separator
                    {
                        Step = 10000,
                        IsEnabled = true,

                    }


                });
                gridchart.DataTooltip = null;
                gridchart.Hoverable = false;
            }
            else
            {
                gridchart.AxisY = new AxesCollection();
                gridchart.AxisY.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false

                });

                gridchart.AxisX = new AxesCollection();
                gridchart.AxisX.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false
                });
            }


            gridchart.LegendLocation = LegendLocation.Right;
        }

        public static void Sec(DataTable Sec, LiveCharts.WinForms.CartesianChart gridchart)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            for (int i = 1; i < Sec.Columns.Count; i++)
            {
                double factor = 1;
                if (Sec.Columns[1].ColumnName[0] == 'A')
                    factor = Math.Pow(10, 6);
                else if (Sec.Columns[1].ColumnName[0] == 'I')
                    factor = Math.Pow(10, 12);
                else if (Sec.Columns[1].ColumnName[0] == 'Y')
                    factor = Math.Pow(10, 3);
                else if (Sec.Columns[1].ColumnName[0] == 'J')
                    factor = Math.Pow(10, 12);

                ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();

                for (int j = 0; j < Sec.Rows.Count; j++)
                {
                    LPoint1.Add(new ObservablePoint
                    {
                        X = Convert.ToDouble(Sec.Rows[j][0]),
                        Y = Convert.ToDouble(Sec.Rows[j][i]) / factor,
                    });
                }
                LPoint.Add(LPoint1);
            }

            gridchart.Series = new SeriesCollection();

            string Title1 = "";

            int index = 1;
            if (Sec.Columns.Count > 1)
                if (Sec.Columns[1].ColumnName[0] == 'A' || Sec.Columns[1].ColumnName[0] == 'J')
                    index = 1;
                else
                    index = 2;

            for (int i = 1; i < Sec.Columns.Count; i++)
            {
                if (Sec.Columns[i].ColumnName[index] == '1')
                    Title1 = "Type 1";
                else if (Sec.Columns[i].ColumnName[index] == '2')
                    Title1 = "Type 2";
                else if (Sec.Columns[i].ColumnName[index] == '3')
                    Title1 = "Type 3";
                else if (Sec.Columns[i].ColumnName[index] == '4')
                    Title1 = "Type 4";
                else if (Sec.Columns[i].ColumnName[index] == '5')
                    Title1 = "Type 5";

                gridchart.Series.Add(new LineSeries
                {
                    Title = Title1,
                    Values = LPoint[i - 1],
                    LineSmoothness = 0,
                    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null,
                    StrokeThickness = 2,
                    LabelPoint = p => "",
                });
            }

            string Ylabel = "";


            if (Sec.Columns.Count > 1)
            {
                if (Sec.Columns[1].ColumnName[0] == 'A')
                    Ylabel = "Area (m2)";
                else if (Sec.Columns[1].ColumnName[0] == 'I')
                    Ylabel = "Moment of Inertia (m4)";
                else if (Sec.Columns[1].ColumnName[1] == 'U')
                    Ylabel = "NA to top flange (m)";
                else if (Sec.Columns[1].ColumnName[1] == 'L')
                    Ylabel = "NA to bottom flange (m)";
                else if (Sec.Columns[1].ColumnName[0] == 'J')
                    Ylabel = "Torsion constant (m4)";

                gridchart.AxisY = new AxesCollection();

                gridchart.AxisY.Add(new Axis
                {
                    Title = Ylabel,
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                });

                gridchart.AxisX = new AxesCollection();
                gridchart.AxisX.Add(new Axis
                {
                    Title = "Length in mm",
                    //MaxValue = Force.Max(p => p.Station),
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),
                    Separator = new Separator
                    {
                        Step = 10000,
                        IsEnabled = true,

                    }


                });
                gridchart.DataTooltip = null;
                gridchart.Hoverable = false;
            }
            else
            {
                gridchart.AxisY = new AxesCollection();
                gridchart.AxisY.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false

                });

                gridchart.AxisX = new AxesCollection();
                gridchart.AxisX.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false
                });
            }


            gridchart.LegendLocation = LegendLocation.Right;
        }

        public static void Stress(DataTable Stress, LiveCharts.WinForms.CartesianChart gridchart, string topbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            for (int i = 1; i < Stress.Columns.Count; i++)
            {

                ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();

                for (int j = 0; j < Stress.Rows.Count; j++)
                {
                    LPoint1.Add(new ObservablePoint
                    {
                        X = Convert.ToDouble(Stress.Rows[j][0]),
                        Y = Convert.ToDouble(Stress.Rows[j][i]),
                    });
                }

                LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(Stress.Rows[Stress.Rows.Count - 1][0]), Y = 0 });
                LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });

                LPoint.Add(LPoint1);
            }
            //var dangerBrush = new SolidColorBrush(Color.FromRgb(238, 83, 80));

            //var mapper = Mappers.Xy<ObservableValue>()
            //   .X((item, index) => index)
            //   .Y(item => item.Value)
            //   .Fill(item => item.Value > 100 ? dangerBrush : null)
            //   .Stroke(item => item.Value > 100 ? dangerBrush : null);

            ChartValues<ObservablePoint> Maxpoint = new ChartValues<ObservablePoint>();








            gridchart.Series = new SeriesCollection();

            string Title1 = "";


            for (int i = 1; i < Stress.Columns.Count; i++)
            {
                if (Stress.Columns[i].ColumnName[1] == '1')
                    Title1 = "DC1";
                else if (Stress.Columns[i].ColumnName[1] == '2')
                    Title1 = "DC2";
                else if (Stress.Columns[i].ColumnName[1] == '3')
                    Title1 = "DC3";
                else if (Stress.Columns[i].ColumnName[1] == '4')
                    Title1 = "DC4";
                else if (Stress.Columns[i].ColumnName[1] == 'w')
                    Title1 = "DW";
                else if (Stress.Columns[i].ColumnName[3] == 'a')
                    Title1 = "LLmax";
                else if (Stress.Columns[i].ColumnName[3] == 'i')
                    Title1 = "LLmin";
                else if (Stress.Columns[i].ColumnName[1] == 'c')
                    Title1 = "Cons";
                else if (Stress.Columns[i].ColumnName[1] == 'u')
                    Title1 = "ULS-I";
                else if (Stress.Columns[i].ColumnName[2] == '1')
                    Title1 = "SLS-I";
                else if (Stress.Columns[i].ColumnName[2] == '2')
                    Title1 = "SLS-II";
                else if (Stress.Columns[i].ColumnName[1] == 'f')
                    Title1 = "FLS";


                gridchart.Series.Add(new LineSeries
                {
                    //Configuration = mapper,
                    Title = Title1,
                    Values = LPoint[i - 1],
                    LineSmoothness = 0,
                    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    //Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null,
                    StrokeThickness = 2,
                    LabelPoint = p => "",
                    //PointForeground = Brushes.White
                });



            }


            if (Stress.Columns.Count > 1)
            {

                gridchart.AxisY = new AxesCollection();

                gridchart.AxisY.Add(new Axis
                {
                    Title = (topbot == "top" ? "Top Flange" : "Bottom Flange") + " Stress (MPa)",
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                });

                gridchart.AxisX = new AxesCollection();

                gridchart.AxisX.Add(new Axis
                {
                    Title = "Length in mm",
                    //MaxValue = Force.Max(p => p.Station),
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                    Separator = new Separator
                    {
                        Step = 10000,
                        IsEnabled = true,
                    }
                });
                gridchart.DataTooltip = null;
                gridchart.Hoverable = false;

            }
            else
            {
                gridchart.AxisY = new AxesCollection();
                gridchart.AxisY.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false

                });

                gridchart.AxisX = new AxesCollection();
                gridchart.AxisX.Add(new Axis
                {

                    Separator = new Separator
                    {
                        IsEnabled = false
                    },
                    ShowLabels = false
                });
            }


            gridchart.LegendLocation = LegendLocation.Right;


        }


        public static void CumStress(DataTable Stress, LiveCharts.WinForms.CartesianChart gridchart, string topbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            for (int i = 1; i < Stress.Columns.Count; i++)
            {

                ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();

                for (int j = 0; j < Stress.Rows.Count; j++)
                {
                    LPoint1.Add(new ObservablePoint
                    {
                        X = Convert.ToDouble(Stress.Rows[j][0]),
                        Y = Convert.ToDouble(Stress.Rows[j][i]),
                    });
                }
                LPoint.Add(LPoint1);
            }


            gridchart.Series = new SeriesCollection();

            string Title1 = "";


            for (int i = 1; i < Stress.Columns.Count; i++)
            {
                if (Stress.Columns[i].ColumnName[1] == '1')
                    Title1 = "DC1";
                else if (Stress.Columns[i].ColumnName[1] == '2')
                    Title1 = "DC2";
                else if (Stress.Columns[i].ColumnName[1] == '3')
                    Title1 = "DC3";
                else if (Stress.Columns[i].ColumnName[1] == '4')
                    Title1 = "DC4";
                else if (Stress.Columns[i].ColumnName[1] == 'w')
                    Title1 = "DW";
                else if (Stress.Columns[i].ColumnName[3] == 'a')
                    Title1 = "LLmax";
                else if (Stress.Columns[i].ColumnName[3] == 'i')
                    Title1 = "LLmin";


                gridchart.Series.Add(new StackedAreaSeries
                {
                    Title = Title1,
                    Values = LPoint[i - 1],
                    LineSmoothness = 0,
                    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                    //Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometry = null,
                    StrokeThickness = 2,
                    LabelPoint = p => "",
                });
            }


            if (Stress.Columns.Count > 1)
            {

                gridchart.AxisY = new AxesCollection();

                gridchart.AxisY.Add(new Axis
                {
                    Title = (topbot == "top" ? "Top Flange" : "Bottom Flange") + " Stress (MPa)",
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                });

                gridchart.AxisX = new AxesCollection();

                gridchart.AxisX.Add(new Axis
                {
                    Title = "Length in mm",
                    //MaxValue = Force.Max(p => p.Station),
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                    Separator = new Separator
                    {
                        Step = 10000,
                        IsEnabled = true,
                    }
                });

            }
            else
            {

            }


            gridchart.LegendLocation = LegendLocation.Right;
        }


        //Sectional checking
        public static void Checkfl(DataTable dt, LiveCharts.WinForms.CartesianChart gridchart)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint.Add(LPoint1);

            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint.Add(LPoint1);
            
            DataRow[] drmax = dt.Select("[fl] = MAX([fl])");            

            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();

            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(drmax[0][0].ToString()), Y = Convert.ToDouble(drmax[0][1].ToString()) });
            

            gridchart.Series = new SeriesCollection();
            gridchart.Series.Add(new LineSeries
            {
                Title = "fl",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridchart.Series.Add(new LineSeries
            {
                Title = "0.6Fy",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });


            gridchart.AxisY = new AxesCollection();

            gridchart.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 50,
                    IsEnabled = true,
                },
                MinValue = 0,

            });

            gridchart.AxisX = new AxesCollection();

            gridchart.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridchart.DataTooltip = null;
            gridchart.Hoverable = false;

            gridchart.LegendLocation = LegendLocation.Right;
        }



        public static void CheckCons(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();
            
            //Sc_top
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();            
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Sc_top combined to fl
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                double Sc = Convert.ToDouble(dt.Rows[j][1]);
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Sc >= 0 ? Sc + Convert.ToDouble(dt.Rows[j][3]) : Sc - Convert.ToDouble(dt.Rows[j][3]) / 3,
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Fnc
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][4]),
                });
            }
            LPoint.Add(LPoint1);

            ////Fnt
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][5]),
                });
            }
            LPoint.Add(LPoint1);

            //Sc_bot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            
            LPoint.Add(LPoint1);

            //Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[fbufl_ten] = MAX([fbufl_ten])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][7].ToString()) });
            dr = dt.Select("[fbufl3_com] = MAX([fbufl3_com])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = -Convert.ToDouble(dr[0][6].ToString()) });

            //Plot to gridtop
            gridtop.Series = new SeriesCollection();

            //gridtop.Series.Add(new LineSeries
            //{
            //    Title = "fbu",
            //    Values = LPoint[0],
            //    LineSmoothness = 0,
            //    //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
            //    //Fill = System.Windows.Media.Brushes.Transparent,
            //    PointGeometry = null,
            //    StrokeThickness = 2,
            //    LabelPoint = p => "",
            //});

            gridtop.Series.Add(new LineSeries
            {
                Title = "fbu & fl",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "Φf*Fnc",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "Φf*Fnt",
                Values = LPoint[3],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },

                //Sections = new SectionsCollection
                //    {
                //        new AxisSection {
                //        Value = 0,
                //        SectionWidth = 20,
                //        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                //    },

                //    }

            }); ;

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;

            //Plot to gridbot
            //Extreme value
            maxminpoint = new ChartValues<ObservablePoint>();
            dr = dt.Select("[Sc_bot] = MAX([Sc_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });
            dr = dt.Select("[Sc_bot] = MIN([Sc_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });

            //Plot to gridtop
            gridbot.Series = new SeriesCollection();

            gridbot.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[4],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });


            gridbot.Series.Add(new LineSeries
            {
                Title = "Φf*Fnc",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new LineSeries
            {
                Title = "Φf*Fnt",
                Values = LPoint[3],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridbot.AxisY = new AxesCollection();

            gridbot.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },

                //Sections = new SectionsCollection
                //    {
                //        new AxisSection {
                //        Value = 0,
                //        SectionWidth = 20,
                //        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                //    },

                //    }

            }); ;

            gridbot.AxisX = new AxesCollection();

            gridbot.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridbot.DataTooltip = null;
            gridbot.Hoverable = false;
            gridbot.LegendLocation = LegendLocation.Right;

        }



        public static void CheckFcrw(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //Sc_top
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Sc_bot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Fcrw
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][3]),
                });
            }            
            LPoint.Add(LPoint1);

            //Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[Sc_top] = MIN([Sc_top])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });
            

            //Top flange
            gridtop.Series = new SeriesCollection();

            gridtop.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "Fcrw",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });



            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },

                //Sections = new SectionsCollection
                //    {
                //        new AxisSection {
                //        Value = 0,
                //        SectionWidth = 20,
                //        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                //    },

                //    }

            }); ;

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;

            //Extreme value
            maxminpoint = new ChartValues<ObservablePoint>();
            dr = dt.Select("[Sc_bot] = MIN([Sc_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });


            //Bottom flange
            gridbot.Series = new SeriesCollection();

            gridbot.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new LineSeries
            {
                Title = "Fcrw",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });



            gridbot.AxisY = new AxesCollection();

            gridbot.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },

                //Sections = new SectionsCollection
                //    {
                //        new AxisSection {
                //        Value = 0,
                //        SectionWidth = 20,
                //        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                //    },

                //    }

            }); ;

            gridbot.AxisX = new AxesCollection();

            gridbot.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridbot.DataTooltip = null;
            gridbot.Hoverable = false;
            gridbot.LegendLocation = LegendLocation.Right;
        }




        public static void CheckCShear(DataTable dt, LiveCharts.WinForms.CartesianChart gridchart)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //Vui
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Vr
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }           
            LPoint.Add(LPoint1);

            //-Vr
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint.Add(LPoint1);






            gridchart.Series = new SeriesCollection();

            gridchart.Series.Add(new LineSeries
            {
                Title = "Vu",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridchart.Series.Add(new LineSeries
            {
                Title = "+Vr",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });
            gridchart.Series.Add(new LineSeries
            {
                Title = "-Vr",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            //Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[Vui] = MAX([Vui])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });
            dr = dt.Select("[Vui] = MIN([Vui])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });

            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });


            gridchart.AxisY = new AxesCollection();

            gridchart.AxisY.Add(new Axis
            {
                Title = "Shear (kN)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    //Step = 1000,
                    IsEnabled = true,
                },

                //Sections = new SectionsCollection
                //    {
                //        new AxisSection {
                //        Value = 0,
                //        SectionWidth = 20,
                //        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                //    },

                //    }

            }); ;

            gridchart.AxisX = new AxesCollection();

            gridchart.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridchart.DataTooltip = null;
            gridchart.Hoverable = false;
            gridchart.LegendLocation = LegendLocation.Right;
        }


        //Checking Ductility
        public static void CheckDuc(DataTable dt, LiveCharts.WinForms.CartesianChart gridchart)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //Dp
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][4]) == 1000000 ? double.NaN : Convert.ToDouble(dt.Rows[j][1]),
                });
            }
           
            LPoint.Add(LPoint1);

            //0.42Dt
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][4]) == 1000000 ? double.NaN : Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint.Add(LPoint1);

            

            gridchart.Series = new SeriesCollection();

            gridchart.Series.Add(new LineSeries
            {
                Title = "Dp",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridchart.Series.Add(new LineSeries
            {
                Title = "0.42*Dt",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });
            

            gridchart.AxisY = new AxesCollection();

            gridchart.AxisY.Add(new Axis
            {
                Title = "Depth (mm)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),
                MinValue = 0,
                Separator = new Separator
                {
                    Step = 250,
                    IsEnabled = true,
                },

                Sections = new SectionsCollection
                    {
                        new AxisSection {
                        Value = 0,
                        SectionWidth = 10,
                        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                    },

                    }

            }); ;

            gridchart.AxisX = new AxesCollection();

            gridchart.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                MinValue = 0,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridchart.DataTooltip = null;
            gridchart.Hoverable = false;
            gridchart.LegendLocation = LegendLocation.Right;
        }

        //Checking Concrete stress
        public static void CheckConcrete(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //fdeck
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = dt.Rows[j][5].ToString() == "-" ? double.NaN : Convert.ToDouble(dt.Rows[j][1]),
                });
            }

            LPoint.Add(LPoint1);

            //0.6feck
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = dt.Rows[j][5].ToString() == "-" ? double.NaN : Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint.Add(LPoint1);

            //fbot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = dt.Rows[j][6].ToString() == "-" ? double.NaN : Convert.ToDouble(dt.Rows[j][3]),
                });
            }
            LPoint.Add(LPoint1);

            //0.6fcbot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = dt.Rows[j][6].ToString() == "-" ? double.NaN : Convert.ToDouble(dt.Rows[j][4]),
                });
            }
            LPoint.Add(LPoint1);



            gridtop.Series = new SeriesCollection();

            gridtop.Series.Add(new LineSeries
            {
                Title = "Deck Con'c Stress (MPa)",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "0.6*f'c",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            //Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[fdeck] = MAX([fdeck])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });
            

            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });


            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),
                MinValue = 0,
                Separator = new Separator
                {
                    Step = 5,
                    IsEnabled = true,
                },
                

            }); ;

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                MinValue = 0,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;


            //Bottom concrete
            gridbot.Series = new SeriesCollection();

            gridbot.Series.Add(new LineSeries
            {
                Title = "Bot Con'c Stress (MPa)",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new LineSeries
            {
                Title = "0.6*f'c",
                Values = LPoint[3],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            //Extreme value
            maxminpoint = new ChartValues<ObservablePoint>();
            dr = dt.Select("[fbot] = MAX([fbot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][3].ToString()) });


            gridbot.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });


            gridbot.AxisY = new AxesCollection();

            gridbot.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),
                MinValue = 0,
                Separator = new Separator
                {
                    Step = 5,
                    IsEnabled = true,
                },


            }); ;

            gridbot.AxisX = new AxesCollection();

            gridbot.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                MinValue = 0,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridbot.DataTooltip = null;
            gridbot.Hoverable = false;
            gridbot.LegendLocation = LegendLocation.Right;
        }

        //ULS
        public static void CheckUstress(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //Su_top
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Su_bot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {                
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][3]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Fnc
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][4]),
                });
            }
            LPoint.Add(LPoint1);

            ////Fnt
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][5]),
                });
            }
            LPoint.Add(LPoint1);


            //Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[Su_top] = MAX([Su_top])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });
            dr = dt.Select("[Su_top] = MIN([Su_top])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });

            //Plot to gridtop
            gridtop.Series = new SeriesCollection();
            
            gridtop.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "Φf*Fnc",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "Φf*Fnt",
                Values = LPoint[3],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },
            }); ;

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;

            //Plot to gridbot
            //Extreme value
            maxminpoint = new ChartValues<ObservablePoint>();
            dr = dt.Select("[Su_bot] = MAX([Su_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][3].ToString()) });
            dr = dt.Select("[Su_bot] = MIN([Su_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][3].ToString()) });

            //Plot to gridtop
            gridbot.Series = new SeriesCollection();

            gridbot.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });


            gridbot.Series.Add(new LineSeries
            {
                Title = "Φf*Fnc",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new LineSeries
            {
                Title = "Φf*Fnt",
                Values = LPoint[3],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridbot.AxisY = new AxesCollection();

            gridbot.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },                
            }); ;

            gridbot.AxisX = new AxesCollection();

            gridbot.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridbot.DataTooltip = null;
            gridbot.Hoverable = false;
            gridbot.LegendLocation = LegendLocation.Right;

        }


        public static void CheckUShear(DataTable dt, LiveCharts.WinForms.CartesianChart gridchart)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //Vui
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Vr
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint.Add(LPoint1);

            //-Vr
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint.Add(LPoint1);






            gridchart.Series = new SeriesCollection();

            gridchart.Series.Add(new LineSeries
            {
                Title = "Vu",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridchart.Series.Add(new LineSeries
            {
                Title = "+Vr",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });
            gridchart.Series.Add(new LineSeries
            {
                Title = "-Vr",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            //Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[Vui] = MAX([Vui])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });
            dr = dt.Select("[Vui] = MIN([Vui])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });

            gridchart.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });


            gridchart.AxisY = new AxesCollection();

            gridchart.AxisY.Add(new Axis
            {
                Title = "Shear (kN)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 500,
                    IsEnabled = true,
                },

                //Sections = new SectionsCollection
                //    {
                //        new AxisSection {
                //        Value = 0,
                //        SectionWidth = 20,
                //        Stroke = new SolidColorBrush(Color.FromRgb(33, 115, 70))
                //    },

                //    }

            }); ;

            gridchart.AxisX = new AxesCollection();

            gridchart.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridchart.DataTooltip = null;
            gridchart.Hoverable = false;
            gridchart.LegendLocation = LegendLocation.Right;
        }


        public static void CheckSstress(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //Ss2_top
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Ss2_bot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Fytop
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][3]),
                });
            }
            LPoint.Add(LPoint1);

            //Fytop
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][3]),
                });
            }
            LPoint.Add(LPoint1);

            ////Fybot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][4]),
                });
            }
            LPoint.Add(LPoint1);

            ////Fybot
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][4]),
                });
            }
            LPoint.Add(LPoint1);


            ////Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[Ss2_top] = MAX([Ss2_top])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });
            dr = dt.Select("[Ss2_top] = MIN([Ss2_top])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });

            //Plot to gridtop
            gridtop.Series = new SeriesCollection();

            gridtop.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "+0.95RhFyf",
                Values = LPoint[2],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "-0.95RhFyf",
                Values = LPoint[3],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },
            }); ;

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;

            //Plot to gridbot
            //Extreme value
            maxminpoint = new ChartValues<ObservablePoint>();
            dr = dt.Select("[Ss2_bot] = MAX([Ss2_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });
            dr = dt.Select("[Ss2_bot] = MIN([Ss2_bot])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][2].ToString()) });

            //Plot to gridtop
            gridbot.Series = new SeriesCollection();

            gridbot.Series.Add(new LineSeries
            {
                Title = "fbu",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });


            gridbot.Series.Add(new LineSeries
            {
                Title = "+0.95RhFyf",
                Values = LPoint[4],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new LineSeries
            {
                Title = "-0.95RhFyf",
                Values = LPoint[5],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridbot.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridbot.AxisY = new AxesCollection();

            gridbot.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },
            }); ;

            gridbot.AxisX = new AxesCollection();

            gridbot.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridbot.DataTooltip = null;
            gridbot.Hoverable = false;
            gridbot.LegendLocation = LegendLocation.Right;

        }


        public static void CheckSFcrw(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //fc
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = -Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //Fcrw
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = Convert.ToDouble(dt.Rows[j][2]),
                });
            }
            
            LPoint.Add(LPoint1);
                        
            ////Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[fc] = MIN([fc])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = -Convert.ToDouble(dr[0][1].ToString()) });
            

            //Plot to gridtop
            gridtop.Series = new SeriesCollection();

            gridtop.Series.Add(new LineSeries
            {
                Title = "fc",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "Fcrw",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            

            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },
            }); ;

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;

            

            

        }


        public static void CheckSFy(DataTable dt, LiveCharts.WinForms.CartesianChart gridtop, LiveCharts.WinForms.CartesianChart gridbot)
        {
            List<ChartValues<ObservablePoint>> LPoint = new List<ChartValues<ObservablePoint>>();

            //fs
            ChartValues<ObservablePoint> LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = dt.Rows[j][3].ToString() == "-" ? Double.NaN : Convert.ToDouble(dt.Rows[j][1]),
                });
            }
            LPoint1.Add(new ObservablePoint { X = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][0]), Y = 0 });
            LPoint1.Add(new ObservablePoint { X = 0, Y = 0 });
            LPoint.Add(LPoint1);

            //O8Fy
            LPoint1 = new ChartValues<ObservablePoint>();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                LPoint1.Add(new ObservablePoint
                {
                    X = Convert.ToDouble(dt.Rows[j][0]),
                    Y = dt.Rows[j][3].ToString() == "-" ? Double.NaN : Convert.ToDouble(dt.Rows[j][2]),
                });
            }

            LPoint.Add(LPoint1);

            ////Extreme value
            ChartValues<ObservablePoint> maxminpoint = new ChartValues<ObservablePoint>();
            DataRow[] dr = dt.Select("[fs] = MAX([fs])");
            maxminpoint.Add(new ObservablePoint { X = Convert.ToDouble(dr[0][0].ToString()), Y = Convert.ToDouble(dr[0][1].ToString()) });


            //Plot to gridtop
            gridtop.Series = new SeriesCollection();

            gridtop.Series.Add(new LineSeries
            {
                Title = "fs",
                Values = LPoint[0],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                //Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });

            gridtop.Series.Add(new LineSeries
            {
                Title = "0.8Fy",
                Values = LPoint[1],
                LineSmoothness = 0,
                //Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(69, 141, 207)),
                Fill = System.Windows.Media.Brushes.Transparent,
                PointGeometry = null,
                StrokeThickness = 2,
                LabelPoint = p => "",
            });



            gridtop.Series.Add(new ScatterSeries
            {
                Title = "Extreme value",
                Values = maxminpoint,
                PointGeometry = DefaultGeometries.Circle,
                StrokeThickness = 10,
                Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                MinPointShapeDiameter = 7,
                DataLabels = true,
                LabelPoint = p => Math.Round(p.Y, 2).ToString(),
            });

            gridtop.AxisY = new AxesCollection();

            gridtop.AxisY.Add(new Axis
            {
                Title = "Stress (MPa)",
                MinValue = 0,
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 100,
                    IsEnabled = true,
                },
            }); 

            gridtop.AxisX = new AxesCollection();

            gridtop.AxisX.Add(new Axis
            {
                Title = "Length in mm",
                //MaxValue = Force.Max(p => p.Station),
                Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(33, 115, 70)),

                Separator = new Separator
                {
                    Step = 10000,
                    IsEnabled = true,
                }
            });
            gridtop.DataTooltip = null;
            gridtop.Hoverable = false;
            gridtop.LegendLocation = LegendLocation.Right;





        }
    }
}
