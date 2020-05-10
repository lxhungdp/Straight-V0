using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mainform
{
    public partial class fAddmore : Form
    {
        public fAddmore()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btApply_Click(object sender, EventArgs e)
        {

            ndiv = Convert.ToInt32(nndiv.Value); 
            


           
        }

        private void fAddmore_Load(object sender, EventArgs e)
        {

        }

        public int ndiv;
    }
}
