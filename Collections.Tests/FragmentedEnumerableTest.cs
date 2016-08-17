using NUnit.Framework;
using System.Linq;

namespace Collections.Tests
{
    [TestFixture]
    public class FragmentedEnumerableTest
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(100)]
        [TestCase(1000000)]
        [TestCase(10000000)]
        public void TestCase(int itemCount)
        {
            int[] expected = Enumerable.Range(0, itemCount)
                                       .Select((x, i) => i)
                                       .ToArray();
            var fragmented = new FragmentedEnumerable<int>(expected, 13);
            int[] actual = fragmented.ToArray();
            Assert.IsTrue(expected.SequenceEqual(actual));
        }
    }
}

