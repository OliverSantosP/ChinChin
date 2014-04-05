namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentTransactionEntity : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentTransactions", "PaymentID", "dbo.Payments");
            DropIndex("dbo.PaymentTransactions", new[] { "PaymentID" });
            DropTable("dbo.PaymentTransactions");
        }
    }
}
