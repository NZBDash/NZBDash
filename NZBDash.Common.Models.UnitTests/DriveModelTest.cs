using NUnit.Framework;

using NZBDash.Common.Models.Hardware;

namespace NZBDash.Common.Models.UnitTests
{
    [TestFixture]
    public class DriveModelTest
    {
        [Test]
        public void GetDriveModelPercentage()
        {
            var model = new DriveModel { TotalFreeSpace = 1024, TotalSize = 2048 };
            Assert.That(model.PercentageFilled, Is.EqualTo(50));
        }
    }
}
