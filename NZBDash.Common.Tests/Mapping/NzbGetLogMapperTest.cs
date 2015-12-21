#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: NzbGetLogMapperTest.cs
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

using NZBDash.Common.Helpers;
using NZBDash.Common.Mapping;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.UI.Models.ViewModels.NzbGet;

using Omu.ValueInjecter;

namespace NZBDash.Common.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class NzbGetLogMapperTest
    {
        [TestCaseSource("TestCaseData")]
        public void NzbGetLogMapper(int id, int time, string kind, string text)
        {
            var source = new LogResult
            {
                ID = id,
                Time = time,
                Kind = kind,
                Text = text
            };
            var target = new NzbGetLogViewModel();
            target.InjectFrom(new NzbGetLogMapper(), source);

            Assert.That(target.Id, Is.EqualTo(id));
            Assert.That(target.Kind, Is.EqualTo(kind));
            Assert.That(target.Text, Is.EqualTo(text));
            Assert.That(target.Time, Is.EqualTo(time.UnixTimeStampToDateTime()));

        }

        private static IEnumerable<ITestCaseData> TestCaseData
        {
            get
            {
                var emptyStrings = new string[0];

                yield return
                    new TestCaseData(1,
                        1448544643,
                        "a",
                        "a"
                        );

                yield return
                new TestCaseData(99999,
                    213123354,
                    "WARNING",
                    "Yo"
                    );

            }
        }
    }
}
