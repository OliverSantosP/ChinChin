namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingUserIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PitchComments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Lends", new[] { "User_Id" });
            DropIndex("dbo.PitchComments", new[] { "User_Id" });
            AddColumn("dbo.Lends", "User_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.PitchComments", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Lends", "User_Id", c => c.String());
            AlterColumn("dbo.PitchComments", "User_Id", c => c.String());
            CreateIndex("dbo.Lends", "User_Id1");
            CreateIndex("dbo.PitchComments", "User_Id1");
            AddForeignKey("dbo.Lends", "User_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PitchComments", "User_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PitchComments", "User_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lends", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.PitchComments", new[] { "User_Id1" });
            DropIndex("dbo.Lends", new[] { "User_Id1" });
            AlterColumn("dbo.PitchComments", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Lends", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.PitchComments", "User_Id1");
            DropColumn("dbo.Lends", "User_Id1");
            CreateIndex("dbo.PitchComments", "User_Id");
            CreateIndex("dbo.Lends", "User_Id");
            AddForeignKey("dbo.PitchComments", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Lends", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
