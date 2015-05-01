using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Diagnostics;

namespace BubbleSort.Tests
{
    [TestClass]
    public class BubbleSortTests
    {
        [TestMethod]
        public void TestBubbleSortWithSwap()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(BubbleSortImplementation.BubbleSortWithSwapFlag, 10000));
        }

        [TestMethod]
        public void TestBubbleSortWithoutSwap()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(BubbleSortImplementation.BubbleSortWithoutSwap, 10000));
        }

        [TestMethod]
        public void TestBubbleSortWithSwapAndCounting()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(n => BubbleSortImplementation.BubbleSortWithSwapFlagAndSortingResult(n).SortedItems, 10000));
        }
    }
}
