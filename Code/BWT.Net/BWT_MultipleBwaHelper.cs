using System;
using System.Collections.Generic;
using System.ComponentModel;
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
       BackgroundWorker _multipleBwaWorker;

       /// <summary>
       /// Handles the Click event of the btnStartMultipleBwa control.
       /// </summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
       private void StartMultipleBwa()
       {

           this.SaveSettings();

           List<string> reads = GetRandomReads();
           InexactSearch iSearch = null;
           this.txbMultiBwaResults.Text = String.Empty;

          this.InitMultiBwaWorker();
           this._multipleBwaWorker.DoWork += (s, arg) =>
           {
               iSearch = new InexactSearch(this.txbReference.Text, this.chbFindGaps.Checked, this._multipleBwaWorker);
               this.RunMultipleAlignments(reads, iSearch);
           };

           this._multipleBwaWorker.RunWorkerCompleted += (s, arg) =>
               {
                   this.txbMultiBwaResults.Text +=  Environment.NewLine+ "Alignment Complete" + Environment.NewLine;
                   
               };

           this._multipleBwaWorker.RunWorkerAsync();
           

       }

       private List<string> GetRandomReads()
       {
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
           double errorPercentage = (double)(this.nupErrorPercentage.Value / 100);

           List<int> startIndexes = new List<int>();
           int lastSampleLocation = refLength - readLength;
           for (int i = 0; i < readCount; i++)
           {
               int currStartIndex = (int)(this._rnd.NextDouble() * lastSampleLocation);
               startIndexes.Add(currStartIndex);
           }
           startIndexes.Sort();
           //This is for easier debugging
           startIndexes[0] = 0;

           List<string> reads = new List<string>();

           StringBuilder sb = new StringBuilder();
           foreach (int index in startIndexes)
           {
               sb.Clear();
               string originalString = this.txbReferenceMirror.Text.Substring(index, readLength);
               for (int i = 0; i < originalString.Length - 1; i++)
               {

                   bool error = this._rnd.NextDouble() < errorPercentage;
                   var nextchar = error ?
                       this.ReferenceLetters[i % this.ReferenceLetters.Count] : originalString[i];
                   sb.Append(nextchar);
               }
               var read = sb.ToString();
               reads.Add(read);

           }
           return reads;
       }

       private void RunMultipleAlignments(List<string> reads, InexactSearch iSearch)
       {

           int parrallelClosureCount = 0;
           Action<int> alignmentAction = (i) =>
           {
               var text = reads[i];
               this.txbSearch.Text = text;
               var results = this.PerformBwaAlignment(iSearch,text);
               string indexesStr = String.Join(",", results.Indexes);


               var percentage = ((parrallelClosureCount++  + 1.0) / reads.Count) * 100;
               this._multipleBwaWorker.ReportProgress((int)percentage, "results for read: " + indexesStr);
           };




           Action singleThreadAction = () =>
           {
               for (int i = 0; i < reads.Count; i++)
               {
                   var read = reads[i];
                   var results = this.PerformBwaAlignment(iSearch, read);
                   string indexesStr = String.Join(",",results.Indexes);

                   var percentage = ((i + 1.0) / reads.Count) * 100;
                   this._multipleBwaWorker.ReportProgress((int)percentage,"results for read #"+i+": "+indexesStr);
               }
           };


           Action multiThreadAction = () =>
           {
               Parallel.For(0, reads.Count, (i) =>
                   {
                       alignmentAction(i);
                   });
               //Parallel.ForEach(reads, alignmentAction);
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

           this._multipleBwaWorker.ReportProgress(0, Environment.NewLine + String.Format("BWA Elapsed time: {0}", sw.Elapsed.ToString()) + Environment.NewLine);

           if (this.rdvBwaBoth.Checked)
           {
               sw.Reset();
               sw.Start();
               singleThreadAction();
               sw.Stop();

               this._multipleBwaWorker.ReportProgress(0,
                   Environment.NewLine + String.Format("Elapsed time (single Thread): {0}{1}{1}", sw.Elapsed.ToString(), Environment.NewLine) + Environment.NewLine
               );
               sw.Reset();
               sw.Start();

               multiThreadAction();

               sw.Stop();
               this._multipleBwaWorker.ReportProgress(0,
                    Environment.NewLine+
                   String.Format("Elapsed time (multi Thread): {0}", sw.Elapsed.ToString())
                   + Environment.NewLine
                   );
           }
       }

      
   }
}
