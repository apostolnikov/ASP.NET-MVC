using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AjaxHw.Startup))]
namespace AjaxHw
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
