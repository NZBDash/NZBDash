using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Moq;

using Octokit;

using Ploeh.AutoFixture;

namespace NZBDash.Services.UpdateService.Tests
{
    [TestFixture]
    public class UpdaterTests
    {
        [Test]
        [Ignore("Need to work out how to Mock out GitHubClient")]
        public void ReleaseCheck()
        {
            var expected = new Fixture().Create<IReadOnlyList<Release>>();
            var g = new Mock<IGitHubClient>();
            g.Setup(x => x.Repository.Release.GetAll(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(expected));
            var u = new Updater(g.Object);
            var result = u.GetLatestRelease();
        
            Assert.That(result, Is.Not.Null);
        }
    }
}
