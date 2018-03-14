using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace K4Y.AMCAS.DataExchange.WebClient.Models
{
    public class MainModel
    {
        public MainModel()
        {
            Content = "";
            ApiApplications = new List<Application>();
            DatabaseApplications = new List<Application>();
        }
        public String Content { get; set; }
        public List<Application> ApiApplications { get; set; }
        public List<Application> DatabaseApplications { get; set; }
    }
}