using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Textfile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            path = path + @"\Sap2000\dh1.OUT";


            List<MST> MST = new List<MST>();
            
            for (int i = 1; i <32; i++)
            {
                MST mst1 = new MST();
                List<string> a = File.ReadLines(path)
                           .SkipWhile(line => !line.Contains("ELEM     " + (100 + i).ToString()))
                           .Skip(5)
                           .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                           .ToList();
                mst1.M1 = double.Parse(a[1]);
                mst1.S1 = double.Parse(a[2]);
                mst1.T1 = double.Parse(a[3]);
                MST.Add(mst1);

                MST mst2 = new MST();
                List<string> b = File.ReadLines(path)
                          .SkipWhile(line => !line.Contains("ELEM     " + (100 + i).ToString()))
                          .Skip(6)
                          .SelectMany(line => line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                          .ToList();
                mst2.M1 = double.Parse(b[1]);
                mst2.S1 = double.Parse(b[2]);
                mst2.T1 = double.Parse(b[3]);
                
                MST.Add(mst2);
            }

            var bs = new BindingSource();
            bs.DataSource = MST;
            dataGridView1.DataSource = bs;



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
