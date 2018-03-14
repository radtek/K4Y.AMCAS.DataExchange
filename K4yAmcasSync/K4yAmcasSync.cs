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
        public K4yAmcasSync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log("Starting service.");

            timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
            timer.Interval = 300000;
            timer.Enabled = true;

            log("Service started.");
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
            log("Service stopped.");
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
            log("Starting syncronization.");

            try
            {
                RestApi.IApiClient apiClient;
                DataStore.AmcasRepository repository;
                apiClient = new RestApi.MockApiClient();
                repository = new DataStore.AmcasRepository();
                log("Data access setup completed.");

                List<DataModel.Application> apiApplications = apiClient.GetApplicationList(DataModel.MedicalInstitutions.University1);
                log("Applications retrieved from REST API: " + apiApplications.Count);
                repository.SyncApplications(apiApplications);
                List<DataModel.Application> repositoryApplications = repository.GetApplicationList();
                log("Applications available in database: " + repositoryApplications.Count);
                log("Syncronization completed.");
            }
            catch (Exception ex)
            {
                log(ex);
                log("Syncronization failed.");
            }
        }
        private void log(Exception ex)
        {
            StreamWriter sw;
            try
            {
                if (ex.InnerException == null)
                {
                    sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", true);
                    sw.WriteLine("{0} Exception: {1}; {2} \n", DateTime.Now.ToString("h:mm:ss.fff"), ex.Source.ToString().Trim(), ex.Message.ToString().Trim());
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    log(ex.InnerException);
                }
            }
            catch
            {
            }
        }
        private void log(string message)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log.txt", true);
                sw.WriteLine("{0} Message: {1}\n", DateTime.Now.ToString("h:mm:ss.fff"), message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
