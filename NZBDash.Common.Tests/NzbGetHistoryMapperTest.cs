using NUnit.Framework;

using NZBDash.Api.Models;
using NZBDash.Common.Mapping;
using NZBDash.UI.Models.NzbGet;

using Omu.ValueInjecter;

namespace NZBDash.Common.Tests
{
    [TestFixture]
    public class NzbGetHistoryMapperTest
    {
        [Test]
        [Ignore]
        public void NzbGetHistoryMapper()
        {
            var source = new NzbGetHistoryResult
            {
                ID = 22,
                NZBName = "Name",
                FileSizeMB = 1024
            };
            var target = new NzbGetHistoryViewModel();
            target.InjectFrom(new NzbGetHistoryMapper(), source);

            Assert.That(target.Id,Is.EqualTo(source.ID));
            Assert.That(target.NzbName, Is.EqualTo(source.NZBName));
            Assert.That(target.FileSize, Is.EqualTo(source.FileSizeMB));

        }
    }
}
