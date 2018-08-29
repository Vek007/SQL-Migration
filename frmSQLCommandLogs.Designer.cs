namespace SQL_Migration
{
    partial class frmSQLCommandLogs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQLCommandLogs));
            this.fromTip = new System.Windows.Forms.ToolTip(this.components);
            this.txtSqlCommands = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
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
            // txtSqlCommands
            // 
            this.txtSqlCommands.BackColor = System.Drawing.Color.Black;
            this.txtSqlCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSqlCommands.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSqlCommands.ForeColor = System.Drawing.Color.Lime;
            this.txtSqlCommands.Location = new System.Drawing.Point(0, 0);
            this.txtSqlCommands.Multiline = true;
            this.txtSqlCommands.Name = "txtSqlCommands";
            this.txtSqlCommands.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSqlCommands.Size = new System.Drawing.Size(754, 421);
            this.txtSqlCommands.TabIndex = 0;
            // 
            // frmSQLCommandLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 421);
            this.Controls.Add(this.txtSqlCommands);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSQLCommandLogs";
            this.Text = "SQL Command Logs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip fromTip;
        private System.Windows.Forms.TextBox txtSqlCommands;
    }
}