using System.Linq;

using Octokit;

namespace NZBDash.Services.UpdateService
{
    public class Updater
    {
        public Updater(IGitHubClient client)
        {
            Git = new GitHubClient(new ProductHeaderValue("NZBDash-Updater"));
        }
        private IGitHubClient Git { get; set; }
        private const string Owner = "NZBDash";
        private const string RepoName = "NZBDash";
        
        public Release GetLatestRelease()
        {
            var releases = Git.Repository.Release.GetAll(Owner, RepoName);
            return releases.Result.FirstOrDefault();
        }
    }
}
