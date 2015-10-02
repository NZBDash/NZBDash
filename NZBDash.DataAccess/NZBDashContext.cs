using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using NZBDash.DataAccess.Models;

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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This is a personal preference
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
