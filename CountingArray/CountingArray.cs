using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingArray
{
	public static class SortingResultsExtensions
	{
		/// <summary>
		/// Calculates the total sum of the enumerable list of sorting results into a single result.
		/// **In the process, all of the items are jammed together, DO NOT expect them to actually be sorted after this**
		/// </summary>
		/// <param name="results"></param>
		/// <returns></returns>
		public static SortingResult Sum(this IEnumerable<SortingResult> results)
		{
			return new SortingResult()
			{
				AlgorithmName = results.First().AlgorithmName,
				Compares =   results.Sum(r => r.Compares),
				Miliseconds = results.Sum(r => r.Miliseconds),
				Retrievals =  results.Sum(r => r.Retrievals),
				Sets = results.Sum(r => r.Sets),
				Swaps = results.Sum(r => r.Swaps),
				SortedItems = results.SelectMany(r => r.SortedItems).ToArray()
			};
		}
	}

	/// <summary>
	/// Defines a class that represents the result of a sorting operation with it's recorded transaction numbers.
	/// </summary>
	public class SortingResult
	{
		/// <summary>
		/// Gets or sets the number of miliseconds that the algorithm took to run.
		/// </summary>
		public long? Miliseconds { get; set; }

		/// <summary>
		/// Gets the number of times that values have been swapped in the array.
		/// </summary>
		public long? Swaps { get; set; }

		/// <summary>
		/// Gets the number of times that values have been compared.
		/// </summary>
		public long? Compares { get; set; }

		/// <summary>
		/// Gets the number of times that the internal array has been accessed through the indexer.
		/// </summary>
		public long? Retrievals { get; set; }

		/// <summary>
		/// Gets the number of times that a value has been set in the internal array through the indexer.
		/// </summary>
		public long? Sets { get; set; }

		/// <summary>
		/// Gets or sets the sorted items.
		/// </summary>
		public int[] SortedItems { get; set; }

		/// <summary>
		/// Gets or sets the name of the sorting algorithm that produced this result.
		/// </summary>
		public string AlgorithmName { get; set; }

		private Tuple<string, string>[] Values
		{
			get
			{
				// Group Values and Headers 
				var values = new[]
				{
					new Tuple<string, string>("Name", AlgorithmName),
					new Tuple<string, string>("Count", SortedItems.Length.ToString()),
					Miliseconds != null ? new Tuple<string, string>("MS",        Miliseconds.ToString()) : null,
					Compares != null ? new Tuple<string, string>("Compares",     Compares.ToString()) : null,
					Swaps != null ? new Tuple<string, string>("Swaps",           Swaps.ToString()) : null,
					Retrievals != null ? new Tuple<string, string>("Retrievals", Retrievals.ToString()) : null,
					Sets != null ? new Tuple<string, string>("Sets",             Sets.ToString()) : null
				};

				return values.Where(v => v != null).ToArray();
			}
		}

		public int MaxColumWidth
		{
			get
			{
				// Calculate column widths
				return Values.Max(v => v.Item2.Length) + 2;
			}
		}

		public string ToString(bool writeHeader = true, int? maxColumnWidth = null)
		{
			StringBuilder stringBuilder = new StringBuilder();

			maxColumnWidth = maxColumnWidth.HasValue ? maxColumnWidth : MaxColumWidth;

			string format = " {0," + maxColumnWidth + "} |";

			// Header
			if (writeHeader)
			{
				foreach (var value in Values)
				{
					stringBuilder.AppendFormat(format, value.Item1);
				}
				stringBuilder.AppendLine();
			}

			foreach (var value in Values)
			{
				stringBuilder.AppendFormat(format, value.Item2);
			}

			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			return this.ToString(writeHeader: true, maxColumnWidth: MaxColumWidth);
		}
	}

	/// <summary>
	/// Defines a class that represents an array that counts different transactions.
	/// </summary>
	[Obsolete("Use SortingResult now.")]
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

		public CountingArray(CountingArray<T> other)
		{
			this.items = new T[items.Length];
			other.items.CopyTo(this.items, 0);
			this.Swaps = other.Swaps;
			this.Compares = other.Compares;
			this.Sets = other.Sets;
			this.Retrievals = other.Retrievals;
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
