using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSIS
{
    public partial class TitledGrid : UserControl
    {
        public TitledGrid()
        {
            InitializeComponent();
        }

        public void SetGridData(List<KeyValuePair<String, Double>> data)
        {
            this.dgvChartData.Columns.Add("clmName", "Name");
            this.dgvChartData.Columns.Add("clmValue", "Value");

            this.dgvChartData.Rows.Add(data.Count);

            int i = 0;
            foreach (KeyValuePair<string, double> row in data)
            {
                this.dgvChartData.Rows[i].Cells[0].Value = row.Key;
                this.dgvChartData.Rows[i].Cells[1].Value = row.Value;
                i++;
            }
        }
    }
}
