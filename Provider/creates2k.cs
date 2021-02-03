using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Provider
{
    public class creates2k
    {
        private List<Node> Node;
        private List<Elm> Elm;
        private List<Sec> Sec;
        private List<Crossbeam> Crossbeam;
        private string path;
        private double Overloading, Es, Wcon, Vp, Mp, tAsphalt, WAsphalt, Wdeck;
        private List<Shoe> Shoe;
        
        List<string> filename, namewrite;
        private Liveload Liveload;

        public creates2k()
        {

        }       

        public creates2k(string path, List<string> filename, List<string> namewrite, List<Node> Node, List<Elm> Elm, List<Sec> Sec, List<Crossbeam> Crossbeam, 
            double Overloading, double Es, double Wcon, double Wdeck, double tAsphalt, double WAsphalt, double Vp, double Mp, Liveload Liveload, List<Shoe> Shoe)
        {
            this.Node = new List<Node>(Node);
            this.Elm = new List<Elm>(Elm);
            this.Sec = new List<Sec>(Sec);
            this.Crossbeam = new List<Crossbeam>(Crossbeam);
            this.path = path;
            this.filename = new List<string>(filename);
            this.namewrite = new List<string>(namewrite);
            this.Shoe = new List<Shoe>(Shoe);

            this.Overloading = Overloading;
            this.Es = Es;
            this.Wcon = Wcon;
            this.Wdeck = Wdeck;
            this.tAsphalt = tAsphalt;
            this.WAsphalt = WAsphalt;                   
            this.Vp = Vp;
            this.Mp = Mp;
            this.Liveload = Liveload;
        }

       


        public StreamWriter Joints(StreamWriter a)
        {
            a.WriteLine("JOINTS");
            foreach (Node N in Node)
            {
                a.WriteLine("  " + N.Joint.ToString() + "  X=  " + (N.X / 1000).ToString() + "  Y=  " + (N.Y / 1000).ToString() + "  Z=  " + (N.Z / 1000).ToString());
            }

            a.WriteLine("; Shoe");
            foreach (Shoe N in Shoe)
            {
                a.WriteLine("  " + N.Joint1 + "  X=  " + (N.X / 1000).ToString() + "  Y=  " + (N.Y1 / 1000).ToString() + "  Z=  " + (N.Z / 1000).ToString());
                if (N.EA == 2)
                    a.WriteLine("  " + N.Joint2 + "  X=  " + (N.X / 1000).ToString() + "  Y=  " + (N.Y2 / 1000).ToString() + "  Z=  " + (N.Z / 1000).ToString());

            }
            return a;
        }

        public StreamWriter Restraints(StreamWriter a)
        {
            
            a.WriteLine("RESTRAINTS");
            foreach (Shoe N in Shoe)
            {
                if (N.Type1 == "Fixed")
                    a.WriteLine("  ADD= " + N.Joint1 + " DOF=Ux,Uy,Uz");
                else if (N.Type1 == "Free")
                    a.WriteLine("  ADD= " + N.Joint1 + " DOF=Uz");
                else if (N.Type1 == "TranFixed")
                    a.WriteLine("  ADD= " + N.Joint1 + " DOF=Uy,Uz");
                else if (N.Type1 == "LongFixed")
                    a.WriteLine("  ADD= " + N.Joint1 + " DOF=Ux,Uz");

                if (N.EA == 2)
                {
                    if (N.Type2 == "Fixed")
                        a.WriteLine("  ADD= " + N.Joint2 + " DOF=Ux,Uy,Uz");
                    else if (N.Type2 == "Free")
                        a.WriteLine("  ADD= " + N.Joint2 + " DOF=Uz");
                    else if (N.Type2 == "TranFixed")
                        a.WriteLine("  ADD= " + N.Joint2 + " DOF=Uy,Uz");
                    else if (N.Type2 == "LongFixed")
                        a.WriteLine("  ADD= " + N.Joint2 + " DOF=Ux,Uz");
                }
            }

            return a;

        }

        public StreamWriter Material(StreamWriter a)
        {
            a.WriteLine("MATERIAL");
            a.WriteLine("Name=  1  W=   " + Overloading);
            a.WriteLine("     E=  " + Es * 1000 + "  U=     .29629630  A=  1.200E-5");
            //Material for rigid link
            a.WriteLine("Name=  10  W=   0");
            a.WriteLine("     E=  210000000  U=     .29629630  A=  1.200E-5");
            return a;
        }

        // Steel only
        public StreamWriter Section1(StreamWriter a)
        {
            a.WriteLine("FRAME SECTION");

            for (int i = 0; i < Sec.Count / 2; i++)
            {
                double A = Math.Round(Sec[i * 2].A1 / Math.Pow(10, 6), 10);
                double J = Math.Round(Sec[i * 2].J1 / Math.Pow(10, 12), 10);
                double Ix = Math.Round(Sec[i * 2].Ix1 / Math.Pow(10, 12), 10);
                double Iy = Math.Round(Sec[i * 2].Iy1 / Math.Pow(10, 12), 10);
                a.WriteLine("  NAME=  " + (i + 1).ToString() + "  MAT=  1  A=    " + A + "  J=    " + J + "  I=   " + Ix + ",    " + Iy);


            }
            return a;
        }

        //Steel + bottom concrete
        public StreamWriter Section2(StreamWriter a)
        {
            a.WriteLine("FRAME SECTION");

            for (int i = 0; i < Sec.Count /2 ; i++)
            {
                double A = Math.Round(Sec[i * 2].A2s / Math.Pow(10, 6), 10);
                double J = Math.Round(Sec[i * 2].J2s / Math.Pow(10, 12), 10);
                double Ix = Math.Round(Sec[i * 2].Ix2s / Math.Pow(10, 12), 10);
                double Iy = Math.Round(Sec[i * 2].Iy2s / Math.Pow(10, 12), 10);
                a.WriteLine("  NAME=  " + (i + 1).ToString() + "  MAT=  1  A=    " + A + "  J=    " + J + "  I=   " + Ix + ",    " + Iy);
            }

            return a;
        }

        //Steel + Bottom Concrete + Deck
        public StreamWriter Section3(StreamWriter a)
        {
            a.WriteLine("FRAME SECTION");

            for (int i = 0; i < Sec.Count / 2; i++)
            {
                double A = Math.Round(Sec[i * 2].A5s / Math.Pow(10, 6), 10);
                double J = Math.Round(Sec[i * 2].J5s / Math.Pow(10, 12), 10);
                double Ix = Math.Round(Sec[i * 2].Ix5s / Math.Pow(10, 12), 10);
                double Iy = Math.Round(Sec[i * 2].Iy5s / Math.Pow(10, 12), 10);
                a.WriteLine("  NAME=  " + (i + 1).ToString() + "  MAT=  1  A=    " + A + "  J=    " + J + "  I=   " + Ix + ",    " + Iy);

            }

            return a;
        }


        public StreamWriter SectionCross(StreamWriter a)
        {
            a.WriteLine("; Crossbeam and Stringer");
            int n = Sec.Count() / 2;
            for (int i = 0; i < Crossbeam.Count; i++)
            {
                double A = Math.Round(Crossbeam[i].Area() / Math.Pow(10, 6), 10);
                double J = Math.Round(Crossbeam[i].J() / Math.Pow(10, 12), 10);
                double Ix = Math.Round(Crossbeam[i].Ix() / Math.Pow(10, 12), 10);
                double Iy = Math.Round(Crossbeam[i].Iy() / Math.Pow(10, 12), 10);
                a.WriteLine("  NAME=  " + (n + i + 1).ToString() + "  MAT=  1  A=    " + A + "  J=    " + J + "  I=   " + Ix + ",    " + Iy);

            }

            a.WriteLine("; Rigid link");
            a.WriteLine("  NAME=  " + (n + Crossbeam.Count + 1).ToString() + "  MAT=  10  A=   10.000   J=   10.000   I=    10.000,   10.000");

            return a;
        }



        public StreamWriter Frame(StreamWriter a)
        {
            a.WriteLine("FRAME");
            foreach (Elm b in Elm)
                a.WriteLine(b.Name.ToString() + " J=  " + b.iNode.ToString() + ",  " + b.jNode.ToString()
                        + " SEC=   " + b.Sec.ToString() + "  NSEG=" + b.Nseg.ToString());
            
            for (int i = 0; i < Shoe.Count; i++)
            {                
                a.WriteLine(Shoe[i].Joint1 +"  J=  " + Shoe[i].Joint.ToString() + ",  " + Shoe[i].Joint1.ToString()
                       + " SEC=   " + (Sec.Count() / 2 + Crossbeam.Count + 1 ).ToString());
                if (Shoe[i].EA == 2)
                {
                    a.WriteLine(Shoe[i].Joint2 + "  J=  " + Shoe[i].Joint.ToString() + ",  " + Shoe[i].Joint2.ToString()
                       + " SEC=   " + (Sec.Count() / 2 + Crossbeam.Count + 1).ToString());
                }
            }
                


            return a;
        }

        public StreamWriter Loadself(StreamWriter a)
        {
            a.WriteLine("LOADS");
            a.WriteLine("  NAME=  " + namewrite[0]);
            a.WriteLine("  Type= Gravity  Elem= Frame ");
            foreach (Elm b in Elm)
                a.WriteLine("  Add=  " + b.Name.ToString() + "  uz=-1.0");

            return a;
        }

        public StreamWriter Loadbottom(StreamWriter a)
        {
            a.WriteLine("LOADS");
            a.WriteLine("  NAME=  " + namewrite[1]);
            a.WriteLine("  Type= Distributed span ");
            foreach (Elm b in Elm)
            {
                double Ac = Node.Where(p => p.Joint == b.iNode).FirstOrDefault().Ac;
                double Pcon = -Ac * Wcon / Math.Pow(10, 6);

                if (b.Name[0] == 'G' && Ac != 0)
                    a.WriteLine("  Add=  " + b.Name.ToString() + "    uz=    " + Pcon);
            }

            return a;
        }
        public StreamWriter Loaddeck(StreamWriter a)
        {
            a.WriteLine("LOADS");
            a.WriteLine("  NAME=  " + namewrite[2]);
            a.WriteLine("  Type= Distributed span ");
            List<Elm> Elm1 = Elm.Where(p => p.Name.Substring(0, 1) == "G").ToList();
            foreach (Elm b in Elm1)
            {
                //Calcualte Vertical load and Moment due to deck
                Node CNode = Node.Where(p => p.Joint == b.iNode).FirstOrDefault();
                double Vd = -(CNode.bleft + CNode.aleft / 2 + CNode.bright + CNode.aright / 2) * CNode.ts * Wdeck / Math.Pow(10, 6);
                double Md = Vd * CNode.e / 1000;

                a.WriteLine("  Add=  " + b.Name.ToString() + "    uz=    " + Vd);
                a.WriteLine("  Add=  " + b.Name.ToString() + "    r1=    " + Md);
            }

            return a;
        }

        public StreamWriter Loadbar(StreamWriter a)
        {
            a.WriteLine("LOADS");
            a.WriteLine("  NAME=  " + namewrite[3]);
            a.WriteLine("  Type= Distributed span ");
            List<Elm> Elm1 = Elm.Where(p => p.Name.Substring(0, 2) == "G1").ToList();
            foreach (Elm b in Elm1)
            {
                a.WriteLine("  Add=  " + b.Name.ToString() + "    uz=    " + -Vp);
                a.WriteLine("  Add=  " + b.Name.ToString() + "    r1=    " + -Mp);
            }

            return a;
        }

        public StreamWriter LoadAsphalt(StreamWriter a)
        {
            a.WriteLine("  NAME=  " + namewrite[4]);
            a.WriteLine("  Type= Distributed span ");
            List<Elm> Elm1 = Elm.Where(p => p.Name.Substring(0, 1) == "G").ToList();
            foreach (Elm b in Elm1)
            {
                //Calcualte Vertical load and Moment due to Asphalt
                Node CNode = Node.Where(p => p.Joint == b.iNode).FirstOrDefault();
                double Vd = -(CNode.bleft + CNode.aleft / 2 + CNode.bright + CNode.aright / 2) * tAsphalt * WAsphalt / Math.Pow(10, 6);
                double Md = Vd * CNode.e / 1000;

                a.WriteLine("  Add=  " + b.Name.ToString() + "    uz=    " + Vd);
                a.WriteLine("  Add=  " + b.Name.ToString() + "    r1=    " + Md);
            }

            return a;
        }

        //Create lane
        public StreamWriter Lane(StreamWriter a)
        {
            a.WriteLine("LANE");
            List<double> Lleft = new List<double>(Liveload.nlane().Item1);
            List<double> Lright = new List<double>(Liveload.nlane().Item2);
            List<double> Lmid = new List<double>(Liveload.nlane().Item3);

            //Pedestrian
            List<Tuple<double, double>> Plane = new List<Tuple<double, double>>(Liveload.PLane());


            string Fr1 = Elm.Where(p => p.Name.Substring(0, 2) == "G1").FirstOrDefault().Name;
            string Fr2 = Elm.Where(p => p.Name.Substring(0, 2) == "G1").LastOrDefault().Name;
            for (int i = 0; i < Lleft.Count; i++)
            {
                a.WriteLine("Name= LEFT" + (i + 1).ToString());
                a.WriteLine("PATH=  " + Fr1 + ",   " + Fr2 + ",    1,    ECC=" + (Lleft[i] / 1000).ToString());
            }

            for (int i = 0; i < Lright.Count; i++)
            {
                a.WriteLine("Name= RIGHT" + (i + 1).ToString());
                a.WriteLine("PATH=  " + Fr1 + ",   " + Fr2 + ",    1,    ECC=" + (Lright[i] / 1000).ToString());
            }

            for (int i = 0; i < Lmid.Count; i++)
            {
                a.WriteLine("Name= MID" + (i + 1).ToString());
                a.WriteLine("PATH=  " + Fr1 + ",   " + Fr2 + ",    1,    ECC=" + (Lmid[i] / 1000).ToString());

            }

            //Pedestrian
            for (int i = 0; i < Plane.Count; i++)
            {
                a.WriteLine("Name= PED" + (i + 1).ToString());
                a.WriteLine("PATH=  " + Fr1 + ",   " + Fr2 + ",    1,    ECC=" + (Plane[i].Item1 / 2000 + Plane[i].Item2 / 1000).ToString());
            }
            return a;
        }

        //Create Vheicle
        public StreamWriter Vehicle(StreamWriter a)
        {
            a.WriteLine("VEHICLE");
            a.WriteLine("NAME= TRUCK  TYPE=      GEN");
            List<Tuple<double, double>> axle = new List<Tuple<double, double>>(Liveload.Axlebyfactor());
            a.WriteLine("P=  " + axle[0].Item2);

            for (int i = 1; i < axle.Count; i++)
                a.WriteLine("D=  " + axle[i].Item1 + "   P=  " + axle[i].Item2);

            a.WriteLine("NAME= LANE  TYPE=      GEN");
            a.WriteLine("W=  " + Liveload.Laneload.ToString());

            //Pedestrian
            //Using load for 1m wide of lane
            a.WriteLine("NAME= PED  TYPE=      GEN");
            a.WriteLine("W=  " + Liveload.Pload.ToString());


            a.WriteLine("");
            a.WriteLine("VEHICLE CLASS");
            a.WriteLine("NAME= TRUCK");
            a.WriteLine("VEHI= TRUCK");
            a.WriteLine("NAME= LANE");
            a.WriteLine("VEHI= LANE");
            a.WriteLine("NAME= PED");
            a.WriteLine("VEHI= PED");

            return a;
        }

        public StreamWriter Bridgeresponse(StreamWriter a)
        {
            a.WriteLine(" BRIDGE RESPONSE");
            a.WriteLine("ELEM=Frame");
            a.WriteLine("ADD=*");
            a.WriteLine("ELEM=JOINT   TYPE=DISP");
            a.WriteLine("ADD=*");
            a.WriteLine("ELEM=JOINT   TYPE=REAC");
            a.WriteLine("ADD=*");
            a.WriteLine("");

            return a;
        }

        public StreamWriter Movingload(StreamWriter a)
        {
            int nlane = Liveload.nlane().Item1.Count;
            int np = Liveload.PLane().Count;

            List<Tuple<double, double>> Plane = new List<Tuple<double, double>>(Liveload.PLane());
            double Pload = Liveload.Pload;

            a.WriteLine(" MOVING LOAD");
            //Truck load
            for (int i = 0; i < nlane; i++)
            {
                a.WriteLine("NAME= TLEFT" + (i + 1).ToString() + "  RF=       1");
                for (int j = 0; j <= i; j++)
                    a.WriteLine("CLASS=  TRUCK  LANE= LEFT" + (j + 1).ToString() + ", SF=1.0 ");
            }
            for (int i = 0; i < nlane; i++)
            {
                a.WriteLine("NAME= TRIGHT" + (i + 1).ToString() + "  RF=       1");
                for (int j = 0; j <= i; j++)
                    a.WriteLine("CLASS=  TRUCK  LANE= RIGHT" + (j + 1).ToString() + ", SF=1.0 ");
            }
            int b = 0;
            for (int i = 0; i < nlane; i++)
            {
                a.WriteLine("NAME= TMID" + (i + 1).ToString() + "  RF=       1");

                for (int j = i + b; j <= i + b + i; j++)
                    a.WriteLine("CLASS=  TRUCK  LANE= MID" + (j + 1).ToString() + ", SF=1.0 ");
                b = b + i;
            }

            //Lane load
            for (int i = 0; i < nlane; i++)
            {
                a.WriteLine("NAME= LLEFT" + (i + 1).ToString() + "  RF=       1");
                for (int j = 0; j <= i; j++)
                    a.WriteLine("CLASS=  LANE  LANE= LEFT" + (j + 1).ToString() + ", SF=1.0 ");
            }
            for (int i = 0; i < nlane; i++)
            {
                a.WriteLine("NAME= LRIGHT" + (i + 1).ToString() + "  RF=       1");
                for (int j = 0; j <= i; j++)
                    a.WriteLine("CLASS=  LANE  LANE= RIGHT" + (j + 1).ToString() + ", SF=1.0 ");
            }

            b = 0;
            for (int i = 0; i < nlane; i++)
            {
                a.WriteLine("NAME= LMID" + (i + 1).ToString() + "  RF=       1");

                for (int j = i + b; j <= i + b + i; j++)
                    a.WriteLine("CLASS=  LANE  LANE= MID" + (j + 1).ToString() + ", SF=1.0 ");
                b = b + i;
            }

            //Pedestrian
            for (int i = 0; i < np; i++)
            {
                a.WriteLine("NAME= PED" + (i + 1).ToString() + "  RF=       1");
                a.WriteLine("CLASS=  PED  LANE= PED" + (i + 1).ToString() + ", SF= " + Plane[i].Item1 / 1000 );
            }

            if (np == 2)
            {
                a.WriteLine("NAME= PED3  RF=       1");
                a.WriteLine("CLASS=  PED  LANE= PED1,  SF= " + Plane[0].Item1 / 1000);
                a.WriteLine("CLASS=  PED  LANE= PED2,  SF= " + Plane[1].Item1  / 1000);
            }


            return a;
        }

        public StreamWriter Combo(StreamWriter a)
        {
            int nlane = Liveload.nlane().Item1.Count;
            int plane = Liveload.PLane().Count;
            List<double> Lanefactor = new List<double>(Liveload.Lanefactor);

            a.WriteLine("");
            a.WriteLine("COMBO");
            a.WriteLine("NAME=" + namewrite[5] + "  TYPE=ENVE");

            //All lane for truck
            for (int i = 0; i < nlane; i++)
            {
                double Lfactor = i <= 4 ? Lanefactor[i] : Lanefactor[4];
                a.WriteLine("MOVE= " + "TLEFT" + (i + 1).ToString() + "   SF=        " + Lfactor);
                a.WriteLine("MOVE= " + "TRIGHT" + (i + 1).ToString() + "   SF=       " + Lfactor);
                a.WriteLine("MOVE= " + "TMID" + (i + 1).ToString() + "   SF=        " + Lfactor);
            }

            //All lane for lane
            a.WriteLine("NAME=" + namewrite[6] + "  TYPE=ENVE");
            for (int i = 0; i < nlane; i++)
            {
                double Lfactor = i <= 4 ? Lanefactor[i] : Lanefactor[4];
                a.WriteLine("MOVE= " + "LLEFT" + (i + 1).ToString() + "   SF=        " + Lfactor);
                a.WriteLine("MOVE= " + "LRIGHT" + (i + 1).ToString() + "   SF=        " + Lfactor);
                a.WriteLine("MOVE= " + "LMID" + (i + 1).ToString() + "   SF=        " + Lfactor);
            }

            // 1lane for truck (Fatigue) 1.15 * 0.8 = 0.92
            a.WriteLine("NAME=" + namewrite[7] + "  TYPE=ENVE");
            a.WriteLine("MOVE= TLEFT1   SF=        0.92");
            a.WriteLine("MOVE= TRIGHT1   SF=        0.92");
            a.WriteLine("MOVE= TMID1   SF=        0.92");

            //Pedestrian
            if (plane > 0)
            {
                a.WriteLine("NAME=" + namewrite[8] + "  TYPE=ENVE");
                for (int i = 0; i < plane; i ++)
                    a.WriteLine("MOVE= " + "PED" + (i + 1).ToString() + "   SF=        1.0");
                if (plane == 2)
                    a.WriteLine("MOVE= PED3   SF=        1.0");

            }         
            

            return a;
        }



        //Create s2k file

        public bool steelonly()
        {
            StreamWriter a = new StreamWriter(path + @"\" + filename[0] + ".s2k");
            a.WriteLine(" SYSTEM");
            a.WriteLine("Length = M  Force = kN");
            a.WriteLine("");
            a = Joints(a);
            a = Restraints(a);
            a = Material(a);
            a = Section1(a);
            a = SectionCross(a);
            a = Frame(a);
            a = Loadself(a);
            a.WriteLine("OUTPUT");
            a.WriteLine(" ELEM=JOINT  TYPE=DISP,APPL,REAC  LOAD=* ");
            a.WriteLine(" ELEM=FRAME  TYPE=FORCE           LOAD=* ");
            a.WriteLine("");
            a.WriteLine("END");
            a.Close();

            if (File.Exists(path + @"\" + filename[0] + ".s2k"))
                return true;
            else
                return false;
        }

        public bool bottomconcrete()
        {
            StreamWriter a = new StreamWriter(path + @"\" + filename[1] + ".s2k");
            a.WriteLine(" SYSTEM");
            a.WriteLine("Length = M  Force = kN");
            a.WriteLine("");
            a = Joints(a);
            a = Restraints(a);
            a = Material(a);
            a = Section1(a);
            a = SectionCross(a);
            a = Frame(a);
            a = Loadbottom(a);
            a.WriteLine("OUTPUT");
            a.WriteLine(" ELEM=JOINT  TYPE=DISP,APPL,REAC  LOAD=* ");
            a.WriteLine(" ELEM=FRAME  TYPE=FORCE           LOAD=* ");
            a.WriteLine("");
            a.WriteLine("END");
            a.Close();

            if (File.Exists(path + @"\" + filename[1] + ".s2k"))
                return true;
            else
                return false;
        }

        public bool deck()
        {
            StreamWriter a = new StreamWriter(path + @"\" + filename[2] + ".s2k");
            a.WriteLine(" SYSTEM");
            a.WriteLine("Length = M  Force = kN");
            a.WriteLine("");
            a = Joints(a);
            a = Restraints(a);
            a = Material(a);
            a = Section2(a);
            a = SectionCross(a);
            a = Frame(a);
            a = Loaddeck(a);
            a.WriteLine("OUTPUT");
            a.WriteLine(" ELEM=JOINT  TYPE=DISP,APPL,REAC  LOAD=* ");
            a.WriteLine(" ELEM=FRAME  TYPE=FORCE           LOAD=* ");
            a.WriteLine("");
            a.WriteLine("END");
            a.Close();

            if (File.Exists(path + @"\" + filename[2] + ".s2k"))
                return true;
            else
                return false;
        }

        public bool bar()
        {
            StreamWriter a = new StreamWriter(path + @"\" + filename[3] + ".s2k");
            a.WriteLine(" SYSTEM");
            a.WriteLine("Length = M  Force = kN");
            a.WriteLine("");
            a = Joints(a);
            a = Restraints(a);
            a = Material(a);
            a = Section3(a);
            a = SectionCross(a);
            a = Frame(a);
            a = Loadbar(a);
            a = LoadAsphalt(a);
            a.WriteLine("OUTPUT");
            a.WriteLine(" ELEM=JOINT  TYPE=DISP,APPL,REAC  LOAD=* ");
            a.WriteLine(" ELEM=FRAME  TYPE=FORCE           LOAD=* ");
            a.WriteLine("");
            a.WriteLine("END");
            a.Close();

            if (File.Exists(path + @"\" + filename[3] + ".s2k"))
                return true;
            else
                return false;
        }

        public bool liveload()
        {
            StreamWriter a = new StreamWriter(path + @"\" + filename[4] + ".s2k");
            a.WriteLine(" SYSTEM");
            a.WriteLine("Length = M  Force = kN");
            a.WriteLine("");
            a = Joints(a);
            a = Restraints(a);
            a = Material(a);
            a = Section3(a);
            a = SectionCross(a);
            a = Frame(a);
            a = Lane(a);
            a = Vehicle(a);
            a = Bridgeresponse(a);
            a = Movingload(a);
            a = Combo(a);
            a.WriteLine("OUTPUT");
            a.WriteLine("  ELEM= JOINT  Type= DISP,APPL,REAC   LOAD= *  COMB=* ;");
            a.WriteLine("  ELEM= FRAME  Type= FORCE   LOAD= *  COMB=* ;");

            a.Close();

            if (File.Exists(path + @"\" + filename[4] + ".s2k"))
                return true;
            else
                return false;
        }



        public bool Run()
        {
            if (steelonly() && bottomconcrete() && deck() && bar() && liveload())
                return RunLL(filename[0]) && RunLL(filename[1]) && RunLL(filename[2]) && RunLL(filename[3]) && RunLL(filename[4]);
            else
                return false;
        }
        


        //public bool Run(string name)
        //{
        //    string pathsap = path;
        //    string s2kfile = name + ".s2k";

        //    string cmdtext = "/C Sapre " + s2kfile;
        //    Process p = new Process();
        //    p.StartInfo.FileName = "CMD.exe";
        //    p.StartInfo.WorkingDirectory = pathsap;
        //    p.StartInfo.Arguments = cmdtext;
        //    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    p.StartInfo.UseShellExecute = false;
        //    p.StartInfo.RedirectStandardInput = true;
        //    p.StartInfo.RedirectStandardOutput = true;
        //    p.StartInfo.CreateNoWindow = true;
        //    p.Start();
        //    p.WaitForExit();
        //    string a = p.StandardOutput.ReadToEnd();
        //    int check1 = a.IndexOf("I N P U T   C O M P L E T E");

        //    string cmdtext1 = "/C Sapgo " + s2kfile;
        //    Process p1 = new Process();
        //    p1.StartInfo.FileName = "CMD.exe";
        //    p1.StartInfo.WorkingDirectory = pathsap;
        //    p1.StartInfo.Arguments = cmdtext1;
        //    p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    p1.StartInfo.UseShellExecute = false;
        //    p1.StartInfo.RedirectStandardInput = true;
        //    p1.StartInfo.RedirectStandardOutput = true;
        //    p1.StartInfo.CreateNoWindow = true;
        //    p1.Start();
        //    p1.WaitForExit();
        //    a = p1.StandardOutput.ReadToEnd();
        //    int check2 = a.IndexOf("A N A L Y S I S   C O M P L E T E");

        //    new List<string>(Directory.GetFiles(path)).ForEach(file =>
        //    {
        //        if (file.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0 &&
        //            Path.GetExtension(file) != ".OUT" &&
        //            Path.GetExtension(file) != ".s2k")
        //            File.Delete(file);
        //    });

        //    if (check1 != -1 && check2 != -1)
        //        return true;
        //    else
        //        return false;
        //}

        public bool RunLL(string name)
        {
            string pathsap = path;
            string s2kfile = name + ".s2k";
            string copiedFileName;
            List<string> output_list = new List<string>();

            var processInfo = new ProcessStartInfo("cmd.exe", "/c Sapre " + s2kfile)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = pathsap
            };
            Process p = Process.Start(processInfo);

            p.BeginErrorReadLine();

            while ((copiedFileName = p.StandardOutput.ReadLine()) != null)
            {
                if (copiedFileName.Contains("I N P U T   C O M P L E T E"))
                    output_list.Add(copiedFileName);
            }

            p.WaitForExit();

            processInfo = new ProcessStartInfo("cmd.exe", "/c Sapgo " + s2kfile)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = pathsap,
            };

            p = Process.Start(processInfo);
            p.BeginErrorReadLine();

            while ((copiedFileName = p.StandardOutput.ReadLine()) != null)
            {
                if (copiedFileName.Contains("A N A L Y S I S   C O M P L E T E"))
                    output_list.Add(copiedFileName);
            }
            p.WaitForExit();

            new List<string>(Directory.GetFiles(pathsap)).ForEach(file =>
            {
                if (file.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0 &&
                    Path.GetExtension(file) != ".OUT" &&
                    Path.GetExtension(file) != ".s2k")
                    File.Delete(file);
            });

            return output_list.Count == 2 ? true : false;
        }


    }
}
