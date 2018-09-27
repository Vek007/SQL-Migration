using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoxProFileStructure;
using FoxProFileStructure.TableFileStructure;
using System.Threading;
using System.Data.OleDb;
using System.Collections;
using System.Text.RegularExpressions;
using System.Security;
using System.Text;
using System.Diagnostics;
using System.Data.Entity;

namespace SQL_Migration
{

    public partial class frmSQL : Form
    {
        private string tableName = "";
        ContextMenu popupMenu = new ContextMenu();

        //private List<Variable> variables = new List<Variable>();
        private static string constConnString = @"Provider=VFPOLEDB.1;Data Source=";// E:\client_data\nossack\dist_ii.dbc;providerName=System.Data.OleDb";
        private string constDataSource = @"\dist_ii.dbc;providerName=System.Data.OleDb";

        private string connString = @"Provider=VFPOLEDB.1;Data Source=";// E:\client_data\nossack\dist_ii.dbc;providerName=System.Data.OleDb";
        private string dataSource = @"\dist_ii.dbc;providerName=System.Data.OleDb";

        private string SQLServerConnString = @"Server=VivekPC\SQL;Database=DtecSQLDatabase;User Id = Dtec; Password=1Intersect$";

        private string connStringFr = @"Provider=VFPOLEDB.1;Data Source=";// E:\client_data\nossack\dist_ii.dbc;providerName=System.Data.OleDb";
        private string dataSourceFr = @"\dist_ii.dbc;providerName=System.Data.OleDb";

        public static bool bAddDbStringTranslations = false;
        public frmSQL()
        {
            this.KeyPreview = true;
            InitializeComponent();

            this.lvTables.View = View.Details;
            // Add columns and set their text.
            this.lvTables.Columns[0].Width = 400;

            this.lvColumns.View = View.Details;
            // Add columns and set their text.
            this.lvColumns.Columns[0].Width = 400;

            dgvST.DataSource = Data.ST_DB.pers.ToList();
            dgvST.Refresh();

        }

        List<SqlResults> sqlResults = new List<SqlResults>();

        private void GetData(string selectCommand)
        {
            var def = DateTime.MinValue;
            SQLExecutor sqlData = new SQLExecutor();

            sqlResults.Clear();
            int spResult = -10;

            if (!chkSqlServer.Checked)
            {
                if (chkSemicolon.Checked)
                {
                    sqlResults = sqlData.DataExecutor(selectCommand, connString, true);
                }
                else
                {
                    sqlResults = sqlData.DataExecutor(selectCommand, connString);
                }
            }
            else
            {
                spResult = sqlData.SPExecutorSQLClient(selectCommand, SQLServerConnString);
                SqlResults sr = new SqlResults();
                sr.AddTextToMessages(spResult.ToString(), false, false);
                sr.AddInputQuery(selectCommand,false,false);
                sqlResults.Add(sr);
            }

            flLayoyt.Invoke((MethodInvoker)delegate {
                flLayoyt.Controls.Clear();
                foreach (SqlResults sr in sqlResults)
                {
                    flLayoyt.Controls.Add(sr);
                }

                foreach (Control ct in flLayoyt.Controls)
                {
                    ct.Dock = DockStyle.Right;
                }

                flLayoyt.Refresh();
                btnExecuteQuery.Enabled = true;
                pnlWaitQuery.Visible = false;
            });


        }

        private string GetFieldTypeValue(FieldType type)
        {
            switch (type)
            {
                case FieldType.Character:
                    return "string";
                case FieldType.Currency:
                case FieldType.Numeric:
                    return "decimal";
                case FieldType.Date:
                    return "DateTime";
                case FieldType.DateTime:
                    return "DateTime";
                case FieldType.Integer:
                    return "int";
                case FieldType.Logical:
                    return "bool";
                default:
                    return "string";
            }
        }

        private string getTableName(string fullTableName)
        {
            string tbName="";

            tbName = fullTableName.Substring(fullTableName.LastIndexOf("\\"));

            return tbName;
        }

        bool bExecuteQuery = true;
        private void PopulateTableNodeWithColumns(TreeNode tn)
        {
            try
            {
                List<Variable> variables = new List<Variable>();
                string file = txtParentFolder.Text + "\\" + tn.Text + ".dbf";
                TableFile tableFile = new TableFile(file);
                if (tableFile == null)
                    return;

                tableFile.Deserialize(false);
                int count = tableFile.Header.SubRecords.Count;
                int num = 0;
                for (int index = count - 1; index >= 0 && tableFile.Header.SubRecords[index].Name.StartsWith("_NullFlags"); --index)
                    ++num;
                if (num > 0)
                    count -= num;
                for (int index = 0; index < count; ++index)
                {
                    FieldSubrecord subRecord = tableFile.Header.SubRecords[index];
                    Variable variable = new Variable();
                    variable.Include = true;
                    variable.PrimaryKey = index == count - 1;
                    variable.VfpFieldName = subRecord.FullName;
                    variable.VfpFieldType = subRecord.Type;
                    variable.DotNetType = this.GetFieldTypeValue(subRecord.Type);

                    variable.ReadOnly = variable.PrimaryKey;

                    variable.Read = true;
                    variable.Insert = !variable.ReadOnly;
                    variable.Update = !variable.ReadOnly;
                    variable.ShallowCopy = !variable.ReadOnly;
                    variable.DeepCopy = true;
                    variable.FoxProLength = subRecord.LengthOfField;
                    variable.DecimalPlaces = subRecord.NumberOfDecimalPlaces;
                    variables.Add(variable);
                }

                //TreeNode[] chNodes = new TreeNode[variables.Count];
                List<TreeNode> chNodes = new List<TreeNode>();
                foreach (Variable fd in variables)
                {
                    TreeNode tn1 = new TreeNode(fd.VfpFieldName);
                    tn1.Name = tn1.Text;
                    tn1.Nodes.Add(fd.VfpFieldType.ToString());
                    tn1.Nodes.Add(fd.DotNetType);
                    chNodes.Add(tn1);
                    //                tn.Nodes.Add(tn1);

                }

                tn.TreeView.Invoke((MethodInvoker)delegate
                {
                    tn.Nodes.AddRange(chNodes.ToArray());
                    bExecuteQuery = true;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error");
            }
        }

        public void UpdateSAOutput(string info)
        {
            txtSAOutput.Invoke((MethodInvoker)delegate
            {
                txtSAOutput.AppendText(info + Environment.NewLine); 

            });
        }

        private void AddColumnNodes(frmSQL thisForm)
        {
            string[] files =
                Directory.GetFiles(thisForm.txtParentFolder.Text, "*.dbf", SearchOption.AllDirectories);

            foreach (string dbfile in files)
            {
                TreeNode[] tnn = null;
                foreach (TreeNode tnnn in this.tvDBFFolder.Nodes[0].Nodes)
                {
                    tnn = tnnn.Nodes.Find(Path.GetFileNameWithoutExtension(dbfile), true);
                    if (tnn.Length > 0)
                        break;
                }

                List<Variable> variables = new List<Variable>();
                variables.Clear();
                if (tnn!=null && tnn.Length > 0)
                {
                    TreeNode tn = tnn[0];
                    string file = txtParentFolder.Text + "\\" + Path.GetFileNameWithoutExtension(dbfile) + ".dbf";
                    TableFile tableFile = new TableFile(file);
                    tableFile.Deserialize(false);
                    int count = tableFile.Header.SubRecords.Count;
                    int num = 0;
                    for (int index = count - 1; index >= 0 && tableFile.Header.SubRecords[index].Name.StartsWith("_NullFlags"); --index)
                        ++num;
                    if (num > 0)
                        count -= num;
                    for (int index = 0; index < count; ++index)
                    {
                        FieldSubrecord subRecord = tableFile.Header.SubRecords[index];
                        Variable variable = new Variable();
                        variable.Include = true;
                        variable.PrimaryKey = index == count - 1;
                        variable.VfpFieldName = subRecord.FullName;
                        variable.VfpFieldType = subRecord.Type;
                        variable.DotNetType = this.GetFieldTypeValue(subRecord.Type);

                        //variable.ReadOnly = variable.PrimaryKey;

                        //variable.Read = true;
                        //variable.Insert = !variable.ReadOnly;
                        //variable.Update = !variable.ReadOnly;
                        //variable.ShallowCopy = !variable.ReadOnly;
                        //variable.DeepCopy = true;
                        //variable.FoxProLength = subRecord.LengthOfField;
                        //variable.DecimalPlaces = subRecord.NumberOfDecimalPlaces;
                        variables.Add(variable);
                    }

                    foreach (Variable fd in variables)
                    {
                        TreeNode tn1 = new TreeNode(fd.VfpFieldName);
                        tn1.Name = tn1.Text;
                        tn1.Nodes.Add(fd.VfpFieldType.ToString());
                        tn1.Nodes.Add(fd.DotNetType);
                        //tn.Nodes.Add(tn1);
                        this.tvDBFFolder.Invoke((MethodInvoker)delegate {
                            tn.Nodes.Add(tn1);
                        });

                    }
                }
            }
        }

        private void PopulateNodes(frmSQL thisForm)
        {
            string[] files =
                Directory.GetFiles(thisForm.txtParentFolder.Text, "*.dbf", SearchOption.AllDirectories);

            TreeNode tnParent = new TreeNode();
            tnParent.Text = "DB Tables";
            for (char c = 'A'; c <= 'Z'; c++)
            {
                TreeNode tn = new TreeNode(c.ToString());
                tn.Name = tn.Text;
                tnParent.Nodes.Add(tn);
            }

            ListViewItem lvi = new ListViewItem("Table");
            foreach (string file in files)
            {
                string na = Path.GetFileNameWithoutExtension(file).Substring(0, 1).ToUpper();
                TreeNode[] tn = tnParent.Nodes.Find(na, true);
                
                if (tn.Length > 0)
                {
                    this.lblTreePopulateWait.Invoke((MethodInvoker)delegate {
                        this.lblTreePopulateWait.Text = "Processing (" + Path.GetFileNameWithoutExtension(file) + ").";
                    });

                    TreeNode tn1 = new TreeNode(Path.GetFileNameWithoutExtension(file));
                    tn1.Name = tn1.Text;
                    tn[0].Nodes.Add(tn1);

                    ListViewItem lvi1 = new ListViewItem(tn1.Name);

                    lvTables.Invoke((MethodInvoker)delegate {
                        lvTables.Items.Add(lvi1);
                    });
                    
                }
            }

            //if(lvi.SubItems.Count >0)
            //    lvTables.Items.Add(lvi);

            this.tvDBFFolder.Invoke((MethodInvoker)delegate {
                this.tvDBFFolder.Nodes.Clear();
                this.tvDBFFolder.Nodes.Add(tnParent);
                this.tvDBFFolder.Nodes[0].Expand();
                this.tvDBFFolder.Nodes[0].Nodes[0].Expand();
                this.tvDBFFolder.Refresh();

                Thread thread = new Thread(() => AddColumnNodes(this));
                //thread.Start();

            });


            this.lblTreePopulateWait.Invoke((MethodInvoker)delegate {
                this.lblTreePopulateWait.Visible = false;
            });

            this.btnBrowse.Invoke((MethodInvoker)delegate {
                this.btnBrowse.Enabled = true;
            });

        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fldDialog = new FolderBrowserDialog();
            fldDialog.SelectedPath = @"E:\client_data\Nossack";
            if (fldDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tvDBFFolder.Nodes.Clear();
                lblTreePopulateWait.Visible = true;
                btnBrowse.Enabled = false;
                txtParentFolder.Text = fldDialog.SelectedPath.ToString();
                dataSource = txtParentFolder.Text + constDataSource;
                connString = constConnString + dataSource;

                Thread thread = new Thread(() => PopulateNodes(this));
                thread.Start();
            }
        }

        private void tvDBFFolder_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Nodes.Count <= 0)
            {
                Thread thread = new Thread(() => PopulateTableNodeWithColumns(e.Node));
                thread.Start();

              //  PopulateTableNodeWithColumns(e.Node);
            }
                
            e.Node.Expand();

            if (chkClearOldQueryResults.Checked)
            {
                txtQuery.Text = "Select * from " + e.Node.Text + ";";
            }
            else
            {
                txtQuery.Text += (Environment.NewLine + "Select * from " + e.Node.Text + ";");
            }
        }

        private void tvDBFFolder_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                MenuItem myMenuItem = new MenuItem("Sync - " + e.Node.Name);
                myMenuItem.Tag = e.Node;
                myMenuItem.Click += new EventHandler(myMenuItem_Click);
                popupMenu.MenuItems.Add(myMenuItem);
                popupMenu.Show(this.tvDBFFolder, e.Location);
            }
        }

        void myMenuItem_Click(object sender, EventArgs e)
        {
            if (sender != null && sender is MenuItem)
            {
                MenuItem mi = (MenuItem)sender;
                if (mi.Tag != null && mi.Tag is TreeNode)
                {
                    TreeNode tn = (TreeNode)mi.Tag;
                    MessageBox.Show(tn.Text);
                }
            }
        }

        private void lvTables_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                //Keep the width not changed.
                e.NewWidth = this.lvTables.Columns[e.ColumnIndex].Width;
                //Cancel the event.
                e.Cancel = true;
            }
        }

        private void lvTables_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private IEnumerable<string> GetColumnNames(TreeNode tvn)
        {
            List<string> clNames = new List<string>();
            if (tvn.Nodes.Count > 0) //we already have the columns.
            {
                foreach (TreeNode tn in tvn.Nodes)
                    clNames.Add(tn.Text);
            }
            else
            {
                PopulateTableNodeWithColumns(tvn);
                //TreeNode[] nodes = tvDBFFolder.Nodes.Find(tvn.Text, true);
                foreach (TreeNode tn in tvn.Nodes)
                    clNames.Add(tn.Text);
            }

            return clNames;
        }

        private void lvTables_ItemActivate(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selItems = lvTables.SelectedItems;
            if (selItems.Count > 0)
            {
                if (chkClearOldQueryResults.Checked)
                {
                    txtQuery.Text = "Select * from " + selItems[0].Text + ";";
                }
                else
                {
                    txtQuery.Text += (Environment.NewLine + "Select * from " + selItems[0].Text + ";");
                }


                if (this.chkClearOldQueryResults.Checked)
                {
                    txtQuery.Text = "Select * from " + selItems[0].Text + ";";
                }
                else
                {
                    txtQuery.Text += Environment.NewLine +  "Select * from " + selItems[0].Text + ";" + Environment.NewLine;
                }

                TreeNode[] nodes = tvDBFFolder.Nodes.Find(selItems[0].Text, true);
                if (nodes.Length > 0)
                {
                    lvColumns.Items.Clear();
                    List<String> clNames = GetColumnNames(nodes[0]).ToList();
                    ListViewItem lvi1 = new ListViewItem();
                    foreach (string str in clNames.OrderBy(p => p))
                    {
                        lvColumns.Items.Add(str);
                    }
                }

                if (this.chkClearOldQueryResults.Checked)
                {
                    btnExecuteQuery_Click(null, null);
                }
            }
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            string sqlCmd = txtQuery.SelectedText;
            btnExecuteQuery.Enabled = false;
            pnlWaitQuery.Visible = true;
            Thread thread = new Thread(() => GetData(sqlCmd));
            thread.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSearch.Text))
                return;

            foreach (ListViewItem lvi in lvTables.Items)
            {
                if (lvi.Text.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    lvi.BackColor = SystemColors.Highlight;
                    lvi.ForeColor = SystemColors.HighlightText;
                    lvi.Selected = true;
                    lvTables.EnsureVisible(lvi.Index);
                    break;
                }
            }

            lvTableFilter.Items.Clear();
            foreach (ListViewItem lvi in lvTables.Items)
            {
                if (lvi.Text.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    ListViewItem lvi1 = (ListViewItem)lvi.Clone();
                    lvTableFilter.Items.Add(lvi1);
                }
            }
        }

        private void txtQuery_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtQuery.Text))
                btnExecuteQuery.Enabled = true;
            else
                btnExecuteQuery.Enabled = false;

            if (bFormatText)
            {
                FormatQueryTextBox();
            }
        }

        private void FormatQueryTextBox()
        {
            String newText = String.Empty;
            List<String> lines = txtQuery.Text.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            txtQuery.Text = string.Empty;
            foreach (string line in lines)
            {
                
                if (!string.IsNullOrEmpty(line) )
                {
                    if (!line.Trim().Contains(";"))
                    {
                        txtQuery.Text += line.Trim() + ";" + Environment.NewLine;
                    }
                    else
                    {
                        txtQuery.Text += line.Trim() + Environment.NewLine;
                    }
                }
            }
            bFormatText = false;
        }

        private void txtQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void frmSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnExecuteQuery_Click(null, null);
            }
        }

        private void btnPopulateFileInfo_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => {
                SourceAnyWhereHelper saHelper = new SourceAnyWhereHelper(this);
                saHelper.PopulateFilesFP("C:\\Source\\");
            });
            thread.Start();
        }

        private void btnFileHistory_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => {
                SourceAnyWhereHelper saHelper = new SourceAnyWhereHelper(this);
                saHelper.PopulateFileHistoryInDb();
            });
            thread.Start();
        }

        private void btnRecreateFileInfo_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => {
                SourceAnyWhereHelper saHelper = new SourceAnyWhereHelper(this);
                saHelper.ReCreateFileInfo("C:\\Source\\");
            });
            thread.Start();
        }

        public string searchDir = "";
        public string searchFiles = "*.vcx";
        private void btnPrgFiles_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select a file";
            //            fdlg.InitialDirectory = Directory.GetCurrentDirectory();
            fdlg.InitialDirectory = "C:\\Source\\dist_ii";
            fdlg.Filter = "VFP Files (*.scx)|*.scx"; 
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                String strFile = fdlg.FileName;
                txtCodeFolder.Text = Path.GetDirectoryName(strFile);
                dataSourceFr = txtCodeFolder.Text + constDataSource;
                connStringFr = constConnString + dataSource;

                //File Extension
                String strExt;
                //Get the Directory and file extension
                this.searchDir = Path.GetDirectoryName(strFile);
                strExt = Path.GetExtension(strFile);
                searchFiles = "*.prg"; 
            //    Thread thread = new Thread(() => SearchPrgFilesBin(this));
            //    thread.Start();

                MessageBoxAnalyzer mba = new MessageBoxAnalyzer(this, txtCodeFolder.Text, connStringFr);
                Thread threadDb = new Thread(() => mba.ProcessFoxproFiles());
                threadDb.Start();

            }
        }

        protected void GetFiles(String strDir, String strExt, bool bRecursive)
        {
            //search pattern can include the wild characters '*' and '?'
            string[] fileList = Directory.GetFiles(strDir, strExt);
            for (int i = 0; i < fileList.Length; i++)
            {
                if (File.Exists(fileList[i]))
                    m_arrFiles.Add(fileList[i]);
            }
            if (bRecursive == true)
            {
                //Get recursively from subdirectories
                string[] dirList = Directory.GetDirectories(strDir);
                for (int i = 0; i < dirList.Length; i++)
                {
                    GetFiles(dirList[i], strExt, true);
                }
            }
        }

        /// <summary>
        /// A case insenstive replace function.
        /// </summary>
        /// <param name="originalString">The string to examine.(HayStack)</param>
        /// <param name="oldValue">The value to replace.(Needle)</param>
        /// <param name="newValue">The new value to be inserted</param>
        /// <returns>A string</returns>
        public string CaseInsenstiveReplace(string originalString, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue,
               RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(originalString, newValue);
        }

        List<int> modifiedLines = new List<int>();
        private int ReplaceText(List<string> file, int stLineNo, int enLineNo, string modifiedLine )
        {
            int st = stLineNo;
            int en = enLineNo;
            stLineNo--;
            enLineNo--;
            string firstLine = file[stLineNo];

            for (int i = enLineNo; i >= stLineNo; i--)
            {
                if (i == file.Count())
                {
                    int j = 0;
                }
                file.RemoveAt(i);
            }

            int totTabs = 0;
            int totSpace = 0;
            for (int i = 0; i < firstLine.Length; i++)
            {
                if (firstLine[i] == '\t')
                {
                    totTabs++;
                    continue;
                }
                else if (firstLine[i] == ' ')
                {
                    totSpace++;
                    continue;
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < totTabs; i++)
                modifiedLine = '\t' + modifiedLine;

            for (int i = 0; i < totSpace; i++)
                modifiedLine = ' ' + modifiedLine;

            file.Insert(stLineNo, modifiedLine);
            modifiedLines.Add(stLineNo);

            return file.Count();
        }

        List<Tuple<int, string>> modifiedBinLines = new List<Tuple<int, string>>();
        private int ReplaceTextExt(List<string> file, int stLineNo, int enLineNo, string modifiedLine)
        {
            bool bAdded = false;
            int st = stLineNo;
            int en = enLineNo;
        //    stLineNo--;
        //    enLineNo--;
//            string firstLine = file[stLineNo];
            string firstLine = System.Text.Encoding.UTF8.GetString(binLines[stLineNo].ToArray()); ;

            for (int i = enLineNo; i >= stLineNo; i--)
            {
                if (i == file.Count())
                {
                    int j = 0;
                }
                file[i] = "";//.RemoveAt(i);
                modifiedLines.Add(i);
                bAdded = true;
            }

            int totTabs = 0;
            int totSpace = 0;
            for (int i = 0; i < firstLine.Length; i++)
            {
                if (firstLine[i] == '\t')
                {
                    totTabs++;
                    continue;
                }
                else if (firstLine[i] == ' ')
                {
                    totSpace++;
                    continue;
                }
                else
                {
                    break;
                }
            }

            for (int i = 0; i < totTabs; i++)
                modifiedLine = '\t' + modifiedLine;

            for (int i = 0; i < totSpace; i++)
                modifiedLine = ' ' + modifiedLine;

            file[stLineNo]= "";
            if(!bAdded)
                modifiedLines.Add(stLineNo);

            Tuple<int, string> mLine = Tuple.Create<int, string>(stLineNo, modifiedLine);

            modifiedBinLines.Add(mLine);
            for (int i = stLineNo+1; i <= enLineNo; i++)
            {
                mLine = Tuple.Create<int, string>(i, "");
                modifiedBinLines.Add(mLine);
            }

            return file.Count();
        }

        public bool CompareFirstChar(List<string> file)
        {
            return true;
            int totLines = Math.Min(file.Count(), binLines.Count());

            for (int l = 0; l < totLines; l++)
            {
                if (file[l].Length == 0 && binLines[l].Count == 0)
                    continue;

                if (file[l].Length >= 0 && binLines[l].Count == 0)
                {
                    int j = 0;
                    j++;
                }
                if (file[l].Length == 0 && binLines[l].Count >= 0)
                {
                    int j = 0;
                    j++;
                }


                int a = (int)file[l][0];
                int b = (int)binLines[l][0];

                if (a != b)
                {
                    int abc = 0;
                    abc++;
                }
            }

            return false;
        }

        public void SaveFileBin(List<string> file, string fileName)
        {
            /*CompareFirstChar(file);
            foreach (int mLine in modifiedLines)
            {
                if (file[mLine].Count() == 0)
                {
                    continue;
                }
                byte[] binLine = System.Text.Encoding.UTF8.GetBytes(file[mLine]);

                binLines[mLine] = binLine.ToList();
            }*/

            string path = Path.GetDirectoryName(fileName) + "\\modified_bin\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName1 = Path.GetFileName(fileName);

            if (File.Exists(path + fileName1))
            {
                File.Delete(path + fileName1);
            }

            FileStream fs = new FileStream(path + fileName1, FileMode.CreateNew);//File.Create("C:\\binary.dat", 2048, FileOptions.None);
            try
            {
                using (BinaryWriter tw = new BinaryWriter(fs))
                {
                    foreach (List<byte> binLine in binLines)
                    {
                        byte[] bLine = binLine.ToArray();
                        for (int i = 0; i < bLine.Length; i++)
                        {
                            //if ((int)bLine[i] == 10 || (int)bLine[i] == 13)
                            //{
                            //    break;
                            //}
                            tw.Write(bLine[i]);
                        }
                        tw.Write((byte)13);
                        tw.Write((byte)10);
                    }
                }
            }
            catch (DirectoryNotFoundException dn)
            {

            }
        }

        public void SaveFile(List<string> file, string fileName)
        {
            string path = Path.GetDirectoryName(fileName) + "\\modified\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName1 = Path.GetFileName(fileName);
            try
            {
                using (TextWriter tw = new StreamWriter(path + fileName1))
                {
                    foreach (String s in file)
                        tw.WriteLine(s);
                }
            }
            catch (DirectoryNotFoundException dn)
            {

            }

        }

        public ArrayList m_arrFiles = new ArrayList();
        int rePlaceInCode = 0;

        public enum CrLf
        {
            EOF=0,
            IsCr,
            IsLf,
            IsCrLf,
            IsLfCr
        }

        private CrLf FindEndOfLine(byte[] binFile, long stIndex, ref long newStIndex, ref long lineLength)
        {
            bool bIsCr = false;
            bool bIsCrLf = false;
            bool bIsLf = false;
            bool bIsLfCr = false;

            long l = 0;
            for (l = stIndex; l < binFile.Length; l++)
            {
                if (binFile[l] == 13)
                {
                    lineLength = l - stIndex;
                    if ((l + 1) < binFile.Length)
                    {
                        if (binFile[l + 1] == 10)
                        {
                            bIsCrLf = true;
                            newStIndex = l + 1 + 1;
                            return CrLf.IsCrLf;
                        }
                       /* else
                        {
                            bIsCr = true;
                            newStIndex = l + 1 + 1;
                            return CrLf.IsCr;
                        }*/
                    }
                }

                /*if (binFile[l] == 10)
                {
                    lineLength = l - stIndex;
                    if ((l + 1) > binFile.Length)
                    {
                        if (binFile[l + 1] == 13)
                        {
                            bIsLfCr = true;
                            newStIndex = l + 1 + 1;
                            return CrLf.IsLfCr;

                        }
                        else
                        {
                            bIsLf = true;
                            newStIndex = l + 1 + 1;
                            return CrLf.IsLf;
                        }
                    }
            }*/
            }

            newStIndex = stIndex;
            lineLength = binFile.Length - stIndex;

            return CrLf.EOF;
        }

        List<List<Byte>> binLines = new List<List<byte>>();
        private bool bFormatText = false;

        private long ProcessBinDataExt(byte[] binFile)
        {
            binLines.Clear();
            long newline = 0;
            long lineSize = 0;
            long stOffset = 0;

            bool bIsLastCrLf = false;
            if (binFile.Length > 0)
            {
                if (binFile[binFile.Length - 1] == '\r' || binFile[binFile.Length - 1] == '\n')
                {
                    bIsLastCrLf = true;
                }
            }

            long stIndex = 0;
            long newStIndex = 0;
            long lineLength = 0;
            byte[] line;
            bool readLoop = true;
            while (readLoop)
            {
                CrLf endValue = FindEndOfLine(binFile, stIndex, ref newStIndex, ref lineLength);
                line = new byte[lineLength];
                Array.Copy(binFile, stIndex, line, 0, lineLength);

                string strLine = System.Text.Encoding.UTF8.GetString(line);
                string strLine1 = System.Text.Encoding.ASCII.GetString(line);

                binLines.Add(line.ToList());

                stIndex = newStIndex;
                newStIndex = 0;
                lineLength = 0;

                if (endValue == CrLf.EOF)
                {
                    break;
                }
            }

            //if (!bIsLastCrLf)
            //{
            //    long offset = binFile.Length - 1;
            //    List<byte> lastLine = new List<byte>();
            //    while (binFile[offset] != '\n')
            //    {
            //        lastLine.Add(binFile[offset]);
            //        offset--;
            //    }
            //    lastLine.Reverse();
            //    binLines.Add(lastLine);
            //    newline++;
            //}

            return newline;
        }

        private long ProcessBinData(byte[] binFile)
        {
            binLines.Clear();
            long newline = 0;
            long lineSize = 0;
            long stOffset = 0;

            bool bIsLastCrLf = false;
            if (binFile.Length > 0)
            {
                if (binFile[binFile.Length-1] == '\r' || binFile[binFile.Length-1] == '\n')
                {
                    bIsLastCrLf = true;
                }
            }

            for (long l = 0; l < binFile.Length; l++)
            {
                lineSize++;
//                if ((binFile[l] == '\r' && binFile[++l] == '\n') || binFile[l] == (byte)26 )
                if ((binFile[l] == '\r' || binFile[++l] == '\n') || binFile[l] == (byte)26)
                {
                    lineSize--;
                    while(binFile[stOffset] == '\r' || binFile[stOffset] == '\n')
                    {
                        stOffset++;
                        if (stOffset >= binFile.Length)
                            return newline;
                    }

                    byte[] line = new byte[lineSize];
                    Array.Copy(binFile, stOffset, line, 0, lineSize);

                    long offset = lineSize - 1;

                    while (offset > 0 && (line[offset] == '\r' || line[offset] == '\n'))
                    {
                        line[offset] = 0;
                        offset--;
                    }

                    string strLine = System.Text.Encoding.UTF8.GetString(line);
                    string strLine1 = System.Text.Encoding.ASCII.GetString(line);

                    binLines.Add(line.ToList());
                    stOffset = l;
                    lineSize = 0;
                    newline++;
                }
            }

            if (!bIsLastCrLf)
            {
                long offset = binFile.Length - 1;
                List<byte> lastLine = new List<byte>();
                while (binFile[offset] != '\n')
                {
                    lastLine.Add(binFile[offset]);
                    offset--;
                }
                lastLine.Reverse();
                binLines.Add(lastLine);
                newline++;
            }

            return newline;
        }

        public void SearchPrgFiles(frmSQL pParent)
        {
            try
            {
                String strDir = this.searchDir;
                //Check First if the Selected Directory exists
                if (!Directory.Exists(strDir))
                    MessageBox.Show("Directory doesn't exist!", "Win Grep Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    this.txtSingleLineMsgBoxOutput.Invoke((MethodInvoker)delegate {
                        this.txtSingleLineMsgBoxOutput.Text = "";
                    });
                    this.txtMultiLineMessageBox.Invoke((MethodInvoker)delegate {
                        this.txtMultiLineMessageBox.Text = "";
                    });
                    this.txtConcateMsgBox.Invoke((MethodInvoker)delegate {
                        this.txtConcateMsgBox.Text = "";
                    });
                    this.txtSmText.Invoke((MethodInvoker)delegate {
                        this.txtSmText.Text = "";
                    });

                    //Initialize the Flags
                    bool bRecursive = true;  //ckRecursive.Checked;
                    bool bIgnoreCase = true; // ckIgnoreCase.Checked;
                    bool bJustFiles = false; // ckJustFiles.Checked;

                    bool bLineNumbers = true;
                    bool bCountLines = true; ;

                    //File Extension
                    String strExt = pParent.searchFiles;
                    //First empty the list
                    m_arrFiles.Clear();
                    //Create recursively a list with all the files complying with the criteria
                    String[] astrExt = strExt.Split(new Char[] { ',' });
                    for (int i = 0; i < astrExt.Length; i++)
                    {
                        //Eliminate white spaces
                        astrExt[i] = astrExt[i].Trim();
                        GetFiles(strDir, astrExt[i], bRecursive);
                    }
                    //Now all the Files are in the ArrayList, open each one
                    //iteratively and look for the search string
                    String strSearch = "MessageBox";
                    String strResults = "";
                    String strMultiLineResults = "";
                    String strConcateLineResults = "";
                    String strLine ="";
                    String strMultiLine="";
                    String strConcateLine = "";
                    String strSingleLine = "";
                    int iLine, iCount, iCountMultiLine, iCountConcateLines, iLineMulti;
                    bool bEmpty = true;
                    IEnumerator enm = m_arrFiles.GetEnumerator();
                    while (enm.MoveNext())
                    {
                        if ((enm.Current.ToString().Contains("modified")))
                            continue;
                        List<string> file = new List<string>();
                        modifiedLines.Clear();
                        if (enm.Current.ToString().IndexOf("z_") >= 0)
                        {
                            continue;
                        }

                        if (enm.Current.ToString().IndexOf("x_") >= 0)
                        {
                            continue;
                        }

                        if (enm.Current.ToString().IndexOf("transferfiles.prg") >= 0)
                        {
                            continue;
                        }

                        try
                        {
                            this.txtSingleLineMsgBoxOutput.Invoke((MethodInvoker)delegate {
                                this.txtSingleLineMsgBoxOutput.Text = (string)enm.Current;
                            });
                            this.txtMultiLineMessageBox.Invoke((MethodInvoker)delegate {
                                this.txtMultiLineMessageBox.Text = (string)enm.Current;
                            });
                            this.txtConcateMsgBox.Invoke((MethodInvoker)delegate {
                                this.txtConcateMsgBox.Text = (string)enm.Current;
                            });

                            StreamReader sr = File.OpenText((string)enm.Current);

                            byte[] binData = File.ReadAllBytes((string)enm.Current);
                            long totLine = ProcessBinData(binData);

                            iLine = 0;
                            iCount = 0;
                            iCountMultiLine = 0;
                            iCountConcateLines = 0;
                            iLineMulti = 0;
                            bool bFirst = true;
                            bool bModifiedFile = false;
                            string fileName = (string)enm.Current;
                            while ((strLine = sr.ReadLine()) != null)
                            {
                                file.Add(strLine);
                                iLine++;
                                iLineMulti++;
                                //Using Regular Expressions as a real Grep
                                Match mtch;
                                if (bIgnoreCase == true)
                                    mtch = Regex.Match(strLine, strSearch, RegexOptions.IgnoreCase);
                                else
                                    mtch = Regex.Match(strLine, strSearch);

                                if (mtch.Success == true)
                                {
                                    bModifiedFile = true;
                                    strSingleLine = strLine.Trim();

                                    if (!strSingleLine.Contains("\""))
                                        continue;

                                    if (strSingleLine[strSingleLine.Length - 1] == ';')
                                    {
                                        strMultiLine = strSingleLine.Replace(';', ' ');
                                        strMultiLine += GetFullFoxproLine(sr, file, ref iLineMulti);

                                        strConcateLine = "";
                                        if (strMultiLine.Contains('+') && IsNotPartOfCharacterCode(strLine))
                                        {
                                            strConcateLine = strMultiLine;
                                            strMultiLine = "";
                                        }
                                        strLine = "";
                                    }
                                    else
                                    {
                                        strLine = strSingleLine;
                                        if (strLine.Contains('+') && IsNotPartOfCharacterCode(strLine))
                                        {
                                            strConcateLine = strLine;
                                            strLine = "";
                                        }
                                        else
                                        {
                                            strConcateLine = "";
                                        }
                                        strMultiLine = "";
                                    }

                                    //strLine = AddSmGetText(strLine);
                                    //strMultiLine = AddSmGetText(strMultiLine);

                                    string modifiedLine = "";
                                    strLine = AddSmGetTextByText(strLine, ref modifiedLine, true);
                                    strMultiLine = AddSmGetText(strMultiLine, ref modifiedLine);
                                    strConcateLine = AddSmGetText(strConcateLine, ref modifiedLine);

                                    modifiedLine = CaseInsenstiveReplace(modifiedLine, "MESSAGEBOX", "msgbox");

                                    iLine = ReplaceTextExt(file, iLine, iLineMulti, modifiedLine);
                                    iLineMulti = iLine;
                                    bEmpty = false;
                                    iCount++;
                                    if (bFirst == true)
                                    {
                                        if (bJustFiles == true)
                                        {
                                            strResults += (string)enm.Current + Environment.NewLine;
                                            strMultiLineResults += (string)enm.Current + Environment.NewLine;
                                            strConcateLine += (string)enm.Current + Environment.NewLine;
                                            break;
                                        }
                                        else
                                        {
                                            strResults += (Environment.NewLine + (string)enm.Current + Environment.NewLine);
                                            strMultiLineResults += (Environment.NewLine + (string)enm.Current + Environment.NewLine);
                                            strConcateLine += (Environment.NewLine + (string)enm.Current + Environment.NewLine);
                                        }
                                        bFirst = false;
                                    }
                                    //Add the Line to Results string
                                    if (bLineNumbers == true)
                                    {
                                        if(strLine != string.Empty)
                                            strResults += "  " + iLine + ": " + strLine + Environment.NewLine;

                                        if(strMultiLine != string.Empty)
                                            strMultiLineResults += "  " + iLine + ": " + strMultiLine + Environment.NewLine + Environment.NewLine;

                                        if (strConcateLine != string.Empty)
                                            strConcateLineResults += "  " + iLine + ": " + strConcateLine + Environment.NewLine + Environment.NewLine;
                                    }
                                    else
                                    {
                                        strResults += "  " + strLine + Environment.NewLine;
                                        strMultiLineResults += "  " + strMultiLine + Environment.NewLine;
                                    }
                                }
                            }
                            sr.Close();
                            if (bModifiedFile)
                            {
                                SaveFile(file, (string)enm.Current);
                                SaveFileBin(file, (string)enm.Current);
                            }


                            if (bFirst == false)
                            {
                                if (bCountLines == true)
                                {
                                    if (strResults != string.Empty)
                                    {
                                        strResults += "  " + iCount + " Lines Matched" + Environment.NewLine;
                                        strResults += Environment.NewLine;
                                    }

                                    if (strMultiLineResults != string.Empty)
                                    {
                                        strMultiLineResults += "  " + iCount + " Lines Matched" + Environment.NewLine;
                                        strMultiLineResults += Environment.NewLine;
                                    }

                                    if (strConcateLineResults != string.Empty)
                                    {
                                        strConcateLineResults += "  " + iCount + " Lines Matched" + Environment.NewLine;
                                        strConcateLine += Environment.NewLine;
                                    }
                                }
                            }
                        }
                        catch (SecurityException)
                        {
                            strResults += "\r\n" + (string)enm.Current + ": Security Exception\r\n\r\n";
                        }
                        catch (FileNotFoundException)
                        {
                            strResults += "\r\n" + (string)enm.Current + ": File Not Found Exception\r\n";
                        }
                    }
                    this.txtSingleLineMsgBoxOutput.Invoke((MethodInvoker)delegate {
                        this.txtSingleLineMsgBoxOutput.Text = strResults;
                    });

                    this.txtMultiLineMessageBox.Invoke((MethodInvoker)delegate {
                        this.txtMultiLineMessageBox.Text = strMultiLineResults;
                    });
                    this.txtConcateMsgBox.Invoke((MethodInvoker)delegate {
                        this.txtConcateMsgBox.Text = strConcateLineResults;
                    });
                    ////  txtCurFile.Text = "";
                    //  if (bEmpty == true)
                    //     // txtResults.Text = "No matches found!";
                    //  else
                    //      //txtResults.Text = strResults;
                }
            }
            finally
            {
                if (Cursor == Cursors.WaitCursor)
                    Cursor = Cursors.Arrow;
            }
        }

        public bool IsNotPartOfCharacterCode(string msgStr)
        {
            string charSet = "0+48";

            int charset = 0;
            int nonCharset = 0;
            for (int i = 0; i < msgStr.Length; i++)
            {
                if (msgStr[i] == '+')
                {
                    if (msgStr[i - 1] == '0' || msgStr[i - 1] == '1' || msgStr[i - 1] == '2' || msgStr[i - 1] == '3' || msgStr[i - 1] == '4' || msgStr[i - 1] == '5' || msgStr[i - 1] == '6' || msgStr[i - 1] == '7' || msgStr[i - 1] == '8' || msgStr[i - 1] == '9')
                    {
                        charset++;
                    }
                    else
                    {
                        nonCharset++;
                    }
                }
            }

            if (nonCharset > 0)
            {
                return true;
            }
            return false;
        }

        public string GetFullFoxproLine(StreamReader sr, List<string> file, ref int iLineMulti)
        {
            string fullLine = "";

            if (sr == null)
            {
                return fullLine.Replace(';', ' '); ;
            }
            else
            {
                string strLine = "";
                while ((strLine = sr.ReadLine()) != null)
                {
                    iLineMulti++;
                    file.Add(strLine);
                    strLine = strLine.Trim();
                    fullLine += strLine;
                    if (strLine[strLine.Length - 1] != ';')
                    {
                        return fullLine.Replace(';', ' ');
                    }
                }
            }

            return fullLine.Replace(';', ' ');
        }

        public string AddSmGetTextByText(string msgStr, ref string modifiedLine, bool bSingleLine = false)
        {
            if (!msgStr.Contains("("))
            {
                return msgStr;
            }

            if (msgStr == string.Empty)
            {
                modifiedLine = msgStr;
                return msgStr;
            }

            if (msgStr.Trim()[0] == '*')
            {
                modifiedLine = msgStr;
                return msgStr;
            }
            if (string.IsNullOrEmpty(msgStr))
            {
                return "";
            }

            string originalText = msgStr;
            
            int msgboxIndex = msgStr.Trim().ToUpper().IndexOf("MESSAGEBOX");
            int stIndex = msgStr.IndexOf("\"", msgboxIndex>0?msgboxIndex:0);
            int enIndex = msgStr.IndexOf("\"", stIndex + 1);

            if (stIndex > 0 && msgboxIndex > stIndex)
            {
                stIndex = msgStr.IndexOf("\"", msgboxIndex);
                enIndex = msgStr.IndexOf("\"", stIndex + 1);
            }
            if (stIndex >= 0 && !bSingleLine)
            {
                msgStr = msgStr.ReplaceAt(stIndex, 1, "sm_get_text_by_text(\"");
                stIndex = msgStr.IndexOf("\"", msgboxIndex > 0 ? msgboxIndex : 0);
                enIndex = msgStr.IndexOf("\"", stIndex + 1);
                if (enIndex > 0)
                {
                    msgStr = msgStr.ReplaceAt(enIndex, 1, "\")");
                }
            }

            if (bSingleLine)
            {

                bool fullMatch;
                List<string> msgStrs = new List<string>();
                originalText = originalText.Replace("\"", string.Empty);
                string[] messageString = SplitMessageString(originalText, bSingleLine);
                if (messageString.Length <= 0)
                    return msgStr;

                msgStr = "";
                foreach (string msg in messageString)
                {
                    msgStr += msg;
                }
                modifiedLine = msgStr;

                if (bAddDbStringTranslations)
                {
                    msgStrs.Add(messageString[1]);
                    string dbString = GetTheMostMatchedString(msgStrs.ToArray(), out fullMatch);
                    this.txtSmText.Invoke((MethodInvoker)delegate {
                        this.txtSmText.Text += (Environment.NewLine + dbString + Environment.NewLine +
                        "---------------------------------------------------------------------------------------------------------------------------------------" +
                        Environment.NewLine + Environment.NewLine);
                    });

                    return (originalText + Environment.NewLine + msgStr + Environment.NewLine + dbString + Environment.NewLine + "-----------------");
                }
                else
                {
                    return (Environment.NewLine + "-----------------" + Environment.NewLine + originalText + Environment.NewLine + msgStr + Environment.NewLine + "-----------------");
                }
            }
            else
            {
                modifiedLine = msgStr;
                return msgStr;
            }
        }

        public string AddSmGetText(string msgStr, ref string modifiedLine)
        {
            string results="";

            HasIfWithAndOr(msgStr);

            if (msgStr == string.Empty)
            {
                return msgStr;
            }

            if (msgStr.Trim()[0] == '*')
            {
                return msgStr;
            }

            if (msgStr.Trim()[0] == '\"')
            {
                int i = 0;
                i = i + 1;
            }

            string[] messageString = SplitMessageString(msgStr, false);
            string[] msgStrs;
            bool bMultiConcate = false;


            if (messageString.Length == 0 && msgStr.Contains('+'))
            {
                bMultiConcate = true;
                msgStrs = msgStr.Split('+');
            }
            else if (messageString.Length < 2)
            {
                return msgStr;
            }
            else
            {
                msgStrs = messageString[1].Split('+');
            }

            string[] msgStrsFroDB = msgStrs.ToArray();

            for (int i = 0; i < msgStrs.Length; i++)
            {
                //msgStrs[i] = AddSmGetTextByText(msgStrs[i]);
                //int stIndex = msgStrs[i].IndexOf("\"");
                //int enIndex = msgStrs[i].IndexOf("\"", stIndex + 1);
                //if (stIndex >= 0)
                //{
                //    msgStrs[i] = msgStrs[i].ReplaceAt(stIndex, 1, "sm_get_text_by_text(\"");
                //    stIndex = msgStrs[i].IndexOf("\"");
                //    enIndex = msgStrs[i].IndexOf("\"", stIndex + 1);
                //    if (enIndex > 0)
                //    {
                //        msgStrs[i] = msgStrs[i].ReplaceAt(enIndex, 1, "\")");
                //    }
                //}
            }

            for (int i = 0; i < msgStrs.Length; i++)
            {
                if (i < msgStrs.Length - 1)
                {
                   results += msgStrs[i] + "+";
                }
                else
                {
                    results += msgStrs[i];
                }
            }

            results = ProcessVariables(results, true);

            if(!bMultiConcate)
                results = messageString[0] + results + messageString[2];

            modifiedLine = results;

            string returnString = Environment.NewLine + "==========================================================================================================" + Environment.NewLine;
            returnString += (msgStr + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;
            returnString += (results + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;
            bool fullMatch;

            if (bAddDbStringTranslations)
            {
                string dbString = GetTheMostMatchedString(msgStrsFroDB, out fullMatch);
                this.txtSmText.Invoke((MethodInvoker)delegate {
                    this.txtSmText.Text += (Environment.NewLine + dbString + Environment.NewLine +
                    "----------------------------------------------------------------------------------------------------------------------------------------" +
                    Environment.NewLine + Environment.NewLine);
                });

                if (fullMatch)
                    returnString += ("@@" + GetTheMostMatchedString(msgStrsFroDB, out fullMatch) + Environment.NewLine);
                else
                    returnString += (GetTheMostMatchedString(msgStrsFroDB, out fullMatch) + Environment.NewLine);
                returnString += "==========================================================================================================" + Environment.NewLine;
            }

            return returnString;
        }

        private bool HasIfWithAndOr(string msgStr)
        {
            if (msgStr.Contains(" if") && (msgStr.Contains(" or ") || msgStr.Contains(" and ")))
                return true;

            if ((msgStr.Contains(" or ") || msgStr.Contains(" and ")))
                return true;

            return false; ;
        }

        private string GetTheMostMatchedString(string[] msgStrsFroDB, out bool fullMatch)
        {
            fullMatch = false;
            string dbString = GetDBString(msgStrsFroDB);
            string[] dbStrings;

            if (dbString.Contains(Environment.NewLine))
            {
                dbString = dbString.Replace("\t", string.Empty);
                dbString = dbString.Replace(";", string.Empty);
                dbStrings = dbString.Split(Environment.NewLine.ToCharArray());
                dbStrings = dbStrings.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                if (dbStrings.Length > 1)
                {
                    List<int> matchSCore = new List<int>();
                    foreach (string dbStr in dbStrings)
                    {
                        matchSCore.Add(MatchStrings(dbStr.Split('+'), msgStrsFroDB));
                    }
                    int maxValue = matchSCore.Max();
                    if (maxValue >= msgStrsFroDB.Length)
                        fullMatch = true;
                    int maxIndex = matchSCore.ToList().IndexOf(maxValue);
//                    return (dbStrings[0] + Environment.NewLine + GetFrenchString(Convert.ToInt32(dbStrings[0])) + Environment.NewLine +dbStrings[maxIndex]);
                    return (dbStrings[0] + Environment.NewLine +
                "----------------------------------------------------------------------------------------------" + Environment.NewLine +
                "French" + Environment.NewLine +
                "----------------------------------------------------------------------------------------------" + Environment.NewLine +
                GetFrenchString(Convert.ToInt32(dbStrings[0])) + Environment.NewLine + GetEnglishString(Convert.ToInt32(dbStrings[0])));
                }
            }

            return dbString;
        }

        private string GetEnglishString(int id)
        {
            string sqlCommand = "Select english from sm_text where pk_sm_text = " + id.ToString();
            string returnString = "";
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                    DataSet ds = new DataSet();
                    int numRows = oldbDA.Fill(ds);
                    if (numRows == 1)
                    {
                        foreach (object item in ds.Tables[0].Rows[0].ItemArray)
                            returnString += (item.ToString().Trim() + Environment.NewLine);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (returnString.Contains("«") || returnString.Contains(";"))
            {
                ReplaceFunnyChars(id, true);
            }

            string paramString = ProcessVariables(returnString, true);
            return returnString + Environment.NewLine + /*"-----------" + Environment.NewLine*/ ">> " + paramString;
//            return returnString;
        }

        private string GetFrenchString(int id)
        {
            string sqlCommand = "Select french from sm_text where pk_sm_text = " + id.ToString();
            string returnString = "";
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                    DataSet ds = new DataSet();
                    int numRows = oldbDA.Fill(ds);
                    if (numRows == 1)
                    {
                        for(int i = 0; i < ds.Tables[0].Rows[0].ItemArray.Count(); i++)//each (object item in ds.Tables[0].Rows[0].ItemArray)
                        {
                            string sentance = ds.Tables[0].Rows[0].ItemArray[i].ToString().Trim();

                            string[] sentances = sentance.Split('+');

                            sentance = "";

                            for (int j = 0; j < sentances.Length; j++)
                            {
                                if (sentances[j].Trim().Length <= 0)
                                    continue;
                                if (!sentances[j].Contains('(') && !sentances[j].Contains(')'))
                                {
                                    if (sentances[j].Trim()[0] != '\"')
                                    {
                                        sentances[j] = ("\"" + sentances[j]);
                                    }
                                    if (sentances[j].Trim()[sentances[j].Trim().Length - 1] != '\"')
                                    {
                                        sentances[j] = (sentances[j] + "\"");
                                    }
                                }

                                sentance += (sentances[j] + "+");
                            }

                            if (sentance[sentance.Length - 1] == '+')
                            {
                                sentance = sentance.Substring(0, sentance.Length - 1);
                            }

                            returnString += (sentance.ToString().Trim() + Environment.NewLine);
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (returnString.Contains("«") || returnString.Contains(";"))
            {
                ReplaceFunnyChars(id, false);
            }

            string paramString = ProcessVariables(returnString, true);
            return returnString + /*Environment.NewLine + "----------" +*/ 
                Environment.NewLine + ">> " + paramString + Environment.NewLine + 
                "----------------------------------------------------------------------------------------------" + Environment.NewLine +
                "English " + Environment.NewLine +
                "----------------------------------------------------------------------------------------------" + Environment.NewLine;
        }

        private void ReplaceFunnyChars(int id, bool bEnglish)
        {
            string sqlCommand = "";
            if(bEnglish)
                sqlCommand =  "Select english from sm_text where pk_sm_text = " + id.ToString();
            else
                sqlCommand = "Select french from sm_text where pk_sm_text = " + id.ToString();

            string sentance = "";
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                    DataSet ds = new DataSet();
                    int numRows = oldbDA.Fill(ds);
                    if (numRows == 1)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows[0].ItemArray.Count(); i++)
                        {
                            sentance = ds.Tables[0].Rows[0].ItemArray[i].ToString().Trim();
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            sentance = sentance.Replace("«", "\"");
            sentance = sentance.Replace(";", String.Empty);
            sentance = sentance.Replace("'", "''");

            if(bEnglish)
                sqlCommand = "update sm_text set english = '" + sentance + "' where pk_sm_text = " + id.ToString();
            else
                sqlCommand = "update sm_text set french = '" + sentance + "' where pk_sm_text = " + id.ToString();


            ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    oldbCmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private int MatchStrings(string[] dbStr, string[] msgStrsFroDB)
        {
            int matchScore = 0;
            for (int i = 0; i < msgStrsFroDB.Length; i++)
            {
                for (int j = 0; j < dbStr.Length; j++)
                {
                    if (String.Compare(msgStrsFroDB[i].Trim(), dbStr[j].Trim(), false) == 0)
                    {
                        matchScore++;
                        break;
                    }
                }
            }

            return matchScore;
        }

        private string GetDBString(string[] msgStrsFroDB)
        {
            string returnString = "";

            if (msgStrsFroDB.Length <= 0)
                return returnString;

            string[] orderedArray  = msgStrsFroDB.OrderByDescending(aux => aux.Length).ToArray();

            for (int i = 0; i < orderedArray.Length; i++)
            {
                orderedArray[i] = orderedArray[i].Trim();
                orderedArray[i] = orderedArray[i].Replace("\"", string.Empty);
                orderedArray[i] = orderedArray[i].Replace("\t", string.Empty);
                orderedArray[i] = orderedArray[i].Replace("'", "''");
            }


            List<string> orderedList = new List<string>(orderedArray);
            for (int i = 0; i < orderedList.Count; i++)
            {
                if (string.IsNullOrEmpty(orderedList[i]))
                {
                    orderedList.RemoveAt(i);
                    --i;
                }
            }

            orderedArray = orderedList.ToArray();

            if (orderedList.Count <= 0)
                return "";

            string sqlCommand = "Select * from sm_text where english like '%" + orderedArray[0] + "%'";
            for (int i = 0; i < orderedArray.Length; i++)
            {

                if (orderedArray[i].ToUpper().Contains("CHR("))
                    continue;

                if (sqlCommand.ToUpper().Contains("IIF("))
                {
                    rePlaceInCode++;
                    return rePlaceInCode.ToString() + "Replace in Code";
                }
                    

                returnString = ExecuteSQL(sqlCommand, out int numRows);
                if (numRows == 1)
                    return returnString;
                else
                {
                    if (numRows <= 0)
                    {
                        sqlCommand = "Select * from sm_text where english like '%" + orderedArray[i] + "%'";
                    }
                    else
                    {
                        i++;
                        for (int j = 0; j <= i && i < orderedArray.Length && j < orderedArray.Length; j++)
                        {
                            if (orderedArray[j].ToUpper().Contains("CHR("))
                            {
                                continue;
                            }
                            if (j == 0)
                                sqlCommand = "Select * from sm_text where english like '%" + orderedArray[j] + "%' ";
                            else
                            {
                                if (j >= orderedArray.Length)
                                    return returnString;
                                else
                                    sqlCommand += "and english like '%" + orderedArray[j] + "%' ";
                            }
                        }
                    }
                }
            }
            return returnString;
        }

        private string ExecuteSQL(string sqlCommand, out int numOfRows)
        {
            numOfRows = 0;
            string returnString = "";
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                    DataSet ds = new DataSet();
                    int numRows = oldbDA.Fill(ds);
                    numOfRows = numRows;
                    if (numRows > 1)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            foreach(object item in ds.Tables[0].Rows[j].ItemArray)
                                returnString += (item.ToString() + Environment.NewLine);
                        }
                        //return returnString;
                    }
                    else if (numRows == 1)
                    {
                        foreach (object item in ds.Tables[0].Rows[0].ItemArray)
                            returnString += (item.ToString() + Environment.NewLine);
//                        returnString += (ds.Tables[0].Rows[0].ToString() + Environment.NewLine);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return returnString;
        }

        private string ProcessVariables(string msgStr, bool removeQuotes)
        {
            bool bFullLoop = false;
            if (msgStr.Trim()[msgStr.Trim().Length-1] ==')')
            {
                bFullLoop = true;
            }

            string[] msgStrs = msgStr.Split('+');
            string results = "";

            Queue<string> parameteres = new Queue<string>();

            for (int i = 0, j=1; i < (bFullLoop? msgStrs.Length: msgStrs.Length - 1); i++)
            {
                if (IsVariable(msgStrs[i]))
                {
                    parameteres.Enqueue(msgStrs[i]);
                    msgStrs[i] = "{" + j.ToString() + "}";
                    j++;
                }
            }

            for (int i = 0; i < msgStrs.Length; i++)
            {
                if (i < msgStrs.Length - 1)
                {
                    results += msgStrs[i].Trim() + " ";// "+";
                }
                else
                {
                    results += msgStrs[i];
                }
            }

            if (removeQuotes)
            {
                results = results.Replace("\"", string.Empty);
                results = results.Replace(Environment.NewLine, string.Empty);
                results = "\"" + results + "\"";
            }

            var list = results.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s));
            results = string.Join(" ", list);

            while (parameteres.Count > 0)
            {
                string param = parameteres.Dequeue().Trim();
                if(param.ToUpper().IndexOf("CHR(") == 0)
                    results += (", \"" + param.Trim() + "\"");
                else
                    results += (", " + param.Trim());
            }

            return "sm_get_text_by_record(" + results + ")";
        }

        public string[] SplitMessageString(string msgStr, bool bSingleLineMsg)
        {
            bool bIsQuoted = false;
            int openBracket = 0;
            int splitIndex = -1;

            for (int i = 0; i < msgStr.Length; i++)
            {
                if (msgStr[i] == '\"' && bIsQuoted)
                {
                    bIsQuoted = false;
                    continue;
                }

                if (msgStr[i] == '\"' && !bIsQuoted)
                {
                    bIsQuoted = true;
                    continue;
                }

                if (msgStr[i] == '(')
                {
                    openBracket++;
                    continue;
                }

                if (msgStr[i] == ')')
                {
                    openBracket--;
                    continue;
                }

                if (msgStr[i] == ',' && !bIsQuoted && openBracket == 1)
                {
                    splitIndex = i;
                    break;
                }
            }

            List<string> quotedString = new List<string>();
            if (splitIndex > 0)
            {
                int firstBracketIndex = msgStr.IndexOf("(");
                if (bSingleLineMsg)
                {
                    string msgWord = msgStr.Substring(0, firstBracketIndex + 1);
                    msgWord = msgWord.ToUpper().Replace("MESSAGEBOX", "msgbox");
                    quotedString.Add(msgWord);
                    quotedString.Add("\"" + msgStr.Substring(firstBracketIndex + 1, (splitIndex - firstBracketIndex) - 1) + "\"");
                }
                else
                {
                    quotedString.Add(msgStr.Substring(0, firstBracketIndex + 1));
                    quotedString.Add(msgStr.Substring(firstBracketIndex + 1, (splitIndex - firstBracketIndex) - 1));
                }

                //quotedString.Add(msgStr.Substring(firstBracketIndex + 1, (splitIndex - firstBracketIndex) -1));
                quotedString.Add(msgStr.Substring(splitIndex, (msgStr.Length - splitIndex)));
            }
            else
            {
                int firstBracketIndex = msgStr.IndexOf("(");
                int closeBracketIndex = msgStr.IndexOf(")");

                if (closeBracketIndex < 0 && bSingleLineMsg) //lcMsgBox
                {
                    quotedString.Clear();
                    return quotedString.ToArray();
                }

                string msgWord = msgStr.Substring(0, firstBracketIndex + 1);
                msgWord = msgWord.ToUpper().Replace("MESSAGEBOX", "msgbox");
                quotedString.Add(msgWord);

                msgWord = "\""+msgStr.Substring(firstBracketIndex + 1, closeBracketIndex- firstBracketIndex-1) +"\"";
                quotedString.Add(msgWord);

                msgWord = msgStr.Substring(closeBracketIndex, msgStr.Length- closeBracketIndex);
                quotedString.Add(msgWord);

            }

            return quotedString.ToArray();
        }

        private bool IsVariable(string msgStr)
        {
            msgStr = msgStr.Trim();
            if(msgStr.Contains("\""))
            {
                return false;
            }

            if (IsNumeric(msgStr.Trim()))
            {
                return false;
            }

            if ( msgStr.Contains("="))
            {
                return false;
            }

            if (msgStr.Contains("(") && msgStr.Contains(")"))
            {
                //Count para
                int totalPara = 0;
                for (int i = 0; i < msgStr.Length; i++)
                {
                    if (msgStr[i] == '(')
                        totalPara++;

                    if (msgStr[i] == ')')
                        totalPara--;

                }

                if (totalPara == 0 && ( (msgStr[0] >= 65 && msgStr[0] <= 90) || (msgStr[0] >= 97 && msgStr[0] <= 122)))
                    return true;
                else
                    return false;
            }


            return true;
        }

        /// <summary>
        /// Extension method that works out if a string is numeric or not
        /// </summary>
        /// <param name="str">string that may be a number</param>
        /// <returns>true if numeric, false if not</returns>
        public bool IsNumeric(String str)
        {
            double myNum = 0;
            if (Double.TryParse(str, out myNum))
            {
                return true;
            }
            return false;
        }

        public void ClearTextBoxes()
        {
            this.txtSingleLineMsgBoxOutput.Text = "";
            this.txtMultiLineMessageBox.Text = "";
            this.txtMultiLineMessageBox.Text = "";
            this.txtSmText.Text = "";
        }
        public void AddTextToSmText(string txtBoxText)
        {
            this.txtSmText.AppendText(txtBoxText + Environment.NewLine);
        }

        public void SearchPrgFilesBin(frmSQL pParent)
        {
            try
            {
                String strDir = this.searchDir;
                //Check First if the Selected Directory exists
                if (!Directory.Exists(strDir))
                    MessageBox.Show("Directory doesn't exist!", "Win Grep Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    this.txtSingleLineMsgBoxOutput.Invoke((MethodInvoker)delegate {
                        this.txtSingleLineMsgBoxOutput.Text = "";
                    });
                    this.txtMultiLineMessageBox.Invoke((MethodInvoker)delegate {
                        this.txtMultiLineMessageBox.Text = "";
                    });
                    this.txtConcateMsgBox.Invoke((MethodInvoker)delegate {
                        this.txtConcateMsgBox.Text = "";
                    });
                    this.txtSmText.Invoke((MethodInvoker)delegate {
                        this.txtSmText.Text = "";
                    });

                    //Initialize the Flags
                    bool bRecursive = true;  //ckRecursive.Checked;
                    bool bIgnoreCase = true; // ckIgnoreCase.Checked;
                    bool bJustFiles = false; // ckJustFiles.Checked;

                    bool bLineNumbers = true;
                    bool bCountLines = true; ;

                    //File Extension
                    String strExt = pParent.searchFiles;
                    //First empty the list
                    m_arrFiles.Clear();
                    //Create recursively a list with all the files complying with the criteria
                    String[] astrExt = strExt.Split(new Char[] { ',' });
                    for (int i = 0; i < astrExt.Length; i++)
                    {
                        //Eliminate white spaces
                        astrExt[i] = astrExt[i].Trim();
                        GetFiles(strDir, astrExt[i], bRecursive);
                    }
                    //Now all the Files are in the ArrayList, open each one
                    //iteratively and look for the search string
                    String strSearch = "MessageBox";
                    String strResults = "";
                    String strMultiLineResults = "";
                    String strConcateLineResults = "";
                    String strLine = "";
                    String strMultiLine = "";
                    String strConcateLine = "";
                    String strSingleLine = "";
                    int iLine, iCount, iCountMultiLine, iCountConcateLines, iLineMulti;
                    bool bEmpty = true;
                    IEnumerator enm = m_arrFiles.GetEnumerator();
                    while (enm.MoveNext())
                    {
                        if ((enm.Current.ToString().Contains("modified")))
                            continue;

                        List<string> file = new List<string>();
                        modifiedBinLines.Clear();
                        binLines.Clear();

                        if (enm.Current.ToString().IndexOf("z_") >= 0)
                        {
                            continue;
                        }

                        if (enm.Current.ToString().IndexOf("x_") >= 0)
                        {
                            continue;
                        }

                        if (enm.Current.ToString().IndexOf("transferfiles.prg") >= 0)
                        {
                            continue;
                        }

                        try
                        {
                            this.txtSingleLineMsgBoxOutput.Invoke((MethodInvoker)delegate
                            {
                                this.txtSingleLineMsgBoxOutput.Text = (string)enm.Current;
                            });
                            this.txtMultiLineMessageBox.Invoke((MethodInvoker)delegate
                            {
                                this.txtMultiLineMessageBox.Text = (string)enm.Current;
                            });
                            this.txtConcateMsgBox.Invoke((MethodInvoker)delegate
                            {
                                this.txtConcateMsgBox.Text = (string)enm.Current;
                            });

                            StreamReader sr = File.OpenText((string)enm.Current);

                            byte[] binData = File.ReadAllBytes((string)enm.Current);

                            long totLine = ProcessBinDataExt(binData);

                            iLine = 0;
                            iCount = 0;
                            iCountMultiLine = 0;
                            iCountConcateLines = 0;
                            iLineMulti = 0;
                            bool bFirst = true;
                            bool bModifiedFile = false;
                            string fileName = (string)enm.Current;
                            for (int binLineIdx = 0; binLineIdx < binLines.Count(); binLineIdx++)
                            {
                                strLine = System.Text.Encoding.UTF8.GetString(binLines[binLineIdx].ToArray());
 /*                               this.txtSmText.Invoke((MethodInvoker)delegate {
                                    this.txtSmText.Text = strLine + Environment.NewLine;
                                });

                            }
                            return;
                            while ((strLine = sr.ReadLine()) != null)
                            {*/
                                file.Add(strLine);
                                iLine = binLineIdx;// ++;
                                iLineMulti = binLineIdx;  //++;
                                //Using Regular Expressions as a real Grep
                                Match mtch;
                                if (bIgnoreCase == true)
                                    mtch = Regex.Match(strLine, strSearch, RegexOptions.IgnoreCase);
                                else
                                    mtch = Regex.Match(strLine, strSearch);

                                if (mtch.Success == true)
                                {
                                    bModifiedFile = true;
                                    strSingleLine = strLine.Trim();

                                    if (!strSingleLine.Contains("\""))
                                        continue;

                                    if (strSingleLine[strSingleLine.Length - 1] == ';')
                                    {
                                        strMultiLine = strSingleLine.Replace(';', ' ');
                                        strMultiLine += GetFullFoxproLineBin(file, ref iLineMulti);

                                        strConcateLine = "";
                                        if (strMultiLine.Contains('+') && IsNotPartOfCharacterCode(strLine))
                                        {
                                            strConcateLine = strMultiLine;
                                            strMultiLine = "";
                                        }
                                        strLine = "";
                                    }
                                    else
                                    {
                                        strLine = strSingleLine;
                                        if (strLine.Contains('+') && IsNotPartOfCharacterCode(strLine))
                                        {
                                            strConcateLine = strLine;
                                            strLine = "";
                                        }
                                        else
                                        {
                                            strConcateLine = "";
                                        }
                                        strMultiLine = "";
                                    }

                                    //strLine = AddSmGetText(strLine);
                                    //strMultiLine = AddSmGetText(strMultiLine);

                                    string modifiedLine = "";
                                    strLine = AddSmGetTextByText(strLine, ref modifiedLine, true);
                                    strMultiLine = AddSmGetText(strMultiLine, ref modifiedLine);
                                    strConcateLine = AddSmGetText(strConcateLine, ref modifiedLine);

                                    modifiedLine = CaseInsenstiveReplace(modifiedLine, "MESSAGEBOX", "msgbox");

                                    iLine = ReplaceTextExt(file, iLine, iLineMulti, modifiedLine);
                                    iLineMulti = iLine;
                                    bEmpty = false;
                                    iCount++;
                                    if (bFirst == true)
                                    {
                                        if (bJustFiles == true)
                                        {
                                            strResults += (string)enm.Current + Environment.NewLine;
                                            strMultiLineResults += (string)enm.Current + Environment.NewLine;
                                            strConcateLine += (string)enm.Current + Environment.NewLine;
                                            break;
                                        }
                                        else
                                        {
                                            strResults += (Environment.NewLine + (string)enm.Current + Environment.NewLine);
                                            strMultiLineResults += (Environment.NewLine + (string)enm.Current + Environment.NewLine);
                                            strConcateLine += (Environment.NewLine + (string)enm.Current + Environment.NewLine);
                                        }
                                        bFirst = false;
                                    }
                                    //Add the Line to Results string
                                    if (bLineNumbers == true)
                                    {
                                        if (strLine != string.Empty)
                                            strResults += "  " + iLine + ": " + strLine + Environment.NewLine;

                                        if (strMultiLine != string.Empty)
                                            strMultiLineResults += "  " + iLine + ": " + strMultiLine + Environment.NewLine + Environment.NewLine;

                                        if (strConcateLine != string.Empty)
                                            strConcateLineResults += "  " + iLine + ": " + strConcateLine + Environment.NewLine + Environment.NewLine;
                                    }
                                    else
                                    {
                                        strResults += "  " + strLine + Environment.NewLine;
                                        strMultiLineResults += "  " + strMultiLine + Environment.NewLine;
                                    }
                                }
                            }
                            sr.Close();
                            if (bModifiedFile)
                            {
                                InjectNewBinLine();
                                //SaveFile(file, (string)enm.Current);
                                SaveFileBin(file, (string)enm.Current);
                            }


                            if (bFirst == false)
                            {
                                if (bCountLines == true)
                                {
                                    if (strResults != string.Empty)
                                    {
                                        strResults += "  " + iCount + " Lines Matched" + Environment.NewLine;
                                        strResults += Environment.NewLine;
                                    }

                                    if (strMultiLineResults != string.Empty)
                                    {
                                        strMultiLineResults += "  " + iCount + " Lines Matched" + Environment.NewLine;
                                        strMultiLineResults += Environment.NewLine;
                                    }

                                    if (strConcateLineResults != string.Empty)
                                    {
                                        strConcateLineResults += "  " + iCount + " Lines Matched" + Environment.NewLine;
                                        strConcateLine += Environment.NewLine;
                                    }
                                }
                            }
                        }
                        catch (SecurityException)
                        {
                            strResults += "\r\n" + (string)enm.Current + ": Security Exception\r\n\r\n";
                        }
                        catch (FileNotFoundException)
                        {
                            strResults += "\r\n" + (string)enm.Current + ": File Not Found Exception\r\n";
                        }
                    }
                    this.txtSingleLineMsgBoxOutput.Invoke((MethodInvoker)delegate {
                        this.txtSingleLineMsgBoxOutput.Text = strResults;
                    });

                    this.txtMultiLineMessageBox.Invoke((MethodInvoker)delegate {
                        this.txtMultiLineMessageBox.Text = strMultiLineResults;
                    });
                    this.txtConcateMsgBox.Invoke((MethodInvoker)delegate {
                        this.txtConcateMsgBox.Text = strConcateLineResults;
                    });
                    ////  txtCurFile.Text = "";
                    //  if (bEmpty == true)
                    //     // txtResults.Text = "No matches found!";
                    //  else
                    //      //txtResults.Text = strResults;
                }
            }
            finally
            {
                if (Cursor == Cursors.WaitCursor)
                    Cursor = Cursors.Arrow;
            }
        }

        private void InjectNewBinLine()
        {
            for (int i =0; i < modifiedBinLines.Count(); i++)
            {
                Tuple<int, string> mLine = modifiedBinLines[i];
                string rec = mLine.Item2;
                binLines[mLine.Item1] = Encoding.UTF8.GetBytes(rec).ToList();
            }
        }

        public string GetFullFoxproLineBin( List<string> file, ref int iLineMulti)
        {
            string fullLine = "";

            {
                string strLine = "";
                //while ((strLine = sr.ReadLine()) != null)
                while(iLineMulti < binLines.Count())
                {
                    iLineMulti++;
                    strLine = System.Text.Encoding.UTF8.GetString(binLines[iLineMulti].ToArray());
                    string strLineAnsi = System.Text.Encoding.ASCII.GetString(binLines[iLineMulti].ToArray());
                    file.Add(strLine);
                    strLine = strLine.Trim();
                    fullLine += strLine;
                    if (strLine[strLine.Length - 1] != ';')
                    {
                        return fullLine.Replace(';', ' ');
                    }
                }
            }

            return fullLine.Replace(';', ' ');
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnSQLResultWindow_Click(object sender, EventArgs e)
        {
            foreach (SqlResults sr in sqlResults)
            {
                Thread thread = new Thread(() => ShowSQLResultsInNewForm(sr));
                thread.IsBackground = false;
                thread.Start();
            }
        }

        private void ShowSQLResultsInNewForm(SqlResults sr)
        {
            frmSQLResults fsr = new frmSQLResults(sr);
            fsr.ShowDialog();
        }

        private void ShowSQLCommandLogsForm()
        {
            frmSQLCommandLogs fsl = new frmSQLCommandLogs();
            fsl.ShowDialog();
        }

        private void btnOpenSQLLogs_Click(object sender, EventArgs e)
        {
            
            if (File.Exists(SQLExecutor.sqlCommandLogFile))
            {
                Thread thread = new Thread(() => ShowSQLCommandLogsForm());
                thread.Start();
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text Files | *.txt";
                ofd.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SQLExecutor.sqlCommandLogFile = ofd.FileName;
                    Thread thread = new Thread(() => ShowSQLCommandLogsForm());
                    thread.Start();
                }
            }
        }

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                bFormatText = true;
                Debug.WriteLine(txtQuery.Text.Trim());
            }
        }

        private void flLayoyt_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.Width = flLayoyt.Width - 25;
        }

        private void frmSQL_ResizeEnd(object sender, EventArgs e)
        {
            foreach (Control c in flLayoyt.Controls)
            {
                c.Width = flLayoyt.Width - 25;
            }
        }

        private void frmSQL_Resize(object sender, EventArgs e)
        {
            foreach (Control c in flLayoyt.Controls)
            {
                c.Width = flLayoyt.Width - 25;
            }
        }
    }
}
