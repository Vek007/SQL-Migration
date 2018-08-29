using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MessageBoxProcessor
{
    public partial class SqlResults
    {
        private DataTable sqlResults;
        public SqlResults()
        {
        }

        public void AssignDataSourceToDGV(DataTable dt)
        {
            sqlResults = dt;
        }

        private bool busy = false;

        public List<Tuple<int,string>> GetMethodsWithId()
        {
            List<Tuple<int, string>> colValues = new List<Tuple<int, string>>();
            string colRef = "refno";
            string colMethod = "methods";
            int rNo = 1;
            try
            {
                if (sqlResults != null)
                {
                    foreach (DataRow item in sqlResults.Rows)
                    {
                        int refno = rNo++;//Convert.ToInt32(item[colRef]);
                        string method = item[colMethod].ToString();

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
    }
}
