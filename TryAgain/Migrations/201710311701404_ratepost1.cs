namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratepost1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "givenRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "givenRate");
        }
    }
}
