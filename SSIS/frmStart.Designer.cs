namespace SSIS
{
    partial class frmStart
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
            this.btnSSIS = new System.Windows.Forms.Button();
            this.cmdSymbols = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSSIS
            // 
            this.btnSSIS.Location = new System.Drawing.Point(3, 3);
            this.btnSSIS.Name = "btnSSIS";
            this.btnSSIS.Size = new System.Drawing.Size(110, 23);
            this.btnSSIS.TabIndex = 0;
            this.btnSSIS.Text = "ssis";
            this.btnSSIS.UseVisualStyleBackColor = true;
            this.btnSSIS.Click += new System.EventHandler(this.btnSSIS_Click);
            // 
            // cmdSymbols
            // 
            this.cmdSymbols.Location = new System.Drawing.Point(3, 27);
            this.cmdSymbols.Name = "cmdSymbols";
            this.cmdSymbols.Size = new System.Drawing.Size(110, 23);
            this.cmdSymbols.TabIndex = 1;
            this.cmdSymbols.Text = "Symbols";
            this.cmdSymbols.UseVisualStyleBackColor = true;
            this.cmdSymbols.Click += new System.EventHandler(this.cmdSymbols_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 76);
            this.Controls.Add(this.cmdSymbols);
            this.Controls.Add(this.btnSSIS);
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.Name = "frmStart";
            this.Text = "Dtec";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSSIS;
        private System.Windows.Forms.Button cmdSymbols;
    }
}

