#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: CpuCounterTest.cs
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
using Moq;
using NUnit.Framework;
using NZBDash.Common;
using NZBDash.Core.Interfaces;
using NZBDash.UI.Helpers;

namespace NZBDash.UI.Test.Helpers
{
    [TestFixture]
    public class CpuCounterTest
    {
        [Test]
        public void TestCounter()
        {
            var h = new Mock<IHardwareService>();
            h.Setup(x => x.GetCpuPercentage()).Returns(99);

            var cpu = new CpuCounter(h.Object);

            cpu.StartCounter();

            Assert.That(CpuCounter.Counter.Count, Is.EqualTo(1));
        }

        [Test]
        public void TestCounterRemove()
        {
            var h = new Mock<IHardwareService>();
            h.Setup(x => x.GetCpuPercentage()).Returns(99);

            var cpu = new CpuCounter(h.Object);

            for (var i = 0; i < 99; i++)
            {
                cpu.StartCounter();
            }
            
            Assert.That(CpuCounter.Counter.Count, Is.EqualTo(60));
        }
    }
}
