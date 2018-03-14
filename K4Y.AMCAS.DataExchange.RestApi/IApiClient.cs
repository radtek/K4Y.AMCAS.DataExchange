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
        List<Application> GetApplicationList(MedicalInstitutions institution);
        List<Application> ParseApplications(string content);
        string GetApiResponseContent(MedicalInstitutions institution);
    }
}
