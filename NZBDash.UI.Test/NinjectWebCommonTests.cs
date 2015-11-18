using System;

using NUnit.Framework;

using NZBDash.UI.App_Start;

namespace NZBDash.UI.Test
{
    [TestFixture]
    public class NinjectWebCommonTests
    {
        [Test]
        public void CreateKernelTest()
        {
            Assert.DoesNotThrow(NinjectWebCommon.Start);
            Assert.DoesNotThrow(NinjectWebCommon.Stop);
        }
    }
}
