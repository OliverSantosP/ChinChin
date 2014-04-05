namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PitchComments",
                c => new
                    {
                        PitchCommentID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Comment = c.String(),
                        Timespan = c.DateTime(nullable: false),
                        ReplyID = c.Int(nullable: false),
                        PitchID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PitchCommentID)
                .ForeignKey("dbo.PitchComments", t => t.ReplyID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ReplyID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        UserGroupID = c.Int(nullable: false, identity: true),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserGroupID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Projects", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroups", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PitchComments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PitchComments", "ReplyID", "dbo.PitchComments");
            DropIndex("dbo.UserGroups", new[] { "User_Id" });
            DropIndex("dbo.PitchComments", new[] { "User_Id" });
            DropIndex("dbo.PitchComments", new[] { "ReplyID" });
            DropColumn("dbo.Projects", "Description");
            DropTable("dbo.UserGroups");
            DropTable("dbo.PitchComments");
        }
    }
}
