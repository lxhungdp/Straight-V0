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
    public partial class MaterialProperty : Form
    {
        public MaterialProperty()
        {
            InitializeComponent();
        }

        private void MaterialProperty_Load(object sender, EventArgs e)
        {
            numWeight.Controls[0].Visible = false;
            numMass.Controls[0].Visible = false;
            
            cbType.SelectedIndex = 0;

            numWeight.Value = 25;
            numMass.Value = Convert.ToDecimal( 25 * 9.81);
            numMass.ReadOnly = true;

            numEs.Controls[0].Visible = false;
            numFy.Controls[0].Visible = false;
            numFu.Controls[0].Visible = false;
            numG.Controls[0].Visible = false;
            numFc.Controls[0].Visible = false;

            numEs.Value = 210000;
            numFy.Value = 380;
            numFu.Value = 500;
            numG.Value = 81000;
            numFc.Value = 35;
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbType.SelectedIndex == 0)
            {
                numWeight.Value = 25;
                numMass.Value = numWeight.Value * Convert.ToDecimal(9.81);
                groupBox4.Visible = true;
                groupBox4.Location = new Point(18, 242);
                groupBox3.Visible = false;

            }
                
            else
            {
                numWeight.Value = 75;
                numMass.Value = numWeight.Value * Convert.ToDecimal(9.81);
                groupBox4.Visible = false;
                groupBox3.Location = new Point(18, 242);
                groupBox3.Visible = true;
            }
                
        }
    }
}
