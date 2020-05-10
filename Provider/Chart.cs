using Classes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider
{
    public class Chart
    {
        
        public static void Bridgegrid(List<Node> grid, LiveCharts.WinForms.CartesianChart gridchart)
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
                    
                }) ;
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
        }

        
    }
}
