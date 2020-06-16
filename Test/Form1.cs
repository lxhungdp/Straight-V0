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

           

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            
            
            
            f.textvalue = textBox1.Text;
            if (f.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = f.textvalue;
            }

        }
    }


   

    
}

