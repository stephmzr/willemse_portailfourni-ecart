using System;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WillemseFranceMP.Startup))]
namespace WillemseFranceMP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                ConfigureAuth(app);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
