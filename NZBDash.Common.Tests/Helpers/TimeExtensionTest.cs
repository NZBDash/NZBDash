#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: TimeExtensionTest.cs
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
    public class TimeExtensionTest
    {
        [TestCaseSource("Data")]
        public void ConvertFromUnitTime(int unix, DateTime expected)
        {
            var result = unix.UnixTimeStampToDateTime();
            Assert.That(result, Is.EqualTo(expected));
        }

        public TestCaseData[] Data =
        {
            new TestCaseData(1448544679, new DateTime(2015,11,26,13,31,19)),
            new TestCaseData(1448546503, new DateTime(2015,11,26,14,1,43)),
            new TestCaseData(1324684799, new DateTime(2011,12,23,23,59,59)),
        };

    }
}
