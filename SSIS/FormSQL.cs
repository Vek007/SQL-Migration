using SSIS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSIS
{
    public partial class FormSQL : Form
    {
        private string tableName = "";
        ContextMenu popupMenu = new ContextMenu();

        //private List<Variable> variables = new List<Variable>();
        private static string constConnString = @"Provider=VFPOLEDB.1;Data Source=";// E:\client_data\nossack\dist_ii.dbc;providerName=System.Data.OleDb";
        private string constDataSource = @"\dist_ii.dbc;providerName=System.Data.OleDb";

        private string connString = @"Provider=VFPOLEDB.1;Data Source=";// E:\client_data\nossack\dist_ii.dbc;providerName=System.Data.OleDb";
        private string dataSource = @"\dist_ii.dbc;providerName=System.Data.OleDb";

        //        private string SQLServerConnString = @"Server=VivekPC\SQL;Database=DtecSQLDatabase;User Id = Dtec; Password=1Intersect$";
        //data source=VIVEKPC\SQL;initial catalog=ts;user id=sa;password=LionGirnar007;

        //data source = DESKTOP - B6OO0H7\VEKSQLSERVER;initial catalog = ts; user id = sa; password=pASS1111;
        private string SQLServerConnString = @"Provider=sqloledb;data source=DESKTOP-B6OO0H7\VEKSQLSERVER;initial catalog = ts; user id = sa; password=pASS1111;MultipleActiveResultSets=True";// providerName="System.Data.SqlClient";


        private string SQLServerConnString1 = @"Provider=sqloledb;Data Source =VivekPC\SQL;Initial Catalog=ts;Integrated Security=SSPI;User Id=sa;Password=LionGirnar007;MultipleActiveResultSets=True";// providerName="System.Data.SqlClient";

        private string connStringFr = @"Provider=VFPOLEDB.1;Data Source=";// E:\client_data\nossack\dist_ii.dbc;providerName=System.Data.OleDb";
        private string dataSourceFr = @"\dist_ii.dbc;providerName=System.Data.OleDb";

        public static bool bAddDbStringTranslations = false;
        public FormSQL()
        {
            this.KeyPreview = true;
            InitializeComponent();
        }

        List<SqlResults> sqlResults = new List<SqlResults>();

        private void GetData(string selectCommand)
        {
            var def = DateTime.MinValue;
            SQLExecutor sqlData = new SQLExecutor();

            sqlResults.Clear();

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
                sqlResults = sqlData.DataExecutor(selectCommand, SQLServerConnString);
            }

            flLayoyt.Invoke((MethodInvoker)delegate {
                flLayoyt.Controls.Clear();
                flLayoyt.SuspendLayout();
                foreach (SqlResults sr in sqlResults)
                {
                    //sr.Dock = DockStyle.Fill;
                    //sr.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    flLayoyt.Controls.Add(sr);
                }

                foreach (Control ct in flLayoyt.Controls)
                {
                   // ct.Dock = DockStyle.Fill;
                }

                flLayoyt.ResumeLayout();
                flLayoyt.Refresh();
                btnExecuteQuery.Enabled = true;
                pnlWaitQuery.Visible = false;
            });
        }

        SSIS.DashBoard db = new SSIS.DashBoard();
        private void btnSearchSym_Click(object sender, EventArgs e)
        {
            txtQuery.Text = string.Empty;
            string[] syml = txtSym.Text.Trim().Split('-');
            string sym = syml[0];
            string ex = syml[1];

            {
                txtQuery.Text += "select * from pr where pr.rt = trim('" + sym + "'); " + Environment.NewLine;
                txtQuery.Text += "select * from ar_view where ar_view.rt = trim('" + sym + "');" + Environment.NewLine;
                txtQuery.Text += "select * from per where per.rt = trim('" + sym + "');" + Environment.NewLine;

                if (ex.Trim().ToLower() == "x")
                {
                    txtQuery.Text += "select * from tx_sec_view where root_ticker = trim('" + sym + "');" + Environment.NewLine;
                    txtQuery.Text += "select * from tx_res_view where root_ticker = trim('" + sym + "');" + Environment.NewLine;
                    txtQuery.Text += "select * from tx_region_view where root_ticker = trim('" + sym + "');" + Environment.NewLine;
                }
                else
                {
                    txtQuery.Text += "select * from t_sec_view where root__ticker = trim('" + sym + "');" + Environment.NewLine;
                    txtQuery.Text += "select * from t_res_view where root__ticker = trim('" + sym + "');" + Environment.NewLine;
                    txtQuery.Text += "select * from t_region_view where root__ticker = trim('" + sym + "');" + Environment.NewLine;
                }
            }

            if (chkDb.Checked)
            {
                if (this.db == null)
                {
                    this.db = new SSIS.DashBoard();
                }
                this.db.Hide();
                SSIS.tsEntities alDb = new SSIS.tsEntities();
                this.db.SetSymbol(sym, ex);
                this.db.Show();
            }

            txtQuery.SelectionStart = 0;
            txtQuery.SelectionLength = txtQuery.Text.Length;

            chkSqlServer.Checked = true;
            chkSqlServer.CheckState = CheckState.Checked;
            btnExecuteQuery_Click(null, null);

            Debug.WriteLine(Webber.GetMStarOwnershipInfo(sym, ex));
        }

        private void btnExecuteQuery_Click(object sender, EventArgs e)
        {
            string sqlCmd = txtQuery.SelectedText;
            btnExecuteQuery.Enabled = false;
            pnlWaitQuery.Visible = true;
            Thread thread = new Thread(() => GetData(sqlCmd));
            thread.Start();
        }

        private void btnSSIS_Click(object sender, EventArgs e)
        {
            SSIS.tsEntities alDb = new SSIS.tsEntities();
            alDb.PopulateNewFromFile(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\new.txt");
            alDb.PopulateArFromFile(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\ar.txt");
            alDb.PopulatePerFromFile(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\per.txt");
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
        private void ShowSQLCommandLogsForm()
        {
            frmSQLCommandLogs fsl = new frmSQLCommandLogs();
            fsl.ShowDialog();
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

        private void txtQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                bFormatText = true;
                Debug.WriteLine(txtQuery.Text.Trim());
            }
        }
        private bool bFormatText = false;

        private void FormatQueryTextBox()
        {
            String newText = String.Empty;
            List<String> lines = txtQuery.Text.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            txtQuery.Text = string.Empty;
            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
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

        private void FormSQL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnExecuteQuery_Click(null, null);
            }
        }
    }
}
