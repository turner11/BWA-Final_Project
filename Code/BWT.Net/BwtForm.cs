using Microsoft.SqlServer.MessageBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BWT
{
    public partial class frmBwt : Form
    {
        /// <summary>
        /// The logics for all BWT manners
        /// </summary>
        BwtLogics _logics;

        

        public frmBwt()
        {
            this._logics = new BwtLogics();
             
            InitializeComponent();
            this._logics.SpeedOverReports = this.chbSpeedOverReports.Checked;

#if DEBUG
            this.txbInput.Text ="^BANANA";
#endif

            this.RestoreValuesFromSettings();

            LinkLabel.Link linkBwt = new LinkLabel.Link();
            linkBwt.LinkData = "http://en.wikipedia.org/wiki/Burrows%E2%80%93Wheeler_transform";
            this.lnkWikipedia.Links.Add(linkBwt);

            LinkLabel.Link linkBwa = new LinkLabel.Link();
            linkBwa.LinkData = "http://www.math.pku.edu.cn/teachers/xirb/Courses/biostatistics2013/Bioinformatics-2009-Li-1754-60.pdf";
            this.lnkBwaPaper.Links.Add(linkBwa);
        }

        /// <summary>
        /// Restores the values from settings.
        /// </summary>
        private void RestoreValuesFromSettings()
        {
            this.txbSearch.Text = BWT.Properties.Settings.Default.searchString;
            this.nupErrorsAllowed.Value = BWT.Properties.Settings.Default.errorsAllowed;
            this.txbReference.Text = BWT.Properties.Settings.Default.referecne;

            this.nupErrorPercentage.Value = BWT.Properties.Settings.Default.errorPercentage;
            this.nupNumberOfReads.Value = BWT.Properties.Settings.Default.numberOfReads;
            this.nupReadLength.Value = BWT.Properties.Settings.Default.readLength;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private void SaveSettings()
        {
            BWT.Properties.Settings.Default.errorPercentage = this.nupErrorPercentage.Value;
            BWT.Properties.Settings.Default.numberOfReads = (int)this.nupNumberOfReads.Value;
            BWT.Properties.Settings.Default.readLength = (int)this.nupReadLength.Value;
            BWT.Properties.Settings.Default.referecne = this.txbReference.Text;

            BWT.Properties.Settings.Default.searchString = this.txbSearch.Text;
            BWT.Properties.Settings.Default.errorsAllowed = (int)this.nupErrorsAllowed.Value;

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
                    this.txbOutPut.Text = bwtResult.OriginalText;
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
        

        

        #endregion

        private void frmBwt_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSettings();
        }

    }
}
