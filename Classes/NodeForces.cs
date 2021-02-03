using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NodeForces
    {
        public NodeForces()
        {

        }

        public int Node
        { get; set; }

        public double Station
        { get; set; }

        public string Description
        { get; set; }

        public double DC1
        { get; set; }
        public double DC2
        { get; set; }
        public double DC3
        { get; set; }
        public double DC4
        { get; set; }
        public double DW
        { get; set; }
        public double Truckmax
        { get; set; }
        public double Truckmin
        { get; set; }
        public double Lanemax
        { get; set; }
        public double Lanemin
        { get; set; }
        public double PLmax
        { get; set; }
        public double PLmin
        { get; set; }
        public double LLfmax
        { get; set; }
        public double LLfmin
        { get; set; }

        public double LLmax
        {
            get
            {
                return Math.Max(1.25 * Truckmax + PLmax, 0.75 * Truckmax * 1.25 + Lanemax + PLmax);
            }
        }

        public double LLmin
        {
            get
            {
                return Math.Min(1.25 * Truckmin + PLmin, 0.75 * Truckmin * 1.25 + Lanemin + PLmin);
            }
        }




    }
}
