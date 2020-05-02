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
using Tools;

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
            double[] a = new double[] { 1, 2, 3, 4 };
            double[] b = new double[a.GetLength(0) + 1];
            b[0] = 0;
            for (int i = 1; i < a.GetLength(0)+1; i++)
            {                
                b[i] = 0;
                for (int j = 0; j < i; j++)
                    b[i] = a[j] + b[i];
            }

            for (int i = 0; i < b.GetLength(0); i++)
               MessageBox.Show(b[i].ToString());
           


        }
    }


    class Node
    {
        public double a1
        { get; set; }
        public string a2
        { get; set; }

       
    }

    
}

