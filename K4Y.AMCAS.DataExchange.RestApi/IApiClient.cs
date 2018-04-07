using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4Y.AMCAS.DataExchange.RestApi
{
    public interface IApiClient
    {
        string GetApiResponseContent(MedicalInstitutions institution);
        List<Application> GetAllApplications(MedicalInstitutions institution);
        List<ApplicationData> GetApplicationList(MedicalInstitutions institution, string year); 
        List<Application> GetSingleApplication(MedicalInstitutions institution, string year, string AAMCID); 
         List<Application> ParseApplications(string content);
        bool BatchIndicator { get; set; }
    }
}
