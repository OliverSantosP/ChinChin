namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix2ndStatusToLoan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "StatusID", "dbo.Status");
            DropIndex("dbo.Loans", new[] { "StatusID" });
            AlterColumn("dbo.Loans", "StatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "StatusID");
            AddForeignKey("dbo.Loans", "StatusID", "dbo.Status", "StatusID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "StatusID", "dbo.Status");
            DropIndex("dbo.Loans", new[] { "StatusID" });
            AlterColumn("dbo.Loans", "StatusID", c => c.Int());
            CreateIndex("dbo.Loans", "StatusID");
            AddForeignKey("dbo.Loans", "StatusID", "dbo.Status", "StatusID");
        }
    }
}
