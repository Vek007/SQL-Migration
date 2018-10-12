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
        }

        public void SetData(List<KeyValuePair<String, Double>> data, string title)
        {
            Series sr = new Series();
            sr.XAxisType = AxisType.Primary;

            double xVal = 5;
            foreach (KeyValuePair<string, double> pt in data)
            {
                DataPoint dp = new DataPoint(xVal, pt.Value);
                dp.AxisLabel = pt.Key;
                sr.Points.AddXY(xVal, pt.Value);
                xVal = xVal + 5;
            }

            this.lblTitle.Text = title;
        }
    }
}
