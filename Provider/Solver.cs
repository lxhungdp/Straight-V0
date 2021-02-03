using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using MathNet.Numerics.Data.Matlab;
using solver;
using MathWorks.MATLAB.NET.Arrays;


namespace Provider
{
    public class Solver
    {
        public Solver()
        {

        }


        public List<Node> Node
        { get; set; }

        public List<Sec> Sec
        { get; set; }
        public List<Shoe> Shoe
        { get; set; }

        public List<Crossbeam> Crossbeam
        { get; set; }

        public List<Mat> Mat
        { get; set; }

        public double Overloading
        { get; set; }

        public List<Parapet> Parapet
        { get; set; }

        public List<double> eParapet
        { get; set; }

        public List<double> Asphalt
        { get; set; }
              

        public Liveload Liveloadinput
        { get; set; }
        
        public double LLoad
        { get; set; }

        public List<double> Lanefactor
        { get; set; }

        public double Pload
        { get; set; }

        public double delta
        { get; set; }


        //Input for Solver
        public MWNumericArray MNode
        {
            get
            {
                double[,] a = new double[Node.Count, 6];
                for (int i = 0; i < a.GetLength(0); i++)
                {
                    a[i, 0] = Node[i].Joint;
                    a[i, 1] = Node[i].BeamID;
                    a[i, 2] = Node[i].Type;
                    a[i, 3] = Node[i].X;
                    a[i, 4] = Node[i].Y;
                    a[i, 5] = Node[i].Z;
                }
                
                return a;
            }
        }

       
        public MWNumericArray MSec1
        {
            get
            {
                int n = Sec.Count / 2 + Crossbeam.Count + 1;
                double[,] a = new double[n, 5];

                for (int i = 0; i < Sec.Count / 2; i++)
                {
                    a[i, 0] = i + 1;
                    a[i, 1] = Sec[i * 2].A1;
                    a[i, 2] = Sec[i * 2].Ix1;
                    a[i, 3] = Sec[i * 2].Iy1;
                    a[i, 4] = Sec[i * 2].J1;                    
                }

                for (int i = Sec.Count / 2; i < n - 1; i++)
                {
                    int k = i - Sec.Count / 2;
                    a[i, 0] = i + 1;
                    a[i, 1] = Crossbeam[k].Area();
                    a[i, 2] = Crossbeam[k].Ix();
                    a[i, 3] = Crossbeam[k].Iy();
                    a[i, 4] = Crossbeam[k].J();
                }

                //Rigid link
                a[n - 1, 0] = n;
                a[n - 1, 1] = 10 * Math.Pow(10, 6);
                a[n - 1, 2] = 10 * Math.Pow(10, 12);
                a[n - 1, 3] = 10 * Math.Pow(10, 12);
                a[n - 1, 4] = 10 * Math.Pow(10, 12);
                
                return a;
            }
        }

        public MWNumericArray MSec2
        {
            get
            {
                int n = Sec.Count / 2 + Crossbeam.Count + 1;
                double[,] a = new double[n, 5];

                for (int i = 0; i < Sec.Count / 2; i++)
                {
                    a[i, 0] = i + 1;
                    a[i, 1] = Sec[i * 2].A2s;
                    a[i, 2] = Sec[i * 2].Ix2s;
                    a[i, 3] = Sec[i * 2].Iy2s;
                    a[i, 4] = Sec[i * 2].J2s;
                }

                for (int i = Sec.Count / 2; i < n - 1; i++)
                {
                    int k = i - Sec.Count / 2;
                    a[i, 0] = i + 1;
                    a[i, 1] = Crossbeam[k].Area();
                    a[i, 2] = Crossbeam[k].Ix();
                    a[i, 3] = Crossbeam[k].Iy();
                    a[i, 4] = Crossbeam[k].J();
                }

                //Rigid link
                a[n - 1, 0] = n;
                a[n - 1, 1] = 10 * Math.Pow(10, 6);
                a[n - 1, 2] = 10 * Math.Pow(10, 12);
                a[n - 1, 3] = 10 * Math.Pow(10, 12);
                a[n - 1, 4] = 10 * Math.Pow(10, 12);
                
                return a;
            }
        }

        public MWNumericArray MSec3
        {
            get
            {
                int n = Sec.Count / 2 + Crossbeam.Count + 1;
                double[,] a = new double[n, 5];

                for (int i = 0; i < Sec.Count / 2; i++)
                {
                    a[i, 0] = i + 1;
                    a[i, 1] = Sec[i * 2].A5s ;
                    a[i, 2] = Sec[i * 2].Ix5s ;
                    a[i, 3] = Sec[i * 2].Iy5s ;
                    a[i, 4] = Sec[i * 2].J5s ;
                }

                for (int i = Sec.Count / 2; i < n - 1; i++)
                {
                    int k = i - Sec.Count / 2;
                    a[i, 0] = i + 1;
                    a[i, 1] = Crossbeam[k].Area() ;
                    a[i, 2] = Crossbeam[k].Ix() ;
                    a[i, 3] = Crossbeam[k].Iy() ;
                    a[i, 4] = Crossbeam[k].J() ;
                }

                //Rigid link
                a[n - 1, 0] = n;
                a[n - 1, 1] = 10 * Math.Pow(10, 6);
                a[n - 1, 2] = 10 * Math.Pow(10, 12);
                a[n - 1, 3] = 10 * Math.Pow(10, 12);
                a[n - 1, 4] = 10 * Math.Pow(10, 12);
                
                return a;
            }
        }

        public MWNumericArray MShoe
        {
            get
            {
                int n = Shoe.Count();
                double[,] a = new double[n, 10];
                for (int i = 0; i < n; i++)
                {
                    a[i, 0] = Shoe[i].Girder;
                    a[i, 1] = Shoe[i].Support;
                    a[i, 2] = Shoe[i].EA;
                    a[i, 3] = Shoe[i].A;
                    a[i, 4] = Shoe[i].B;
                    a[i, 5] = Shoe[i].Joint;
                    a[i, 6] = Shoe[i].X;
                    a[i, 7] = Shoe[i].Y;
                    a[i, 8] = Shoe[i].Z;
                    a[i, 9] = Shoe[i].Type == "Fixed" ? 1 : (Shoe[i].Type == "TranFixed" ? 2 : (Shoe[i].Type == "LongFixed" ? 3 : 4));
                    
                }
                
                return a;
            }
        }

        public MWNumericArray MMat
        {
            get
            {                
                double[,] a = new double[4,1];
                a[0, 0] = Mat[0].Es;
                a[1, 0] = Mat[0].G;
                a[2, 0] = Mat[0].Ws * Overloading;
                a[3, 0] = Mat[6].Wc;
                
                return a;
            }
        }

        public MWNumericArray MBot
        {
            get
            {
                int nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
                int ntran = Node.Where(p => p.BeamID <=10).ToList().Count / nlong;

                double[,] a = new double[(nlong - 1) * ntran,1];
                for (int i = 0; i < ntran; i++)
                    for (int j = 0; j < nlong - 1; j++)
                        a[i * (nlong - 1) + j, 0] = Node[i * nlong + j].Ac;
               
                return a;
            }
        }

        public MWNumericArray MDeck
        {
            get
            {
                int nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
                int ntran = Node.Where(p => p.BeamID <= 10).ToList().Count / nlong;

                double[,] a = new double[(nlong - 1) * ntran,6];
                for (int i = 0; i < ntran; i++)
                    for (int j = 0; j < nlong - 1; j++)
                    {
                        a[i * (nlong - 1) + j, 0] = Node[i * nlong + j].bleft;
                        a[i * (nlong - 1) + j, 1] = Node[i * nlong + j].bright;
                        a[i * (nlong - 1) + j, 2] = Node[i * nlong + j].aleft;
                        a[i * (nlong - 1) + j, 3] = Node[i * nlong + j].aright;
                        a[i * (nlong - 1) + j, 4] = Node[i * nlong + j].e;
                        a[i * (nlong - 1) + j, 5] = Node[i * nlong + j].ts;
                    }
               
                return a;
            }
        }

        public MWNumericArray MBar
        {
            get
            {
                int n = Parapet.Count;
                
                double[,] a = new double[n, 2];
                for (int i = 0; i < n; i++)
                {
                    a[i, 0] = Parapet[i].Area();
                    a[i, 1] = Parapet[i].Ecc(); // eParapet[i] + Parapet[i].Ecc();
                }
                
                return a;
            }
        }

        public MWNumericArray MAsphalt
        {
            get
            {
                double[,] a = new double[2,1];
                a[0, 0] = Asphalt[0];
                a[1, 0] = Asphalt[1];
                
                return a;
            }
        }

        public MWNumericArray MTruck
        {
            get
            {
                List<Tuple<double, double>> axle = new List<Tuple<double, double>>(Liveloadinput.Axlebyfactor());
                int n = axle.Count;
                double[,] a = new double[n, 2];

                for (int i = 0; i < axle.Count; i++)
                {
                    a[i, 0] = axle[i].Item1;
                    a[i, 1] = axle[i].Item2;
                }
                
                return a;
            }
        }

        public MWNumericArray MLLoad
        {
            get
            {               
                double[,] a = new double[1,1];
                a[0, 0] = LLoad;

                return a;
            }
        }

        public MWNumericArray Me
        {
            get
            {
                List<double> LLeft = new List<double>(Liveloadinput.nlane().Item1);
                List<double> LRight = new List<double>(Liveloadinput.nlane().Item2);
                int n = LLeft.Count;

                double[,] a = new double[n, 2]; //Skip mid lane
                for (int i = 0; i < n; i ++)
                {
                    a[i, 0] = LLeft[i] / 1000;
                    a[i, 1] = LRight[i] / 1000;
                } 
                
                return a;
            }
        }

        public MWNumericArray MLfactor
        {
            get
            {                
                int n = Lanefactor.Count;

                double[, ] a = new double[n, 1];
                for (int i = 0; i < n; i++)                
                    a[i , 0] = Lanefactor[i];
               
                return a;
            }
        }

        public MWNumericArray MPload
        {
            get
            {
                int n = Liveloadinput.PLane().Count;

                double[,] a = new double[n, 2];
                for (int i = 0; i < n; i++)
                {
                    a[i, 0] = Liveloadinput.PLane()[i].Item1 / 1000 * Pload;
                    a[i, 1] = Liveloadinput.PLane()[i].Item2 / 1000 + Liveloadinput.PLane()[i].Item1 / 2000;
                }
                return a;
            }
        }

        public MWNumericArray Mdelta
        {
            get
            {                
                return new double[1, 1] { { delta } };
            }
        }

        //Result
        public double[,] Result
        {
            get
            {  
                PUS solver = new PUS();
                MWArray a = solver.steeltoliveload(MNode, MSec1, MSec2, MSec3, MMat, MBot, MDeck, MShoe, MBar, MAsphalt, MTruck, MLLoad, Me, Mdelta, MPload, MLfactor);               

                return (double[,])((MWNumericArray)a).ToArray(MWArrayComponent.Real);
            }
        }

        public double[,] Steel
        {
            get
            {
                PUS solver = new PUS();
                MWArray a = solver.steel(MNode, MSec1, MMat, MBot, MShoe);

                return (double[,])((MWNumericArray)a).ToArray(MWArrayComponent.Real);
            }
        }

        public double[,] Deck
        {
            get
            {
                PUS solver = new PUS();
                MWArray a = solver.deck(MNode, MSec2, MMat, MDeck, MShoe);

                return (double[,])((MWNumericArray)a).ToArray(MWArrayComponent.Real);
            }
        }

        public double[,] Barrier
        {
            get
            {
                PUS solver = new PUS();
                MWArray a = solver.barrier(MNode, MSec3, MMat, MDeck, MShoe, MBar, MAsphalt);

                return (double[,])((MWNumericArray)a).ToArray(MWArrayComponent.Real);
            }
        }

        public double[,] Liveload
        {
            get
            {
                PUS solver = new PUS();
                MWArray a = solver.liveload(MNode, MSec3, MMat, MShoe, MTruck, MLLoad, Me, Mdelta, MPload, MLfactor);

                return (double[,])((MWNumericArray)a).ToArray(MWArrayComponent.Real);
            }
        }


    }
}
