namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixedrelations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Individuals", "Id", "dbo.Images");
            DropForeignKey("dbo.Images", "Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Id", "dbo.Multimedias");
            DropForeignKey("dbo.Organizations", "Id", "dbo.Images");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Individual_Id", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.Images", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Events", "Place_Id", "dbo.Organizations");
            DropForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations");
            DropIndex("dbo.Individuals", new[] { "Id" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.Images", new[] { "Id" });
            DropIndex("dbo.Organizations", new[] { "Id" });
            RenameColumn(table: "dbo.Events", name: "Individual_Id", newName: "Individual_ImageId");
            RenameColumn(table: "dbo.Events", name: "Place_Id", newName: "Place_ImageId");
            RenameColumn(table: "dbo.Images", name: "Multimedia_Id", newName: "Multimedia_EventId");
            RenameIndex(table: "dbo.Events", name: "IX_Place_Id", newName: "IX_Place_ImageId");
            RenameIndex(table: "dbo.Events", name: "IX_Individual_Id", newName: "IX_Individual_ImageId");
            RenameIndex(table: "dbo.Images", name: "IX_Multimedia_Id", newName: "IX_Multimedia_EventId");
            DropPrimaryKey("dbo.Individuals");
            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Multimedias");
            DropPrimaryKey("dbo.Organizations");
            AddColumn("dbo.Individuals", "ImageId", c => c.Guid(nullable: false));
            AddColumn("dbo.Events", "ImageId", c => c.Guid(nullable: false));
            AddColumn("dbo.Multimedias", "EventId", c => c.Guid(nullable: false));
            AddColumn("dbo.Organizations", "ImageId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Individuals", "ImageId");
            AddPrimaryKey("dbo.Events", "ImageId");
            AddPrimaryKey("dbo.Multimedias", "EventId");
            AddPrimaryKey("dbo.Organizations", "ImageId");
            CreateIndex("dbo.Individuals", "ImageId");
            CreateIndex("dbo.Events", "ImageId");
            CreateIndex("dbo.Organizations", "ImageId");
            AddForeignKey("dbo.Individuals", "ImageId", "dbo.Images", "Id");
            AddForeignKey("dbo.Events", "ImageId", "dbo.Images", "Id");
            AddForeignKey("dbo.Events", "ImageId", "dbo.Multimedias", "EventId");
            AddForeignKey("dbo.Organizations", "ImageId", "dbo.Images", "Id");
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Individual_ImageId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals", "ImageId");
            AddForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events", "ImageId", cascadeDelete: true);
            AddForeignKey("dbo.Images", "Multimedia_EventId", "dbo.Multimedias", "EventId");
            AddForeignKey("dbo.Events", "Place_ImageId", "dbo.Organizations", "ImageId");
            AddForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations", "ImageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations");
            DropForeignKey("dbo.Events", "Place_ImageId", "dbo.Organizations");
            DropForeignKey("dbo.Images", "Multimedia_EventId", "dbo.Multimedias");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Individual_ImageId", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Organizations", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Events", "ImageId", "dbo.Multimedias");
            DropForeignKey("dbo.Events", "ImageId", "dbo.Images");
            DropForeignKey("dbo.Individuals", "ImageId", "dbo.Images");
            DropIndex("dbo.Organizations", new[] { "ImageId" });
            DropIndex("dbo.Events", new[] { "ImageId" });
            DropIndex("dbo.Individuals", new[] { "ImageId" });
            DropPrimaryKey("dbo.Organizations");
            DropPrimaryKey("dbo.Multimedias");
            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Individuals");
            DropColumn("dbo.Organizations", "ImageId");
            DropColumn("dbo.Multimedias", "EventId");
            DropColumn("dbo.Events", "ImageId");
            DropColumn("dbo.Individuals", "ImageId");
            AddPrimaryKey("dbo.Organizations", "Id");
            AddPrimaryKey("dbo.Multimedias", "Id");
            AddPrimaryKey("dbo.Events", "Id");
            AddPrimaryKey("dbo.Individuals", "Id");
            RenameIndex(table: "dbo.Images", name: "IX_Multimedia_EventId", newName: "IX_Multimedia_Id");
            RenameIndex(table: "dbo.Events", name: "IX_Individual_ImageId", newName: "IX_Individual_Id");
            RenameIndex(table: "dbo.Events", name: "IX_Place_ImageId", newName: "IX_Place_Id");
            RenameColumn(table: "dbo.Images", name: "Multimedia_EventId", newName: "Multimedia_Id");
            RenameColumn(table: "dbo.Events", name: "Place_ImageId", newName: "Place_Id");
            RenameColumn(table: "dbo.Events", name: "Individual_ImageId", newName: "Individual_Id");
            CreateIndex("dbo.Organizations", "Id");
            CreateIndex("dbo.Images", "Id");
            CreateIndex("dbo.Events", "Id");
            CreateIndex("dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Place_Id", "dbo.Organizations", "Id");
            AddForeignKey("dbo.Images", "Multimedia_Id", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals", "Id");
            AddForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Individual_Id", "dbo.Individuals", "Id");
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Organizations", "Id", "dbo.Images", "Id");
            AddForeignKey("dbo.Events", "Id", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.Images", "Id", "dbo.Events", "Id");
            AddForeignKey("dbo.Individuals", "Id", "dbo.Images", "Id");
        }
    }
}
