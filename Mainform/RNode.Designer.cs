namespace Mainform
{
    partial class RNode
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
            this.dgvNode = new System.Windows.Forms.DataGridView();
            this.Node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LLmax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A5s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.w = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J2s = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A2l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ix2l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy2l = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ix4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Iy4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YU4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.YL4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.J4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNode)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNode
            // 
            this.dgvNode.AllowUserToAddRows = false;
            this.dgvNode.AllowUserToDeleteRows = false;
            this.dgvNode.AllowUserToResizeColumns = false;
            this.dgvNode.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkCyan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNode.ColumnHeadersHeight = 30;
            this.dgvNode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Node,
            this.Station1,
            this.Des,
            this.DC1,
            this.Z,
            this.DC2,
            this.DC3,
            this.DC4,
            this.DW,
            this.LLmax,
            this.A2s,
            this.D,
            this.Iy2s,
            this.A5s,
            this.YU2s,
            this.ts,
            this.w,
            this.YL2s,
            this.J2s,
            this.A2l,
            this.Ix2l,
            this.Iy2l,
            this.YU3,
            this.YL3,
            this.crb,
            this.J3,
            this.A4,
            this.Ix4,
            this.Iy4,
            this.YU4,
            this.YL4,
            this.J4});
            this.dgvNode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNode.Location = new System.Drawing.Point(0, 0);
            this.dgvNode.Name = "dgvNode";
            this.dgvNode.RowHeadersVisible = false;
            this.dgvNode.RowTemplate.Height = 23;
            this.dgvNode.Size = new System.Drawing.Size(1634, 811);
            this.dgvNode.TabIndex = 5;
            // 
            // Node
            // 
            this.Node.DataPropertyName = "Joint";
            this.Node.HeaderText = "Joint";
            this.Node.Name = "Node";
            this.Node.Width = 50;
            // 
            // Station1
            // 
            this.Station1.DataPropertyName = "Label";
            this.Station1.HeaderText = "Label";
            this.Station1.Name = "Station1";
            // 
            // Des
            // 
            this.Des.DataPropertyName = "X";
            this.Des.HeaderText = "X";
            this.Des.Name = "Des";
            this.Des.Width = 50;
            // 
            // DC1
            // 
            this.DC1.DataPropertyName = "Y";
            this.DC1.HeaderText = "Y";
            this.DC1.Name = "DC1";
            this.DC1.Width = 50;
            // 
            // Z
            // 
            this.Z.DataPropertyName = "Z";
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.Width = 50;
            // 
            // DC2
            // 
            this.DC2.DataPropertyName = "btop";
            this.DC2.HeaderText = "btop";
            this.DC2.Name = "DC2";
            this.DC2.Width = 50;
            // 
            // DC3
            // 
            this.DC3.DataPropertyName = "ttop";
            this.DC3.HeaderText = "ttop";
            this.DC3.Name = "DC3";
            this.DC3.Width = 50;
            // 
            // DC4
            // 
            this.DC4.DataPropertyName = "ctop";
            this.DC4.HeaderText = "ctop";
            this.DC4.Name = "DC4";
            this.DC4.Width = 50;
            // 
            // DW
            // 
            this.DW.DataPropertyName = "bbot";
            this.DW.HeaderText = "bbot";
            this.DW.Name = "DW";
            this.DW.Width = 50;
            // 
            // LLmax
            // 
            this.LLmax.DataPropertyName = "tbot";
            this.LLmax.HeaderText = "tbot";
            this.LLmax.Name = "LLmax";
            this.LLmax.Width = 50;
            // 
            // A2s
            // 
            this.A2s.DataPropertyName = "cbot";
            this.A2s.HeaderText = "cbot";
            this.A2s.Name = "A2s";
            this.A2s.Width = 50;
            // 
            // D
            // 
            this.D.DataPropertyName = "D";
            this.D.HeaderText = "D";
            this.D.Name = "D";
            this.D.Width = 50;
            // 
            // Iy2s
            // 
            this.Iy2s.DataPropertyName = "tw";
            this.Iy2s.HeaderText = "tw";
            this.Iy2s.Name = "Iy2s";
            this.Iy2s.Width = 50;
            // 
            // A5s
            // 
            this.A5s.DataPropertyName = "S";
            this.A5s.HeaderText = "S";
            this.A5s.Name = "A5s";
            this.A5s.Width = 50;
            // 
            // YU2s
            // 
            this.YU2s.DataPropertyName = "Hc";
            this.YU2s.HeaderText = "Hc";
            this.YU2s.Name = "YU2s";
            this.YU2s.Width = 50;
            // 
            // ts
            // 
            this.ts.DataPropertyName = "ts";
            this.ts.HeaderText = "ts";
            this.ts.Name = "ts";
            this.ts.Width = 50;
            // 
            // w
            // 
            this.w.DataPropertyName = "w";
            this.w.HeaderText = "w";
            this.w.Name = "w";
            this.w.Width = 50;
            // 
            // YL2s
            // 
            this.YL2s.DataPropertyName = "th";
            this.YL2s.HeaderText = "th";
            this.YL2s.Name = "YL2s";
            this.YL2s.Width = 50;
            // 
            // J2s
            // 
            this.J2s.DataPropertyName = "bh";
            this.J2s.HeaderText = "bh";
            this.J2s.Name = "J2s";
            this.J2s.Width = 50;
            // 
            // A2l
            // 
            this.A2l.DataPropertyName = "drt";
            this.A2l.HeaderText = "drt";
            this.A2l.Name = "A2l";
            this.A2l.Width = 50;
            // 
            // Ix2l
            // 
            this.Ix2l.DataPropertyName = "art";
            this.Ix2l.HeaderText = "art";
            this.Ix2l.Name = "Ix2l";
            this.Ix2l.Width = 50;
            // 
            // Iy2l
            // 
            this.Iy2l.DataPropertyName = "crt";
            this.Iy2l.HeaderText = "crt";
            this.Iy2l.Name = "Iy2l";
            this.Iy2l.Width = 50;
            // 
            // YU3
            // 
            this.YU3.DataPropertyName = "drb";
            this.YU3.HeaderText = "drb";
            this.YU3.Name = "YU3";
            this.YU3.Width = 50;
            // 
            // YL3
            // 
            this.YL3.DataPropertyName = "arb";
            this.YL3.HeaderText = "arb";
            this.YL3.Name = "YL3";
            this.YL3.Width = 50;
            // 
            // crb
            // 
            this.crb.DataPropertyName = "crb";
            this.crb.HeaderText = "crb";
            this.crb.Name = "crb";
            this.crb.Width = 50;
            // 
            // J3
            // 
            this.J3.DataPropertyName = "nst";
            this.J3.HeaderText = "nst";
            this.J3.Name = "J3";
            this.J3.Width = 50;
            // 
            // A4
            // 
            this.A4.DataPropertyName = "Hst";
            this.A4.HeaderText = "Hst";
            this.A4.Name = "A4";
            this.A4.Width = 50;
            // 
            // Ix4
            // 
            this.Ix4.DataPropertyName = "tst";
            this.Ix4.HeaderText = "tst";
            this.Ix4.Name = "Ix4";
            this.Ix4.Width = 50;
            // 
            // Iy4
            // 
            this.Iy4.DataPropertyName = "nsb";
            this.Iy4.HeaderText = "nsb";
            this.Iy4.Name = "Iy4";
            this.Iy4.Width = 50;
            // 
            // YU4
            // 
            this.YU4.DataPropertyName = "Hsb";
            this.YU4.HeaderText = "Hsb";
            this.YU4.Name = "YU4";
            this.YU4.Width = 50;
            // 
            // YL4
            // 
            this.YL4.DataPropertyName = "tsb";
            this.YL4.HeaderText = "tsb";
            this.YL4.Name = "YL4";
            this.YL4.Width = 50;
            // 
            // J4
            // 
            this.J4.DataPropertyName = "ns";
            this.J4.HeaderText = "ns";
            this.J4.Name = "J4";
            this.J4.Width = 50;
            // 
            // RNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1634, 811);
            this.Controls.Add(this.dgvNode);
            this.Name = "RNode";
            this.Text = "Sectional Dimensions";
            this.Load += new System.EventHandler(this.RNode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Node;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Des;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DW;
        private System.Windows.Forms.DataGridViewTextBoxColumn LLmax;
        private System.Windows.Forms.DataGridViewTextBoxColumn A2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn D;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn A5s;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn ts;
        private System.Windows.Forms.DataGridViewTextBoxColumn w;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn J2s;
        private System.Windows.Forms.DataGridViewTextBoxColumn A2l;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ix2l;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy2l;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU3;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL3;
        private System.Windows.Forms.DataGridViewTextBoxColumn crb;
        private System.Windows.Forms.DataGridViewTextBoxColumn J3;
        private System.Windows.Forms.DataGridViewTextBoxColumn A4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ix4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Iy4;
        private System.Windows.Forms.DataGridViewTextBoxColumn YU4;
        private System.Windows.Forms.DataGridViewTextBoxColumn YL4;
        private System.Windows.Forms.DataGridViewTextBoxColumn J4;
    }
}