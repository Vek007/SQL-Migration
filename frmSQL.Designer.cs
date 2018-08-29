namespace SQL_Migration
{
    partial class frmSQL
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ForPro Tables");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSQL));
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tvDBFFolder = new System.Windows.Forms.TreeView();
            this.txtParentFolder = new System.Windows.Forms.TextBox();
            this.lblTreePopulateWait = new System.Windows.Forms.Label();
            this.tabOperations = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSQLResultWindow = new System.Windows.Forms.Button();
            this.chkClearOldQueryResults = new System.Windows.Forms.CheckBox();
            this.chkSemicolon = new System.Windows.Forms.CheckBox();
            this.chkSqlServer = new System.Windows.Forms.CheckBox();
            this.pnlWaitQuery = new System.Windows.Forms.Panel();
            this.txtQueryWaitMsg = new System.Windows.Forms.TextBox();
            this.pnlSQLFlow = new System.Windows.Forms.Panel();
            this.flLayoyt = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExecuteQuery = new System.Windows.Forms.Button();
            this.tabSourceAnyWhere = new System.Windows.Forms.TabPage();
            this.btnRecreateFileInfo = new System.Windows.Forms.Button();
            this.txtSAOutput = new System.Windows.Forms.TextBox();
            this.btnFileHistory = new System.Windows.Forms.Button();
            this.btnPopulateFileInfo = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtCodeFolder = new System.Windows.Forms.TextBox();
            this.tbMsgBox = new System.Windows.Forms.TabControl();
            this.tabMsgBoxOutput = new System.Windows.Forms.TabPage();
            this.txtSingleLineMsgBoxOutput = new System.Windows.Forms.TextBox();
            this.tbPageMultiLineTextBox = new System.Windows.Forms.TabPage();
            this.txtMultiLineMessageBox = new System.Windows.Forms.TextBox();
            this.TabConcateMessageBox = new System.Windows.Forms.TabPage();
            this.txtConcateMsgBox = new System.Windows.Forms.TextBox();
            this.tbSmText = new System.Windows.Forms.TabPage();
            this.txtSmText = new System.Windows.Forms.TextBox();
            this.btnPrgFiles = new System.Windows.Forms.Button();
            this.tbSync = new System.Windows.Forms.TabPage();
            this.txtSyncLog = new System.Windows.Forms.TextBox();
            this.tbLeft = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lvColumns = new System.Windows.Forms.ListView();
            this.FPFileds = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvTables = new System.Windows.Forms.ListView();
            this.FPTables = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbpFilter = new System.Windows.Forms.TabPage();
            this.lvTableFilter = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOpenSQLLogs = new System.Windows.Forms.Button();
            this.tabOperations.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.pnlWaitQuery.SuspendLayout();
            this.pnlSQLFlow.SuspendLayout();
            this.tabSourceAnyWhere.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tbMsgBox.SuspendLayout();
            this.tabMsgBoxOutput.SuspendLayout();
            this.tbPageMultiLineTextBox.SuspendLayout();
            this.TabConcateMessageBox.SuspendLayout();
            this.tbSmText.SuspendLayout();
            this.tbSync.SuspendLayout();
            this.tbLeft.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tbpFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtQuery
            // 
            this.txtQuery.BackColor = System.Drawing.Color.Black;
            this.txtQuery.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuery.ForeColor = System.Drawing.Color.Lime;
            this.txtQuery.Location = new System.Drawing.Point(6, 27);
            this.txtQuery.Multiline = true;
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuery.Size = new System.Drawing.Size(1372, 261);
            this.txtQuery.TabIndex = 0;
            this.txtQuery.TextChanged += new System.EventHandler(this.txtQuery_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(1800, 9);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(34, 26);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tvDBFFolder
            // 
            this.tvDBFFolder.BackColor = System.Drawing.Color.Black;
            this.tvDBFFolder.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvDBFFolder.ForeColor = System.Drawing.Color.Lime;
            this.tvDBFFolder.Location = new System.Drawing.Point(6, 7);
            this.tvDBFFolder.Name = "tvDBFFolder";
            treeNode1.Name = "ndRoot";
            treeNode1.Text = "ForPro Tables";
            this.tvDBFFolder.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.tvDBFFolder.Size = new System.Drawing.Size(407, 891);
            this.tvDBFFolder.TabIndex = 2;
            this.tvDBFFolder.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDBFFolder_NodeMouseClick);
            this.tvDBFFolder.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvDBFFolder_NodeMouseDoubleClick);
            // 
            // txtParentFolder
            // 
            this.txtParentFolder.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtParentFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParentFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParentFolder.Location = new System.Drawing.Point(13, 9);
            this.txtParentFolder.Name = "txtParentFolder";
            this.txtParentFolder.Size = new System.Drawing.Size(1781, 26);
            this.txtParentFolder.TabIndex = 3;
            // 
            // lblTreePopulateWait
            // 
            this.lblTreePopulateWait.AutoSize = true;
            this.lblTreePopulateWait.BackColor = System.Drawing.Color.Black;
            this.lblTreePopulateWait.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTreePopulateWait.ForeColor = System.Drawing.Color.Lime;
            this.lblTreePopulateWait.Location = new System.Drawing.Point(132, 367);
            this.lblTreePopulateWait.Name = "lblTreePopulateWait";
            this.lblTreePopulateWait.Size = new System.Drawing.Size(172, 15);
            this.lblTreePopulateWait.TabIndex = 4;
            this.lblTreePopulateWait.Text = "Processing DBF files... Please wait";
            this.lblTreePopulateWait.Visible = false;
            // 
            // tabOperations
            // 
            this.tabOperations.Controls.Add(this.tabPage1);
            this.tabOperations.Controls.Add(this.tabSourceAnyWhere);
            this.tabOperations.Controls.Add(this.tabPage2);
            this.tabOperations.Controls.Add(this.tbSync);
            this.tabOperations.Location = new System.Drawing.Point(446, 40);
            this.tabOperations.Name = "tabOperations";
            this.tabOperations.SelectedIndex = 0;
            this.tabOperations.Size = new System.Drawing.Size(1392, 931);
            this.tabOperations.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnOpenSQLLogs);
            this.tabPage1.Controls.Add(this.btnSQLResultWindow);
            this.tabPage1.Controls.Add(this.chkClearOldQueryResults);
            this.tabPage1.Controls.Add(this.chkSemicolon);
            this.tabPage1.Controls.Add(this.chkSqlServer);
            this.tabPage1.Controls.Add(this.pnlWaitQuery);
            this.tabPage1.Controls.Add(this.pnlSQLFlow);
            this.tabPage1.Controls.Add(this.btnExecuteQuery);
            this.tabPage1.Controls.Add(this.txtQuery);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1384, 905);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Query";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSQLResultWindow
            // 
            this.btnSQLResultWindow.Image = ((System.Drawing.Image)(resources.GetObject("btnSQLResultWindow.Image")));
            this.btnSQLResultWindow.Location = new System.Drawing.Point(6, 0);
            this.btnSQLResultWindow.Name = "btnSQLResultWindow";
            this.btnSQLResultWindow.Size = new System.Drawing.Size(30, 27);
            this.btnSQLResultWindow.TabIndex = 13;
            this.btnSQLResultWindow.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSQLResultWindow.UseVisualStyleBackColor = true;
            this.btnSQLResultWindow.Click += new System.EventHandler(this.btnSQLResultWindow_Click);
            // 
            // chkClearOldQueryResults
            // 
            this.chkClearOldQueryResults.AutoSize = true;
            this.chkClearOldQueryResults.Location = new System.Drawing.Point(935, 6);
            this.chkClearOldQueryResults.Name = "chkClearOldQueryResults";
            this.chkClearOldQueryResults.Size = new System.Drawing.Size(138, 17);
            this.chkClearOldQueryResults.TabIndex = 12;
            this.chkClearOldQueryResults.Text = "Clear Old Query Results";
            this.chkClearOldQueryResults.UseVisualStyleBackColor = true;
            // 
            // chkSemicolon
            // 
            this.chkSemicolon.AutoSize = true;
            this.chkSemicolon.Location = new System.Drawing.Point(1091, 6);
            this.chkSemicolon.Name = "chkSemicolon";
            this.chkSemicolon.Size = new System.Drawing.Size(123, 17);
            this.chkSemicolon.TabIndex = 11;
            this.chkSemicolon.Text = "Treat \';\' as New Line";
            this.chkSemicolon.UseVisualStyleBackColor = true;
            // 
            // chkSqlServer
            // 
            this.chkSqlServer.AutoSize = true;
            this.chkSqlServer.Location = new System.Drawing.Point(1235, 6);
            this.chkSqlServer.Name = "chkSqlServer";
            this.chkSqlServer.Size = new System.Drawing.Size(109, 17);
            this.chkSqlServer.TabIndex = 10;
            this.chkSqlServer.Text = "Stored Procedure";
            this.chkSqlServer.UseVisualStyleBackColor = true;
            // 
            // pnlWaitQuery
            // 
            this.pnlWaitQuery.Controls.Add(this.txtQueryWaitMsg);
            this.pnlWaitQuery.Location = new System.Drawing.Point(406, 98);
            this.pnlWaitQuery.Name = "pnlWaitQuery";
            this.pnlWaitQuery.Size = new System.Drawing.Size(591, 100);
            this.pnlWaitQuery.TabIndex = 9;
            this.pnlWaitQuery.Visible = false;
            // 
            // txtQueryWaitMsg
            // 
            this.txtQueryWaitMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQueryWaitMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQueryWaitMsg.Location = new System.Drawing.Point(186, 37);
            this.txtQueryWaitMsg.Name = "txtQueryWaitMsg";
            this.txtQueryWaitMsg.Size = new System.Drawing.Size(223, 24);
            this.txtQueryWaitMsg.TabIndex = 0;
            this.txtQueryWaitMsg.Text = "Processing...";
            this.txtQueryWaitMsg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlSQLFlow
            // 
            this.pnlSQLFlow.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSQLFlow.Controls.Add(this.flLayoyt);
            this.pnlSQLFlow.Location = new System.Drawing.Point(6, 297);
            this.pnlSQLFlow.Name = "pnlSQLFlow";
            this.pnlSQLFlow.Size = new System.Drawing.Size(1370, 602);
            this.pnlSQLFlow.TabIndex = 8;
            // 
            // flLayoyt
            // 
            this.flLayoyt.AutoScroll = true;
            this.flLayoyt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flLayoyt.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flLayoyt.Location = new System.Drawing.Point(3, 3);
            this.flLayoyt.MinimumSize = new System.Drawing.Size(1000, 300);
            this.flLayoyt.Name = "flLayoyt";
            this.flLayoyt.Size = new System.Drawing.Size(1361, 590);
            this.flLayoyt.TabIndex = 7;
            this.flLayoyt.WrapContents = false;
            // 
            // btnExecuteQuery
            // 
            this.btnExecuteQuery.Enabled = false;
            this.btnExecuteQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecuteQuery.ForeColor = System.Drawing.Color.Red;
            this.btnExecuteQuery.Location = new System.Drawing.Point(1353, 1);
            this.btnExecuteQuery.Name = "btnExecuteQuery";
            this.btnExecuteQuery.Size = new System.Drawing.Size(25, 25);
            this.btnExecuteQuery.TabIndex = 2;
            this.btnExecuteQuery.Text = "!";
            this.btnExecuteQuery.UseVisualStyleBackColor = true;
            this.btnExecuteQuery.Click += new System.EventHandler(this.btnExecuteQuery_Click);
            // 
            // tabSourceAnyWhere
            // 
            this.tabSourceAnyWhere.Controls.Add(this.btnRecreateFileInfo);
            this.tabSourceAnyWhere.Controls.Add(this.txtSAOutput);
            this.tabSourceAnyWhere.Controls.Add(this.btnFileHistory);
            this.tabSourceAnyWhere.Controls.Add(this.btnPopulateFileInfo);
            this.tabSourceAnyWhere.Location = new System.Drawing.Point(4, 22);
            this.tabSourceAnyWhere.Name = "tabSourceAnyWhere";
            this.tabSourceAnyWhere.Padding = new System.Windows.Forms.Padding(3);
            this.tabSourceAnyWhere.Size = new System.Drawing.Size(1384, 905);
            this.tabSourceAnyWhere.TabIndex = 1;
            this.tabSourceAnyWhere.Text = "SourceAnywhere";
            this.tabSourceAnyWhere.UseVisualStyleBackColor = true;
            // 
            // btnRecreateFileInfo
            // 
            this.btnRecreateFileInfo.Location = new System.Drawing.Point(1260, 8);
            this.btnRecreateFileInfo.Name = "btnRecreateFileInfo";
            this.btnRecreateFileInfo.Size = new System.Drawing.Size(118, 23);
            this.btnRecreateFileInfo.TabIndex = 3;
            this.btnRecreateFileInfo.Text = "Re-Create";
            this.btnRecreateFileInfo.UseVisualStyleBackColor = true;
            this.btnRecreateFileInfo.Click += new System.EventHandler(this.btnRecreateFileInfo_Click);
            // 
            // txtSAOutput
            // 
            this.txtSAOutput.BackColor = System.Drawing.Color.Black;
            this.txtSAOutput.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSAOutput.ForeColor = System.Drawing.Color.Lime;
            this.txtSAOutput.Location = new System.Drawing.Point(6, 37);
            this.txtSAOutput.Multiline = true;
            this.txtSAOutput.Name = "txtSAOutput";
            this.txtSAOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSAOutput.Size = new System.Drawing.Size(1372, 862);
            this.txtSAOutput.TabIndex = 2;
            // 
            // btnFileHistory
            // 
            this.btnFileHistory.Location = new System.Drawing.Point(139, 8);
            this.btnFileHistory.Name = "btnFileHistory";
            this.btnFileHistory.Size = new System.Drawing.Size(118, 23);
            this.btnFileHistory.TabIndex = 1;
            this.btnFileHistory.Text = "Populate File History";
            this.btnFileHistory.UseVisualStyleBackColor = true;
            this.btnFileHistory.Click += new System.EventHandler(this.btnFileHistory_Click);
            // 
            // btnPopulateFileInfo
            // 
            this.btnPopulateFileInfo.Location = new System.Drawing.Point(6, 8);
            this.btnPopulateFileInfo.Name = "btnPopulateFileInfo";
            this.btnPopulateFileInfo.Size = new System.Drawing.Size(118, 23);
            this.btnPopulateFileInfo.TabIndex = 0;
            this.btnPopulateFileInfo.Text = "Populate File Info";
            this.btnPopulateFileInfo.UseVisualStyleBackColor = true;
            this.btnPopulateFileInfo.Click += new System.EventHandler(this.btnPopulateFileInfo_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtCodeFolder);
            this.tabPage2.Controls.Add(this.tbMsgBox);
            this.tabPage2.Controls.Add(this.btnPrgFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1384, 905);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "French Translation";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtCodeFolder
            // 
            this.txtCodeFolder.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtCodeFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodeFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodeFolder.Location = new System.Drawing.Point(6, 8);
            this.txtCodeFolder.Name = "txtCodeFolder";
            this.txtCodeFolder.Size = new System.Drawing.Size(1332, 26);
            this.txtCodeFolder.TabIndex = 7;
            // 
            // tbMsgBox
            // 
            this.tbMsgBox.Controls.Add(this.tabMsgBoxOutput);
            this.tbMsgBox.Controls.Add(this.tbPageMultiLineTextBox);
            this.tbMsgBox.Controls.Add(this.TabConcateMessageBox);
            this.tbMsgBox.Controls.Add(this.tbSmText);
            this.tbMsgBox.Location = new System.Drawing.Point(7, 38);
            this.tbMsgBox.Name = "tbMsgBox";
            this.tbMsgBox.SelectedIndex = 0;
            this.tbMsgBox.Size = new System.Drawing.Size(1371, 856);
            this.tbMsgBox.TabIndex = 6;
            // 
            // tabMsgBoxOutput
            // 
            this.tabMsgBoxOutput.Controls.Add(this.txtSingleLineMsgBoxOutput);
            this.tabMsgBoxOutput.Location = new System.Drawing.Point(4, 22);
            this.tabMsgBoxOutput.Name = "tabMsgBoxOutput";
            this.tabMsgBoxOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabMsgBoxOutput.Size = new System.Drawing.Size(1363, 830);
            this.tabMsgBoxOutput.TabIndex = 0;
            this.tabMsgBoxOutput.Text = "SingleLineMessageBox";
            this.tabMsgBoxOutput.UseVisualStyleBackColor = true;
            // 
            // txtSingleLineMsgBoxOutput
            // 
            this.txtSingleLineMsgBoxOutput.BackColor = System.Drawing.Color.Black;
            this.txtSingleLineMsgBoxOutput.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSingleLineMsgBoxOutput.ForeColor = System.Drawing.Color.Lime;
            this.txtSingleLineMsgBoxOutput.Location = new System.Drawing.Point(7, 6);
            this.txtSingleLineMsgBoxOutput.Multiline = true;
            this.txtSingleLineMsgBoxOutput.Name = "txtSingleLineMsgBoxOutput";
            this.txtSingleLineMsgBoxOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSingleLineMsgBoxOutput.Size = new System.Drawing.Size(1351, 812);
            this.txtSingleLineMsgBoxOutput.TabIndex = 6;
            // 
            // tbPageMultiLineTextBox
            // 
            this.tbPageMultiLineTextBox.Controls.Add(this.txtMultiLineMessageBox);
            this.tbPageMultiLineTextBox.Location = new System.Drawing.Point(4, 22);
            this.tbPageMultiLineTextBox.Name = "tbPageMultiLineTextBox";
            this.tbPageMultiLineTextBox.Padding = new System.Windows.Forms.Padding(3);
            this.tbPageMultiLineTextBox.Size = new System.Drawing.Size(1363, 830);
            this.tbPageMultiLineTextBox.TabIndex = 1;
            this.tbPageMultiLineTextBox.Text = "MultiLineMsgBox";
            this.tbPageMultiLineTextBox.UseVisualStyleBackColor = true;
            // 
            // txtMultiLineMessageBox
            // 
            this.txtMultiLineMessageBox.BackColor = System.Drawing.Color.Black;
            this.txtMultiLineMessageBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMultiLineMessageBox.ForeColor = System.Drawing.Color.Lime;
            this.txtMultiLineMessageBox.Location = new System.Drawing.Point(6, 12);
            this.txtMultiLineMessageBox.Multiline = true;
            this.txtMultiLineMessageBox.Name = "txtMultiLineMessageBox";
            this.txtMultiLineMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMultiLineMessageBox.Size = new System.Drawing.Size(1351, 809);
            this.txtMultiLineMessageBox.TabIndex = 7;
            // 
            // TabConcateMessageBox
            // 
            this.TabConcateMessageBox.Controls.Add(this.txtConcateMsgBox);
            this.TabConcateMessageBox.Location = new System.Drawing.Point(4, 22);
            this.TabConcateMessageBox.Name = "TabConcateMessageBox";
            this.TabConcateMessageBox.Padding = new System.Windows.Forms.Padding(3);
            this.TabConcateMessageBox.Size = new System.Drawing.Size(1363, 830);
            this.TabConcateMessageBox.TabIndex = 2;
            this.TabConcateMessageBox.Text = "Concate Message Box";
            this.TabConcateMessageBox.UseVisualStyleBackColor = true;
            // 
            // txtConcateMsgBox
            // 
            this.txtConcateMsgBox.BackColor = System.Drawing.Color.Black;
            this.txtConcateMsgBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcateMsgBox.ForeColor = System.Drawing.Color.Lime;
            this.txtConcateMsgBox.Location = new System.Drawing.Point(6, 11);
            this.txtConcateMsgBox.Multiline = true;
            this.txtConcateMsgBox.Name = "txtConcateMsgBox";
            this.txtConcateMsgBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConcateMsgBox.Size = new System.Drawing.Size(1351, 809);
            this.txtConcateMsgBox.TabIndex = 8;
            // 
            // tbSmText
            // 
            this.tbSmText.Controls.Add(this.txtSmText);
            this.tbSmText.Location = new System.Drawing.Point(4, 22);
            this.tbSmText.Name = "tbSmText";
            this.tbSmText.Padding = new System.Windows.Forms.Padding(3);
            this.tbSmText.Size = new System.Drawing.Size(1363, 830);
            this.tbSmText.TabIndex = 3;
            this.tbSmText.Text = "SM_TEXT";
            this.tbSmText.UseVisualStyleBackColor = true;
            // 
            // txtSmText
            // 
            this.txtSmText.BackColor = System.Drawing.Color.Black;
            this.txtSmText.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSmText.ForeColor = System.Drawing.Color.Lime;
            this.txtSmText.Location = new System.Drawing.Point(6, 11);
            this.txtSmText.Multiline = true;
            this.txtSmText.Name = "txtSmText";
            this.txtSmText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSmText.Size = new System.Drawing.Size(1351, 809);
            this.txtSmText.TabIndex = 9;
            // 
            // btnPrgFiles
            // 
            this.btnPrgFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrgFiles.Location = new System.Drawing.Point(1344, 7);
            this.btnPrgFiles.Name = "btnPrgFiles";
            this.btnPrgFiles.Size = new System.Drawing.Size(34, 28);
            this.btnPrgFiles.TabIndex = 5;
            this.btnPrgFiles.Text = "...";
            this.btnPrgFiles.UseVisualStyleBackColor = true;
            this.btnPrgFiles.Click += new System.EventHandler(this.btnPrgFiles_Click);
            // 
            // tbSync
            // 
            this.tbSync.Controls.Add(this.txtSyncLog);
            this.tbSync.Location = new System.Drawing.Point(4, 22);
            this.tbSync.Name = "tbSync";
            this.tbSync.Padding = new System.Windows.Forms.Padding(3);
            this.tbSync.Size = new System.Drawing.Size(1384, 905);
            this.tbSync.TabIndex = 3;
            this.tbSync.Text = "Sync";
            this.tbSync.UseVisualStyleBackColor = true;
            // 
            // txtSyncLog
            // 
            this.txtSyncLog.BackColor = System.Drawing.Color.Black;
            this.txtSyncLog.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSyncLog.ForeColor = System.Drawing.Color.Lime;
            this.txtSyncLog.Location = new System.Drawing.Point(6, 8);
            this.txtSyncLog.Multiline = true;
            this.txtSyncLog.Name = "txtSyncLog";
            this.txtSyncLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSyncLog.Size = new System.Drawing.Size(1372, 891);
            this.txtSyncLog.TabIndex = 3;
            // 
            // tbLeft
            // 
            this.tbLeft.Controls.Add(this.tabPage3);
            this.tbLeft.Controls.Add(this.tabPage4);
            this.tbLeft.Controls.Add(this.tbpFilter);
            this.tbLeft.Location = new System.Drawing.Point(13, 41);
            this.tbLeft.Name = "tbLeft";
            this.tbLeft.SelectedIndex = 0;
            this.tbLeft.Size = new System.Drawing.Size(427, 930);
            this.tbLeft.TabIndex = 6;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lblTreePopulateWait);
            this.tabPage3.Controls.Add(this.tvDBFFolder);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(419, 904);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Tree View";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnSearch);
            this.tabPage4.Controls.Add(this.txtSearch);
            this.tabPage4.Controls.Add(this.lvColumns);
            this.tabPage4.Controls.Add(this.lvTables);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(419, 904);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "List View";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(383, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(29, 25);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(6, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(372, 23);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lvColumns
            // 
            this.lvColumns.BackColor = System.Drawing.Color.Black;
            this.lvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FPFileds});
            this.lvColumns.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvColumns.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lvColumns.Location = new System.Drawing.Point(5, 543);
            this.lvColumns.Name = "lvColumns";
            this.lvColumns.Size = new System.Drawing.Size(407, 351);
            this.lvColumns.TabIndex = 1;
            this.lvColumns.UseCompatibleStateImageBehavior = false;
            this.lvColumns.View = System.Windows.Forms.View.Details;
            // 
            // FPFileds
            // 
            this.FPFileds.Text = "Fields";
            // 
            // lvTables
            // 
            this.lvTables.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvTables.BackColor = System.Drawing.Color.Black;
            this.lvTables.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.FPTables});
            this.lvTables.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTables.ForeColor = System.Drawing.Color.Lime;
            this.lvTables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTables.Location = new System.Drawing.Point(6, 36);
            this.lvTables.MultiSelect = false;
            this.lvTables.Name = "lvTables";
            this.lvTables.ShowGroups = false;
            this.lvTables.Size = new System.Drawing.Size(407, 501);
            this.lvTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvTables.TabIndex = 0;
            this.lvTables.UseCompatibleStateImageBehavior = false;
            this.lvTables.View = System.Windows.Forms.View.Details;
            this.lvTables.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.lvTables_ColumnWidthChanging);
            this.lvTables.ItemActivate += new System.EventHandler(this.lvTables_ItemActivate);
            this.lvTables.SelectedIndexChanged += new System.EventHandler(this.lvTables_SelectedIndexChanged);
            // 
            // FPTables
            // 
            this.FPTables.Text = "Foxpro Tables";
            // 
            // tbpFilter
            // 
            this.tbpFilter.Controls.Add(this.lvTableFilter);
            this.tbpFilter.Location = new System.Drawing.Point(4, 22);
            this.tbpFilter.Name = "tbpFilter";
            this.tbpFilter.Padding = new System.Windows.Forms.Padding(3);
            this.tbpFilter.Size = new System.Drawing.Size(419, 904);
            this.tbpFilter.TabIndex = 2;
            this.tbpFilter.Text = "filter";
            this.tbpFilter.UseVisualStyleBackColor = true;
            // 
            // lvTableFilter
            // 
            this.lvTableFilter.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvTableFilter.BackColor = System.Drawing.Color.Black;
            this.lvTableFilter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvTableFilter.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvTableFilter.ForeColor = System.Drawing.Color.Lime;
            this.lvTableFilter.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTableFilter.Location = new System.Drawing.Point(6, 6);
            this.lvTableFilter.MultiSelect = false;
            this.lvTableFilter.Name = "lvTableFilter";
            this.lvTableFilter.ShowGroups = false;
            this.lvTableFilter.Size = new System.Drawing.Size(407, 892);
            this.lvTableFilter.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvTableFilter.TabIndex = 1;
            this.lvTableFilter.UseCompatibleStateImageBehavior = false;
            this.lvTableFilter.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Foxpro Tables";
            // 
            // btnOpenSQLLogs
            // 
            this.btnOpenSQLLogs.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSQLLogs.Image")));
            this.btnOpenSQLLogs.Location = new System.Drawing.Point(42, 0);
            this.btnOpenSQLLogs.Name = "btnOpenSQLLogs";
            this.btnOpenSQLLogs.Size = new System.Drawing.Size(30, 27);
            this.btnOpenSQLLogs.TabIndex = 14;
            this.btnOpenSQLLogs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOpenSQLLogs.UseVisualStyleBackColor = true;
            this.btnOpenSQLLogs.Click += new System.EventHandler(this.btnOpenSQLLogs_Click);
            // 
            // frmSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(300, 300);
            this.ClientSize = new System.Drawing.Size(1850, 983);
            this.Controls.Add(this.tbLeft);
            this.Controls.Add(this.tabOperations);
            this.Controls.Add(this.txtParentFolder);
            this.Controls.Add(this.btnBrowse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Helper";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSQL_KeyDown);
            this.tabOperations.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.pnlWaitQuery.ResumeLayout(false);
            this.pnlWaitQuery.PerformLayout();
            this.pnlSQLFlow.ResumeLayout(false);
            this.tabSourceAnyWhere.ResumeLayout(false);
            this.tabSourceAnyWhere.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tbMsgBox.ResumeLayout(false);
            this.tabMsgBoxOutput.ResumeLayout(false);
            this.tabMsgBoxOutput.PerformLayout();
            this.tbPageMultiLineTextBox.ResumeLayout(false);
            this.tbPageMultiLineTextBox.PerformLayout();
            this.TabConcateMessageBox.ResumeLayout(false);
            this.TabConcateMessageBox.PerformLayout();
            this.tbSmText.ResumeLayout(false);
            this.tbSmText.PerformLayout();
            this.tbSync.ResumeLayout(false);
            this.tbSync.PerformLayout();
            this.tbLeft.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tbpFilter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TreeView tvDBFFolder;
        private System.Windows.Forms.TextBox txtParentFolder;
        private System.Windows.Forms.Label lblTreePopulateWait;
        private System.Windows.Forms.TabControl tabOperations;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabSourceAnyWhere;
        private System.Windows.Forms.TabControl tbLeft;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView lvTables;
        private System.Windows.Forms.ListView lvColumns;
        private System.Windows.Forms.ColumnHeader FPFileds;
        private System.Windows.Forms.ColumnHeader FPTables;
        private System.Windows.Forms.Button btnExecuteQuery;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.FlowLayoutPanel flLayoyt;
        private System.Windows.Forms.Panel pnlSQLFlow;
        private System.Windows.Forms.Panel pnlWaitQuery;
        private System.Windows.Forms.TextBox txtQueryWaitMsg;
        private System.Windows.Forms.Button btnPopulateFileInfo;
        private System.Windows.Forms.Button btnFileHistory;
        private System.Windows.Forms.TextBox txtSAOutput;
        private System.Windows.Forms.Button btnRecreateFileInfo;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnPrgFiles;
        private System.Windows.Forms.TextBox txtSingleLineMsgBoxOutput;
        private System.Windows.Forms.TabControl tbMsgBox;
        private System.Windows.Forms.TabPage tabMsgBoxOutput;
        private System.Windows.Forms.TabPage tbPageMultiLineTextBox;
        private System.Windows.Forms.TextBox txtMultiLineMessageBox;
        private System.Windows.Forms.TabPage TabConcateMessageBox;
        private System.Windows.Forms.TextBox txtConcateMsgBox;
        private System.Windows.Forms.TabPage tbSmText;
        private System.Windows.Forms.TextBox txtSmText;
        private System.Windows.Forms.TextBox txtCodeFolder;
        private System.Windows.Forms.CheckBox chkSqlServer;
        private System.Windows.Forms.TabPage tbSync;
        private System.Windows.Forms.TextBox txtSyncLog;
        private System.Windows.Forms.CheckBox chkSemicolon;
        private System.Windows.Forms.CheckBox chkClearOldQueryResults;
        private System.Windows.Forms.TabPage tbpFilter;
        private System.Windows.Forms.ListView lvTableFilter;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnSQLResultWindow;
        private System.Windows.Forms.Button btnOpenSQLLogs;
    }
}

