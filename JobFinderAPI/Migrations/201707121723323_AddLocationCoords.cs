namespace JobFinderAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocationCoords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Longitude", c => c.String());
            AddColumn("dbo.Jobs", "Latitude", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Latitude");
            DropColumn("dbo.Jobs", "Longitude");
        }
    }
}
