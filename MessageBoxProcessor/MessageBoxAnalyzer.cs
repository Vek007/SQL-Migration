using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MessageBoxProcessor
{
    public class MessageBoxAnalyzer
    {
        public string MatchString = "MessageBox";

        private string connString = "";
        private string rootFolder = "";
        private List<string> files = new List<string>();

        public MessageBoxAnalyzer()
        {
            this.rootFolder = "";
            this.connString = "";

            files.Clear();
        }


        public MessageBoxAnalyzer(string rootFolder, string connString)
        {
            this.rootFolder = rootFolder;
            this.connString = connString;

            files.Clear();
            GetFiles(rootFolder, "*.scx", true);
            GetFiles(rootFolder, "*.Vcx", true);
            //GetFiles(rootFolder, "*.prg", true);

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

        public void ProcessPrgFiles()
        {
            try
            {
                foreach (string file in files)
                {
                    string[] prgLines = File.ReadAllLines(file);
                    Console.WriteLine(file);
                    ProcessPrgFile(prgLines.ToList(), file);
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        public void GetPrgFiles(String strDir, String strExt, bool bRecursive)
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
                    GetPrgFiles(dirList[i], strExt, true);
                }
            }
        }

        public void ProcessFoxproFiles()
        {
            String strSearch = "MessageBox";
//            string selectCommandPrefix = "Select Refno, methods from ";
            string selectCommandPrefix = "Select methods from ";

            List<string> filesInOrder = files.OrderBy(s=>s).ToList();
            foreach(string file in filesInOrder)
            {
                SQLExecutor sqlData = new SQLExecutor();
                string selectCommand = selectCommandPrefix + file + ";";
                List<SqlResults> sqlResults = sqlData.DataExecutor(selectCommand, connString);
                if (sqlResults.Count() > 0)
                {

                    List<Tuple<int, string>> methodsWithId = sqlResults[0].GetMethodsWithId();

                    List<string> newMethods = new List<string>();
                    StringBuilder sb = new StringBuilder();
                    Console.WriteLine("##>> " + file + "Methods:" + methodsWithId.Count.ToString());

                    for (int i =0; i < methodsWithId.Count; i++)
                    {
                        Tuple<int, string> rec = methodsWithId[i];

                        sb.Clear();
                        bool bAppend = false;
                        //string method = methods[i];
                        int pKey = rec.Item1;
                        string method = rec.Item2.ToString();

                        newMethods.Add(method);

                        if (method == string.Empty)
                            continue;

                        List<string> methodLines = method.Split( new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();
                        methodLines = RemoveSplitLines(methodLines);

                        for(int j=0; j < methodLines.Count;j++)
                        {
                            Match mtch;
                            mtch = Regex.Match(methodLines[j], strSearch, RegexOptions.IgnoreCase);

                            if (mtch.Success == true)
                            {
                                string oldLine = "";
                                string newLine = "";

                                if (methodLines[j].Contains(";"))
                                {
                                    int ijk = 0;
                                    ijk++;
                                }

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

                                    oldLine = strMultiLine;
                                    newLine = ProcessMessageBoxes(strMultiLine);
                                    //newLine = ProcessMessageBoxes(methodLines[j]);
                                }
                                else
                                {
                                    oldLine = methodLines[j];
                                    newLine = ProcessMessageBoxes(methodLines[j]);
                                }

                                newLine = AddPrecedingTabsOrSpaces(newLine, methodLines[j]);

                                dumpToTextFile(oldLine, file);
                                dumpToTextFile(newLine, file);

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
                    }

                }
            }
        }

        private bool CheckWellFormed(string input)
        {
            var stack = new Stack<char>();
            // dictionary of matching starting and ending pairs
            var allowedChars = new Dictionary<char, char>() { { '(', ')' }, { '[', ']' }, { '{', '}' } };

            var wellFormated = true;
            foreach (var chr in input)
            {
                if (allowedChars.ContainsKey(chr))
                {
                    // if starting char then push on stack
                    stack.Push(chr);
                }
                // ContainsValue is linear but with a small number is faster than creating another object
                else if (allowedChars.ContainsValue(chr))
                {
                    // make sure something to pop if not then know it's not well formated
                    wellFormated = stack.Any();
                    if (wellFormated)
                    {
                        // hit ending char grab previous starting char
                        var startingChar = stack.Pop();
                        // check it is in the dictionary
                        wellFormated = allowedChars.Contains(new KeyValuePair<char, char>(startingChar, chr));
                    }
                    // if not wellformated exit loop no need to continue
                    if (!wellFormated)
                    {
                        break;
                    }
                }
            }
            return wellFormated;
        }

        public string GetProcessedMessageBox(string msgString)
        {
            string strSearch = "MessageBox";
            {
                Match mtch;
                mtch = Regex.Match(msgString, strSearch, RegexOptions.IgnoreCase);

                if (mtch.Success == true)
                {
                    int j = 0;
                    string oldLine = "";
                    string newLine = "";

                    if (msgString.Contains(";"))
                    {
                        int ijk = 0;
                        ijk++;
                    }

                    if (msgString[msgString.Length - 1] == ';' || msgString.Contains(';'))
                    {
                        string[] msgStrings = msgString.Split(';');
                        string strMultiLine = msgString;
                        strMultiLine = msgString.Replace(";", string.Empty);
                        strMultiLine += GetFullFoxproLineBin(msgStrings.ToList(), ref j);

                        oldLine = strMultiLine;
                        newLine = ProcessMessageBoxes(strMultiLine);
                        //newLine = ProcessMessageBoxes(methodLines[j]);
                    }
                    else
                    {
                        oldLine = msgString;
                        newLine = ProcessMessageBoxes(msgString);
                    }

                    newLine = AddPrecedingTabsOrSpaces(newLine, msgString);

                    return newLine;
                }
                else
                {
                    return msgString;
                }
            }
        }

        public void ProcessPrgFile(List<string> programLines, string fileName)
        {
            String strSearch = "MessageBox";
            StringBuilder sb = new StringBuilder();
            List<string> newMethods = new List<string>();
            newMethods = programLines.ToList();
            bool bAppend = false;
            for (int j = 0; j < programLines.Count; j++)
            {
                Match mtch;
                mtch = Regex.Match(programLines[j], strSearch, RegexOptions.IgnoreCase);

                if (mtch.Success == true)
                {
                    string oldLine = "";
                    string newLine ="";
                    if (programLines[j][programLines[j].Length - 1] == ';')
                    {
                        int stIndex = j;
                        string strMultiLine = programLines[j];
                        strMultiLine = programLines[j].Replace(";", string.Empty);
                        strMultiLine += GetFullFoxproLineBin(programLines, ref j);
                        if (stIndex < j)
                        {
                            for (int k = stIndex + 1; k < j; k++)
                            {
                                newMethods[k] = "";
                            }
                        }

                        oldLine = strMultiLine;
                        dumpToTextFile((Environment.NewLine + "#############################################" + Environment.NewLine), fileName);
                        dumpToTextFile(oldLine, fileName);
                        dumpToTextFile((Environment.NewLine + "=============================================" + Environment.NewLine), fileName);
                        newLine = ProcessMessageBoxes(strMultiLine);
                        dumpToTextFile(newLine, fileName);
                        dumpToTextFile((Environment.NewLine + "#############################################" + Environment.NewLine), fileName);
                    }
                    else
                    {
                        oldLine = programLines[j];
                        newLine = ProcessMessageBoxes(programLines[j]);
                    }

                    //string newLine = ProcessMessageBoxes(programLines[j]);

                    newLine = AddPrecedingTabsOrSpaces(newLine, programLines[j]);

                    sb.AppendLine(newLine);

                    bAppend = true;
                }
                else
                {
                    sb.AppendLine(programLines[j]);
                }
            }

            WriteToTextFiles(sb.ToString(), fileName);
        }

        public void dumpToTextFile(string fileLines, string fileName)
        {
            string path = path = Path.GetDirectoryName(fileName) + "\\msg_dumps\\"; ;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName1 = Path.GetFileName(fileName).Replace(".", "_");

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName1);

            fileName1 = fileNameWithoutExtension + ".txt";//"_" + methodId.ToString() + ".txt";

            using (StreamWriter sw = new StreamWriter(File.Open((path + fileName1), FileMode.Append), Encoding.UTF8))
            {
                sw.WriteLine(fileLines);
            }

        }

        public void WriteToTextFiles(string fileLines, string fileName)
        {
            string path = path = Path.GetDirectoryName(fileName) + "\\modified_new\\"; ;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName1 = Path.GetFileName(fileName).Replace(".", "_");

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            fileName1 = fileNameWithoutExtension + ".prg";//"_" + methodId.ToString() + ".txt";

            if (File.Exists(path + fileName1))
            {
                File.Delete(path + fileName1);
            }

            using (StreamWriter sw = new StreamWriter(File.Open((path + fileName1), FileMode.Create), Encoding.UTF8))
            {
                sw.Write(fileLines);
            }

        }

        private string AddPrecedingTabsOrSpaces(string newLine, string oldLine)
        {
            int totTabs = 0;
            int totSpaces = 0;
            for (int i = 0; i < oldLine.Length; i++)
            {
                if (oldLine[i] == '\t')
                    totTabs++;
                else if (oldLine[i] == ' ')
                    totSpaces++;
                else
                    break;
            }

            for (int i = 0; i < totTabs; i++)
            {
                newLine = ("\t" + newLine);
            }

            for (int i = 0; i < totSpaces; i++)
            {
                newLine = (" " + newLine);
            }

            return newLine;
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
          /*  if (baseString.Contains("sm_get_text_by_text") && (baseString.Contains("sm_get_text_by_record") || baseString.Contains("msgbox")))
            {
                Console.WriteLine("\t\t" + baseString);
            }

            return baseString;*/

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
                if (msgStr[i] == '+'&& i > 0)
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

            string returnString = Environment.NewLine + "==========================================================================================================" + Environment.NewLine;
            returnString += (msgStr + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;
            returnString += (results + Environment.NewLine);
            returnString += "----------------------------------------------------------" + Environment.NewLine;

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

        private string ProcessVariables(string msgStr, bool removeQuotes)
        {
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
