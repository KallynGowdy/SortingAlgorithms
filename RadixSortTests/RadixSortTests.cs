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
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(RadixSort.RadixSortImplementation.SortByLeastSignificantDigit, 10000));
        }

        [TestMethod]
        public void TestRadixSortByLeastSignificantDigitAndSwap()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(RadixSort.RadixSortImplementation.SortByLeastSignificantDigitAndSwap, 10));
        }

        [TestMethod]
        public void TestRadixSortByLeastSignificantDigitAndSwapWithCounting()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithmWithCounting(RadixSort.RadixSortImplementation.SortByLeastSignificantDigitAndSwap, 10));
        }
    }
}
