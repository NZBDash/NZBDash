#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: LinksConfigurationControllerTests.cs
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

using Moq;

using NUnit.Framework;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Controllers;

using Ploeh.AutoFixture;


namespace NZBDash.UI.Test.Controllers
{
    [TestFixture]
    public class LinksConfigurationControllerTests
    {
        private LinksConfigurationController _controller;
        private LinksConfigurationDto _dto;
        private IEnumerable<LinksConfigurationDto> _dtos;

        [SetUp]
        public void MockSetup()
        {
            var f = new Fixture();
            var mockLinks = new Mock<ILinksConfiguration>();
            _dto = f.Create<LinksConfigurationDto>();
            _dtos = f.Build<LinksConfigurationDto>().With(x => x.LinkEndpoint, "http://www.google.com").CreateMany();

            mockLinks.Setup(x => x.AddLink(It.IsAny<LinksConfigurationDto>())).Returns(_dto);
            mockLinks.Setup(x => x.GetLinks()).Returns(_dtos);
            mockLinks.Setup(x => x.RemoveLink(It.IsAny<int>()));
            mockLinks.Setup(x => x.UpdateLink(It.IsAny<LinksConfigurationDto>())).Returns(true);

            _controller = new LinksConfigurationController(mockLinks.Object);
        }

        [Test]
        public void Index()
        {
            var result = _controller.Index();

        }
    }
}
