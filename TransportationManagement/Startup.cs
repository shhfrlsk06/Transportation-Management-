using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransportationManagement.Startup))]
namespace TransportationManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
