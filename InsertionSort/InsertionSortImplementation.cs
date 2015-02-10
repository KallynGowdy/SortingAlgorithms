﻿using CountingArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSort
{
    /// <summary>
    /// Defines a static class that contains an implementation of insertion sort.
    /// </summary>
    public static class InsertionSortImplementation
    {

        /// <summary>
        /// Sorts the given list of numbers using the insertion sort algorithm.
        /// 
        /// Insertion sort can be seen as a reverse of bubble sort. We propogate the array from the first to the last, but instead of
        /// moving the largest items to the top first, we instead move the smallest items to the bottom first. This is much faster
        /// because we are only moving through a small portion of the array at a time, not the entire array each iteration.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns>Returns a new array containing the sorted numbers.</returns>
        public static int[] InsertionSortWithSwap(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            int[] output = new int[numbers.Length];
            numbers.CopyTo(output, 0);

            for (int i = 1; i < output.Length; i++)
            {
                int index = i;
                while (index > 0 && output[index] < output[index - 1])
                {
                    int temp = output[index - 1];
                    output[index - 1] = output[index];
                    output[index] = temp;
                    index--;
                }
            }

            return output;
        }

        public static CountingArray<int> InsertionSortWithSwap(CountingArray<int> numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            for (int i = 1; i < numbers.Length; i++)
            {
                int index = i;
                while (index > 0 && numbers.Compare(index, index-1, (f,s) => f < s))
                {
                    numbers.Swap(index, index - 1);
                    index--;
                }
            }

            return numbers;
        }

    }
}
