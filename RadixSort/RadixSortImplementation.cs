using CountingArray;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace RadixSort
{
	/// <summary>
	/// Defines a static class that contains implementations of the Radix/Bucket sort.
	/// </summary>
	public static class RadixSortImplementation
	{
		/// <summary>
		/// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
		/// 
		/// Note that for negative numbers to work we need to make another pass after we've sorted the numbers to filter out and reverse the negatives.
		/// 
		/// This version sorts the numbers by putting the numbers into a seperate array that does not get affected by the transactions
		/// and uses the output array for each of the encountered numbers.
		/// Could potentially be sped up by converting the numbers to strings and comparing their characters instead of dividing them and performing modulus operations.
		/// </summary>
		/// <param name="numbers">The numbers that should be sorted.</param>
		/// <returns></returns>
		public static int[] SortLsdFirstTry(int[] numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");
			int[] output = new int[numbers.Length];
			numbers.CopyTo(output, 0);

			int digit = 0;
			bool hadDigit = false;
			do
			{
				int[] currentNumbers = new int[output.Length];
				output.CopyTo(currentNumbers, 0);
				int currentPlace = 0;
				hadDigit = false;

				for (int c = 0; c < 10; c++)
				{
					for (int i = 0; i < currentNumbers.Length; i++)
					{
						int n = currentNumbers[i];
						int d = (int)Math.Pow(10, digit);
						int v = (n / d);
						if (v % 10 == c)
						{
							output[currentPlace++] = n;
							if (v != 0)
								hadDigit = true;
						}
						else if (v % 10 == -c)
						{
							output[currentPlace++] = n;
							if (v != 0)
								hadDigit = true;
						}
					}
				}
				digit++;
			} while (hadDigit);

			Stack<int> negatives = new Stack<int>(output.Length);

			for (int i = 0; i < output.Length; i++)
			{
				int n = output[i];
				if (n < 0)
				{
					negatives.Push(n);
				}
			}

			return negatives.Concat(output.Where(n => n >= 0)).ToArray();
		}

		/// <summary>
		/// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
		/// 
		/// Note that for negative numbers to work we need to make another pass after we've sorted the numbers to filter out and reverse the negatives.
		/// 
		/// This version sorts the numbers by putting the numbers into a seperate array that does not get affected by the transactions
		/// and uses the output array for each of the encountered numbers.
		/// Could potentially be sped up by converting the numbers to strings and comparing their characters instead of dividing them and performing modulus operations.
		/// </summary>
		/// <param name="numbers">The numbers that should be sorted.</param>
		/// <returns>
		/// Returns a tuple that contains the sorted array as the first item, the number of retrievals as the second item, and the number
		/// of sets in the third item.
		/// </returns>
		public static SortingResult SortLsdFirstTryCounting(int[] numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");
			int[] output = new int[numbers.Length];
			numbers.CopyTo(output, 0);

			long gets = 0;
			long sets = 0;

			int digit = 0;
			bool hadDigit = false;
			do
			{
				int[] currentNumbers = new int[output.Length];
				output.CopyTo(currentNumbers, 0);
				int currentPlace = 0;
				hadDigit = false;

				for (int c = 0; c < 10; c++)
				{
					for (int i = 0; i < currentNumbers.Length; i++)
					{
						int n = currentNumbers[i];
						gets++;
						int d = (int)Math.Pow(10, digit);
						int v = (n / d);
						if (v % 10 == c)
						{
							output[currentPlace++] = n;
							sets++;
							if (v != 0)
								hadDigit = true;
						}
						else if (v % 10 == -c)
						{
							output[currentPlace++] = n;
							sets++;
							if (v != 0)
								hadDigit = true;
						}
					}
				}
				digit++;
			} while (hadDigit);

			Stack<int> negatives = new Stack<int>(output.Length);

			for (int i = 0; i < output.Length; i++)
			{
				int n = output[i];
				gets++;
				if (n < 0)
				{
					negatives.Push(n);
					sets++;
				}
			}

			return new SortingResult()
			{
				Retrievals = gets,
				Sets = sets,
				SortedItems = negatives.Concat(output.Where(n => n >= 0)).ToArray(),
				AlgorithmName = "Radix First Try"
			};
		}


		/// <summary>
		/// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
		/// 
		/// This implementation is very similar to the one above, however it uses char objects to try to avoid costly division. The attempt was
		/// interesting. In order to avoid calling ToString() and string.Length and ToCharArray() many times I had to cache the values, so
		/// when the numbers are given they are instantly copied into a Tuple array that contains the actual number, number of characters in the
		/// string representation of the number and the actual characters. Doing this first makes ToString() a O(n) operation. Once we've converted the
		/// values all we need to do is just sort them normally. The big difference is that we no longer need to perform exponentiation or modulo,
		/// but rather just some subtraction to determine where the value goes.
		/// 
		/// For 1,000,000 items, it ends up being close to 0.9 seconds faster than the previous version.
		/// </summary>
		/// <param name="numbers">The numbers that should be sorted.</param>
		/// <returns>Returns a new array containing the sorted numbers.</returns>
		public static int[] SortLsdByChar(int[] numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");
			Tuple<int, int, char[]>[] output = new Tuple<int, int, char[]>[numbers.Length];
			for (int i = 0; i < numbers.Length; i++)
			{
				int n = numbers[i];
				char[] s = (n < 0 ? -n : n).ToString().ToCharArray();
				output[i] = new Tuple<int, int, char[]>(n, s.Length, s);
			}

			int digit = 0;
			bool hadDigit = false;

			do
			{
				var currentNumbers = new Tuple<int, int, char[]>[output.Length];
				output.CopyTo(currentNumbers, 0);
				int currentPlace = 0;
				hadDigit = false;

				for (char c = '0'; c <= '9'; c++) // Use the character values instead of the integer values because comparisions can
												  // be made directly without any sort of conversion (i.e. subtracting 48)
				{
					for (int i = 0; i < currentNumbers.Length; i++)
					{
						var n = currentNumbers[i];
						int l = n.Item2;
						char ch = digit < l ? n.Item3[l - digit - 1] : '0';
						if (ch == c)
						{
							output[currentPlace++] = n;
							if (ch != '0')
								hadDigit = true;
						}
					}
				}

				digit++;
			} while (hadDigit);

			Stack<int> negatives = new Stack<int>(output.Length);

			for (int i = 0; i < output.Length; i++)
			{
				int n = output[i].Item1;
				if (n < 0)
				{
					negatives.Push(n);
				}
			}

			return negatives.Concat(output.Select(n => n.Item1).Where(n => n >= 0)).ToArray();
		}

		/// <summary>
		/// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
		/// 
		/// This implementation is very similar to the one above, however it uses char objects to try to avoid costly division. The attempt was
		/// interesting. In order to avoid calling ToString() and string.Length and ToCharArray() many times I had to cache the values, so
		/// when the numbers are given they are instantly copied into a Tuple array that contains the actual number, number of characters in the
		/// string representation of the number and the actual characters. Doing this first makes ToString() a O(n) operation. Once we've converted the
		/// values all we need to do is just sort them normally. The big difference is that we no longer need to perform exponentiation or modulo,
		/// but rather just some subtraction to determine where the value goes.
		/// 
		/// For 1,000,000 items, it ends up being close to 0.9 seconds faster than the previous version.
		/// </summary>
		/// <param name="numbers">The numbers that should be sorted.</param>
		/// <returns>Returns a new array containing the sorted numbers.</returns>
		public static SortingResult SortLsdByCharCounting(int[] numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");
			Tuple<int, int, char[]>[] output = new Tuple<int, int, char[]>[numbers.Length];
			for (int i = 0; i < numbers.Length; i++)
			{
				int n = numbers[i];
				char[] s = (n < 0 ? -n : n).ToString().ToCharArray();
				output[i] = new Tuple<int, int, char[]>(n, s.Length, s);
			}
			long gets = 0;
			long sets = 0;
			int digit = 0;
			bool hadDigit = false;

			do
			{
				var currentNumbers = new Tuple<int, int, char[]>[output.Length];
				output.CopyTo(currentNumbers, 0);
				int currentPlace = 0;
				hadDigit = false;

				for (char c = '0'; c <= '9'; c++) // Use the character values instead of the integer values because comparisions can
												  // be made directly without any sort of conversion (i.e. subtracting 48)
				{
					for (int i = 0; i < currentNumbers.Length; i++)
					{
						var n = currentNumbers[i];
						gets++;
						int l = n.Item2;
						char ch = digit < l ? n.Item3[l - digit - 1] : '0';
						if (ch == c)
						{
							output[currentPlace++] = n;
							sets++;
							if (ch != '0')
								hadDigit = true;
						}
					}
				}

				digit++;
			} while (hadDigit);

			Stack<int> negatives = new Stack<int>(output.Length);

			for (int i = 0; i < output.Length; i++)
			{
				int n = output[i].Item1;
				gets++;
				if (n < 0)
				{
					negatives.Push(n);
					sets++;
				}
			}

			return new SortingResult()
			{
				AlgorithmName = "Radix Sort By Char",
				SortedItems = negatives.Concat(output.Select(n => n.Item1).Where(n => n >= 0)).ToArray(),
				Retrievals = gets,
				Sets = sets
			};
		}

		/// <summary>
		/// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
		/// 
		/// This implementation piggy backs off of the 'ByChar' variant, but decides to sort by each byte instead of each character.
		/// This could have a big performance gain since it only uses bit shifts and bitwise AND operations. It lives in the strange world of 
		/// Base 2, but it works with any number you can throw at it.
		/// </summary>
		/// <param name="numbers">The numbers that should be sorted.</param>
		/// <returns>Returns a new array that contains the sorted numbers.</returns>
		public static int[] SortLsdBase2(int[] numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");
			uint[] nums = numbers.Select(n => ((uint)n) + int.MaxValue).ToArray(); // Eliminate the negative numbers

			// We can probably make this real fast by using bit shifts and the bitwise AND operation
			// If we work in base 2 compared to base 10, then we can use the bit shifts to remove our last digit
			// and we can use the AND operator to check if the digit is high or low (i.e. in place of the modulus operator)

			// To get the "first" digit of a value (Remember, for Base 2 this means either 0 or 1 for our buckets):

			// 1). Take a number
			// 2). Bit shift it by our current place
			// 3). Then AND the number with 1 to get the last (least significant) bit
			// 4). Then put the number in either the 1 or 0 place
			for (int digit = 0; digit < 32; digit++) // Start from the lowest order bit
			{
				uint[] currentNumbers = new uint[nums.Length];
				nums.CopyTo(currentNumbers, 0);
				int currentPlace = 0;
				for (byte c = 0; c < 2; c++) // Once for 0 and once for 1
				{
					for (int i = 0; i < nums.Length; i++)
					{
						uint num = currentNumbers[i];
						uint lastBit = (num >> digit) & 1;	// Shift to our current digit and then get the last one
						if (lastBit == c)
						{
							nums[currentPlace++] = num;
						}
					}
				}
			}

			return nums.Select(n => (int)(n - int.MaxValue)).ToArray();	// Reintroduce the negatives
		}

		/// <summary>
		/// Sorts the given array of numbers into a new array using the Radix/Bin/Bucket sorting algorithm by the Least Significant Digit.
		/// 
		/// This implementation piggy backs off of the 'ByChar' variant, but decides to sort by each byte instead of each character.
		/// This could have a big performance gain since it only uses bit shifts and bitwise AND operations. It lives in the strange world of 
		/// Base 2, but it works with any number you can throw at it.
		/// </summary>
		/// <param name="numbers">The numbers that should be sorted.</param>
		/// <returns>Returns a new array that contains the sorted numbers.</returns>
		public static SortingResult SortLsdBase2Counting(int[] numbers)
		{
			if (numbers == null) throw new ArgumentNullException("numbers");
			uint[] nums = numbers.Select(n => ((uint)n) + int.MaxValue).ToArray(); // Eliminate the negative numbers

			long gets = 0;
			long sets = 0;

			// We can probably make this real fast by using bit shifts and the bitwise AND operation
			// If we work in base 2 compared to base 10, then we can use the bit shifts to remove our last digit
			// and we can use the AND operator to check if the digit is high or low (i.e. in place of the modulus operator)

			// To get the "first" digit of a value (Remember, for Base 2 this means either 0 or 1 for our buckets):

			// 1). Take a number
			// 2). Bit shift it by our current place
			// 3). Then AND the number with 1 to get the last (least significant) bit
			// 4). Then put the number in either the 1 or 0 place
			for (int digit = 0; digit < 32; digit++) // Start from the lowest order bit
			{
				uint[] currentNumbers = new uint[nums.Length];
				nums.CopyTo(currentNumbers, 0);
				int currentPlace = 0;
				for (byte c = 0; c < 2; c++) // Once for 0 and once for 1
				{
					for (int i = 0; i < nums.Length; i++)
					{
						uint num = currentNumbers[i];
						gets++;
						uint lastBit = (num >> digit) & 1;	// Shift to our current digit and then get the last one
						if (lastBit == c)
						{
							nums[currentPlace++] = num;
							sets++;
						}
					}
				}
			}

			return new SortingResult()
			{
				AlgorithmName = "Radix Base 2",
				SortedItems = nums.Select(n => (int)(n - int.MaxValue)).ToArray(),	// Reintroduce the negatives
				Retrievals = gets,
				Sets = sets
			};
		}
	}
}
