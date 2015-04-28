using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingArray;

namespace ShellSort
{
    /// <summary>
    /// Defines a static class that provides an implementation of shell sort.
    /// </summary>
    public static class ShellSortImplementation
    {

        /// <summary>
        /// Sorts the given list of items using the shell sort algorithm.
        /// 
        /// This might be improved in a couple ways, although I would like to use the profiler to get a couple leads on how.
        /// </summary>
        /// <typeparam name="T">The type of the items that are going to be sorted.</typeparam>
        /// <param name="items">The items that should be sorted.</param>
        /// <returns></returns>
        public static T[] Sort<T>(T[] items)
            where T : IComparable
        {
            if (items == null) throw new ArgumentNullException("items");

            // The distance that we sort the items at.
            T[] output = new T[items.Length];
            items.CopyTo(output, 0);

            for (int distance = items.Length / 2; distance > 0; distance /= 2)
            {
                for (int i = distance; i < items.Length; i++)
                {
                    int index = i;
                    while (index >= distance)
                    {
                        T first = output[index];
                        T second = output[index - distance];

                        // Optimization to cache the numbers and prevent accessing the 
                        if (first.CompareTo(second) < 0)
                        {

                            output[index - distance] = first;
                            output[index] = second;
                            index -= distance;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Sorts the given list of items using the shell sort algorithm and returns a <see cref="SortingResult"/> with the items and statistics.
        /// 
        /// This might be improved in a couple ways, although I would like to use the profiler to get a couple leads on how.
        /// </summary>
        /// <typeparam name="T">The type of the items that are going to be sorted.</typeparam>
        /// <param name="items">The items that should be sorted.</param>
        /// <returns></returns>
        public static SortingResult SortWithSortingResult(int[] items)
        {
            if (items == null) throw new ArgumentNullException("items");

            // The distance that we sort the items at.
            int[] output = new int[items.Length];
            items.CopyTo(output, 0);

            int swaps = 0;
            int compares = 0;
            int retrievals = 0;

            int increment = 0;
            while (increment <= items.Length / 3)
            {
                increment = increment * 3 + 1;
            }

            for (int distance = increment; distance > 0; distance = (distance - 1) / 3)
            {
                for (int i = distance; i < items.Length; i++)
                {
                    int index = i;
                    while (index >= distance)
                    {
                        int first = output[index];
                        int second = output[index - distance];
                        retrievals += 2;
                        if (first < second)
                        {
                            output[index - distance] = first;
                            output[index] = second;
                            index -= distance;
                            swaps++;
                        }
                        else
                        {
                            break;
                        }
                        compares++;
                    }
                }
            }

            return new SortingResult()
            {
                AlgorithmName = "Shell Sort",
                Compares = compares,
                Swaps = swaps,
                Retrievals = retrievals,
                SortedItems = output,
                Sets = retrievals // Because retrievals and sets are always the same
            };
        }

    }
}
