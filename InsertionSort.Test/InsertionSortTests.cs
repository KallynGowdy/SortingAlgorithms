using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InsertionSort.Test
{
    [TestClass]
    public class InsertionSortTests
    {
        [TestMethod]
        public void TestInsertionSortWithSwap()
        {
            Assert.IsTrue(TestHelpers.TestHelpers.TestAlgorithm(InsertionSortImplementation.InsertionSortWithSwap, 10000));
        }
    }
}
