using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Elmah;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;
using YourGet.Infrastructure;

[assembly: OwinStartup(typeof(YourGet.Startup))]
namespace YourGet
{
    public partial class Startup
    {
        public bool HasRun { get; set; };

        // This method is auto-detected by the OWIN pipeline. DO NOT RENAME IT!
        public void Configuration(IAppBuilder app)
        {
            // Tune ServicePointManager
            // (based on http://social.technet.microsoft.com/Forums/en-US/windowsazuredata/thread/d84ba34b-b0e0-4961-a167-bbe7618beb83 and https://msdn.microsoft.com/en-us/library/system.net.servicepointmanager.aspx)
            ServicePointManager.DefaultConnectionLimit = 500;
            ServicePointManager.UseNagleAlgorithm = false;
            ServicePointManager.Expect100Continue = false;

            // Register IoC
            app.UseAutofacInjection();
            var dependencyResolver = DependencyResolver.Current;

            // Register Elmah
            var elmahServiceCenter = new DependencyResolverServiceProviderAdapter(dependencyResolver);
            ServiceCenter.Current = _ => elmahServiceCenter;

            // Configure logging
            app.SetLoggerFactory(new DiagnosticsLoggerFactory());

            // Remove X-AspNetMvc-Version header
            MvcHandler.DisableMvcResponseHeader = true;

            // Catch unobserved exceptions from threads before they cause IIS to crash:
            TaskScheduler.UnobservedTaskException += (sender, exArgs) =>
            {
                // Send to ELMAH
                try
                {
                    HttpContext current = HttpContext.Current;
                    if (current != null)
                    {
                        var errorSignal = ErrorSignal.FromContext(current);
                        if (errorSignal != null)
                        {
                            errorSignal.Raise(exArgs.Exception, current);
                        }
                    }
                }
                catch (Exception)
                {
                    // more tragedy... swallow Exception to prevent crashing IIS
                }

                exArgs.SetObserved();
            };

            HasRun = true;
        }
    }
}
