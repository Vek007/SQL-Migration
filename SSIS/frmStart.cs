using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSIS.Entity;

namespace SSIS
{
    public partial class frmStart : Form
    {
        public static tsEntities alDb = new tsEntities();
        public frmStart()
        {
            InitializeComponent();
        }

        private void btnSSIS_Click(object sender, EventArgs e)
        {
            alDb.PopulateArFromFile("E:\\vivek\\st\\ar.txt");
        }

        private void cmdSymbols_Click(object sender, EventArgs e)
        {
            //alDb.DumpTsxSym("E:\\vivek\\st\\tsx.txt");
            //alDb.DumpTsxvSym("E:\\vivek\\st\\tsxv.txt");
            alDb.DumpSym(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\allSym.csv");
        }

        private void btnPer_Click(object sender, EventArgs e)
        {
            alDb.PopulatePerFromFile("E:\\vivek\\st\\per.txt");
        }

        private void btnImpSymbol_Click(object sender, EventArgs e)
        {
            //alDb.ImportNYSESymbols(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\NYSE.csv","NYSE", 300000,'|');
            //alDb.ImportNYSESymbols(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\nas.csv", "NAS", 400000, '|');
            alDb.ImportNYSESymbols(@"E:\vivek\SQL\SQL\SQL Migration\SSIS\ar-per\amex.csv", "AMEX", 500000, '|');
        }
    }
}
