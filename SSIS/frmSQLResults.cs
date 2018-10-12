using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSIS
{
    public partial class frmSQLResults : Form
    {
        public frmSQLResults(SqlResults sr)
        {
            InitializeComponent();
            this.sqlResult.AssignDataSourceToDGV(sr.GetQueryDataSource(), sr.GetConnectionString()) ;
            this.sqlResult.AddInputQuery(sr.GetQueryString(), true, false);
            this.sqlResult.AddTextToMessages(sr.GetMessage(), true, false);
            sqlResult.ShowSizeButtons(false);

            this.Text = this.Text + " ( " + GetFromText(sr.GetQueryString()) + " )";
        }

        public void SetFormTip(string tipMessage)
        {
            this.fromTip.SetToolTip(this, tipMessage);
            this.fromTip.SetToolTip(this.sqlResult, tipMessage);
        }

        private string GetFromText(string strQuery)
        {
            int idx = strQuery.IndexOf(" from ");
            if (idx > 0)
            {
                string fromTable = strQuery.Substring(idx, strQuery.Length - idx);

                string[] lex = fromTable.Split(' ');

                bool nextIsTableName = false;
                foreach(string l in lex)
                {
                    if (l.Trim().ToUpper() == "FROM")
                    {
                        nextIsTableName = true;
                        continue;
                    }

                    if (nextIsTableName && l.Trim().Length > 0)
                    {
                        return l.Trim();
                    }
                }
            }

            return "";
        }
    }
}
