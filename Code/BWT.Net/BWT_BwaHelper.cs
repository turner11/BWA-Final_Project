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
    public partial class tplBwaReference
    {
        /// <summary>
        /// The letters 
        /// </summary>
       
        List<char> ReferenceLetters
        {
            get
            {
                return this._seqLogics.ReferenceLetters;
            }
        }
                
        #region Event Handlers
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
           
            this.txbBwaResults.Text =  results.GetSummaryMessage();

        }


        /// <summary>
        /// Performs the BWA alignment for the Text in <see cref="txbSearch"/>.
        /// </summary>
        /// <param name="iSearch">The InexactSearch instance - this is for avoiding creation of a new one which will result a new index.</param>
        /// <returns>The alignment results</returns>
        private async Task<InexactSearch.Results> PerformBwaAlignment()
        {
            var text = this.txbSearch.Text;
            return await this.PerformBwaAlignment(text);
        }

        /// <summary>
        /// Performs the BWA alignment for the specified text
        /// </summary>
        /// <param name="iSearch">The InexactSearch instance - this is for avoiding creation of a new one which will result a new index.</param>
        /// <param name="text">The text.</param>
        /// <returns>
        /// The alignment results
        /// </returns>
        private async Task<InexactSearch.Results> PerformBwaAlignment( string text)
        {
            InexactSearch.Results ret = null;
            await Task.Run(() =>
                {
                    ret = this._seqLogics.PerformBwaAlignment(text, (int)this.nupErrorsAllowed.Value);
                });
            return ret;
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
        /// Handles the TextChanged event of the txbReference control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txbReference_TextChanged(object sender, EventArgs e)
        {            
            this._seqLogics.Reference = this.txbReference.Text;
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
            if (this.txbReference.Text.Length >0 && illigalLetters.Count > 0)
            {
                MessageBox.Show("The letters '" + String.Join(",", illigalLetters) + "' Do not appear in reference and are not valid");
            }
            this.txbSearch.Text = String.Join(String.Empty, this.txbSearch.Text.Where(c=> !filterFunc(c)).ToList());

            this.lblSearch.Text = String.Format("String to search ({0})", this.txbSearch.Text.Length);
            await this.SetNumberOfStrignsToSearch();
        }

        private async Task SetNumberOfStrignsToSearch()
        {
            long singleSearchCount = -1;
            long multiSearchCount = -1;
            await Task.Run(() =>
                {
                    var alphBetSize = this._seqLogics.iSearch.ALPHA_BET_LETTERS.Count;
                    var handleGaps = this.chbFindGaps.Checked;
                    singleSearchCount = this._seqLogics.GetNumberOfStringsTosearch(this.txbSearch.Text.Length, alphBetSize, handleGaps,(int)this.nupErrorsAllowed.Value);

                    var errorAllowed_multi = (int)Math.Round(this.nupErrorPercentage.Value / 100M * this.nupReadLength.Value);
                    multiSearchCount =
                        this._seqLogics.GetNumberOfStringsTosearch((int)this.nupReadLength.Value, alphBetSize, handleGaps, errorAllowed_multi)
                        * (long)this.nupNumberOfReads.Value;

                });
            this.nupCountGeneratedStrings_Single.Value = singleSearchCount;
            this.nupCountGeneratedStrings_Multi.Value = multiSearchCount;
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
        #endregion
    }
}
