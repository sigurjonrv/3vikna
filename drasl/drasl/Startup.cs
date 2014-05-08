using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(drasl.Startup))]
namespace drasl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
