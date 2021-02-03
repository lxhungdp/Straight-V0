using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class NodePrint
    {
       
        public int Joint   //101 102 103 201 202 203. Maximum 100 node
        { get; set; }
               
        public string Label
        { get; set; }       

        public double X
        { get; set; }
        public double Y
        { get; set; }
        public double Z
        { get; set; }
       
        public double btop
        { get; set; }
        public double ttop
        { get; set; }


        //Bottom flange
        public double bbot
        { get; set; }

        public double tbot
        { get; set; }
       

        //Web        
        public double D
        { get; set; }
        public double tw
        { get; set; }

        //Bottom concrete
        public double Hc
        { get; set; }

        //Deck
        public double ts
        { get; set; }
        

    }
}
