namespace Checking
{
    partial class Mainform
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectionalPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectionalForcesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sectionalCheckingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.constructibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ultimateLimitStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serviceLimitStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fatigueLimitStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(75, 192);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(303, 265);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.checkingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(686, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inputToolStripMenuItem
            // 
            this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            this.inputToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.inputToolStripMenuItem.Text = "Input";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sectionalPropertiesToolStripMenuItem,
            this.sectionalForcesToolStripMenuItem,
            this.stressToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // sectionalPropertiesToolStripMenuItem
            // 
            this.sectionalPropertiesToolStripMenuItem.Name = "sectionalPropertiesToolStripMenuItem";
            this.sectionalPropertiesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.sectionalPropertiesToolStripMenuItem.Text = "Sectional Properties";
            // 
            // sectionalForcesToolStripMenuItem
            // 
            this.sectionalForcesToolStripMenuItem.Name = "sectionalForcesToolStripMenuItem";
            this.sectionalForcesToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.sectionalForcesToolStripMenuItem.Text = "Sectional Forces";
            // 
            // stressToolStripMenuItem
            // 
            this.stressToolStripMenuItem.Name = "stressToolStripMenuItem";
            this.stressToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.stressToolStripMenuItem.Text = "Stress";
            // 
            // checkingToolStripMenuItem
            // 
            this.checkingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sectionalCheckingToolStripMenuItem});
            this.checkingToolStripMenuItem.Name = "checkingToolStripMenuItem";
            this.checkingToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.checkingToolStripMenuItem.Text = "Checking";
            // 
            // sectionalCheckingToolStripMenuItem
            // 
            this.sectionalCheckingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.constructibilityToolStripMenuItem,
            this.ultimateLimitStateToolStripMenuItem,
            this.serviceLimitStateToolStripMenuItem,
            this.fatigueLimitStateToolStripMenuItem});
            this.sectionalCheckingToolStripMenuItem.Name = "sectionalCheckingToolStripMenuItem";
            this.sectionalCheckingToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.sectionalCheckingToolStripMenuItem.Text = "Sectional Checking";
            // 
            // constructibilityToolStripMenuItem
            // 
            this.constructibilityToolStripMenuItem.Name = "constructibilityToolStripMenuItem";
            this.constructibilityToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.constructibilityToolStripMenuItem.Text = "Constructibility";
            this.constructibilityToolStripMenuItem.Click += new System.EventHandler(this.constructibilityToolStripMenuItem_Click);
            // 
            // ultimateLimitStateToolStripMenuItem
            // 
            this.ultimateLimitStateToolStripMenuItem.Name = "ultimateLimitStateToolStripMenuItem";
            this.ultimateLimitStateToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.ultimateLimitStateToolStripMenuItem.Text = "Ultimate Limit State";
            // 
            // serviceLimitStateToolStripMenuItem
            // 
            this.serviceLimitStateToolStripMenuItem.Name = "serviceLimitStateToolStripMenuItem";
            this.serviceLimitStateToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.serviceLimitStateToolStripMenuItem.Text = "Service Limit State";
            // 
            // fatigueLimitStateToolStripMenuItem
            // 
            this.fatigueLimitStateToolStripMenuItem.Name = "fatigueLimitStateToolStripMenuItem";
            this.fatigueLimitStateToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.fatigueLimitStateToolStripMenuItem.Text = "Fatigue Limit State";
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 488);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUS";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sectionalCheckingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem constructibilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ultimateLimitStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serviceLimitStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fatigueLimitStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sectionalPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sectionalForcesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stressToolStripMenuItem;
    }
}

