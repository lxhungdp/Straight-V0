using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;

namespace Provider
{
    public class s2k
    {
        public s2k()
        {

        }

        public s2k(List<Node> Node, List<Elm> Elm, List<Shoe> Shoe)
        {
            this.Node = new List<Node>(Node);
            this.Shoe = new List<Shoe>(Shoe);
            this.Elm = new List<Elm>(Elm);
        }


        public List<Node> Node
        { get; set; }

        public List<Elm> Elm
        { get; set; }

        public List<Sec> Sec
        { get; set; }

        public List<Crossbeam> Crossbeam
        { get; set; }

        public double Overloading
        { get; set; }

        public double Es
        { get; set; }

        public double Wcon
        { get; set; }

        public double Wdeck
        { get; set; }
        public double tAsphalt
        { get; set; }
        public double WAsphalt
        { get; set; }

        public double Vparapet
        { get; set; }

        public double Mparapet
        { get; set; }

        public List<string> filename
        { get; set; }

        public string path
        { get; set; }

        //Name of load for write s2k
        public List<string> namewrite
        { get; set; }

        //Name of load for read
        public List<string> nameread
        { get; set; }

        //properties name of ElmForces
        public List<string> prop
        { get; set; }

        public Liveload Liveload
        { get; set; }

        public List<Shoe> Shoe
        { get; set; }

        public ProgressBar ProBar
        { get; set; }

        public Label LabelStatus
        { get; set; }




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

        //Write Sec to DB
        public List<SecSap> SecSap
        {
            get
            {
                List<SecSap> SecSap = new List<SecSap>();
                for (int i = 0; i < Sec.Count / 2; i++)
                {
                    int ID = i + 1;
                    double A1 = Math.Round(Sec[i * 2].A1 / Math.Pow(10, 6), 10);
                    double J1 = Math.Round(Sec[i * 2].J1 / Math.Pow(10, 12), 10);
                    double Ix1 = Math.Round(Sec[i * 2].Ix1 / Math.Pow(10, 12), 10);
                    double Iy1 = Math.Round(Sec[i * 2].Iy1 / Math.Pow(10, 12), 10);

                    double A2 = Math.Round(Sec[i * 2].A2s / Math.Pow(10, 6), 10);
                    double J2 = Math.Round(Sec[i * 2].J2s / Math.Pow(10, 12), 10);
                    double Ix2 = Math.Round(Sec[i * 2].Ix2s / Math.Pow(10, 12), 10);
                    double Iy2 = Math.Round(Sec[i * 2].Iy2s / Math.Pow(10, 12), 10);

                    double A3 = Math.Round(Sec[i * 2].A5s / Math.Pow(10, 6), 10);
                    double J3 = Math.Round(Sec[i * 2].J5s / Math.Pow(10, 12), 10);
                    double Ix3 = Math.Round(Sec[i * 2].Ix5s / Math.Pow(10, 12), 10);
                    double Iy3 = Math.Round(Sec[i * 2].Iy5s / Math.Pow(10, 12), 10);

                    SecSap.Add(new Classes.SecSap(ID, A1, Ix1, Iy1, J1, A2, Ix2, Iy2, J2, A3, Ix3, Iy3, J3));
                }

                //List<Crossbeam> Crossbeam = new List<Crossbeam>(Input.Crossbeam);
                //Crossbeam and stringer
                for (int i = 0; i < Crossbeam.Count; i++)
                {
                    int ID = Sec.Count / 2 + i + 1;
                    double A = Math.Round(Crossbeam[i].Area() / Math.Pow(10, 6), 10);
                    double J = Math.Round(Crossbeam[i].J() / Math.Pow(10, 12), 10);
                    double Ix = Math.Round(Crossbeam[i].Ix() / Math.Pow(10, 12), 10);
                    double Iy = Math.Round(Crossbeam[i].Iy() / Math.Pow(10, 12), 10);

                    SecSap.Add(new Classes.SecSap(ID, A, Ix, Iy, J, A, Ix, Iy, J, A, Ix, Iy, J));
                }
                //Rigid link
                SecSap.Add(new Classes.SecSap(Sec.Count / 2 + Crossbeam.Count + 1, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10));

                return SecSap;
            }
        }


        public bool NoError()
        {           
            
            ProBar.Value = 10;
            LabelStatus.Text = "Creating s2k file then running ...";
            LabelStatus.Update();

            creates2k creates2k = new creates2k(path, filename, namewrite, Node, Elm, Sec, Crossbeam, Overloading, Es, Wcon, Wdeck, tAsphalt, WAsphalt, Vparapet, Mparapet, Liveload, Shoe);

            if (creates2k.Run())
                return true;
            else
                return false;           
        }

        public Results Results()
        {

            //ProBar.Value = 50;
            string out1 = path + @"\" + filename[0];
            string out2 = path + @"\" + filename[1];
            string out3 = path + @"\" + filename[2];
            string out4 = path + @"\" + filename[3];
            string out5 = path + @"\" + filename[4];

            ProBar.Value = 30;
            LabelStatus.Text = "Reading DC1 from output file ...";
            LabelStatus.Update();
            datafroms2k data1 = new datafroms2k(R1st, out1, nameread[0], prop[0]);

            ProBar.Value = 35;
            LabelStatus.Text = "Reading DC2 from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out2, nameread[1], prop[1]);

            ProBar.Value = 40;
            LabelStatus.Text = "Reading DC3 from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out3, nameread[2], prop[2]);

            ProBar.Value = 45;
            LabelStatus.Text = "Reading DC4 from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out4, nameread[3], prop[3]);

            ProBar.Value = 50;
            LabelStatus.Text = "Reading DW from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out4, nameread[4], prop[4]);

            ProBar.Value = 55;
            LabelStatus.Text = "Reading Truckmax from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out5, nameread[5], prop[5]);

            ProBar.Value = 60;
            LabelStatus.Text = "Reading Truckmin from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out5, nameread[6], prop[6]);

            ProBar.Value = 65;
            LabelStatus.Text = "Reading Lanemax from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out5, nameread[7], prop[7]);

            ProBar.Value = 70;
            LabelStatus.Text = "Reading Lanemin from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out5, nameread[8], prop[8]);

            ProBar.Value = 73;
            LabelStatus.Text = "Reading Truck Fatigue Max from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out5, nameread[9], prop[9]);

            ProBar.Value = 77;
            LabelStatus.Text = "Reading Truck Fatigue Min from output file ...";
            LabelStatus.Update();
            data1 = new datafroms2k(data1.Results(), out5, nameread[10], prop[10]);

            if (Liveload.PLane().Count > 0)
            {
                ProBar.Value = 80;
                LabelStatus.Text = "Reading PL max from output file ...";
                LabelStatus.Update();
                data1 = new datafroms2k(data1.Results(), out5, nameread[11], prop[11]);

                ProBar.Value = 80;
                LabelStatus.Text = "Reading PL min from output file ...";
                LabelStatus.Update();
                data1 = new datafroms2k(data1.Results(), out5, nameread[12], prop[12]);
            }


            return data1.Results();
        }
    }
}
