namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLend : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lends",
                c => new
                    {
                        LendID = c.Int(nullable: false, identity: true),
                        Amount = c.Single(nullable: false),
                        AmountLeft = c.Single(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                        LoanID = c.Int(nullable: false),
                        Status_StatusID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LendID)
                .ForeignKey("dbo.Loans", t => t.LoanID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.LoanID)
                .Index(t => t.Status_StatusID)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Payments", "Timestamp", c => c.Binary());
            AddColumn("dbo.PaymentDetails", "Lend_LendID", c => c.Int());
            CreateIndex("dbo.PaymentDetails", "Lend_LendID");
            AddForeignKey("dbo.PaymentDetails", "Lend_LendID", "dbo.Lends", "LendID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lends", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Lends", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.PaymentDetails", "Lend_LendID", "dbo.Lends");
            DropForeignKey("dbo.Lends", "LoanID", "dbo.Loans");
            DropIndex("dbo.PaymentDetails", new[] { "Lend_LendID" });
            DropIndex("dbo.Lends", new[] { "User_Id" });
            DropIndex("dbo.Lends", new[] { "Status_StatusID" });
            DropIndex("dbo.Lends", new[] { "LoanID" });
            DropColumn("dbo.PaymentDetails", "Lend_LendID");
            DropColumn("dbo.Payments", "Timestamp");
            DropTable("dbo.Lends");
        }
    }
}
