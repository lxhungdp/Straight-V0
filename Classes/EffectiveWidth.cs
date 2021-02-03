using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class EffectiveWidth
    {       
       
        public EffectiveWidth(NodeInput Node, double[] Aspan, double[] Atran)
        {
            this.Node = Node;
            this.Aspan = Aspan;
            this.Atran = Atran;
        }

        public NodeInput Node
        { get; set; }

        //
        public double[] Aspan
        { get; set; }
        
        public double[] Atran
        { get; set; }
           
        public double bl
        {
            get
            {
                if (Node.BeamID > 10)
                    return 0;               
                else if (Node.BeamID == 1)
                    return Atran[0] - Node.w / 2;
                else
                    return  (Atran[Node.BeamID - 1] - Node.w) / 2; 
            }
        }

        public double br
        {
            get
            {
                if (Node.BeamID > 10)
                    return 0;
                else if (Node.BeamID == Atran.GetLength(0) - 2)
                    return Atran[Atran.GetLength(0) - 2] - Node.w / 2;
                else
                    return (Atran[Node.BeamID] - Node.w) / 2;               
            }
        }

        //Eccentricity
        public double e
        {
            get
            {
                return (bl + Node.w + br) / 2 - bl - Node.w / 2; 
            }
        }

        public double Leff
        { 
            get
            {
                double Leff;
                int span = Aspan.GetLength(0);

                // Determine the cumulate of span
                double c1 = 0;
                List<double> c = Aspan.Select(p => c1 += p).ToList();
                c.Add(0);
                c.Sort();

                int index;
                if (Node.X == c[span])
                    index = span - 1;
                else
                    index = c.FindLastIndex(p => p <= Node.X);

                //Determine Leff
                if (span == 1)
                    Leff = Aspan[0];
                else
                {
                    if (index == 0)
                    {
                        if (Node.X <= 0.8 * Aspan[0])
                            Leff = 0.8 * Aspan[0];
                        else
                            Leff = 0.2 * Aspan[0] + 0.2 * Aspan[1];
                    }

                    else if (index == span - 1)
                    {
                        if (Node.X > c[span] - 0.8 * Aspan[span - 1])
                            Leff = 0.8 * Aspan[span - 1];
                        else
                            Leff = 0.2 * Aspan[span - 2] + 0.2 * Aspan[span - 1];
                    }
                    else
                    {
                        if (Node.X <= c[index] + 0.2 * Aspan[index])
                            Leff = 0.2 * Aspan[index - 1] + 0.2 * Aspan[index];
                        else if (Node.X <= c[index] + 0.8 * Aspan[index])
                            Leff = 0.6 * Aspan[index];
                        else
                            Leff = 0.2 * Aspan[index] + 0.2 * Aspan[index + 1];
                    }
                }
                return Leff;
               
            } 
        }


        public double beff
        {
            get
            {
                if (Node.ntop == 2)
                    return Math.Min(Math.Min(0.5 * Leff, 24 * Node.ts + Node.btop), bl + Node.w + br);
                else
                    return Math.Min(Math.Min(0.25 * Leff, 12 * Node.ts + Node.ctop), bl + br) + Node.w;
            }
        }

        public double bleft
        {
            get
            {
                if (Node.BeamID == 1)
                    return Atran[0];
                else
                    return 0;
            }
        }

        public double bright
        {
            get
            {
                if (Node.BeamID == Atran.GetLength(0) - 2)
                    return Atran[Atran.GetLength(0) - 2];
                else
                    return 0;
            }
        }

        public double aleft
        {
            get
            {
                if (Atran.GetLength(0) - 2 == 1)
                    return 0;
                else
                {
                    if (Node.BeamID == 1 || Node.BeamID > 10)
                        return 0;
                    else
                        return Atran[Node.BeamID - 1];
                } 
            }
        }

        public double aright
        {
            get
            {
                if (Atran.GetLength(0) - 2 == 1)
                    return 0;
                else
                {
                    if (Node.BeamID == Atran.GetLength(0) - 2 || Node.BeamID > 10)
                        return 0;
                    else
                        return Atran[Node.BeamID];
                }
            }
        }

       
    }
}
