namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Content = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        Event_Id = c.Int(),
                        Multimedia_Id = c.Int(),
                        Author_Id = c.String(maxLength: 128),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.Multimedias", t => t.Multimedia_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Author_Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Multimedia_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        City = c.String(maxLength: 100),
                        IsBanned = c.Boolean(nullable: false),
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
                        Name = c.String(),
                        Location = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        AgeRestriction = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        IsBanned = c.Boolean(nullable: false),
                        Privacy = c.Int(nullable: false),
                        Begins = c.DateTime(nullable: false),
                        Ends = c.DateTime(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        EventType = c.Int(nullable: false),
                        MultimediaId = c.Int(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        Place_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Multimedias", t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.Place_Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Id)
                .Index(t => t.Place_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Multimedias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Privacy = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsDeleted = c.Boolean(nullable: false),
                        Privacy = c.Int(nullable: false),
                        Likes = c.Int(nullable: false),
                        Dislikes = c.Int(nullable: false),
                        MultimediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Multimedias", t => t.MultimediaId, cascadeDelete: true)
                .Index(t => t.MultimediaId);
            
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
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Images", "MultimediaId", "dbo.Multimedias");
            DropForeignKey("dbo.Comments", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Events", "Place_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Events", "Id", "dbo.Multimedias");
            DropForeignKey("dbo.Comments", "Multimedia_Id", "dbo.Multimedias");
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Images", new[] { "MultimediaId" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "Place_Id" });
            DropIndex("dbo.Events", new[] { "Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Comments", new[] { "Image_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.Comments", new[] { "Multimedia_Id" });
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            DropTable("dbo.UserEvents");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IndividualFriends");
            DropTable("dbo.IndividualPlaces");
            DropTable("dbo.Images");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.Multimedias");
            DropTable("dbo.Events");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.Comments");
        }
    }
}
