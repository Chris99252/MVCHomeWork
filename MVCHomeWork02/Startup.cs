using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCHomeWork02.Startup))]
namespace MVCHomeWork02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
