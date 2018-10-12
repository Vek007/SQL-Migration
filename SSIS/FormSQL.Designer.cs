using System;

namespace SSIS
{
    partial class FormSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSQL));
            this.tblMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabOperations = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pnlWaitQuery = new System.Windows.Forms.Panel();
            this.txtQueryWaitMsg = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.pnlSQLFlow = new System.Windows.Forms.Panel();
            this.flLayoyt = new System.Windows.Forms.FlowLayoutPanel();
            this.grpTools = new System.Windows.Forms.GroupBox();
            this.chkDb = new System.Windows.Forms.CheckBox();
            this.btnSSIS = new System.Windows.Forms.Button();
            this.btnSearchSym = new System.Windows.Forms.Button();
            this.txtSym = new System.Windows.Forms.TextBox();
            this.btnSQLResultWindow = new System.Windows.Forms.Button();
            this.btnOpenSQLLogs = new System.Windows.Forms.Button();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.chkSqlServer = new System.Windows.Forms.CheckBox();
            this.chkSemicolon = new System.Windows.Forms.CheckBox();
            this.chkClearOldQueryResults = new System.Windows.Forms.CheckBox();
            this.tblMain.SuspendLayout();
            this.tabOperations.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlWaitQuery.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlSQLFlow.SuspendLayout();
            this.grpTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblMain
            // 
            this.tblMain.ColumnCount = 1;
            this.tblMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Controls.Add(this.tabOperations, 0, 0);
            this.tblMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblMain.Location = new System.Drawing.Point(0, 0);
            this.tblMain.Name = "tblMain";
            this.tblMain.RowCount = 1;
            this.tblMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblMain.Size = new System.Drawing.Size(1428, 837);
            this.tblMain.TabIndex = 0;
            // 
            // tabOperations
            // 
            this.tabOperations.Controls.Add(this.tabPage1);
            this.tabOperations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOperations.Location = new System.Drawing.Point(3, 3);
            this.tabOperations.Name = "tabOperations";
            this.tabOperations.SelectedIndex = 0;
            this.tabOperations.Size = new System.Drawing.Size(1422, 831);
            this.tabOperations.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pnlWaitQuery);
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1414, 805);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Query";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pnlWaitQuery
            // 
            this.pnlWaitQuery.Controls.Add(this.txtQueryWaitMsg);
            this.pnlWaitQuery.Location = new System.Drawing.Point(423, 124);
            this.pnlWaitQuery.Name = "pnlWaitQuery";
            this.pnlWaitQuery.Size = new System.Drawing.Size(591, 100);
            this.pnlWaitQuery.TabIndex = 9;
            this.pnlWaitQuery.Visible = false;
            // 
            // txtQueryWaitMsg
            // 
            this.txtQueryWaitMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQueryWaitMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQueryWaitMsg.Location = new System.Drawing.Point(184, 37);
            this.txtQueryWaitMsg.Name = "txtQueryWaitMsg";
            this.txtQueryWaitMsg.Size = new System.Drawing.Size(223, 24);
            this.txtQueryWaitMsg.TabIndex = 0;
            this.txtQueryWaitMsg.Text = "Processing...";
            this.txtQueryWaitMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtQuery, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlSQLFlow, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpTools, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.36161F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.63839F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1408, 799);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // txtQuery
            // 
            this.txtQuery.BackColor = System.Drawing.Color.Black;
            this.txtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuery.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.ForeColor = System.Drawing.Color.Lime;
            this.txtQuery.Location = new System.Drawing.Point(3, 45);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(1402, 231);
            this.txtQuery.TabIndex = 0;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            this.txtQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuery_KeyDown);
            // 
            // pnlSQLFlow
            // 
            this.pnlSQLFlow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSQLFlow.Controls.Add(this.flLayoyt);
            this.pnlSQLFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSQLFlow.Location = new System.Drawing.Point(3, 282);
            this.pnlSQLFlow.Name = "pnlSQLFlow";
            this.pnlSQLFlow.Size = new System.Drawing.Size(1402, 514);
            this.pnlSQLFlow.TabIndex = 8;
            // 
            // flLayoyt
            // 
            this.flLayoyt.AutoScroll = true;
            this.flLayoyt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flLayoyt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flLayoyt.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flLayoyt.Location = new System.Drawing.Point(0, 0);
            this.flLayoyt.MinimumSize = new System.Drawing.Size(1000, 300);
            this.flLayoyt.Name = "flLayoyt";
            this.flLayoyt.Size = new System.Drawing.Size(1398, 510);
            this.flLayoyt.TabIndex = 7;
            this.flLayoyt.WrapContents = false;
            // 
            // grpTools
            // 
            this.grpTools.Controls.Add(this.chkDb);
            this.grpTools.Controls.Add(this.btnSSIS);
            this.grpTools.Controls.Add(this.btnSearchSym);
            this.grpTools.Controls.Add(this.txtSym);
            this.grpTools.Controls.Add(this.btnSQLResultWindow);
            this.grpTools.Controls.Add(this.btnOpenSQLLogs);
            this.grpTools.Controls.Add(this.btnExecuteQuery);
            this.grpTools.Controls.Add(this.chkSqlServer);
            this.grpTools.Controls.Add(this.chkSemicolon);
            this.grpTools.Controls.Add(this.chkClearOldQueryResults);
            this.grpTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTools.Location = new System.Drawing.Point(3, 3);
            this.grpTools.Name = "grpTools";
            this.grpTools.Size = new System.Drawing.Size(1402, 36);
            this.grpTools.TabIndex = 9;
            this.grpTools.TabStop = false;
            // 
            // chkDb
            // 
            this.chkDb.AutoSize = true;
            this.chkDb.Checked = true;
            this.chkDb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDb.Location = new System.Drawing.Point(467, 13);
            this.chkDb.Name = "chkDb";
            this.chkDb.Size = new System.Drawing.Size(41, 17);
            this.chkDb.TabIndex = 18;
            this.chkDb.Text = "DB";
            this.chkDb.UseVisualStyleBackColor = true;
            // 
            // btnSSIS
            // 
            this.btnSSIS.Image = ((System.Drawing.Image)(resources.GetObject("btnSSIS.Image")));
            this.btnSSIS.Location = new System.Drawing.Point(642, 9);
            this.btnSSIS.Name = "btnSSIS";
            this.btnSSIS.Size = new System.Drawing.Size(25, 24);
            this.btnSSIS.TabIndex = 17;
            this.btnSSIS.UseVisualStyleBackColor = true;
            this.btnSSIS.Click += new System.EventHandler(this.btnSSIS_Click);
            // 
            // btnSearchSym
            // 
            this.btnSearchSym.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchSym.Image")));
            this.btnSearchSym.Location = new System.Drawing.Point(611, 9);
            this.btnSearchSym.Name = "btnSearchSym";
            this.btnSearchSym.Size = new System.Drawing.Size(25, 23);
            this.btnSearchSym.TabIndex = 16;
            this.btnSearchSym.UseVisualStyleBackColor = true;
            this.btnSearchSym.Click += new System.EventHandler(this.btnSearchSym_Click);
            // 
            // txtSym
            // 
            this.txtSym.Location = new System.Drawing.Point(558, 11);
            this.txtSym.Name = "txtSym";
            this.txtSym.Size = new System.Drawing.Size(48, 20);
            this.txtSym.TabIndex = 15;
            // 
            // btnSQLResultWindow
            // 
            this.btnSQLResultWindow.Image = ((System.Drawing.Image)(resources.GetObject("btnSQLResultWindow.Image")));
            this.btnSQLResultWindow.Location = new System.Drawing.Point(6, 8);
            this.btnSQLResultWindow.Name = "btnSQLResultWindow";
            this.btnSQLResultWindow.Size = new System.Drawing.Size(25, 24);
            this.btnSQLResultWindow.TabIndex = 13;
            this.btnSQLResultWindow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSQLResultWindow.UseVisualStyleBackColor = true;
            this.btnSQLResultWindow.Click += new System.EventHandler(this.btnSQLResultWindow_Click);
            // 
            // btnOpenSQLLogs
            // 
            this.btnOpenSQLLogs.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSQLLogs.Image")));
            this.btnOpenSQLLogs.Location = new System.Drawing.Point(39, 8);
            this.btnOpenSQLLogs.Name = "btnOpenSQLLogs";
            this.btnOpenSQLLogs.Size = new System.Drawing.Size(25, 24);
            this.btnOpenSQLLogs.TabIndex = 14;
            this.btnOpenSQLLogs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenSQLLogs.UseVisualStyleBackColor = true;
            this.btnOpenSQLLogs.Click += new System.EventHandler(this.btnOpenSQLLogs_Click);
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteQuery.ForeColor = System.Drawing.Color.Red;
            this.btnExecuteQuery.Location = new System.Drawing.Point(72, 8);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(25, 24);
            this.btnExecuteQuery.TabIndex = 2;
            this.btnExecuteQuery.Text = "!";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // chkSqlServer
            // 
            this.chkSqlServer.AutoSize = true;
            this.chkSqlServer.Checked = true;
            this.chkSqlServer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSqlServer.Location = new System.Drawing.Point(513, 13);
            this.chkSqlServer.Name = "chkSqlServer";
            this.chkSqlServer.Size = new System.Drawing.Size(40, 17);
            this.chkSqlServer.TabIndex = 10;
            this.chkSqlServer.Text = "SS";
            this.chkSqlServer.UseVisualStyleBackColor = true;
            // 
            // chkSemicolon
            // 
            this.chkSemicolon.AutoSize = true;
            this.chkSemicolon.Location = new System.Drawing.Point(247, 13);
            this.chkSemicolon.Name = "chkSemicolon";
            this.chkSemicolon.Size = new System.Drawing.Size(123, 17);
            this.chkSemicolon.TabIndex = 11;
            this.chkSemicolon.Text = "Treat \';\' as New Line";
            this.chkSemicolon.UseVisualStyleBackColor = true;
            // 
            // chkClearOldQueryResults
            // 
            this.chkClearOldQueryResults.AutoSize = true;
            this.chkClearOldQueryResults.Location = new System.Drawing.Point(103, 13);
            this.chkClearOldQueryResults.Name = "chkClearOldQueryResults";
            this.chkClearOldQueryResults.Size = new System.Drawing.Size(138, 17);
            this.chkClearOldQueryResults.TabIndex = 12;
            this.chkClearOldQueryResults.Text = "Clear Old Query Results";
            this.chkClearOldQueryResults.UseVisualStyleBackColor = true;
            // 
            // FormSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1428, 837);
            this.Controls.Add(this.tblMain);
            this.Name = "FormSQL";
            this.Text = "FormSQL";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSQL_KeyDown);
            this.tblMain.ResumeLayout(false);
            this.tabOperations.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.pnlWaitQuery.ResumeLayout(false);
            this.pnlWaitQuery.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlSQLFlow.ResumeLayout(false);
            this.grpTools.ResumeLayout(false);
            this.grpTools.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.TableLayoutPanel tblMain;
        private System.Windows.Forms.TabControl tabOperations;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel pnlWaitQuery;
        private System.Windows.Forms.TextBox txtQueryWaitMsg;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Panel pnlSQLFlow;
        private System.Windows.Forms.FlowLayoutPanel flLayoyt;
        private System.Windows.Forms.GroupBox grpTools;
        private System.Windows.Forms.CheckBox chkDb;
        private System.Windows.Forms.Button btnSSIS;
        private System.Windows.Forms.Button btnSearchSym;
        private System.Windows.Forms.TextBox txtSym;
        private System.Windows.Forms.Button btnSQLResultWindow;
        private System.Windows.Forms.Button btnOpenSQLLogs;
        private System.Windows.Forms.Button btnExecuteQuery;
        private System.Windows.Forms.CheckBox chkSqlServer;
        private System.Windows.Forms.CheckBox chkSemicolon;
        private System.Windows.Forms.CheckBox chkClearOldQueryResults;
    }
}