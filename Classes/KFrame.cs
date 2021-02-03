using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class KFrame
    {
       
            
        public KFrame(double Station, bool Location , string Description)
        {
            this.Location = Location;
            this.Station = Station;
            this.Description = Description;
        }

        public double Station
        { get; set; }

        public bool Location
        { get; set; }        

        public string Description
        { get; set; }
    }
}

