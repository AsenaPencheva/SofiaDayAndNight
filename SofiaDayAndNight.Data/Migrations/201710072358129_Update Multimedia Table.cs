namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMultimediaTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Multimedia_EventId", "dbo.Multimedias");
            DropIndex("dbo.Images", new[] { "Multimedia_EventId" });
            CreateTable(
                "dbo.MultimediaImages",
                c => new
                    {
                        MultimediaId = c.Guid(nullable: false),
                        ImageId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.MultimediaId, t.ImageId })
                .ForeignKey("dbo.Multimedias", t => t.MultimediaId, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.ImageId, cascadeDelete: true)
                .Index(t => t.MultimediaId)
                .Index(t => t.ImageId);
            
            AddColumn("dbo.Events", "MultimediaId", c => c.Guid(nullable: false));
            DropColumn("dbo.Images", "Multimedia_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Multimedia_EventId", c => c.Guid());
            DropForeignKey("dbo.MultimediaImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.MultimediaImages", "MultimediaId", "dbo.Multimedias");
            DropIndex("dbo.MultimediaImages", new[] { "ImageId" });
            DropIndex("dbo.MultimediaImages", new[] { "MultimediaId" });
            DropColumn("dbo.Events", "MultimediaId");
            DropTable("dbo.MultimediaImages");
            CreateIndex("dbo.Images", "Multimedia_EventId");
            AddForeignKey("dbo.Images", "Multimedia_EventId", "dbo.Multimedias", "EventId");
        }
    }
}
