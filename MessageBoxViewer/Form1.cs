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
using MessageBoxProcessor;

namespace MessageBoxViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select a file";
            //            fdlg.InitialDirectory = Directory.GetCurrentDirectory();
            fdlg.InitialDirectory = "C:\\Source\\dist_ii";
            fdlg.Filter = "VFP Files (*.prg)|*.prg";
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                String strFile = fdlg.FileName;
                txtFileName.Text = Path.GetDirectoryName(strFile);

                string[] prgLines = File.ReadAllLines(strFile);
                MessageBoxAnalyzer mn = new MessageBoxAnalyzer();
                mn.ProcessPrgFile(prgLines.ToList(), strFile);

                string fileName1 = Path.GetFileName(strFile);

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName1);

                fileName1 = fileNameWithoutExtension + "_prg.txt";//"_" + methodId.ToString() + ".txt";

                string dumpfileName = Path.GetDirectoryName(strFile) + "\\msg_dumps\\" + fileName1;

                prgLines = File.ReadAllLines(dumpfileName);
                txtModifiedFile.Text = "";
                foreach (string line in prgLines)
                {
                    txtModifiedFile.Text += (line + Environment.NewLine);
                }

            }

        }
    }
}
