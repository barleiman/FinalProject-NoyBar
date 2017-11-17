namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratepost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "postRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "postRate");
        }
    }
}
