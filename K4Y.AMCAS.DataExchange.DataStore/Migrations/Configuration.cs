namespace K4Y.AMCAS.DataExchange.DataStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<K4Y.AMCAS.DataExchange.DataStore.AmcasContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "K4Y.AMCAS.DataExchange.DataAccess.AmcasContext";
        }

        protected override void Seed(K4Y.AMCAS.DataExchange.DataStore.AmcasContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
