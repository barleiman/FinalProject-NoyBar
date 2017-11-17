namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doubleRateMig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "givenRate", c => c.Double(nullable: false));
            AlterColumn("dbo.Posts", "postRate", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "postRate", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "givenRate", c => c.Int(nullable: false));
        }
    }
}
