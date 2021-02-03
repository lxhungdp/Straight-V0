using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Results
    {
        public Results()
        {

        }

        public List<Mat> Mat
        { get; set; }

        public double Pforms
        { get; set; }
        public double ADTT
        { get; set; }

        public List<Node> Node
        { get; set; }
        public List<Sec> Sec
        { get; set; }

        public List<ElmForces> Moment
        { get; set; }

        public List<ElmForces> Shear
        { get; set; }

        public List<ElmForces> Torsion
        { get; set; }

        public List<NodeForces> Deflection
        { get; set; }

        public List<NodeForces> Reaction
        { get; set; }

        public List<Node> Node2
        {
            get
            {
                int nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
                int nmain = Node.Where(p => p.BeamID <= 10).ToList().Count / nlong;
                List<Node> Node2 = new List<Node>();
                for (int i = 0; i < nmain; i++)
                    for (int j = 0; j < nlong - 1; j++)
                    {
                        Node2.Add(Node[i * nlong + j]);
                        Node2.Add(Node[i * nlong + j]);
                    }

                return Node2;
            }
        }

        public List<Stress> Stress
        {
            get
            {
                List<Stress> Stress = new List<Stress>();
                for (int i = 0; i < Moment.Count; i++)
                    Stress.Add(new Stress(Sec[i], Moment[i]));

                return Stress;
            }
        }

        public List<Check_Cons> Check_Cons
        {
            get
            {
                List<Check_Cons> Check_Cons = new List<Check_Cons>();
                for (int i = 0; i < Moment.Count; i++)
                    Check_Cons.Add(new Check_Cons(Node2[i], Sec[i], Stress[i], Moment[i], Torsion[i], Shear[i], Mat[0], Mat[1], Pforms));

                return Check_Cons;
            }
        }

        public List<Check_ULS> Check_ULS
        {
            get
            {
                List<Mat> Mat1 = new List<Mat> { Mat[0], Mat[1], Mat[3], Mat[6], Mat[7], Mat[8] };
                List<Check_ULS> Check_ULS = new List<Check_ULS>();
                for (int i = 0; i < Moment.Count; i++)
                    Check_ULS.Add(new Check_ULS(Node2[i], Sec[i], Stress[i], Moment[i], Torsion[i], Shear[i], Mat1));

                return Check_ULS;
            }
        }

        public List<Check_SLS> Check_SLS
        {
            get
            {
                List<Mat> Mat1 = new List<Mat> { Mat[0], Mat[1], Mat[8] };
                List<Check_SLS> Check_SLS = new List<Check_SLS>();
                for (int i = 0; i < Moment.Count; i++)
                    Check_SLS.Add(new Check_SLS(Node2[i], Sec[i], Stress[i], Moment[i], Mat1));

                return Check_SLS;
            }
        }

        public List<Check_FLS> Check_FLS
        {
            get
            {
                List<Check_FLS> Check_FLS = new List<Check_FLS>();
                for (int i = 0; i < Moment.Count; i++)
                    Check_FLS.Add(new Check_FLS(Node2[i], Sec[i], Stress[i], Shear[i], Mat[1], ADTT));

                return Check_FLS;
            }
        }
    }
}
