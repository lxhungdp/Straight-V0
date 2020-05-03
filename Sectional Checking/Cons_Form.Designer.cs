namespace Sectional_Checking
{
    partial class frmCons
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChartCons1 = new LiveCharts.WinForms.CartesianChart();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dtgCons1 = new System.Windows.Forms.DataGridView();
            this.Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flexure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mlw = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mlo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mlf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mlc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fy06 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check_fl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCons1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChartCons1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1188, 179);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lateral bending stress ( fl )  and the stress limit (0.6Fy) by Graph";
            // 
            // ChartCons1
            // 
            this.ChartCons1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartCons1.Location = new System.Drawing.Point(3, 16);
            this.ChartCons1.Name = "ChartCons1";
            this.ChartCons1.Size = new System.Drawing.Size(1182, 160);
            this.ChartCons1.TabIndex = 1;
            this.ChartCons1.Text = "cartesianChart1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(3, 182);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(471, 519);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // dtgCons1
            // 
            this.dtgCons1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgCons1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgCons1.ColumnHeadersHeight = 30;
            this.dtgCons1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Label,
            this.Station,
            this.Flexure,
            this.Mlw,
            this.Mlo,
            this.Mlf,
            this.Mlc,
            this.fl,
            this.Fy06,
            this.Check_fl,
            this.Ratio});
            this.dtgCons1.Location = new System.Drawing.Point(480, 182);
            this.dtgCons1.Name = "dtgCons1";
            this.dtgCons1.RowHeadersVisible = false;
            this.dtgCons1.RowHeadersWidth = 60;
            this.dtgCons1.Size = new System.Drawing.Size(696, 519);
            this.dtgCons1.TabIndex = 5;
            // 
            // Label
            // 
            this.Label.DataPropertyName = "Label";
            this.Label.HeaderText = "Label";
            this.Label.MinimumWidth = 50;
            this.Label.Name = "Label";
            // 
            // Station
            // 
            this.Station.DataPropertyName = "Sta";
            this.Station.HeaderText = "Station";
            this.Station.MinimumWidth = 50;
            this.Station.Name = "Station";
            // 
            // Flexure
            // 
            this.Flexure.DataPropertyName = "Flexure";
            this.Flexure.HeaderText = "Flexure";
            this.Flexure.MinimumWidth = 70;
            this.Flexure.Name = "Flexure";
            // 
            // Mlw
            // 
            this.Mlw.DataPropertyName = "Mlw";
            this.Mlw.HeaderText = "Mlw";
            this.Mlw.MinimumWidth = 50;
            this.Mlw.Name = "Mlw";
            // 
            // Mlo
            // 
            this.Mlo.DataPropertyName = "Mlo";
            this.Mlo.HeaderText = "Mlo";
            this.Mlo.MinimumWidth = 50;
            this.Mlo.Name = "Mlo";
            // 
            // Mlf
            // 
            this.Mlf.DataPropertyName = "Mlf";
            this.Mlf.HeaderText = "Mlf";
            this.Mlf.MinimumWidth = 50;
            this.Mlf.Name = "Mlf";
            // 
            // Mlc
            // 
            this.Mlc.DataPropertyName = "Mlc";
            this.Mlc.HeaderText = "Mlc";
            this.Mlc.MinimumWidth = 50;
            this.Mlc.Name = "Mlc";
            // 
            // fl
            // 
            this.fl.DataPropertyName = "fl";
            this.fl.HeaderText = "fl";
            this.fl.MinimumWidth = 50;
            this.fl.Name = "fl";
            // 
            // Fy06
            // 
            this.Fy06.DataPropertyName = "fy06";
            this.Fy06.HeaderText = "0.6Fy";
            this.Fy06.MinimumWidth = 50;
            this.Fy06.Name = "Fy06";
            // 
            // Check_fl
            // 
            this.Check_fl.DataPropertyName = "Check_fl";
            this.Check_fl.HeaderText = "Checking";
            this.Check_fl.MinimumWidth = 50;
            this.Check_fl.Name = "Check_fl";
            // 
            // Ratio
            // 
            this.Ratio.DataPropertyName = "Check_fl_ratio";
            this.Ratio.HeaderText = "Ratio";
            this.Ratio.MinimumWidth = 50;
            this.Ratio.Name = "Ratio";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1013, 708);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 29);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 741);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtgCons1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCons";
            this.Text = "Cons_Form";
            this.Load += new System.EventHandler(this.frmCons_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCons1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private LiveCharts.WinForms.CartesianChart ChartCons1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dtgCons1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Label;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flexure;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mlw;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mlo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mlf;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mlc;
        private System.Windows.Forms.DataGridViewTextBoxColumn fl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fy06;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check_fl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ratio;
        private System.Windows.Forms.Button button1;
    }
}