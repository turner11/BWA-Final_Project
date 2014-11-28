using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWT
{
   public partial class frmBwt
   {
       Random _rnd = new Random(0);

       /// <summary>
       /// Handles the Click event of the btnStartMultipleBwa control.
       /// </summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
       private void btnStartMultipleBwa_Click(object sender, EventArgs e)
       {

           this.SaveSettings();

           //StringBuilder sba = new StringBuilder();
           //var a = new char[] { 'A', 'C', 'G', 'T' };
           //for (int i = 0; i < 2000; i++)
           //{
           //    var idx = (int)Math.Round(this._rnd.NextDouble() * 3, MidpointRounding.AwayFromZero); ;
           //    var nextchar = a[idx];
           //    sba.Append(nextchar);
           //}
           //var s = sba.ToString();
           int refLength = this.txbReferenceMirror.Text.Length;
           int readLength = (int)Math.Min(this.nupReadLength.Value, refLength);
           int readCount = (int)this.nupNumberOfReads.Value;
           decimal errorPercentage = this.nupErrorPercentage.Value / 100;

           List<int> startIndexes = new List<int>();
           int lastSampleLocation = refLength - readLength;
           for (int i = 0; i < readCount; i++)
           {
               int currStartIndex = (int)(this._rnd.NextDouble() * lastSampleLocation);
               startIndexes.Add(currStartIndex);
           }



           List<string> reads = new List<string>();
           
           StringBuilder sb = new StringBuilder();
           foreach (int index in startIndexes)
           {
               sb.Clear();    
               string originalString = this.txbReferenceMirror.Text.Substring(index, readLength);
               for (int i = 0; i < originalString.Length-1; i++)
               {
                   
                   bool error = this._rnd.NextDouble() < 0.5;
                   var nextchar = error?
                       this.ReferenceLetters[i % this.ReferenceLetters.Count] : originalString[i];
                   sb.Append(nextchar);
               }
                reads.Add(sb.ToString());
             
           }

          
           InexactSearch iSearch = new InexactSearch(this.txbReference.Text, this.chbFindGaps.Checked);



           Action<string> alignmentAction = (r) =>
               {
                   this.txbSearch.Text = r;
                   this.PerformBwaAlignment(iSearch);
               };


          
           
           Action singleThreadAction = () =>
           {

               foreach (var read in reads)
               {
                   this.txbSearch.Text = read;
                   this.PerformBwaAlignment(iSearch);
               }    
           };

           
           Action multiThreadAction = () =>
           {
               Parallel.ForEach(reads, alignmentAction);
           };

           
           Stopwatch sw = new Stopwatch();
           sw.Start();

           if (this.rdvBwaSingleThread.Checked)
           {
               singleThreadAction();
           }
           else if (this.rdvBwaMultipleThread.Checked)
           {
               multiThreadAction();
           }
           sw.Stop();

           this.txbMultiBwaResults.Text = String.Format("Elapsed time: {0}", sw.Elapsed.ToString());

           if (this.rdvBwaBoth.Checked)           
           {
               sw.Reset();
               sw.Start();
               singleThreadAction();
               sw.Stop();

               this.txbMultiBwaResults.Text = 
                   String.Format("Elapsed time (single Thread): {0}{1}{1}", sw.Elapsed.ToString(),Environment.NewLine);
               
               this.txbMultiBwaResults.Refresh();
               sw.Reset();
               sw.Start();
               
               multiThreadAction();
               
               sw.Stop();
               this.txbMultiBwaResults.Text +=
                   String.Format("Elapsed time (multi Thread): {0}", sw.Elapsed.ToString());
           }
           

       }

      
   }
}
