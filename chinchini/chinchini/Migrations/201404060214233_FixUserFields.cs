namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUserFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "User_Id" });
            AddColumn("dbo.Projects", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Projects", "User_Id", c => c.String());
            CreateIndex("dbo.Projects", "User_Id1");
            AddForeignKey("dbo.Projects", "User_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "User_Id1" });
            AlterColumn("dbo.Projects", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Projects", "User_Id1");
            CreateIndex("dbo.Projects", "User_Id");
            AddForeignKey("dbo.Projects", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
