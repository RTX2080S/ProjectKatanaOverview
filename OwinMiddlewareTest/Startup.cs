using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
using System.Web;
using Microsoft.Owin.Extensions;

[assembly: OwinStartup(typeof(OwinMiddlewareTest.Startup))]

namespace OwinMiddlewareTest
{
    public class Startup
    {
        /// The output window will display: 
        /// Current IIS event: AuthenticateRequest Msg: Middleware 1
        /// Current IIS event: AuthenticateRequest Msg: 2nd MW
        /// Current IIS event: ResolveRequestCache Msg: 3rd MW
        /// <see cref="https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-middleware-in-the-iis-integrated-pipeline"/>
        public void Configuration(IAppBuilder app)
        {
            app.Use((context, next) =>
            {
                PrintCurrentIntegratedPipelineStage(context, "Middleware 1");
                return next.Invoke();
            });
            app.Use((context, next) =>
            {
                PrintCurrentIntegratedPipelineStage(context, "2nd MW");
                return next.Invoke();
            });

            /// Configures all the previously registered middleware components (in this case, our two diagnostic components) 
            /// to run on the authentication stage of the pipeline
            app.UseStageMarker(PipelineStage.Authenticate);

            app.Run(context =>
            {
                PrintCurrentIntegratedPipelineStage(context, "3rd MW");
                return context.Response.WriteAsync("Hello world \nPlease check your Visual Studio output window for Debug output");
            });

            /// The EARLIEST stage of calls to app.UseStageMarker wins. 
            /// For example, if you switch the order of app.UseStageMarker calls from this example,
            /// The output window will display: 
            /// Current IIS event: AuthenticateRequest Msg: Middleware 1
            /// Current IIS event: AuthenticateRequest Msg: 2nd MW
            /// Current IIS event: AuthenticateRequest Msg: 3rd MW
            app.UseStageMarker(PipelineStage.ResolveCache);
        }

        private void PrintCurrentIntegratedPipelineStage(IOwinContext context, string msg)
        {
            var currentIntegratedpipelineStage = HttpContext.Current.CurrentNotification;
            context.Get<TextWriter>("host.TraceOutput").WriteLine($"Current IIS event: { currentIntegratedpipelineStage } Msg: { msg }");
        }
    }
}
