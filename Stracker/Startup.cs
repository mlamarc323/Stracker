using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stracker.Startup))]
namespace Stracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
