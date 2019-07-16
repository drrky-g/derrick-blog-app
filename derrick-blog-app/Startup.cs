using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(derrick_blog_app.Startup))]
namespace derrick_blog_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
