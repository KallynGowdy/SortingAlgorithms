using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RadixSortTests
{
    [TestClass]
    public class RadixSortTests
    {
        [TestMethod]
        public void TestRadixSortByLeastSignificantDigit()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(RadixSort.RadixSortImplementation.SortLsdFirstTry, 10000));
        }

        [TestMethod]
        public void TestRadixSortLsdChars()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(RadixSort.RadixSortImplementation.SortLsdByChar, 10000));
        }

		[TestMethod]
		public void TestRadixSortLsdBase2()
		{
			Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(RadixSort.RadixSortImplementation.SortLsdBase2, 10000));
		}

		[TestMethod]
		public void TestRadixSortLsdBase2SortingResult()
		{
			Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(nums => RadixSort.RadixSortImplementation.SortLsdBase2Counting(nums).SortedItems, 10000));
		}

		[TestMethod]
		public void TestRadixSortLsdFirstTrySortingResult()
		{
			Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(nums => RadixSort.RadixSortImplementation.SortLsdFirstTryCounting(nums).SortedItems, 10000));
		}

		[TestMethod]
		public void TestRadixSortLsdCharsSortingResult()
		{
			Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(nums => RadixSort.RadixSortImplementation.SortLsdByCharCounting(nums).SortedItems, 10000));
		}
	}
}
