namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusFieldtoLoan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "Status_StatusID", "dbo.Status");
            DropIndex("dbo.Loans", new[] { "Status_StatusID" });
            RenameColumn(table: "dbo.Loans", name: "Status_StatusID", newName: "StatusID");
            AlterColumn("dbo.Loans", "StatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "StatusID");
            AddForeignKey("dbo.Loans", "StatusID", "dbo.Status", "StatusID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "StatusID", "dbo.Status");
            DropIndex("dbo.Loans", new[] { "StatusID" });
            AlterColumn("dbo.Loans", "StatusID", c => c.Int());
            RenameColumn(table: "dbo.Loans", name: "StatusID", newName: "Status_StatusID");
            CreateIndex("dbo.Loans", "Status_StatusID");
            AddForeignKey("dbo.Loans", "Status_StatusID", "dbo.Status", "StatusID");
        }
    }
}
