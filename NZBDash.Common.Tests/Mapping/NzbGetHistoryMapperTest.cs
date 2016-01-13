#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: NzbGetHistoryMapperTest.cs
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using NUnit.Framework;

using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.UI.Models.ViewModels.NzbGet;
using NZBDash.Common.Mapping;

using Omu.ValueInjecter;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class NzbGetHistoryMapperTest
    {
        [TestCaseSource("TestCaseData")]
        public void NzbGetHistoryMapper(int id, string nzbName, int fileSize, string category, int health, int historyTime, string status, string name)
        {
            var source = new NzbGetHistoryResult
            {
                ID = id,
                NZBName = nzbName,
                FileSizeMB = fileSize,
                Category = category,
                Health = health,
                HistoryTime = historyTime,
                Status = status,
                Name = name
            };
            var target = new NzbGetHistoryViewModel();
            target.InjectFrom(new NzbGetHistoryMapper(), source);

            Assert.That(target.Id, Is.EqualTo(id));
            Assert.That(target.NzbName, Is.EqualTo(nzbName));
            Assert.That(target.FileSize, Is.EqualTo(fileSize.ToString()));
            Assert.That(target.Category, Is.EqualTo(category));
            Assert.That(target.Health, Is.EqualTo(health));
            Assert.That(target.HistoryTime, Is.EqualTo(historyTime));
            Assert.That(target.Status, Is.EqualTo(status));
            Assert.That(target.Name, Is.EqualTo(name));
        }

        private static IEnumerable<ITestCaseData> TestCaseData
        {
            get
            {
                var emptyStrings = new string[0];

                yield return
                    new TestCaseData(1,
                        "Name",
                        2042,
                        "Cat1",
                        97,
                        2000,
                        "Running",
                        "name2");

                yield return
                    new TestCaseData(1,
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName",
                        int.MaxValue,
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName",
                        int.MaxValue,
                        int.MaxValue,
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName",
                        "LongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongNameLongName");
            }
        }
    }
}
