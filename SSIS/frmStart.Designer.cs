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
            this.btnImportAr = new System.Windows.Forms.Button();
            this.cmdSymbols = new System.Windows.Forms.Button();
            this.btnPer = new System.Windows.Forms.Button();
            this.btnImpSymbol = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportAr
            // 
            this.btnImportAr.Location = new System.Drawing.Point(3, 3);
            this.btnImportAr.Name = "btnImportAr";
            this.btnImportAr.Size = new System.Drawing.Size(110, 23);
            this.btnImportAr.TabIndex = 0;
            this.btnImportAr.Text = "An Res";
            this.btnImportAr.UseVisualStyleBackColor = true;
            this.btnImportAr.Click += new System.EventHandler(this.btnSSIS_Click);
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
            // btnPer
            // 
            this.btnPer.Location = new System.Drawing.Point(3, 56);
            this.btnPer.Name = "btnPer";
            this.btnPer.Size = new System.Drawing.Size(110, 23);
            this.btnPer.TabIndex = 2;
            this.btnPer.Text = "Per";
            this.btnPer.UseVisualStyleBackColor = true;
            this.btnPer.Click += new System.EventHandler(this.btnPer_Click);
            // 
            // btnImpSymbol
            // 
            this.btnImpSymbol.Location = new System.Drawing.Point(3, 82);
            this.btnImpSymbol.Name = "btnImpSymbol";
            this.btnImpSymbol.Size = new System.Drawing.Size(110, 23);
            this.btnImpSymbol.TabIndex = 3;
            this.btnImpSymbol.Text = "Import Sym";
            this.btnImpSymbol.UseVisualStyleBackColor = true;
            this.btnImpSymbol.Click += new System.EventHandler(this.btnImpSymbol_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(120, 112);
            this.Controls.Add(this.btnImpSymbol);
            this.Controls.Add(this.btnPer);
            this.Controls.Add(this.cmdSymbols);
            this.Controls.Add(this.btnImportAr);
            this.ForeColor = System.Drawing.Color.DarkCyan;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmStart";
            this.Text = "Dtec";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnImportAr;
        private System.Windows.Forms.Button cmdSymbols;
        private System.Windows.Forms.Button btnPer;
        private System.Windows.Forms.Button btnImpSymbol;
    }
}

