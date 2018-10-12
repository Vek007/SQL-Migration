namespace SSIS
{
    partial class frmSQLResults
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQLResults));
            this.sqlResult = new SqlResults();
            this.fromTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // sqlResult
            // 
            this.sqlResult.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.sqlResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sqlResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlResult.Location = new System.Drawing.Point(0, 0);
            this.sqlResult.Name = "sqlResult";
            this.sqlResult.Size = new System.Drawing.Size(754, 421);
            this.sqlResult.TabIndex = 0;
            // 
            // fromTip
            // 
            this.fromTip.AutoPopDelay = 1000;
            this.fromTip.InitialDelay = 500;
            this.fromTip.IsBalloon = true;
            this.fromTip.ReshowDelay = 100;
            this.fromTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.fromTip.ToolTipTitle = "SQL Query";
            // 
            // frmSQLResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 421);
            this.Controls.Add(this.sqlResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSQLResults";
            this.Text = "SQL Results";
            this.ResumeLayout(false);

        }

        #endregion

        private SqlResults sqlResult;
        private System.Windows.Forms.ToolTip fromTip;
    }
}