namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaymentDetail : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Payments", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "Timestamp", c => c.Binary());
        }
    }
}
