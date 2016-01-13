#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: ThirdPartySerializerTest.cs
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
// ************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.IO;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Tests.Helpers
{
    [TestFixture]
    public class ThirdPartySerializerTest
    {
        [Test]
        public void JsonSerializer()
        {
            var jsonData = File.ReadAllText("TestData/Class.json");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(jsonData);

            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializedJsonData<RootObject>(jsonData);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void JsonSerializerException()
        {
            var jsonData = File.ReadAllText("TestData/Class.json");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Throws<Exception>();

            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializedJsonData<RootObject>(jsonData);
        }

        [Test]
        public void JsonSerializerWithFunc()
        {
            var jsonData = File.ReadAllText("TestData/Class.json");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.UploadString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(jsonData);

            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializedJsonData(jsonData, "POST", () => new RootObject());

            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void JsonSerializerWithFuncException()
        {
            var jsonData = File.ReadAllText("TestData/Class.json");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.UploadString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializedJsonData(jsonData, "POST", () => new RootObject());
        }

        [Test]
        public void XmlSerializer()
        {
            var xmlData = File.ReadAllText("TestData/Class.xml");
            var mockWebClient = new Mock<IWebClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(xmlData);

            var seralizer = new ThirdPartySerializer(mockWebClient.Object);
            var result = seralizer.SerializeXmlData<RootObject>(xmlData);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }
    }

    public class Phones
    {
        public string home { get; set; }
        public string mobile { get; set; }
    }

    public class Person
    {
        public string dateOfBirth { get; set; }
        public List<string> email { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Phones phones { get; set; }
        public bool registered { get; set; }
    }

    public class RootObject
    {
        public bool demo { get; set; }
        public Person person { get; set; }
        public string releaseDate { get; set; }
        public double version { get; set; }
    }
}