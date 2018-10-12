using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SSIS
{
    public partial class SqlResults : UserControl
    {
        ToolTip toolTip1 = new ToolTip();

        private string connString = string.Empty;

        public SqlResults()
        {
            InitializeComponent();
            toolTip1.ShowAlways = true;

            this.txtConnString.ContextMenuStrip = this.cnxtMenu;
            this.txtInputQuery.ContextMenuStrip = this.cnxtMenu;
        }

        public void AssignDataSourceToDGV(DataTable dt, string connString = "")
        {
            this.txtConnString.Text = connString;
            dgvSQLResults.DataSource = dt;
            dgvSQLResults.Refresh();

            PopulateTransposeGrid(0);

        }

        public void PopulateTransposeGrid(int rowIndex)
        {
            dgvTranspose.Rows.Clear();
            if (dgvSQLResults.Rows.Count > rowIndex)
            {
                dgvTranspose.Rows.Add(dgvSQLResults.ColumnCount);
                foreach (DataGridViewColumn dgvc in dgvSQLResults.Columns)
                {
                    dgvTranspose.Rows[dgvc.Index].Cells[0].Value = dgvc.HeaderText;
                    dgvTranspose.Rows[dgvc.Index].Cells[1].Value = dgvSQLResults.Rows[rowIndex].Cells[dgvc.Index].Value;
                }
            }
        }

        public string GetConnectionString()
        {
            return this.txtConnString.Text;
        }

        public string GetQueryString()
        {
            return this.txtInputQuery.Text;
        }

        public DataTable GetQueryDataSource()
        {
            return (DataTable)this.dgvSQLResults.DataSource;
        }

        public string GetMessage()
        {
            return this.txtSQLMessages.Text;
        }

        public void AddTextToMessages(string strMessage, bool deletePreviousText, bool addNewLineBefore)
        {
            if (deletePreviousText)
                txtSQLMessages.Text = strMessage;
            else
                txtSQLMessages.Text += (addNewLineBefore? (Environment.NewLine + strMessage) : strMessage);


            //toolTip1.SetToolTip(txtSQLMessages, txtSQLMessages.Text);
        }

        public void AddInputQuery(string strMessage, bool deletePreviousText, bool addNewLineBefore)
        {

            if (deletePreviousText)
                txtInputQuery.Text = strMessage;
            else
                txtInputQuery.Text += (addNewLineBefore ? (Environment.NewLine + strMessage) : strMessage);


            toolTip1.SetToolTip(txtSQLMessages, txtSQLMessages.Text);
        }

        public void FormatQueryString()
        {
            string strMessage = txtInputQuery.Text.Trim();

            strMessage = strMessage.Replace(",", ("," + Environment.NewLine));

            strMessage = strMessage.Replace((Environment.NewLine + Environment.NewLine), (Environment.NewLine));

            txtInputQuery.Text = strMessage;
        }

        public void AddConnString(string strMessage)
        {
            this.txtConnString.Text = strMessage;
        }

        public void AdjustHeightBasedOnTotalRows()
        {

            if (!txtSQLMessages.Text.Contains("Error"))
            {
                //if error then keep the txtHeight as it is                //
                //dgvSQLResults.Height = dgvSQLResults.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvSQLResults.ColumnHeadersHeight + 2;
                tabSqlResults.Height = dgvSQLResults.Height + 35;
                txtSQLMessages.Height = dgvSQLResults.Height;
                this.Height = tabSqlResults.Height + 10;
            }
            else
            {
                tabSqlResults.SelectedTab = tabSqlResults.TabPages[1];
            }
        }

        public void ShowSizeButtons(bool show = true)
        {
            this.lblIncrease.Visible = show;
            this.lblDecreseSize.Visible = show;
        }

        private bool busy = false;

        private void txtSQLMessages_ClientSizeChanged(object sender, EventArgs e)
        {
            if (busy) return;
            busy = true;
            TextBox tb = sender as TextBox;
            Size tS = TextRenderer.MeasureText(tb.Text, tb.Font);
            bool Hsb = tb.ClientSize.Height < tS.Height + Convert.ToInt32(tb.Font.Size);
            bool Vsb = tb.ClientSize.Width < tS.Width;

            if (Hsb && Vsb)
                tb.ScrollBars = ScrollBars.Both;
            else if (!Hsb && !Vsb)
                tb.ScrollBars = ScrollBars.None;
            else if (Hsb && !Vsb)
                tb.ScrollBars = ScrollBars.Vertical;
            else if (!Hsb && Vsb)
                tb.ScrollBars = ScrollBars.Horizontal;

            sender = tb as object;
            busy = false;
        }

        private void txtSQLMessages_TextChanged(object sender, EventArgs e)
        {
            if (busy) return;
            busy = true;
            TextBox tb = sender as TextBox;
            Size tS = TextRenderer.MeasureText(tb.Text, tb.Font);
            bool Hsb = tb.ClientSize.Height < tS.Height + Convert.ToInt32(tb.Font.Size);
            bool Vsb = tb.ClientSize.Width < tS.Width;

            if (Hsb && Vsb)
                tb.ScrollBars = ScrollBars.Both;
            else if (!Hsb && !Vsb)
                tb.ScrollBars = ScrollBars.None;
            else if (Hsb && !Vsb)
                tb.ScrollBars = ScrollBars.Vertical;
            else if (!Hsb && Vsb)
                tb.ScrollBars = ScrollBars.Horizontal;

            sender = tb as object;
            busy = false;

        }

        public List<string> GetColumnValueAsString(string columnName)
        {
            List<string> colValues = new List<string>();

            try
            {
                if (dgvSQLResults.DataSource != null)
                {
                    foreach (DataGridViewRow item in dgvSQLResults.Rows)
                    {
                        if(item.Cells[columnName].Value==null)
                            colValues.Add("");
                        else
                            colValues.Add(item.Cells[columnName].Value.ToString());
                    }
                }
                return colValues;
            }
            catch (Exception ex)
            {
                return colValues;
            }
        }

        public List<decimal> GetColumnValueAsDecimals(string columnName)
        {
            List<decimal> colValues = new List<decimal>();

            try
            {
                if (dgvSQLResults.DataSource != null)
                {
                    foreach (DataGridViewRow item in dgvSQLResults.Rows)
                    {
                        if (item.Cells[columnName].Value == null)
                            colValues.Add(0);
                        else
                            colValues.Add((decimal)item.Cells[columnName].Value);
                    }
                }
                return colValues;
            }
            catch (Exception ex)
            {
                return colValues;
            }
        }

        public List<Tuple<int,string>> GetMethodsWithId()
        {
            List<Tuple<int, string>> colValues = new List<Tuple<int, string>>();
            string colRef = "refno";
            string colMethod = "methods";

            try
            {
                if (dgvSQLResults.DataSource != null)
                {
                    foreach (DataGridViewRow item in dgvSQLResults.Rows)
                    {
                        int refno = Convert.ToInt32(item.Cells[colRef].Value);
                        string method = item.Cells[colMethod].Value.ToString();

                        Tuple<int, string> rec = Tuple.Create(refno, method);
                        colValues.Add(rec);
                    }
                }
                return colValues;
            }
            catch (Exception ex)
            {
                return colValues;
            }
        }

        private void lblIncrease_Click(object sender, EventArgs e)
        {
            if (this.dgvSQLResults.Rows.Count > 0)
            {
                this.Height += (this.dgvSQLResults.Rows[0].Height * 3);
            }
        }

        private void lblDecreseSize_Click(object sender, EventArgs e)
        {
            this.Height -= (this.dgvSQLResults.Rows[0].Height * 3);

            if (this.Height < 100)
            {
                this.Height = 100;
            }
        }

        private void tmrDecSize_Tick(object sender, EventArgs e)
        {
            lblDecreseSize_Click(null, null);
        }

        private void tmrIncSize_Tick(object sender, EventArgs e)
        {
            lblIncrease_Click(null, null);
        }

        private void lblIncrease_MouseEnter(object sender, EventArgs e)
        {
            tmrIncSize.Enabled = true;
            tmrDecSize.Enabled = false;
        }

        private void lblIncrease_MouseLeave(object sender, EventArgs e)
        {
            tmrIncSize.Enabled = false;
        }

        private void lblDecreseSize_MouseEnter(object sender, EventArgs e)
        {
            tmrIncSize.Enabled = false;
            tmrDecSize.Enabled = true;
        }

        private void lblDecreseSize_MouseLeave(object sender, EventArgs e)
        {
            tmrDecSize.Enabled = false;
        }

        private void dgvSQLResults_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.txtSQLMessages.Text += Environment.NewLine + "## " + e.Exception.ToString() + Environment.NewLine;
        }

        private void tabSqlResults_DoubleClick(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => ShowSQLResultsInNewForm(this));
            thread.IsBackground = false;
            thread.Start();
        }

        private void ShowSQLResultsInNewForm(SqlResults sr)
        {
            frmSQLResults fsr = new frmSQLResults(sr);
            try
            {
                fsr.ShowDialog();
            }
            catch (Exception ex)
            {
                fsr.ShowDialog();
            }
        }

        private void txtInputQuery_DoubleClick(object sender, EventArgs e)
        {
            //aa
        }

        private void RefreshQuery()
        {
            try
            {
                using (OleDbConnection con = new OleDbConnection(this.txtConnString.Text.Trim()))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(this.txtInputQuery.Text, con);
                    OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                    DataSet ds = new DataSet();
                    int numRows = oldbDA.Fill(ds);

                    using (StreamWriter w = File.AppendText("sqlCommandHistory.txt"))
                    {
                        w.WriteLine(this.txtInputQuery.Text.Trim());
                    }

                    string sqlMsg = "Total Rows returned (" + numRows.ToString() + ").";

                    if (ds.Tables.Count > 0)
                    {
                        this.Invoke(
                            (MethodInvoker)delegate {
                            this.connString = this.txtConnString.Text.Trim();
                            this.AssignDataSourceToDGV(ds.Tables[0], connString);
                            this.AddTextToMessages(sqlMsg, true, false);
                            this.AddInputQuery(this.txtInputQuery.Text, true, false);
                            this.AddConnString(this.txtConnString.Text.Trim());
                            this.tabSqlResults.SelectedIndex = 0;
                        });
                        //this.AssignDataSourceToDGV(ds.Tables[0], connString);
                    }
                    con.Close();

                }
            }
            catch (Exception ex)
            {
                this.Invoke(
                (MethodInvoker)delegate {
                    string queryCommand = this.txtInputQuery.Text;
                    this.AddTextToMessages(queryCommand, true, false);
                    string sqlMsg = "Error: " + ex.ToString();
                    this.AddTextToMessages(sqlMsg, false, true);
                    this.AddConnString(this.txtConnString.Text.Trim());
                    tabSqlResults.SelectedIndex = 1;
                });
            }
        }

        private void txtConnString_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.cnxtMenu.Show();
            }
        }

        private void MiRefresh_Click(object sender, EventArgs e)
        {
            Thread thRerfesh = new Thread(RefreshQuery);
            thRerfesh.Start();
            //RefreshQuery();
        }

        private void txtInputQuery_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Thread thRerfesh = new Thread(RefreshQuery);
                thRerfesh.Start();
                //RefreshQuery();
            }
            else if (e.KeyCode == Keys.F6)
            {
                FormatQueryString();
            }
        }

        private void dgvSQLResults_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged == DataGridViewElementStates.Selected)
            {
                dgvTranspose.Rows.Clear();

                DataGridViewSelectedRowCollection dgvrs = this.dgvSQLResults.SelectedRows;
                if (dgvrs != null && dgvrs.Count > 0)
                {
                    PopulateTransposeGrid(dgvrs[0].Index);
                }
            }
        }
    }
}
