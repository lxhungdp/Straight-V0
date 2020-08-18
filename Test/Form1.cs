using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using Dapper;

namespace Test
{
    public partial class Form1 : Form
    {
        
        
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            List<double> a = new List<double> { 1, 2, 3, 4, 5 };

            List<double> b = new List<double> { 1.5, 3.2, 2,1};

            //List<int> index = new List<int>();
            //foreach (double b1 in b)
            //    index.Add(a.FindLastIndex(p => p<=b1));

            int c = a.FindLastIndex(p => p <= 0);
            MessageBox.Show(c.ToString());



        }

        

      
    }


   

    
}

