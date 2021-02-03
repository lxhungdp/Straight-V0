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
    public partial class RNode : Form
    {
        public RNode()
        {
            InitializeComponent();
        }

        private void RNode_Load(object sender, EventArgs e)
        {
            DataTable dt = SQL.getNode();

            dgvNode.DataSource = dt;
           
            dgvNode.Columns[11].DefaultCellStyle.Format = "0.00";
            for (int i = 0; i < dgvNode.RowCount; i++)
            {
                if (i % 2 == 0)
                {
                    dgvNode.Rows[i].DefaultCellStyle.BackColor = Color.White;

                }
                else
                {
                    dgvNode.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(217, 217, 217);

                }
            }
            Changecolor(dgvNode);
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
