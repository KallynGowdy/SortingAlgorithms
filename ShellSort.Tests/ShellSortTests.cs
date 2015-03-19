using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ShellSort.Tests
{
    [TestClass]
    public class ShellSortTests
    {
        [TestMethod]
        public void TestShellSort()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(ShellSortImplementation.Sort, 100000));
        }

        [TestMethod]
        public void TestShellSortSortingResult()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(i => ShellSortImplementation.SortWithSortingResult(i).SortedItems, 100000));
        }
    }
}
