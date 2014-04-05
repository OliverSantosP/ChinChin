namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectPitchLoanEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donations",
                c => new
                    {
                        DonationID = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        ProjectID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DonationID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProjectID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Amount = c.Single(nullable: false),
                        StatusID = c.Int(nullable: false),
                        PitchID = c.Int(nullable: false),
                        ProjectTypeID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Pitches", t => t.PitchID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.StatusID)
                .Index(t => t.PitchID)
                .Index(t => t.ProjectTypeID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Pitches",
                c => new
                    {
                        PitchID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Body = c.String(),
                        VideoURL = c.String(),
                        FB = c.String(),
                        TW = c.String(),
                    })
                .PrimaryKey(t => t.PitchID);
            
            CreateTable(
                "dbo.PitchGalleries",
                c => new
                    {
                        PitchGalleryID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                        Timespan = c.DateTime(),
                        StatusID = c.Int(nullable: false),
                        PitchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PitchGalleryID)
                .ForeignKey("dbo.Pitches", t => t.PitchID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.StatusID)
                .Index(t => t.PitchID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.ProjectTypes",
                c => new
                    {
                        ProjectTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectTypeID);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanID = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        Rate = c.Single(nullable: false),
                        Quota = c.Single(nullable: false),
                        Debt = c.Single(nullable: false),
                        PeriodDays = c.Int(nullable: false),
                        DateRequested = c.DateTime(nullable: false),
                        LastUpdatedDate = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ProjectID = c.Int(nullable: false),
                        LoanTypeID = c.Int(nullable: false),
                        Status_StatusID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LoanID)
                .ForeignKey("dbo.LoanTypes", t => t.LoanTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ProjectID)
                .Index(t => t.LoanTypeID)
                .Index(t => t.Status_StatusID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.LoanTypes",
                c => new
                    {
                        LoanTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Max = c.Single(nullable: false),
                        Min = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.LoanTypeID);
            
            AddColumn("dbo.AspNetUsers", "Status_StatusID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Status_StatusID");
            AddForeignKey("dbo.AspNetUsers", "Status_StatusID", "dbo.Status", "StatusID");
            DropColumn("dbo.AspNetUsers", "StatusID");
            DropColumn("dbo.AspNetUsers", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MyProperty", c => c.Int());
            AddColumn("dbo.AspNetUsers", "StatusID", c => c.Int());
            DropForeignKey("dbo.Loans", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.Loans", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Loans", "LoanTypeID", "dbo.LoanTypes");
            DropForeignKey("dbo.Donations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Donations", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.Projects", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Projects", "ProjectTypeID", "dbo.ProjectTypes");
            DropForeignKey("dbo.Projects", "PitchID", "dbo.Pitches");
            DropForeignKey("dbo.PitchGalleries", "StatusID", "dbo.Status");
            DropForeignKey("dbo.PitchGalleries", "PitchID", "dbo.Pitches");
            DropIndex("dbo.Loans", new[] { "User_Id" });
            DropIndex("dbo.Loans", new[] { "Status_StatusID" });
            DropIndex("dbo.Loans", new[] { "LoanTypeID" });
            DropIndex("dbo.Loans", new[] { "ProjectID" });
            DropIndex("dbo.AspNetUsers", new[] { "Status_StatusID" });
            DropIndex("dbo.PitchGalleries", new[] { "PitchID" });
            DropIndex("dbo.PitchGalleries", new[] { "StatusID" });
            DropIndex("dbo.Projects", new[] { "User_Id" });
            DropIndex("dbo.Projects", new[] { "ProjectTypeID" });
            DropIndex("dbo.Projects", new[] { "PitchID" });
            DropIndex("dbo.Projects", new[] { "StatusID" });
            DropIndex("dbo.Donations", new[] { "User_Id" });
            DropIndex("dbo.Donations", new[] { "ProjectID" });
            DropColumn("dbo.AspNetUsers", "Status_StatusID");
            DropTable("dbo.LoanTypes");
            DropTable("dbo.Loans");
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.Status");
            DropTable("dbo.PitchGalleries");
            DropTable("dbo.Pitches");
            DropTable("dbo.Projects");
            DropTable("dbo.Donations");
        }
    }
}
