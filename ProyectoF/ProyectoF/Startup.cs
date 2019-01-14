using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoF.Startup))]
namespace ProyectoF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
