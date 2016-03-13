#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: UiTestBase.cs
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
using System.Data.Common;

using Moq;

using NZBDash.Common.Interfaces;
using NZBDash.Core;
using NZBDash.DataAccessLayer.Interfaces;

namespace NZBDash.UI.Test
{
    public class UiTestBase
    {

        public void CreateMappings()
        {
            var sqlMock = new Mock<ISqliteConfiguration>();
            var logger = new Mock<ILogger>();
            var dbProvider = new Mock<DbProviderFactory>();
            var s = new Setup(sqlMock.Object, logger.Object, dbProvider.Object);
            s.SetupMappers();
        }
    }
}
