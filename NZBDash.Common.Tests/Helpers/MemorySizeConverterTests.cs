#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: MemorySizeConverterTests.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;

using NUnit.Framework;

using NZBDash.Common.Helpers;

namespace NZBDash.Common.Tests.Helpers
{
    [TestFixture]
    public class MemorySizeConverterTests
    {
        [TestCase(1, "1 KB")]
        [TestCase(1024, "1 MB")]
        [TestCase(2048, "2 MB")]
        [TestCase(4879456, "4.7 GB")]
        [TestCase(10485760, "10 GB")]
        [TestCase(104857600000, "97.7 TB")]
        public void SizeSuffix(Int64 input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffix(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(1, "1 KB/s")]
        [TestCase(1024, "1 MB/s")]
        [TestCase(2048, "2 MB/s")]
        [TestCase(4879456, "4.7 GB/s")]
        [TestCase(10485760, "10 GB/s")]
        [TestCase(104857600000, "97.7 TB/s")]
        public void SizeSuffixTime(Int64 input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffixTime(input);
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase(1024, "1 GB")]
        [TestCase(3817, "3.7 GB")]
        [TestCase(2048, "2 GB")]
        [TestCase(4879456, "4.7 TB")]
        [TestCase(10485760, "10 TB")]
        public void SizeSuffixMb(Int64 input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffixMb(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase("10.0 MB", 10)]
        [TestCase("1 GB", 1024)]
        [TestCase("5.3 GB", 5427.2)]
        public void ConvertToMbTest(string input, double expected)
        {
            var result = MemorySizeConverter.ConvertToMb(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(1024, "1 GB")]
        [TestCase(3817, "3.7 GB")]
        [TestCase(2048, "2 GB")]
        [TestCase(4879456, "4.7 TB")]
        [TestCase(10485760, "10 TB")]
        public void SizeSuffixMb(double input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffixMb(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(1, "1 KB")]
        [TestCase(1024, "1 MB")]
        [TestCase(2048, "2 MB")]
        [TestCase(4879456, "4.7 GB")]
        [TestCase(10485760, "10 GB")]
        [TestCase(104857600000, "97.7 TB")]
        public void SizeSuffixLong(long input, string expected)
        {
            var result = MemorySizeConverter.SizeSuffix(input);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}