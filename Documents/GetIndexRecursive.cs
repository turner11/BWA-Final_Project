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