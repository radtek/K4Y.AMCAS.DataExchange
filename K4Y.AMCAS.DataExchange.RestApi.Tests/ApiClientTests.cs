using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace K4Y.AMCAS.DataExchange.RestApi.Tests
{
    [TestClass]
    public class ApiClientTests
    {
        [TestMethod]
        public void AllApplicationsResponse_Parsed_Successfully()
        {
            int expectedApplicationCount = 10;
            var apiClient = ApiClientFactory.Create(ApiClientTypes.Mock);
            List<DataModel.Application> applications = apiClient.GetAllApplications(DataModel.MedicalInstitutions.NovaSoutheastern);
            Assert.AreEqual(expectedApplicationCount, applications.Count, "Application count not as expected");
        }
        [TestMethod]
        public void ApplicationListResponse_Parsed_Successfully()
        {
            int expectedApplicationCount = 3000;
            var apiClient = ApiClientFactory.Create(ApiClientTypes.Mock);
            List<DataModel.ApplicationData> applications = apiClient.GetApplicationList(DataModel.MedicalInstitutions.NovaSoutheastern, "2018");
            Assert.AreEqual(expectedApplicationCount, applications.Count, "Application count not as expected");
        }
        [TestMethod]
        public void SingleApplicationResponse_Parsed_Successfully()
        {
            int expectedApplicationCount = 1;
            var apiClient = ApiClientFactory.Create(ApiClientTypes.Mock);
            List<DataModel.Application> applications = apiClient.GetSingleApplication(DataModel.MedicalInstitutions.NovaSoutheastern, "2018", "21070028");
            Assert.AreEqual(expectedApplicationCount, applications.Count, "Application count not as expected");
        }
    }
}
