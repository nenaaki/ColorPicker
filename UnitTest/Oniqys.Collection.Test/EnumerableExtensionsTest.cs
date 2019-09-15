
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Oniqys.Collection.Test
{
    [TestClass]
    public class EnumerableExtensionsTest
    {
        [TestMethod]
        public void ToArrayFastTest()
        {
            int[] values = new int[1000];
            for (int idx = 0; idx < values.Length; idx++)
            {
                values[idx] = idx;
            }
            var result = values.ToArrayFast();

            Assert.AreEqual(values.Length, result.Length);

            for (int idx = 0; idx < values.Length; idx++)
            {
                Assert.AreEqual(values[idx], result[idx]);
            }
        }
    }
}
