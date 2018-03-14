namespace K4Y.AMCAS.DataExchange.DataStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applications", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "Phone");
        }
    }
}
