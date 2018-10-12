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
    public partial class TitledChart : UserControl
    {
        public TitledChart()
        {
            InitializeComponent();
            this.chart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True; 
        }

        public void SetChartData(Series s, string title)
        {
            this.chart.Series.Clear();
            this.chart.Series.Add(s);
        }
    }
}
