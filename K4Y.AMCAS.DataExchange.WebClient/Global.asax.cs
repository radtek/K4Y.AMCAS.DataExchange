using K4Y.AMCAS.DataExchange.DataStore;
using K4Y.AMCAS.DataExchange.RestApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace K4Y.AMCAS.DataExchange.WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["apiClient"] = ApiClientFactory.Create(ApiClientTypes.Mock);
            Application["amcasRepository"] = new AmcasRepository();
        }
    }
}
