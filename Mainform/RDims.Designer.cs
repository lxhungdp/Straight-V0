namespace Mainform
{
    partial class RDims
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDim = new System.Windows.Forms.DataGridView();
            this.Node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LLmax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ix2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A2l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ix2l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy2l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ix4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ix5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDim)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDim
            // 
            this.dgvDim.AllowUserToAddRows = false;
            this.dgvDim.AllowUserToDeleteRows = false;
            this.dgvDim.AllowUserToResizeColumns = false;
            this.dgvDim.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkCyan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDim.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDim.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDim.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Node,
            this.Station1,
            this.Des,
            this.DC1,
            this.DC2,
            this.DC3,
            this.DC4,
            this.DW,
            this.LLmax,
            this.A2s,
            this.Ix2s,
            this.Iy2s,
            this.YU2s,
            this.YL2s,
            this.J2s,
            this.A2l,
            this.Ix2l,
            this.Iy2l,
            this.YU3,
            this.YL3,
            this.J3,
            this.A4,
            this.Ix4,
            this.Iy4,
            this.YU4,
            this.YL4,
            this.J4,
            this.A5s,
            this.Ix5s,
            this.Iy5s,
            this.YU5s,
            this.YL5s,
            this.J5s});
            this.dgvDim.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDim.Location = new System.Drawing.Point(0, 0);
            this.dgvDim.Name = "dgvDim";
            this.dgvDim.RowHeadersVisible = false;
            this.dgvDim.RowTemplate.Height = 23;
            this.dgvDim.Size = new System.Drawing.Size(1634, 811);
            this.dgvDim.TabIndex = 4;
            // 
            // Node
            // 
            this.Node.DataPropertyName = "Element";
            this.Node.HeaderText = "Element";
            this.Node.Name = "Node";
            this.Node.Width = 60;
            // 
            // Station1
            // 
            this.Station1.DataPropertyName = "Node";
            this.Station1.HeaderText = "Node";
            this.Station1.Name = "Station1";
            this.Station1.Width = 60;
            // 
            // Des
            // 
            this.Des.DataPropertyName = "Station";
            this.Des.HeaderText = "Station";
            this.Des.Name = "Des";
            this.Des.Width = 60;
            // 
            // DC1
            // 
            this.DC1.DataPropertyName = "A1";
            this.DC1.HeaderText = "A         (Type 1)";
            this.DC1.Name = "DC1";
            // 
            // DC2
            // 
            this.DC2.DataPropertyName = "Ix1";
            this.DC2.HeaderText = "Ix        (Type 1)";
            this.DC2.Name = "DC2";
            // 
            // DC3
            // 
            this.DC3.DataPropertyName = "Iy1";
            this.DC3.HeaderText = "Iy        (Type 1)";
            this.DC3.Name = "DC3";
            // 
            // DC4
            // 
            this.DC4.DataPropertyName = "YU1";
            this.DC4.HeaderText = "YU        (Type 1)";
            this.DC4.Name = "DC4";
            // 
            // DW
            // 
            this.DW.DataPropertyName = "YL1";
            this.DW.HeaderText = "YL        (Type 1)";
            this.DW.Name = "DW";
            // 
            // LLmax
            // 
            this.LLmax.DataPropertyName = "J1";
            this.LLmax.HeaderText = "J         (Type 1)";
            this.LLmax.Name = "LLmax";
            // 
            // A2s
            // 
            this.A2s.DataPropertyName = "A2s";
            this.A2s.HeaderText = "A         (Type 2)";
            this.A2s.Name = "A2s";
            // 
            // Ix2s
            // 
            this.Ix2s.DataPropertyName = "Ix2s";
            this.Ix2s.HeaderText = "Ix        (Type 2)";
            this.Ix2s.Name = "Ix2s";
            // 
            // Iy2s
            // 
            this.Iy2s.DataPropertyName = "Iy2s";
            this.Iy2s.HeaderText = "Iy        (Type 2)";
            this.Iy2s.Name = "Iy2s";
            // 
            // YU2s
            // 
            this.YU2s.DataPropertyName = "YU2s";
            this.YU2s.HeaderText = "YU        (Type 2)";
            this.YU2s.Name = "YU2s";
            // 
            // YL2s
            // 
            this.YL2s.DataPropertyName = "YL2s";
            this.YL2s.HeaderText = "YL        (Type 2)";
            this.YL2s.Name = "YL2s";
            // 
            // J2s
            // 
            this.J2s.DataPropertyName = "J2s";
            this.J2s.HeaderText = "J         (Type 2)";
            this.J2s.Name = "J2s";
            // 
            // A2l
            // 
            this.A2l.DataPropertyName = "A3s";
            this.A2l.HeaderText = "A         (Type 3)";
            this.A2l.Name = "A2l";
            // 
            // Ix2l
            // 
            this.Ix2l.DataPropertyName = "Ix3s";
            this.Ix2l.HeaderText = "Ix        (Type 3)";
            this.Ix2l.Name = "Ix2l";
            // 
            // Iy2l
            // 
            this.Iy2l.DataPropertyName = "Iy3s";
            this.Iy2l.HeaderText = "Iy        (Type 3)";
            this.Iy2l.Name = "Iy2l";
            // 
            // YU3
            // 
            this.YU3.DataPropertyName = "YU3s";
            this.YU3.HeaderText = "YU        (Type 3)";
            this.YU3.Name = "YU3";
            // 
            // YL3
            // 
            this.YL3.DataPropertyName = "YL3s";
            this.YL3.HeaderText = "YL        (Type 3)";
            this.YL3.Name = "YL3";
            // 
            // J3
            // 
            this.J3.DataPropertyName = "J3s";
            this.J3.HeaderText = "J         (Type 3)";
            this.J3.Name = "J3";
            // 
            // A4
            // 
            this.A4.DataPropertyName = "A4s";
            this.A4.HeaderText = "A         (Type 4)";
            this.A4.Name = "A4";
            // 
            // Ix4
            // 
            this.Ix4.DataPropertyName = "Ix4s";
            this.Ix4.HeaderText = "Ix        (Type 4)";
            this.Ix4.Name = "Ix4";
            // 
            // Iy4
            // 
            this.Iy4.DataPropertyName = "Iy4s";
            this.Iy4.HeaderText = "Iy        (Type 4)";
            this.Iy4.Name = "Iy4";
            // 
            // YU4
            // 
            this.YU4.DataPropertyName = "YU4s";
            this.YU4.HeaderText = "YU        (Type 4)";
            this.YU4.Name = "YU4";
            // 
            // YL4
            // 
            this.YL4.DataPropertyName = "YL4s";
            this.YL4.HeaderText = "YL        (Type 4)";
            this.YL4.Name = "YL4";
            // 
            // J4
            // 
            this.J4.DataPropertyName = "J4s";
            this.J4.HeaderText = "J         (Type 4)";
            this.J4.Name = "J4";
            // 
            // A5s
            // 
            this.A5s.DataPropertyName = "A5s";
            this.A5s.HeaderText = "A         (Type 5)";
            this.A5s.Name = "A5s";
            // 
            // Ix5s
            // 
            this.Ix5s.DataPropertyName = "Ix5s";
            this.Ix5s.HeaderText = "Ix        (Type 5)";
            this.Ix5s.Name = "Ix5s";
            // 
            // Iy5s
            // 
            this.Iy5s.DataPropertyName = "Iy5s";
            this.Iy5s.HeaderText = "Iy        (Type 5)";
            this.Iy5s.Name = "Iy5s";
            // 
            // YU5s
            // 
            this.YU5s.DataPropertyName = "YU5s";
            this.YU5s.HeaderText = "YU        (Type 5)";
            this.YU5s.Name = "YU5s";
            // 
            // YL5s
            // 
            this.YL5s.DataPropertyName = "YL5s";
            this.YL5s.HeaderText = "YL        (Type 5)";
            this.YL5s.Name = "YL5s";
            // 
            // J5s
            // 
            this.J5s.DataPropertyName = "J5s";
            this.J5s.HeaderText = "J         (Type 5)";
            this.J5s.Name = "J5s";
            // 
            // RDims
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1634, 811);
            this.Controls.Add(this.dgvDim);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RDims";
            this.Text = "Sectional Properties";
            this.Load += new System.EventHandler(this.RDims_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDim)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDim;
        private System.Windows.Forms.DataGridViewTextBoxColumn Node;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Des;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DW;
        private System.Windows.Forms.DataGridViewTextBoxColumn LLmax;
        private System.Windows.Forms.DataGridViewTextBoxColumn A2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ix2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn J2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn A2l;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ix2l;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy2l;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU3;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL3;
        private System.Windows.Forms.DataGridViewTextBoxColumn J3;
        private System.Windows.Forms.DataGridViewTextBoxColumn A4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ix4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy4;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU4;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL4;
        private System.Windows.Forms.DataGridViewTextBoxColumn J4;
        private System.Windows.Forms.DataGridViewTextBoxColumn A5s;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ix5s;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy5s;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU5s;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL5s;
        private System.Windows.Forms.DataGridViewTextBoxColumn J5s;
    }
}