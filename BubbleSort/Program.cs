// 2015 Kallyn Gowdy
// Bubble Sort

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public const int Rounds = 7;

        static void Main(string[] args)
        {

            TestAlgorithm("LINQ's OrderBy", numbers => numbers.OrderBy(n => n).ToArray());

            Console.WriteLine("Press Any Key To Quit...");
            Console.Read();
        }

        static void CompareAlgorithms(params Tuple<string, Func<int[], int[]>>[] algorithms)
        {
            Tuple<string, long[]>[] times = algorithms.Select(a => new Tuple<string, long[]>(a.Item1, TestAlgorithm(a.Item1, a.Item2))).ToArray();

            for (int i = 0; i < Rounds; i++)
            {
                Console.WriteLine("Round {0}| " + String.Join("|", times.Select(a => a.Item1)));

                for (int c = 0; c < times.Length; c++)
                {


                }
            }
        }

        static long[] TestAlgorithm(string name, Func<int[], int[]> sortAlgorithm)
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

                Console.WriteLine("{0} took {1}ms to sort {2} random numbers.", name, linqWatch.ElapsedMilliseconds, currentAmount);
                Console.WriteLine();

                linqTimes[c] = linqWatch.ElapsedMilliseconds;

                currentAmount *= Factor;
            }

            Console.WriteLine("{0} is done being tested. It took {1}ms total.", name, linqTimes.Sum());

            return linqTimes;
        }
    }
}
