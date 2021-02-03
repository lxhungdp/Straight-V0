using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using Provider;

namespace Mainform
{
    public class Analysis : IDisposable
    {

        private List<string> stage = new List<string>() { "self", "bottom", "deck", "bar", "liveload" };
        private int Nseg = 1;

        public Analysis()
        {
            
        }

        bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.Input = null;                    
                    this.Node3 = null;
                    this.Solving = null;
                }
            }
            
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Input Input
        { get; set; }

        public string path
        { get; set; }

        public string Method
        { get; set; }

        public ProgressBar ProBar
        { get; set; }

        public Label LabelStatus
        { get; set; }

        //Calculate


        public int nspan()
        {
            return Input.Aspan().GetLength(0);
        }


        // Create Node;
        public List<NodeInput> Node1
        {
            get
           { 
                List<NodeInput> Nodet = new List<NodeInput>();
                //ProBar.Value = 90;
                //Add Haunch, Closed box section and bottom concrete
                if (nspan() > 1)
                {
                    double[,] AddHaunch = Cumatrix(Haunch1().Harray, 0);
                    Nodet = Addnode(Input.Node(), AddHaunch, "Haunch", 4, "btop");
                }

                if (nspan() > 1)
                {
                    double[,] AddClosedbox = Cumatrix(Provider.Haunch.Closedbox(Input.Aspan(), Input.Acbox), 0);
                    Nodet = Addnode(Nodet, AddClosedbox, "ntop", 4, "Haunch");
                }

                if (nspan() > 1)
                {
                    double[,] AddBottomcon = Cumatrix(Provider.Haunch.Bottomcon(Input.Aspan(), Input.Acon), 0);
                    Nodet = Addnode(Nodet, AddBottomcon, "Hc", 4, "Haunch,ntop");
                }

                //Add Top, bottom flange and web to PUS
                double[,] Addtop = Cumatrix(Input.Atop, 2);
                Nodet = Addnode(Nodet, Addtop, "btop,ttop", 5, "Haunch,ntop,Hc");

                double[,] Addweb = Cumatrix(Input.Aweb, 0);
                Nodet = Addnode(Nodet, Addweb, "tw", 5, "Haunch,ntop,Hc,btop,ttop");

                double[,] Addbot = Cumatrix(Input.Abot, 0);
                Nodet = Addnode(Nodet, Addbot, "tbot", 5, "Haunch,ntop,Hc,btop,ttop,tw");

                //Add other dims
                double[] Odims = new double[] { Input.ts, Input.th, Input.bh, Input.drt, Input.art, Input.crt, Input.drb, Input.arb, Input.crb, Input.S, Input.w, Input.D1, Input.ctop, Input.cbot };
                Nodet = Add1prop(Nodet, Odims, "ts,th,bh,drt,art,crt,drb,arb,crb,S,w,D,ctop,cbot");

                //Add Rib            
                double[,] Addribtop = Cumatrix(Input.Aribtop, 2);
                Nodet = Addnode(Nodet, Addribtop, "nst,Hst,tst", 6, "Haunch,ntop,Hc,btop,ttop,tw,ts,th,bh,drt,art,crt,drb,arb,crb,S,w,D,ctop,cbot");

                double[,] Addribbot = Cumatrix(Input.Aribbot, 0);
                Nodet = Addnode(Nodet, Addribbot, "nsb,Hsb,tsb", 6, "Haunch,ntop,Hc,btop,ttop,tw,ts,th,bh,drt,art,crt,drb,arb,crb,S,w,D,ctop,cbot,nst,Hst,tst");

                double[] Ans = new double[] { Input.ns };
                Nodet = Add1prop(Nodet, Ans, "ns");

                Nodet = Addd0(Nodet, Input.Atranstiff, "d0");



                return Nodet;

            }

            
           
        }

        public List<NodeInput> Node2
        {
           get
            {
                //Add Kframe and Lb
                
                List<NodeInput> Node2 = new List<NodeInput>(AddKframe(Node1, Input.KFrame));


                //Remove the node which is unselected
                List<int> Divindex = new List<int>();
                if (Input.Divindex[0] == 0)
                    Divindex.AddRange(new List<int> { 5, 6 });
                if (Input.Divindex[1] == 0)
                    Divindex.AddRange(new List<int> { 7 });

                for (int i = 0; i < Divindex.Count; i++)
                    Node2 = Node2.Where(p => p.Type != Divindex[i]).ToList();

                //Divide element by condition
                Node2 = Selection(Node2, Input.numseg1, Input.numseg2);

                return Node2;
            }
            
        }

        public List<Node> Node3
        {
            get
            {
                List<Node> Nodeex = new List<Node>();

                for (int i = 0; i < Node2.Count; i++)
                    Nodeex.Add(new Node(Node2[i], Haunch2().Dw[i], nspan(), new EffectiveWidth(Node2[i], Input.Aspan(), Input.Aspacing())));
                return Nodeex;
            }

            set
            {

            }
            
        }


        //Create element ... from Node

        public List<Elm> Elm
        {
            get
            {
                List<Node> Node = new List<Node>(Node3);
                List<Elm> a = new List<Elm>();

                int nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
                int ntran = Node.Count / nlong;
                int nmain = Node.Where(p => p.BeamID <= 10).ToList().Count / nlong;
                int nstringer = ntran - nmain;

                List<Node> stringer = Node.Where(p => p.BeamID > 10).ToList();

                //Main beam and stringer
                for (int i = 0; i < ntran; i++)
                    for (int j = 0; j < nlong - 1; j++)
                    {
                        Elm b = new Elm();
                        b.iNode = Node[i * nlong + j].Joint;
                        b.iStation = Node[i * nlong + j].X;
                        b.jNode = Node[i * nlong + j + 1].Joint;
                        b.jStation = Node[i * nlong + j + 1].X;

                        if (Node[i * nlong].BeamID <= 10)
                        {
                            b.Name = "G" + ((i + 1) * 100 + (j + 1)).ToString();
                            b.Sec = i * (nlong - 1) + j + 1;
                        }
                        else
                        {
                            b.Name = "S" + ((i + 1) * 100 + (j + 1)).ToString();
                            b.Sec = (nlong - 1) * nmain + 1;
                        }
                        b.Length = Node[i * nlong + j + 1].X - Node[i * nlong + j].X;
                        b.Nseg = Nseg;
                        a.Add(b);
                    }


                //Cross beam
                int index;
                for (int i = 0; i < ntran - 1; i++)
                {
                    index = (ntran + i + 1) * 100 + 1;
                    for (int j = 0; j < nlong; j++)
                    {

                        if (Node[i * nlong + j].Type == 1)
                        {
                            Elm b = new Elm();
                            b.Name = "C" + index.ToString();
                            b.iNode = Node[i * nlong + j].Joint;
                            b.iStation = Node[i * nlong + j].X;
                            b.jNode = Node[(i + 1) * nlong + j].Joint;
                            b.jStation = Node[(i + 1) * nlong + j].X;
                            b.Sec = (nlong - 1) * nmain + 2;
                            b.Nseg = Nseg;
                            b.Length = Node[(i + 1) * nlong + j].Y - Node[i * nlong + j].Y;
                            a.Add(b);
                        }

                        else if (Node[i * nlong + j].Type == 3)
                        {
                            Elm b = new Elm();
                            b.Name = "C" + index.ToString();
                            b.iNode = Node[i * nlong + j].Joint;
                            b.iStation = Node[i * nlong + j].X;
                            b.jNode = Node[(i + 1) * nlong + j].Joint;
                            b.jStation = Node[(i + 1) * nlong + j].X;
                            b.Sec = (nlong - 1) * nmain + 3;
                            b.Nseg = Nseg;
                            b.Length = Node[(i + 1) * nlong + j].Y - Node[i * nlong + j].Y;
                            a.Add(b);
                        }
                        else if (Node[i * nlong + j].Type == 2)
                        {
                            Elm b = new Elm();
                            b.Name = "C" + index.ToString();
                            b.iNode = Node[i * nlong + j].Joint;
                            b.iStation = Node[i * nlong + j].X;
                            b.jNode = Node[(i + 1) * nlong + j].Joint;
                            b.jStation = Node[(i + 1) * nlong + j].X;
                            b.Sec = (nlong - 1) * nmain + 4;
                            b.Nseg = Nseg;
                            b.Length = Node[(i + 1) * nlong + j].Y - Node[i * nlong + j].Y;
                            a.Add(b);
                        }
                        index = index + 1;
                    }
                }

                return a;
            }
        }



        public List<Sec> Sec
        {
            get
            {
                double nEb = Input.Matuse[0].Es / Input.Matuse[7].Ec;
                double nEd = Input.Matuse[0].Es / Input.Matuse[6].Ec;

                List<Node> Node = new List<Node>(Node3);
                int nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
                int nmain = Node.Where(p => p.BeamID <= 10).ToList().Count / nlong;
                List<Sec> Sec = new List<Sec>();
                for (int i = 0; i < nmain; i++)
                    for (int j = 0; j < nlong - 1; j++)
                    {
                        Sec.Add(new Sec(Node[i * nlong + j], nEb, nEd, 0, 0));
                        Sec.Add(new Sec(Node[i * nlong + j], nEb, nEd, 1, Node[i * nlong + j + 1].X));
                    }
                        

                return Sec;
            }

        }


        



        public List<Shoe> Shoe
        {
            get
            {
                //var watch = System.Diagnostics.Stopwatch.StartNew();
                List<Node> Nodesupport = Node3.Where(p => p.Restrain != "" && p.Restrain != null).ToList();
                List<Shoe> Shoe = new List<Shoe>(Input.Shoe);
                for (int i = 0; i < Shoe.Count; i++)
                {
                    Shoe[i].Joint = Nodesupport[i].Joint;
                    Shoe[i].Station = Nodesupport[i].X;
                    Shoe[i].D = Nodesupport[i].D;
                }
                //watch.Stop();
                //MessageBox.Show(watch.ElapsedMilliseconds.ToString());
                return Shoe;
            }
        }

        public Liveload Liveload
        {
            get
            {
                Liveload Liveload = new Liveload(Input.Asection, Input.Atran, 0, Input.Tructype, Input.Truckgrade, Input.Laneload, Input.Truckaxle, Input.Lanefactor, Input.Pload); //Assumed straight bridge R = 0;
                return Liveload;
            }
        }



        //Running

        public s2k Running()
        {
            double Overloading = Input.Matuse[0].Ws * Input.Overloading;
            double Es = Input.Matuse[0].Es;
            double Wcon = Input.Matuse[7].Wc;
            double Wdeck = Input.Matuse[6].Wc;
            double Vparapet = Input.Parapet.Sum(p => p.Area()) * Wdeck / Math.Pow(10, 6);
            double Mparapet = (Input.Parapet[0].Area() * Input.eParapet()[0] + Input.Parapet[1].Area() * Input.eParapet()[1] + Input.Parapet[2].Area() * Input.eParapet()[2]) * Wdeck / Math.Pow(10, 9);


            s2k s2k = new s2k();
            s2k.path = Const.Sap2000;
            s2k.filename = new List<string> { "SELF", "BOTT", "DECK", "BARR", "LIVE" };
            s2k.nameread = new List<string> { "DC1 ------------------", "DC2 ------------------", "DC3 ------------------", "DC4 ------------------", "DW ------------------", 
                "COMBT ------------------ MAX", "COMBT ------------------ MIN", "COMBL ------------------ MAX", "COMBL ------------------ MIN", "COMBF ------------------ MAX", "COMBF ------------------ MIN", "COMBP ------------------ MAX", "COMBP ------------------ MIN" };
            s2k.namewrite = new List<string> { "DC1", "DC2", "DC3", "DC4", "DW", "COMBT", "COMBL", "COMBF", "COMBP" };
            s2k.prop = new List<string> { "DC1", "DC2", "DC3", "DC4", "DW", "Truckmax", "Truckmin", "Lanemax", "Lanemin", "LLfmax", "LLfmin", "PLmax", "PLmin" };

            s2k.Node = new List<Node>(Node3);

            s2k.Elm = new List<Elm>(Elm);
            s2k.Sec = new List<Sec>(Sec);
            s2k.Crossbeam = new List<Crossbeam>(Input.Crossbeam);
            s2k.Wcon = Wcon;
            s2k.Wdeck = Wdeck;
            s2k.tAsphalt = Input.tAshalt;
            s2k.WAsphalt = Input.gAsphalt;
            s2k.Overloading = Overloading;
            s2k.Es = Es;
            s2k.Vparapet = Vparapet;
            s2k.Mparapet = Mparapet;
            s2k.Liveload = Liveload;
            s2k.Shoe = Shoe;
            s2k.ProBar = ProBar;
            s2k.LabelStatus = LabelStatus;

            return s2k;
        }

        public List<SecSap> SecSap
        {
            get
            {
                return Running().SecSap;
            }
        }

        public Solving Solving
        {
            get
            {
                Solving Solving = new Solving();
                Solving.Node = new List<Node>(Node3);
                Solving.Sec = new List<Sec>(Sec);
                Solving.Shoe = new List<Shoe>(Shoe);
                Solving.Crossbeam = new List<Crossbeam>(Input.Crossbeam);
                Solving.Mat = new List<Mat>(Input.Matuse);
                Solving.Overloading = Input.Overloading;
                Solving.Parapet = new List<Parapet>(Input.Parapet);
                Solving.eParapet = new List<double>(Input.eParapet());
                Solving.Asphalt = new List<double> { Input.tAshalt, 23.5 };
                Solving.Liveloadinput = Liveload;
                Solving.LLoad = Input.Laneload;
                Solving.Lanefactor = Input.Lanefactor;
                Solving.Pload = Input.Pload;
                Solving.delta = 2;
                Solving.Elm = new List<Elm>(Elm);
                Solving.ProBar = ProBar;
                Solving.LabelStatus = LabelStatus;
                return Solving;
            }

            set
            {

            }
            
        }




        //Add Sec to results to calcualate Stress
        public bool Results
        {
            get
            {
                if (Method == "Solver")
                {
                    Results R = Solving.Results();
                    R.Sec = new List<Sec>(Sec);
                    R.Node = Node3;
                    R.Mat = new List<Mat>(Input.Matuse);
                    R.Pforms = Input.Pforms;
                    R.ADTT = Input.ADTT;

                    SQL.WriteResulttoDB(R);
                    
                    return true;                    
                }
                else
                {
                    if (Running().NoError())
                    {
                        Results R = Running().Results();
                        R.Sec = new List<Sec>(Sec);
                        R.Node = Node3;
                        R.Mat = new List<Mat>(Input.Matuse);
                        R.Pforms = Input.Pforms;
                        R.ADTT = Input.ADTT;

                        SQL.WriteResulttoDB(R);
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
            }

            set { }
        }
        











        public Haunch Haunch1()
        {
            return new Haunch(Input.Aspan(), Input.Ahaunch);
        }

        public Haunch Haunch2()
        {
            Haunch Haunch = new Haunch(Input.Aspan(), Input.Ahaunch);
            Haunch.Sta = Node2.Select(p => p.X).ToList();

            return Haunch;
        }


        //Tool 

        public List<NodeInput> Addnode(List<NodeInput> LNode, double[,] b, string pro, int Type, string expro)
        {
            var nlong = LNode.Where(p => p.BeamID == 1).ToList().Count;
            var n = LNode.Count / nlong;
            string[] exprostr = expro.Split(',');
            int index;
            LNode = LNode.OrderBy(p => p.BeamID).ThenBy(p => p.Y).ThenBy(p => p.X).ToList();
            //Insert the point if X is the new X
            for (int i = 0; i < b.GetLength(1); i++)
            {
                if (LNode.Select(p => p.X).ToList().IndexOf(b[0, i]) == -1)
                {
                    for (int j = 0; j < n; j++) //Do for all girder
                    {
                        NodeInput a = new NodeInput();
                        a.X = b[0, i];
                        a.Z = 0;
                        a.Y = LNode[j * nlong].Y;
                        a.BeamID = LNode[j * nlong].BeamID;
                        a.Type = Type;
                        a.Label = "Section Changed";

                        index = LNode.FindLastIndex(p => p.X <= b[0, i]);
                        for (int k = 0; k < exprostr.GetLength(0); k++)
                        {
                            PropertyInfo newpro = a.GetType().GetProperty(exprostr[k]);
                            PropertyInfo oldpro = LNode[index].GetType().GetProperty(exprostr[k]);

                            newpro.SetValue(a, oldpro.GetValue(LNode[index], null));
                        }

                        LNode.Add(a);
                    }

                }
            }

            LNode = Reorder(LNode);

            //Add pro
            string[] prostring = pro.Split(',');
            double provalue = 0;

            for (int i = 0; i < prostring.GetLength(0); i++)
            {
                for (int j = 0; j < LNode.Count; j++)
                {
                    for (int k = 0; k < b.GetLength(1); k++)
                    {
                        if (b[0, k] == LNode[j].X)
                            provalue = b[i + 1, k];
                    }
                    PropertyInfo propertyInfo = LNode[j].GetType().GetProperty(prostring[i]);
                    propertyInfo.SetValue(LNode[j], provalue);
                }
            }

            return LNode;

        }


        public static List<NodeInput> Reorder(List<NodeInput> Node)
        {
            var nlong = Node.Where(p => p.BeamID == 1).ToList().Count;
            var ntran = Node.Count / nlong;

            Node = Node.OrderBy(p => p.BeamID).ThenBy(p => p.Y).ThenBy(p => p.X).ToList();

            for (int i = 0; i < ntran; i++)
                for (int j = 0; j < nlong; j++)
                {
                    Node[i * nlong + j].Joint = (i + 1) * 100 + j + 1;
                }

            return Node;
        }

        //Cumulate the loc-row
        public double[,] Cumatrix(double[,] a, int loc)
        {
            double[,] b = new double[a.GetLength(0) - loc, a.GetLength(1) + 1];

            b[0, 0] = 0;
            for (int i = 1; i < a.GetLength(1) + 1; i++)
            {
                b[0, i] = 0;
                for (int j = 0; j < i; j++)
                    b[0, i] = a[loc, j] + b[0, i];
            }


            for (int i = loc + 1; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    b[i - loc, j] = a[i, j];
            }

            return b;
        }

        public List<NodeInput> Add1prop(List<NodeInput> LNode, double[] b, string a)
        {
            string[] pro = a.Split(',');

            for (int i = 0; i < pro.GetLength(0); i++)
            {
                for (int j = 0; j < LNode.Count; j++)
                {
                    PropertyInfo propertyInfo = LNode[j].GetType().GetProperty(pro[i]);
                    propertyInfo.SetValue(LNode[j], b[i]);
                }
            }

            return LNode;
        }

        public List<NodeInput> Addd0(List<NodeInput> LNode, double[,] Atop, string a)
        {

            var c = Cumatrix(Atop, 2);
            double b = 0;

            for (int j = 0; j < LNode.Count; j++)
            {
                for (int k = 0; k < c.GetLength(1) - 1; k++)
                {
                    if (c[0, k] <= LNode[j].X)
                        b = Atop[2, k];
                }
                PropertyInfo propertyInfo = LNode[j].GetType().GetProperty(a);
                propertyInfo.SetValue(LNode[j], b);
            }

            return LNode;
        }

        public List<NodeInput> AddKframe(List<NodeInput> Node, List<KFrame> KK)
        {
            var Node1 = Node.Where(p => p.BeamID == 1).ToList();
            var nlong = Node1.Count;
            var n = Node.Count / nlong;

            var K = KK.Where(p => p.Location == true).ToList();

            for (int i = 0; i < K.Count; i++)
            {
                int index = Node1.Select(p => p.X).ToList().IndexOf(K[i].Station * 1000);
                if (index == -1)
                {
                    int id = Node1.FindLastIndex(p => p.X <= K[i].Station * 1000);
                    for (int j = 0; j < n; j++)
                    {
                        NodeInput a = Node[j * nlong + id].ShallowCopy();
                        a.X = K[i].Station * 1000;
                        a.Type = 7;
                        a.Label = "K - Frame";
                        a.Restrain = "";
                        Node.Add(a);
                    }

                }
                else
                {
                    for (int j = 0; j < n; j++)
                    {
                        Node[j * nlong + index].Type = 7;
                        Node[j * nlong + index].Label = "K - Frame";
                    }
                }
            }


            //Reorder by X and Y
            Node = Reorder(Node);

            // Set Lb
            Node1 = Node.Where(p => p.BeamID == 1).ToList();
            var Lb = Node.Where(p => (p.Type == 1 || p.Type == 2 || p.Type == 3 || p.Type == 7) && p.BeamID == 1).Select(p => p.X).ToList();
            nlong = Node1.Count;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < nlong - 1; j++)
                {
                    int Lbindex = Lb.FindLastIndex(p => p <= Node1[j].X);
                    Node[i * nlong + j].Lb = Lb[Lbindex + 1] - Lb[Lbindex];
                }
                Node[i * nlong + nlong - 1].Lb = Lb[Lb.Count - 1] - Lb[Lb.Count - 2];
            }

            return Node;
        }

        public List<NodeInput> Selection(List<NodeInput> LNode, double x1, double x2)
        {
            List<NodeInput> b = new List<NodeInput>();

            List<NodeInput> Node1 = LNode.Where(p => p.BeamID == 1).ToList();
            int nlong = Node1.Count;
            int ntran = LNode.Count / nlong;

            List<NodeInput> Nodeadd = new List<NodeInput>();

            for (int k = 0; k < ntran; k++)
            {
                for (int i = 0; i < nlong - 1; i++)
                {
                    double x = Node1[i].Haunch == 0 ? x1 * 1000 : x2 * 1000;

                    if (Node1[i + 1].X - Node1[i].X > x)
                    {
                        int n = (int)Math.Ceiling((Node1[i + 1].X - Node1[i].X) / x) - 1;
                        for (int j = 0; j < n; j++)
                        {
                            NodeInput a = LNode[k * nlong + i].ShallowCopy();
                            a.X = Node1[i].X + (Node1[i + 1].X - Node1[i].X) / (n + 1) * (j + 1);
                            a.Type = 8;
                            a.Label = "";
                            a.Restrain = "";
                            Nodeadd.Add(a);
                        }

                    }
                }
            }
            b.AddRange(LNode);
            b.AddRange(Nodeadd);
            b = Reorder(b);
            return b;
        }


    }
}
