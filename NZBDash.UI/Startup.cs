using Microsoft.Owin;

using NZBDash.Core;

using Owin;

[assembly: OwinStartupAttribute(typeof(NZBDash.UI.Startup))]
namespace NZBDash.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            // If we don't have any configuration, create a blank config.
            var configCheck = new Setup();

            if (!configCheck.ApplicationConfigurationExist())
            {
                configCheck.SetupNow();
            }
        }
    }
}
