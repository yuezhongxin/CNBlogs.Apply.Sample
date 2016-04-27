namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginNameWithUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JsPermissionApplys", "User_LoginName", c => c.String(maxLength: 128));
            AddColumn("dbo.JsPermissionApplys", "User_DisplayName", c => c.String());
            AddColumn("dbo.JsPermissionApplys", "User_Email", c => c.String());
            AddColumn("dbo.JsPermissionApplys", "User_Alias", c => c.String());
            AddColumn("dbo.JsPermissionApplys", "User_Id", c => c.Int(nullable: false));
            DropColumn("dbo.JsPermissionApplys", "UserLoginName");
            DropColumn("dbo.JsPermissionApplys", "UserDisplayName");
            DropColumn("dbo.JsPermissionApplys", "UserAlias");
            DropColumn("dbo.JsPermissionApplys", "UserEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JsPermissionApplys", "UserEmail", c => c.String(maxLength: 128));
            AddColumn("dbo.JsPermissionApplys", "UserAlias", c => c.String(maxLength: 50));
            AddColumn("dbo.JsPermissionApplys", "UserDisplayName", c => c.String(maxLength: 128));
            AddColumn("dbo.JsPermissionApplys", "UserLoginName", c => c.String(maxLength: 128));
            DropColumn("dbo.JsPermissionApplys", "User_Id");
            DropColumn("dbo.JsPermissionApplys", "User_Alias");
            DropColumn("dbo.JsPermissionApplys", "User_Email");
            DropColumn("dbo.JsPermissionApplys", "User_DisplayName");
            DropColumn("dbo.JsPermissionApplys", "User_LoginName");
        }
    }
}
