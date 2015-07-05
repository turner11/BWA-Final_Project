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
    public partial class frmBwa
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


        private async Task SetNumberOfStrignsToSearch()
        {
            long singleSearchCount = -1;
            long multiSearchCount = -1;
            await Task.Run(() =>
                {
                    var alphBetSize = this._seqLogics.iSearch.ALPHA_BET_LETTERS.Count;
                    var handleGaps = this.chbFindGaps.Checked;
                    singleSearchCount = this._seqLogics.GetNumberOfStringsTosearch(this.txbSearch.Text.Length, alphBetSize, handleGaps,(int)this.nupErrorsAllowed.Value);

                    
                    multiSearchCount =
                        this._seqLogics.GetNumberOfStringsTosearch((int)this.nupReadLength.Value, alphBetSize, handleGaps, (int)this.nupErrorsAllowed.Value)
                        * (long)this.nupNumberOfReads.Value;

                });
            this.nupCountGeneratedStrings_Single.Value = singleSearchCount;
            this.nupCountGeneratedStrings_Multi.Value = multiSearchCount;
        }

       
     
    }
}
