using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    /// <summary>
    /// Defines a static class that contains implementations of merge sort.
    /// </summary>
    public static class MergeSortImplementation
    {

        /// <summary>
        /// Sorts the given numbers in ascending order using the merge sort algorithm.
        /// This variant uses recursion to sort the items. It kinda "cheats" on splitting items because it recognizes that
        /// at the end of the splitting process each item is in an array of 1, so just put each number into its own array.
        /// Merging uses recursion though by taking two lists at a time and merging them in their sorted order. Then it takes the result of
        /// those mergers and runs it again.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns>Returns a new array that represents the sorted numbers.</returns>
        public static int[] SortUsingRecursion(int[] numbers)
        {
            return Merge(numbers.Select(n => new int[] { n }).ToArray());
        }

        /// <summary>
        /// Performs the grunt-work of the recursion enabled version of merge sort.
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        private static int[] Merge(params int[][] lists)
        {
            if (lists.Length <= 1)
            {
                return lists.First();
            }
            else
            {
                List<int[]> results = new List<int[]>();
                for (int i = 1; i < lists.Length; i += 2)
                {
                    results.Add(Merge(lists[i - 1], lists[i]));
                }
                return Merge(results.ToArray());
            }
        }

        /// <summary>
        /// Merges and sorts the two lists into one result list.
        /// </summary>
        /// <param name="firstList">The first list that should be used.</param>
        /// <param name="secondList">The second list that should be used.</param>
        /// <returns>Returns a single list that contains all of the numbers that are sorted in ascending order.</returns>
        private static int[] Merge(int[] firstList, int[] secondList)
        {
            int[] result = new int[firstList.Length + secondList.Length];
            int[] shortest = firstList.Length > secondList.Length ? secondList : firstList;
            int[] longest = firstList.Length > secondList.Length ? firstList : secondList;
            for (int i = 0; i < shortest.Length; i += 2)
            {
                if (shortest[i] < longest[i])
                {
                    result[i] = shortest[i];
                    result[i + 1] = longest[i];
                }
                else
                {
                    result[i] = longest[i];
                    result[i + 1] = shortest[i];
                }
            }
            for (int i = shortest.Length; i < longest.Length; i++)
            {
                result[(shortest.Length * 2 + (i - shortest.Length))] = longest[i];
            }
            return result;
        }

        /// <summary>
        /// Sorts the given list of numbers using for loops instead of recursion.
        /// 
        /// This variant uses a Queue to contain the lists and a while loop that keeps running until there is only one list left
        /// in the merge process. 
        /// First, each number in the given list is put into its own list, then the first two lists are taken from the queue and merge sorted together.
        /// The result of the merge is put back onto the queue at the end, and the process repeats until there is only one list left in the queue.
        /// </summary>
        /// <param name="numbers">The numbers that should be sorted.</param>
        /// <returns>Returns a new list containing the sorted numbers.</returns>
        public static int[] SortUsingLoops(int[] numbers)
        {
            // 1). Split numbers into seperate lists
            // 2). Dequeue top two lists and merge
            // 3). Enqueue result back onto the stack
            // 4). Repeat steps 2 & 3 until there is only 1 list left

            Queue<int[]> lists = new Queue<int[]>(numbers.Select(n => new[] { n }));

            while (lists.Count > 1)
            {
                lists.Enqueue(Merge(lists.Dequeue(), lists.Dequeue())); // Pop the top two elements off the list and merge them, then push the result.
            }

            return lists.Dequeue();
        }

    }
}
