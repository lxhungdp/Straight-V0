using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Mainform
{
    public class InitializeValues
    {
        //General
        public static string bridgename()
        {
            return "PUS Bridge 1";
        }

        public static int ngirder()
        {
            return 1;
        }

        public static string txtspan()
        {
            return "50";
        }

        //Material
        public static List<Mat> Mat()
        {
            List<Mat> Mat = new List<Mat>();
            Mat.Add(new Mat("Concrete", "Concrete", 75, 210000, 81000, 380, 500, 25, 35, 35000, ""));
            Mat.Add(new Mat("Steel-HB380", "Steel", 75, 210000, 81000, 380, 500, 25, 35, 35000, ""));
            return Mat;
        }

        public static List<Mat> Matuse()
        {
            List<Mat> Matuse = new List<Mat>();
            Matuse.Add(Mat()[1]);
            Matuse.Add(Mat()[1]);
            Matuse.Add(Mat()[1]);
            Matuse.Add(Mat()[1]);
            Matuse.Add(Mat()[1]);
            Matuse.Add(Mat()[1]);

            Matuse.Add(Mat()[0]);
            Matuse.Add(Mat()[0]);

            Mat Matempty = new Mat("", "", 0, 0, 0, 0, 0, 0, 0, 0, "");
            Matuse.Add(Matempty);
            Matuse.Add(Matempty);
            Matuse.Add(Matempty);
            Matuse.Add(Matempty);
            Matuse.Add(Matempty);
            Matuse.Add(Matempty);
            return Matuse;
        }



        //Loading
        public static int Tructype()
        { return 0; }

        public static int Truckgrade()
        { return 0; }
        public static double Laneload()
        { return 12.7; }
        public static double Pload()
        { return 3.5; }
        public static double ADTT()
        { return 1500; }
        public static double Overloading()
        { return 1.4; }
        public static double Pforms()
        { return 3.0; }
        public static double Pparapet()
        { return 3.0; }
        public static double tAsphalt()
        { return 80; }
        public static double gAsphalt()
        { return 23.5; }



        public static List<double> Lanefactor()
        { return new List<double> { 1, 0.9, 0.8, 0.7, 0.65 }; }

        public static List<Tuple<double, double>> Truckaxle()
        {
            List<Tuple<double, double>> a = new List<Tuple<double, double>>();
            Tuple<double, double> b = Tuple.Create(0.0, 48.0);
            a.Add(b);
            b = Tuple.Create(3.6, 135.0);
            a.Add(b);
            b = Tuple.Create(4.8, 135.0);
            a.Add(b);
            b = Tuple.Create(12.0, 192.0);
            a.Add(b);

            return a;
        }

        //Grid tab

        public static double[,] Across(double[] Aspan)
        {
            // 1st row = Type: 1 abut, 2 pier, 3 cross
            // 2nd row = order of section, to set different color for different sections
            // 3rd row = length

            double[,] Across = new double[3, Aspan.GetLength(0)];
            for (int i = 0; i < Aspan.GetLength(0); i++)
            {
                Across[0, i] = 2.0;
                Across[1, i] = i + 1;
                Across[2, i] = Aspan[i];
            }
            Across[0, 0] = 1.0;

            return Across;
        }

        public static double[,] ANcross()
        {
            double[,] Across = new double[3, 1];
            Across[0, 0] = 1;
            Across[1, 0] = 1;
            Across[2, 0] = 5000;

            return Across;
        }

        public static double[,] Atran(int ngirder)
        {
            //1st row: BeamID: 1 -> 10, stringer 11, 21, 31
            //2nd row: to set color;
            //3rd row: length            

            double[,] Atran = new double[3, ngirder + 1];
            for (int i = 0; i < Atran.GetLength(1); i++)
            {
                if (i == 0 || i == Atran.GetLength(1) - 1)
                {
                    Atran[0, i] = 0;
                    Atran[1, i] = 0;
                }
                else
                {
                    Atran[0, i] = i;
                    Atran[1, i] = i;
                }
                Atran[2, i] = 1500;
            }
            return Atran;
        }

        public static double[,] ANtran()
        {

            double[,] Atran = new double[3, 2];
            Atran[0, 0] = 0;
            Atran[0, 1] = 0;
            Atran[1, 0] = 0;
            Atran[1, 1] = 0;
            Atran[2, 0] = 1500;
            Atran[2, 1] = 1500;

            return Atran;
        }

        public static double[,] Asection(int ngirder)
        {
            //1st = length;
            //2nd = ID: 1, 2
            double[,] Asection = new double[2, 3];
            Asection[0, 0] = 500;
            Asection[0, 1] = (ngirder + 1) * 1500 - 1000;
            Asection[0, 2] = 500;
            Asection[1, 0] = 1;
            Asection[1, 1] = 2;
            Asection[1, 2] = 1;
            return Asection;
        }

        public static double[,] ANsection()
        {
            //1st = length;
            //2nd = ID: 1, 2
            double[,] Asection = new double[2, 3];
            Asection[0, 0] = 500;
            Asection[0, 1] = 2000;
            Asection[0, 2] = 500;
            Asection[1, 0] = 1;
            Asection[1, 1] = 2;
            Asection[1, 2] = 1;
            return Asection;
        }
        public static List<string> NSupport()
        {
            //List<Support> NSupport = new List<Support>();
            //NSupport.Add(new Support(101, 0, 0, 0, "Fixed"));
            //NSupport.Add(new Support(102, 50000, 0, 0, "TranFixed"));
            //return NSupport;
            return new List<string>() { "Fixed", "TranFixed" };
        }


        public static List<Shoe> NShoe()
        {
            List<Shoe> Shoe = new List<Shoe>();
            Shoe.Add(new Shoe(1, 1, "Support #1", 1, 0, 0, new double[1] { 50000 }, new double[1] { 0.0 }));
            Shoe.Add(new Shoe(1, 2, "Support #2", 1, 0, 0, new double[1] { 50000 }, new double[1] { 0.0 }));

            return Shoe;
        }

        public static List<Shoe> Shoe(int ngirder, double[] Aspan, double[] Aspacing)
        {
            int span = Aspan.GetLength(0);
            List<Shoe> Shoe = new List<Shoe>();
            for (int i = 1; i <= ngirder; i ++)
            {               
                for (int j = 1; j <= span + 1; j++)
                    Shoe.Add(new Shoe(i, j, "Support #" + j.ToString(), 1, 0, 0, Aspan, Aspacing));
            }
                
            return Shoe;
        }



        public static List<string> savesupport
        { get; set; }

        //Haunch tab
        public static double[,] Ahaunch(double[] Aspan)
        {
            double[,] Ahaunch = new double[Aspan.GetLength(0) - 1, 6];

            if (Aspan.GetLength(0) > 1)
            {
                int pier = Aspan.GetLength(0) - 1;

                for (int i = 0; i < pier; i++)
                {
                    if (Aspan[i] > 17500 && Aspan[i + 1] > 17500)
                    {
                        Ahaunch[i, 0] = 15000;
                        Ahaunch[i, 1] = 5000;
                        Ahaunch[i, 2] = 15000;
                        Ahaunch[i, 3] = 2000;
                        Ahaunch[i, 4] = 2500;
                        Ahaunch[i, 5] = 2000;
                    }
                    else
                    {
                        Ahaunch[i, 0] = 0;
                        Ahaunch[i, 1] = 0;
                        Ahaunch[i, 2] = 0;
                        Ahaunch[i, 3] = 2000;
                        Ahaunch[i, 4] = 2000;
                        Ahaunch[i, 5] = 2000;
                    }
                }
            }
            return Ahaunch;
        }

        public static double[,] Acbox(double[] Aspan)
        {
            double[,] Acbox = new double[Aspan.GetLength(0) - 1, 2];

            if (Aspan.GetLength(0) > 1)
            {
                int pier = Aspan.GetLength(0) - 1;
                for (int i = 0; i < pier; i++)
                {
                    if (Aspan[i] > 20000 && Aspan[i + 1] > 20000)
                    {
                        Acbox[i, 0] = 5000;
                        Acbox[i, 1] = 5000;
                    }
                    else
                    {
                        Acbox[i, 0] = 0;
                        Acbox[i, 1] = 0;
                    }
                }
            }
            return Acbox;
        }



        public static double[,] Acon()
        {
            double[,] Acon = new double[10 + 2, 2];
            int pier = 10; // => Limit 10 pier
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

            return Acon;
        }

        //Dim tab
        public static double[,] Atop(double[,] Acbox, double[] Aspan)
        {
            if (Aspan.GetLength(0) > 1)
            {
                int numinsup = Acbox.GetLength(0);
                double[,] Atop2 = new double[5, 2 * numinsup + 1];

                for (int i = 0; i < Atop2.GetLength(1); i++)
                {
                    Atop2[0, i] = 1; //Set all ID = 1
                    Atop2[1, i] = i + 1; //Set order 1-2-3
                    Atop2[4, i] = 16;
                }

                Atop2[2, 0] = Aspan[0] - Acbox[0, 0];
                Atop2[3, 0] = 600;

                Atop2[2, 2 * numinsup] = Aspan[numinsup] - Acbox[numinsup - 1, 1];
                Atop2[3, 2 * numinsup] = 600;
                for (int i = 0; i < numinsup; i++)
                    Atop2[2, i * 2 + 1] = Acbox[i, 0] + Acbox[i, 1];
                for (int i = 0; i < numinsup - 1; i++)
                    Atop2[2, i * 2 + 2] = Aspan[i + 1] - Acbox[i, 1] - Acbox[i + 1, 0];

                return Atop2;
            }

            else
            {
                double[,] Atop2 = new double[5, 1];
                Atop2[0, 0] = 1;
                Atop2[1, 0] = 1;
                Atop2[2, 0] = Aspan.Sum();
                Atop2[3, 0] = 600;
                Atop2[4, 0] = 16;
                return Atop2;
            }

        }

        public static double[,] ANtop()
        {
            return new double[5, 1] { { 1 }, { 1 }, { 5000 }, { 600 }, { 16 } };
        }

        public static double[,] Abot(double[] Aspan)
        {
            return new double[2, 1] { { Aspan.Sum() }, { 16 } };
        }
        public static double[,] ANbot()
        {
            return new double[2, 1] { { 5000 }, { 16 } };
        }

        public static double[,] Aweb(double[] Aspan)
        {
            return new double[2, 1] { { Aspan.Sum() }, { 12 } };
        }

        public static double[,] ANweb()
        {
            return new double[2, 1] { { 5000 }, { 12 } };
        }

        public static double ts()
        { return 250; }

        public static double th()
        { return 0; }

        public static double bh()
        { return 0; }

        public static double drt()
        { return 16; }
        public static double art()
        { return 200; }
        public static double crt()
        { return 50; }
        public static double drb()
        { return 16; }
        public static double arb()
        { return 200; }
        public static double crb()
        { return 50; }

        public static double Sr()
        { return 0; }
        public static double Sd()
        { return 0; }
        public static double Ss()
        { return 0; }
        public static int Sindex()
        { return 0; }

        public static double w()
        { return 2000; }

        public static double D1()
        { return 2500; }

        public static double ctop()
        { return 200; }
        public static double cbot()
        { return 120; }

        //Stiff tab
        public static double[,] Aribtop(double[] Atop1)
        {
            double[,] b = new double[6, Atop1.GetLength(0)];
            for (int i = 0; i < Atop1.GetLength(0); i++)
            {
                b[0, i] = 1;
                b[1, i] = i + 1;
                b[2, i] = Atop1[i];
                if (i % 2 == 0)
                {
                    b[3, i] = 0;
                    b[4, i] = 0;
                    b[5, i] = 0;
                }
                else
                {
                    b[3, i] = 2;
                    b[4, i] = 160;
                    b[5, i] = 16;
                }
            }

            return b;
        }


        public static double[,] Aribbot(double[] Aspan)
        {
            return new double[4, 1] { { Aspan.Sum() }, { 2 }, { 160 }, { 16 } };
        }

        public static double[,] ANribbot()
        {
            return new double[4, 1] { { 5000 }, { 2 }, { 160 }, { 16 } };
        }


        public static double[,] Atranstif(double[,] Across)
        {
            double[,] b = new double[3, Across.GetLength(1)];
            for (int i = 0; i < Across.GetLength(1); i++)
            {
                b[0, i] = 1;
                b[1, i] = i + 1;
                b[2, i] = Across[2, i];
            }
            return b;
        }

        public static double[,] ANtranstif()
        {
            return new double[3, 1] { { 1 }, { 1 }, { 5000 } };
        }


        public static double ns()
        {
            return 2.0;
        }

        //Other tab
        public static List<Crossbeam> Crossbeam(double[,] Across, double[,] Atran)
        {
            List<Crossbeam> Crossbeam = new List<Crossbeam>();
            Crossbeam.Add(new Crossbeam("Stringer", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("Exterior-Support Crossbeam", 12, 300, 12, 300, 1400, 12, 1));
            Crossbeam.Add(new Crossbeam("General Crossbeam", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("Interior-Support Crossbeam", 0, 0, 0, 0, 0, 0, 0));

            for (int i = 0; i < Atran.GetLength(1); i++)
                if (Atran[0, i] > 10)
                {
                    Crossbeam[0] = new Crossbeam("Stringer", 12, 300, 12, 300, 1000, 10, 1);
                    break;
                }               

            double[] a = new double[Across.GetLength(1)];
            for (int i = 0; i < Across.GetLength(1); i++)
                a[i] = Across[0, i];
            List<double> type = a.ToList();
            if (type.IndexOf(11) != -1)
                Crossbeam[2] = new Crossbeam("General Crossbeam", 12, 300, 12, 300, 1000, 10, 1);           

            if (type.IndexOf(2) != -1)
                Crossbeam[3] = new Crossbeam("Interior-Support Crossbeam", 12, 300, 12, 300, 1800, 16, 1);
            

            return Crossbeam;
        }
        public static List<Crossbeam> NCrossbeam()
        {
            List<Crossbeam> Crossbeam = new List<Crossbeam>();
            Crossbeam.Add(new Crossbeam("Stringer", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("Exterior-Support Crossbeam", 12, 300, 12, 300, 1400, 12, 1));
            Crossbeam.Add(new Crossbeam("General Crossbeam", 0, 0, 0, 0, 0, 0, 0));
            Crossbeam.Add(new Crossbeam("Interior-Support Crossbeam", 0, 0, 0, 0, 0, 0, 0));

            return Crossbeam;
        }

        public static List<Parapet> Parapet(double[,] Asection)
        {
            List<Parapet> Parapet = new List<Parapet>();
            Parapet.Add(new Parapet("Left-side barrier", 970, 175, 205, 205, 70, 120, 10));
            Parapet.Add(new Parapet("Jersey barrier", 0, 0, 0, 0, 0, 0, 0));
            Parapet.Add(new Parapet("Right-side barrier", 970, 175, 205, 205, 70, 120, 10));
            
            for (int i = 1; i < Asection.GetLength(1) - 1; i++)
                if (Asection[1, i] == 1)
                {
                    Parapet[1] = new Parapet("Jersey barrier", 970, 175, 205, 205, 70, 120, 0);
                    break;
                }
            return Parapet;
        }
        public static List<Parapet> NParapet()
        {
            List<Parapet> Parapet = new List<Parapet>();
            Parapet.Add(new Parapet("Left-side barrier", 970, 175, 205, 205, 70, 120, 10));
            Parapet.Add(new Parapet("Jersey barrier", 0, 0, 0, 0, 0, 0, 0));
            Parapet.Add(new Parapet("Right-side barrier", 970, 175, 205, 205, 70, 120, 10));

            return Parapet;
        }

        public static List<KFrame> KFrame(double[,] Across, double[,] Atranstiff)
        {
            double[,] CAcross = Cumatrix(Across, 2);
            double[,] CAtranstiff = Cumatrix(Atranstiff, 2);
            List<KFrame> KFrame = new List<KFrame>();
            for (int i = 0; i < CAcross.GetLength(1); i++)
            {
                if (CAcross[0, i] == 1 || CAcross[0, i] == 0)
                    KFrame.Add(new KFrame(CAcross[2, i] / 1000, false, "Exterior Support"));
                if (CAcross[0, i] == 2)
                    KFrame.Add(new KFrame(CAcross[2, i] / 1000, false, "Interior Support"));
                if (CAcross[0, i] > 10)
                    KFrame.Add(new KFrame(CAcross[2, i] / 1000, false, "Crossbeam"));
            }

            List<double> Sta = KFrame.Select(p => p.Station).ToList();
            for (int i = 0; i < CAtranstiff.GetLength(1); i++)
            {
                if (Sta.IndexOf(CAtranstiff[2, i] / 1000) == -1)
                    KFrame.Add(new KFrame(CAtranstiff[2, i] / 1000, true, ""));
            }

            return KFrame.OrderBy(p => p.Station).ToList();
        }

        public static List<KFrame> NKFrame()
        {

            List<KFrame> KFrame = new List<KFrame>();
            KFrame.Add(new KFrame(0, false, "Exterior Support"));
            KFrame.Add(new KFrame(5, false, "Exterior Support"));
            return KFrame.OrderBy(p => p.Station).ToList();
        }

        //Tool for cumulative sum
        public static double[,] Cumatrix(double[,] a, int loc)
        {
            double[,] b = new double[a.GetLength(0), a.GetLength(1) + 1];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    b[i, j] = a[i, j];
            }

            b[loc, 0] = 0;
            for (int i = 1; i < a.GetLength(1) + 1; i++)
            {
                b[loc, i] = 0;
                for (int j = 0; j < i; j++)
                    b[loc, i] = a[loc, j] + b[loc, i];
            }
            return b;
        }

        // Analysis page

        public static List<int> Divindex()
        {
            return new List<int> { 1, 1 };
        }

        public static double numseg1()
        {
            return 5.0;
        }

        public static double numseg2()
        {
            return 2.5;
        }

        public static string savefolder
        { get; set; }

    }
}
