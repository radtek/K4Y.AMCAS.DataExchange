using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace K4Y.AMCAS.DataExchange.Service
{
    // To install the service:
    // Run "cmd" as administrator
    // <source_code>\K4Y\K4Y.AMCAS.DataExchange\K4yAmcasSync\bin\Debug>"c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" K4yAmcasSync.exe

    // To uninstall the service:
    // Stop the service
    // Run "cmd" as administrator
    // <source_code>\K4Y\K4Y.AMCAS.DataExchange\K4yAmcasSync\bin\Debug>"c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" /u K4yAmcasSync.exe

    public partial class K4yAmcasSync : ServiceBase
    {
        Timer timer = new Timer();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static int interval = -1;
        public K4yAmcasSync()
        {
            InitializeComponent();
        }

        static int RequestInterval
        {
            get
            {
                if (interval == -1)
                {
                    interval = 300000;

                    try
                    {
                        var appSettings = ConfigurationManager.AppSettings;
                        if (appSettings["RequestInterval"] != null)
                        {
                            interval = int.Parse(appSettings["RequestInterval"]);
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e, "Failed to read API request interval. Assuming 5 minutes.");
                    }
                }

                logger.Info("Request interval set to {0} minutes", interval / 60000);
                return interval;
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
                timer.Interval = RequestInterval;
                timer.Enabled = true;
                logger.Info("Service started successfully.");
            }
            catch (Exception e)
            {
                logger.Error(e);
            }
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            logger.Info("Service stopped successfully.");
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private void onElapsedTime(object source, ElapsedEventArgs e)
        {
            logger.Info("Starting syncronization.");

            try
            {
                RestApi.IApiClient apiClient = RestApi.ApiClientFactory.Create(RestApi.ApiClientTypes.Curl);
                DataStore.AmcasRepository repository = new DataStore.AmcasRepository();

                do
                {
                    List<DataModel.Application> apiApplications = apiClient.GetAllApplications(DataModel.MedicalInstitutions.NovaSoutheastern);
                    logger.Info("Applications retrieved from REST API: {0}", apiApplications.Count);
                    repository.SyncApplications(apiApplications);
                    List<DataModel.Application> repositoryApplications = repository.GetApplicationList();
                    logger.Info("Applications available in database: {0}", repositoryApplications.Count);
                    logger.Info("Batch indicator: {0}", apiClient.BatchIndicator);
                }
                while (apiClient.BatchIndicator);
                logger.Info("Syncronization completed.");
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
