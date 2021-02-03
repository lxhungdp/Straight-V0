using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Provider
{
    public class Text
    {
        public List<NodeInput> Node
        { get; set; }

        public string path
        { get; set; }

        public int Nseg
        { get; set; }

        public StreamWriter Sw
        { get; set; }

        

        public StreamWriter Joints
        {
            get
            {                
                Sw.WriteLine("JOINTS");
                foreach (NodeInput N in Node)
                {
                    Sw.WriteLine("  " + N.Joint.ToString() + "  X=  " + (N.X / 1000).ToString() + "  Y=  " + (N.X / 1000).ToString() + "  Z=  " + (N.Z / 1000).ToString());
                }
                Sw.Close();

                return Sw;
            }
        }

        public StreamWriter Restraints
        {
            get
            {
                List<NodeInput> Restrain = Node.Where(p => p.Type == 1 || p.Type == 2).ToList();
                StreamWriter a = new StreamWriter(path);
                a.WriteLine("RESTRAINTS");
                foreach (NodeInput N in Restrain)
                {
                    if (N.Restrain == "Fixed")
                        a.WriteLine("  ADD= " + N.Joint.ToString() + " DOF=Ux,Uy,Uz");
                    else if (N.Restrain == "Free")
                        a.WriteLine("  ADD= " + N.Joint.ToString() + " DOF=Uz");
                    else if (N.Restrain == "TranFixed")
                        a.WriteLine("  ADD= " + N.Joint.ToString() + " DOF=Uy,Uz");
                    else if (N.Restrain == "LongFixed")
                        a.WriteLine("  ADD= " + N.Joint.ToString() + " DOF=Ux,Uz");
                }
                a.Close();
                return a;
            }
        }

        public StreamWriter Materials
        {
            get
            {               
                StreamWriter a = new StreamWriter(path);
                a.Close();
                return a;
            }
        }

        //public StreamWriter Frame
        //{
        //    get
        //    {
        //        StreamWriter a = new StreamWriter(path);
        //        foreach (Elm b in Elm)
        //            a.WriteLine(b.Name.ToString() + " J=  " + b.iNode.ToString() + ",  " + b.jNode.ToString().ToString()
        //                    + " SEC =   " + b.Sec.ToString() + "  NSEG=" + b.Nseg.ToString());
        //        a.Close();
        //        return a;
        //    }
        //}

        //public StreamWriter Loadself
        //{
        //    get
        //    {
        //        StreamWriter a = new StreamWriter(path);
        //        a.WriteLine("LOADS");
        //        a.WriteLine("  NAME=  1");
        //        a.WriteLine("  Type= Gravity  Elem= Frame ");
        //        foreach (Elm b in Elm)
        //            a.WriteLine("  Add=  " + b.Name.ToString() + "  uz=-1.0");
        //        a.Close();
        //        return a;
        //    }
        //}
    }
}
