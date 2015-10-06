using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using NZBDash.DataAccess.Models;
using NZBDash.DataAccess.Models.Settings;

namespace NZBDash.DataAccess
{
    public class NZBDashContext : DbContext 
    {
        public NZBDashContext()
            : base("NZBDashConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NZBDashContext, Configuration>("NZBDashConnection"));
        }

        public DbSet<AdminConfiguration> AdminConfiguration { get; set; }
        public DbSet<ApplicationConfiguration> ApplicationConfiguration { get; set; }
        public DbSet<LinksConfiguration> LinksConfiguration { get; set; }
        public DbSet<EmailConfiguration> EmailConfiguration { get; set; }
        public DbSet<SupportedApplications> SupportedApplications { get; set; }
        public DbSet<DashboardGrid> DashboardGrid { get; set; }
        public DbSet<Applications> Applications { get; set; }

        // Settings Pages
        public DbSet<CouchPotatoSettings> CouchPotatoSettings { get; set; }
        public DbSet<NzbGetSettings> NzbGetSettings { get; set; }
        public DbSet<SabNzbSettings> SabNzbSettings { get; set; }
        public DbSet<SonarrSettings> SonarrSettings { get; set; }
        public DbSet<PlexSettings> PlexSettings { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This is a personal preference
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
