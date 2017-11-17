namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBUserChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "Fans");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Fans", newName: "Users");
        }
    }
}
