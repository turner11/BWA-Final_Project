using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWT
{
    public partial class BwtBenchmarkingPanel : UserControl
    {
        /// <summary>
        /// Gets or sets the precision to be shown. 
        /// </summary>
        /// <value>
        /// The precision.
        /// </value>
        public int Precision { get; set; }
        public BwtBenchmarkingPanel()
        {
            InitializeComponent();
            this.Precision = 8;
        }

        public void SetValues(int inputLength, TimeSpan time)
        {
            
            this.txbBwtTime.Text = Math.Round(time.TotalSeconds,this.Precision) + " (s)";
            if (inputLength != 0)
            {
                var timePerChar = time.TotalSeconds / inputLength;


                this.txbTimePerChar.Text = Math.Round(timePerChar, this.Precision) + " (s/c)";
            }
        }

        internal void Reset()
        {
            this.txbBwtTime.Text = String.Empty;
            this.txbTimePerChar.Text = String.Empty;
        }
    }
}
