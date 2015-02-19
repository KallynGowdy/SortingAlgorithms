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

        /// <summary>
        /// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Most Significan Digit.
        /// 
        /// This implementation is quite similar to the previous one. It sorts by the most significant digit first to hopefully take
        /// better advantage of branch prediction. (I don't know if it actually will though) It also prevents making new arrays
        /// every single digit cycle by swapping the array references every single digit cycle. Also, it compares digits by their
        /// character value, not by using modulus math. It is potentially much faster than the modulus version because ToString()
        /// probably uses bit shifts to calculate the string, which are much faster than modulus and division calculations.
        /// </summary>
        /// <param name="numbers">The numbers that should be swapped.</param>
        /// <returns>Returns a new array containing the sorted numbers.</returns>
        public static int[] SortByLeastSignificantDigitByChar(int[] numbers)
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

                for (char c = '0'; c < '9'; c++) // Use the character values instead of the integer values because comparisions can
                                                 // be made directly without any sort of conversion (i.e. subtracting 48)
                {
                    for (int i = 0; i < currentNumbers.Length; i++)
                    {
                        int n = currentNumbers[i];
                        string s = (n < 0 ? -n : n).ToString();
                        char ch = s.Reverse().Skip(digit).FirstOrDefault(); // Get the character at the last digit
                        if (ch == '\0')
                        {
                            ch = '0';
                        }
                        if (ch == c)
                        {
                            output[currentPlace++] = n;
                            if (ch != '0' )
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
