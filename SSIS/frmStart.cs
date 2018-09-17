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
            /*
            string[] csvLines = File.ReadAllLines("e:\\vivek\\st\\t_v.csv");

            for(int i=1;i<csvLines.Length;i++)
            {
                t_x t = new t_x();
                if (!t.PopulateFromCSVLine(csvLines[i], '|'))
                {
                    Debug.WriteLine(i);
                    Debug.WriteLine(csvLines[i]);
                }
                else
                {
                    try
                    {
                        alDb.t_x.Add(t);
                        alDb.SaveChanges();
                        Debug.WriteLine(">> " + csvLines[i]);
                    }
                    catch (DbEntityValidationException dbEx)
                    {
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            }
                        }
                    }
                }
            }*/

            alDb.PopulateArFromFile("E:\\vivek\\st\\ar\\t.txt");
            alDb.PopulateArFromFile("E:\\vivek\\st\\ar\\tx.txt");
            alDb.PopulateArFromFile("E:\\vivek\\st\\ar\\tx1.txt");
            alDb.PopulateArFromFile("E:\\vivek\\st\\ar\\tx2.txt");
        }

        private void cmdSymbols_Click(object sender, EventArgs e)
        {
            alDb.DumpTsxSym("E:\\vivek\\st\\tsx.txt");
            alDb.DumpTsxvSym("E:\\vivek\\st\\tsxv.txt");
        }
    }
}
