using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2
{
    public static class Check_SLS
    {

        public static double okk(this Node n)
        {
            return n.ntop * n.btop * n.ttop * Material.Fyf / 1000;
        }

    }

}
