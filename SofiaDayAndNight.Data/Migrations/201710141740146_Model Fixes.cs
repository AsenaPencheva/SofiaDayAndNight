namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelFixes : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "IsForbidden" });
            AddColumn("dbo.Events", "AccessType", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "AccessType", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "AccessType");
            DropColumn("dbo.Events", "IsForbidden");
            DropColumn("dbo.Users", "IsForbidden");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsForbidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "IsForbidden", c => c.Boolean(nullable: false));
            DropIndex("dbo.Users", new[] { "AccessType" });
            DropColumn("dbo.Users", "AccessType");
            DropColumn("dbo.Events", "AccessType");
            CreateIndex("dbo.Users", "IsForbidden");
        }
    }
}
