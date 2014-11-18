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
        static BwtLogics _bwtLogics;
        static BwtLogics.BwtResults _bwtResults;
        static BwtLogics.BwtResults _bwtReverseResults;
        /// <summary>
        /// The reference
        /// </summary>
        static string X_Reference = "googol" + BwtLogics.END_OF_FILE_CHAR;
        static int[] D;

        public static readonly IReadOnlyCollection<char> ALPHA_BET_LETTERS;

        static InexactSearch()
        {
            InexactSearch._bwtLogics = new BwtLogics();
            /*Get BWt for the reference*/
            InexactSearch._bwtResults = InexactSearch._bwtLogics.Bwt(X_Reference);
            
            /*Get Bwt for reverse reference*/
            char[] chars =InexactSearch.X_Reference.Replace(BwtLogics.END_OF_FILE_CHAR.ToString(),String.Empty).ToCharArray(); 
            Array.Reverse(chars);
            string reverseReference = new String(chars);
            InexactSearch._bwtReverseResults = InexactSearch._bwtLogics.Bwt(reverseReference);

            /*Define the alphbet*/
            var letters = X_Reference.Substring(0,X_Reference.Length-1).Distinct().ToList();
            letters.Sort(); //this is just for easier code comparing.
            InexactSearch.ALPHA_BET_LETTERS = letters.AsReadOnly();//new List<char> { 'A', 'C', 'G', 'T' }.AsReadOnly();

            //TODO: Calculate array C(·) and O(·,·) from _bwtResults.BwtString (work that can be done in before run time)
            //TODO: Calculate array O'(·,·) from _bwtReverseResults.BwtString  (work that can be done in before run time)

        }
        /// <summary>
        /// Get the index of the specified string using an inexact search
        /// </summary>
        /// <param name="w_stringToMatch"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Results GetIndex(string w_stringToMatch, int errorsAlloed)
        {           
            InexactSearch.D = InexactSearch.CalculateD2(w_stringToMatch);
            var indexes = GetIndexRecursive(w_stringToMatch, w_stringToMatch.Length-1,errorsAlloed,0,InexactSearch.X_Reference.Length -1).ToArray();
            
            return new Results(indexes, InexactSearch._bwtResults.SuffixTable, InexactSearch._bwtResults.BwtString, w_stringToMatch, errorsAlloed);
        }

        private static IEnumerable<int> GetIndexRecursive(string w_stringToMatch, int i, int errorsAlloed, int lowerBound, int upperBound)
        {
            if (errorsAlloed < 0)
                return new List<int>();
            if (i < 0)
                return new List<int>() { lowerBound, upperBound };
           
            

            /*The bounds to retunr*/
            IEnumerable<int> I = new List<int>();

            /*Find matches with less errors*/
            var boundsWithLessErrors = InexactSearch.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed - 1, lowerBound, upperBound).ToList();
            I = I.Union(boundsWithLessErrors);//Remove?


            foreach (var letter in InexactSearch.ALPHA_BET_LETTERS)
            {
                
                var letterAsString = letter.ToString();
                var c_letter = InexactSearch.C(letter);
                var temp_lowerBound = c_letter + InexactSearch.O(letter,lowerBound-1) + 1;
                var temp_upperBound = c_letter + InexactSearch.O(letter, upperBound);

                bool isLegalBounds = lowerBound <= upperBound;
                if (isLegalBounds)
                {
                    var boundsInNewBoundeariesWithLessErrors =
                        InexactSearch.GetIndexRecursive(w_stringToMatch, i, errorsAlloed - 1, temp_lowerBound, temp_upperBound);
                    I = I.Union(boundsInNewBoundeariesWithLessErrors); //Remove?
                    
                    if (letter == w_stringToMatch[i])
                    {
                        //current leeter is a match, we go on into deeper recursion with same ampunt of eerrors allowes
                        var deepperRecursiveBoundaries =
                        InexactSearch.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed, temp_lowerBound, temp_upperBound);
                        I = I.Union(deepperRecursiveBoundaries);
                    }
                    else 
                    {
                        //we found an error, we reduce the number of allowed errors in next iteration
                        var deepperRecursiveWithLessErrorsBoundaries =
                       InexactSearch.GetIndexRecursive(w_stringToMatch, i - 1, errorsAlloed - 1, temp_lowerBound, temp_upperBound).ToList();
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


        private static int[] CalculateD2(string w)
        {
            int[] d = new int[w.Length];
            
            int k = 1;
            int l = InexactSearch.X_Reference.Length - 1;
            int z = 0;

            for (int i = 0; i < w.Length; i++)
            {
                var currChar = w[i]; /*a*/
                var charAsString = currChar.ToString();
                k = InexactSearch.C(currChar) + InexactSearch.OT(currChar, k - 1) + 1;//InexactSearch.GetBottomRowIndex(charAsString);
                l = InexactSearch.C(currChar) + InexactSearch.OT(currChar, l);

                if (k > l)
                {
                    k = l;
                    l = InexactSearch.X_Reference.Length - 1;
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
            string str = InexactSearch._bwtResults.BwtString;
            return O_Base(a, i, str);
        }

        /// <summary>
        /// Gets the number of occurrences of a in BT[0,i]
        /// </summary>
        /// <param name="a"></param>
        /// <returns>the number of occurrences of a in B[0,i]</returns>
        private static int OT(char a, int i)
        {
            string str = InexactSearch._bwtReverseResults.BwtString;
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


        public class Results
        {
            public int[] Indexes { get; private set; }
            public DataTable SuffixArray { get; private set; }
            public string BwtString { get; private set; }
            public string StringToMatch { get; private set; }
            public int ErrorAllowed { get; private set; }



            public Results(int[] indexes, DataTable suffixArray, string bwtString, string stringToMatch, int errorAllowed)
            {
                this.Indexes = indexes;
                this.SuffixArray = suffixArray;
                this.BwtString = bwtString;
                this.StringToMatch = stringToMatch;
                this.ErrorAllowed = errorAllowed;
            }
        }
    }
}
