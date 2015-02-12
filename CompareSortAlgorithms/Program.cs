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
using InsertionSort;

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
            var algorithms = new[]
            {
                //new Tuple<string, Func<int[], int[]>>("Bubble Sort", BubbleSortImplementation.BubbleSortWithoutSwap),           
                //new Tuple<string, Func<int[], int[]>>("Insertion Sort", InsertionSortImplementation.InsertionSortWithSwap),
                //new Tuple<string, Func<int[], int[]>>("Selection Sort", SelectionSortImplementation.SelectionSortBySwap),
                new Tuple<string, Func<int[], IEnumerable<int>>>("Merge Sort By Kal", MergeSortImplementation.SortUsingLoops),
                new Tuple<string, Func<int[], IEnumerable<int>>>("Merge Sort /W Recursion", MergeSortImplementation.SortUsingRecursion),
                new Tuple<string, Func<int[], IEnumerable<int>>>("Merge Sort By Book", MergeSortImplementation.SortUsingBookMethod),
                new Tuple<string, Func<int[], IEnumerable<int>>>("OrderBy", n => n.OrderBy(v => v)),
            };

            Console.WriteLine("Kals timer: ");

            CompareAlgorithms<KalTimer>(algorithms);

            Console.WriteLine();

            Console.WriteLine("System timer: ");

            CompareAlgorithms<SystemTimer>(algorithms);

            Console.WriteLine("Press Any Key To Quit...");
            Console.Read();
        }

        static void CompareAlgorithms<T>(params Tuple<string, Func<int[], IEnumerable<int>>>[] algorithms)
            where T : ITimer, new()
        {
            Tuple<string, long[]>[] times = algorithms.Select(a => new Tuple<string, long[]>(a.Item1, TestAlgorithm<T>(a.Item1, a.Item2, false))).ToArray();

            Console.WriteLine("Round X | {0, 10} | " + String.Join(" | ", times.Select(a => a.Item1)), "Count");
            for (int i = 0; i < Rounds; i++)
            {
                Console.Write("Round {0} | {1, 10} | ", i, (StartingAmountOfNumbers * Math.Pow(Factor, i)));
                for (int c = 0; c < times.Length; c++)
                {
                    string labelFormat = string.Format("{{0, {0}}}ms | ", times[c].Item1.Length - 2);
                    Console.Write(labelFormat, times[c].Item2[i]);
                }
                Console.WriteLine();
            }
        }

        static long[] TestAlgorithm<T>(string name, Func<int[], IEnumerable<int>> sortAlgorithm, bool printProgress = true)
            where T : ITimer, new()
        {
            int currentAmount = StartingAmountOfNumbers;
            long[] linqTimes = new long[Rounds];
            for (int c = 0; c < Rounds; c++)
            {
                int[] numbers = new int[currentAmount];

                Random r = new Random();

                // Generate a bunch of numbers
                for (int i = 0; i < currentAmount; i++)
                {
                    numbers[i] = r.Next(int.MinValue, int.MaxValue);
                }

                T linqWatch = new T();
                linqWatch.Start();
                IEnumerable<int> linqOrderedNumbers = sortAlgorithm(numbers).ToArray();
                linqWatch.Stop();
                if (printProgress)
                {
                    Console.WriteLine("{0} took {1}ms to sort {2} random numbers.", name, linqWatch.TotalMiliseconds, currentAmount);
                    Console.WriteLine();
                }

                linqTimes[c] = (long)linqWatch.TotalMiliseconds;

                currentAmount *= Factor;
            }

            Console.WriteLine("{0} is done being tested. It took {1}ms total.", name, linqTimes.Sum());

            return linqTimes;
        }
    }
}
