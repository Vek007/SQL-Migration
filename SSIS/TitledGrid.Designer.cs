namespace SSIS
{
    partial class TitledGrid
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvChartData = new System.Windows.Forms.DataGridView();
            this.tbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChartData)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMain
            // 
            this.tbMain.ColumnCount = 1;
            this.tbMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbMain.Controls.Add(this.lblTitle, 0, 0);
            this.tbMain.Controls.Add(this.dgvChartData, 0, 1);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(0, 0);
            this.tbMain.Name = "tbMain";
            this.tbMain.RowCount = 2;
            this.tbMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tbMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbMain.Size = new System.Drawing.Size(196, 205);
            this.tbMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(190, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvChartData
            // 
            this.dgvChartData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChartData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChartData.Location = new System.Drawing.Point(3, 33);
            this.dgvChartData.Name = "dgvChartData";
            this.dgvChartData.Size = new System.Drawing.Size(190, 169);
            this.dgvChartData.TabIndex = 2;
            // 
            // TitledGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbMain);
            this.Name = "TitledGrid";
            this.Size = new System.Drawing.Size(196, 205);
            this.tbMain.ResumeLayout(false);
            this.tbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChartData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvChartData;
    }
}
