using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace K4Y.AMCAS.DataExchange.RestApi
{
    public abstract class BaseApiClient : IApiClient
    {
        public BaseApiClient()
        {
            BatchIndicator = false;
        }
        public bool BatchIndicator { get; set; }
        public abstract string GetApiResponseContent(MedicalInstitutions institution);
        public abstract List<Application> GetAllApplications(MedicalInstitutions institution);
        public abstract List<ApplicationData> GetApplicationList(MedicalInstitutions institution, string year);
        public abstract List<Application> GetSingleApplication(MedicalInstitutions institution, string year, string AAMCID);
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

        protected List<Application> parseApplicationsXml(XmlDocument doc)
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
                firstName = node.SelectSingleNode("amcas:IdentifyingInformation/amcas:NameDetails/amcas:Legal/amcas:FirstName", nsmgr).InnerText;
                lastName = node.SelectSingleNode("amcas:IdentifyingInformation/amcas:NameDetails/amcas:Legal/amcas:LastName", nsmgr).InnerText;
                phone = node.SelectSingleNode("amcas:ContactInformation/amcas:PermanentAddress/amcas:DayPhone", nsmgr).InnerText;
                applicationList.Add(new Application()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Phone = phone
                });
            }

            var batchNode = doc.DocumentElement.SelectSingleNode("/amcas:Export/amcas:Applications/amcas:BatchIndicator", nsmgr);
            if (batchNode != null)
            {
                string batchIndicator = batchNode.InnerText;
                bool batchParseResult = false;
                BatchIndicator = bool.TryParse(batchIndicator, out batchParseResult);
            }

            return applicationList;
        }

        protected List<ApplicationData> parseApplicationListXml(XmlDocument doc)
        {
            List<ApplicationData> applicationList = new List<ApplicationData>();

            string xmlns = doc.DocumentElement.Attributes["xmlns"].Value;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("amcas", xmlns);

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/amcas:ExportIDList/amcas:Application", nsmgr);
            string aAMCID = "";
            foreach (XmlNode node in nodes)
            {
                aAMCID = node.SelectSingleNode("amcas:AAMCID", nsmgr).InnerText;
                applicationList.Add(new ApplicationData()
                {
                    AAMCID = aAMCID,
                });
            }

            return applicationList;
        }
    }
}
