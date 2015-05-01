using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SelectionSort;
using TestHelpers;

namespace SelectionSortTests
{
    [TestClass]
    public class SelectionSortTests
    {
        [TestMethod]
        public void TestSelectionSortWithoutList()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(SelectionSortImplementation.SelectionSortWithoutList, 10000));
        }

        [TestMethod]
        public void TestSelectionSortWithList()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(SelectionSortImplementation.SelectionSortWithList, 10000));
        }

        [TestMethod]
        public void TestSelectionSortBySwap()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(SelectionSortImplementation.SelectionSortBySwap, 10000));
        }

        [TestMethod]
        public void TestSelectionSortBySwapAndCount()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(n => SelectionSortImplementation.SelectionSortBySwapWithSortingResult(n).SortedItems, 10000));
        }
    }
}
