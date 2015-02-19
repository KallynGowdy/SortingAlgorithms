using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingArray
{
    /// <summary>
    /// Defines a class that represents an array that counts different transactions.
    /// </summary>
    public class CountingArray<T> : IEnumerable<T>
    {
        T[] items;

        /// <summary>
        /// Gets the number of times that values have been swapped in the array.
        /// </summary>
        public long Swaps { get; private set; }

        /// <summary>
        /// Gets the number of times that values have been compared.
        /// </summary>
        public long Compares { get; private set; }

        /// <summary>
        /// Gets the number of times that the internal array has been accessed through the indexer.
        /// </summary>
        public long Retrievals { get; private set; }

        /// <summary>
        /// Gets the number of times that a value has been set in the internal array through the indexer.
        /// </summary>
        public long Sets { get; private set; }

        public CountingArray(int size)
        {
            items = new T[size];
        }

        public CountingArray(T[] items)
        {
            this.items = new T[items.Length];
            items.CopyTo(this.items, 0);
        }

        /// <summary>
        /// Gets or sets the value that is stored at the given index.
        /// </summary>
        /// <param name="index">The index that the item should be stored/retrieved at/by.</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                Retrievals++;
                return items[index];
            }
            set
            {
                Sets++;
                items[index] = value;
            }
        }

        /// <summary>
        /// Swaps the values at the given indexes and records the transaction.
        /// </summary>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        public void Swap(int firstIndex, int secondIndex)
        {
            T temp = this[firstIndex];
            this[firstIndex] = this[secondIndex];
            this[secondIndex] = temp;
            Swaps++;
        }

        /// <summary>
        /// Compares the two values at the given indexes using the given comparision function.
        /// </summary>
        /// <param name="firstIndex">The index of the first value that should be compared.</param>
        /// <param name="secondIndex">The index of the second value that should be compared.</param>
        /// <param name="compare">The function that, given the two values, returns whether the comparision is true or false.</param>
        /// <returns></returns>
        public bool Compare(int firstIndex, int secondIndex, Func<T, T, bool> compare)
        {
            T first = this[firstIndex];
            T second = this[secondIndex];
            Compares++;
            return compare(first, second);
        }

        public int Length
        {
            get
            {
                return items.Length;
            }
        }

        public long LongLength
        {
            get
            {
                return items.LongLength;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
