namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginmigNew : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "commentUser_ID", "dbo.Users");
            DropForeignKey("dbo.Posts", "postUser_ID", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "commentUser_ID" });
            DropIndex("dbo.Posts", new[] { "postUser_ID" });
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Comments", "commentUser_Email", c => c.String(maxLength: 128));
            AddColumn("dbo.Posts", "postUser_Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.Users", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", new[] { "ID", "Email" });
            CreateIndex("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" });
            CreateIndex("dbo.Posts", new[] { "postUser_ID", "postUser_Email" });
            AddForeignKey("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" }, "dbo.Users", new[] { "ID", "Email" });
            AddForeignKey("dbo.Posts", new[] { "postUser_ID", "postUser_Email" }, "dbo.Users", new[] { "ID", "Email" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", new[] { "postUser_ID", "postUser_Email" }, "dbo.Users");
            DropForeignKey("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" }, "dbo.Users");
            DropIndex("dbo.Posts", new[] { "postUser_ID", "postUser_Email" });
            DropIndex("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "ID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Posts", "postUser_Email");
            DropColumn("dbo.Comments", "commentUser_Email");
            AddPrimaryKey("dbo.Users", "ID");
            CreateIndex("dbo.Posts", "postUser_ID");
            CreateIndex("dbo.Comments", "commentUser_ID");
            AddForeignKey("dbo.Posts", "postUser_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Comments", "commentUser_ID", "dbo.Users", "ID");
        }
    }
}
