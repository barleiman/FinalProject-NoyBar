namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comAndPstMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "commentUser_ID", c => c.Int());
            AddColumn("dbo.Posts", "postUser_ID", c => c.Int());
            CreateIndex("dbo.Comments", "commentUser_ID");
            CreateIndex("dbo.Posts", "postUser_ID");
            AddForeignKey("dbo.Comments", "commentUser_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Posts", "postUser_ID", "dbo.Users", "ID");
            DropColumn("dbo.Comments", "Commenter");
            DropColumn("dbo.Comments", "CommenterSiteAddr");
            DropColumn("dbo.Posts", "Author");
            DropColumn("dbo.Posts", "AuthorSiteAddr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "AuthorSiteAddr", c => c.String());
            AddColumn("dbo.Posts", "Author", c => c.String(maxLength: 80));
            AddColumn("dbo.Comments", "CommenterSiteAddr", c => c.String());
            AddColumn("dbo.Comments", "Commenter", c => c.String(maxLength: 80));
            DropForeignKey("dbo.Posts", "postUser_ID", "dbo.Users");
            DropForeignKey("dbo.Comments", "commentUser_ID", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "postUser_ID" });
            DropIndex("dbo.Comments", new[] { "commentUser_ID" });
            DropColumn("dbo.Posts", "postUser_ID");
            DropColumn("dbo.Comments", "commentUser_ID");
        }
    }
}
