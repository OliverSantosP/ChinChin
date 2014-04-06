namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Description = c.String(),
                        Amount = c.Single(nullable: false),
                        StatusID = c.Int(nullable: false),
                        PitchID = c.Int(nullable: false),
                        ProjectTypeID = c.Int(nullable: false),
                        LoanID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ProjectID)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.Loans", t => t.LoanID)
                .ForeignKey("dbo.Pitches", t => t.PitchID, cascadeDelete: true)
                .ForeignKey("dbo.ProjectTypes", t => t.ProjectTypeID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.StatusID)
                .Index(t => t.PitchID)
                .Index(t => t.ProjectTypeID)
                .Index(t => t.LoanID)
                .Index(t => t.User_Id);
            
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
                        StatusID = c.Int(nullable: false),
                        LoanTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanID)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("dbo.LoanTypes", t => t.LoanTypeID, cascadeDelete: true)
                .Index(t => t.StatusID)
                .Index(t => t.LoanTypeID);
            
            CreateTable(
                "dbo.Lends",
                c => new
                    {
                        LendID = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        AmountLeft = c.Single(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        LoanID = c.Int(nullable: false),
                        Status_StatusID = c.Int(),
                    })
                .PrimaryKey(t => t.LendID)
                .ForeignKey("dbo.Loans", t => t.LoanID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id)
                .Index(t => t.LoanID)
                .Index(t => t.Status_StatusID);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        PaymentDetailID = c.Int(nullable: false, identity: true),
                        TimeStamp = c.Binary(),
                        PaymentID = c.Int(nullable: false),
                        Lend_LendID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentDetailID)
                .ForeignKey("dbo.Payments", t => t.PaymentID, cascadeDelete: true)
                .ForeignKey("dbo.Lends", t => t.Lend_LendID)
                .Index(t => t.PaymentID)
                .Index(t => t.Lend_LendID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        LoanID = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                        PaymentMethodID = c.Int(nullable: false),
                        Authorization = c.String(),
                        Timestamp = c.Binary(),
                        Status_StatusID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Loans", t => t.LoanID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .Index(t => t.LoanID)
                .Index(t => t.Status_StatusID);
            
            CreateTable(
                "dbo.PaymentTransactions",
                c => new
                    {
                        PaymentTransactionID = c.Int(nullable: false, identity: true),
                        Rejected = c.Boolean(nullable: false),
                        Message = c.String(),
                        PaymentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentTransactionID)
                .ForeignKey("dbo.Payments", t => t.PaymentID, cascadeDelete: true)
                .Index(t => t.PaymentID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Balance = c.Single(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Status_StatusID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .Index(t => t.Status_StatusID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.ProjectTypes",
                c => new
                    {
                        ProjectTypeID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProjectTypeID);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        PaymentMethodID = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        Routing = c.String(),
                        BankName = c.String(),
                        AccountName = c.String(),
                        Status_StatusID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PaymentMethodID)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Status_StatusID)
                .Index(t => t.User_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroups", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PitchComments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PitchComments", "ReplyID", "dbo.PitchComments");
            DropForeignKey("dbo.PaymentMethods", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentMethods", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.Donations", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Donations", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projects", "ProjectTypeID", "dbo.ProjectTypes");
            DropForeignKey("dbo.Projects", "PitchID", "dbo.Pitches");
            DropForeignKey("dbo.PitchGalleries", "StatusID", "dbo.Status");
            DropForeignKey("dbo.PitchGalleries", "PitchID", "dbo.Pitches");
            DropForeignKey("dbo.Projects", "LoanID", "dbo.Loans");
            DropForeignKey("dbo.Loans", "LoanTypeID", "dbo.LoanTypes");
            DropForeignKey("dbo.Lends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lends", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.PaymentDetails", "Lend_LendID", "dbo.Lends");
            DropForeignKey("dbo.Payments", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.Projects", "StatusID", "dbo.Status");
            DropForeignKey("dbo.Loans", "StatusID", "dbo.Status");
            DropForeignKey("dbo.PaymentTransactions", "PaymentID", "dbo.Payments");
            DropForeignKey("dbo.PaymentDetails", "PaymentID", "dbo.Payments");
            DropForeignKey("dbo.Payments", "LoanID", "dbo.Loans");
            DropForeignKey("dbo.Lends", "LoanID", "dbo.Loans");
            DropIndex("dbo.UserGroups", new[] { "User_Id" });
            DropIndex("dbo.PitchComments", new[] { "User_Id" });
            DropIndex("dbo.PitchComments", new[] { "ReplyID" });
            DropIndex("dbo.PaymentMethods", new[] { "User_Id" });
            DropIndex("dbo.PaymentMethods", new[] { "Status_StatusID" });
            DropIndex("dbo.PitchGalleries", new[] { "PitchID" });
            DropIndex("dbo.PitchGalleries", new[] { "StatusID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Status_StatusID" });
            DropIndex("dbo.PaymentTransactions", new[] { "PaymentID" });
            DropIndex("dbo.Payments", new[] { "Status_StatusID" });
            DropIndex("dbo.Payments", new[] { "LoanID" });
            DropIndex("dbo.PaymentDetails", new[] { "Lend_LendID" });
            DropIndex("dbo.PaymentDetails", new[] { "PaymentID" });
            DropIndex("dbo.Lends", new[] { "Status_StatusID" });
            DropIndex("dbo.Lends", new[] { "LoanID" });
            DropIndex("dbo.Lends", new[] { "User_Id" });
            DropIndex("dbo.Loans", new[] { "LoanTypeID" });
            DropIndex("dbo.Loans", new[] { "StatusID" });
            DropIndex("dbo.Projects", new[] { "User_Id" });
            DropIndex("dbo.Projects", new[] { "LoanID" });
            DropIndex("dbo.Projects", new[] { "ProjectTypeID" });
            DropIndex("dbo.Projects", new[] { "PitchID" });
            DropIndex("dbo.Projects", new[] { "StatusID" });
            DropIndex("dbo.Donations", new[] { "User_Id" });
            DropIndex("dbo.Donations", new[] { "ProjectID" });
            DropTable("dbo.UserGroups");
            DropTable("dbo.PitchComments");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.ProjectTypes");
            DropTable("dbo.PitchGalleries");
            DropTable("dbo.Pitches");
            DropTable("dbo.LoanTypes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Status");
            DropTable("dbo.PaymentTransactions");
            DropTable("dbo.Payments");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.Lends");
            DropTable("dbo.Loans");
            DropTable("dbo.Projects");
            DropTable("dbo.Donations");
        }
    }
}
