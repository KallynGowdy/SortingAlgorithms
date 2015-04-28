using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicksort
{
    /// <summary>
    /// Defines a static class that provides an implementation of quick sort.
    /// </summary>
    public static class QuickSortImplementation
    {
        /// <summary>
        /// Sorts the given list using the quicksort algorithm.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static T[] Sort<T>(T[] items)
            where T : IComparable
        {
            if (items == null) throw new ArgumentNullException("items");

            return SortInternalAsync(items, 0, items.Length - 1, 2, 0, new Random((int)DateTime.Now.Ticks)).Result;
        }

        private static async Task<T[]> SortInternalAsync<T>(T[] items, int lowerBound, int upperBound, int parallelDepth, int currentDepth, Random rng)
            where T : IComparable
        {
            if (lowerBound >= upperBound) return items;

            // Select our piviot point
            int piviot = rng.Next(lowerBound, upperBound);
            T p = items[piviot];

            Swap(items, piviot, upperBound);
            int storeIndex = lowerBound;
            // Move all items that are smaller to the left of the piviot and all that are larger to the right
            for (int i = lowerBound; i < upperBound; i++)
            {
                if (items[i].CompareTo(p) <= 0)
                {
                    Swap(items, i, storeIndex);
                    storeIndex++;
                }
            }
            Swap(items, storeIndex, upperBound);

            await SortInternalAsync(items,
                lowerBound,
                storeIndex - 1,
                parallelDepth,
                currentDepth + 1, rng);

            await SortInternalAsync(items,
                storeIndex + 1,
                upperBound,
                parallelDepth,
                currentDepth + 1, rng);
            return items;
        }

        private static void Swap<T>(T[] items, int first, int second)
        {
            T f = items[first];
            items[first] = items[second];
            items[second] = f;
        }
    }
}
