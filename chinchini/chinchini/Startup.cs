using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(chinchini.Startup))]
namespace chinchini
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
