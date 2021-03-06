﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWT
{
   public partial class frmBwa
   {
       BackgroundWorker _multipleBwaWorker;
       BackgroundWorker _bwBenchmark;
       SequenceLogics _seqLogics;

       

       /// <summary>
       /// Handles the Click event of the btnStartMultipleBwa control.
       /// </summary>
       /// <param name="sender">The source of the event.</param>
       /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
       private void StartMultipleBwa( bool getShallowResults)
       {

           this.SaveSettings();

           IList<string> reads = this.chbUseGeneratedSequencies.Checked? this.txbSequencies.Text.Split(new string[]{Environment.NewLine},StringSplitOptions.RemoveEmptyEntries) : (IList<string>)this.GetRandomReads();
         
           this.txbMultiBwaResults.Text = String.Empty;

          
           

          this.InitMultiBwaWorker();
           this._multipleBwaWorker.DoWork += (s, arg) =>
           {
              // var sw = Stopwatch.StartNew();
              

               //var e1 = sw.Elapsed;
               //sw.Restart();
               //var serialized = iSearch.Serialize();
               //InexactSearch deserialized = InexactSearch.DeSerialize(serialized);
               //var e2 = sw.Elapsed;
               //sw.Stop();
               SequenceLogics.AlignMode alignMode = 0;
               if (this.rdvBwaSingleThread.Checked) alignMode = SequenceLogics.AlignMode.SingleThread;
               if (this.rdvBwaMultipleThread.Checked) alignMode = SequenceLogics.AlignMode.MultiThread;
               if (this.rdvBwaBoth.Checked) alignMode = SequenceLogics.AlignMode.SingleThread | SequenceLogics.AlignMode.MultiThread;
               this._seqLogics.RunMultipleAlignments(reads, (int)this.nupErrorsAllowed.Value, this._multipleBwaWorker, alignMode, getShallowResults);
           };

           this._multipleBwaWorker.RunWorkerCompleted += (s, arg) =>
               {
                   this.txbMultiBwaResults.AppendText(Environment.NewLine+ "Alignment Complete" + Environment.NewLine);
                   this.btnCancelMulti.Visible = false;
                   
               };
           this.btnCancelMulti.Visible = true;
           this._multipleBwaWorker.RunWorkerAsync();
           

       }

       private List<string> GetRandomReads()
       {
       
           int refLength = this.txbReference.Text.Length;
           int readLength = (int)Math.Min(this.nupReadLength.Value, refLength);
           int readCount = (int)this.nupNumberOfReads.Value;
           double errorPercentage = (double)(this.nupErrorPercentage.Value / 100);

           return this._seqLogics.GetRandomReads(readCount, readLength, errorPercentage);
       }

       

      
   }
}
