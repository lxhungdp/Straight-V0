namespace Mainform
{
    partial class RForces
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
            this.dgvMST = new System.Windows.Forms.DataGridView();
            this.Node = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DC4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LLmax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LLmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LLfmax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LLfmin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMST)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMST
            // 
            this.dgvMST.AllowUserToAddRows = false;
            this.dgvMST.AllowUserToDeleteRows = false;
            this.dgvMST.AllowUserToResizeColumns = false;
            this.dgvMST.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkCyan;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMST.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMST.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMST.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Node,
            this.Station1,
            this.Des,
            this.DC1,
            this.DC2,
            this.DC3,
            this.DC4,
            this.DW,
            this.LLmax,
            this.LLmin,
            this.LLfmax,
            this.LLfmin});
            this.dgvMST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMST.Location = new System.Drawing.Point(0, 0);
            this.dgvMST.Name = "dgvMST";
            this.dgvMST.RowHeadersVisible = false;
            this.dgvMST.RowTemplate.Height = 23;
            this.dgvMST.Size = new System.Drawing.Size(1027, 639);
            this.dgvMST.TabIndex = 3;
            // 
            // Node
            // 
            this.Node.DataPropertyName = "Node";
            this.Node.HeaderText = "Node";
            this.Node.Name = "Node";
            this.Node.Width = 76;
            // 
            // Station1
            // 
            this.Station1.DataPropertyName = "Station";
            this.Station1.HeaderText = "Station";
            this.Station1.Name = "Station1";
            // 
            // Des
            // 
            this.Des.DataPropertyName = "Description";
            this.Des.HeaderText = "Label";
            this.Des.Name = "Des";
            this.Des.Width = 110;
            // 
            // DC1
            // 
            this.DC1.DataPropertyName = "DC1";
            this.DC1.HeaderText = "DC1";
            this.DC1.Name = "DC1";
            this.DC1.Width = 80;
            // 
            // DC2
            // 
            this.DC2.DataPropertyName = "DC2";
            this.DC2.HeaderText = "DC2";
            this.DC2.Name = "DC2";
            this.DC2.Width = 80;
            // 
            // DC3
            // 
            this.DC3.DataPropertyName = "DC3";
            this.DC3.HeaderText = "DC3";
            this.DC3.Name = "DC3";
            this.DC3.Width = 80;
            // 
            // DC4
            // 
            this.DC4.DataPropertyName = "DC4";
            this.DC4.HeaderText = "DC4";
            this.DC4.Name = "DC4";
            this.DC4.Width = 80;
            // 
            // DW
            // 
            this.DW.DataPropertyName = "DW";
            this.DW.HeaderText = "DW";
            this.DW.Name = "DW";
            this.DW.Width = 80;
            // 
            // LLmax
            // 
            this.LLmax.DataPropertyName = "LLmax";
            this.LLmax.HeaderText = "LLmax";
            this.LLmax.Name = "LLmax";
            this.LLmax.Width = 80;
            // 
            // LLmin
            // 
            this.LLmin.DataPropertyName = "LLmin";
            this.LLmin.HeaderText = "LLmin";
            this.LLmin.Name = "LLmin";
            this.LLmin.Width = 80;
            // 
            // LLfmax
            // 
            this.LLfmax.DataPropertyName = "LLfmax";
            this.LLfmax.HeaderText = "LLmax (Fatigue)";
            this.LLfmax.Name = "LLfmax";
            this.LLfmax.Width = 80;
            // 
            // LLfmin
            // 
            this.LLfmin.DataPropertyName = "LLfmin";
            this.LLfmin.HeaderText = "LLmin (Fatigue)";
            this.LLfmin.Name = "LLfmin";
            this.LLfmin.Width = 80;
            // 
            // RForces
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 639);
            this.Controls.Add(this.dgvMST);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RForces";
            this.Text = "RForces";
            this.Load += new System.EventHandler(this.RForces_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMST)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMST;
        private System.Windows.Forms.DataGridViewTextBoxColumn Node;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Des;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DC4;
        private System.Windows.Forms.DataGridViewTextBoxColumn DW;
        private System.Windows.Forms.DataGridViewTextBoxColumn LLmax;
        private System.Windows.Forms.DataGridViewTextBoxColumn LLmin;
        private System.Windows.Forms.DataGridViewTextBoxColumn LLfmax;
        private System.Windows.Forms.DataGridViewTextBoxColumn LLfmin;
    }
}