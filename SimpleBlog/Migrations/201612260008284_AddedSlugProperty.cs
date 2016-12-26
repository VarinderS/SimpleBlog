namespace SimpleBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSlugProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Slug", c => c.String(maxLength: 256));
            AddColumn("dbo.Tags", "Slug", c => c.String(maxLength: 256));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tags", "Slug");
            DropColumn("dbo.Posts", "Slug");
        }
    }
}
