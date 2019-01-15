using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(prjProyecto.Startup))]
namespace prjProyecto
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
