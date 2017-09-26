namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abstractmodelsanddbContextfixedmodels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUsers", newName: "Users");
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Comments", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Events", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Comments", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Images", "MultimediaId", "dbo.Multimedias");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Id", "dbo.Multimedias");
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            DropIndex("dbo.Comments", new[] { "Multimedia_Id" });
            DropIndex("dbo.Comments", new[] { "Image_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.Events", new[] { "PlaceId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Images", new[] { "MultimediaId" });
            DropIndex("dbo.EventAttended", new[] { "EventRefId" });
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropColumn("dbo.IdentityUserClaims", "UserId");
            DropColumn("dbo.IdentityUserRoles", "UserId");
            RenameColumn(table: "dbo.IdentityUserClaims", name: "ApplicationUser_Id", newName: "UserId");
            RenameColumn(table: "dbo.Events", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.IdentityUserLogins", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.IdentityUserRoles", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Events", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.IdentityUserLogins", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            DropPrimaryKey("dbo.Comments");
            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Individuals");
            DropPrimaryKey("dbo.Multimedias");
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.IdentityUserRoles");
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.EventAttended");
            CreateTable(
                "dbo.IndividualPlace",
                c => new
                    {
                        IndividualRefId = c.Guid(nullable: false),
                        PlaceRefId = c.Guid(nullable: false),
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
                        IndividualRefId = c.Guid(nullable: false),
                        FriendRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualRefId, t.FriendRefId })
                .ForeignKey("dbo.Individuals", t => t.IndividualRefId)
                .ForeignKey("dbo.Individuals", t => t.FriendRefId)
                .Index(t => t.IndividualRefId)
                .Index(t => t.FriendRefId);
            
            AddColumn("dbo.Comments", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Users", "IsForbidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Users", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Users", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Events", "IsForbidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.Events", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Events", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Events", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Events", "Place_Id", c => c.Guid());
            AddColumn("dbo.Individuals", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Individuals", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Individuals", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Individuals", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Individuals", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Multimedias", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Multimedias", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Multimedias", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Places", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Places", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Places", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Places", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Places", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Images", "Name", c => c.String(nullable: false));
            AddColumn("dbo.Images", "ContentType", c => c.String(nullable: false));
            AddColumn("dbo.Images", "Data", c => c.Binary(nullable: false));
            AddColumn("dbo.Images", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Images", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.Images", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Images", "Multimedia_Id", c => c.Guid());
            AlterColumn("dbo.Comments", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "CreatedOn", c => c.DateTime());
            AlterColumn("dbo.Comments", "Image_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Events", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Events", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Events", "Description", c => c.String(maxLength: 200));
            AlterColumn("dbo.Individuals", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Individuals", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Individuals", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Multimedias", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Places", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Places", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Places", "Location", c => c.String(nullable: false));
            AlterColumn("dbo.IdentityUserRoles", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Images", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.EventAttended", "EventRefId", c => c.Guid(nullable: false));
            AlterColumn("dbo.EventAttended", "IndividualRefId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Comments", "Id");
            AddPrimaryKey("dbo.Events", "Id");
            AddPrimaryKey("dbo.Individuals", "Id");
            AddPrimaryKey("dbo.Multimedias", "Id");
            AddPrimaryKey("dbo.Places", "Id");
            AddPrimaryKey("dbo.IdentityUserRoles", new[] { "RoleId", "UserId" });
            AddPrimaryKey("dbo.Images", "Id");
            AddPrimaryKey("dbo.EventAttended", new[] { "EventRefId", "IndividualRefId" });
            CreateIndex("dbo.Comments", "IsDeleted");
            CreateIndex("dbo.Comments", "Image_Id");
            CreateIndex("dbo.IdentityUserClaims", "UserId");
            CreateIndex("dbo.Events", "Id");
            CreateIndex("dbo.Events", "IsDeleted");
            CreateIndex("dbo.Events", "Place_Id");
            CreateIndex("dbo.Individuals", "IsDeleted");
            CreateIndex("dbo.Places", "IsDeleted");
            CreateIndex("dbo.Multimedias", "IsDeleted");
            CreateIndex("dbo.IdentityUserRoles", "UserId");
            CreateIndex("dbo.Images", "IsDeleted");
            CreateIndex("dbo.Images", "Multimedia_Id");
            CreateIndex("dbo.EventAttended", "EventRefId");
            CreateIndex("dbo.EventAttended", "IndividualRefId");
            AddForeignKey("dbo.Events", "Place_Id", "dbo.Places", "Id");
            AddForeignKey("dbo.Comments", "Image_Id", "dbo.Images", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Images", "Multimedia_Id", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Events", "Id", "dbo.Multimedias", "Id");
            DropColumn("dbo.Comments", "Event_Id");
            DropColumn("dbo.Comments", "Multimedia_Id");
            DropColumn("dbo.Users", "IsBanned");
            DropColumn("dbo.Events", "IsBanned");
            DropColumn("dbo.Individuals", "ApplicationUserId");
            DropColumn("dbo.Places", "ApplicationUserId");
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
            
            AddColumn("dbo.Places", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Individuals", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Events", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "Multimedia_Id", c => c.Int());
            AddColumn("dbo.Comments", "Event_Id", c => c.Int());
            DropForeignKey("dbo.Events", "Id", "dbo.Multimedias");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Images", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Comments", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Events", "Place_Id", "dbo.Places");
            DropForeignKey("dbo.IndividualFriends", "FriendRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlace", "PlaceRefId", "dbo.Places");
            DropForeignKey("dbo.IndividualPlace", "IndividualRefId", "dbo.Individuals");
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropIndex("dbo.EventAttended", new[] { "EventRefId" });
            DropIndex("dbo.IndividualFriends", new[] { "FriendRefId" });
            DropIndex("dbo.IndividualFriends", new[] { "IndividualRefId" });
            DropIndex("dbo.IndividualPlace", new[] { "PlaceRefId" });
            DropIndex("dbo.IndividualPlace", new[] { "IndividualRefId" });
            DropIndex("dbo.Images", new[] { "Multimedia_Id" });
            DropIndex("dbo.Images", new[] { "IsDeleted" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.Multimedias", new[] { "IsDeleted" });
            DropIndex("dbo.Places", new[] { "IsDeleted" });
            DropIndex("dbo.Individuals", new[] { "IsDeleted" });
            DropIndex("dbo.Events", new[] { "Place_Id" });
            DropIndex("dbo.Events", new[] { "IsDeleted" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "Image_Id" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropPrimaryKey("dbo.EventAttended");
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.IdentityUserRoles");
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.Multimedias");
            DropPrimaryKey("dbo.Individuals");
            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.EventAttended", "IndividualRefId", c => c.Int(nullable: false));
            AlterColumn("dbo.EventAttended", "EventRefId", c => c.Int(nullable: false));
            AlterColumn("dbo.Images", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.IdentityUserRoles", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Places", "Location", c => c.String());
            AlterColumn("dbo.Places", "Name", c => c.String());
            AlterColumn("dbo.Places", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Multimedias", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Individuals", "LastName", c => c.String());
            AlterColumn("dbo.Individuals", "FirstName", c => c.String());
            AlterColumn("dbo.Individuals", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "Title", c => c.String());
            AlterColumn("dbo.Events", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String());
            AlterColumn("dbo.Comments", "Image_Id", c => c.Int());
            AlterColumn("dbo.Comments", "CreatedOn", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "Content", c => c.String());
            AlterColumn("dbo.Comments", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Images", "Multimedia_Id");
            DropColumn("dbo.Images", "ModifiedOn");
            DropColumn("dbo.Images", "CreatedOn");
            DropColumn("dbo.Images", "DeletedOn");
            DropColumn("dbo.Images", "Data");
            DropColumn("dbo.Images", "ContentType");
            DropColumn("dbo.Images", "Name");
            DropColumn("dbo.Places", "ModifiedOn");
            DropColumn("dbo.Places", "CreatedOn");
            DropColumn("dbo.Places", "DeletedOn");
            DropColumn("dbo.Places", "IsDeleted");
            DropColumn("dbo.Places", "UserId");
            DropColumn("dbo.Multimedias", "ModifiedOn");
            DropColumn("dbo.Multimedias", "CreatedOn");
            DropColumn("dbo.Multimedias", "DeletedOn");
            DropColumn("dbo.Individuals", "ModifiedOn");
            DropColumn("dbo.Individuals", "CreatedOn");
            DropColumn("dbo.Individuals", "DeletedOn");
            DropColumn("dbo.Individuals", "IsDeleted");
            DropColumn("dbo.Individuals", "UserId");
            DropColumn("dbo.Events", "Place_Id");
            DropColumn("dbo.Events", "ModifiedOn");
            DropColumn("dbo.Events", "CreatedOn");
            DropColumn("dbo.Events", "DeletedOn");
            DropColumn("dbo.Events", "IsForbidden");
            DropColumn("dbo.Users", "ModifiedOn");
            DropColumn("dbo.Users", "CreatedOn");
            DropColumn("dbo.Users", "DeletedOn");
            DropColumn("dbo.Users", "IsDeleted");
            DropColumn("dbo.Users", "IsForbidden");
            DropColumn("dbo.Comments", "ModifiedOn");
            DropColumn("dbo.Comments", "DeletedOn");
            DropColumn("dbo.Comments", "IsDeleted");
            DropColumn("dbo.Comments", "ImageId");
            DropTable("dbo.IndividualFriends");
            DropTable("dbo.IndividualPlace");
            AddPrimaryKey("dbo.EventAttended", new[] { "EventRefId", "IndividualRefId" });
            AddPrimaryKey("dbo.Images", "Id");
            AddPrimaryKey("dbo.IdentityUserRoles", new[] { "RoleId", "UserId" });
            AddPrimaryKey("dbo.Places", "Id");
            AddPrimaryKey("dbo.Multimedias", "Id");
            AddPrimaryKey("dbo.Individuals", "Id");
            AddPrimaryKey("dbo.Events", "Id");
            AddPrimaryKey("dbo.Comments", "Id");
            RenameIndex(table: "dbo.IdentityUserLogins", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Events", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.IdentityUserRoles", name: "UserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.IdentityUserLogins", name: "User_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Events", name: "User_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.IdentityUserClaims", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.IdentityUserRoles", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.IdentityUserClaims", "UserId", c => c.String());
            CreateIndex("dbo.EventAttended", "IndividualRefId");
            CreateIndex("dbo.EventAttended", "EventRefId");
            CreateIndex("dbo.Images", "MultimediaId");
            CreateIndex("dbo.IdentityUserRoles", "ApplicationUser_Id");
            CreateIndex("dbo.Events", "PlaceId");
            CreateIndex("dbo.Events", "Id");
            CreateIndex("dbo.IdentityUserClaims", "ApplicationUser_Id");
            CreateIndex("dbo.Comments", "Image_Id");
            CreateIndex("dbo.Comments", "Multimedia_Id");
            CreateIndex("dbo.Comments", "Event_Id");
            AddForeignKey("dbo.Events", "Id", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Images", "MultimediaId", "dbo.Multimedias", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "Image_Id", "dbo.Images", "Id");
            AddForeignKey("dbo.Events", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "Multimedia_Id", "dbo.Multimedias", "Id");
            AddForeignKey("dbo.Comments", "Event_Id", "dbo.Events", "Id");
            RenameTable(name: "dbo.Users", newName: "ApplicationUsers");
        }
    }
}
