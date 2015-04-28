using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MergeSort;
using System.Linq;

namespace MergeSortTests
{
    [TestClass]
    public class MergeSortTests
    {
        [TestMethod]
        public void TestMergeSortWithRecursion()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(MergeSortImplementation.SortUsingRecursion, 100000));
        }

        [TestMethod]
        public void TestMergeSortWithoutRecursion()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(MergeSortImplementation.SortUsingLoops, 100000));
        }

        [TestMethod]
        public void TestMergeSortUsingBook()
        {
			Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(MergeSortImplementation.SortUsingBookMethod, 100000));
        }

        [TestMethod]
        public void TestLinqOrderBy()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(n => n.OrderBy(v => v).ToArray(), 100000));
        }

    }
}
