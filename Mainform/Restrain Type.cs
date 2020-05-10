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
    public partial class fRestrain : Form
    {
        public fRestrain()
        {
            InitializeComponent();
        }


        public string Scheck;

        private void btApply_Click(object sender, EventArgs e)
        {
            foreach (RadioButton item in groupBox1.Controls)
            {
                if (item != null)
                    if (item.Checked)
                    {
                        Scheck = item.Name;
                        break;
                    }
            }        
           
        }
    }

    
}
