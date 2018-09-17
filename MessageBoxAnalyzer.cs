using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Migration
{
    public class MessageBoxAnalyzer
    {
        public string MatchString = "MessageBox";

        public bool bAddDbStringTranslations = false;

        private string connString = "";
        private string rootFolder = "";
        private List<string> files = new List<string>();
        private frmSQL parentForm = null;

        public MessageBoxAnalyzer(frmSQL pParent, string rootFolder, string connString)
        {
            this.rootFolder = rootFolder;
            this.connString = connString;
            this.parentForm = pParent;

            files.Clear();
            GetFiles(rootFolder, "*.scx", true);
            GetFiles(rootFolder, "*.Vcx", true);
        }

        void GetFiles(String strDir, String strExt, bool bRecursive)
        {
            string[] fileList = Directory.GetFiles(strDir, strExt);
            for (int i = 0; i < fileList.Length; i++)
            {
                if (File.Exists(fileList[i]))
                {
                    files.Add(fileList[i]);
                }
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

        public void PrepareSCXAndVCXFiles()
        {
            this.parentForm.Invoke((MethodInvoker)delegate {
                this.parentForm.ClearTextBoxes();
            });

            SQLExecutor sqlCmd = new SQLExecutor();
            foreach (string file in files)
            {
                bool result = ReplaceUserWithUniqueID(file, connString);
                this.parentForm.Invoke((MethodInvoker)delegate {
                    this.parentForm.AddTextToSmText(Environment.NewLine);
                    this.parentForm.AddTextToSmText("########################################################################");
                    this.parentForm.AddTextToSmText(file +":"+ result.ToString());
                    this.parentForm.AddTextToSmText("########################################################################");
                });
            }
        }

        private bool ReplaceUserWithUniqueID(string fileName, string connString)
        {
            string sqlCommand = "UPDATE " + fileName + " SET User = Uniqueid";
            SQLExecutor sqlExe = new SQLExecutor();
            return sqlExe.CommandExecutor(sqlCommand, connString);
        }

        private void AddUniqueID(string fileName, string connString)
        {
            string selectCommand = "Select User from " + fileName;
            SQLExecutor sqlData = new SQLExecutor();
            List<SqlResults> sqlResults = sqlData.DataExecutor(selectCommand, connString);
            List<string> uniqueIds = sqlResults[0].GetColumnValueAsString("User");

        }

        public void ProcessFoxproFiles()
        {
            String strSearch = "MessageBox";
            this.parentForm.Invoke((MethodInvoker)delegate {
                this.parentForm.ClearTextBoxes();
            });

            string selectCommandPrefix = "Select Refno, methods from ";

            List<string> filesInOrder = files.OrderByDescending(s=>s).ToList();
            foreach(string file in filesInOrder)
            {
                string file1 = "C:\\source\\scx_vcx\\application.vcx";
                SQLExecutor sqlData = new SQLExecutor();
                string selectCommand = selectCommandPrefix + file + ";";

                List<SqlResults> sqlResults = sqlData.DataExecutor(selectCommand, connString);

                int loop = 2;

                if (sqlResults.Count() > 0)
                {

                    List<Tuple<int, string>> methodsWithId = sqlResults[0].GetMethodsWithId();

                    List<string> newMethods = new List<string>();
                    StringBuilder sb = new StringBuilder();
                    Debug.WriteLine("##>>" + file + "Methods:" + methodsWithId.Count.ToString());

                    for (int i =0; i < methodsWithId.Count; i++)
                    {
                        Tuple<int, string> rec = methodsWithId[i];

                        sb.Clear();
                        bool bAppend = false;
                        //string method = methods[i];
                        int pKey = rec.Item1;
                        string method = rec.Item2.ToString();
                        Debug.WriteLine("##>>\t" + pKey.ToString());

                        newMethods.Add(method);

                        if (method == string.Empty)
                            continue;

                        List<string> methodLines = method.Split( new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                        methodLines = RemoveSplitLines(methodLines);

                        for(int j=0; j < methodLines.Count;j++)
                        {
                            Debug.WriteLine("##>>\t\t" + j.ToString());

                            Match mtch;
                            mtch = Regex.Match(methodLines[j], strSearch, RegexOptions.IgnoreCase);

                            if (mtch.Success == true)
                            {

                                if (methodLines[j][methodLines[j].Length - 1] == ';')
                                {
                                    int stIndex = j;
                                    string strMultiLine = methodLines[j];
                                    strMultiLine = methodLines[j].Replace(";", string.Empty);
                                    strMultiLine += GetFullFoxproLineBin(methodLines, ref j);
                                    if (stIndex < j)
                                    {
                                        for (int k = stIndex + 1; k < j; k++)
                                        {
                                            newMethods[k] = "";
                                        }
                                    }
                                }


                                string newLine = "";// ProcessMessageBoxes(methodLines[j]);

                                if (!methodLines[j].ToUpper().Contains("sm_get_text_by_text"))
                                    newLine = ProcessMessageBoxes(methodLines[j]);
                                else
                                {
                                    int iiii = 0;
                                }

                                sb.AppendLine(newLine);
                                bAppend = true;
                            }
                            else
                            {
                                sb.AppendLine(methodLines[j]);
                            }
                        }

                        newMethods[i] = sb.ToString();

                        if (bAppend)
                        {
                            byte[] bytes = Encoding.Default.GetBytes(newMethods[i]);
                            string newMethodInUTF8 = Encoding.UTF8.GetString(bytes);
                            bytes = Encoding.Default.GetBytes(method);
                            string oldMethodInUTF8 = Encoding.UTF8.GetString(bytes);
                            int id = pKey;//this should be the primary key
                            string fileNameToWrite = file;
                            WriteMethodToFile(file, id, newMethodInUTF8, bAppend, oldMethodInUTF8);
                        }
                        else
                        {
                            byte[] bytes = Encoding.Default.GetBytes(newMethods[i]);
                            string newMethodInUTF8 = Encoding.UTF8.GetString(bytes);
                            int id = pKey;//this should be the primary key
                            string fileNameToWrite = file;
                            WriteMethodToFile(file, id, newMethodInUTF8);
                        }

                        Application.DoEvents();
                    }

                }
            }
        }

        private void WriteMethodToFile(string fileName, int methodId, string newMethod, bool bStoreOriginalMethod=false, string oldMethod="")
        {
            string path = "";
            if (bStoreOriginalMethod)
                path = Path.GetDirectoryName(fileName) + "\\modified_new\\";
            else
                path = Path.GetDirectoryName(fileName) + "\\modified_new1\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName1 = Path.GetFileName(fileName).Replace(".","_");

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName1);

            fileName1 = fileNameWithoutExtension + "_" + methodId.ToString() + ".txt";

            if (File.Exists(path + fileName1))
            {
                File.Delete(path + fileName1);
            }

            using (StreamWriter sw = new StreamWriter(File.Open((path + fileName1), FileMode.Create), Encoding.UTF8))
            {
                sw.Write(newMethod);
            }

            if (bStoreOriginalMethod)
            {
                path = Path.GetDirectoryName(fileName) + "\\modified_old\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                fileName1 = Path.GetFileName(fileName).Replace(".", "_");

                fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName1);

                fileName1 = fileNameWithoutExtension + "_" + methodId.ToString() + ".txt";

                if (File.Exists(path + fileName1))
                {
                    File.Delete(path + fileName1);
                }

                using (StreamWriter sw = new StreamWriter(File.Open((path + fileName1), FileMode.Create), Encoding.UTF8))
                {
                    sw.Write(oldMethod);
                }
            }
        }

        public string GetFullFoxproLineBin(List<string> methodLines, ref int iLineMulti)
        {
            string fullLine = "";

            {
                string strLine = "";
                //while ((strLine = sr.ReadLine()) != null)
                while (iLineMulti < methodLines.Count())
                {
                    iLineMulti++;
                    strLine = methodLines[iLineMulti];//System.Text.Encoding.UTF8.GetString(methodLines[iLineMulti].ToArray());
                    //string strLineAnsi = System.Text.Encoding.ASCII.GetString(binLines[iLineMulti].ToArray());
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

        private List<string> RemoveSplitLines(List<string> methodLines)
        {
            List<string> noSplitLines = new List<string>();
            string line = "";
            for(int lineIdx=0; lineIdx < methodLines.Count; lineIdx++)
            {
                line = methodLines[lineIdx];
                line = methodLines[lineIdx];
                if (!line.ToUpper().Contains("MESSAGEBOX"))
                {
                    noSplitLines.Add(line);
                    continue;
                }
                if (line.Contains(";"))
                {
                    lineIdx++;
                    while (methodLines[lineIdx].Contains(";") && lineIdx < methodLines.Count)
                    {
                        line += methodLines[lineIdx];
                        lineIdx++;
                    }
                    line += methodLines[lineIdx];
                }
                line = line.Replace(";", String.Empty);
                noSplitLines.Add(line);
            }
            return noSplitLines;
        }

        public string ProcessMessageBoxes(string baseString)
        {
            string strMultiLine = "";
            string strConcateLine = "";
            string strSingleLine = baseString.Trim();

            if (!strSingleLine.Contains("\""))
            {
                return baseString;
            }

            baseString = strSingleLine;
            if (baseString.Contains('+') && IsNotPartOfCharacterCode(baseString))
            {
                strConcateLine = baseString;
                baseString = "";
            }
            else
            {
                strConcateLine = "";
            }
            strMultiLine = "";


            string modifiedLine = "";
            baseString = AddSmGetTextByText(baseString, ref modifiedLine, true);
            strMultiLine = AddSmGetText(strMultiLine, ref modifiedLine);
            strConcateLine = AddSmGetText(strConcateLine, ref modifiedLine);

            modifiedLine = CaseInsenstiveReplace(modifiedLine, "MESSAGEBOX", "msgbox");
            return modifiedLine;
        }

        private bool IsNotPartOfCharacterCode(string msgStr)
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
            int stIndex = msgStr.IndexOf("\"", msgboxIndex > 0 ? msgboxIndex : 0);
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
               /* bool fullMatch;
                List<string> msgStrs = new List<string>();
                originalText = originalText.Replace("\"", string.Empty);
                string[] messageString = SplitMessageString(originalText, bSingleLine);
                if (messageString.Length <= 0)
                    return msgStr;

                msgStr = "";
                foreach (string msg in messageString)
                {
                    msgStr += msg;
                }*/
                modifiedLine = msgStr;

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
                string msgWord = msgStr.Substring(0, firstBracketIndex + 1);
                msgWord = msgWord.ToUpper().Replace("MESSAGEBOX", "msgbox");
                quotedString.Add(msgWord);

                msgWord = "\"" + msgStr.Substring(firstBracketIndex + 1, closeBracketIndex - firstBracketIndex - 1) + "\"";
                quotedString.Add(msgWord);

                msgWord = msgStr.Substring(closeBracketIndex, msgStr.Length - closeBracketIndex);
                quotedString.Add(msgWord);

            }

            return quotedString.ToArray();
        }

        public string AddSmGetText(string msgStr, ref string modifiedLine)
        {

            string results = "";

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

            if (!bMultiConcate)
                results = messageString[0] + results + messageString[2];

            modifiedLine = results;

            /*string returnString = Environment.NewLine + "==========================================================================================================" + Environment.NewLine;
            returnString += (msgStr + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;
            returnString += (results + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;*/

            string returnString = Environment.NewLine + "==========================================================================================================" + Environment.NewLine;
            returnString += (msgStr + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;
            returnString += (results + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;
            bool fullMatch;

            if (bAddDbStringTranslations)
            {
                string dbString = GetTheMostMatchedString(msgStrsFroDB, out fullMatch);

                this.parentForm.Invoke((MethodInvoker)delegate {
                    this.parentForm.AddTextToSmText(returnString +
                        "----------------------------------------------------------" +
                        dbString + Environment.NewLine +
                        "----------------------------------------------------------" +
                                        Environment.NewLine + Environment.NewLine);
                });



                //if (fullMatch)
                //    returnString += ("@@" + GetTheMostMatchedString(msgStrsFroDB, out fullMatch) + Environment.NewLine);
                //else
                //    returnString += (GetTheMostMatchedString(msgStrsFroDB, out fullMatch) + Environment.NewLine);

                returnString += "==========================================================================================================" + Environment.NewLine;
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
                    if (ds.Tables[0].Rows.Count > 20)
                    {
                        int waitDebug = 0;
                        numOfRows = 0;
                        return "";
                    }

                    if (numRows > 1)
                    {
                        for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                        {
                            foreach (object item in ds.Tables[0].Rows[j].ItemArray)
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
        private string GetDBString(string[] msgStrsFroDB)
        {
            string returnString = "";

            if (msgStrsFroDB.Length <= 0)
                return returnString;

            string[] orderedArray = msgStrsFroDB.OrderByDescending(aux => aux.Length).ToArray();

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

                if (orderedArray[i].ToUpper().Contains("(") && !orderedArray[i].ToUpper().Contains(" "))
                    continue;


                //if (sqlCommand.ToUpper().Contains("IIF("))
                //{
                //    rePlaceInCode++;
                //    return rePlaceInCode.ToString() + "Replace in Code";
                //}


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
                            if (orderedArray[j].ToUpper().Contains("(") && !orderedArray[j].ToUpper().Contains(" "))
                                continue;

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
                    return (Environment.NewLine + dbStrings[0] + Environment.NewLine +
                "----------------------------------------------------------------------------------------------" + Environment.NewLine +
                "French" + Environment.NewLine +
                "----------------------------------------------------------------------------------------------" + Environment.NewLine +
                GetFrenchString(Convert.ToInt32(dbStrings[0])) + Environment.NewLine + GetEnglishString(Convert.ToInt32(dbStrings[0])));
                }
            }

            return dbString;
        }

        private string GetFrenchString(int id)
        {
            string dbfFile = "E:\\client_data\\Nossack\\sm_text";
//            string sqlCommand = "Select french from sm_text where pk_sm_text = " + id.ToString();
            string sqlCommand = "Select french from "+ dbfFile + " where pk_sm_text = " + id.ToString();
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
                        for (int i = 0; i < ds.Tables[0].Rows[0].ItemArray.Count(); i++)//each (object item in ds.Tables[0].Rows[0].ItemArray)
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

        private void ReplaceFunnyChars(int id, bool bEnglish)
        {
            string sqlCommand = "";
            if (bEnglish)
                sqlCommand = "Select english from sm_text where pk_sm_text = " + id.ToString();
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

            if (bEnglish)
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

        private bool HasIfWithAndOr(string msgStr)
        {
            if (msgStr.Contains(" if") && (msgStr.Contains(" or ") || msgStr.Contains(" and ")))
                return true;

            if ((msgStr.Contains(" or ") || msgStr.Contains(" and ")))
                return true;

            return false; ;
        }

        private string ProcessVariables(string msgStr, bool removeQuotes)
        {
            if (string.IsNullOrEmpty(msgStr))
            {
                return msgStr;
            }

            bool bFullLoop = false;
            if (msgStr.Trim()[msgStr.Trim().Length - 1] == ')')
            {
                bFullLoop = true;
            }

            string[] msgStrs = msgStr.Split('+');

            if (!bFullLoop)
            {
                if (msgStrs[msgStrs.Length - 1].Trim()[0] != '\"' && !Char.IsNumber(msgStrs[msgStrs.Length - 1].Trim()[0]))
                {
                    bFullLoop = true;
                }
            }

            string results = "";

            Queue<string> parameteres = new Queue<string>();

            for (int i = 0, j = 1; i < (bFullLoop ? msgStrs.Length : msgStrs.Length - 1); i++)
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
                if (param.ToUpper().IndexOf("CHR(") == 0)
                    results += (", \"" + param.Trim() + "\"");
                else
                    results += (", " + param.Trim());
            }

            return "sm_get_text_by_record(" + results + ")";
        }

        private bool IsVariable(string msgStr)
        {
            msgStr = msgStr.Trim();
            if (msgStr.Contains("\""))
            {
                return false;
            }

            if (IsNumeric(msgStr.Trim()))
            {
                return false;
            }

            if (msgStr.Contains("="))
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

                if (totalPara == 0 && ((msgStr[0] >= 65 && msgStr[0] <= 90) || (msgStr[0] >= 97 && msgStr[0] <= 122)))
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

        public string CaseInsenstiveReplace(string originalString, string oldValue, string newValue)
        {
            Regex regEx = new Regex(oldValue,
               RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return regEx.Replace(originalString, newValue);
        }

    }
}
