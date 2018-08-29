using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Migration
{
    public class SourceAnyWhereHelper
    {
        frmSQL parentForm;

        public SourceAnyWhereHelper(frmSQL pForm)
        {
            this.parentForm = pForm;
        }

        private string saRoot = "$/dtec2016.root/";
        private string dtecFolderName = "dtec2016";
        private string sadRoot = "$/dist_ii/";
        private string sadFolderName = "dist_ii_new";


        public void PopulateFiles(string rootPath)
        {
            string[] fileNames = Directory.GetFiles((rootPath + dtecFolderName), "*.*", SearchOption.AllDirectories);

            try
            {
                SqlConnection conn = new SqlConnection("Data source=VIVEKPC\\SQL; Database=SourceAnywhere;User Id=sa;Password=LionGirnar007");
                conn.Open();
                foreach (string filename in fileNames)
                {
                    if (filename.IndexOf(".vs") >= 0)
                        continue;

                    FileInfo saFile = new FileInfo(filename);
                    Debug.WriteLine(saFile.DirectoryName);
                    Debug.WriteLine(saFile.Name);
                    string saName = saFile.DirectoryName.Replace(rootPath, saRoot).Replace("\\","/") ;
                    SqlCommand cmd;

                    if (IsDirectory(filename))
                    {
                        cmd = new SqlCommand("insert into files (file_name, working_folder, is_in_source_control) values ('" + saFile.Name + "', '" + saName + "', 1);", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into files (file_name, working_folder, is_in_source_control) values ('" + saFile.Name + "', '" + saName + "', 1);", conn);
                    }

                    cmd.ExecuteNonQuery();

                    //StringBuilder output = GetFileHistory(saName, saFile.Name);

                    //List<FileHistory> fileHistory = GetFormattedFileHistory(output, saFile.Name);

                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception :" + e.Message + "\t" + e.GetType());
            }
        }

        public void PopulateFilesFP(string rootPath)
        {
            string[] fileNames = Directory.GetFiles((rootPath + sadFolderName), "*.*", SearchOption.AllDirectories);

            try
            {
                SqlConnection conn = new SqlConnection("Data source=VIVEKPC\\SQL; Database=SourceAnywhere;User Id=sa;Password=LionGirnar007");
                conn.Open();
                foreach (string filename in fileNames)
                {
                    if (filename.IndexOf(".vs") >= 0)
                        continue;

                    FileInfo saFile = new FileInfo(filename);
                    Debug.WriteLine(saFile.DirectoryName);
                    Debug.WriteLine(saFile.Name);
                    string saName = sadRoot.Substring(0, sadRoot.Length - 1); // saFile.DirectoryName.Replace(rootPath, sadRoot).Replace("\\", "/");
                    SqlCommand cmd;

                    if (IsDirectory(filename))
                    {
                        cmd = new SqlCommand("insert into files (file_name, working_folder, is_in_source_control, is_fp) values ('" + saFile.Name + "', '" + saName + "', 1, 1);", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into files (file_name, working_folder, is_in_source_control, is_fp) values ('" + saFile.Name + "', '" + saName + "', 1, 1);", conn);
                    }

                    cmd.ExecuteNonQuery();

                    //StringBuilder output = GetFileHistory(saName, saFile.Name);

                    //List<FileHistory> fileHistory = GetFormattedFileHistory(output, saFile.Name);

                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception :" + e.Message + "\t" + e.GetType());
            }
        }


        public void PopulateFileHistoryInDb()
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data source=VIVEKPC\\SQL; Database=SourceAnywhere;User Id=sa;Password=LionGirnar007");
                conn.Open();
                string sql = "select * from files";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                List<Guid> unSourcedFiles = new List<Guid>();
                int i = 0;
                while (dr.Read())
                {
                    i++;
                    System.Guid file_id;
                    if (Guid.TryParse(dr["file_id"].ToString(), out Guid res))
                        file_id = res;
                    else
                        continue;

                    string file_name = dr["file_name"].ToString();

                    this.parentForm.UpdateSAOutput(file_name + "(" + i.ToString() + ")");

                    string wFolder = dr["working_folder"].ToString();

                    var isFP = (bool)dr["is_fp"];

                    StringBuilder output = GetFileHistory(wFolder, file_name);

                    //List<FileHistory> fileHistory = GetFormattedFileHistory(output, file_name);
                    List<FileHistory> fileHistory = GetFormattedFileHistoryWithComments(output, file_name);

                    if (fileHistory.Count <= 0)
                    {
                        unSourcedFiles.Add(file_id);

                        continue;
                    }

                    SqlConnection conn1 = new SqlConnection("Data source=VIVEKPC\\SQL; Database=SourceAnywhere;User Id=sa;Password=LionGirnar007");
                    conn1.Open();

                    foreach (FileHistory hRec in fileHistory)
                    {
                        SqlCommand cmd1;
                        cmd1 = new SqlCommand("insert into file_history (FileId, user_name, rev_date, version_no, action, comment) values (@fid, @uname, @rDate, @vno, @action,@comment);", conn1);

                        cmd1.Parameters.AddWithValue("@fid", file_id);
                        cmd1.Parameters.AddWithValue("@uname", hRec.user_name);
                        cmd1.Parameters.AddWithValue("@rDate", hRec.revDate);
                        cmd1.Parameters.AddWithValue("@vno", hRec.version);
                        cmd1.Parameters.AddWithValue("@action", hRec.action);
                        cmd1.Parameters.AddWithValue("@comment", hRec.comment != null ? hRec.comment.Trim() : string.Empty);
                        cmd1.ExecuteNonQuery();
                    }
                    conn1.Close();
                }

                dr.Close();

                foreach (Guid hRec in unSourcedFiles)
                {
                    SqlCommand cmd1;
                    cmd1 = new SqlCommand("update files set is_in_source_control = 0 where file_id = @fid;", conn);
                    cmd1.Parameters.AddWithValue("@fid", hRec);
                    cmd1.ExecuteNonQuery();
                }

                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception :" + e.Message + "\t" + e.GetType());
            }
        }

        private static bool IsDirectory(string path)
        {
            System.IO.FileAttributes fa = System.IO.File.GetAttributes(path);
            return (fa & FileAttributes.Directory) != 0;
        }

        public StringBuilder GetFileHistory(string project, string filename)
        {
            string retValue = "";

            var process = new Process();

            string exePath = @"C:\Program Files (x86)\Dynamsoft\SourceAnywhere Client\";
            string exeCmd = "SAWSCmd.exe";

            process.StartInfo.FileName = exePath + exeCmd;
            string arguments = "GetFileHistory -file " + filename + " -prj " + project + " -server dtec2 -port 7777 -username Vivek -pwd PumaAmerica008 -repository Default";
            if (!string.IsNullOrEmpty(arguments))
            {
                process.StartInfo.Arguments = arguments;
            }

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;

            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            var stdOutput = new StringBuilder();
            process.OutputDataReceived += (sender, args) => stdOutput.AppendLine(args.Data); // Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

            string stdError = null;
            try
            {
                process.Start();
                process.BeginOutputReadLine();
                stdError = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception("OS error while executing : " + e.Message, e);
            }

            if (process.ExitCode >= 0 )
            {
                return stdOutput;
            }

            else
            {
                var message = new StringBuilder();

                if (!string.IsNullOrEmpty(stdError))
                {
                    message.AppendLine(stdError);
                }

                if (stdOutput.Length != 0)
                {
                    message.AppendLine("Std output:");
                    message.AppendLine(stdOutput.ToString());
                }

                throw new Exception(filename + " finished with exit code = " + process.ExitCode + ": " + message);
            }
        }

        public List<FileHistory> GetFormattedFileHistory(StringBuilder sb, string filename)
        {
            string[] stringArray = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<FileHistory> history = new List<FileHistory>();
            foreach(string str in stringArray)
            {
                string[] hStr = str.Trim().Split('\t');
                if (hStr.Length > 0)
                {
                    if (int.TryParse(hStr[0], out int val))
                    {
                        FileHistory fh = new FileHistory();

                        fh.version = val;
                        fh.file_name = filename;
                        fh.user_name = hStr[1];
                        fh.revDate = Convert.ToDateTime(hStr[2]);
                        fh.action = hStr[3];

                        history.Add(fh);
                    }
                }
            }
            return history;
        }

        public List<FileHistory> GetFormattedFileHistoryWithComments(StringBuilder sb, string filename)
        {
            try
            {
                string[] stringArray = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                List<FileHistory> history = new List<FileHistory>();
                //foreach (string str in stringArray)
                for(int idx = 0; idx < stringArray.Length; idx++)
                {
                    FileHistory fh = null;// new FileHistory();
                    string[] hStr = stringArray[idx].Trim().Split('\t');
                    if (hStr.Length > 0)
                    {
                        if (int.TryParse(hStr[0], out int val))
                        {
                            fh = new FileHistory();

                            fh.version = val;
                            fh.file_name = filename;
                            fh.user_name = hStr[1];
                            fh.revDate = Convert.ToDateTime(hStr[2]);
                            fh.action = hStr[3];

                            if (idx + 1 < stringArray.Length)
                            {
                                if (stringArray[idx + 1].Contains("Comment:"))
                                {
                                    string comments = stringArray[idx + 1].Replace("Comment:", "").Trim();
                                    fh.comment = comments;
                                }
                            }

                             history.Add(fh);
                        }
                    }
                }
                return history;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public void ReCreateFileInfo(string rootPath)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Data source=VIVEKPC\\SQL; Database=SourceAnywhere;User Id=sa;Password=LionGirnar007");
                conn.Open();
                string sql = "delete from file_history";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                sql = "delete from files";
                cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

                PopulateFiles(rootPath);
                PopulateFilesFP(rootPath);
                PopulateFileHistoryInDb();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
