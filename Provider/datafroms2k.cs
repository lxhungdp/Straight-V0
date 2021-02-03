using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Classes;

namespace Provider
{
    public class datafroms2k
    {        
        private List<NodeForces> _Deflection, _Reaction;
        private string path;
        private string nameread;
        private string prop;
        private Results R;
       

        public datafroms2k()
        {

        }
        public datafroms2k(Results Results, string path, string nameread, string prop)
        {                      
            this._Deflection = new List<NodeForces>(Results.Deflection);
            this._Reaction = new List<NodeForces>(Results.Reaction);            
            this.path = path;
            this.nameread = nameread;
            this.prop = prop;
            this.R = Results;            
        }               

        public Tuple<List<ElmForces>, List<ElmForces>, List<ElmForces>> Forces()
        {
            List<ElmForces> Mo = new List<ElmForces>(R.Moment);
            List<ElmForces> Sh = new List<ElmForces>(R.Shear);
            List<ElmForces> To = new List<ElmForces>(R.Torsion);
            PropertyInfo propertyInfo = Mo[0].GetType().GetProperty(prop);            

            var lines = File.ReadAllLines(path + ".OUT").SkipWhile(line => !line.Contains("F R A M E   E L E M E N T   I N T E R N A L   F O R C E S"));

            for (int i = 0; i < Mo.Count / 2; i++)
            {
                lines = lines
                      .SkipWhile(line => !line.Contains("ELEM    " + Mo[2 * i].Element));

                var a = lines
                      .SkipWhile(line => !line.Contains(nameread))
                      .SkipWhile(line => !line.Contains("REL DIST"))
                      .SkipWhile(line => !line.StartsWith(" 0.00"))
                      .Skip(0)
                      .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                      .ToList();
               
                var b = lines
                        .SkipWhile(line => !line.Contains(nameread))
                        .SkipWhile(line => !line.Contains("REL DIST"))
                        .SkipWhile(line => !line.StartsWith(" 1.00"))
                        .Skip(0)
                        .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                        .ToList();

                //propertyInfoM.SetValue(Mo[i * 2 + 1], double.Parse(b[6]));
                //propertyInfoS.SetValue(Sh[i * 2 + 1], double.Parse(b[2]));
                //propertyInfoT.SetValue(To[i * 2 + 1], double.Parse(b[4]));

                ElmForces M = Mo[i * 2].ShallowCopy();
                propertyInfo.SetValue(M, double.Parse(a[6]));
                Mo[i * 2] = M;

                ElmForces S = Sh[i * 2].ShallowCopy();
                propertyInfo.SetValue(S, double.Parse(a[2]));
                Sh[i * 2] = S;

                ElmForces T = To[i * 2].ShallowCopy();
                propertyInfo.SetValue(T, double.Parse(a[4]));
                To[i * 2] = T;

                M = Mo[i * 2 + 1].ShallowCopy();
                propertyInfo.SetValue(M, double.Parse(b[6]));
                Mo[i * 2 + 1] = M;

                S = Sh[i * 2 + 1].ShallowCopy();
                propertyInfo.SetValue(S, double.Parse(b[2]));
                Sh[i * 2 + 1] = S;

                T = To[i * 2 + 1].ShallowCopy();
                propertyInfo.SetValue(T, double.Parse(b[4]));
                To[i * 2 + 1] = T;
            }

            return Tuple.Create(Mo, Sh, To);
        }

        public List<NodeForces> Deflection()
        {
            List<NodeForces> Def = new List<NodeForces>(_Deflection);
            PropertyInfo propertyInfo = Def[0].GetType().GetProperty(prop);            
            var lines = File.ReadAllLines(path + ".OUT")
                .SkipWhile(line => !line.Contains("J O I N T   D I S P L A C E M E N T S"))
                .SkipWhile(line => !line.Contains(nameread))
                .SkipWhile(line => !line.Contains("JOINT"));

            for (int i = 0; i < Def.Count; i++)
            {
                NodeForces Def1 = new NodeForces();

                lines = lines
                        .SkipWhile(line => !line.StartsWith("     " + Def[i].Node.ToString()));

                var a = lines
                        .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                        .ToList();

                propertyInfo.SetValue(Def[i], double.Parse(a[3]));                
            }

            return Def;
        }

        public List<NodeForces> Reaction()
        {
            List<NodeForces> Rea = new List<NodeForces>(_Reaction);
            PropertyInfo propertyInfo = Rea[0].GetType().GetProperty(prop);
            //string L = "        ".Substring(0, 8 - loading.Length) + loading;

            var lines = File.ReadAllLines(path + ".OUT").SkipWhile(line => !line.Contains(" R E S T R A I N T   F O R C E S   ( R E A C T I O N S )"))
                    .SkipWhile(line => !line.Contains(nameread))
                    .SkipWhile(line => !line.Contains("JOINT"));

            for (int i = 0; i < Rea.Count; i++)
            {
                lines = lines
                        .SkipWhile(line => !line.StartsWith("        ".Remove(0, Rea[i].Description.Length) + Rea[i].Description));
                var a = lines
                    .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                    .ToList();

                propertyInfo.SetValue(Rea[i], double.Parse(a[3]));                
            }
            return Rea;
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
