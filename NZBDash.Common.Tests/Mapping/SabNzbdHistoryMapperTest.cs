﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SabNzbdHistoryMapperTest.cs
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
using System.Globalization;

using NUnit.Framework;

using NZBDash.Common.Helpers;
using NZBDash.Common.Mapping;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;
using NZBDash.UI.Models.ViewModels.NzbGet;

using Omu.ValueInjecter;

using Ploeh.AutoFixture;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    public class SabNzbdHistoryMapperTest
    {
        [Test]
        public void SabNzbdHistoryMapper()
        {
            var f = new Fixture();
            var source = f.Build<Slot>().With(x => x.size, "20 MB").Create();
            var target = new NzbGetHistoryViewModel();
            target.InjectFrom(new SabNzbdHistoryMapper(), source);

            Assert.That(target.Id, Is.EqualTo(source.id));
            Assert.That(target.NzbName, Is.EqualTo(source.nzb_name));
            Assert.That(target.Status, Is.EqualTo(source.status));
            Assert.That(target.FileSize, Is.EqualTo(MemorySizeConverter.ConvertToMb(source.size).ToString(CultureInfo.CurrentUICulture)));

            Assert.That(target.Id, Is.EqualTo(source.id));
            Assert.That(target.Category, Is.EqualTo(source.category));
            Assert.That(target.Category, Is.EqualTo(source.category));

        }
    }
}
