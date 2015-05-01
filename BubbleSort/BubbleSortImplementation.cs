using CountingArray;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BubbleSort
{
    /// <summary>
    /// Defines a static class that contains a bubble sort implementation.
    /// </summary>
    public static class BubbleSortImplementation
    {

        /// <summary>
        /// Sorts the given numbers using the bubble sort algorithm. Returns a new array containing the sorted elements.
        /// 
        /// This particular implementation uses a flag to determine whether a value has been swapped.
        /// If the algorithm goes through the loop without swapping anything, then it knows that the array is sorted.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns></returns>
        public static int[] BubbleSortWithSwapFlag(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            int[] output = new int[numbers.Length];
            numbers.CopyTo(output, 0);
            bool swappedValue = false;
            do
            {
                swappedValue = false;
                for (int i = 0; i < output.Length - 1; i++)
                {
                    int first = output[i];
                    int second = output[i + 1];
                    if (first > second)
                    {
                        output[i] = second;
                        output[i + 1] = first;
                        swappedValue = true;
                    }
                }
            } while (swappedValue);

            return output;
        }

        public static SortingResult BubbleSortWithSwapFlagAndSortingResult(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            int[] output = new int[numbers.Length];
            numbers.CopyTo(output, 0);

            long compares = 0;
            long swaps = 0;

            bool swappedValue = false;
            do
            {
                swappedValue = false;
                for (int i = 0; i < output.Length - 1; i++)
                {
                    compares++;
                    int first = output[i];
                    int second = output[i + 1];
                    if (first > second)
                    {
                        output[i] = second;
                        output[i + 1] = first;
                        swappedValue = true;
                        swaps++;
                    }
                }
            } while (swappedValue);

            return new SortingResult()
            {
                AlgorithmName = "Bubble Sort",
                Compares = compares,
                Swaps = swaps,
                SortedItems = output
            };
        }

        /// <summary>
        /// Sorts the given numbers using the bubble sort algorithm. Returns a new array containing the sorted elements.
        /// 
        /// This particular implementation uses logic to determine whether the array is sorted by only traversing it
        /// for a set number of times (based on how many elements there are to sort).
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns>Returns a new array containing the sorted numbers.</returns>
        public static int[] BubbleSortWithoutSwap(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            int[] output = new int[numbers.Length];
            
            numbers.CopyTo(output, 0);

            for (int i = 0; i < output.Length; i++)
            {
                for (int c = 0; c < (output.Length - i) - 1; c++)
                {
                    int first = output[c];
                    int second = output[c + 1];
                    if (first > second)
                    {
                        output[c] = second;
                        output[c + 1] = first;
                    }
                }
            }

            return output;
        }


    }
}
