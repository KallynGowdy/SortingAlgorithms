using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountingArray;

namespace Quicksort
{
    /// <summary>
    /// Defines a static class that provides an implementation of quick sort.
    /// </summary>
    public static class QuickSortImplementation
    {
        /// <summary>
        /// Sorts the given list using the quicksort algorithm.
        /// 
        /// This version uses recursion to sort the items by dividing and conquering.
        /// </summary>
        /// <typeparam name="T">The type of items that should be sorted.</typeparam>
        /// <param name="items">The items that should be sorted.</param>
        /// <returns>Returns a new array that represents the sorted items.</returns>
        public static T[] Sort<T>(T[] items)
            where T : IComparable<T>
        {
            if (items == null) throw new ArgumentNullException("items");
            T[] result = new T[items.Length];
            items.CopyTo(result, 0);
            return SortInternal(result, 0, result.Length - 1, new Random((int)DateTime.Now.Ticks));
        }

        /// <summary>
        /// Sorts the items recursively within the given lower bound and upper bound (inclusive)
        /// </summary>
        /// <typeparam name="T">The type of objects that are being sorted.</typeparam>
        /// <param name="items">The list of items that should be sorted.</param>
        /// <param name="lowerBound">The lower bound that determines the smallest limit for items being swapped in the array.</param>
        /// <param name="upperBound">The upper bound that determines the largest limit for items being swapped in the array.</param>
        /// <param name="rng">The random number generator that should be used for selecting pivot values.</param>
        /// <returns></returns>
        private static T[] SortInternal<T>(T[] items, int lowerBound, int upperBound, Random rng)
            where T : IComparable<T>
        {
            if (lowerBound >= upperBound) return items;

            // Select our piviot point
            int pivot = rng.Next(lowerBound, upperBound);
            T p = items[pivot];

            // Swap(T, T) was inlined to increase performance
            T f = items[pivot];
            items[pivot] = items[upperBound];
            items[upperBound] = f;

            int storeIndex = lowerBound;
            // Move all items that are smaller to the left of the pivot and all that are larger to the right
            for (int i = lowerBound; i < upperBound; i++)
            {
                // This is a big bottleneck for value types because
                // they need to be boxed to call CompareTo(T)
                if (items[i].CompareTo(p) < 0)
                {
                    T f1 = items[i];
                    items[i] = items[storeIndex];
                    items[storeIndex] = f1;
                    storeIndex++;
                }
            }
            T f2 = items[storeIndex];
            items[storeIndex] = items[upperBound];
            items[upperBound] = f2;

            SortInternal(items,
                lowerBound,
                storeIndex - 1, rng);

            SortInternal(items,
                storeIndex + 1,
                upperBound, rng);

            return items;
        }

        /// <summary>
        /// Sorts the given list using the quicksort algorithm.
        /// 
        /// This version uses recursion to sort the items by dividing and conquering.
        /// Because sorting results only use integers, this version of quicksort is actually faster
        /// than the previous. The reason is that it can compare integers using the provided operator and not by calling CompareTo().
        /// That prevents the boxing from occurring for the call and therefore speeds up the algorithm even though it is manipulating more
        /// values. (The swap and compare values)
        /// </summary>
        /// <typeparam name="T">The type of items that should be sorted.</typeparam>
        /// <param name="items">The items that should be sorted.</param>
        /// <returns>Returns a new array that represents the sorted items.</returns>
        public static SortingResult SortWithSortingResult(int[] numbers)
        {
            if (numbers == null) throw new ArgumentNullException("items");
            int[] result = new int[numbers.Length];
            numbers.CopyTo(result, 0);
            int swaps = 0;
            int compares = 0;
            SortInternalWithSortingResult(result, 0, result.Length - 1, new Random((int)DateTime.Now.Ticks), ref swaps, ref compares);

            return new SortingResult()
            {
                AlgorithmName = "Quicksort",
                Compares = compares,
                Swaps = swaps,
                SortedItems = result
            };
        }

        /// <summary>
        /// Sorts the items recursively within the given lower bound and upper bound (inclusive)
        /// </summary>
        /// <typeparam name="T">The type of objects that are being sorted.</typeparam>
        /// <param name="items">The list of items that should be sorted.</param>
        /// <param name="lowerBound">The lower bound that determines the smallest limit for items being swapped in the array.</param>
        /// <param name="upperBound">The upper bound that determines the largest limit for items being swapped in the array.</param>
        /// <param name="rng">The random number generator that should be used for selecting pivot values.</param>
        /// <returns></returns>
        private static void SortInternalWithSortingResult(int[] items, int lowerBound, int upperBound, Random rng, ref int currentSwapCount, ref int currentCompareCount)
        {
            if (lowerBound >= upperBound) return;

            // Select our piviot point
            int pivot = rng.Next(lowerBound, upperBound);
            int p = items[pivot];

            // Inline Swap to prevent extra method call (Swap() likely wont be inlined by the C# or IL compiler)
            int f = items[pivot];
            items[pivot] = items[upperBound];
            items[upperBound] = f;
            currentSwapCount++;

            int storeIndex = lowerBound;

            // Move all items that are smaller to the left of the pivot and all that are larger to the right
            for (int i = lowerBound; i < upperBound; i++)
            {
                if (items[i] < p)
                {
                    int f1 = items[i];
                    items[i] = items[storeIndex];
                    items[storeIndex] = f1;
                    currentSwapCount++;
                    storeIndex++;
                }
                currentCompareCount++;
            }
            int f2 = items[storeIndex];
            items[storeIndex] = items[upperBound];
            items[upperBound] = f2;
            currentSwapCount++;

            SortInternalWithSortingResult(items,
                lowerBound,
                storeIndex - 1, rng,
                ref currentSwapCount,
                ref currentCompareCount);

            SortInternalWithSortingResult(items,
                storeIndex + 1,
                upperBound,
                rng,
                ref currentSwapCount,
                ref currentCompareCount);

        }
    }
}
