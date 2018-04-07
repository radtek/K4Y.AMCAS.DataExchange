using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace K4Y.AMCAS.DataExchange.RestApi
{
    public class ApiClient : BaseApiClient
    {
        private HttpClient client = null;
        private String apiURL = "https://ws‐amcas.staging.aamc.org/amcas‐data‐service/"; // 143.220.15.63
        public ApiClient()
        {
            WebRequestHandler handler = new WebRequestHandler();
            String certificateFilePath = ".\\client.crt";
            X509Certificate2 certificate = new X509Certificate2(certificateFilePath);
            handler.ClientCertificates.Add(certificate);

            client = new HttpClient(handler);
            client.BaseAddress = new Uri(apiURL); 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        }
        public override string GetApiResponseContent(MedicalInstitutions institution)
        {
            return getApiResponseContentAsync(institution).Result;
        }
        public override List<Application> GetAllApplications(MedicalInstitutions institution)
        {
            return getApplicationListAsync(institution).Result;
        }
        private async Task<string> getApiResponseContentAsync(MedicalInstitutions institution)
        {
            string content = "";

            String query = String.Format("applications");
            HttpResponseMessage response = await client.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
            }

            return content;
        }
        private async Task<List<Application>> getApplicationListAsync(MedicalInstitutions institution)
        {
            List<Application> applications = new List<Application>();

            String query = String.Format("applications");
            HttpResponseMessage response = await client.GetAsync(query);
            if (response.IsSuccessStatusCode)
            {
                applications = await response.Content.ReadAsAsync<List<Application>>();
            }

            return applications;
        }

        public override List<ApplicationData> GetApplicationList(MedicalInstitutions institution, string year)
        {
            throw new NotImplementedException();
        }

        public override List<Application> GetSingleApplication(MedicalInstitutions institution, string year, string AAMCID)
        {
            throw new NotImplementedException();
        }
    }
}
