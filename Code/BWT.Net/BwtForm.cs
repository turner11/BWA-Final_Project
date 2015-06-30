using ChartVisualizer;
using Microsoft.SqlServer.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWT
{
    public partial class tplBwaReference : Form
    {

        const int EXPANDED_REFERENCE_HEIGHT = 200;
        const int COMPACT_REFERENCE_HEIGHT = 75;

        TextWindow _frmSequencies;

        /// <summary>
        /// The logics for all BWT manners
        /// </summary>
        BwtLogics _logics;

        const string DEFAULT_REFERENCE =
        #region DefaultReference
 "TAGAGCTCCAGGGTAGCGGTGGATTGAGCTCTGCGACAGGTCGGGGGCCACGCGGCCCTGGGCAGCCAGGAGGTGCTGGGCCACGCGGCCCGGCTGGCCTCCTCCGGTGCTATAGTCGACAGTTGAAGTCGGAAGTTTACATACACTTAGGTTGGAGTGGGCAGCCAGGAGGTGCTGGGCCACGCGGCCCGGCTGGCCTCCTCCGGTATCCATCGATCCAAGATGGCGGCGCTGAGCAGCGGCAGCAGCGCCCATAACCCACCATCAATATATTTAGAGGCCTATGAGGAGTACACCAGCGGGATCACTCTCGGCATGGACGAGCTGTACAAGTAAGTCGACGTCGACTCGATCATCATAATTCTGTCTCATTCCTCTTTGCACTCGGATCGTACGCTATTCTATGATTACACGGTTGCGATCCATAATCTCCCTTGGGGAATATACACGCTGGCTTACTCGAATTTGACTCCGTACGGTACTCGCCATCCGAACTAGATAACCCTTATAGAAATATACACGCTTGTGAAACATAATCCGGCTGCAACGTTTAACACTCCTAATTAGGACATAACTCTTGACACTTACTTAGAGAACTCTCCGCTCATAGGAGATAATTTACTAAAGATGACGGTTTCTTTTGACTCGAAGCATTTACCGCCCCTAGATCCATCATATGATGCCGTTGACTCCAGCGTAGATATCAGCTGGCCCTCGCATGATATCTGCGCGATGCGGGTCGATCCATATGCCTATCGGCAAGCGGATGATATCGGCGCTGCCTGCATGGATGATATCGGCTCACCAGATGGCCTATCGGCTTCCGTGGCTACAACGTTGTAGCCACGGAAGCCGATAGCTATCGGCCGTCGTGGCTACAACGTTGTAGCCACGACGGCCGATAGGATAGTCGACGCCGAGGAAACGCGCGGAATTCGATGGATTTCCCTCGGATAGTCGACCAGGGTAGCGGTGGATTGTCGACTGCGACAGGTCGGGATAGTCGACACCTCCATCTGCGATACCATTTCGTACTGCGACAGGTCGGCGGGCGATAGTCGACCGCAGCCACTCCCGATGCTTGCTTGGATAGTCGACCGCAGCCACCTCCGATGCTTGTCCGGACATGACAGGGATAGTCGACCGCAGCCACCTCCGATGCTCCCTTGGACATGACAGGGATAGTCGACCGCAGCCACCTCTCCTGCTTGCTTTCCCATGACAGGTCGCTATCGGCGGTCGTGGCTACAACGTTGTAGCCACGACCGCCGATAGCTATCGGCATACGTGGCTACAACGTTGTAGCCACGTATGCCGATAGGCCGCCTTTCGGCAAGCGCGCTTGCCGAAAGGCGGCGCCGCCTGATGGCAAGCGCGCTTGCCATCAGGCGGCGATCGTCGACTTGTAGGGAGGTCTCAATGAGCTCCGCAGCCACCTCCGATGATAGAGCTCGCCGAGGAAACGGATCGTCGACATGGATTTCCCTCGATAGAGCTCCAGGGTAGCGGTGGATTGAGCTCTGCGACAGGTCGGGGGCCACGCGGCCCCCTATCGGCAAGCGGATGATATCGGCGCTGCCTGCATGGATGATATCGGCTCACCAGATGGCCTATCGGCTTCCGTGGCTACAACGTTGTAGCCACGGAAGCCGATAGCTATCGGCCGTCGTGGCTACAACGTTGTAGCCACGACGGCCGATAGGATAGTCGACGCCGAGGAAACGCGCGGAATTCGATGGATTTCCCTCGGATAGTCGACCAGGGTAGCGGTGGATTGTCGACTGCGACAGGTCGGGATAGTCGACACCTCCATCTGCGATACCATTTCGTACTGCGACAGGTCGGCGGGCGATAGTCGACCGCAGCCACTCCCGATGCTTGCTTGGATAGTCGACCGCAGCCACCTCCGATGCTTGTCCGGACATGACAGGGATAGTCGACCGCAGCCACCTCCGATGCTCCCTTGGACATGACAGGGATAGTCGACCGCAGCCACCTCT";
        #endregion

        public tplBwaReference()
        {
            this._logics = new BwtLogics();

            InitializeComponent();

            this.scBwa.SplitterDistance = COMPACT_REFERENCE_HEIGHT;

            this._logics.SpeedOverReports = this.chbSpeedOverReports.Checked;

            this.nupCountGeneratedStrings_Multi.Maximum = decimal.MaxValue;
            this.nupCountGeneratedStrings_Single.Maximum = decimal.MaxValue;


            this.nupCountGeneratedStrings_Single.DoubleClick += nupCountGeneratedStrings_DoubleClick;
            this.nupCountGeneratedStrings_Multi.DoubleClick += nupCountGeneratedStrings_DoubleClick;




            this._seqLogics = new SequenceLogics();
            this._seqLogics.PreAlignmnet += seqLogics_PreAlignmnet;




            //#if DEBUG
            this.txbInput.Text = "^BANANA";
            //#endif

            this.RestoreValuesFromSettings();
            this._seqLogics.FindGapgs = this.chbFindGaps.Checked;

            LinkLabel.Link linkBwt = new LinkLabel.Link();
            linkBwt.LinkData = "http://en.wikipedia.org/wiki/Burrows%E2%80%93Wheeler_transform";
            this.lnkWikipedia.Links.Add(linkBwt);

            LinkLabel.Link linkBwa = new LinkLabel.Link();
            linkBwa.LinkData = "http://www.math.pku.edu.cn/teachers/xirb/Courses/biostatistics2013/Bioinformatics-2009-Li-1754-60.pdf";
            this.lnkBwaPaper.Links.Add(linkBwa);


            this._frmSequencies = new TextWindow();

            this.InitMultiBwaWorker();
        }



        private void InitMultiBwaWorker()
        {
            if (this._multipleBwaWorker != null)
            {
                this._multipleBwaWorker.Dispose();
            }
            this._multipleBwaWorker = new BackgroundWorker();
            

            this._multipleBwaWorker.WorkerReportsProgress = true;
            this._multipleBwaWorker.WorkerSupportsCancellation = true;
            this._multipleBwaWorker.ProgressChanged += (s, arg) =>
            {
                Action updateProg = () =>
                    {
                        this.txbMultiBwaResults.AppendText(arg.UserState.ToString() + Environment.NewLine);
                        this.pbTransform.Value = Math.Min(Math.Max(0, arg.ProgressPercentage), 100);
                    };

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(updateProg, new object[0]);
                }
                else
                {
                    updateProg();
                }
            };
        }

        /// <summary>
        /// Restores the values from settings.
        /// </summary>
        private void RestoreValuesFromSettings()
        {
            string settingsReference = BWT.Properties.Settings.Default.referecne;


            if (String.IsNullOrWhiteSpace(settingsReference))
            {
                BWT.Properties.Settings.Default.referecne = DEFAULT_REFERENCE;
                BWT.Properties.Settings.Default.Save();
            }

            this.txbReference.Text = BWT.Properties.Settings.Default.referecne;

            this.txbSearch.Text = BWT.Properties.Settings.Default.searchString;
            this.nupErrorsAllowed.Value = BWT.Properties.Settings.Default.errorsAllowed;


            this.nupErrorPercentage.Value = BWT.Properties.Settings.Default.errorPercentage;
            this.nupNumberOfReads.Value = BWT.Properties.Settings.Default.numberOfReads;
            this.nupReadLength.Value = BWT.Properties.Settings.Default.readLength;

            this.nupMaxDegreeOfParallelism.Value = BWT.Properties.Settings.Default.MaxDegreeOfParallelism;

            this.chbFindGaps.Checked = BWT.Properties.Settings.Default.handleGaps;

            this.txbBenchmarkSeqCountVary.Text = BWT.Properties.Settings.Default.benchmark_count;
            this.txbBenchmarkLengthVaryVariables.Text = BWT.Properties.Settings.Default.bnechmark_length;
            this.txbBenchmarkSorts.Text = BWT.Properties.Settings.Default.benchmark_sort;  
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            BWT.Properties.Settings.Default.errorPercentage = this.nupErrorPercentage.Value;
            BWT.Properties.Settings.Default.numberOfReads = (long)this.nupNumberOfReads.Value;
            BWT.Properties.Settings.Default.readLength = (int)this.nupReadLength.Value;
            BWT.Properties.Settings.Default.referecne = this.txbReference.Text;

            BWT.Properties.Settings.Default.searchString = this.txbSearch.Text;
            BWT.Properties.Settings.Default.errorsAllowed = (int)this.nupErrorsAllowed.Value;
            BWT.Properties.Settings.Default.MaxDegreeOfParallelism = (int)this.nupMaxDegreeOfParallelism.Value;
            BWT.Properties.Settings.Default.handleGaps = (bool)this.chbFindGaps.Checked;

            BWT.Properties.Settings.Default.benchmark_count = this.txbBenchmarkSeqCountVary.Text;
            BWT.Properties.Settings.Default.bnechmark_length = this.txbBenchmarkLengthVaryVariables.Text;
            BWT.Properties.Settings.Default.benchmark_sort = this.txbBenchmarkSorts.Text;



            BWT.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Performs the B-W transform a-synchronously.
        /// </summary>
        /// <param name="input">The input to perform the transform upon.</param>
        private void PerformBwt(string input)
        {
            this.ClearResults();
            BwtLogics.BwtResults bwtResult = null;

            Exception thrownException = null;
            BackgroundWorker bw = new BackgroundWorker() { WorkerReportsProgress = true };

            Stopwatch sw = new Stopwatch();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                try
                {
                    sw.Start();
                    bwtResult = this._logics.Bwt(input);
                    sw.Stop();
                }
                catch (Exception ex)
                {

                    thrownException = ex;
                }

            };
            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                this.AssignProgressToGUI(bwArgs);
            };
            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {

                bw.Dispose();

                this.bchBwt.SetValues(input.Length, sw.Elapsed);
                this.bchBwt.Visible = true;

                if (thrownException == null && bwtResult != null)
                {
                    this.txbOutPut.Text = bwtResult.BwtString;
                    if (this.chbPerformReverseTransform.Checked)
                    {
                        this.PerformReverseBwt(bwtResult.BwtString);
                    }
                }
                else
                {
                    string caption = "An error occurred during calculation of BWT transform";
                    this.ShowExceptionMessageBox(thrownException, caption);
                }

            };
            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Performs the reverse B-W transform a-synchronously.
        /// </summary>
        /// <param name="input">The input to perform the reverse transform upon.</param>
        private void PerformReverseBwt(string transformedInput)
        {
            string origInput = String.Empty;
            if (String.IsNullOrEmpty(transformedInput))
            {
                MessageBox.Show("Could not calculate reverse BWT. the transformed output was empty.");
                return;
            }

            string output = String.Empty;
            Exception thrownException = null;
            BackgroundWorker bw = new BackgroundWorker() { WorkerReportsProgress = true };
            Stopwatch sw = new Stopwatch();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                try
                {
                    sw.Start();
                    output = this._logics.ReverseBwt(transformedInput, bw);
                    sw.Stop();
                }
                catch (Exception ex)
                {

                    thrownException = ex;
                }

            };
            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                this.AssignProgressToGUI(bwArgs);
            };
            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.bchReverse.SetValues(transformedInput.Length, sw.Elapsed);
                this.bchReverse.Visible = true;

                bw.Dispose();
                if (thrownException == null)
                {
                    this.txbReversedOutput.Text = output;

                }
                else
                {
                    string caption = "An error occurred during calculation of reverse transform";
                    this.ShowExceptionMessageBox(thrownException, caption);
                }

            };
            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Assigns the progress to GUI based on specified <see cref="ProgressChangedEventArgs"/> arguments.
        /// </summary>
        /// <param name="bwArgs">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
        private void AssignProgressToGUI(ProgressChangedEventArgs bwArgs)
        {

            int percentage = Math.Max(Math.Min(bwArgs.ProgressPercentage, 100), 0);
            this.pbTransform.Value = percentage;
            this.pbTransform.Visible = percentage != 100;

            string progressState = bwArgs.UserState as string;

            bool showProgressText = !String.IsNullOrWhiteSpace(progressState)
                                    && (progressState.Length < 15
                                        || percentage == 100);
            if (showProgressText)
            {
                this.txbIntermediates.Text += Environment.NewLine + Environment.NewLine + progressState;
            }
        }

        /// <summary>
        /// Shows an exception message box.
        /// </summary>
        /// <param name="thrownException">The exception.</param>
        /// <param name="caption">The caption.</param>
        private void ShowExceptionMessageBox(Exception thrownException, string caption)
        {
            ExceptionMessageBox emb = new ExceptionMessageBox(thrownException);

            emb.Caption = caption;
            emb.Message = thrownException;

            emb.Symbol = ExceptionMessageBoxSymbol.Error;

            emb.Show(this);

        }

        /// <summary>
        /// Clears the results from GUI.
        /// </summary>
        private void ClearResults()
        {
            this.txbOutPut.Text = String.Empty;
            this.txbReversedOutput.Text = String.Empty;
            this.txbIntermediates.Text = String.Empty;
            this.bchReverse.Visible = false;
            this.bchReverse.Visible = false;
            this.bchBwt.Visible = false;

            this.bchReverse.Reset();
            this.bchBwt.Reset();
        }


        private void CollapseReferenceTextBox()
        {
            this.scBwa.SplitterDistance = COMPACT_REFERENCE_HEIGHT;
        }

        #region Event Handlers
        /// <summary>
        /// Handles the Click event of the btnExecute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExecute_Click(object sender, EventArgs e)
        {

            string input = this.txbInput.Text;
            this.PerformBwt(input);

            this.txbInput.TextChanged += new System.EventHandler(this.TreanformTextBox_TextChanged);
            this.txbReversedOutput.TextChanged += new System.EventHandler(this.TreanformTextBox_TextChanged);

        }

        /// <summary>
        /// Handles the TextChanged event of the txbInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txbInput_TextChanged(object sender, EventArgs e)
        {
            this.ClearResults();
        }

        /// <summary>
        /// Handles the LinkClicked event of the lnkWikipedia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void lnkWikipedia_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }

        /// <summary>
        /// Handles the KeyDown event of the txbInput control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chbSpeedOverReports control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chbSpeedOverReports_CheckedChanged(object sender, EventArgs e)
        {
            this._logics.SpeedOverReports = this.chbSpeedOverReports.Checked;
        }



        private void frmBwt_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSettings();
        }


        private void btnStartMultipleBwa_Click(object sender, EventArgs e)
        {
            this.StartMultipleBwa();
        }

        private void btnClearMultiple_Click(object sender, EventArgs e)
        {
            this.txbMultiBwaResults.Clear();
        }

        private async void chbFindGaps_CheckedChanged(object sender, EventArgs e)
        {
            this._seqLogics.FindGapgs = this.chbFindGaps.Checked;
            this.SaveSettings();
            await this.SetNumberOfStrignsToSearch();
        }

        void seqLogics_PreAlignmnet(object sender, EventArgs e)
        {
            this._seqLogics.FindGapgs = this.chbFindGaps.Checked;
        }

        private async void txbReference_DoubleClick(object sender, EventArgs e)
        {
            if (this._seqLogics.iSearch != null)
            {
                string sa = null;
                await Task.Run(() =>
                    {
                        sa = this._seqLogics.iSearch.GetIndexedSuffixArray();

                    });

                var txtWindow = new TextWindow();
                //txtWindow.txb.WordWrap = false;// for performance
                txtWindow.Show(this);
                txtWindow.TextContent = "Loading...";
                txtWindow.Refresh();
                txtWindow.TextContent = sa;



            }
        }


        private void txbMultiBwaResults_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblReference_DoubleClick(object sender, EventArgs e)
        {
            bool isExpanded = this.scBwa.SplitterDistance >= EXPANDED_REFERENCE_HEIGHT;
            this.scBwa.SplitterDistance = isExpanded ? COMPACT_REFERENCE_HEIGHT : EXPANDED_REFERENCE_HEIGHT;
        }

        private async void nupErrorsAllowed_ValueChanged(object sender, EventArgs e)
        {
            await this.SetNumberOfStrignsToSearch();
        }

        private async void nupErrorPercentage_ValueChanged(object sender, EventArgs e)
        {
            await this.SetNumberOfStrignsToSearch();
        }

        private async void nupNumberOfReads_ValueChanged(object sender, EventArgs e)
        {
            await this.SetNumberOfStrignsToSearch();
        }

        void nupCountGeneratedStrings_DoubleClick(object sender, EventArgs e)
        {
            var nup = sender as NumericUpDown;
            if (nup != null)
            {
                var p = nup.PointToScreen(nup.Location);
                this._tt.Show(nup.Value.ToString("N0"), this, p, 5000);
            }

        }

        /// <summary>
        /// Handles the TextChanged event of the TreanformTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TreanformTextBox_TextChanged(object sender, EventArgs e)
        {
            var led_ok = BWT.Properties.Resources.green_led;
            var led_intermidiate = BWT.Properties.Resources.orange_led;
            var led_error = BWT.Properties.Resources.red_led;

            bool inputhasText = !String.IsNullOrWhiteSpace(this.txbInput.Text);
            bool reverseHasText = !String.IsNullOrWhiteSpace(this.txbReversedOutput.Text);

            this.pbLed.Visible = inputhasText || reverseHasText;

            Bitmap image;
            if (inputhasText ^ reverseHasText)
            {
                image = led_intermidiate;
            }
            else if (this.txbInput.Text.Equals(this.txbReversedOutput.Text, StringComparison.Ordinal))
            {
                image = led_ok;
            }
            else
            {
                image = led_error;
            }

            this.pbLed.Image = image;


        }

        /// <summary>
        /// Handles the Click event of the btnInexactSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnInexactSearch_Click(object sender, EventArgs e)
        {
            this.CollapseReferenceTextBox();
            //clear history
            this.txbBwaResults.Text = String.Empty;
            this.SaveSettings();



            var results = await this.PerformBwaAlignment();

            this.txbBwaResults.Text = results.GetSummaryMessage();

        }

        /// <summary>
        /// Handles the KeyPress event of the txbSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        private void txbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Check for a naughty character in the KeyDown event.
            //22 = paste...
            if (e.KeyChar != 22 && !this.ReferenceLetters.Contains(e.KeyChar) && e.KeyChar != '\b')
            {
                // Stop the character from being entered into the control since it is illegal.
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
        /// <summary>
        /// Handles the TextChanged event of the txbSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void txbSearch_TextChanged(object sender, EventArgs e)
        {
            Func<char, bool> filterFunc = c => !this.ReferenceLetters.Contains(c);
            var illigalLetters = this.txbSearch.Text.Where(filterFunc).ToList();
            if (this.txbReference.Text.Length > 0 && illigalLetters.Count > 0)
            {
                MessageBox.Show("The letters '" + String.Join(",", illigalLetters) + "' Do not appear in reference and are not valid");
            }
            this.txbSearch.Text = String.Join(String.Empty, this.txbSearch.Text.Where(c => !filterFunc(c)).ToList());

            this.lblSearch.Text = String.Format("String to search ({0})", this.txbSearch.Text.Length);
            await this.SetNumberOfStrignsToSearch();
        }

        /// <summary>
        /// Handles the KeyDown event of the txbReference control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txbReference_KeyDown(object sender, KeyEventArgs e)
        {
            this.HandleTextBoxKeyDown(sender, e);
        }

        /// <summary>
        /// Handles the text box key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void HandleTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            var txb = sender as TextBox;
            if (txb == null)
            {
                return;
            }
            if (e.Control && (e.KeyCode == Keys.A))
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyDown event of the txbBwaResults control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txbBwaResults_KeyDown(object sender, KeyEventArgs e)
        {
            this.HandleTextBoxKeyDown(sender, e);
        }



        /// <summary>
        /// Handles the ValueChanged event of the nupReadLength control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void nupReadLength_ValueChanged(object sender, EventArgs e)
        {
            int maxDiff = InexactSearch.GetCalculatedMaxError((int)this.nupReadLength.Value);
            this.lblRecommendedMaxError.Text = String.Format("Calculated Max Error: {0}", maxDiff);
            await this.SetNumberOfStrignsToSearch();
        }

        /// <summary>
        /// Handles the TextChanged event of the txbReference control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txbReference_TextChanged(object sender, EventArgs e)
        {
            this._seqLogics.Reference = this.txbReference.Text;
        }
        private void nupMaxDegreeOfParallelism_ValueChanged(object sender, EventArgs e)
        {
            var val = (int)this.nupMaxDegreeOfParallelism.Value;
            this._seqLogics.DegreeOfParallelism = val > 1 ? (int?)val : null;
        }

        private void btnGenerateSequencies_Click(object sender, EventArgs e)
        {
            var seqs = this._seqLogics.GetRandomReads((int)this.nupNumberOfReads.Value,
                                                    (int)this.nupReadLength.Value,
                                                    (double)this.nupErrorPercentage.Value);

            this.txbSequencies.Lines = seqs.ToArray();

        }

        private void btnClearSequencies_Click(object sender, EventArgs e)
        {

            this.txbSequencies.Clear();
        }

        private void btnSortSequencies_Click(object sender, EventArgs e)
        {

            var lines = this.txbSequencies.Lines.ToArray();
            Array.Sort(lines);
            this.txbSequencies.Lines = lines;

        }

        private void txbSequencies_TextChanged(object sender, EventArgs e)
        {
            this.chbUseGeneratedSequencies.Checked = this.txbSequencies.Text.Length > 0;
        }

        private void nupMaxDegreeOfParallelism_DoubleClick(object sender, EventArgs e)
        {
            this.nupMaxDegreeOfParallelism.Value = Environment.ProcessorCount - 1;
        }
        #endregion

        private async void btnlblBenchmarkVariantLength_Click(object sender, EventArgs e)
        {
            var txt = this.txbBenchmarkLengthVaryVariables.Text;
            var parsedParams = this.ParseBenchmarkArguments(txt);
            var seqCount = parsedParams.freeParam;

            var reads = new List<List<string>>();
            for (int currLength = parsedParams.min; currLength <= parsedParams.max; currLength += parsedParams.interval)
            {

                var currReads = this._seqLogics.GetRandomReads(seqCount, currLength, (int)this.nupErrorPercentage.Value);
                reads.Add(currReads);
            }

            string title = "Time over Seq length";
            Func<List<string>, double> xAxisFunc = (readsCollection) => readsCollection.First().Length;
            await RunBenchmarkTest(reads, xAxisFunc, title);


        }

        private ParsedBenchmarkParameters ParseBenchmarkArguments(string txt)
        {
            var args = txt.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var paramCollection = new ParsedBenchmarkParameters();
            try
            {
                var range = args[0].Split(new char[] { '-' });
                paramCollection.min = int.Parse(range[0]);
                paramCollection.max = int.Parse(range[1]);
                paramCollection.interval = int.Parse(args[1]);
                paramCollection.freeParam = int.Parse(args[2]);
              

                var legths = new List<int>();
                var benchmarks = new List<double>();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to parse test input values.");
                paramCollection = null;
            }
            return paramCollection;
        }

        class ParsedBenchmarkParameters
        {
            public int min = -1;
            public int max = -1;
            public int interval = -1;
            public int freeParam = -1;
           
        }

        private async void btnBenchmarkVariantSeqCount_Click(object sender, EventArgs e)
        {
            var txt = this.txbBenchmarkSeqCountVary.Text;
            var parsedParams = this.ParseBenchmarkArguments(txt);
            var seqLength = parsedParams.freeParam;

            var reads = new List<List<string>>();
            for (int currCount = parsedParams.min; currCount <= parsedParams.max; currCount += parsedParams.interval)
            {

                var currReads = this._seqLogics.GetRandomReads(currCount, seqLength,(int)this.nupErrorPercentage.Value);
                reads.Add(currReads);
            }

            string title = "Time over Seq Count";
            Func<List<string>, double> xAxisFunc = (readsCollection) => readsCollection.Count;
            await RunBenchmarkTest(reads, xAxisFunc, title);
        }

        private async void btnBenchmarkSorts_Click(object sender, EventArgs e)
        {
            var txt = this.txbBenchmarkSorts.Text;
            var parsedParams = this.ParseBenchmarkArguments(txt);
            var seqLength = parsedParams.freeParam;

            var reads = new List<List<string>>();
            for (int currCount = parsedParams.min; currCount <= parsedParams.max; currCount += parsedParams.interval)
            {

                var currReads = this._seqLogics.GetRandomReads(currCount, seqLength, (int)this.nupErrorPercentage.Value);
                reads.Add(currReads);
            }

            var readsSorted = reads.Select(r => r.OrderBy(c => c).ToList()).ToList();
            string title = "Time over Seq Count. sorted VS not sorted";
            Func<List<string>, double> xAxisFunc = (readsCollection) => readsCollection.Count;
            await RunBenchmarkTest(readsSorted, "Sorted", reads, "Not Sorted", SequenceLogics.AlignMode.MultiThread, SequenceLogics.AlignMode.MultiThread, xAxisFunc, title);
        }

        private Task RunBenchmarkTest(List<List<string>> reads, Func<List<string>, double> readsToxAxisFunc, string title)
        {
            return this.RunBenchmarkTest(reads,"Parallel", reads,"Sequential", SequenceLogics.AlignMode.MultiThread, SequenceLogics.AlignMode.SingleThread, readsToxAxisFunc, title);
        }

        private async Task RunBenchmarkTest(List<List<string>> readsCollection1, string legend1, List<List<string>> readsCollection2, string legend2, SequenceLogics.AlignMode testMode1, SequenceLogics.AlignMode testMode2, Func<List<string>, double> readsToxAxisFunc, string title)
        {
            this.SaveSettings();
            var chartForm = new ChartForm();

            chartForm.Title = title;
            var series_multi = chartForm.AddSeries(legend1, Color.Green);
            var series_signle = chartForm.AddSeries(legend2, Color.Red);
            var series_ration = chartForm.AddSeries(legend1 + " / " +legend2, Color.Blue);

            series_multi.IsValueShownAsLabel = true;
            series_signle.IsValueShownAsLabel = true;
            series_ration.IsValueShownAsLabel = true;

            chartForm.Text = String.Format("{0} ", legend1 + " / " + legend2);
            chartForm.Show();

            var readSetsCount = Math.Min(readsCollection1.Count, readsCollection2.Count);

            await Task.Run(() =>
                {
                    for (int i = 0; i < readSetsCount; i++)
                    {

                        var bw = new BackgroundWorker() { WorkerReportsProgress = true };
                        Stopwatch sw = null;
                        TimeSpan elapsedMulti = TimeSpan.Zero;
                        TimeSpan elapsedSingle = TimeSpan.Zero;
                        var test1reads = readsCollection1[i];
                        var test2reads = readsCollection2[i];

                        bw.ProgressChanged += (s, arg) =>
                        {
                            Action ac = () => this.pbTransform.Value = Math.Min(Math.Max(0, arg.ProgressPercentage), 100);
                            this.pbTransform.BeginInvoke(ac, null);
                        };


                        sw = Stopwatch.StartNew();
                        elapsedMulti = this._seqLogics.RunMultipleAlignments(test1reads, (int)this.nupErrorsAllowed.Value, bw, testMode1);
                        var test1 = sw.Elapsed;
                        
                        sw.Restart();
                        elapsedSingle = this._seqLogics.RunMultipleAlignments(test2reads, (int)this.nupErrorsAllowed.Value, bw, testMode2);
                        var test2 = sw.Elapsed;
                        sw.Stop();


                        var currXValue = readsToxAxisFunc(test1reads);
                        chartForm.BeginInvoke(new Action(() =>
                        {

                            series_multi.Points.AddXY(currXValue, elapsedMulti.TotalSeconds);
                            series_signle.Points.AddXY(currXValue, elapsedSingle.TotalSeconds);
                            series_ration.Points.AddXY(currXValue, elapsedSingle.TotalSeconds / elapsedMulti.TotalSeconds);
                            chartForm.Refresh();
                        }), null);
                        // this.txbBenchmarkLog.Text = String.Format("Aligned {0} length sequences ({1} seconds)", currLength, sw.Elapsed.TotalSeconds);


                    }
                });

            chartForm.Title = title + "  - Completed.";




        }

        private void btnCancelMulti_Click(object sender, EventArgs e)
        {
            if (this._multipleBwaWorker != null)
            {
                this._multipleBwaWorker.CancelAsync();
            }
        }

        



















    }
}
