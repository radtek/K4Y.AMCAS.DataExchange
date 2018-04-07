using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace K4Y.AMCAS.DataExchange.RestApi
{
    public class CurlApiClient : BaseApiClient
    {
        public override string GetApiResponseContent(MedicalInstitutions institution)
        {
            var psi = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + @"curl\curl.exe")
            {
                Arguments = @" --insecure --cacert curl-ca-bundle.crt -H ""Med-Inst-Id:870"" --cert client.pem:password https://ws-amcas.staging.aamc.org/amcas-data-service/applications",
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "curl",
                RedirectStandardOutput = true
            };
            var process = Process.Start(psi);
            string response = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return response;
        }
        public override List<Application> GetAllApplications(MedicalInstitutions institution)
        {
            string responseContent = GetApiResponseContent(institution);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseContent);
            return parseApplicationsXml(doc);
        }

        public override List<ApplicationData> GetApplicationList(MedicalInstitutions institution, string year)
        {
            var psi = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + @"curl\curl.exe")
            {
                Arguments = @" --insecure --cacert curl-ca-bundle.crt -H ""Med-Inst-Id:870"" --cert client.pem:password https://ws-amcas.staging.aamc.org/amcas-data-service/applications/list/" + year,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "curl",
                RedirectStandardOutput = true
            };
            var process = Process.Start(psi);
            string response = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            return parseApplicationListXml(doc);
        }

        public override List<Application> GetSingleApplication(MedicalInstitutions institution, string year, string AAMCID)
        {
            var psi = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + @"curl\curl.exe")
            {
                Arguments = @" --insecure --cacert curl-ca-bundle.crt -H ""Med-Inst-Id:870"" --cert client.pem:password https://ws-amcas.staging.aamc.org/amcas-data-service/applications/list/" + year + "/" + AAMCID,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + "curl",
                RedirectStandardOutput = true
            };
            var process = Process.Start(psi);
            string response = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(response);
            return parseApplicationsXml(doc);
        }
    }
}
