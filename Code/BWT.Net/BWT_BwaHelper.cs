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


        /// <summary>
        /// The letters 
        /// </summary>
        List<char> _referenceLetters;
        List<char> ReferenceLetters
        {
            get
            {
                if(this._referenceLetters == null || this._referenceLetters.FirstOrDefault() == 0)
                {
                    this._referenceLetters = this.txbReference.Text.Distinct().ToList();
                }
                return this._referenceLetters;
            }
        }
                
        #region Event Handlers
        /// <summary>
        /// Handles the Click event of the btnInexactSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInexactSearch_Click(object sender, EventArgs e)
        {
            //clear history
            this.txbBwaResults.Text = String.Empty;
            this.SaveSettings();
            


            var results = this.PerformBwaAlignment();
           
            this.txbBwaResults.Text = results.GetSummaryMessage();

        }

        /// <summary>
        /// Performs the BWA alignment.
        /// </summary>
        /// <returns>The alignment results</returns>
        private InexactSearch.Results PerformBwaAlignment()
        {
            InexactSearch iSearch = new InexactSearch(this.txbReference.Text, this.chbFindGaps.Checked);
            return PerformBwaAlignment(iSearch);
        }

        /// <summary>
        /// Performs the BWA alignment.
        /// </summary>
        /// <param name="iSearch">The InexactSearch instance - this is for avoiding creation of a new one which will result a new index.</param>
        /// <returns>The alignment results</returns>
        private InexactSearch.Results PerformBwaAlignment(InexactSearch iSearch)
        {
            var results = iSearch.GetIndex(this.txbSearch.Text, (int)this.nupErrorsAllowed.Value);
            return results;
        }

        /// <summary>
        /// Handles the KeyPress event of the txbSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs" /> instance containing the event data.</param>
        private void txbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Check for a naughty character in the KeyDown event.
            if (!this.ReferenceLetters.Contains(e.KeyChar) && e.KeyChar != '\b')
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
            this._referenceLetters = null;

            if (this.txbReferenceMirror.Text != this.txbReference.Text)
            {
                this.txbReferenceMirror.Text = this.txbReference.Text;
            }

        }

        /// <summary>
        /// Handles the TextChanged event of the txbSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txbSearch_TextChanged(object sender, EventArgs e)
        {
            Func<char, bool> filterFunc = c => !this.ReferenceLetters.Contains(c);
            var illigalLetters = this.txbSearch.Text.Where(filterFunc).ToList();
            if (illigalLetters.Count > 0)
            {
                MessageBox.Show("The letters '" + String.Join(",", illigalLetters) + "' Do not appear in reference and are not valid");
            }
            this.txbSearch.Text = String.Join(String.Empty, this.txbSearch.Text.Where(filterFunc).ToList());
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
        /// Handles the TextChanged event of the txbReferenceMirror control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txbReferenceMirror_TextChanged(object sender, EventArgs e)
        {
            if (this.txbReference.Text != this.txbReferenceMirror.Text)
            {
                this.txbReference.Text = this.txbReferenceMirror.Text;
            }
        }

        /// <summary>
        /// Handles the ValueChanged event of the nupReadLength control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void nupReadLength_ValueChanged(object sender, EventArgs e)
        {
            int maxDiff = InexactSearch.GetCalculatedMaxError((int)this.nupReadLength.Value);
            this.lblRecommendedMaxError.Text = String.Format("Calculated Max Error: {0}", maxDiff);
        }
        #endregion
    }
}
