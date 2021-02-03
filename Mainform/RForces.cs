using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Classes;
using Provider;

namespace Mainform
{
    public partial class RForces : Form
    {
        public RForces()
        {
            InitializeComponent();

        }

        public int nForces;

        private void RForces_Load(object sender, EventArgs e)
        {
                      

            List<ElmPrint> ElmPrint = SQL.getForces(nForces, 10);
            dgvMST.DataSource = ElmPrint;
            
            for (int i = 2; i <= 10; i++)
            {
                dgvMST.Columns[i].DefaultCellStyle.Format = "0.000";
            }

            for (int i = 0; i < dgvMST.RowCount; i++)
            {
                if (Convert.ToDouble(dgvMST.Rows[i].Cells[0].Value) % 2 == 0)
                {
                    dgvMST.Rows[i].DefaultCellStyle.BackColor = Color.White;                   

                }
                else
                {
                    dgvMST.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);                  

                }
            }
            Changecolor(dgvMST);


            if (nForces == 0)
                this.Text = "Moment (kNm)";
            else if (nForces == 1)
                this.Text = "Shear (kN)";
            else if (nForces == 2)
                this.Text = "Torsion (kNm)";
            else if (nForces == 3)
                this.Text = "Deflection (m)";
            else if (nForces == 4)
                this.Text = "Reaction (kN)";

        }

        private void Changecolor(DataGridView a)
        {
            
            a.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 89, 103);
            a.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(183, 222, 232);
            a.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            a.EnableHeadersVisualStyles = false;
            a.ClearSelection();            
            a.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            a.CellBorderStyle = DataGridViewCellBorderStyle.Raised;

        }
    }
}
