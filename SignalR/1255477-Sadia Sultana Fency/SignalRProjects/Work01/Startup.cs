using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Work01.Startup))]

namespace Work01
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR<ChatConnection>("/chat");
        }
    }
}
