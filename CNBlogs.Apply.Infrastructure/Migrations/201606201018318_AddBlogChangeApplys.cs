namespace CNBlogs.Apply.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBlogChangeApplys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogChangeApplys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Reason = c.String(nullable: false, maxLength: 3000),
                        User_DisplayName = c.String(maxLength: 128),
                        User_Alias = c.String(maxLength: 50),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BlogChangeApplys");
        }
    }
}
