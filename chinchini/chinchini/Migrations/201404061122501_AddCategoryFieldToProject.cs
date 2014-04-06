namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryFieldToProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "CategoryID");
            AddForeignKey("dbo.Projects", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Projects", new[] { "CategoryID" });
            DropColumn("dbo.Projects", "CategoryID");
        }
    }
}
