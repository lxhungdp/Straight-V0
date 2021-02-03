using Provider;
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
            foreach (RadioButton item in panel1.Controls)
            {
                if (item != null)
                    if (item.Checked)
                    {
                        Scheck = item.Name;
                        break;
                    }
            }        
           
        }

        private void fRestrain_Load(object sender, EventArgs e)
        {
            pictureBox1.Load(Const.Folderstring + @"\Picture\support.PNG");
        }

        
    }

    
}
