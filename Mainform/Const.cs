using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mainform
{
    public class Const
    {
        public static string Constring
        {
             get { return Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName; }            
          
        }
    }
}
