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
            application.BeginRequest +=
                (new EventHandler(this.Application_BeginRequest));
            application.EndRequest +=
                (new EventHandler(this.Application_EndRequest));
        }

        private void Application_BeginRequest(Object source,
             EventArgs e)
        {
            // Create HttpApplication and HttpContext objects to access
            // request and response properties.
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            context.Response.Write("<hr><h3><font color=grey>HelloWorldModule: Beginning of Request</ font ></ h3 >< hr > ");
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            context.Response.Write("<hr><h3><font color=grey>HelloWorldModule: End of Request </ font ></ h3 > ");
        }

        public void Dispose()
        {
        }
    }
}