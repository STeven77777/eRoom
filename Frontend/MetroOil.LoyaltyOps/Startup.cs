using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MetroOil.LoyaltyOps.Startup))]
namespace MetroOil.LoyaltyOps
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
