using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortailFournisseur.Startup))]
namespace PortailFournisseur
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
