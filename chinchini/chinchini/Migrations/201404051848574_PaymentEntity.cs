namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        LoanID = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                        PaymentMethodID = c.Int(nullable: false),
                        Authorization = c.String(),
                        Status_StatusID = c.Int(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.Loans", t => t.LoanID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.Status_StatusID)
                .Index(t => t.LoanID)
                .Index(t => t.Status_StatusID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Status_StatusID", "dbo.Status");
            DropForeignKey("dbo.Payments", "LoanID", "dbo.Loans");
            DropIndex("dbo.Payments", new[] { "Status_StatusID" });
            DropIndex("dbo.Payments", new[] { "LoanID" });
            DropTable("dbo.Payments");
        }
    }
}
