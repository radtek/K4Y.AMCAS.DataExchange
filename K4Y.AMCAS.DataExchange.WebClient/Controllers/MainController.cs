using K4Y.AMCAS.DataExchange.DataModel;
using K4Y.AMCAS.DataExchange.DataStore;
using K4Y.AMCAS.DataExchange.RestApi;
using K4Y.AMCAS.DataExchange.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K4Y.AMCAS.DataExchange.WebClient.Controllers
{
    public class MainController : Controller
    {
        private List<Application> parsedApplications
        {
            get { return Session["parsedApplications"] as List<Application>; }
            set { Session["parsedApplications"] = value; }
        }

        private List<Application> databaseApplications
        {
            get { return Session["databaseApplications"] as List<Application>; }
            set { Session["databaseApplications"] = value; }
        }
        private string loadedContent
        {
            get { return (string)Session["loadedContent"]; }
            set { Session["loadedContent"] = value; }
        }

        // GET: Main
        public ActionResult Index()
        {
            var model = new MainModel();
            loadedContent = model.Content;
            parsedApplications = model.ApiApplications;
            databaseApplications = model.DatabaseApplications;
            return View(model);
        }

        [HttpPost]
        public ActionResult LoadApiResponse(MainModel model)
        {
            IApiClient apiClient = HttpContext.Application["apiClient"] as IApiClient;
            model.Content = apiClient.GetApiResponseContent(MedicalInstitutions.University1);
            loadedContent = model.Content;

            model.ApiApplications = parsedApplications;
            model.DatabaseApplications = databaseApplications;

            return View("Index", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Parse(MainModel model)
        {
            loadedContent = model.Content;

            IApiClient apiClient = HttpContext.Application["apiClient"] as IApiClient;
            model.ApiApplications = apiClient.ParseApplications(model.Content);
            parsedApplications = model.ApiApplications;

            model.DatabaseApplications = databaseApplications;

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Sync(MainModel model)
        {
            AmcasRepository repository = HttpContext.Application["amcasRepository"] as AmcasRepository;
            repository.SyncApplications(parsedApplications);

            model.Content = loadedContent;
            model.ApiApplications = parsedApplications;

            model.DatabaseApplications = repository.GetApplicationList();
            databaseApplications = model.DatabaseApplications;

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult LoadDatabaseContent(MainModel model)
        {
            model.Content = loadedContent;
            model.ApiApplications = parsedApplications;

            AmcasRepository repository = HttpContext.Application["amcasRepository"] as AmcasRepository;
            model.DatabaseApplications = repository.GetApplicationList();
            databaseApplications = model.DatabaseApplications;

            return View("Index", model);
        }
    }
}