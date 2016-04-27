namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserLoginNameUserDisplayNameUserAliasUserEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JsPermissionApplys", "UserLoginName", c => c.String(maxLength: 128));
            AddColumn("dbo.JsPermissionApplys", "UserDisplayName", c => c.String(maxLength: 128));
            AddColumn("dbo.JsPermissionApplys", "UserAlias", c => c.String(maxLength: 50));
            AddColumn("dbo.JsPermissionApplys", "UserEmail", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JsPermissionApplys", "UserEmail");
            DropColumn("dbo.JsPermissionApplys", "UserAlias");
            DropColumn("dbo.JsPermissionApplys", "UserDisplayName");
            DropColumn("dbo.JsPermissionApplys", "UserLoginName");
        }
    }
}
