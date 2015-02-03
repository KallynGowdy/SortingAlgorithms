using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MergeSort;

namespace MergeSortTests
{
    [TestClass]
    public class MergeSortTests
    {
        [TestMethod]
        public void TestMergeSortWithRecursion()
        {
            TestHelpers.TestHelpers.TestAlgorithm(MergeSortImplementation.SortUsingRecursion, 10000);
        }
    }
}
