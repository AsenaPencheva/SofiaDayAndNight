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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        City = c.String(maxLength: 100),
                        IsForbidden = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                .Index(t => t.IsDeleted);
            
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
                        Individual_Id = c.Guid(),
                        Place_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Individuals", t => t.Individual_Id)
                .ForeignKey("dbo.Organizations", t => t.Place_Id)
                .ForeignKey("dbo.Multimedias", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Individual_Id)
                .Index(t => t.Place_Id);
            
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
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);
            
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
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);
            
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
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Events", "Id", "dbo.Multimedias");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.EventAttended", "EventRefId", "dbo.Events");
            DropForeignKey("dbo.Individuals", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IndividualFriends", "FriendRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriends", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualPlaces", "PlaceRefId", "dbo.Organizations");
            DropForeignKey("dbo.IndividualPlaces", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Organizations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Events", "Place_Id", "dbo.Organizations");
            DropForeignKey("dbo.Events", "Individual_Id", "dbo.Individuals");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropIndex("dbo.EventAttended", new[] { "EventRefId" });
            DropIndex("dbo.IndividualFriends", new[] { "FriendRefId" });
            DropIndex("dbo.IndividualFriends", new[] { "IndividualRefId" });
            DropIndex("dbo.IndividualPlaces", new[] { "PlaceRefId" });
            DropIndex("dbo.IndividualPlaces", new[] { "IndividualRefId" });
            DropIndex("dbo.Multimedias", new[] { "IsDeleted" });
            DropIndex("dbo.Organizations", new[] { "User_Id" });
            DropIndex("dbo.Organizations", new[] { "IsDeleted" });
            DropIndex("dbo.Individuals", new[] { "User_Id" });
            DropIndex("dbo.Individuals", new[] { "IsDeleted" });
            DropIndex("dbo.Events", new[] { "Place_Id" });
            DropIndex("dbo.Events", new[] { "Individual_Id" });
            DropIndex("dbo.Events", new[] { "IsDeleted" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "IsDeleted" });
            DropIndex("dbo.Users", new[] { "IsForbidden" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropTable("dbo.EventAttended");
            DropTable("dbo.IndividualFriends");
            DropTable("dbo.IndividualPlaces");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Multimedias");
            DropTable("dbo.Organizations");
            DropTable("dbo.Individuals");
            DropTable("dbo.Events");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
