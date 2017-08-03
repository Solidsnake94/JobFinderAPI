namespace JobFinderAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdInUserDetail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserDetails", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserDetails", "UserId", c => c.Guid(nullable: false));
        }
    }
}
