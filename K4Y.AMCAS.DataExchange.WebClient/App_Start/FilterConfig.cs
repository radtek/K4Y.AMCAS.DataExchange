using System.Web;
using System.Web.Mvc;

namespace K4Y.AMCAS.DataExchange.WebClient
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
