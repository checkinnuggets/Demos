using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

// ReSharper disable once CheckNamespace 
namespace ShortlistManager.Web
{
    // for Owin discovery
    public class Startup 
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });
        }
    }
}