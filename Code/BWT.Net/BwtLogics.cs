using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace BWT
{
    [Serializable]
    internal class BwtLogics
    {
        #region Data members
        /// <summary>
        /// The end of file character
        /// </summary>
        public const char END_OF_FILE_CHAR = '$';

        /// <summary>
        /// The number of elements to take for hash of cached results
        /// </summary>
        private const int NUMBER_OF_ELEMENTS_FOR_HASH = 500;

        /// <summary>
        /// Gets or sets a value indicating whether speed is valued over reports during work.
        /// </summary>
        /// <value>
        ///   <c>true</c> if speed should be valued over reports during work ; otherwise, <c>false</c>.
        /// </value>
        public bool SpeedOverReports { get; set; } 
        #endregion

        

       
        #region Public methods
        /// <summary>
        /// Performs the Burrows–Wheeler transform on the specified input.
        /// </summary>
        /// <param name="input">The input to perform the transform on.</param>
        /// <returns>The text after the transform</returns>
        public BwtResults Bwt(string input)
        {   
            this.ThrowExceptionIfInputNotValid(input);

            string sterileInput = this.SterilizeInput(input);


            var table = GetPopulatedBwtDataTable(sterileInput);

            int lastColumnIndex = table.Columns.Count - 1;
            string sortStr = this.GetSortString(table);
            table.DefaultView.Sort = sortStr;

            var sortedTable = table.DefaultView.ToTable();
            string[] outputChars = sortedTable.AsEnumerable().Select(row => row[lastColumnIndex].ToString()).ToArray();
            string output = String.Concat(outputChars);

            BwtResults results = new BwtResults(input, table, sortedTable, output);


            return results;

        }

        
        /// <summary>
        /// Performs the reverse Burrows–Wheeler transform on the specified input.
        /// <remarks>
        /// Input should be an output of BWT
        /// </remarks>
        /// </summary>
        /// <param name="transformedInput">The input to perform the reverse transform on.</param>
        /// <param name="bw">The <see cref="BackgroundWorker"/> for reporting progress.</param>
        /// <returns>
        /// The text after the reverse transform
        /// </returns>
        public unsafe string ReverseBwt(string transformedInput, BackgroundWorker bw)
        {
            BackgroundWorker worker = bw ?? new BackgroundWorker();

            /*http://en.wikipedia.org/wiki/Burrows%E2%80%93Wheeler_transform#Explanation
             * The inverse can be understood this way. 
             * Take the final table in the BWT algorithm, and erase all but the last column. 
             * Given only this information, you can easily reconstruct the first column.             
             */

            DataTable dt = this.GetEmptyBwtDataTable(transformedInput.Length);

            /*The last column IS THE transformed input*/
            dt.PopulateTableLastColumn(transformedInput);

            /*The last column tells you all the characters in the text, 
            so just sort these characters alphabetically to get the first column. */

            List<char> allChars = transformedInput.ToList();
            allChars.Sort();
            var sortedInput = new StringBuilder().Append(allChars.ToArray()).ToString();



            for (int i = 0; i < transformedInput.Length; i++)
            {
                dt.ShiftColumnsData();
                dt.PopulateTableFirstColumn(transformedInput);

                /* Then, the first and last columns (of each row) together give you all pairs of successive characters in the document, 
                 * where pairs are taken cyclically so that the last and first character form a pair. */

                /* Continuing in this manner, you can reconstruct the entire list. */

                /*SIn First iteration sorting the list of pairs gives the first and second columns. */
                //NOTE: This part is Evil. to much memory allocation an time consuming.
                //Probably should use list of lists with fixed size instead of Data table in order to avoid the reallocation
                /*dt.DefaultView.Sort = dt.Columns[0].ColumnName + " ASC";
                dt = dt.DefaultView.ToTable();*/
                dt = dt.AsEnumerable().OrderBy(r => r[0]).CopyToDataTable();


                string currJoinedTable = this.SpeedOverReports ? String.Empty : dt.GetJoinedTable();
                int percentage = (int)(100 * ((i + 1) / (double)transformedInput.Length));
                worker.ReportProgress(percentage, currJoinedTable);

            }

            string joinedTable = dt.GetJoinedTable();
            string[] joinedTableRows = joinedTable.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            /*the row with the "end of file" character at the end is the original text.*/
            string originalInput = joinedTableRows.FirstOrDefault(s => s.EndsWith(BwtLogics.END_OF_FILE_CHAR.ToString()));
            //remove EOF            
            return originalInput.Substring(0, originalInput.Length - 1);

            #region Old Ideas...
            //var pairs = dt.GetCyclicSequence(i+2);
            //StringComparer comparer1 = StringComparer.Ordinal;

            /*Sorting the list of pairs gives the first and second columns. */
            //pairs.Sort(comparer1);

            /*string lastCOl = dt.GetJoinedLastColumn();
            string firstCOl = dt.GetJoinedColumn(0);
            string joinedTable2 = dt.GetJoinedTable();*/

            #endregion

        } 
        #endregion

        #region Private methods
        /// <summary>
        /// Gets the sort string for sorting entire table according to for BWT.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private unsafe string GetSortString(DataTable table)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                string currAddition = String.Format("{0}, ", table.Columns[i].ColumnName);
                sb.Append(currAddition);
            }
            //removing last comma
            sb.Remove(sb.Length - 2, 1);
            sb.Append("ASC");
            var sortStr = sb.ToString();
            return sortStr;

        }

        /// <summary>
        /// Gets the populated BWT data table based on specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private unsafe DataTable GetPopulatedBwtDataTable(string input)
        {
            DataTable dt = this.GetEmptyBwtDataTable(input.Length);

            var rotations = this.GetRotations(input);


            foreach (var currRotation in rotations)
            {
                var newRow = dt.NewRow();
                for (int i = 0; i < currRotation.Length; i++)
                {
                    newRow[i] = currRotation[i];
                }
                dt.Rows.Add(newRow);
                 
            }
            return dt;
        }

        /// <summary>
        /// Gets all the rotations for given input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private unsafe IList<string> GetRotations(string input)
        {
            var rotations = new string[input.Length];
            StringBuilder sb = new StringBuilder(input);
            //adding first rotation (the input itself)
            rotations[0] = sb.ToString();
            int length = input.Length;
            for (int i = 1; i < length; i++)
            {
                char last = sb[length - 1];
                sb.Remove(length - 1, 1);
                sb.Insert(0, last);
                rotations[i]  = sb.ToString();

            }
            return rotations;

        }

        /// <summary>
        /// Gets the empty BWT data table (structure only, no data).
        /// </summary>
        /// <param name="inputLength">Length of the input.</param>
        /// <returns></returns>
        private DataTable GetEmptyBwtDataTable(int inputLength)
        {
            DataTable dt = new DataTable("BWT");
            for (int i = 0; i < inputLength; i++)
            {
                dt.Columns.Add(String.Format("Column #{0}", i + 1), typeof(char));
            }
            return dt;
        }

        /// <summary>
        /// Sterilizes the input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>the Sterilized input</returns>
        private unsafe string SterilizeInput(string input)
        {
            if (input.Last() != END_OF_FILE_CHAR)
            {
                input += END_OF_FILE_CHAR;
            }
            return input;
        }

        /// <summary>
        /// Throws exception if input not valid.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <exception cref="System.ArgumentException">Input EOF was not valid</exception>
        private void ThrowExceptionIfInputNotValid(string input)
        {
            bool valid = ValidateInputEof(input);
            if (!valid)
            {
                throw new ArgumentException("Input EOF was not valid");
            }
        }

        /// <summary>
        /// Validates the input is appropriate.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private bool ValidateInputEof(string input)
        {
            int eofCount = input.Count(c => c == END_OF_FILE_CHAR);
            bool valid = eofCount == 0
                       || (eofCount == 1 && input.Last() == END_OF_FILE_CHAR);
            return valid;
        } 
        #endregion


        /// <summary>
        /// A wrapperclass for the BWT results
        /// </summary>
        [Serializable]
        public class BwtResults
        {
            public readonly string OriginalText;
            public readonly DataTable RotationTable;
            public readonly DataTable SuffixTable;
            public readonly string BwtString;

            public BwtResults(string originalText, DataTable rotationTable, DataTable suffixTable,string bwtString)
            {
                this.OriginalText = originalText;
                this.RotationTable = rotationTable;
                this.SuffixTable = suffixTable;
                this.BwtString = bwtString;
            }
            
        }
    }
}
