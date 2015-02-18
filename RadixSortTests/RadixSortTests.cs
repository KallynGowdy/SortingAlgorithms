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
    }
}
