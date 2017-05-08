using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleKatanaServer
{
    /// <summary>
    /// Katana looks for a class named Startup in namespace matching the assembly name or the global namespace.
    /// <see cref="https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-startup-class-detection" />
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
            {
                context.Response.ContentType = "text/plain";
                return context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
