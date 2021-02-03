using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Provider;

namespace Mainform
{
    public partial class RDims : Form
    {
        public RDims()
        {
            InitializeComponent();
        }

        public int ngirder;

        private void RDims_Load(object sender, EventArgs e)
        {
            DataTable dt = SQL.getSec2(ngirder);           
            
            dgvDim.DataSource = dt;

            for (int i = 3; i <= 32; i++)
            {
                dgvDim.Columns[i].DefaultCellStyle.Format = "0.00";
                dgvDim.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }

            for (int i = 0; i < dgvDim.RowCount; i++)
            {
                if (i % 4 == 0 || i % 4 == 1)
                {
                    dgvDim.Rows[i].DefaultCellStyle.BackColor = Color.White;

                }
                else
                {
                    dgvDim.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);

                }
            }
            Changecolor(dgvDim);
           
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
