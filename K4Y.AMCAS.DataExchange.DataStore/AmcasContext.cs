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
        public AmcasContext() : base(ConfigurationManager.ConnectionStrings["AmcasSyncDatabase"].ConnectionString)
        {
        }
        public DbSet<Application> Applications { get; set; }
    }
}
