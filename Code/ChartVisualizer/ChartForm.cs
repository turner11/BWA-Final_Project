using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartVisualizer
{
   

    // FakeChart.cs
// ------------------------------------------------------------------
//
// A Winforms app that produces a contrived chart using
// DataVisualization (MSChart).  Requires .net 4.0.
//
// Author: Dino
//
// ------------------------------------------------------------------
//
// compile: \net4.0\csc.exe /t:winexe /debug+ /R:\net4.0\System.Windows.Forms.DataVisualization.dll FakeChart.cs
//

    public partial  class ChartForm : Form
    {
        System.Windows.Forms.DataVisualization.Charting.Chart Chart;

        public ChartForm()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Chart.Series.Clear();

            //var series = this.AddSeries("aaa", isInvisibleLegend: false);

            //for (int i = 0; i < 100; i++)
            //{
            //    series.Points.AddXY(i, i);

            //}
            /*----------------------------------------------*/
            //var name = "Series1";
            //var color = Color.Green;
            //var isInvisibleLegend = false;
            //var isXvalueIndexed = true;
            //var chartType = SeriesChartType.Line;

            //var series = AddSeries(name, isInvisibleLegend, isXvalueIndexed, chartType);
            //series.Color = Color.Red;
        }

        /// <summary>
        /// Adds the series.
        /// </summary>
        /// <param name="name"> Gets or sets the unique name of a Series object.</param>
        /// <param name="isVisibleLegend"> Gets or sets a flag that indicates whether the item is shown in the legend.</param>
        /// <param name="isXvalueIndexed">Gets or sets a flag that indicates whether data point indices will be used
        ///  for the X-values.</param>
        /// <param name="chartType">Gets or sets the chart type of a series.</param>
        public Series AddSeries(string name, Color color, bool isVisibleLegend = true, bool isXvalueIndexed = true, SeriesChartType chartType = SeriesChartType.Line)
        { 
           
            var series = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = name,
                Color = color,
                IsVisibleInLegend = isVisibleLegend,
                IsXValueIndexed = isXvalueIndexed,
                ChartType = chartType
            };
            //var xs = Enumerable.Range(0, 1000).Select(i => i * 0.1).ToArray();
            //var f = new Func<double, double>(x => Math.Sin(x));
            //var y = xs.Select(x => f(x)).ToArray();

            //for (int i = 0; i < xs.Length; i++)
            //{
            //    series.Points.AddXY(xs[i], y[i]);
            //}
            this.AddSeries(series);

            return series;
        }

        public void AddSeries(Series series)
        {
            this.Chart.Series.Add(series);
            Chart.Invalidate();
        }


        public void Clear()
        {
            if (this.Chart.Series != null)
            {
                this.Chart.Series.Clear();    
            }
            
        }
    }
}

