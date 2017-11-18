namespace TryAgain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginmigNew1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" }, "dbo.Users");
            DropForeignKey("dbo.Posts", new[] { "postUser_ID", "postUser_Email" }, "dbo.Users");
            DropIndex("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" });
            DropIndex("dbo.Posts", new[] { "postUser_ID", "postUser_Email" });
            RenameColumn(table: "dbo.Comments", name: "commentUser_ID", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Comments", name: "commentUser_Email", newName: "commentUser_ID");
            RenameColumn(table: "dbo.Posts", name: "postUser_ID", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.Posts", name: "postUser_Email", newName: "postUser_ID");
            RenameColumn(table: "dbo.Comments", name: "__mig_tmp__0", newName: "commentUser_Email");
            RenameColumn(table: "dbo.Posts", name: "__mig_tmp__1", newName: "postUser_Email");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Comments", "commentUser_Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "commentUser_ID", c => c.Int());
            AlterColumn("dbo.Posts", "postUser_Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "postUser_ID", c => c.Int());
            AddPrimaryKey("dbo.Users", new[] { "Email", "ID" });
            CreateIndex("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" });
            CreateIndex("dbo.Posts", new[] { "postUser_Email", "postUser_ID" });
            AddForeignKey("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" }, "dbo.Users", new[] { "Email", "ID" });
            AddForeignKey("dbo.Posts", new[] { "postUser_Email", "postUser_ID" }, "dbo.Users", new[] { "Email", "ID" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", new[] { "postUser_Email", "postUser_ID" }, "dbo.Users");
            DropForeignKey("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" }, "dbo.Users");
            DropIndex("dbo.Posts", new[] { "postUser_Email", "postUser_ID" });
            DropIndex("dbo.Comments", new[] { "commentUser_Email", "commentUser_ID" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Posts", "postUser_ID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Posts", "postUser_Email", c => c.Int());
            AlterColumn("dbo.Comments", "commentUser_ID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "commentUser_Email", c => c.Int());
            AddPrimaryKey("dbo.Users", new[] { "ID", "Email" });
            RenameColumn(table: "dbo.Posts", name: "postUser_Email", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.Comments", name: "commentUser_Email", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.Posts", name: "postUser_ID", newName: "postUser_Email");
            RenameColumn(table: "dbo.Posts", name: "__mig_tmp__1", newName: "postUser_ID");
            RenameColumn(table: "dbo.Comments", name: "commentUser_ID", newName: "commentUser_Email");
            RenameColumn(table: "dbo.Comments", name: "__mig_tmp__0", newName: "commentUser_ID");
            CreateIndex("dbo.Posts", new[] { "postUser_ID", "postUser_Email" });
            CreateIndex("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" });
            AddForeignKey("dbo.Posts", new[] { "postUser_ID", "postUser_Email" }, "dbo.Users", new[] { "ID", "Email" });
            AddForeignKey("dbo.Comments", new[] { "commentUser_ID", "commentUser_Email" }, "dbo.Users", new[] { "ID", "Email" });
        }
    }
}
