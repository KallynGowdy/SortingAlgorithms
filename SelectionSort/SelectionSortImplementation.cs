using CountingArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSort
{
    /// <summary>
    /// Defines a static class that provides a implementation of selection sort.
    /// </summary>
    public static class SelectionSortImplementation
    {
        /// <summary>
        /// Sorts the given list of numbers using the Selection Sort Algorithm and returns a new array containing the sorted elements.
        /// 
        /// This method uses two arrays to sort the values. One array as the input and one as the output. The input array
        /// is copied from the given list of numbers and each element is nullable. 
        /// When a value is selected from the input array, the location at the input array is set to null so that we know to skip over
        /// it. A much better alternative would be to sort the items in place by swapping the lowest number to the beginning of the unsorted items.
        /// A difficulty with this algorithm is the requirement of resizing the input array for it to work properly.
        /// </summary>
        /// <param name="numbers">The list of numbers that should be sorted.</param>
        /// <returns>Returns a new list containing the sorted numbers.</returns>
        public static int[] SelectionSortWithoutList(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            int?[] input = numbers.Select(n => (int?)n).ToArray();
            int[] output = new int[numbers.Length];

            for (int i = 0; i < input.Length; i++)
            {
                int lowest = 0;
                for (int c = 1; c < input.Length; c++)
                {
                    if (!input[c].HasValue) continue;
                    if (!input[lowest].HasValue || input[lowest] > input[c])
                    {
                        lowest = c;
                    }
                }
                output[i] = input[lowest].Value;
                input[lowest] = null;
            }

            return output;
        }

        /// <summary>
        /// Sorts the given list of numbers using the Selection Sort Algorithm and returns a new array containing the sorted elements.
        /// 
        /// This method sorts the numbers by removing the selected item from the input list and moving it to the output list.
        /// This is much faster than the "Without List" variant, but is still much slower than the "Swap" version because it involves
        /// creating two new arrays/lists and manipulating both of them.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns>Returns a new array containing the sorted numbers.</returns>
        public static int[] SelectionSortWithList(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            List<int> input = numbers.ToList();
            int[] output = new int[numbers.Length];

            for (int i = 0; i < output.Length; i++)
            {
                int lowest = 0;
                for (int c = 1; c < input.Count; c++)
                {
                    if (input[lowest] > input[c])
                    {
                        lowest = c;
                    }
                }
                output[i] = input[lowest];
                input.RemoveAt(lowest);
            }

            return output;
        }

        /// <summary>
        /// Sorts the given numbers using the Selection Sort algorithm and returns a new array containing the sorted numbers.
        /// 
        /// This variant creates only one output array and swaps the selected item to the first (second, third, ect..). 
        /// This avoids creating two different data structures, which benefits performance greatly.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns>Returns a new array containing the sorted numbers.</returns>
        public static int[] SelectionSortBySwap(int[] numbers)
        {
            return SelectionSortBySwap(numbers, false);
        }

        /// <summary>
        /// Sorts the given numbers using the Selection Sort algorithm and returns a list containing the sorted numbers.
        /// 
        /// This variant creates a new list if the 'createNewArray' parameter is true and swaps the selected item to the first (second, third, ect..). 
        /// This avoids creating two different data structures, which benefits performance greatly.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <param name="createNewArray">Whether a new array should be created for the output or if the given input array should be manipulated instead.</param>
        /// <returns>Returns list containing the sorted numbers.</returns>
        public static int[] SelectionSortBySwap(int[] numbers, bool createNewArray)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            int[] output;
            if (createNewArray)
            {
                output = new int[numbers.Length];
                output = numbers.ToArray();
            }
            else
            {
                output = numbers;
            }

            for (int i = 0; i < output.Length; i++)
            {
                int lowest = i;
                for (int c = i + 1; c < output.Length; c++)
                {
                    if (output[lowest] > output[c])
                    {
                        lowest = c;
                    }
                }
                int num = output[i];
                output[i] = output[lowest];
                output[lowest] = num;
            }

            return output;
        }

        /// <summary>
        /// Sorts the given numbers using the Selection Sort algorithm and returns a list containing the sorted numbers.
        /// 
        /// This variant creates a new list if the 'createNewArray' parameter is true and swaps the selected item to the first (second, third, ect..). 
        /// This avoids creating two different data structures, which benefits performance greatly.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <param name="createNewArray">Whether a new array should be created for the output or if the given input array should be manipulated instead.</param>
        /// <returns>Returns list containing the sorted numbers.</returns>
        public static CountingArray<int> SelectionSortBySwap(CountingArray<int> numbers)
        {
            if (numbers == null) throw new ArgumentNullException("numbers");

            for (int i = 0; i < numbers.Length; i++)
            {
                int lowest = i;
                for (int c = i + 1; c < numbers.Length; c++)
                {
                    if (numbers.Compare(lowest, c, (f,s) => f > s))
                    {
                        lowest = c;
                    }
                }

                numbers.Swap(lowest, i);
            }

            return numbers;
        }
    }
}
