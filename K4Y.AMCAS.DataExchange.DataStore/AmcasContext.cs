using K4Y.AMCAS.DataExchange.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4Y.AMCAS.DataExchange.DataStore
{
    public class AmcasContext : DbContext
    {
        public AmcasContext() : base("name=AmcasDatabase")
        {
        }
        public DbSet<Application> Applications { get; set; }
    }
}
