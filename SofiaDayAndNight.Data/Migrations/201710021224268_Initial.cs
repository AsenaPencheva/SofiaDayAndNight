namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Content = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Author_Id = c.String(maxLength: 128),
                        Image_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .Index(t => t.IsDeleted)
                .Index(t => t.Author_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        City = c.String(maxLength: 100),
                        IsForbidden = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsForbidden)
                .Index(t => t.IsDeleted)
                .Index(t => t.IsCompleted);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Individuals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Privacy = c.Int(nullable: false),
                        IsForbidden = c.Boolean(nullable: false),
                        Begins = c.DateTime(nullable: false),
                        Ends = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        EventType = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Place_Id = c.Guid(),
                        Individual_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Place_Id)
                .ForeignKey("dbo.Multimedias", t => t.Id)
                .ForeignKey("dbo.Individuals", t => t.Individual_Id)
                .Index(t => t.Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Place_Id)
                .Index(t => t.Individual_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ContentType = c.String(nullable: false),
                        Data = c.Binary(nullable: false),
                        Privacy = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        Multimedia_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Id)
                .ForeignKey("dbo.Multimedias", t => t.Multimedia_Id)
                .Index(t => t.Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Multimedia_Id);
            
            CreateTable(
                "dbo.Multimedias",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Privacy = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Location = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EventAttended",
                c => new
                    {
                        EventRefId = c.Guid(nullable: false),
                        IndividualRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.EventRefId, t.IndividualRefId })
                .ForeignKey("dbo.Events", t => t.EventRefId, cascadeDelete: true)
                .ForeignKey("dbo.Individuals", t => t.IndividualRefId, cascadeDelete: true)
                .Index(t => t.EventRefId)
                .Index(t => t.IndividualRefId);
            
            CreateTable(
                "dbo.IndividualPlaces",
                c => new
                    {
                        IndividualRefId = c.Guid(nullable: false),
                        PlaceRefId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualRefId, t.PlaceRefId })
                .ForeignKey("dbo.Individuals", t => t.IndividualRefId, cascadeDelete: true)
                .ForeignKey("dbo.Organizations", t => t.PlaceRefId, cascadeDelete: true)
                .Index(t => t.IndividualRefId)
                .Index(t => t.PlaceRefId);
            
            CreateTable(
                "dbo.IndividualFriendRequests",
                c => new
                    {
                        IndividualId = c.Guid(nullable: false),
                        RequestedFromId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualId, t.RequestedFromId })
                .ForeignKey("dbo.Individuals", t => t.IndividualId)
                .ForeignKey("dbo.Individuals", t => t.RequestedFromId)
                .Index(t => t.IndividualId)
                .Index(t => t.RequestedFromId);
            
            CreateTable(
                "dbo.IndividualFriends",
                c => new
                    {
                        IndividualId = c.Guid(nullable: false),
                        FriendId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualId, t.FriendId })
                .ForeignKey("dbo.Individuals", t => t.IndividualId)
                .ForeignKey("dbo.Individuals", t => t.FriendId)
                .Index(t => t.IndividualId)
                .Index(t => t.FriendId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Comments", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Individuals", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Individuals", "Id", "dbo.Images");
            DropForeignKey("dbo.IndividualFriends", "FriendId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "RequestedFromId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequests", "IndividualId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations");
            DropForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Individual_Id", "dbo.Individuals");
            DropForeignKey("dbo.Events", "Id", "dbo.Multimedias");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.Organizations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Organizations", "Id", "dbo.Images");
            DropForeignKey("dbo.Events", "Place_Id", "dbo.Organizations");
            DropForeignKey("dbo.Images", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Images", "Id", "dbo.Events");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.IndividualFriends", new[] { "FriendId" });
            DropIndex("dbo.IndividualFriends", new[] { "IndividualId" });
            DropIndex("dbo.IndividualFriendRequests", new[] { "RequestedFromId" });
            DropIndex("dbo.IndividualFriendRequests", new[] { "IndividualId" });
            DropIndex("dbo.IndividualPlaces", new[] { "PlaceRefId" });
            DropIndex("dbo.IndividualPlaces", new[] { "IndividualRefId" });
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropIndex("dbo.EventAttended", new[] { "EventRefId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.Organizations", new[] { "User_Id" });
            DropIndex("dbo.Organizations", new[] { "IsDeleted" });
            DropIndex("dbo.Organizations", new[] { "Id" });
            DropIndex("dbo.Multimedias", new[] { "IsDeleted" });
            DropIndex("dbo.Images", new[] { "Multimedia_Id" });
            DropIndex("dbo.Images", new[] { "IsDeleted" });
            DropIndex("dbo.Images", new[] { "Id" });
            DropIndex("dbo.Events", new[] { "Individual_Id" });
            DropIndex("dbo.Events", new[] { "Place_Id" });
            DropIndex("dbo.Events", new[] { "IsDeleted" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.Individuals", new[] { "User_Id" });
            DropIndex("dbo.Individuals", new[] { "IsDeleted" });
            DropIndex("dbo.Individuals", new[] { "Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "IsCompleted" });
            DropIndex("dbo.Users", new[] { "IsDeleted" });
            DropIndex("dbo.Users", new[] { "IsForbidden" });
            DropIndex("dbo.Comments", new[] { "Image_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropTable("dbo.IndividualFriends");
            DropTable("dbo.IndividualFriendRequests");
            DropTable("dbo.IndividualPlaces");
            DropTable("dbo.EventAttended");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.Organizations");
            DropTable("dbo.Multimedias");
            DropTable("dbo.Images");
            DropTable("dbo.Events");
            DropTable("dbo.Individuals");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
