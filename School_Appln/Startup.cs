using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School_Appln.Startup))]
namespace School_Appln
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
