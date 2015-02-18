using CountingArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelpers
{
	/// <summary>
	/// Defines a static class of helpers for sort algorithm tests.
	/// </summary>
	public static class TestHelpers
	{
		/// <summary>
		/// Tests the given algorithm which accepts an array of integers with the number of given numbers.
		/// </summary>
		/// <param name="algo">The algorithm that should be tested.</param>
		/// <param name="n">The number of numbers that the algorithm should be tested to.</param>
		/// <returns>Returns whether the algorithm passed the test.</returns>
		public static bool TestAlgorithm(Func<int[], int[]> algo, int n)
		{
			int[] numbers = new int[n];

			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = i - (n / 2);
			}

			Random r = new Random();

			int[] unsorted = numbers.OrderBy(a => r.Next(int.MinValue, int.MaxValue)).ToArray();

			int[] sorted = algo(unsorted);

			bool correct = sorted.SequenceEqual(numbers);

			Assert.IsTrue(correct);

			return correct;
		}

		/// <summary>
		/// Tests the given algorithm which accepts an array of integers with the number of given numbers.
		/// </summary>
		/// <param name="algo">The algorithm that should be tested.</param>
		/// <param name="n">The number of numbers that the algorithm should be tested to.</param>
		/// <returns>Returns whether the algorithm passed the test.</returns>
		public static bool TestAlgorithmWithCounting(Func<CountingArray<int>, CountingArray<int>> algo, int n)
		{
			int[] numbers = new int[n];

			for (int i = 0; i < numbers.Length; i++)
			{
				numbers[i] = i;
			}

			Random r = new Random();

			CountingArray<int> unsorted = new CountingArray<int>(numbers.OrderBy(a => r.Next()).ToArray());

			CountingArray<int> sorted = algo(unsorted);

			return sorted.SequenceEqual(numbers);
		}
	}
}
