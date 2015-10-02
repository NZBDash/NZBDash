using System.Collections.Generic;
using System.Linq;

using NZBDash.DataAccess;
using NZBDash.DataAccess.Models;

namespace NZBDash.Core
{
    public class Setup
    {

        public bool ApplicationConfigurationExist()
        {
            var supportedApps = db.SupportedApplications.ToList();
            var appConfig = db.ApplicationConfiguration.ToList();
            
            return supportedApps.Any() || appConfig.Any();
        }

        public int Destroy()
        {
            var config = db.AdminConfiguration.ToList();
            var app = db.ApplicationConfiguration.ToList();
            var supp = db.SupportedApplications.ToList();
            var email = db.EmailConfiguration.ToList();
            var link = db.LinksConfiguration.ToList();

            db.AdminConfiguration.RemoveRange(config);
            db.LinksConfiguration.RemoveRange(link);
            db.ApplicationConfiguration.RemoveRange(app);
            db.SupportedApplications.RemoveRange(supp);
            db.EmailConfiguration.RemoveRange(email);
            return db.SaveChanges();
        }

        public NZBDashContext db = new NZBDashContext();

        public void SetupNow()
        {
            var apps = new List<SupportedApplications>
            {
                new SupportedApplications { Name = "SabNzb" },
                new SupportedApplications { Name = "Plex" },
                new SupportedApplications { Name = "Sonarr" },
                new SupportedApplications { Name = "Sickbeard" },
                new SupportedApplications { Name = "Kodi" },
                new SupportedApplications { Name = "NzbGet" },
                new SupportedApplications { Name = "CouchPotato" },
            };
            db.SupportedApplications.AddRange(apps);

            db.SaveChanges();
        }
    }
}
