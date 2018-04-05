using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    // <source_code>\K4Y\K4Y.AMCAS.DataExchange\K4yAmcasSync\bin\Debug>"c:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" K4yAmcasSync.exe
    public partial class K4yAmcasSync : ServiceBase
    {
        Timer timer = new Timer();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public K4yAmcasSync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
                timer.Interval = 300000;
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
                RestApi.IApiClient apiClient = new RestApi.MockApiClient();
                DataStore.AmcasRepository repository = new DataStore.AmcasRepository();

                do
                {
                    List<DataModel.Application> apiApplications = apiClient.GetApplicationList(DataModel.MedicalInstitutions.NovaSoutheastern);
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
                logger.Error(ex, "Syncronization failed.");
            }
        }
    }
}
