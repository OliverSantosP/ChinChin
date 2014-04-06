namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyLoanIDToBeNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "LoanID", "dbo.Loans");
            DropIndex("dbo.Projects", new[] { "LoanID" });
            AlterColumn("dbo.Projects", "LoanID", c => c.Int());
            CreateIndex("dbo.Projects", "LoanID");
            AddForeignKey("dbo.Projects", "LoanID", "dbo.Loans", "LoanID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "LoanID", "dbo.Loans");
            DropIndex("dbo.Projects", new[] { "LoanID" });
            AlterColumn("dbo.Projects", "LoanID", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "LoanID");
            AddForeignKey("dbo.Projects", "LoanID", "dbo.Loans", "LoanID", cascadeDelete: true);
        }
    }
}
