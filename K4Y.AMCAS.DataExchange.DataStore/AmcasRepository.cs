using System;
using System.Collections.Generic;
using System.ComponentModel;
using K4Y.AMCAS.DataExchange.DataModel;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K4Y.AMCAS.DataExchange.DataStore
{
    public class AmcasRepository
    {
        public AmcasRepository()
        {
            using (var context = new AmcasContext())
            {
                context.Applications.Load();
            }
        }
        public List<Application> GetApplicationList()
        {
            using (var context = new AmcasContext())
            {
                return context.Applications.ToList();
            }
        }
        public void SyncApplications(List<Application> apiApplications)
        {
            if (apiApplications == null || apiApplications.Count == 0)
            {
                return;
            }

            using (var context = new AmcasContext())
            {
                foreach (Application a in apiApplications)
                {
                    var found = context.Applications.SingleOrDefault(dbApplication => dbApplication.FirstName == a.FirstName && dbApplication.LastName == a.LastName);
                    if (found != null)
                    {
                        a.Id = found.Id;
                    }
                }

                // Connection to database is closed at this point
            }

            // Open new connection to add or modify applications. Note that merging both loops in the same connection throws an exception.
            using (var context = new AmcasContext())
            {
                foreach (Application a in apiApplications)
                {
                    context.Entry(a).State = a.Id == 0 ?
                                       EntityState.Added :
                                       EntityState.Modified;

                    context.SaveChanges();
                }
            }
        }
    }
}
