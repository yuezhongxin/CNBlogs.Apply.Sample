namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetApprovedTimenull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JsPermissionApplys", "ApprovedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JsPermissionApplys", "ApprovedTime", c => c.DateTime(nullable: false));
        }
    }
}
