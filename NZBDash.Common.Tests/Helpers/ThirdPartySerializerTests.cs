#region Copyright
// /************************************************************************
//   Copyright (c) 2016 Jamie Rees
//   File: ThirdPartySerializerTests.cs
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

using Moq;

using NUnit.Framework;

using NZBDash.Common.Helpers;
using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Tests.Helpers
{
    [TestFixture]
    public class ThirdPartySerializerTests
    {
        public string JsonData = "{  \"version\": 3.1,\"releaseDate\": \"2014-06-25T00:00:00.000Z\",\"demo\": true,\"person\": {\"id\": 12345,\"name\": \"John Doe\",\"phones\": {\"home\": \"800-123-4567\",\"mobile\": \"877-123-1234\"},\"email\": [\"jd@example.com\",\"jd@example.org\"],\"dateOfBirth\": \"1980-01-02T00:00:00.000Z\",\"registered\": true}}";
        public string XmlData = "<?xml version=\"1.0\" encoding=\"utf-16\"?><RootObject xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><version>3.1</version><releaseDate>2014-06-25T00:00:00.000Z</releaseDate><demo>true</demo><person><id>12345</id><name>John Doe</name><phones><home>800-123-4567</home><mobile>877-123-1234</mobile></phones><email>  <string>jd @example.com</string>  <string>jd @example.org</string></email><dateOfBirth>1980-01-02T00:00:00.000Z</dateOfBirth><registered>true</registered></person></RootObject>";
        [Test]
        public void JsonSerializer()
        {
            
            var mockWebClient = new Mock<IHttpClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(JsonData);

            var serializer = new ThirdPartySerializer(mockWebClient.Object);
            var result = serializer.SerializedJsonData<RootObject>(JsonData);

            mockWebClient.Verify(x => x.DownloadString(JsonData),Times.Once);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }

        [Test]
        public void JsonSerializerException()
        {
           
            var mockWebClient = new Mock<IHttpClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Throws<Exception>();

            var serializer = new ThirdPartySerializer(mockWebClient.Object);
            Assert.Throws(
                typeof(Exception),
                () =>
                {
                   serializer.SerializedJsonData<RootObject>(JsonData);
                });
        }

        [Test]
        public void JsonSerializerWithFunc()
        {
           
            var mockWebClient = new Mock<IHttpClient>();

            mockWebClient.Setup(x => x.UploadString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(JsonData);

            var serializer = new ThirdPartySerializer(mockWebClient.Object);
            var result = serializer.SerializedJsonData<RootObject, string>(JsonData, "POST", () => "abc");

            mockWebClient.Verify(x => x.UploadString(JsonData,"POST","abc"),Times.Once);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.version, Is.EqualTo(3.1));
            Assert.That(result.releaseDate, Is.EqualTo("2014-06-25T00:00:00.000Z"));
            Assert.That(result.demo, Is.True);
            Assert.That(result.person.id, Is.EqualTo(12345));
        }

        [Test]
        public void JsonSerializerWithFuncException()
        {
            var mockWebClient = new Mock<IHttpClient>();

            mockWebClient.Setup(x => x.UploadString(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<Exception>();

            var serializer = new ThirdPartySerializer(mockWebClient.Object);
            Assert.Throws(typeof(Exception), () =>
            {
                serializer.SerializedJsonData<RootObject, int>(JsonData, "POST", () => 2);
            });
        }

        [Test]
        public void XmlSerializer()
        {
           var mockWebClient = new Mock<IHttpClient>();

            mockWebClient.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(XmlData);

            var serializer = new ThirdPartySerializer(mockWebClient.Object);
            var result = serializer.SerializeXmlData<RootObject>(XmlData);

            mockWebClient.Verify(x => x.DownloadString(XmlData), Times.Once);
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