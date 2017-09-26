namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventattendencetables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndividualEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Privacy = c.Int(nullable: false),
                        IndividualId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventAttended",
                c => new
                    {
                        EventRefId = c.Int(nullable: false),
                        IndividualRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.EventRefId, t.IndividualRefId })
                .ForeignKey("dbo.Events", t => t.EventRefId, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.IndividualRefId, cascadeDelete: true)
                .Index(t => t.EventRefId)
                .Index(t => t.IndividualRefId);
            
            DropTable("dbo.UserEvents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Privacy = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropIndex("dbo.EventAttended", new[] { "EventRefId" });
            DropTable("dbo.EventAttended");
            DropTable("dbo.IndividualEvents");
        }
    }
}
