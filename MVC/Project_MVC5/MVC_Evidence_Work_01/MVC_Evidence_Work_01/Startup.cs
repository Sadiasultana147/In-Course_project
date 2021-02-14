using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(MVC_Evidence_Work_01.Startup))]

namespace MVC_Evidence_Work_01
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
            AuthenticationType =DefaultAuthenticationTypes.ApplicationCookie,
            LoginPath=new PathString("/Accounts/Login")
            });
        }
    }
}
