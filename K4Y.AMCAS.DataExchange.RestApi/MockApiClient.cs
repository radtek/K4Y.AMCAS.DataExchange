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
    public class MockApiClient : IApiClient
    {
        public List<Application> GetApplicationList(MedicalInstitutions institution)
        {
            List<Application>  applicationList = new List<Application>();

            Assembly a = Assembly.GetExecutingAssembly();
            Stream s = a.GetManifestResourceStream("K4Y.AMCAS.DataExchange.RestApi.Samples.Applications.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(s);

            return parseApplicationsXml(doc);
        }
        public List<Application> ParseApplications(string content)
        {
            List<Application> result = new List<Application>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                result = parseApplicationsXml(doc);
            }
            catch
            {
            }

            return result;
        }

        private List<Application> parseApplicationsXml(XmlDocument doc)
        {
            List<Application> applicationList = new List<Application>();

            string xmlns = doc.DocumentElement.Attributes["xmlns"].Value;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("amcas", xmlns);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/amcas:Export/amcas:Applications/amcas:Application", nsmgr);
            string firstName = "";
            string lastName = "";
            string phone = "";
            foreach (XmlNode node in nodes)
            {
                firstName = node.SelectSingleNode("amcas:IdentifyingInformation/amcas:FirstName", nsmgr).InnerText;
                lastName = node.SelectSingleNode("amcas:IdentifyingInformation/amcas:LastName", nsmgr).InnerText;
                phone = node.SelectSingleNode("amcas:ContactInformation/amcas:Phone", nsmgr).InnerText;
                applicationList.Add(new Application()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone
                });
            }
            return applicationList;
        }

        public string GetApiResponseContent(MedicalInstitutions institution)
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
    }
}
