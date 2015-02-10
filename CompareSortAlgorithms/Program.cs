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
        public const int Rounds = 4;

        static void Main(string[] args)
        {
            var algorithms = new[]
            {
                new Tuple<string, Func<CountingArray<int>, CountingArray<int>>>("Bubble Sort", BubbleSortImplementation.BubbleSortWithSwapFlag),
                new Tuple<string, Func<CountingArray<int>, CountingArray<int>>>("Insertion Sort", InsertionSortImplementation.InsertionSortWithSwap)
            };

            Console.WriteLine("Transaction Counter: ");

            CompareAlgorithms(algorithms);

            Console.WriteLine();

            Console.WriteLine("Press Any Key To Quit...");
            Console.Read();
        }

        static void CompareAlgorithms(params Tuple<string, Func<CountingArray<int>, CountingArray<int>>>[] algorithms)
        {
            var times = algorithms.Select(a => new Tuple<string, Tuple<long, long>[]>(a.Item1, TestAlgorithm(a.Item1, a.Item2, false))).ToArray();

            Console.WriteLine("Round X | {0, 10} | " + String.Join(" | ", times.Select(a => a.Item1)), "Count");
            for (int i = 0; i < Rounds; i++)
            {
                Console.Write("Round {0} | {1, 10} | ", i, (StartingAmountOfNumbers * Math.Pow(Factor, i)));
                for (int c = 0; c < times.Length; c++)
                {

                    string labelFormat = string.Format("{0} Swaps - {1} Compares |",  times[c].Item2[i].Item1, times[c].Item2[i].Item2);
                    Console.Write("{0, 40}", labelFormat);
                }
                Console.WriteLine();
            }
        }

        static void CompareAlgorithms<T>(params Tuple<string, Func<int[], int[]>>[] algorithms)
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

        static long[] TestAlgorithm<T>(string name, Func<int[], int[]> sortAlgorithm, bool printProgress = true)
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
                int[] linqOrderedNumbers = sortAlgorithm(numbers);
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

        static Tuple<long, long>[] TestAlgorithm(string name, Func<CountingArray<int>, CountingArray<int>> sortAlgorithm, bool printProgress = true)
        {
            int currentAmount = StartingAmountOfNumbers;
            Tuple<long, long>[] linqTimes = new Tuple<long,long>[Rounds];
            for (int c = 0; c < Rounds; c++)
            {
                int[] numbers = new int[currentAmount];

                Random r = new Random();

                // Generate a bunch of numbers
                for (int i = 0; i < currentAmount; i++)
                {
                    numbers[i] = r.Next(int.MinValue, int.MaxValue);
                }



                CountingArray<int> linqOrderedNumbers = sortAlgorithm(new CountingArray<int>(numbers));


                if (printProgress)
                {
                    Console.WriteLine("{0} took {1} swaps and {2} compares.", name, linqOrderedNumbers.Swaps, linqOrderedNumbers.Compares);
                    Console.WriteLine();
                }

                linqTimes[c] = new Tuple<long, long>(linqOrderedNumbers.Swaps, linqOrderedNumbers.Compares);

                currentAmount *= Factor;
            }

            return linqTimes;
        }
    }
}
