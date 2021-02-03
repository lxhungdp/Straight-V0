using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using solver;

namespace Provider
{
    public class Solving
    {
        public Solving()
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


        public List<Elm> Elm
        { get; set; }

        public ProgressBar ProBar
        { get; set; }

        public Label LabelStatus
        { get; set; }

        //Building Result structure

        public Results R1st
        {
            get
            {
                Results R = new Results();
                List<NodeForces> Deflection = new List<NodeForces>();
                List<NodeForces> Reaction = new List<NodeForces>();
                List<ElmForces> Moment = new List<ElmForces>();

                List<Node> NodeD = Node.Where(p => p.BeamID <= 10).ToList();
                for (int i = 0; i < NodeD.Count; i++)
                {
                    NodeForces D1 = new NodeForces();
                    D1.Node = NodeD[i].Joint;
                    D1.Station = NodeD[i].X;
                    string Des = NodeD[i].Label;
                    D1.Description = (Des.Contains("Support") || Des.Contains("Cross")) ? Des : "";
                    Deflection.Add(D1);
                }

                //List<Nodeex> NodeR = Node.Where(p => p.Restrain != null && p.Restrain.Contains('e') && p.BeamID <= 10).ToList();
                for (int i = 0; i < Shoe.Count; i++)
                {
                    NodeForces D1 = new NodeForces();
                    D1.Node = Shoe[i].Joint;
                    D1.Description = Shoe[i].Joint1;
                    D1.Station = Shoe[i].Station;
                    Reaction.Add(D1);

                    if (Shoe[i].EA == 2)
                    {
                        NodeForces D2 = new NodeForces();
                        D2.Node = Shoe[i].Joint;
                        D2.Description = Shoe[i].Joint2;
                        D2.Station = Shoe[i].Station;
                        Reaction.Add(D2);
                    }

                }

                List<Elm> ElmG = Elm.Where(p => p.Name[0] == 'G').ToList();
                for (int i = 0; i < ElmG.Count; i++)
                {
                    ElmForces mst1 = new ElmForces();
                    mst1.Element = ElmG[i].Name;
                    mst1.Node = ElmG[i].iNode;
                    mst1.Station = ElmG[i].iStation;
                    string Des = Node.Where(p => p.Joint == ElmG[i].iNode).FirstOrDefault().Label;
                    mst1.Description = (Des.Contains("Support") || Des.Contains("Cross")) ? Des : "";
                    Moment.Add(mst1);

                    ElmForces mst2 = new ElmForces();
                    mst2.Element = ElmG[i].Name;
                    mst2.Node = ElmG[i].jNode;
                    mst2.Station = ElmG[i].jStation;
                    Des = Node.Where(p => p.Joint == ElmG[i].jNode).FirstOrDefault().Label;
                    mst2.Description = (Des.Contains("Support") || Des.Contains("Cross")) ? Des : "";
                    Moment.Add(mst2);
                }

                R.Moment = new List<ElmForces>(Moment);
                R.Shear = new List<ElmForces>(Moment);
                R.Torsion = new List<ElmForces>(Moment);
                R.Deflection = new List<NodeForces>(Deflection);
                R.Reaction = new List<NodeForces>(Reaction);

                return R;
            }
        }

        public Results Results()
        {
            Solver Solver = new Solver();
            Solver.Node = new List<Node>(Node);
            Solver.Sec = new List<Sec>(Sec);
            Solver.Shoe = new List<Shoe>(Shoe);
            Solver.Crossbeam = new List<Crossbeam>(Crossbeam);
            Solver.Mat = new List<Mat>(Mat);
            Solver.Overloading = Overloading;
            Solver.Parapet = new List<Parapet>(Parapet);
            Solver.eParapet = new List<double>(eParapet);
            Solver.Asphalt = new List<double>(Asphalt);
            Solver.Liveloadinput = Liveloadinput;
            Solver.LLoad = LLoad;
            Solver.Lanefactor = new List<double>(Lanefactor);
            Solver.Pload = Pload;
            Solver.delta = delta;

            ProBar.Value = 30;
            LabelStatus.Text = "Solving for steel ...";
            LabelStatus.Update();
            datafromarray data1 = new datafromarray(R1st, Solver.Steel, "steel");

            ProBar.Value = 45;
            LabelStatus.Text = "Solving for deck ...";
            LabelStatus.Update();
            data1 = new datafromarray(data1.Results(), Solver.Deck, "deck");

            ProBar.Value = 60;
            LabelStatus.Text = "Solving for Barrier ...";
            LabelStatus.Update();
            data1 = new datafromarray(data1.Results(), Solver.Barrier, "barrier");

            ProBar.Value = 75;
            LabelStatus.Text = "Solving for Liveload ...";
            LabelStatus.Update();
            data1 = new datafromarray(data1.Results(), Solver.Liveload, "liveload");

            return data1.Results();
        }

        public Solver Solver()
        {
            Solver Solver = new Solver();
            Solver.Node = new List<Node>(Node);
            Solver.Sec = new List<Sec>(Sec);
            Solver.Shoe = new List<Shoe>(Shoe);
            Solver.Crossbeam = new List<Crossbeam>(Crossbeam);
            Solver.Mat = new List<Mat>(Mat);
            Solver.Overloading = Overloading;
            Solver.Parapet = new List<Parapet>(Parapet);
            Solver.eParapet = new List<double>(eParapet);
            Solver.Asphalt = new List<double>(Asphalt);
            Solver.Liveloadinput = Liveloadinput;
            Solver.LLoad = LLoad;
            Solver.Lanefactor = new List<double>(Lanefactor);
            Solver.Pload = Pload;
            Solver.delta = delta;

            

            return Solver;
        }

    }
}
