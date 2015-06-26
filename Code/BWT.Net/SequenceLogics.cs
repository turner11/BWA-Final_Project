using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWT
{
    public class SequenceLogics
    {
        public event EventHandler PreAlignmnet;
        #region Data Members
       
        BackgroundWorker worker;
        public InexactSearch iSearch { get; private set; }

        public int DegreeOfParallelism { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string Reference
        {
            get { return this.iSearch != null ? iSearch.X_Reference : String.Empty; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    this.iSearch = null;
                }
                else if (this.Reference != value)
                {
                    this.iSearch = new InexactSearch(value, true, worker);
                }
            }
        }



        /// <summary>
        /// The letters 
        /// </summary>
        List<char> _referenceLetters;
        /// <summary>
        /// Gets the reference  legal letters.
        /// </summary>
        /// <value>
        /// The reference legal letters.
        /// </value>
        public List<char> ReferenceLetters
        {
            get
            {
                if (this._referenceLetters == null || this._referenceLetters.FirstOrDefault() == 0)
                {
                    this._referenceLetters = (this.Reference ?? "").Where(c=> c!= BwtLogics.END_OF_FILE_CHAR).Distinct().ToList();

                }
                return this._referenceLetters;
            }
        }
        /// <summary>
        /// Gets the length of the reference.
        /// </summary>
        /// <value>
        /// The length of the reference.
        /// </value>
        public int ReferenceLength
        {
            get
            {
                return this.Reference.Length;
            }
        }

        public bool FindGapgs
        {
            get
            {
                return this.iSearch != null ?
                    this.iSearch.FindGapError : false;
            }
            set
            {
                if ( this.iSearch != null )
                {
                    this.iSearch.FindGapError =value;
                }
            }
        } 
        #endregion

        #region C'tor
        public SequenceLogics() : this(string.Empty, null) { }
        public SequenceLogics(string reference, BackgroundWorker bw)
        {
            this.worker = bw ?? new BackgroundWorker() { WorkerReportsProgress = true };
            this.Reference = reference;
        } 
        #endregion

        /// <summary>
        /// Gets a list of random reads.
        /// </summary>
        /// <param name="readCount">The read count to generate.</param>
        /// <param name="readLength">Length of each read.</param>
        /// <param name="errorPercentage">The error percentage expectancy for each read.</param>
        /// <returns>the list of reads</returns>
        public List<string> GetRandomReads(int readCount, int readLength, double errorPercentage)
        {
            //var reference = this.Reference.TakeWhile(c=> c != BwtLogics.END_OF_FILE_CHAR).ToArray; //ommit the end of word char
            var rnd = new Random();

            List<int> startIndexes = new List<int>();
            int lastSampleLocation = this.ReferenceLength - readLength -1; //-1 will omit the "end of file char"
            for (int i = 0; i < readCount; i++)
            {
                int currStartIndex = (int)(rnd.NextDouble() * lastSampleLocation);
                startIndexes.Add(currStartIndex);
            }
            startIndexes.Sort();
            //This is for easier debugging
            startIndexes[0] = 0;

            var reads = new List<string>();

            StringBuilder sb = new StringBuilder();
            foreach (int index in startIndexes)
            {
                sb.Clear();
                
                //string originalString = new String(this.Reference.Skip(index).Take(readLength + 1).Where(c=> c!= BwtLogics.END_OF_FILE_CHAR).Take(readLength).ToArray());
                string originalString = this.Reference.Substring(index, readLength);
                for (int i = 0; i < originalString.Length; i++)
                {
                    var rndVal = rnd.NextDouble()*100;
                    bool error = rndVal < errorPercentage;
                    var nextchar = error ?
                        this.ReferenceLetters[i % this.ReferenceLetters.Count] : originalString[i];
                    sb.Append(nextchar);
                }
                var read = sb.ToString();             
                reads.Add(read);

            }
            return reads;
        }

        public void RunMultipleAlignments(int readCount, int readLength, double errorPercentage, int errorsAllowed, BackgroundWorker bwReporter, AlignMode alignMode)
        {
            List<string> reads = this.GetRandomReads(readCount, readLength, errorPercentage);
            this.RunMultipleAlignments(reads, errorsAllowed, bwReporter, alignMode);
        }

        public void RunMultipleAlignments(IList<string> reads, int errorsAllowed, BackgroundWorker bwReporter,AlignMode alignMode)
        {
            bwReporter = bwReporter ?? new BackgroundWorker() { WorkerReportsProgress = true };            

            int parrallelClosureCount = 0;
            Action<int> alignmentAction = (i) =>
            {
                var text = reads[i];
                //this.txbSearch.Text = text;
                var results = this.PerformBwaAlignment(text, errorsAllowed);
                string indexesStr = String.Join(",", results.Indexes);


                var percentage = ((parrallelClosureCount++ + 1.0) / reads.Count) * 100;
                bwReporter.ReportProgress((int)percentage, "results for read #" + i + ": " + indexesStr);
            };




            Action singleThreadAction = () =>
            {
                for (int i = 0; i < reads.Count; i++)
                {
                    var read = reads[i];
                    var results = this.PerformBwaAlignment(read,errorsAllowed);
                    string indexesStr = String.Join(",", results.Indexes);

                    var percentage = ((i + 1.0) / reads.Count) * 100;
                    bwReporter.ReportProgress((int)percentage, "results for read #" + i + ": " + indexesStr);
                }
            };

             
            Action multiThreadAction = () =>
            {
                var po = new ParallelOptions { MaxDegreeOfParallelism = this.DegreeOfParallelism };
                Parallel.For(0, reads.Count,po, (i) =>
                {
                    alignmentAction(i);
                });
                //Parallel.ForEach(reads, alignmentAction);
            };


            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (alignMode == AlignMode.SingleThread)
            {
                singleThreadAction();
            }
            else if (alignMode == AlignMode.MultiThread)
            {
                multiThreadAction();
            }
            sw.Stop();

            bwReporter.ReportProgress(0, Environment.NewLine + String.Format("BWA Elapsed time: {0}", sw.Elapsed.ToString()) + Environment.NewLine);

            if (alignMode.HasFlag(AlignMode.SingleThread) && alignMode.HasFlag(AlignMode.MultiThread))
            {
                sw.Reset();
                sw.Start();
                singleThreadAction();
                sw.Stop();

                bwReporter.ReportProgress(0,
                    Environment.NewLine + String.Format("Elapsed time (single Thread): {0}{1}{1}", sw.Elapsed.ToString(), Environment.NewLine) + Environment.NewLine
                );
                sw.Reset();
                sw.Start();

                multiThreadAction();

                sw.Stop();
                bwReporter.ReportProgress(0,
                     Environment.NewLine +
                    String.Format("Elapsed time (multi Thread): {0}", sw.Elapsed.ToString())
                    + Environment.NewLine
                    );
            }
        }

        public long GetNumberOfStringsTosearch(int seqLength, int alphbetSize, bool handleGap, int errorsAllowd)
        {
            var potentialErrorLocations = PermutationsAndCombinations.nCr(seqLength, errorsAllowd);
            //we relate to a gap as another option to mistake, just outside of the alphabet... 2 is because a letter can be added / omitted
            // -1 is because there is the option that we got the right value...
            var errorOptions = alphbetSize - 1 + (handleGap ? 2:0); 

            var numberOfStringsToSearch = errorOptions * potentialErrorLocations;
            return numberOfStringsToSearch +1;// +1 for the original string...
        }


        internal InexactSearch.Results PerformBwaAlignment(string text, int errorsAllowed)
        {
            if (this.PreAlignmnet != null)
            {
                this.PreAlignmnet(this, EventArgs.Empty);
            }
            var results = this.iSearch.GetIndex(text, errorsAllowed);
            return results; 
        }

        [Flags]
        public enum AlignMode
        {
            SingleThread = 1,
            MultiThread = 2
        }

        private static class PermutationsAndCombinations
        {
            public static long nCr(int n, int r)
            {
                // naive: return Factorial(n) / Factorial(r) * Factorial(n - r);
                return nPr(n, r) / Factorial(r);
            }

            public static long nPr(int n, int r)
            {
                // naive: return Factorial(n) / Factorial(n - r);
                return FactorialDivision(n, n - r);
            }

            private static long FactorialDivision(int topFactorial, int divisorFactorial)
            {
                long result = 1;
                for (int i = topFactorial; i > divisorFactorial; i--)
                    result *= i;
                return result;
            }

            private static long Factorial(int i)
            {
                if (i <= 1)
                    return 1;
                return i * Factorial(i - 1);
            }
        }
    }
}
