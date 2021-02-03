using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Liveload
    {
        double Lanemax = 3600;
        double Lanemin = 3000;
        public Liveload( double[,] Asection, double[,] Atran, double R, int Trucktype, int Truckgrade, double Laneload, List<Tuple<double, double>> Truckaxle, List<double> Lanefactor, double Pload)
        {
            this.Asection = Asection;
            this.Atran = Atran;
            this.Trucktype = Trucktype;
            this.Truckgrade = Truckgrade;
            this.Laneload = Laneload;
            this.Truckaxle = new List<Tuple<double, double>>(Truckaxle);
            this.Lanefactor = new List<double>(Lanefactor);
            this.Pload = Pload;
        }

        public double[,] Asection
        {
            get; set;
        }

        public double[,] Atran
        {
            get; set;
        }

        public int Trucktype
        { get; set; }

        public int Truckgrade
        { get; set; }

        public double Laneload
        { get; set; }

        public List<Tuple<double, double>> Truckaxle
        { get; set; }

        public List<double> Lanefactor
        { get; set; }

        public double Pload
        { get; set; }


        //Curved radius
        public double R
        { get; set; }



       public List<Tuple<double, double>> GetLaneInfo(double [,] Asection, double[,] Atran, int ID)
        {
            List<Tuple<double, double>> Lane = new List<Tuple<double, double>>();

            for (int i = 0; i < Asection.GetLength(1); i++)
            {
                if (Asection[1, i] == ID)
                {
                    //Width of lane for live load
                    double B = Asection[0, i];

                    //Distance from left edge of lane to left edge of deck, then to 1st girder
                    double e = 0;
                    for (int j = 0; j < i; j++)
                        e += Asection[0, j];
                    e = e - Atran[2, 0];

                    Lane.Add(Tuple.Create(B, e));
                }
            }
            return Lane;
        }

        public List<Tuple<double, double>> LLLane()
        {
            return GetLaneInfo(Asection, Atran, 2);
        }

        public List<Tuple<double, double>> PLane()
        {
            return GetLaneInfo(Asection, Atran, 3);
        }

        public Tuple<List<double>,List<double>, List<double>> nlane()
        {
            //Width of lane in left side
            double Bleft = LLLane()[0].Item1;
            //Width of lane in right side
            double Bright = LLLane().Count == 1 ? 0 : LLLane()[1].Item1;

            //From 1st girder to edge of left lane
            double Eleft = LLLane()[0].Item2;
            //From 1st girder to edge of right lane
            double Eright = LLLane().Count == 1 ? 0 : LLLane()[1].Item2;

            //Number lane in left side
            int nleft = Convert.ToInt32(Math.Floor(Bleft / Lanemin));
            //Number lane in right side
            int nright = Convert.ToInt32(Math.Floor(Bright / Lanemin));

            //Width of left lane
            double Wleft = Bleft / nleft >= Lanemax ? Lanemax : Bleft / nleft;
            //Width of left right
            double Wright = nright == 0? 0 : (Bright / nright >= Lanemax ? Lanemax : Bright / nright);

            //Distance from center of i-lane to 1st girder for the left side
            List<double> Dleft = new List<double>();
            for (int i = 1; i <= nleft; i++)            
                Dleft.Add(Eleft + Lanemin / 2 + (i - 1) * (R == 0 ? Wleft : Lanemin));
            for (int i = 1; i <= nright; i++)
                Dleft.Add(Eright + Lanemin / 2 + (i - 1) * (R == 0 ? Wright : Lanemin));

            //Distance from center of i-lane to 1st girder for the right side
            List<double> Dright = new List<double>();
            for (int i = 1; i <= nright; i++)
                Dright.Add(Eright + Bright - Lanemin / 2 - (i - 1) * (R == 0 ? Wright : Lanemin));
            for (int i = 1; i <= nleft; i++)
                Dright.Add(Eleft + Bleft - Lanemin / 2 - (i - 1) * (R == 0 ? Wleft : Lanemin));

            //Distance from center of i-lane to 1st girder for the Middle
            List<double> Dmid = new List<double>();

            if (nright == 0)
            {
                for (int i = 1; i <= nleft; i++)
                {                    
                    if (i % 2 != 0)
                    {
                        Dmid.Add(Eleft + Bleft / 2);
                        for (int j = 2; j <= i; j++)
                            Dmid.Add(Eleft + Bleft / 2 + (j % 2 == 0 ? -1 : 1) * (j - (j % 2 == 0 ? 1 : 2)) / 2.0 * (R == 0 ? Wleft : Lanemin) + (j % 2 == 0 ? -1 : 1) * Lanemin / 2);
                    }
                    else
                    {                       
                        for (int j = 1; j <= i; j ++)
                            Dmid.Add(Eleft + Bleft / 2 + (j % 2 == 0 ? 1 : -1) * Lanemin / 2 + (j % 2 == 0 ? 1 : -1) * (j - (j % 2 == 0 ? 2 : 1)) / 2.0 * (R == 0 ? Wleft : Lanemin) );
                    }                    
                }
            }






            //Do later;
            //else
            //{               
            //    for (int i = 1; i <= nleft + nright; i++)
            //    {
                    
                    
                    
            //        for (int j = 1; j <=i; j ++)
            //        {
            //            int ileft = 1;
            //            while (ileft <= nleft)
            //            {
            //                Dmid.Add(Eleft + Bleft - 1500 - (ileft - 1) * (R == 0 ? Wleft : 3000));
            //                ileft = ileft + 1;
            //            }

            //            int iright = 1;
            //            while (iright <= nright)
            //            {
            //                Dmid.Add(Eright + 1500 + (iright - 1) * (R == 0 ? Wright : 3000));
            //                iright = iright + 1;
            //            }

            //        }
            //    }
            //}




            return Tuple.Create(Dleft, Dright, Dmid);
        }


        public List<Tuple<double, double>> Axlebyfactor()
        {
            double factor = 0;
            if (Trucktype == 0)
            {
                if (Truckgrade == 0)
                    factor = 1;
                else if (Truckgrade == 1)
                    factor = 0.75;
                else if (Truckgrade == 2)
                    factor = 0.75 * 0.75;
            }
            else
            {
                // Do for DB24 and HL93
            }

            List<Tuple<double, double>> a = new List<Tuple<double, double>>();
            for (int i = 0; i < Truckaxle.Count; i ++)
            {
                a.Add(Tuple.Create(Truckaxle[i].Item1, Truckaxle[i].Item2 * factor));
            }

            return a;
        }




    }
}
