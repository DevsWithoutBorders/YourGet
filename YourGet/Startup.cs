using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YourGet.Startup))]
namespace YourGet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
//Test
            ConfigureAuth(app);
        }
    }
}
