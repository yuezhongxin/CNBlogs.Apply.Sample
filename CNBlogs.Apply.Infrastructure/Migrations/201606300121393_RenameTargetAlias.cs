namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTargetAlias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogChangeApplys", "TargetBlogApp", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.BlogChangeApplys", "TargetAlias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogChangeApplys", "TargetAlias", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.BlogChangeApplys", "TargetBlogApp");
        }
    }
}
