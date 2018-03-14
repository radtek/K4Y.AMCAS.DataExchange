using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace K4Y.AMCAS.DataExchange.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //if (Environment.UserInteractive)
            //{
            //    K4yAmcasSync service1 = new K4yAmcasSync();
            //    service1.TestStartupAndStop(args);
            //}
            //else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new K4yAmcasSync()
                };
                ServiceBase.Run(ServicesToRun);
            }
           
        }
    }
}
