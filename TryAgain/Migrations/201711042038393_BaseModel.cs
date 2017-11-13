namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BaseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "geoIP", c => c.String());
            AddColumn("dbo.Fans", "geoIP", c => c.String());
            AddColumn("dbo.Posts", "geoIP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "geoIP");
            DropColumn("dbo.Fans", "geoIP");
            DropColumn("dbo.Comments", "geoIP");
        }
    }
}
