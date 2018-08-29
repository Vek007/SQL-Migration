using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MessageBoxProcessor
{
    public class SQLExecutor
    {
        public List<SqlResults> DataExecutor(string selectCommand, string connString)
        {
            List<SqlResults> sqlResults = new List<SqlResults>();
            string[] sqlStrs = selectCommand.Split(';');

            for (int i = 0; i < sqlStrs.Length; i++)
            {
                sqlStrs[i] = sqlStrs[i].Replace(Environment.NewLine, "");
                if (String.IsNullOrEmpty(sqlStrs[i]))
                    continue;

                SqlResults results = new SqlResults();
                try
                {
                    using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                    {
                        con.Open();
                        OleDbCommand oldbCmd = new OleDbCommand(sqlStrs[i], con);
                        OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                        DataSet ds = new DataSet();
                        int numRows = oldbDA.Fill(ds);

                        if (ds.Tables.Count > 0)
                        {
                            results.AssignDataSourceToDGV(ds.Tables[0]);
                        }

                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(">> Error:" + sqlStrs[i]);
                    string sqlMsg = "Error: " + ex.ToString();
                    Console.WriteLine(">> Error:" + sqlMsg); 
                }
                sqlResults.Add(results);
            }
            return sqlResults;
        }
        public bool CommandExecutor(string sqlCommand, string connString)
        {
            bool returnValue = true;
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString)) 
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    oldbCmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnValue;
        }
    }
}
