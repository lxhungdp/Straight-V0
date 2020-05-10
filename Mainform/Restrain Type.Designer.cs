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
            this.Free = new System.Windows.Forms.RadioButton();
            this.TranFixed = new System.Windows.Forms.RadioButton();
            this.LongFixed = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fixed
            // 
            this.Fixed.AutoSize = true;
            this.Fixed.Location = new System.Drawing.Point(25, 19);
            this.Fixed.Name = "Fixed";
            this.Fixed.Size = new System.Drawing.Size(133, 17);
            this.Fixed.TabIndex = 2;
            this.Fixed.TabStop = true;
            this.Fixed.Text = "Fixed in both directions";
            this.Fixed.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Free);
            this.groupBox1.Controls.Add(this.TranFixed);
            this.groupBox1.Controls.Add(this.LongFixed);
            this.groupBox1.Controls.Add(this.Fixed);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 115);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select restrain type for support";
            // 
            // Free
            // 
            this.Free.AutoSize = true;
            this.Free.Location = new System.Drawing.Point(25, 88);
            this.Free.Name = "Free";
            this.Free.Size = new System.Drawing.Size(129, 17);
            this.Free.TabIndex = 5;
            this.Free.TabStop = true;
            this.Free.Text = "Free in both directions";
            this.Free.UseVisualStyleBackColor = true;
            // 
            // TranFixed
            // 
            this.TranFixed.AutoSize = true;
            this.TranFixed.Location = new System.Drawing.Point(25, 65);
            this.TranFixed.Name = "TranFixed";
            this.TranFixed.Size = new System.Drawing.Size(250, 17);
            this.TranFixed.TabIndex = 4;
            this.TranFixed.TabStop = true;
            this.TranFixed.Text = "Fixed in transverse, Free in longitudinal direction";
            this.TranFixed.UseVisualStyleBackColor = true;
            // 
            // LongFixed
            // 
            this.LongFixed.AutoSize = true;
            this.LongFixed.Location = new System.Drawing.Point(25, 42);
            this.LongFixed.Name = "LongFixed";
            this.LongFixed.Size = new System.Drawing.Size(250, 17);
            this.LongFixed.TabIndex = 3;
            this.LongFixed.TabStop = true;
            this.LongFixed.Text = "Fixed in longitudinal, Free in transverse direction";
            this.LongFixed.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 134);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 32);
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
            this.btApply.Location = new System.Drawing.Point(212, 134);
            this.btApply.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(94, 32);
            this.btApply.TabIndex = 11;
            this.btApply.Text = "Apply";
            this.btApply.UseVisualStyleBackColor = false;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // fRestrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 174);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.groupBox1);
            this.Name = "fRestrain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Restrain Type";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
    }
}