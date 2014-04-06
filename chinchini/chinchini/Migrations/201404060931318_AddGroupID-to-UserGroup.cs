namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupIDtoUserGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserGroups", "GroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.UserGroups", "GroupID");
            AddForeignKey("dbo.UserGroups", "GroupID", "dbo.Groups", "GroupID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroups", "GroupID", "dbo.Groups");
            DropIndex("dbo.UserGroups", new[] { "GroupID" });
            DropColumn("dbo.UserGroups", "GroupID");
        }
    }
}
