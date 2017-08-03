namespace JobFinderAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoryIDinJobs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "Category_Id" });
            RenameColumn(table: "dbo.Jobs", name: "Category_Id", newName: "CategoryID");
            AlterColumn("dbo.Jobs", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "CategoryID");
            AddForeignKey("dbo.Jobs", "CategoryID", "dbo.Categories", "Id", cascadeDelete: true);
            DropColumn("dbo.Jobs", "CategoryIdentifier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CategoryIdentifier", c => c.Int(nullable: false));
            DropForeignKey("dbo.Jobs", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "CategoryID" });
            AlterColumn("dbo.Jobs", "CategoryID", c => c.Int());
            RenameColumn(table: "dbo.Jobs", name: "CategoryID", newName: "Category_Id");
            CreateIndex("dbo.Jobs", "Category_Id");
            AddForeignKey("dbo.Jobs", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
