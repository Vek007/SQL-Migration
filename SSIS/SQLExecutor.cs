using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace SSIS
{
    public class SQLExecutor
    {
        public static string sqlCommandLogFile = "sqlCommandHistory.txt";

        public List<SqlResults> DataExecutor(string selectCommand, string connString, bool treatSemicolonAsNewLine = false)
        {
            List<SqlResults> sqlResults = new List<SqlResults>();

            string[] sqlStrs = new string[] {string.Empty};

            if (!treatSemicolonAsNewLine)
            {
                sqlStrs = selectCommand.Split(';');
            }
            else
            {
                sqlStrs[0] = selectCommand.Replace(';', ' ');
            }

            for (int i = 0; i < sqlStrs.Length; i++)
            {
                using (StreamWriter w = File.AppendText(SQLExecutor.sqlCommandLogFile))
                {
                    w.WriteLine(sqlStrs[i]);
                }

                sqlStrs[i] = sqlStrs[i].Replace(Environment.NewLine, "");

                if (String.IsNullOrEmpty(sqlStrs[i]))
                {
                    continue;
                }

                SqlResults ucSQLResults = new SqlResults();
                try
                {
                    using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                    {
                        con.Open();
                        OleDbCommand oldbCmd = new OleDbCommand(sqlStrs[i], con);
                        OleDbDataAdapter oldbDA = new OleDbDataAdapter(oldbCmd);
                        DataSet ds = new DataSet();
                        int numRows = oldbDA.Fill(ds);

                        string sqlMsg = "Total Rows returned (" + numRows.ToString() + ").";

                        if (ds.Tables.Count > 0)
                        {
                            ucSQLResults.AssignDataSourceToDGV(ds.Tables[0], connString);
                        }

                        ucSQLResults.AddTextToMessages(sqlMsg, true, false);
                        ucSQLResults.AddInputQuery(sqlStrs[i], true, false);
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    string queryCommand = sqlStrs[i];
                    ucSQLResults.AddTextToMessages(queryCommand, true, false);
                    string sqlMsg = "Error: " + ex.ToString();
                    ucSQLResults.AddTextToMessages(sqlMsg, false, true);
                    ucSQLResults.AddInputQuery(queryCommand, false, true);
                    ucSQLResults.AddConnString(connString);
                }
                ucSQLResults.AdjustHeightBasedOnTotalRows();
                sqlResults.Add(ucSQLResults);
            }

            return sqlResults;
        }

        public bool CommandExecutor(string sqlCommand, string connString)
        {
            bool returnValue = true;
            SqlResults ucSQLResults = new SqlResults();
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
                return false;
            }
            return returnValue;
        }

        public int SPExecutor(string sqlCommand, string connString)
        {
            int returnValue = 0;
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (OleDbConnection con = new OleDbConnection(connString))  //here is the error
                {
                    con.Open();
                    OleDbCommand oldbCmd = new OleDbCommand(sqlCommand, con);
                    oldbCmd.CommandType = CommandType.StoredProcedure;
                    OleDbParameter rParam = new OleDbParameter();
                    rParam.Direction = ParameterDirection.ReturnValue;
                    rParam.DbType = DbType.Int64;
                    oldbCmd.Parameters.Add(rParam);
                    Object res = oldbCmd.ExecuteScalar();
                    con.Close();
                    returnValue = Convert.ToInt32(rParam.Value);
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return returnValue;
        }

        public int SPExecutorSQLClient(string sqlCommand, string connString)
        {
            int returnValue = 0;
            SqlResults ucSQLResults = new SqlResults();
            try
            {
                using (SqlConnection con = new SqlConnection(connString))  //here is the error
                {
                    con.Open();
                    SqlCommand oldbCmd = new SqlCommand(sqlCommand, con);
                    oldbCmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter rParam = new SqlParameter();
                    rParam.Direction = ParameterDirection.ReturnValue;
                    rParam.DbType = DbType.Int64;
                    oldbCmd.Parameters.Add(rParam);
                    Object res = oldbCmd.ExecuteScalar();
                    con.Close();
                    returnValue = Convert.ToInt32(rParam.Value);
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return returnValue;
        }


        public bool AddAutoIncrementedValues(string sqlCommand, string connString)
        {
            bool returnValue = true;
            SqlResults ucSQLResults = new SqlResults();
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
                return false;
            }
            return returnValue;
        }


    }
}
