namespace Checking
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChartCons1 = new LiveCharts.WinForms.CartesianChart();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.dtgCons2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sc_top = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sc_bot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fnc_LB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fnc_LTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fnc_OF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fcb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fcv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fnc_BF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fnc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Slender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbufl_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check_comOF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fbufl3_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Check_com = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ChartCons2 = new LiveCharts.WinForms.CartesianChart();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCons1)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCons2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1101, 686);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Controls.Add(this.panel3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1093, 660);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Flanges Lateral Bending Stress";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtgCons1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(529, 182);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(561, 475);
            this.panel2.TabIndex = 5;
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
            this.dtgCons1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgCons1.Location = new System.Drawing.Point(0, 0);
            this.dtgCons1.Name = "dtgCons1";
            this.dtgCons1.RowHeadersVisible = false;
            this.dtgCons1.RowHeadersWidth = 60;
            this.dtgCons1.Size = new System.Drawing.Size(561, 475);
            this.dtgCons1.TabIndex = 0;
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
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(3, 182);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(526, 475);
            this.panel3.TabIndex = 4;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(27, 527);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(471, 197);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(27, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(471, 491);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChartCons1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1087, 179);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lateral bending stress ( fl )  and the stress limit (0.6Fy) by Graph";
            // 
            // ChartCons1
            // 
            this.ChartCons1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartCons1.Location = new System.Drawing.Point(3, 16);
            this.ChartCons1.Name = "ChartCons1";
            this.ChartCons1.Size = new System.Drawing.Size(1081, 160);
            this.ChartCons1.TabIndex = 1;
            this.ChartCons1.Text = "cartesianChart1";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.panel4);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1093, 660);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Flange Compression Stress";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.Controls.Add(this.dtgCons2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(515, 163);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(575, 494);
            this.panel5.TabIndex = 7;
            // 
            // dtgCons2
            // 
            this.dtgCons2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgCons2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgCons2.ColumnHeadersHeight = 30;
            this.dtgCons2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Sc_top,
            this.Sc_bot,
            this.Fnc_LB,
            this.Fnc_LTB,
            this.Fnc_OF,
            this.Fcb,
            this.Fcv,
            this.Fnc_BF,
            this.Fnc,
            this.Slender,
            this.fbufl_com,
            this.Check_comOF,
            this.fbufl3_com,
            this.Check_com});
            this.dtgCons2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgCons2.Location = new System.Drawing.Point(0, 0);
            this.dtgCons2.Name = "dtgCons2";
            this.dtgCons2.RowHeadersVisible = false;
            this.dtgCons2.RowHeadersWidth = 60;
            this.dtgCons2.Size = new System.Drawing.Size(575, 494);
            this.dtgCons2.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Label";
            this.dataGridViewTextBoxColumn1.HeaderText = "Label";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Sta";
            this.dataGridViewTextBoxColumn2.HeaderText = "Station";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 50;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Flexure";
            this.dataGridViewTextBoxColumn3.HeaderText = "Flexure";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 70;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // Sc_top
            // 
            this.Sc_top.DataPropertyName = "Sc_top";
            this.Sc_top.HeaderText = "fbu_top";
            this.Sc_top.MinimumWidth = 70;
            this.Sc_top.Name = "Sc_top";
            // 
            // Sc_bot
            // 
            this.Sc_bot.DataPropertyName = "Sc_bot";
            this.Sc_bot.HeaderText = "fbu_bottom";
            this.Sc_bot.MinimumWidth = 70;
            this.Sc_bot.Name = "Sc_bot";
            // 
            // Fnc_LB
            // 
            this.Fnc_LB.DataPropertyName = "Fnc_LB";
            this.Fnc_LB.HeaderText = "Fnc_LB";
            this.Fnc_LB.MinimumWidth = 70;
            this.Fnc_LB.Name = "Fnc_LB";
            // 
            // Fnc_LTB
            // 
            this.Fnc_LTB.DataPropertyName = "Fnc_LTB";
            this.Fnc_LTB.HeaderText = "Fnc_LTB";
            this.Fnc_LTB.MinimumWidth = 70;
            this.Fnc_LTB.Name = "Fnc_LTB";
            // 
            // Fnc_OF
            // 
            this.Fnc_OF.DataPropertyName = "Fnc_OF";
            this.Fnc_OF.HeaderText = "Fnc (OF)";
            this.Fnc_OF.MinimumWidth = 70;
            this.Fnc_OF.Name = "Fnc_OF";
            // 
            // Fcb
            // 
            this.Fcb.DataPropertyName = "Fcb";
            this.Fcb.HeaderText = "Fcb";
            this.Fcb.MinimumWidth = 70;
            this.Fcb.Name = "Fcb";
            // 
            // Fcv
            // 
            this.Fcv.DataPropertyName = "Fcv";
            this.Fcv.HeaderText = "Fcv";
            this.Fcv.MinimumWidth = 70;
            this.Fcv.Name = "Fcv";
            // 
            // Fnc_BF
            // 
            this.Fnc_BF.DataPropertyName = "Fnc_BF";
            this.Fnc_BF.HeaderText = "Fnc (BF)";
            this.Fnc_BF.MinimumWidth = 70;
            this.Fnc_BF.Name = "Fnc_BF";
            // 
            // Fnc
            // 
            this.Fnc.DataPropertyName = "Fnc";
            this.Fnc.HeaderText = "Fnc";
            this.Fnc.MinimumWidth = 70;
            this.Fnc.Name = "Fnc";
            // 
            // Slender
            // 
            this.Slender.DataPropertyName = "Slender";
            this.Slender.HeaderText = "Type of Web";
            this.Slender.MinimumWidth = 100;
            this.Slender.Name = "Slender";
            // 
            // fbufl_com
            // 
            this.fbufl_com.DataPropertyName = "fbufl_com";
            this.fbufl_com.HeaderText = "fbu + fl";
            this.fbufl_com.MinimumWidth = 70;
            this.fbufl_com.Name = "fbufl_com";
            // 
            // Check_comOF
            // 
            this.Check_comOF.DataPropertyName = "Check_comOF";
            this.Check_comOF.HeaderText = "Checking";
            this.Check_comOF.MinimumWidth = 70;
            this.Check_comOF.Name = "Check_comOF";
            // 
            // fbufl3_com
            // 
            this.fbufl3_com.DataPropertyName = "fbufl3_com";
            this.fbufl3_com.HeaderText = "fbu + fl/3";
            this.fbufl3_com.MinimumWidth = 70;
            this.fbufl3_com.Name = "fbufl3_com";
            // 
            // Check_com
            // 
            this.Check_com.DataPropertyName = "Check_com";
            this.Check_com.HeaderText = "Checking";
            this.Check_com.MinimumWidth = 70;
            this.Check_com.Name = "Check_com";
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(3, 163);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(512, 494);
            this.panel4.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(5, 22);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(489, 613);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ChartCons2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1087, 160);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // ChartCons2
            // 
            this.ChartCons2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChartCons2.Location = new System.Drawing.Point(3, 16);
            this.ChartCons2.Name = "ChartCons2";
            this.ChartCons2.Size = new System.Drawing.Size(1081, 141);
            this.ChartCons2.TabIndex = 3;
            this.ChartCons2.Text = "cartesianChart1";
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1093, 660);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Flange Tension Stress";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1093, 660);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Web Shear";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1101, 715);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(943, 757);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Export to Excel";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmCons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 824);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.Name = "frmCons";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Constructibility Checking";
            this.Load += new System.EventHandler(this.Cons_Form_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCons1)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCons2)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private LiveCharts.WinForms.CartesianChart ChartCons1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dtgCons2;
        private LiveCharts.WinForms.CartesianChart ChartCons2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sc_top;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sc_bot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fnc_LB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fnc_LTB;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fnc_OF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fcb;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fcv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fnc_BF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fnc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slender;
        private System.Windows.Forms.DataGridViewTextBoxColumn fbufl_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check_comOF;
        private System.Windows.Forms.DataGridViewTextBoxColumn fbufl3_com;
        private System.Windows.Forms.DataGridViewTextBoxColumn Check_com;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button1;
    }
}