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
using System.Diagnostics;
using Provider;
using System.Threading;

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
            Node N = new Node();
            N.A1 = 1;
            N.A2 = 3.0;
            N.A3 = " ggg";

            List<Node> LN = new List<Node>();
            LN.Add(N);

            int n = LN[0].GetType().GetProperties().Length;
            var prop = LN[0].GetType().GetProperties();

            MessageBox.Show(prop[0].PropertyType.Name);

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if ((Application.OpenForms["Form2"] as Form2) == null)
            {
                Form2 f = new Form2() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.panel1.Controls.Add(f);

                f.Show();
            }         



        }
    } 
    
    public class Node
    {
        public Node()
        {

        }

        public int A1
        { get; set; }

        public double A2
        { get; set; }

        public string A3
        { get; set; }
    }

    
}
   

    


