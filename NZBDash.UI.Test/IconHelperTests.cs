using NUnit.Framework;

using NZBDash.UI.Helpers;
using NZBDash.UI.Models.Dashboard;

namespace NZBDash.UI.Test
{
    [TestFixture]
    public class IconHelperTests
    {
        [TestCase(DownloadStatus.DOWNLOADING, "fa-download")]
        [TestCase(DownloadStatus.EXECUTING_SCRIPT, "fa-question")]
        [TestCase(DownloadStatus.PAUSED, "fa-pause")]
        [TestCase(DownloadStatus.FETCHING, "fa-spinner")]
        [TestCase(DownloadStatus.LOADING_PARS, "fa-spinner")]
        [TestCase(DownloadStatus.MOVING, "fa-arrow-right")]
        [TestCase(DownloadStatus.QUEUED, "fa-clock-o")]
        public void TestIcon(DownloadStatus status, string output)
        {
            var result = IconHelper.ChooseIcon(status);

            Assert.That(result, Is.EqualTo(output));
        }
    }
}
