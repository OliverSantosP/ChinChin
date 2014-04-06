namespace chinchini.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoanAndLenders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "ProjectID", "dbo.Projects");
            DropIndex("dbo.Loans", new[] { "ProjectID" });
            AddColumn("dbo.Projects", "LoanID", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "LoanID");
            AddForeignKey("dbo.Projects", "LoanID", "dbo.Loans", "LoanID", cascadeDelete: true);
            DropColumn("dbo.Loans", "ProjectID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Loans", "ProjectID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Projects", "LoanID", "dbo.Loans");
            DropIndex("dbo.Projects", new[] { "LoanID" });
            DropColumn("dbo.Projects", "LoanID");
            CreateIndex("dbo.Loans", "ProjectID");
            AddForeignKey("dbo.Loans", "ProjectID", "dbo.Projects", "ProjectID", cascadeDelete: true);
        }
    }
}
