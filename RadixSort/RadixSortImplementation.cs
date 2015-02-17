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
        /// This version sorts the numbers by putting the numbers into a seperate array that does not get affected by the transactions
        /// and uses the output array for each of the encountered numbers. Could possibly be sped up by swapping instead of inserting items.
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
                int currentPlace = 0;
                output.CopyTo(currentNumbers, 0);
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
                            if (v > 0)
                                hadDigit = true;
                        }
                    }
                }
                digit++;
            } while (hadDigit);

            return output;
        }

        public static int[] SortByLeastSignificantDigitAndSwap(int[] numbers)
        {
            int[] output = new int[numbers.Length];
            numbers.CopyTo(output, 0);

            int digit = 0;
            bool hadDigit = false;
            do
            {
                int currentPlace = 0;
                hadDigit = false;
                for (int c = 0; c < 10; c++)
                {
                    for (int i = 0; i < output.Length; i++)
                    {
                        int n = output[i];
                        int d = (int)Math.Pow(10, digit);
                        int v = (n / d);
                        if (v % 10 == c)
                        {
                            int temp = output[currentPlace]; // Swap numbers
                            output[currentPlace++] = n;
                            output[i] = temp;
                            if (v > 0)
                                hadDigit = true;
                        }
                    }
                }
                digit++;
            } while (hadDigit);

            return output;
        }

        public static CountingArray<int> SortByLeastSignificantDigitAndSwap(CountingArray<int> numbers)
        {
            int digit = 0;
            bool hadDigit = false;
            do
            {
                int currentPlace = 0;
                hadDigit = false;
                for (int c = 0; c < 10; c++)
                {
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        int n = numbers[i];
                        int d = (int)Math.Pow(10, digit);
                        int v = (n / d);
                        if (v % 10 == c)
                        {
                            numbers.Swap(currentPlace++, i);
                            if (v > 0)
                                hadDigit = true;
                        }
                    }
                }
                digit++;
            } while (hadDigit);

            return numbers;
        }
    }
}
