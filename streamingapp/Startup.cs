using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(streamingapp.Startup))]
namespace streamingapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
