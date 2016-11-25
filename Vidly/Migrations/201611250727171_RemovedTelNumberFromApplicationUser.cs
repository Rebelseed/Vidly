namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTelNumberFromApplicationUser : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "TelNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TelNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
