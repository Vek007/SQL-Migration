namespace SQL_Migration
{
    partial class SqlResults
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabSqlResults = new System.Windows.Forms.TabControl();
            this.tpSqlResults = new System.Windows.Forms.TabPage();
            this.dgvSQLResults = new System.Windows.Forms.DataGridView();
            this.tpMessages = new System.Windows.Forms.TabPage();
            this.txtSQLMessages = new System.Windows.Forms.TextBox();
            this.tbInputQuery = new System.Windows.Forms.TabPage();
            this.txtInputQuery = new System.Windows.Forms.TextBox();
            this.tbTranspose = new System.Windows.Forms.TabPage();
            this.dgvTranspose = new System.Windows.Forms.DataGridView();
            this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpConnString = new System.Windows.Forms.TabPage();
            this.txtConnString = new System.Windows.Forms.TextBox();
            this.lblIncrease = new System.Windows.Forms.Label();
            this.lblDecreseSize = new System.Windows.Forms.Label();
            this.tmrIncSize = new System.Windows.Forms.Timer(this.components);
            this.tmrDecSize = new System.Windows.Forms.Timer(this.components);
            this.cnxtMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSqlResults.SuspendLayout();
            this.tpSqlResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSQLResults)).BeginInit();
            this.tpMessages.SuspendLayout();
            this.tbInputQuery.SuspendLayout();
            this.tbTranspose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTranspose)).BeginInit();
            this.tpConnString.SuspendLayout();
            this.cnxtMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSqlResults
            // 
            this.tabSqlResults.Controls.Add(this.tpSqlResults);
            this.tabSqlResults.Controls.Add(this.tpMessages);
            this.tabSqlResults.Controls.Add(this.tbInputQuery);
            this.tabSqlResults.Controls.Add(this.tbTranspose);
            this.tabSqlResults.Controls.Add(this.tpConnString);
            this.tabSqlResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSqlResults.Location = new System.Drawing.Point(0, 0);
            this.tabSqlResults.Name = "tabSqlResults";
            this.tabSqlResults.SelectedIndex = 0;
            this.tabSqlResults.Size = new System.Drawing.Size(1328, 159);
            this.tabSqlResults.TabIndex = 0;
            this.tabSqlResults.DoubleClick += new System.EventHandler(this.tabSqlResults_DoubleClick);
            // 
            // tpSqlResults
            // 
            this.tpSqlResults.Controls.Add(this.dgvSQLResults);
            this.tpSqlResults.Location = new System.Drawing.Point(4, 22);
            this.tpSqlResults.Name = "tpSqlResults";
            this.tpSqlResults.Padding = new System.Windows.Forms.Padding(3);
            this.tpSqlResults.Size = new System.Drawing.Size(1320, 133);
            this.tpSqlResults.TabIndex = 0;
            this.tpSqlResults.Text = "Results";
            this.tpSqlResults.UseVisualStyleBackColor = true;
            // 
            // dgvSQLResults
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dgvSQLResults.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSQLResults.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvSQLResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSQLResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSQLResults.Location = new System.Drawing.Point(3, 3);
            this.dgvSQLResults.Name = "dgvSQLResults";
            this.dgvSQLResults.ReadOnly = true;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Green;
            this.dgvSQLResults.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSQLResults.Size = new System.Drawing.Size(1314, 127);
            this.dgvSQLResults.TabIndex = 0;
            this.dgvSQLResults.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSQLResults_DataError);
            this.dgvSQLResults.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvSQLResults_RowStateChanged);
            // 
            // tpMessages
            // 
            this.tpMessages.Controls.Add(this.txtSQLMessages);
            this.tpMessages.Location = new System.Drawing.Point(4, 22);
            this.tpMessages.Name = "tpMessages";
            this.tpMessages.Padding = new System.Windows.Forms.Padding(3);
            this.tpMessages.Size = new System.Drawing.Size(1320, 133);
            this.tpMessages.TabIndex = 1;
            this.tpMessages.Text = "Messages";
            this.tpMessages.UseVisualStyleBackColor = true;
            // 
            // txtSQLMessages
            // 
            this.txtSQLMessages.BackColor = System.Drawing.Color.Black;
            this.txtSQLMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLMessages.ForeColor = System.Drawing.Color.Lime;
            this.txtSQLMessages.Location = new System.Drawing.Point(3, 3);
            this.txtSQLMessages.Multiline = true;
            this.txtSQLMessages.Name = "txtSQLMessages";
            this.txtSQLMessages.ReadOnly = true;
            this.txtSQLMessages.Size = new System.Drawing.Size(1314, 127);
            this.txtSQLMessages.TabIndex = 0;
            this.txtSQLMessages.ClientSizeChanged += new System.EventHandler(this.txtSQLMessages_ClientSizeChanged);
            this.txtSQLMessages.TextChanged += new System.EventHandler(this.txtSQLMessages_TextChanged);
            // 
            // tbInputQuery
            // 
            this.tbInputQuery.Controls.Add(this.txtInputQuery);
            this.tbInputQuery.Location = new System.Drawing.Point(4, 22);
            this.tbInputQuery.Name = "tbInputQuery";
            this.tbInputQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tbInputQuery.Size = new System.Drawing.Size(1320, 133);
            this.tbInputQuery.TabIndex = 2;
            this.tbInputQuery.Text = "Query";
            this.tbInputQuery.UseVisualStyleBackColor = true;
            // 
            // txtInputQuery
            // 
            this.txtInputQuery.BackColor = System.Drawing.Color.Black;
            this.txtInputQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputQuery.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInputQuery.ForeColor = System.Drawing.Color.Lime;
            this.txtInputQuery.Location = new System.Drawing.Point(3, 3);
            this.txtInputQuery.Multiline = true;
            this.txtInputQuery.Name = "txtInputQuery";
            this.txtInputQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInputQuery.Size = new System.Drawing.Size(1314, 127);
            this.txtInputQuery.TabIndex = 1;
            this.txtInputQuery.DoubleClick += new System.EventHandler(this.txtInputQuery_DoubleClick);
            this.txtInputQuery.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInputQuery_PreviewKeyDown);
            // 
            // tbTranspose
            // 
            this.tbTranspose.Controls.Add(this.dgvTranspose);
            this.tbTranspose.Location = new System.Drawing.Point(4, 22);
            this.tbTranspose.Name = "tbTranspose";
            this.tbTranspose.Padding = new System.Windows.Forms.Padding(3);
            this.tbTranspose.Size = new System.Drawing.Size(1320, 133);
            this.tbTranspose.TabIndex = 3;
            this.tbTranspose.Text = "Transposed";
            this.tbTranspose.UseVisualStyleBackColor = true;
            // 
            // dgvTranspose
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Green;
            this.dgvTranspose.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTranspose.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvTranspose.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTranspose.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colField,
            this.colValue});
            this.dgvTranspose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTranspose.Location = new System.Drawing.Point(3, 3);
            this.dgvTranspose.Name = "dgvTranspose";
            this.dgvTranspose.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.dgvTranspose.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTranspose.Size = new System.Drawing.Size(1314, 127);
            this.dgvTranspose.TabIndex = 1;
            // 
            // colField
            // 
            this.colField.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colField.DefaultCellStyle = dataGridViewCellStyle4;
            this.colField.HeaderText = "Column";
            this.colField.Name = "colField";
            this.colField.ReadOnly = true;
            // 
            // colValue
            // 
            this.colValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colValue.DefaultCellStyle = dataGridViewCellStyle5;
            this.colValue.HeaderText = "Value";
            this.colValue.Name = "colValue";
            this.colValue.ReadOnly = true;
            // 
            // tpConnString
            // 
            this.tpConnString.Controls.Add(this.txtConnString);
            this.tpConnString.Location = new System.Drawing.Point(4, 22);
            this.tpConnString.Name = "tpConnString";
            this.tpConnString.Padding = new System.Windows.Forms.Padding(3);
            this.tpConnString.Size = new System.Drawing.Size(1320, 133);
            this.tpConnString.TabIndex = 4;
            this.tpConnString.Text = "Connection String";
            this.tpConnString.UseVisualStyleBackColor = true;
            // 
            // txtConnString
            // 
            this.txtConnString.BackColor = System.Drawing.Color.Black;
            this.txtConnString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnString.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnString.ForeColor = System.Drawing.Color.Lime;
            this.txtConnString.Location = new System.Drawing.Point(3, 3);
            this.txtConnString.Multiline = true;
            this.txtConnString.Name = "txtConnString";
            this.txtConnString.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConnString.Size = new System.Drawing.Size(1314, 127);
            this.txtConnString.TabIndex = 2;
            this.txtConnString.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtConnString_MouseClick);
            // 
            // lblIncrease
            // 
            this.lblIncrease.AutoSize = true;
            this.lblIncrease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblIncrease.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncrease.Location = new System.Drawing.Point(338, 0);
            this.lblIncrease.Margin = new System.Windows.Forms.Padding(0);
            this.lblIncrease.Name = "lblIncrease";
            this.lblIncrease.Size = new System.Drawing.Size(18, 20);
            this.lblIncrease.TabIndex = 1;
            this.lblIncrease.Text = "+";
            this.lblIncrease.MouseEnter += new System.EventHandler(this.lblIncrease_MouseEnter);
            this.lblIncrease.MouseLeave += new System.EventHandler(this.lblIncrease_MouseLeave);
            // 
            // lblDecreseSize
            // 
            this.lblDecreseSize.AutoSize = true;
            this.lblDecreseSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDecreseSize.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDecreseSize.Location = new System.Drawing.Point(360, -1);
            this.lblDecreseSize.Margin = new System.Windows.Forms.Padding(0);
            this.lblDecreseSize.Name = "lblDecreseSize";
            this.lblDecreseSize.Size = new System.Drawing.Size(14, 20);
            this.lblDecreseSize.TabIndex = 2;
            this.lblDecreseSize.Text = "-";
            this.lblDecreseSize.MouseEnter += new System.EventHandler(this.lblDecreseSize_MouseEnter);
            this.lblDecreseSize.MouseLeave += new System.EventHandler(this.lblDecreseSize_MouseLeave);
            // 
            // tmrIncSize
            // 
            this.tmrIncSize.Interval = 1000;
            this.tmrIncSize.Tick += new System.EventHandler(this.tmrIncSize_Tick);
            // 
            // tmrDecSize
            // 
            this.tmrDecSize.Interval = 500;
            this.tmrDecSize.Tick += new System.EventHandler(this.tmrDecSize_Tick);
            // 
            // cnxtMenu
            // 
            this.cnxtMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRefresh});
            this.cnxtMenu.Name = "cnxtMenu";
            this.cnxtMenu.Size = new System.Drawing.Size(114, 26);
            // 
            // miRefresh
            // 
            this.miRefresh.Name = "miRefresh";
            this.miRefresh.Size = new System.Drawing.Size(113, 22);
            this.miRefresh.Text = "Refresh";
            this.miRefresh.Click += new System.EventHandler(this.MiRefresh_Click);
            // 
            // SqlResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.lblDecreseSize);
            this.Controls.Add(this.lblIncrease);
            this.Controls.Add(this.tabSqlResults);
            this.Name = "SqlResults";
            this.Size = new System.Drawing.Size(1328, 159);
            this.tabSqlResults.ResumeLayout(false);
            this.tpSqlResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSQLResults)).EndInit();
            this.tpMessages.ResumeLayout(false);
            this.tpMessages.PerformLayout();
            this.tbInputQuery.ResumeLayout(false);
            this.tbInputQuery.PerformLayout();
            this.tbTranspose.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTranspose)).EndInit();
            this.tpConnString.ResumeLayout(false);
            this.tpConnString.PerformLayout();
            this.cnxtMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabSqlResults;
        private System.Windows.Forms.TabPage tpSqlResults;
        private System.Windows.Forms.TabPage tpMessages;
        private System.Windows.Forms.DataGridView dgvSQLResults;
        private System.Windows.Forms.TextBox txtSQLMessages;
        private System.Windows.Forms.TabPage tbInputQuery;
        private System.Windows.Forms.TextBox txtInputQuery;
        private System.Windows.Forms.TabPage tbTranspose;
        private System.Windows.Forms.DataGridView dgvTranspose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private System.Windows.Forms.Label lblIncrease;
        private System.Windows.Forms.Label lblDecreseSize;
        private System.Windows.Forms.Timer tmrIncSize;
        private System.Windows.Forms.Timer tmrDecSize;
        private System.Windows.Forms.TabPage tpConnString;
        private System.Windows.Forms.TextBox txtConnString;
        private System.Windows.Forms.ContextMenuStrip cnxtMenu;
        private System.Windows.Forms.ToolStripMenuItem miRefresh;
    }
}
