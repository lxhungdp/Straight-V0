namespace Mainform
{
    partial class fRestrain
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
            this.Fixed = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LongFixed = new System.Windows.Forms.RadioButton();
            this.Free = new System.Windows.Forms.RadioButton();
            this.TranFixed = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fixed
            // 
            this.Fixed.AutoSize = true;
            this.Fixed.Location = new System.Drawing.Point(3, 13);
            this.Fixed.Name = "Fixed";
            this.Fixed.Size = new System.Drawing.Size(14, 13);
            this.Fixed.TabIndex = 2;
            this.Fixed.TabStop = true;
            this.Fixed.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(14, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 140);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select restrain type for support";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(37, 18);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(27, 106);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "Free in both directions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Fixed in transverse, Free in longitudinal direction";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Fixed in longitudinal, Free in transverse direction";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Fixed in both directions";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LongFixed);
            this.panel1.Controls.Add(this.Free);
            this.panel1.Controls.Add(this.Fixed);
            this.panel1.Controls.Add(this.TranFixed);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(21, 122);
            this.panel1.TabIndex = 6;
            // 
            // LongFixed
            // 
            this.LongFixed.AutoSize = true;
            this.LongFixed.Location = new System.Drawing.Point(3, 39);
            this.LongFixed.Name = "LongFixed";
            this.LongFixed.Size = new System.Drawing.Size(14, 13);
            this.LongFixed.TabIndex = 3;
            this.LongFixed.TabStop = true;
            this.LongFixed.UseVisualStyleBackColor = true;
            // 
            // Free
            // 
            this.Free.AutoSize = true;
            this.Free.Location = new System.Drawing.Point(3, 93);
            this.Free.Name = "Free";
            this.Free.Size = new System.Drawing.Size(14, 13);
            this.Free.TabIndex = 5;
            this.Free.TabStop = true;
            this.Free.UseVisualStyleBackColor = true;
            // 
            // TranFixed
            // 
            this.TranFixed.AutoSize = true;
            this.TranFixed.Location = new System.Drawing.Point(3, 66);
            this.TranFixed.Name = "TranFixed";
            this.TranFixed.Size = new System.Drawing.Size(14, 13);
            this.TranFixed.TabIndex = 4;
            this.TranFixed.TabStop = true;
            this.TranFixed.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(15, 158);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 30);
            this.button1.TabIndex = 12;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btApply
            // 
            this.btApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btApply.FlatAppearance.BorderSize = 0;
            this.btApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btApply.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btApply.ForeColor = System.Drawing.Color.White;
            this.btApply.Location = new System.Drawing.Point(346, 158);
            this.btApply.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(110, 30);
            this.btApply.TabIndex = 11;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = false;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // fRestrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(469, 197);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.groupBox1);
            this.Name = "fRestrain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Restrain Type";
            this.Load += new System.EventHandler(this.fRestrain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton Fixed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton Free;
        private System.Windows.Forms.RadioButton TranFixed;
        private System.Windows.Forms.RadioButton LongFixed;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}