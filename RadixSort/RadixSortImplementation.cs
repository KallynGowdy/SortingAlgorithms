using CountingArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort
{
    /// <summary>
    /// Defines a static class that contains implementations of the Radix/Bucket sort.
    /// </summary>
    public static class RadixSortImplementation
    {

        /// <summary>
        /// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
        /// 
        /// Note that for negative numbers to work we need to make another pass after we've sorted the numbers to filter out and reverse the negatives.
        /// 
        /// This version sorts the numbers by putting the numbers into a seperate array that does not get affected by the transactions
        /// and uses the output array for each of the encountered numbers.
        /// Could potentially be sped up by converting the numbers to strings and comparing their characters instead of dividing them and performing modulus operations.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns></returns>
        public static int[] SortByLeastSignificantDigit(int[] numbers)
        {
            int[] output = new int[numbers.Length];
            numbers.CopyTo(output, 0);

            int digit = 0;
            bool hadDigit = false;
            do
            {
                int[] currentNumbers = new int[output.Length];
                output.CopyTo(currentNumbers, 0);
                int currentPlace = 0;
                hadDigit = false;

                for (int c = 0; c < 10; c++)
                {
                    for (int i = 0; i < currentNumbers.Length; i++)
                    {
                        int n = currentNumbers[i];
                        int d = (int)Math.Pow(10, digit);
                        int v = (n / d);
                        if (v % 10 == c)
                        {
                            output[currentPlace++] = n;
                            if (v != 0)
                                hadDigit = true;
                        }
                        else if (v % 10 == -c)
                        {
                            output[currentPlace++] = n;
                            if (v != 0)
                                hadDigit = true;
                        }
                    }
                }
                digit++;
            } while (hadDigit);

            Stack<int> negatives = new Stack<int>(output.Length);

            for (int i = 0; i < output.Length; i++)
            {
                int n = output[i];
                if (n < 0)
                {
                    negatives.Push(n);
                }
            }

            return negatives.Concat(output.Where(n => n >= 0)).ToArray();
        }
    }
}
