namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fansmig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "UserName", c => c.String());
            AddColumn("dbo.Fans", "Password", c => c.String());
            AddColumn("dbo.Fans", "FanAuthority", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fans", "FanAuthority");
            DropColumn("dbo.Fans", "Password");
            DropColumn("dbo.Fans", "UserName");
        }
    }
}
