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

            var mainbeam = grid.Where(p => p.BeamID < 10).ToList();
            var firstbeam = grid.Where(p => p.BeamID == 1).ToList();
            var maincross = grid.Where(p => ((p.Type == 1 || p.Type == 2) && p.BeamID < 10)).ToList();
            var restcross = grid.Where(p => (p.Type == 3  && p.BeamID < 10)).ToList();

            int ngirder = mainbeam.Count / firstbeam.Count;

            int longcount = firstbeam.Count;
            int trancount = grid.Count / longcount;
            int nstringer = trancount - ngirder;
            int nmcross = maincross.Count / ngirder;
            int nrcross = restcross.Count / ngirder;
            double maxy = grid.Max(p => p.Y);

            // Plot Main girder
            List<ChartValues<ObservablePoint>> GirderPoint = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < ngirder; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < longcount; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = grid[j * longcount + i].X,
                        Y = grid[j * longcount + i].Y
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
                });
            }

            //Plot stringer
            List<ChartValues<ObservablePoint>> StringerPoint = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < nstringer; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < longcount; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = grid[ngirder * longcount + j * longcount + i].X,
                        Y = grid[ngirder * longcount + j * longcount + i].Y
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
                    //StrokeThickness = 4,
                });
            }

            //Plot Cross beam at Pier and Abu
            List<ChartValues<ObservablePoint>> Crossmain = new List<ChartValues<ObservablePoint>>();

            for (int j = 0; j < nmcross; j++)
            {
                ChartValues<ObservablePoint> List1Points = new ChartValues<ObservablePoint>();

                for (int i = 0; i < ngirder; i++)
                {
                    List1Points.Add(new ObservablePoint
                    {
                        X = maincross[i*nmcross + j].X,
                        Y = maincross[i * nmcross + j].Y
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
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                    Fill = System.Windows.Media.Brushes.Transparent,                    
                    
                });
            }

            //Plot the rest crossbeam
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
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(198, 89, 17)),
                    Fill = System.Windows.Media.Brushes.Transparent,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 2 },
                });
            }

            gridchart.AxisY = new AxesCollection();
            gridchart.AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = maxy == 0? 10 : maxy ,
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
}
