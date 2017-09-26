namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedattributesandcollectionsfixedmodels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Comments", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Comments", "Image_Id", "dbo.Images");
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            DropIndex("dbo.Comments", new[] { "Multimedia_Id" });
            DropIndex("dbo.Comments", new[] { "Image_Id" });
            RenameColumn(table: "dbo.Comments", name: "Image_Id", newName: "ImageId");
            CreateTable(
                "dbo.IndividualPlace",
                c => new
                    {
                        IndividualRefId = c.Int(nullable: false),
                        PlaceRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualRefId, t.PlaceRefId })
                .ForeignKey("dbo.Individuals", t => t.IndividualRefId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceRefId, cascadeDelete: true)
                .Index(t => t.IndividualRefId)
                .Index(t => t.PlaceRefId);
            
            CreateTable(
                "dbo.IndividualFriends",
                c => new
                    {
                        IndividualRefId = c.Int(nullable: false),
                        FriendRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualRefId, t.FriendRefId })
                .ForeignKey("dbo.Individuals", t => t.IndividualRefId)
                .ForeignKey("dbo.Individuals", t => t.FriendRefId)
                .Index(t => t.IndividualRefId)
                .Index(t => t.FriendRefId);
            
            AddColumn("dbo.Comments", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Images", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Images", "ContentType", c => c.String(nullable: false));
            AddColumn("dbo.Images", "Data", c => c.Binary(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Events", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.Individuals", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Individuals", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Places", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Places", "Location", c => c.String(nullable: false));
            CreateIndex("dbo.Comments", "ImageId");
            AddForeignKey("dbo.Comments", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
            DropColumn("dbo.Comments", "UserId");
            DropColumn("dbo.Comments", "Event_Id");
            DropColumn("dbo.Comments", "Multimedia_Id");
            DropTable("dbo.IndividualEvents");
            DropTable("dbo.IndividualPlaces");
            DropTable("dbo.IndividualFriends");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.IndividualFriends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Privacy = c.Int(nullable: false),
                        IndividualId = c.Int(nullable: false),
                        FriendId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IndividualPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Privacy = c.Int(nullable: false),
                        IndividualId = c.Int(nullable: false),
                        PlaceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            AddColumn("dbo.Comments", "Multimedia_Id", c => c.Int());
            AddColumn("dbo.Comments", "Event_Id", c => c.Int());
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "ImageId", "dbo.Images");
            DropForeignKey("dbo.IndividualFriends", "FriendRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlace", "PlaceRefId", "dbo.Places");
            DropForeignKey("dbo.IndividualPlace", "IndividualRefId", "dbo.Individuals");
            DropIndex("dbo.IndividualFriends", new[] { "FriendRefId" });
            DropIndex("dbo.IndividualFriends", new[] { "IndividualRefId" });
            DropIndex("dbo.IndividualPlace", new[] { "PlaceRefId" });
            DropIndex("dbo.IndividualPlace", new[] { "IndividualRefId" });
            DropIndex("dbo.Comments", new[] { "ImageId" });
            AlterColumn("dbo.Places", "Location", c => c.String());
            AlterColumn("dbo.Places", "Name", c => c.String());
            AlterColumn("dbo.Individuals", "LastName", c => c.String());
            AlterColumn("dbo.Individuals", "FirstName", c => c.String());
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "Title", c => c.String());
            AlterColumn("dbo.Comments", "ImageId", c => c.Int());
            AlterColumn("dbo.Comments", "Content", c => c.String());
            DropColumn("dbo.Images", "Data");
            DropColumn("dbo.Images", "ContentType");
            DropColumn("dbo.Images", "Name");
            DropColumn("dbo.Comments", "isDeleted");
            DropColumn("dbo.Comments", "ApplicationUserId");
            DropTable("dbo.IndividualFriends");
            DropTable("dbo.IndividualPlace");
            RenameColumn(table: "dbo.Comments", name: "ImageId", newName: "Image_Id");
            CreateIndex("dbo.Comments", "Image_Id");
            CreateIndex("dbo.Comments", "Multimedia_Id");
            CreateIndex("dbo.Comments", "Event_Id");
            AddForeignKey("dbo.Comments", "Image_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.Comments", "Multimedia_Id", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.Comments", "Event_Id", "dbo.Events", "Id");
        }
    }
}
