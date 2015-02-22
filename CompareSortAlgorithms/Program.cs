// 2015 Kallyn Gowdy
// Bubble Sort

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelectionSort;
using KalsTimer;
using MergeSort;
using CountingArray;
using InsertionSort;
using RadixSort;

namespace BubbleSort
{
	class Program
	{
		/// <summary>
		/// The number of numbers that should be used to start.
		/// </summary>
		public const int StartingAmountOfNumbers = 10;

		/// <summary>
		/// The factor that the number of numbers to use should be increased by.
		/// </summary>
		public const int Factor = 10;

		/// <summary>
		/// The number of rounds to run.
		/// </summary>
		public const int Rounds = 6;

		static void Main(string[] args)
		{
			Console.WriteLine("Radix Sort: ");
			Console.WriteLine();

			CompareAlgorithms<SystemTimer>
			(
				RadixSortImplementation.SortLsdFirstTryCounting,
				RadixSortImplementation.SortLsdByCharCounting,
				RadixSortImplementation.SortLsdBase2Counting
            );

			Console.WriteLine("Press Any Key To Quit...");
			Console.Read();
		}

		static void CompareAlgorithms<TTimer>(params Func<int[], SortingResult>[] algorithms)
			where TTimer : ITimer, new()
		{
			var times = algorithms.SelectMany(TestAlgorithm<TTimer>).ToArray();

			bool writeHeader = true;

			int maxWidth = times.Max(t => t.MaxColumWidth);

			foreach (var time in times)
			{
				Console.WriteLine(time.ToString(writeHeader, maxWidth));
				writeHeader = false;
			}
		}

		static SortingResult[] TestAlgorithm<TTimer>(Func<int[], SortingResult> sortAlgorithm)
			where TTimer : ITimer, new()
		{
			int currentAmount = StartingAmountOfNumbers;
			SortingResult[] times = new SortingResult[Rounds];
			for (int c = 0; c < Rounds; c++)
			{
				int[] numbers = new int[currentAmount];

				Random r = new Random();

				// Generate a bunch of numbers
				for (int i = 0; i < currentAmount; i++)
				{
					numbers[i] = r.Next(int.MinValue, int.MaxValue);
				}

				TTimer timer = new TTimer();
				timer.Start();
				SortingResult result = sortAlgorithm(numbers);
				timer.Stop();

				result.Miliseconds = (long)timer.TotalMiliseconds;
				times[c] = result;

				currentAmount *= Factor;
			}

			return times;
		}


	}
}
