using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using System.ComponentModel;


namespace BWT
{

    [TestFixtureAttribute]
    public class TestSuit
    {
        #region Data members
        const string Reference = "ACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCAACGTCGGGCGACCGATAGCGAGGTCA";
        //const string Reference = "bannana";
        //const int SEQ_LENGTH = 5;
        const int SEQ_LENGTH = 35;
        readonly SequenceLogics _slogics;

        const int REFERENCE_LENGTH = 780;
        /// <summary>
        /// Gets the start index of sequence at end of reference.
        /// </summary>        
        const int IndexOfSeqAtEnd = REFERENCE_LENGTH - SEQ_LENGTH;
        /// <summary>
        /// Gets the start index of sequence at end of reference.
        /// </summary>
        const int IndexOfSeqAtMiddle = REFERENCE_LENGTH / 2;

        /// <summary>
        /// The initial state of gap handling
        /// </summary>
        const bool INITIAL_GAP_STATE = true;



        BackgroundWorker bw;
        #endregion

        #region Ctors + Initialize / Cleanup
        public TestSuit()
        {
            this.bw = new BackgroundWorker() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            this._slogics = new SequenceLogics();
            this._slogics.Reference = Reference;
            this._slogics.FindGapgs = INITIAL_GAP_STATE;


            //bw);

        }
        //Use TestInitialize to run code before running each test
        [SetUpAttribute]
        public virtual void MyTestInitialize()
        {

            try
            {
                //allow tests to change without affecting other tests...
                this._slogics.FindGapgs = INITIAL_GAP_STATE;
            }
            catch (Exception ex)
            {
                Assert.Fail("Failed to initialize Test ('MyTestInitialize'): " + Environment.NewLine + ex.ToString());
            }
        }

        //Use TestCleanup to run code after each test has run
        [TearDownAttribute]
        public virtual void MyTestCleanup()
        {
            //GC.Collect();
            //GC.WaitForFullGCComplete();
        }
        #endregion


        /// <summary>
        /// Aligns a sequence.
        /// </summary>
        /// <param name="startIndex">The start index of sequence.</param>
        /// <param name="length">The length of sequence.</param>
        [TestAttribute]
        [TestCase(0, SEQ_LENGTH, TestName = "Align sequence at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, TestName = "Align sequence at Middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, TestName = "Align sequence at End")]
        public void AlignSequence(int startIndex, int length)
        {
            AlignSequence(startIndex, length, 0, 0, ErrorLocation.Begining, ErrorType.Swap);
        }


        /// <summary>
        /// Aligns a sequence starting at index <paramref name="startIndex"/> and length 
        /// <paramref name="length"/>.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <param name="errorsAllowed">The errors allowed while aligning.</param>
        /// <param name="errorsCountToImplant">The number of errors count to implant in sequence.</param>
        /// <param name="errorLocation">The error location in sequence.</param>
        /// <remarks>
        /// In case <paramref name="errorsAllowed"/> is larger than <paramref name="errorsCountToImplant"/> index is expected not to be found.
        /// </remarks>
        [TestAttribute]
        [TestCase(0, SEQ_LENGTH, 2, 2, ErrorLocation.Begining, ErrorType.Swap, TestName = "Align sequence at beginning - with Errors at beginning")]
        [TestCase(0, SEQ_LENGTH, 2, 3, ErrorLocation.Begining, ErrorType.Swap, TestName = "Miss Align sequence at beginning - with errors at beginning")]
        [TestCase(0, SEQ_LENGTH, 2, 1, ErrorLocation.Begining, ErrorType.Gap, TestName = "Align sequence at beginning - gap errors at beginning")]
        [TestCase(0, SEQ_LENGTH, 2, 3, ErrorLocation.Begining, ErrorType.Gap, TestName = "Miss Align sequence at beginning - gap errors  at beginning")]

        [TestCase(0, SEQ_LENGTH, 2, 2, ErrorLocation.Middle, ErrorType.Swap, TestName = "Align sequence at beginning - with Errors at middle")]
        [TestCase(0, SEQ_LENGTH, 2, 3, ErrorLocation.Middle, ErrorType.Swap, TestName = "Miss Align sequence at beginning - with Errors at middle")]
        [TestCase(0, SEQ_LENGTH, 2, 1, ErrorLocation.Middle, ErrorType.Gap, TestName = "Align sequence at beginning - gap errors at middle")]
        [TestCase(0, SEQ_LENGTH, 2, 3, ErrorLocation.Middle, ErrorType.Gap, TestName = "Miss Align sequence at beginning - gap errors at middle")]

        [TestCase(0, SEQ_LENGTH, 2, 2, ErrorLocation.End, ErrorType.Swap, TestName = "Align sequence at beginning - with Errors")]
        [TestCase(0, SEQ_LENGTH, 2, 3, ErrorLocation.End, ErrorType.Swap, TestName = "Miss Align sequence at beginning")]
        [TestCase(0, SEQ_LENGTH, 2, 1, ErrorLocation.End, ErrorType.Gap, TestName = "Align sequence at beginning - gap errors")]

        /*---*/

        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 2, ErrorLocation.Begining, ErrorType.Swap, TestName = "Align sequence at Middle - with Errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 3, ErrorLocation.Begining, ErrorType.Swap, TestName = "Miss Align sequence at Middle - with errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 1, ErrorLocation.Begining, ErrorType.Gap, TestName = "Align sequence at Middle - gap errors  at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 3, ErrorLocation.Begining, ErrorType.Gap, TestName = "Miss Align sequence at Middle - gap errors at beginning")]

        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 2, ErrorLocation.Middle, ErrorType.Swap, TestName = "Align sequence at Middle - with Errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 3, ErrorLocation.Middle, ErrorType.Swap, TestName = "Miss Align sequence at Middle with errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 1, ErrorLocation.Middle, ErrorType.Gap, TestName = "Align sequence at Middle - gap errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 3, ErrorLocation.Middle, ErrorType.Gap, TestName = "Miss Align sequence at Middle - gap errors at middle")]

        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 2, ErrorLocation.End, ErrorType.Swap, TestName = "Align sequence at Middle - with Errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 3, ErrorLocation.End, ErrorType.Swap, TestName = "Miss Align sequence at Middle - with errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, SEQ_LENGTH, 2, 1, ErrorLocation.End, ErrorType.Gap, TestName = "Align sequence at Middle - gap errors at end")]

        /*---*/
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 2, ErrorLocation.Begining, ErrorType.Swap, TestName = "Align sequence at End - with Errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 3, ErrorLocation.Begining, ErrorType.Swap, TestName = "Miss Align sequence at End - with errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 1, ErrorLocation.Begining, ErrorType.Gap, TestName = "Align sequence at End - gap errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 3, ErrorLocation.Begining, ErrorType.Gap, TestName = "Miss Align sequence at Middle - gap errors at beginning")]

        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 2, ErrorLocation.Middle, ErrorType.Swap, TestName = "Align sequence at End - with Errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 3, ErrorLocation.Middle, ErrorType.Swap, TestName = "Miss Align sequence at End - with errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 1, ErrorLocation.Middle, ErrorType.Gap, TestName = "Align sequence at End - gap errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 3, ErrorLocation.Middle, ErrorType.Gap, TestName = "Miss Align sequence at Middle - gap errors at middle")]

        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 2, ErrorLocation.End, ErrorType.Swap, TestName = "Align sequence at End - with Errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 3, ErrorLocation.End, ErrorType.Swap, TestName = "Miss Align sequence at End - with Errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 1, ErrorLocation.End, ErrorType.Gap, TestName = "Align sequence at End - gap errors at end ")]
        // Not a valid test - gaps from end will result a valid alignment of the trimmed sequence... [TestCase(TestSuit.IndexOfSeqAtEnd, SEQ_LENGTH, 2, 3, ErrorLocation.End, ErrorType.Gap, TestName = "Miss Align sequence at End - gap errors")]        
        public void AlignSequence(int startIndex, int length, int errorsAllowed, int errorsCountToImplant, ErrorLocation errorLocation, ErrorType errorType)
        {
            this._slogics.FindGapgs = errorType == ErrorType.Gap; //this will preventing findings false alignments...
            //Arrange
            var seqWithErrors = this.GetRead(startIndex, length, errorsCountToImplant, errorLocation, errorType);

            //Act
            var results = this._slogics.iSearch.GetIndex(seqWithErrors, errorsAllowed);

            //Assert
            var foundIndexes = results.Indexes;
            bool shouldFinfMatch = errorsCountToImplant <= errorsAllowed;
            bool foundIndex = foundIndexes.Contains(startIndex);
            if (shouldFinfMatch)
            {
                Assert.IsTrue(foundIndex, "Expected index was not found in BWA results.");

            }
            else
            {
                Assert.IsFalse(foundIndex, "Index was unexpectedly found in BWA results.");


            }
        }



        [TestAttribute]
        [TestCase(1000, 2, TestName = "Compare Multi / Single thread duration")]
        public void CompareMultiThreadingDuration(int seqCount, int errorsAllowed)
        {


            this._slogics.FindGapgs = false; //for performance
            //arrange
            var reads = this._slogics.GetRandomReads(seqCount, SEQ_LENGTH, 2);

            //act            
            var tsSingle = this._slogics.RunMultipleAlignments(reads, errorsAllowed, this.bw, SequenceLogics.AlignMode.SingleThread, false).Duration;
            var tsMulti = this._slogics.RunMultipleAlignments(reads, errorsAllowed, this.bw, SequenceLogics.AlignMode.MultiThread, false).Duration;
            var actualFactor = tsSingle.TotalSeconds / tsMulti.TotalSeconds;

            //assert
            const int TARGET_FACTOR = 3;
            Assert.IsTrue(actualFactor >= TARGET_FACTOR, String.Format("Got a time factor of {0}, while expecting at list {1}", actualFactor.ToString("N2"), TARGET_FACTOR));

        }

        


        [TestAttribute]
        [TestCase(0, 2, 2, ErrorLocation.Begining, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at beginning - with Errors at beginning")]
        [TestCase(0, 2, 3, ErrorLocation.Begining, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at beginning -  (Miss Align) with errors at beginning")]
        [TestCase(0, 2, 1, ErrorLocation.Begining, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at  beginning - gap errors at beginning")]
        [TestCase(0, 2, 3, ErrorLocation.Begining, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at  beginning - (Miss align) gap errors  at beginning")]

        [TestCase(0, 2, 2, ErrorLocation.Middle, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at beginning - with Errors at middle")]
        [TestCase(0, 2, 3, ErrorLocation.Middle, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at  beginning -  (Miss Align) with Errors at middle")]
        [TestCase(0, 2, 1, ErrorLocation.Middle, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at beginning - gap errors at middle")]
        [TestCase(0, 2, 3, ErrorLocation.Middle, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at beginning -  (Miss Align) gap errors at middle")]

        [TestCase(0, 2, 2, ErrorLocation.End, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at beginning - with Errors")]
        [TestCase(0, 2, 3, ErrorLocation.End, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at beginning -  (Miss Align)")]
        [TestCase(0, 2, 1, ErrorLocation.End, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at beginning - gap errors")]

        /*---*/

        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 2, ErrorLocation.Begining, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at Middle - with Errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 3, ErrorLocation.Begining, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at Middle -  (Miss Align) with errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 1, ErrorLocation.Begining, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle - gap errors  at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 3, ErrorLocation.Begining, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle -  (Miss Align) gap errors at beginning")]

        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 2, ErrorLocation.Middle, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at Middle - with Errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 3, ErrorLocation.Middle, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at Middle -(Miss Align) with errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 1, ErrorLocation.Middle, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle - gap errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 3, ErrorLocation.Middle, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle -  (Miss Align) gap errors at middle")]

        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 2, ErrorLocation.End, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at Middle - with Errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 3, ErrorLocation.End, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at Middle -  (Miss Align) with errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtMiddle, 2, 1, ErrorLocation.End, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle - gap errors at end")]

        /*---*/
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 2, ErrorLocation.Begining, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at End - with Errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 3, ErrorLocation.Begining, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at End -  (Miss Align) with errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 1, ErrorLocation.Begining, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at End - gap errors at beginning")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 3, ErrorLocation.Begining, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle -  (Miss Align) gap errors at beginning")]

        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 2, ErrorLocation.Middle, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at End - with Errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 3, ErrorLocation.Middle, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at End -  (Miss Align) with errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 1, ErrorLocation.Middle, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at End - gap errors at middle")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 3, ErrorLocation.Middle, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at Middle -  (Miss Align) gap errors at middle")]

        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 2, ErrorLocation.End, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at End - with Errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 3, ErrorLocation.End, ErrorType.Swap, TestName = "Compare Multi / Single thread Results at End -  (Miss Align) with Errors at end")]
        [TestCase(TestSuit.IndexOfSeqAtEnd, 2, 1, ErrorLocation.End, ErrorType.Gap, TestName = "Compare Multi / Single thread Results at End - gap errors at end ")]
        public void CompareMultiThreadingResults(int startIndex, int errorsAllowed, int errorsCountToImplant, ErrorLocation errorLocation, ErrorType errorType)
        {

            this._slogics.FindGapgs = errorType == ErrorType.Gap; //for performance
            //arrange
            var read = GetRead(startIndex, SEQ_LENGTH, errorsCountToImplant, errorLocation, errorType);

            Func<SequenceLogics.MultiAlignResults, int[][]> resultToIndexes =
                       (res) => res.AllResults.Select(r => r.Indexes.OrderBy(i => i).ToArray()).ToArray();
            //act            
            var resSingle = this._slogics.RunMultipleAlignments(new List<string> { read }, errorsAllowed, this.bw, SequenceLogics.AlignMode.MultiThread, false);
            var indexes_single = resultToIndexes(resSingle);

            var resMulti = this._slogics.RunMultipleAlignments(new List<string> { read }, errorsAllowed, this.bw, SequenceLogics.AlignMode.MultiThread, false);
            var indexes_multi = resultToIndexes(resMulti);

            Func<int[][], int[][], bool> oneSideComparison =
                                           (r1, r2) => r1.All(res => r2.Any(res2 => res2.SequenceEqual(res)));

            Func<int[][], int[][], bool> twoSideComparison =
                       (r1, r2) => oneSideComparison(r1, r2) && oneSideComparison(r2, r1);
            //assert
            bool isIdentical = twoSideComparison(indexes_single, indexes_multi);
            Assert.IsTrue(isIdentical, String.Format("Unexpectedly got different results for single / multi threads"));



        }

        #region Helpers

        /// <summary>
        /// Gets a read by specified parameters.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="length">The length.</param>
        /// <param name="errorsCountToImplant">The errors count to implant.</param>
        /// <param name="errorLocation">The error location.</param>
        /// <param name="errorType">Type of the error.</param>
        /// <returns></returns>
        private string GetRead(int startIndex, int length, int errorsCountToImplant, ErrorLocation errorLocation, ErrorType errorType)
        {
            var seq = new String(this._slogics.Reference.Skip(startIndex).Take(length).ToArray());
            var seqWithErrors = this.ImplantErrorsInSequence(seq, errorsCountToImplant, errorLocation, errorType);
            return seqWithErrors;
        }

        /// <summary>
        /// Implants errors in sequence.
        /// </summary>
        /// <param name="sequence">The sequence to implant errors into.</param>
        /// <param name="errorCount">The error count.</param>
        /// <param name="errorLocation">The error location.</param>
        /// <returns>the sequence with errors</returns>
        private string ImplantErrorsInSequence(string sequence, int errorCount, ErrorLocation errorLocation, ErrorType errorType)
        {
            int startIndex;
            switch (errorLocation)
            {

                case ErrorLocation.Middle:
                    startIndex = sequence.Length / 2;
                    break;
                case ErrorLocation.End:
                    startIndex = sequence.Length - errorCount;
                    break;
                case ErrorLocation.Begining:
                default:
                    startIndex = 0;
                    break;

            }

            var alphabet = this._slogics.iSearch.ALPHA_BET_LETTERS;
            StringBuilder sb = new StringBuilder(sequence);
            for (int i = errorCount - 1; i >= 0; i--)
            {
                var charIndex = startIndex + i;
                switch (errorType)
                {
                    case ErrorType.Gap:
                        //var updatedIndex = charIndex - i; // we have modified the string - index should be matched...
                        sb.Remove(charIndex, 1);
                        break;
                    case ErrorType.Swap:
                    default:

                        var currLetter = sequence[charIndex];
                        var errorLetter = alphabet.Last() == currLetter ?
                            alphabet.First() : alphabet.SkipWhile(c => c != currLetter).Skip(1).Take(1).First();
                        sb[charIndex] = errorLetter;


                        break;
                }

            }

            return sb.ToString();
        }

        #endregion


        public enum ErrorLocation
        {
            Begining,
            Middle,
            End
        }

        public enum ErrorType
        {
            Gap,
            Swap
        }
    }
}





