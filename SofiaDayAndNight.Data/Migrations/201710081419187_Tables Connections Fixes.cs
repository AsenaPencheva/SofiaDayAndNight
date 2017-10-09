namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TablesConnectionsFixes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Multimedia_EventId", "dbo.Multimedias");
            DropForeignKey("dbo.Events", "ImageId", "dbo.Multimedias");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Individual_ImageId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequested", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequested", "RequestedToId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals");
            DropForeignKey("dbo.Comments", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.Events", "Place_ImageId", "dbo.Organizations");
            DropForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations");
            DropIndex("dbo.Images", new[] { "Multimedia_EventId" });
            RenameColumn(table: "dbo.Events", name: "Individual_ImageId", newName: "Individual_Id");
            RenameColumn(table: "dbo.Events", name: "Place_ImageId", newName: "Organization_Id");
            RenameIndex(table: "dbo.Events", name: "IX_Place_ImageId", newName: "IX_Organization_Id");
            RenameIndex(table: "dbo.Events", name: "IX_Individual_ImageId", newName: "IX_Individual_Id");
            DropPrimaryKey("dbo.Individuals");
            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Multimedias");
            DropPrimaryKey("dbo.Organizations");
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
            AddPrimaryKey("dbo.Individuals", "Id");
            AddPrimaryKey("dbo.Events", "Id");
            AddPrimaryKey("dbo.Multimedias", "Id");
            AddPrimaryKey("dbo.Organizations", "Id");
            CreateIndex("dbo.Events", "MultimediaId");
            AddForeignKey("dbo.Events", "MultimediaId", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Individual_Id", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IndividualFriendRequested", "IndividualId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriendRequested", "RequestedToId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.Comments", "IndividualId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Organization_Id", "dbo.Organizations", "Id");
            AddForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations", "Id", cascadeDelete: true);
            DropColumn("dbo.Images", "Multimedia_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Multimedia_EventId", c => c.Guid());
            DropForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations");
            DropForeignKey("dbo.Events", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.Comments", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequested", "RequestedToId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequested", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Individual_Id", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "MultimediaId", "dbo.Multimedias");
            DropForeignKey("dbo.MultimediaImages", "ImageId", "dbo.Images");
            DropForeignKey("dbo.MultimediaImages", "MultimediaId", "dbo.Multimedias");
            DropIndex("dbo.MultimediaImages", new[] { "ImageId" });
            DropIndex("dbo.MultimediaImages", new[] { "MultimediaId" });
            DropIndex("dbo.Events", new[] { "MultimediaId" });
            DropPrimaryKey("dbo.Organizations");
            DropPrimaryKey("dbo.Multimedias");
            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Individuals");
            DropColumn("dbo.Events", "MultimediaId");
            DropTable("dbo.MultimediaImages");
            AddPrimaryKey("dbo.Organizations", "ImageId");
            AddPrimaryKey("dbo.Multimedias", "EventId");
            AddPrimaryKey("dbo.Events", "ImageId");
            AddPrimaryKey("dbo.Individuals", "ImageId");
            RenameIndex(table: "dbo.Events", name: "IX_Individual_Id", newName: "IX_Individual_ImageId");
            RenameIndex(table: "dbo.Events", name: "IX_Organization_Id", newName: "IX_Place_ImageId");
            RenameColumn(table: "dbo.Events", name: "Organization_Id", newName: "Place_ImageId");
            RenameColumn(table: "dbo.Events", name: "Individual_Id", newName: "Individual_ImageId");
            CreateIndex("dbo.Images", "Multimedia_EventId");
            AddForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Place_ImageId", "dbo.Organizations", "ImageId");
            AddForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "IndividualId", "dbo.Individuals", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriendRequested", "RequestedToId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriendRequested", "IndividualId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Individual_ImageId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Events", "ImageId", "dbo.Multimedias", "EventId");
            AddForeignKey("dbo.Images", "Multimedia_EventId", "dbo.Multimedias", "EventId");
        }
    }
}
