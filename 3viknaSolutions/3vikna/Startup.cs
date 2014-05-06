using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_3vikna.Startup))]
namespace _3vikna
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
