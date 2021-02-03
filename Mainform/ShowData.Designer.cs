namespace Mainform
{
    partial class ShowData
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
            this.gridShowdata = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridShowdata)).BeginInit();
            this.SuspendLayout();
            // 
            // gridShowdata
            // 
            this.gridShowdata.AllowUserToAddRows = false;
            this.gridShowdata.AllowUserToDeleteRows = false;
            this.gridShowdata.AllowUserToResizeColumns = false;
            this.gridShowdata.AllowUserToResizeRows = false;
            this.gridShowdata.ColumnHeadersHeight = 40;
            this.gridShowdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridShowdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridShowdata.Location = new System.Drawing.Point(0, 0);
            this.gridShowdata.Name = "gridShowdata";
            this.gridShowdata.RowHeadersVisible = false;
            this.gridShowdata.Size = new System.Drawing.Size(1438, 568);
            this.gridShowdata.TabIndex = 0;
            // 
            // ShowData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1438, 568);
            this.Controls.Add(this.gridShowdata);
            this.Name = "ShowData";
            this.Text = "ShowData";
            this.Load += new System.EventHandler(this.ShowData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridShowdata)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridShowdata;
    }
}