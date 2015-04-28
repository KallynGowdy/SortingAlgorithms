using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuickSort.Tests
{
    [TestClass]
    public class QuickSortTests
    {
        [TestMethod]
        public void TestQuickSort()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(items => Quicksort.QuickSortImplementation.Sort(items), 100000));
        }
    }
}
