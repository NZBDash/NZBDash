using System.Data.Entity.Migrations;

namespace NZBDash.DataAccess
{
    internal sealed class Configuration : DbMigrationsConfiguration<NZBDashContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "NZBDash.DataAccess.NZBDashContext";
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
