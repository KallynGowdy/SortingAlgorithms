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
            CompareAlgorithms(new[]
            {
                new Tuple<string, Func<int[], int[]>>("Bubble Sort /W Swap", BubbleSortImplementation.BubbleSortWithSwapFlag),
                new Tuple<string, Func<int[], int[]>>("Bubble Sort /WO Swap", BubbleSortImplementation.BubbleSortWithoutSwap)    ,
                new Tuple<string, Func<int[], int[]>>("Selection Sort /W Array", SelectionSortImplementation.SelectionSortWithoutList),            
                new Tuple<string, Func<int[], int[]>>("Selection Sort /W List", SelectionSortImplementation.SelectionSortWithList),            
                new Tuple<string, Func<int[], int[]>>("Selection Sort /W Swap", SelectionSortImplementation.SelectionSortBySwap),
            });

            Console.WriteLine("Press Any Key To Quit...");
            Console.Read();
        }

        static void CompareAlgorithms(params Tuple<string, Func<int[], int[]>>[] algorithms)
        {
            Tuple<string, long[]>[] times = algorithms.AsParallel().Select(a => new Tuple<string, long[]>(a.Item1, TestAlgorithm(a.Item1, a.Item2, false))).ToArray();

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

        static long[] TestAlgorithm(string name, Func<int[], int[]> sortAlgorithm, bool printProgress = true)
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

                Stopwatch linqWatch = Stopwatch.StartNew();
                int[] linqOrderedNumbers = sortAlgorithm(numbers);
                linqWatch.Stop();
                if (printProgress)
                {
                    Console.WriteLine("{0} took {1}ms to sort {2} random numbers.", name, linqWatch.ElapsedMilliseconds, currentAmount);
                    Console.WriteLine();
                }

                linqTimes[c] = linqWatch.ElapsedMilliseconds;

                currentAmount *= Factor;
            }

            Console.WriteLine("{0} is done being tested. It took {1}ms total.", name, linqTimes.Sum());

            return linqTimes;
        }
    }
}
