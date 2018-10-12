namespace SSIS
{
    partial class DashBoard
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
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.dbePer = new SSIS.DashBoardElement();
            this.dbeAr = new SSIS.DashBoardElement();
            this.tblMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 2;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblMain.Controls.Add(this.dbePer, 0, 0);
            this.tblMain.Controls.Add(this.dbeAr, 1, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblMain.Size = new System.Drawing.Size(1004, 396);
            this.tblMain.TabIndex = 0;
            // 
            // dbePer
            // 
            this.dbePer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbePer.Location = new System.Drawing.Point(3, 3);
            this.dbePer.Name = "dbePer";
            this.dbePer.Size = new System.Drawing.Size(496, 390);
            this.dbePer.TabIndex = 0;
            // 
            // dbeAr
            // 
            this.dbeAr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbeAr.Location = new System.Drawing.Point(505, 3);
            this.dbeAr.Name = "dbeAr";
            this.dbeAr.Size = new System.Drawing.Size(496, 390);
            this.dbeAr.TabIndex = 1;
            // 
            // DashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 396);
            this.Controls.Add(this.tblMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DashBoard";
            this.Text = "Dtec";
            this.tblMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private DashBoardElement dbePer;
        private DashBoardElement dbeAr;
    }
}