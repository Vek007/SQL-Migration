namespace SSIS
{
    partial class DashBoardElement
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
            this.tbMain = new System.Windows.Forms.TabControl();
            this.tbChart = new System.Windows.Forms.TabPage();
            this.tbGrid = new System.Windows.Forms.TabPage();
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tChart = new SSIS.TitledChart();
            this.tGrid = new SSIS.TitledGrid();
            this.tbMain.SuspendLayout();
            this.tbChart.SuspendLayout();
            this.tbGrid.SuspendLayout();
            this.tblMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMain
            // 
            this.tbMain.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tbMain.Controls.Add(this.tbChart);
            this.tbMain.Controls.Add(this.tbGrid);
            this.tbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMain.Location = new System.Drawing.Point(3, 33);
            this.tbMain.Name = "tbMain";
            this.tbMain.SelectedIndex = 0;
            this.tbMain.Size = new System.Drawing.Size(258, 199);
            this.tbMain.TabIndex = 0;
            // 
            // tbChart
            // 
            this.tbChart.Controls.Add(this.tChart);
            this.tbChart.Location = new System.Drawing.Point(4, 25);
            this.tbChart.Name = "tbChart";
            this.tbChart.Padding = new System.Windows.Forms.Padding(3);
            this.tbChart.Size = new System.Drawing.Size(250, 170);
            this.tbChart.TabIndex = 0;
            this.tbChart.Text = "C";
            this.tbChart.UseVisualStyleBackColor = true;
            // 
            // tbGrid
            // 
            this.tbGrid.Controls.Add(this.tGrid);
            this.tbGrid.Location = new System.Drawing.Point(4, 25);
            this.tbGrid.Name = "tbGrid";
            this.tbGrid.Padding = new System.Windows.Forms.Padding(3);
            this.tbGrid.Size = new System.Drawing.Size(250, 170);
            this.tbGrid.TabIndex = 1;
            this.tbGrid.Text = "G";
            this.tbGrid.UseVisualStyleBackColor = true;
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Controls.Add(this.tbMain, 0, 1);
            this.tblMain.Controls.Add(this.lblTitle, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 2;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(264, 235);
            this.tblMain.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(258, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "T";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tChart
            // 
            this.tChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tChart.Location = new System.Drawing.Point(3, 3);
            this.tChart.Name = "tChart";
            this.tChart.Size = new System.Drawing.Size(244, 164);
            this.tChart.TabIndex = 0;
            // 
            // tGrid
            // 
            this.tGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tGrid.Location = new System.Drawing.Point(3, 3);
            this.tGrid.Name = "tGrid";
            this.tGrid.Size = new System.Drawing.Size(244, 164);
            this.tGrid.TabIndex = 0;
            // 
            // DashBoardElement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tblMain);
            this.Name = "DashBoardElement";
            this.Size = new System.Drawing.Size(264, 235);
            this.tbMain.ResumeLayout(false);
            this.tbChart.ResumeLayout(false);
            this.tbGrid.ResumeLayout(false);
            this.tblMain.ResumeLayout(false);
            this.tblMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbMain;
        private System.Windows.Forms.TabPage tbChart;
        private System.Windows.Forms.TabPage tbGrid;
        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.Label lblTitle;
        private TitledChart tChart;
        private TitledGrid tGrid;
    }
}
