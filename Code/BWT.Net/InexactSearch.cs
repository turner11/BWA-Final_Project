using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWT
{
    public class InexactSearch
    {
        /*Check the A C G T - i think i miss understood when adding them.*/


        /// <summary>
        /// The reference
        /// </summary>
        static string X_Reference = "googol$";
        static int[] D;

        public static readonly IReadOnlyCollection<char> ALPHA_BET_LETTERS;

        static InexactSearch()
        {
            InexactSearch.ALPHA_BET_LETTERS = new List<char> { 'A', 'C', 'G', 'T' }.AsReadOnly();
        }
        /// <summary>
        /// Get the index of the specified string using an inexact search
        /// </summary>
        /// <param name="w_stringToMatch"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static int[] GetIndex(string w_stringToMatch, int errorsAlloed)
        {
            InexactSearch.D = InexactSearch.CalculateD(w_stringToMatch);
            var indexes = GetIndexRecursive(w_stringToMatch, w_stringToMatch.Length-1,errorsAlloed,1,InexactSearch.X_Reference.Length -1).ToArray();
            return indexes;
        }

        private static IEnumerable<int> GetIndexRecursive(string w_stringToMatch, int i, int errorsAlloed, int lowerBound, int upperBound)
        {
            if (errorsAlloed < InexactSearch.D[i])
                return new List<int>();
            if (i < 0)
                return new List<int>(){ lowerBound, upperBound };

            /*The bounds to retunr*/
            IEnumerable<int> I = new List<int>();

            /*Find matches with less errors*/
            var boundsWithLessErrors = InexactSearch.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed - 1, lowerBound, upperBound);
            I = I.Union(boundsWithLessErrors);


            foreach (var letter in InexactSearch.ALPHA_BET_LETTERS)
            {
                var letterAsString = letter.ToString();
                lowerBound = InexactSearch.GetBottomRowIndex(letterAsString);
                upperBound = InexactSearch.GetTopRowIndex(letterAsString);

                bool isLegalBounds = lowerBound <= upperBound;
                if (isLegalBounds)
                {
                    var boundsInNewBoundeariesWithLessErrors = 
                        InexactSearch.GetIndexRecursive(w_stringToMatch,i,errorsAlloed -1 ,lowerBound,upperBound);
                    I = I.Union(boundsInNewBoundeariesWithLessErrors);
                    
                    if (letter == w_stringToMatch[i])
                    {
                        //current leeter is a match, we go on into deeper recursion with same ampunt of eerrors allowes
                        var deepperRecursiveBoundaries =
                        InexactSearch.GetIndexRecursive(w_stringToMatch, i-1, errorsAlloed, lowerBound, upperBound);
                        I = I.Union(deepperRecursiveBoundaries);
                    }
                    else 
                    {
                        //we found an error, we reduce the number of allowed errors in next iteration
                        var deepperRecursiveWithLessErrorsBoundaries =
                       InexactSearch.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed -1, lowerBound, upperBound);
                        I = I.Union(deepperRecursiveWithLessErrorsBoundaries);
                    }
                }
            }



            return I;
        }

        private static int[] CalculateD(string w)
        {
            int[] d = new int[w.Length];
            int z = 0;
            int j = 0;
            for (int i = 0; i < w.Length; i++)
            {
                string substring = w.Substring(j, i);
                bool isSubstring = InexactSearch.X_Reference.IndexOf(substring) >=0;
                if (!isSubstring)
                {
                    z = z + 1;
                    j = i + 1;
                }
                d[i] = z;


            }
            return d;
        }


        private static string CalculateD2(string w)
        {
            throw new NotImplementedException();
            
            int k = 1;
            int l = InexactSearch.X_Reference.Length - 1;
            int z = 0;

            for (int i = 0; i < w.Length; i++)
            {
                var currChar = w[i]; /*a*/
               // k = C(currChar)  + ;/*C(a)*/

                
            }
        }
       
        /// <summary>
        /// Gets the number of symbols in X[0,n−2] that are lexicographically 
        /// smaller than a
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of symbols in X[0,n−2] that are lexicographically 
        /// smaller than a</returns>
        private static int C(char a)
        {
           int c = /*C(a)*/InexactSearch.X_Reference.Take(InexactSearch.X_Reference.Length - 1)
                                                 .Count(x => x < a);
            return c;
        }


        /// <summary>
        /// Gets the number of occurrences of a in B[0,i]
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of occurrences of a in B[0,i]</returns>
        private static int O(char a, int i)
        {
            int c = /*C(a)*/InexactSearch.X_Reference.Take(i+1)
                                                  .Count(x => x == a);
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


        private static int GetBottomRowIndex(string w)
        {
            return GetRowIndex(w, RowBoundaries.Bottom);
        }

        private static int GetTopRowIndex(string w)
        {
            return GetRowIndex(w, RowBoundaries.Top);
        }

        private static int GetRowIndex(string w, RowBoundaries boundary)
        {
            if (w == string.Empty)
                return boundary == RowBoundaries.Bottom ? 1 : InexactSearch.X_Reference.Length - 1;
            //the prefiex
            char a = w[0];

            string trimmed_W = w.Substring(1);
            int trimmedIndex = InexactSearch.GetRowIndex(trimmed_W,boundary);

            int innerOffset = boundary == RowBoundaries.Bottom ? -1 : 0;
            int outerOffset = boundary == RowBoundaries.Bottom ?  1 : 0;

            int index = InexactSearch.C(a) + InexactSearch.O(a, trimmedIndex + innerOffset) + outerOffset;
            return index;
        }

        enum RowBoundaries
        {
            Top,
            Bottom
        }
    }
}
