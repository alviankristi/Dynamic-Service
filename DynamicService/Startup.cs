using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DynamicService.Startup))]
namespace DynamicService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
