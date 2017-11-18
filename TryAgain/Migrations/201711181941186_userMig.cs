namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userMig : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Fans", newName: "Users");
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String());
            AddColumn("dbo.Users", "SiteAddr", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.Users", "geoIP");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "geoIP", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            DropColumn("dbo.Users", "SiteAddr");
            DropColumn("dbo.Users", "ConfirmPassword");
            DropColumn("dbo.Users", "Email");
            RenameTable(name: "dbo.Users", newName: "Fans");
        }
    }
}
