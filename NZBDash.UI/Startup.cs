using Microsoft.Owin;

using NZBDash.UI;

using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace NZBDash.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
