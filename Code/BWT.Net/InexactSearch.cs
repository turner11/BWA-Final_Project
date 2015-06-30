//#define NO_OPT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.Timers;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BWT
{
    [Serializable]
    public class InexactSearch 
    {
        

        BwtLogics _bwtLogics;
        BwtLogics.BwtResults _bwtResults;
        BwtLogics.BwtResults _bwtReverseResults;
        /// <summary>
        /// The reference
        /// </summary>
        public readonly string X_Reference;
        /// <summary>
        /// Determines weather this instance will try to find gap errors. 
        /// </summary>
        /// <remarks>
        /// In general the algorithm should, but in order to get more intuitive and clear results for debugging, we might want to shut off this option.
        /// </remarks>
        public bool FindGapError;

        int[] D;

        public readonly IReadOnlyCollection<char> ALPHA_BET_LETTERS;

        readonly int MIN_CACH_INDEX ;

        //do not use! this is just for serialization
        private InexactSearch()
        {

        }
        public InexactSearch(string reference, bool findGapErrors, BackgroundWorker bw)
        {
            this._bwtLogics = new BwtLogics();
            this.X_Reference = reference + BwtLogics.END_OF_FILE_CHAR;

            this.FindGapError = findGapErrors;

            /*Get BWt for the reference*/
            Stopwatch sw = new Stopwatch();
            
            bw.ReportProgress(0,"Starting Index");
            sw.Start();
            this._bwtResults = this._bwtLogics.Bwt(X_Reference);
            sw.Stop();

            this._o_cache = new Dictionary<Tuple<int, char>, int>(O_CACHE_SIZE);
            this._oT_cache = new Dictionary<Tuple<int, char>, int>(O_CACHE_SIZE);
            MIN_CACH_INDEX = (int)Math.Sqrt(this._bwtResults.BwtString.Length);



            bw.ReportProgress(0, "Index Took: "+sw.Elapsed.ToString());

            /*Get Bwt for reverse reference*/
            char[] chars = this.X_Reference.Replace(BwtLogics.END_OF_FILE_CHAR.ToString(), String.Empty).ToCharArray();
            Array.Reverse(chars);
            string reverseReference = new String(chars);
            sw.Reset();
            bw.ReportProgress(0, "Starting Reverse index.");
            sw.Start();
            this._bwtReverseResults = this._bwtLogics.Bwt(reverseReference);
            sw.Stop();
            bw.ReportProgress(0, "Reverse Index Took: " + sw.Elapsed.ToString());

            /*Define the alphabet*/
            var letters = X_Reference.Substring(0, X_Reference.Length - 1).Distinct().ToList();
            letters.Sort(); //this is just for easier code comparing.
            this.ALPHA_BET_LETTERS = letters.AsReadOnly();//new List<char> { 'A', 'C', 'G', 'T' }.AsReadOnly();

            //TODO: Calculate array C(·) and O(·,·) from _bwtResults.BwtString (work that can be done in before run time)
            //TODO: Calculate array O'(·,·) from _bwtReverseResults.BwtString  (work that can be done in before run time)

        }

        /// <summary>
        /// Get the index of the specified string using an inexact search
        /// </summary>
        /// <param name="w_stringToMatch">The string to match (e.g. DNA Sample).</param>
        /// <param name="errorsAllowed">The maximum number of errors allowed per sample.</param>
        /// <returns>The <see cref="Results"/> object containing full results for the inexact search</returns>
        public Results GetIndex(string w_stringToMatch, int errorsAllowed)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            this.D = this.CalculateD2(w_stringToMatch);
            
            var indexes = GetIndexRecursive(w_stringToMatch, w_stringToMatch.Length - 1, errorsAllowed, 0, this.X_Reference.Length - 1).ToArray();
            sw.Stop();
            sw.Start();
            return new Results(indexes, this._bwtResults.SuffixTable, this._bwtResults.BwtString, w_stringToMatch, errorsAllowed, this.FindGapError, sw.Elapsed);
        }

        /// <summary>
        /// The recursive function for getting the index of the specified string using an inexact search.
        /// </summary>
        /// <param name="w_stringToMatch">The string to match (e.g. DNA Sample).</param>
        /// <param name="i">The index in suffix array.</param>
        /// <param name="errorsAllowed">The maximum number of errors allowed per sample.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <returns>The indexes that the <paramref name="w_stringToMatch"/> is located at, bounded to the number of errors allowed</returns>
        private IEnumerable<int> GetIndexRecursive(string w_stringToMatch, int i, int errorsAllowed, int lowerBound, int upperBound)
        {
            if (errorsAllowed < 0)
                return new List<int>();
            if (i < 0)
            {
                int startIndex = lowerBound;
                int count = Math.Max(0, upperBound - lowerBound + 1);
                var range = Enumerable.Range(lowerBound, count).ToList();
                return range;
            }

            /*The bounds to return*/
            IEnumerable<int> I = new List<int>();

            if (this.FindGapError)
            {
                /*Find matches with gaps*/
                var boundsWithLessErrors = this.GetIndexRecursive(w_stringToMatch, i - 1, errorsAllowed - 1, lowerBound, upperBound).ToList();
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
                            this.GetIndexRecursive(w_stringToMatch, i, errorsAllowed - 1, temp_lowerBound, temp_upperBound);
                        I = I.Union(boundsInNewBoundeariesWithLessErrors);
                    }

                    if (letter == w_stringToMatch[i])
                    {
                        //current letter is a match, we go on into deeper recursion with same amount of errors allowed
                        var deepperRecursiveBoundaries =
                        this.GetIndexRecursive(w_stringToMatch, i - 1, errorsAllowed, temp_lowerBound, temp_upperBound);
                        I = I.Union(deepperRecursiveBoundaries);
                        var a = String.Join(",", I.ToList());
                    }
                    else
                    {
                        //we found an error, we reduce the number of allowed errors in next iteration
                        var deepperRecursiveWithLessErrorsBoundaries =
                       this.GetIndexRecursive(w_stringToMatch, i - 1, errorsAllowed - 1, temp_lowerBound, temp_upperBound).ToList();
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

        Dictionary<Tuple<int, char>, int> _o_cache;
        /// <summary>
        /// Gets the number of occurrences of a in B[0,i]
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of occurrences of a in B[0,i]</returns>
        private int O(char a, int i)
        {
#if NO_OPT
            string str1 = this._bwtResults.BwtString;
            return O_Base(a, i, str1);

#else


            var key = new Tuple<int,char>(i,a);
            if (_o_cache.ContainsKey(key ))
            {
                return _o_cache[key];
            }
            else
            {
                string str = this._bwtResults.BwtString;
                var val =  O_Base(a, i, str);
                if (_o_cache.Keys.Count < O_CACHE_SIZE && i > MIN_CACH_INDEX)
                {
                    return _o_cache[key] = val;
                }
                return val;
            }
#endif
        }
        const int O_CACHE_SIZE = 100;
        Dictionary<Tuple<int, char>, int> _oT_cache ;
        /// <summary>
        /// Gets the number of occurrences of a in BT[0,i]
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of occurrences of a in B[0,i]</returns>
        private int OT(char a, int i)
        {
#if NO_OPT
            string str1 = this._bwtReverseResults.BwtString;
            return  O_Base(a, i, str1);
#else 

            var key = new Tuple<int, char>(i, a);
            if (_oT_cache.ContainsKey(key))
            {
                return _oT_cache[key];
            }
            else
            {
                string str = this._bwtReverseResults.BwtString;
                var val = O_Base(a, i, str);
                if (_oT_cache.Keys.Count < O_CACHE_SIZE && i > MIN_CACH_INDEX)
                {
                    return _oT_cache[key] = val;
                }
                return val;
            }
            
#endif
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
            public static bool GenerateShallowResults;

            public int[] IndexesSa { get; private set; }
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
                this.IndexesSa = indexes;

                bool shallow = Results.GenerateShallowResults;


                this.SuffixArray = shallow? new DataTable(): suffixArray;
                this.BwtString = bwtString;
                this.StringToMatch = stringToMatch;
                this.ErrorAllowed = errorAllowed;
                this.HandleGapError = handleGapError;
                this.TimeElapsed = timeElapes;
                this.Indexes = shallow? new int[0]: indexes.Select(i => saIndexToReferenceIndex(i)).OrderBy(i=>i).ToArray();
                
                if (!Results.GenerateShallowResults)
                {
                    Array.Sort(this.IndexesSa);
                }
            }

            private int saIndexToReferenceIndex(int saIndex)
            {
                var currSaRow = this.SuffixArray.Rows[saIndex];
                var rowString = String.Join("", currSaRow.ItemArray);
                var index = (rowString.Length - 1) - rowString.IndexOf(BwtLogics.END_OF_FILE_CHAR); //-1 1 is because length is 1-seed
                return index;
                //var fromRef = new string(this._slogics.Reference.Skip(index).Take(seq.Length).ToArray());
            }
            /// <summary>
            /// Gets the summary message for the BWA algorithm results.
            /// </summary>
            /// <returns></returns>
            public string GetSummaryMessage()
            {


                string indexedTable = InexactSearch.GetIndexedSuffixArray(this.SuffixArray); 

                string msg = String.Format("The query:{0}" +
                                           "\t'{1}' with {2} errors allowed ({3}){0} " +
                                           "The results:{0}" +
                                            "\tIndexes: {4}{0}{0}" +
                                            "\tSA Indexes: {5}{0}{0}" +
                                           "\tQuery took: {6}{0}" +
                                           "\t({7} sec/char){0}",
                                           Environment.NewLine,
                                           this.StringToMatch,
                                           this.ErrorAllowed,
                                           (this.HandleGapError ? "With" : "NO") + " Gap handling",
                                           String.Join(",", this.Indexes),
                                           String.Join(",", this.IndexesSa),
                                           this.TimeElapsed.ToString(),
                                           this.SecondsPerCharachtar);

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

       

        public string Serialize()
        {
            string  ret = null;
            try
            {
                using (var stream = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    byte[] bArr = stream.ToArray();
                    ret = System.Convert.ToBase64String(bArr);
                    
                }
                
            }
            catch (Exception)
            {

                ret = null;
            }
            return ret;
            
        }


        public static InexactSearch DeSerialize(string strSerialized)
        {
            byte[] serialized = System.Convert.FromBase64String(strSerialized);
        
            InexactSearch ret = null;
            try
            {
                var formatter = new BinaryFormatter();
                using (var stream = new MemoryStream(serialized))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    ret = formatter.Deserialize(stream) as InexactSearch;
                }
                
                    
            }
            catch (Exception)
            {

                ret = null;
            }
            return ret;

        }


         public string GetIndexedSuffixArray()
        {
            return InexactSearch.GetIndexedSuffixArray(this._bwtResults.SuffixTable);
        }

        internal static string GetIndexedSuffixArray(DataTable suffixArray)
        {
            string[] indexedTableRows =
                     suffixArray.GetJoinedTable().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < indexedTableRows.Length; i++)
            {
                indexedTableRows[i] = "[" + (i) + "] " + indexedTableRows[i];
            }
            string indexedTable = String.Join(Environment.NewLine, indexedTableRows);
            return indexedTable;
        }
    }
}
