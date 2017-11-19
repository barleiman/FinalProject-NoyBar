namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trymig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        Title = c.String(),
                        Text = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                        givenRate = c.Double(nullable: false),
                        geoIP = c.String(),
                        commentUser_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Users", t => t.commentUser_Email)
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.commentUser_Email);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Gender = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        FanAuthority = c.Int(nullable: false),
                        SiteAddr = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        PostText = c.String(),
                        postRate = c.Double(nullable: false),
                        geoIP = c.String(),
                        postUser_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Users", t => t.postUser_Email)
                .Index(t => t.postUser_Email);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "postUser_Email", "dbo.Users");
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Comments", "commentUser_Email", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "postUser_Email" });
            DropIndex("dbo.Comments", new[] { "commentUser_Email" });
            DropIndex("dbo.Comments", new[] { "PostID" });
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
