// 2015 Kallyn Gowdy
// Bubble Sort

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelectionSort;
using KalsTimer;
using MergeSort;
using CountingArray;
using InsertionSort;
using Quicksort;
using RadixSort;
using ShellSort;

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
        public const int Rounds = 5;

        static void Main(string[] args)
        {
            Console.WriteLine(
@"----------------------------------------------
Already Sorted - Ascending:
-----------------------------------------------");
            Console.WriteLine();

            CompareAlgorithms<SystemTimer>
            (
                NumberGeneration.AlreadySorted_Ascending,

                BubbleSortImplementation.BubbleSortWithSwapFlagAndSortingResult,
                SelectionSortImplementation.SelectionSortBySwapWithSortingResult,
                InsertionSortImplementation.InsertionSortWithSwapSortingResult,
                MergeSortImplementation.SortUsingLoopsSortingResult,
                ShellSortImplementation.SortWithSortingResult,
                RadixSortImplementation.SortLsdBase2Counting,
                QuickSortImplementation.SortWithSortingResult
            );

            Console.WriteLine();
            Console.WriteLine(
@"----------------------------------------------
Already Sorted - Descending:
-----------------------------------------------");
            Console.WriteLine();

            CompareAlgorithms<SystemTimer>
            (
                NumberGeneration.AlreadySorted_Descending,

                BubbleSortImplementation.BubbleSortWithSwapFlagAndSortingResult,
                SelectionSortImplementation.SelectionSortBySwapWithSortingResult,
                InsertionSortImplementation.InsertionSortWithSwapSortingResult,
                MergeSortImplementation.SortUsingLoopsSortingResult,
                ShellSortImplementation.SortWithSortingResult,
                RadixSortImplementation.SortLsdBase2Counting,
                QuickSortImplementation.SortWithSortingResult
            );

            Console.WriteLine();
            Console.WriteLine(
@"----------------------------------------------
Random:
-----------------------------------------------");
            Console.WriteLine();

            CompareAlgorithms<SystemTimer>
            (
                NumberGeneration.Random,

                BubbleSortImplementation.BubbleSortWithSwapFlagAndSortingResult,
                SelectionSortImplementation.SelectionSortBySwapWithSortingResult,
                InsertionSortImplementation.InsertionSortWithSwapSortingResult,
                MergeSortImplementation.SortUsingLoopsSortingResult,
                ShellSortImplementation.SortWithSortingResult,
                RadixSortImplementation.SortLsdBase2Counting,
                QuickSortImplementation.SortWithSortingResult
            );

            Console.WriteLine("Press Any Key To Quit...");
            Console.Read();
        }

        static void CompareAlgorithms<TTimer>(NumberGeneration generationType, params Func<int[], SortingResult>[] algorithms)
            where TTimer : ITimer, new()
        {
            var times = algorithms.SelectMany(a => TestAlgorithm<TTimer>(a, generationType)).ToArray();

            bool writeHeader = true;

            int maxWidth = times.Max(t => t.MaxColumWidth);

            foreach (var time in times)
            {
                Console.WriteLine(time.ToString(writeHeader, maxWidth));
                writeHeader = false;
            }
        }

        static SortingResult[] TestAlgorithm<TTimer>(Func<int[], SortingResult> sortAlgorithm, NumberGeneration generationType)
            where TTimer : ITimer, new()
        {
            int currentAmount = StartingAmountOfNumbers;
            SortingResult[] times = new SortingResult[Rounds];
            for (int c = 0; c < Rounds; c++)
            {
                int[] numbers = new int[currentAmount];

                switch (generationType)
                {
                case NumberGeneration.AlreadySorted_Ascending:
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        numbers[i] = i - (numbers.Length / 2);
                    }
                    break;
                case NumberGeneration.AlreadySorted_Descending:
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        numbers[i] = (numbers.Length / 2) - i;
                    }
                    break;
                case NumberGeneration.Random:
                    Random rng = new Random();

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        numbers[i] = rng.Next(int.MinValue, int.MaxValue);
                    }
                    break;
                }

                TTimer timer = new TTimer();
                timer.Start();
                SortingResult result = sortAlgorithm(numbers);
                timer.Stop();

                result.Miliseconds = (long)timer.TotalMiliseconds;
                times[c] = result;

                Debug.Assert(numbers.OrderBy(n => n).SequenceEqual(result.SortedItems), "The items were not properly sorted.");

                currentAmount *= Factor;
            }

            return times;
        }


    }

    public enum NumberGeneration
    {
        AlreadySorted_Ascending,
        AlreadySorted_Descending,
        Random
    }
}
