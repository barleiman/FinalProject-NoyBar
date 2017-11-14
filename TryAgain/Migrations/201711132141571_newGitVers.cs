namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newGitVers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "RegistrationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Fans", "ClubSeniority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fans", "ClubSeniority", c => c.Int(nullable: false));
            DropColumn("dbo.Fans", "RegistrationDate");
        }
    }
}
