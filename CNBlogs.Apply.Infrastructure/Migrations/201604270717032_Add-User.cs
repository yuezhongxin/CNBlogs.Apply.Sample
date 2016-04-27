namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JsPermissionApplys", "User_DisplayName", c => c.String(maxLength: 128));
            AlterColumn("dbo.JsPermissionApplys", "User_Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.JsPermissionApplys", "User_Alias", c => c.String(maxLength: 50));
            DropColumn("dbo.JsPermissionApplys", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JsPermissionApplys", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.JsPermissionApplys", "User_Alias", c => c.String());
            AlterColumn("dbo.JsPermissionApplys", "User_Email", c => c.String());
            AlterColumn("dbo.JsPermissionApplys", "User_DisplayName", c => c.String());
        }
    }
}
