using NUnit.Framework;

using NZBDash.Core.Services;

namespace NZBDash.Core.Test
{
    [TestFixture]
    public class WindowsHardwareServiceTests
    {
        [Test]
        public void GetDrives()
        {
            var service = new WindowsHardwareService();
            var result = service.GetDrives();

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetRam()
        {
            var service = new WindowsHardwareService();
            var result = service.GetRam();

            Assert.That(result, Is.Not.Null);
        }
    }
}
