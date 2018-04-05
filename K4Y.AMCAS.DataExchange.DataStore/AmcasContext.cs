using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4Y.AMCAS.DataExchange.DataStore
{
    public class AmcasContext : DbContext
    {
        public AmcasContext() : base(createNewConnectionString("AmcasSyncDatabase", "W@terfall1"))
        {
        }
        public DbSet<Application> Applications { get; set; }

        private static string createNewConnectionString(string connectionName, string password)
        {
            var connectionSettings = ConfigurationManager.ConnectionStrings[connectionName];
            var originalConnectionString = connectionSettings.ConnectionString;
            var modifiedConnectionString = originalConnectionString.Replace("dummy_password", password);

            return modifiedConnectionString;
        }
    }
}
