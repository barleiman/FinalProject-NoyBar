namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" }, "dbo.Users");
            DropForeignKey("dbo.Posts", new[] { "postUser_Email", "postUser_ID" }, "dbo.Users");
            DropIndex("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" });
            DropIndex("dbo.Posts", new[] { "postUser_Email", "postUser_ID" });
            DropPrimaryKey("dbo.Users");
            AddPrimaryKey("dbo.Users", "Email");
            CreateIndex("dbo.Comments", "commentUser_Email");
            CreateIndex("dbo.Posts", "postUser_Email");
            AddForeignKey("dbo.Comments", "commentUser_Email", "dbo.Users", "Email");
            AddForeignKey("dbo.Posts", "postUser_Email", "dbo.Users", "Email");
            DropColumn("dbo.Comments", "commentUser_ID");
            DropColumn("dbo.Users", "ID");
            DropColumn("dbo.Posts", "postUser_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "postUser_ID", c => c.Int());
            AddColumn("dbo.Users", "ID", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "commentUser_ID", c => c.Int());
            DropForeignKey("dbo.Posts", "postUser_Email", "dbo.Users");
            DropForeignKey("dbo.Comments", "commentUser_Email", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "postUser_Email" });
            DropIndex("dbo.Comments", new[] { "commentUser_Email" });
            DropPrimaryKey("dbo.Users");
            AddPrimaryKey("dbo.Users", new[] { "Email", "ID" });
            CreateIndex("dbo.Posts", new[] { "postUser_Email", "postUser_ID" });
            CreateIndex("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" });
            AddForeignKey("dbo.Posts", new[] { "postUser_Email", "postUser_ID" }, "dbo.Users", new[] { "Email", "ID" });
            AddForeignKey("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" }, "dbo.Users", new[] { "Email", "ID" });
        }
    }
}
