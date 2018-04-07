using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using System.IO;

namespace K4Y.AMCAS.DataExchange.RestApi
{
    class MockApiClient : BaseApiClient
    {
        public override string GetApiResponseContent(MedicalInstitutions institution)
        {
            string content = "";
            Assembly a = Assembly.GetExecutingAssembly();
            Stream s = a.GetManifestResourceStream("K4Y.AMCAS.DataExchange.RestApi.Samples.Applications.xml");
            using (StreamReader reader = new StreamReader(s))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }
        public override List<Application> GetAllApplications(MedicalInstitutions institution)
        {
            List<Application>  applicationList = new List<Application>();

            Assembly a = Assembly.GetExecutingAssembly();
            Stream s = a.GetManifestResourceStream("K4Y.AMCAS.DataExchange.RestApi.Samples.Applications.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(s);

            return parseApplicationsXml(doc);
        }
        public override List<ApplicationData> GetApplicationList(MedicalInstitutions institution, string year)
        {
            List<Application> applicationList = new List<Application>();

            Assembly a = Assembly.GetExecutingAssembly();
            Stream s = a.GetManifestResourceStream("K4Y.AMCAS.DataExchange.RestApi.Samples.Applications2018.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(s);

            return parseApplicationListXml(doc);
        }
        public override List<Application> GetSingleApplication(MedicalInstitutions institution, string year, string AAMCID)
        {
            List<Application> applicationList = new List<Application>();

            Assembly a = Assembly.GetExecutingAssembly();
            Stream s = a.GetManifestResourceStream("K4Y.AMCAS.DataExchange.RestApi.Samples.SingleApplication.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(s);

            return parseApplicationsXml(doc);
        }
    }
}
