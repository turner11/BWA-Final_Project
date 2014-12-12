using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BWT
{
    public class InexactSearch
    {
        BwtLogics _bwtLogics;
        BwtLogics.BwtResults _bwtResults;
        BwtLogics.BwtResults _bwtReverseResults;
        /// <summary>
        /// The reference
        /// </summary>
        readonly string X_Reference;
        /// <summary>
        /// Determines weather this instance will try to find gap errors. 
        /// </summary>
        /// <remarks>
        /// In general the algorithem should, but in order to get more ituitive and clear results for debugging, we might want to shut off this option.
        /// </remarks>
        public readonly bool FindGapError;

        int[] D;

        public readonly IReadOnlyCollection<char> ALPHA_BET_LETTERS;

        public InexactSearch(string reference, bool findGapErrors)
        {
            this._bwtLogics = new BwtLogics();
            this.X_Reference = reference + BwtLogics.END_OF_FILE_CHAR;

            this.FindGapError = findGapErrors;

            /*Get BWt for the reference*/
            this._bwtResults = this._bwtLogics.Bwt(X_Reference);

            /*Get Bwt for reverse reference*/
            char[] chars = this.X_Reference.Replace(BwtLogics.END_OF_FILE_CHAR.ToString(), String.Empty).ToCharArray();
            Array.Reverse(chars);
            string reverseReference = new String(chars);
            this._bwtReverseResults = this._bwtLogics.Bwt(reverseReference);

            /*Define the alphbet*/
            var letters = X_Reference.Substring(0, X_Reference.Length - 1).Distinct().ToList();
            letters.Sort(); //this is just for easier code comparing.
            this.ALPHA_BET_LETTERS = letters.AsReadOnly();//new List<char> { 'A', 'C', 'G', 'T' }.AsReadOnly();

            //TODO: Calculate array C(·) and O(·,·) from _bwtResults.BwtString (work that can be done in before run time)
            //TODO: Calculate array O'(·,·) from _bwtReverseResults.BwtString  (work that can be done in before run time)

        }
        
        /// <summary>
        /// Get the index of the specified string using an inexact search
        /// </summary>
        /// <param name="w_stringToMatch"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Results GetIndex(string w_stringToMatch, int errorsAlloed)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            this.D = this.CalculateD2(w_stringToMatch);
            
            var indexes = GetIndexRecursive(w_stringToMatch, w_stringToMatch.Length - 1, errorsAlloed, 0, this.X_Reference.Length - 1).ToArray();
            sw.Stop();
            sw.Start();
            return new Results(indexes, this._bwtResults.SuffixTable, this._bwtResults.BwtString, w_stringToMatch, errorsAlloed, this.FindGapError, sw.Elapsed);
        }

        private IEnumerable<int> GetIndexRecursive(string w_stringToMatch, int i, int errorsAlloed, int lowerBound, int upperBound)
        {
            if (errorsAlloed < 0)
                return new List<int>();
            if (i < 0)
            {
                int startIndex = lowerBound;
                int count = Math.Max(0, upperBound - lowerBound + 1);
                var range = Enumerable.Range(lowerBound, count).ToList();
                return range;
            }



            /*The bounds to retunr*/
            IEnumerable<int> I = new List<int>();

            if (this.FindGapError)
            {
                /*Find matches with less errors (gaps?)*/
                var boundsWithLessErrors = this.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed - 1, lowerBound, upperBound).ToList();
                I = I.Union(boundsWithLessErrors);//Remove?
            }


            foreach (var letter in this.ALPHA_BET_LETTERS)
            {

                int c_index = this.C(letter);
                var temp_lowerBound = c_index + this.O(letter, lowerBound - 1) + 1;
                var temp_upperBound = c_index + this.O(letter, upperBound);

                bool isLegalBounds = lowerBound <= upperBound;
                if (isLegalBounds)
                {
                    if (this.FindGapError)
                    {
                        var boundsInNewBoundeariesWithLessErrors =
                            this.GetIndexRecursive(w_stringToMatch, i, errorsAlloed - 1, temp_lowerBound, temp_upperBound);
                        I = I.Union(boundsInNewBoundeariesWithLessErrors);
                    }

                    if (letter == w_stringToMatch[i])
                    {
                        //current leeter is a match, we go on into deeper recursion with same ampunt of eerrors allowes
                        var deepperRecursiveBoundaries =
                        this.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed, temp_lowerBound, temp_upperBound);
                        I = I.Union(deepperRecursiveBoundaries);
                        var a = String.Join(",", I.ToList());
                    }
                    else
                    {
                        //we found an error, we reduce the number of allowed errors in next iteration
                        var deepperRecursiveWithLessErrorsBoundaries =
                       this.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed - 1, temp_lowerBound, temp_upperBound).ToList();
                        I = I.Union(deepperRecursiveWithLessErrorsBoundaries);
                    }
                }
            }



            return I;
        }

        private int[] CalculateD(string w)
        {
            int[] d = new int[w.Length];
            int z = 0;
            int j = 0;
            for (int i = 0; i < w.Length; i++)
            {
                string substring = w.Substring(j, i);
                bool isSubstring = this.X_Reference.IndexOf(substring) >= 0;
                if (!isSubstring)
                {
                    z = z + 1;
                    j = i + 1;
                }
                d[i] = z;


            }
            return d;
        }


        private int[] CalculateD2(string w)
        {
            int[] d = new int[w.Length];

            int k = 1;
            int l = this.X_Reference.Length - 1;
            int z = 0;

            for (int i = 0; i < w.Length; i++)
            {
                var currChar = w[i]; /*a*/
                var charAsString = currChar.ToString();
                k = this.C(currChar) + this.OT(currChar, k - 1) + 1;//this.GetBottomRowIndex(charAsString);
                l = this.C(currChar) + this.OT(currChar, l);

                if (k > l)
                {
                    k = l;
                    l = this.X_Reference.Length - 1;
                    z++;
                }
                d[i] = z;


            }
            return d;
        }

        /// <summary>
        /// Gets the number of symbols in X[0,n−2] that are lexicographically 
        /// smaller than a
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of symbols in X[0,n−2] that are lexicographically 
        /// smaller than a</returns>
        private int C(char a)
        {
            int c = /*C(a)*/this.X_Reference.Take(this.X_Reference.Length - 1)
                                                  .Count(x => x < a);
            return c;
        }


        /// <summary>
        /// Gets the number of occurrences of a in B[0,i]
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of occurrences of a in B[0,i]</returns>
        private int O(char a, int i)
        {
            string str = this._bwtResults.BwtString;
            return O_Base(a, i, str);
        }

        /// <summary>
        /// Gets the number of occurrences of a in BT[0,i]
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of occurrences of a in B[0,i]</returns>
        private int OT(char a, int i)
        {
            string str = this._bwtReverseResults.BwtString;
            return O_Base(a, i, str);
        }
        /// <summary>
        /// Gets the number of occurrences of a in specified string
        /// </summary>
        /// <param name="a">the char to find it's occurences</param>
        /// <param name="i">the index to look up to</param>
        /// <param name="str">the str4ing to look in</param>
        /// <returns>number of occurrences of a in specified string</returns>
        private static int O_Base(char a, int i, string str)
        {
            int c = /*O(a,i)*/str.Take(i + 1).Count(x => x == a);
            return c;
        }


        private static int GetBottomRowIndex2(string w)
        {

            throw new NotImplementedException();

        }


        private static int GetTopRowIndex2(string w)
        {
            throw new NotImplementedException();

        }






        private int GetRowIndex(string w, RowBoundaries boundary)
        {
            if (w == string.Empty)
                return boundary == RowBoundaries.Bottom ? 1 : this.X_Reference.Length - 1;
            //the prefiex
            char a = w[0];

            string trimmed_W = w.Substring(1);
            int trimmedIndex = this.GetRowIndex(trimmed_W, boundary);

            int innerOffset = boundary == RowBoundaries.Bottom ? -1 : 0;
            int outerOffset = boundary == RowBoundaries.Bottom ? 1 : 0;

            int index = this.C(a) + this.O(a, trimmedIndex + innerOffset) + outerOffset;
            return index;
        }

        enum RowBoundaries
        {
            Top,
            Bottom
        }


        public class Results
        {
            public int[] Indexes { get; private set; }
            public DataTable SuffixArray { get; private set; }
            public string BwtString { get; private set; }
            public string StringToMatch { get; private set; }
            public int ErrorAllowed { get; private set; }
            public bool HandleGapError { get; private set; }
            public TimeSpan TimeElapsed { get; private set; }
            public double SecondsPerCharachtar
            {
                get
                {
                    return this.TimeElapsed.TotalSeconds / this.StringToMatch.Length;
                }
            }


            public Results(int[] indexes, DataTable suffixArray, string bwtString, string stringToMatch, int errorAllowed, bool handleGapError, TimeSpan timeElapes)
            {
                this.Indexes = indexes;
                Array.Sort(this.Indexes);
                this.SuffixArray = suffixArray;
                this.BwtString = bwtString;
                this.StringToMatch = stringToMatch;
                this.ErrorAllowed = errorAllowed;
                this.HandleGapError = handleGapError;
                this.TimeElapsed = timeElapes;
            }
            /// <summary>
            /// Gets the summary message for the BWA algorithm results.
            /// </summary>
            /// <returns></returns>
            public string GetSummaryMessage()
            {
                string[] indexedTableRows =
                    this.SuffixArray.GetJoinedTable().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < indexedTableRows.Length; i++)
                {
                    indexedTableRows[i] = "[" + (i) + "] " + indexedTableRows[i];
                }
                string indexedTable = String.Join(Environment.NewLine, indexedTableRows);

                string msg = String.Format("The query:{0}" +
                                           "\t'{1}' with {2} errors allowed ({3}){0} " +
                                           "The results:{0}" +
                                            "\tIndexes: {4}{0}{0}" +
                                           "\tQuery took: {5}{0}" +
                                           "\t({6} sec/char){0}" +
                                           "The suffix array: {0}{0}" +
                                           "{7}{0}{0}",
                                           Environment.NewLine,
                                           this.StringToMatch,
                                           this.ErrorAllowed,
                                           (this.HandleGapError ? "With" : "NO") + " Gap handling",
                                           String.Join(",", this.Indexes),
                                           this.TimeElapsed.ToString(),
                                           this.SecondsPerCharachtar,
                                           indexedTable);

                return msg;
            }
        }

        const decimal READ_ERRROR_AVERAGE = 0.02M;
        const int MAX_ERROR_THRESHOLD = 4;
        internal static int GetCalculatedMaxError(int maxReadLength)
        {
            return InexactSearch.GetCalculatedMaxError(maxReadLength, READ_ERRROR_AVERAGE, MAX_ERROR_THRESHOLD);
        }

        private static int GetCalculatedMaxError(int maxReadLength, decimal errorAvg, int threshold)
        {
            double elambda = Math.Exp((double)(-maxReadLength * errorAvg));
            double sum = elambda;
            decimal  y = 1;
            int x = 1;
            for (int k = 1; k < 1000; k++)
            {
                y *= maxReadLength * errorAvg;
                x *= k;
                sum += elambda * (double)y / x;
                System.Diagnostics.Debug.WriteLine("Sum = "+ sum);
                if(1.0-sum < threshold) 
                    return k;
            }
            return 2;
        }
    }
}
