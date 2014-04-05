namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaymentDetailEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        PaymentDetailID = c.Int(nullable: false, identity: true),
                        TimeStamp = c.Binary(),
                        PaymentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentDetailID)
                .ForeignKey("dbo.Payments", t => t.PaymentID, cascadeDelete: true)
                .Index(t => t.PaymentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentDetails", "PaymentID", "dbo.Payments");
            DropIndex("dbo.PaymentDetails", new[] { "PaymentID" });
            DropTable("dbo.PaymentDetails");
        }
    }
}
