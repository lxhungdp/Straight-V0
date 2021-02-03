using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Provider
{
    public class datafromarray
    {
        private Results R;
        private string type;
        private double[,] ar;

        public datafromarray()
        {

        }

        public datafromarray(Results Results, double[,] ar, string type)
        {
            this.R = Results;
            this.type = type;
            this.ar = ar;
        }

        public Tuple<List<ElmForces>, List<ElmForces>, List<ElmForces>> Forces()
        {
            List<ElmForces> Moment = new List<ElmForces>(R.Moment);
            List<ElmForces> Shear = new List<ElmForces>(R.Shear);
            List<ElmForces> Torsion = new List<ElmForces>(R.Torsion);

            if (type == "steel")
            {
                for (int i = 0; i < Moment.Count; i++)
                {
                    ElmForces M = Moment[i].ShallowCopy();
                    ElmForces S = Shear[i].ShallowCopy();
                    ElmForces T = Torsion[i].ShallowCopy();

                    if (i % 2 == 0)
                    {
                        M.DC1 = ar[i / 2, 2];
                        M.DC2 = ar[i / 2, 6];                        
                    }
                    else
                    {
                        M.DC1 = ar[(i - 1) / 2, 3];
                        M.DC2 = ar[(i - 1) / 2, 7];                        
                    }

                    S.DC1 = ar[i / 2, 4];
                    S.DC2 = ar[i / 2, 8];
                    
                    T.DC1 = ar[i / 2, 5];
                    T.DC2 = ar[i / 2, 9];

                    Moment[i] = M;
                    Shear[i] = S;
                    Torsion[i] = T;
                }
            }

            else if (type == "deck")
            {
                for (int i = 0; i < Moment.Count; i++)
                {
                    ElmForces M = Moment[i].ShallowCopy();
                    ElmForces S = Shear[i].ShallowCopy();
                    ElmForces T = Torsion[i].ShallowCopy();
                    if (i % 2 == 0)
                    {
                        M.DC3 = ar[i / 2, 1];                        
                    }
                    else
                    {
                        M.DC3 = ar[(i - 1) / 2, 2];                        
                    }

                    S.DC3 = ar[i / 2, 3];
                    T.DC3 = ar[i / 2, 4];

                    Moment[i] = M;
                    Shear[i] = S;
                    Torsion[i] = T;
                }
            }

            else if (type == "barrier")
            {
                for (int i = 0; i < Moment.Count; i++)
                {
                    ElmForces M = Moment[i].ShallowCopy();
                    ElmForces S = Shear[i].ShallowCopy();
                    ElmForces T = Torsion[i].ShallowCopy();

                    if (i % 2 == 0)
                    {
                        M.DC4 = ar[i / 2, 2];
                        M.DW = ar[i / 2, 6];
                    }
                    else
                    {
                        M.DC4 = ar[(i - 1) / 2, 3];
                        M.DW = ar[(i - 1) / 2, 7];
                    }

                    S.DC4 = ar[i / 2, 4];
                    S.DW = ar[i / 2, 8];

                    T.DC4 = ar[i / 2, 5];
                    T.DW = ar[i / 2, 9];

                    Moment[i] = M;
                    Shear[i] = S;
                    Torsion[i] = T;
                }
            }

            else
            {
                for (int i = 0; i < Moment.Count; i++)
                {
                    ElmForces M = Moment[i].ShallowCopy();
                    ElmForces S = Shear[i].ShallowCopy();
                    ElmForces T = Torsion[i].ShallowCopy();

                    if (i % 2 == 0)
                    {
                        M.Truckmax = ar[i / 2, 8];
                        M.Truckmin = ar[i / 2, 10];
                        M.LLfmax = ar[i / 2, 16] * 0.8 * 1.15;
                        M.LLfmin = ar[i / 2, 18] * 0.8 * 1.15;
                        M.Lanemax = ar[i / 2, 24];
                        M.Lanemin = ar[i / 2, 26];
                        M.PLmax = ar[i / 2, 32];
                        M.PLmin = ar[i / 2, 34];
                    }
                    else
                    {
                        M.Truckmax = ar[(i - 1) / 2, 9];
                        M.Truckmin = ar[(i - 1) / 2, 11];
                        M.LLfmax = ar[(i - 1) / 2, 17] * 0.8 * 1.15;
                        M.LLfmin = ar[(i - 1) / 2, 19] * 0.8 * 1.15;
                        M.Lanemax = ar[(i - 1) / 2, 25];
                        M.Lanemin = ar[(i - 1) / 2, 27];
                        M.PLmax = ar[(i - 1) / 2, 33];
                        M.PLmin = ar[(i - 1) / 2, 35];
                    }
                    
                    S.Truckmax = ar[i / 2, 12];
                    S.Truckmin = ar[i / 2, 13];
                    S.LLfmax = ar[i / 2, 20] * 0.8 * 1.15;
                    S.LLfmin = ar[i / 2, 21] * 0.8 * 1.15;
                    S.Lanemax = ar[i / 2, 28];
                    S.Lanemin = ar[i / 2, 29];
                    S.PLmax = ar[i / 2, 36];
                    S.PLmin = ar[i / 2, 37];
                    
                    T.Truckmax = ar[i / 2, 14];
                    T.Truckmin = ar[i / 2, 15];
                    T.LLfmax = ar[i / 2, 22] * 0.8 * 1.15;
                    T.LLfmin = ar[i / 2, 23] * 0.8 * 1.15;
                    T.Lanemax = ar[i / 2, 30];
                    T.Lanemin = ar[i / 2, 31];
                    T.PLmax = ar[i / 2, 38];
                    T.PLmin = ar[i / 2, 39];

                    Moment[i] = M;
                    Shear[i] = S;
                    Torsion[i] = T;
                }
            }
            return Tuple.Create(Moment, Shear, Torsion);
        }

        public List<NodeForces> Deflection()
        {
            List<NodeForces> Deflection = new List<NodeForces>(R.Deflection);

            if (type == "steel")
            {
                for (int i = 0; i < Deflection.Count; i++)
                {
                    Deflection[i].DC1 = ar[i, 0];
                    Deflection[i].DC2 = ar[i, 1];
                }
            }

            else if (type == "deck")
            {
                for (int i = 0; i < Deflection.Count; i++)
                {
                    Deflection[i].DC3 = ar[i, 0];
                }
            }

            else if (type == "barrier")
            {
                for (int i = 0; i < Deflection.Count; i++)
                {
                    Deflection[i].DC4 = ar[i, 0];
                    Deflection[i].DW = ar[i, 1];
                }
            }

            else
            {
                for (int i = 0; i < Deflection.Count; i++)
                {
                    Deflection[i].Truckmax = ar[i, 0];
                    Deflection[i].Truckmin = ar[i, 1];
                    Deflection[i].LLfmax = ar[i, 2];
                    Deflection[i].LLfmin = ar[i, 3];
                    Deflection[i].Lanemax = ar[i, 4];
                    Deflection[i].Lanemin = ar[i, 5];
                    Deflection[i].PLmax = ar[i, 6];
                    Deflection[i].PLmin = ar[i, 7];
                }
            }           

            return Deflection;
        }

        public List<NodeForces> Reaction()
        {
            List<NodeForces> Reaction = new List<NodeForces>(R.Reaction);

            if (type == "steel")
            {
                for (int i = 0; i < Reaction.Count; i++)
                {
                    Reaction[i].DC1 = ar[i, 10];
                    Reaction[i].DC2 = ar[i, 11];
                }
            }

            else if (type == "deck")
            {
                for (int i = 0; i < Reaction.Count; i++)
                {
                    Reaction[i].DC3 = ar[i, 5];
                }
            }

            else if (type == "barrier")
            {
                for (int i = 0; i < Reaction.Count; i++)
                {
                    Reaction[i].DC4 = ar[i, 10];
                    Reaction[i].DW = ar[i, 11];
                }
            }

            else
            {
                for (int i = 0; i < Reaction.Count; i++)
                {
                    Reaction[i].Truckmax = ar[i, 40];
                    Reaction[i].Truckmin = ar[i, 41];
                    Reaction[i].LLfmax = ar[i, 42];
                    Reaction[i].LLfmin = ar[i, 43];
                    Reaction[i].Lanemax = ar[i, 44];
                    Reaction[i].Lanemin = ar[i, 45];
                    Reaction[i].PLmax = ar[i, 46];
                    Reaction[i].PLmin = ar[i, 47];
                }
            }

            return Reaction;
        }

        public Results Results()
        {
            Results R = new Results();
            R.Moment = new List<ElmForces>(Forces().Item1);
            R.Shear = new List<ElmForces>(Forces().Item2);
            R.Torsion = new List<ElmForces>(Forces().Item3);
            R.Deflection = new List<NodeForces>(Deflection());
            R.Reaction = new List<NodeForces>(Reaction());
            return R;
        }
    }
}
