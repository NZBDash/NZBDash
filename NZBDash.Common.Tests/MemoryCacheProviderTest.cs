#region Copyright
// ************************************************************************
//   Copyright (c) 2015 
//   File: MemoryCacheProviderTest.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************
#endregion
using NUnit.Framework;

using NZBDash.Common.Interfaces;

using Ploeh.AutoFixture;

namespace NZBDash.Common.Tests
{
    [TestFixture]
    public class MemoryCacheProviderTest
    {
        [SetUp]
        public void Setup()
        {
            Cache = new MemoryCacheProvider();
            F = new Fixture();
            Key = "Test";
        }

        private ICacheProvider Cache { get; set; }
        private Fixture F { get; set; }
        private string Key { get; set; }

        [Test]
        public void AddToCache()
        {
            var item = F.Create<object>();
            Cache.Set(Key, item, 2);

            Assert.That(Cache.Get<object>(Key), Is.Not.Null);
        }

        [Test]
        public void GetOrSetNewItem()
        {
            AddToCache();

            var item = Cache.GetOrSet<object>("NewKey", () => string.Empty, 5);
            Assert.That(item, Is.Not.Null);
            Assert.That(item, Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetOrSetExistingItem()
        {
            AddToCache();

            var item = Cache.GetOrSet<object>(Key, null, 5);

            Assert.That(item, Is.Not.Null);
        }

        [Test]
        public void RemoveFromCache()
        {
            AddToCache();
            Cache.Remove(Key);

            Assert.That(Cache.Get<object>(Key), Is.Null);
        }
    }
}