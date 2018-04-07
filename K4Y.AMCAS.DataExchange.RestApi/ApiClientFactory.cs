using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4Y.AMCAS.DataExchange.RestApi
{
    public static class ApiClientFactory
    {
        public static IApiClient Create(ApiClientTypes clientType)
        {
            switch (clientType)
            {
                case ApiClientTypes.DotNet:
                    return new ApiClient();
                case ApiClientTypes.Curl:
                    return new CurlApiClient();
                case ApiClientTypes.Mock:
                default:
                    return new MockApiClient();
            }

        }
    }
}
