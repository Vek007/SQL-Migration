using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SSIS
{
    public partial class DashBoardElement : UserControl
    {
        public DashBoardElement()
        {
            InitializeComponent();
            this.tblMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        public void SetData(List<KeyValuePair<String, Double>> data, string title)
        {
            Series sr = new Series();
            sr.ChartType = SeriesChartType.Point;
            sr.XAxisType = AxisType.Primary;
            sr.YAxisType = AxisType.Secondary;
            sr.LabelForeColor = Color.Cyan;

            double xVal = 5;
            foreach (KeyValuePair<string, double> pt in data)
            {
                DataPoint dp = new DataPoint(xVal, pt.Value);
                dp.AxisLabel = pt.Key;
                dp.Label = pt.Value.ToString();
                sr.Points.Add(dp);
                xVal = xVal + 5;
            }
            this.tChart.SetChartData(sr, title);

            this.tGrid.SetGridData(data);

            this.lblTitle.Text = title;
        }
    }
}
