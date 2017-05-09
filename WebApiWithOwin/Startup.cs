using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IO;
using System.Web;
using Microsoft.Owin.Extensions;

[assembly: OwinStartup(typeof(WebApiWithOwin.Startup))]

namespace WebApiWithOwin
{
    public class Startup
    {
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
        }
        
        private void PrintCurrentIntegratedPipelineStage(IOwinContext context, string msg)
        {
            var currentIntegratedpipelineStage = HttpContext.Current.CurrentNotification;
            context.Get<TextWriter>("host.TraceOutput").WriteLine($"Current IIS event: { currentIntegratedpipelineStage } Msg: { msg }");
        }
    }
}
