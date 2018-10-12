using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSIS
{
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }

        public List<KeyValuePair<string, double>> GetKvpData(DataTable dt)
        {
            List<KeyValuePair<string, double>> data = new List<KeyValuePair<string, double>>();
            foreach (DataColumn dc in dt.Columns)
            {
                double val = 0;
                if (!double.TryParse(dt.Rows[0][dc.ColumnName].ToString(), out val))
                {
                    val = 0;
                }
                KeyValuePair<string, double> kvp = new KeyValuePair<string, double>(dc.ColumnName, val);
                data.Add(kvp);
            }

            return data;
        }

        public void SetPer(DataTable dt)
        {
            List <KeyValuePair<string, double>> data = GetKvpData(dt);

            this.dbePer.SetData(data,"Per");
        }

        public void SetAr(DataTable dt)
        {
            List<KeyValuePair<string, double>> data = GetKvpData(dt);

            this.dbePer.SetData(data, "Per");
        }

        public void SetPr(DataTable dt)
        {
            List<KeyValuePair<string, double>> data = GetKvpData(dt);

            this.dbePrice.SetData(data, "Pr");
        }

        public void SetTitle(string text)
        {
            this.Text = text;
        }

        public void SetSymbol(string sym, string ex)
        {
            SSIS.tsEntities alDb = new SSIS.tsEntities();
            ar a = alDb.ars.Where(arr => arr.rt == sym && arr.ex == ex).FirstOrDefault();
            pr pr = alDb.prs.Where(prr => prr.rt == sym && prr.ex == ex).FirstOrDefault();
            if (a != null)
            {
                List<KeyValuePair<string, double>> data = new List<KeyValuePair<string, double>>();

                data.Add(new KeyValuePair<string, double>("low", (a.lt.HasValue ? a.lt.Value : 0)));
                data.Add(new KeyValuePair<string, double>("med", (a.lt.HasValue ? a.mdt.Value : 0)));
                data.Add(new KeyValuePair<string, double>("mean", (a.lt.HasValue ? a.met.Value : 0)));
                data.Add(new KeyValuePair<string, double>("high", (a.lt.HasValue ? a.ht.Value : 0)));

                if (pr != null)
                {
                    data.Add(new KeyValuePair<string, double>("cur", (pr.price.HasValue ? pr.price.Value : 0)));
                }

                this.dbeAr.SetData(data, "AR");
            }

            per p = alDb.pers.Where(prr => prr.rt == sym && prr.ex == ex).FirstOrDefault();
            if (p != null)
            {
                List<KeyValuePair<string, double>> data = new List<KeyValuePair<string, double>>();

                data.Add(new KeyValuePair<string, double>("five", (p.five_day.HasValue ? p.five_day.Value : 0)));
                data.Add(new KeyValuePair<string, double>("OneM", (p.one_month.HasValue ? p.one_month.Value : 0)));
                data.Add(new KeyValuePair<string, double>("ThrM", (p.three_month.HasValue ? p.three_month.Value : 0)));
                data.Add(new KeyValuePair<string, double>("ytd", (p.ytd.HasValue ? p.ytd.Value : 0)));
                data.Add(new KeyValuePair<string, double>("oneY", (p.one_year.HasValue ? p.one_year.Value : 0)));
                this.dbePer.SetData(data, "Per");
            }
        }
    }
}
