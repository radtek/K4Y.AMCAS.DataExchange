using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4Y.AMCAS.DataExchange.WebHandlers
{
    public class ErrorHandlerModule : IHttpModule
    {
        public ErrorHandlerModule()
        {
        }

        public String ModuleName
        {
            get { return "ErrorHandlerModule"; }
        }

        // In the Init function, register for HttpApplication 
        // events by adding your handlers.
        public void Init(HttpApplication application)
        {
            application.PostAcquireRequestState += 
                new EventHandler(Application_PostAcquireRequestState);
        }

        private void Application_PostAcquireRequestState(object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            //Session state should be available Now
            logParsedApplications(app);
        }

        private void logParsedApplications(HttpApplication app)
        {
            HttpContext context = app.Context;
            List<Application> parsedApplications = null;
            string log = "init log";
            try
            {
                parsedApplications = app.Session["parsedApplications"] as List<Application>;
            }
            catch (Exception ex)
            {
                log = log = String.Format("; parsedApplications exception: {0}", ex.ToString());
            }

            log = String.Format("parsedApplications: {0}",
                parsedApplications == null ? "null" : parsedApplications.Count().ToString());
            context.Response.Write(log);
        }

        public void Dispose()
        {
        }
    }
}