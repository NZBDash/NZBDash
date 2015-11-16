using System;
using NUnit.Framework;

namespace NZBDash.Common.Tests
{
    [TestFixture]
    public class MemorySizeConverterTest
    {
        [TestCase(1024, "1.0 MB")]
        [TestCase(2048, "2.0 MB")]
        [TestCase(4879456, "4.7 GB")]
        [TestCase(10485760, "10.0 GB")]
        [TestCase(104857600000, "97.7 TB")]
        public void SizeSuffix(Int64 input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffix(input);
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
