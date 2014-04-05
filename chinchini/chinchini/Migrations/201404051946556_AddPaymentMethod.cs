namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentMethod : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentMethods", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentMethods", "Status_StatusID", "dbo.Status");
            DropIndex("dbo.PaymentMethods", new[] { "User_Id" });
            DropIndex("dbo.PaymentMethods", new[] { "Status_StatusID" });
            DropTable("dbo.PaymentMethods");
        }
    }
}
