namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cualquiervaina : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.UserGroups", "GroupID", "dbo.Groups");
            DropIndex("dbo.Projects", new[] { "CategoryID" });
            DropIndex("dbo.UserGroups", new[] { "GroupID" });
            DropColumn("dbo.Projects", "CategoryID");
            DropColumn("dbo.UserGroups", "GroupID");
            DropTable("dbo.Categories");
            DropTable("dbo.Groups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            AddColumn("dbo.UserGroups", "GroupID", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.UserGroups", "GroupID");
            CreateIndex("dbo.Projects", "CategoryID");
            AddForeignKey("dbo.UserGroups", "GroupID", "dbo.Groups", "GroupID", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
    }
}
