using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSIS
{
    public partial class frmSQLCommandLogs : Form
    {
        public frmSQLCommandLogs()
        {
            InitializeComponent();

            try
            {
                txtSqlCommands.Text = "";
                string[] lines;
                lines = System.IO.File.ReadAllLines(SQLExecutor.sqlCommandLogFile);
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    txtSqlCommands.AppendText(line + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                txtSqlCommands.Text = "Unable to locate sql command history.";
            }

        }

        public void SetFormTip(string tipMessage)
        {
        }
    }
}
