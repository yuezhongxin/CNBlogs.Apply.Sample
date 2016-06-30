namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogChangeApplys1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogChangeApplys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TargetBlogApp = c.String(nullable: false, maxLength: 50),
                        Reason = c.String(nullable: false, maxLength: 3000),
                        User_DisplayName = c.String(nullable: false, maxLength: 128),
                        User_Alias = c.String(nullable: false, maxLength: 50),
                        User_RegisterTime = c.DateTime(),
                        User_Id = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Ip = c.String(maxLength: 50),
                        ApplyTime = c.DateTime(nullable: false),
                        ReplyContent = c.String(maxLength: 3000),
                        ApprovedTime = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.JsPermissionApplys", "User_DisplayName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.JsPermissionApplys", "User_Alias", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JsPermissionApplys", "User_Alias", c => c.String(maxLength: 50));
            AlterColumn("dbo.JsPermissionApplys", "User_DisplayName", c => c.String(maxLength: 128));
            DropTable("dbo.BlogChangeApplys");
        }
    }
}
