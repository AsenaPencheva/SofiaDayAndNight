namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class splitindividualandplacefromuser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUsers", newName: "Individuals");
            DropForeignKey("dbo.Events", "Place_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.Events", new[] { "Place_Id" });
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropColumn("dbo.Events", "PlaceId");
            RenameColumn(table: "dbo.Events", name: "Place_Id", newName: "PlaceId");
            DropPrimaryKey("dbo.Individuals");
            DropPrimaryKey("dbo.EventAttended");
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Location = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Individuals", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.Individuals", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Individuals", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Individuals", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "PlaceId", c => c.Int(nullable: false));
            AlterColumn("dbo.EventAttended", "IndividualRefId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Individuals", "Id");
            AddPrimaryKey("dbo.EventAttended", new[] { "EventRefId", "IndividualRefId" });
            CreateIndex("dbo.Events", "PlaceId");
            CreateIndex("dbo.Individuals", "User_Id");
            CreateIndex("dbo.EventAttended", "IndividualRefId");
            AddForeignKey("dbo.Individuals", "User_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Events", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals", "Id", cascadeDelete: true);
            DropColumn("dbo.Individuals", "City");
            DropColumn("dbo.Individuals", "IsBanned");
            DropColumn("dbo.Individuals", "Email");
            DropColumn("dbo.Individuals", "EmailConfirmed");
            DropColumn("dbo.Individuals", "PasswordHash");
            DropColumn("dbo.Individuals", "SecurityStamp");
            DropColumn("dbo.Individuals", "PhoneNumber");
            DropColumn("dbo.Individuals", "PhoneNumberConfirmed");
            DropColumn("dbo.Individuals", "TwoFactorEnabled");
            DropColumn("dbo.Individuals", "LockoutEndDateUtc");
            DropColumn("dbo.Individuals", "LockoutEnabled");
            DropColumn("dbo.Individuals", "AccessFailedCount");
            DropColumn("dbo.Individuals", "UserName");
            DropColumn("dbo.Individuals", "Name");
            DropColumn("dbo.Individuals", "Location");
            DropColumn("dbo.Individuals", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Individuals", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Individuals", "Location", c => c.String());
            AddColumn("dbo.Individuals", "Name", c => c.String());
            AddColumn("dbo.Individuals", "UserName", c => c.String());
            AddColumn("dbo.Individuals", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Individuals", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Individuals", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Individuals", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Individuals", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Individuals", "PhoneNumber", c => c.String());
            AddColumn("dbo.Individuals", "SecurityStamp", c => c.String());
            AddColumn("dbo.Individuals", "PasswordHash", c => c.String());
            AddColumn("dbo.Individuals", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Individuals", "Email", c => c.String());
            AddColumn("dbo.Individuals", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.Individuals", "City", c => c.String(maxLength: 100));
            DropForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.Individuals");
            DropForeignKey("dbo.Events", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "User_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Individuals", "User_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.EventAttended", new[] { "IndividualRefId" });
            DropIndex("dbo.Places", new[] { "User_Id" });
            DropIndex("dbo.Individuals", new[] { "User_Id" });
            DropIndex("dbo.Events", new[] { "PlaceId" });
            DropPrimaryKey("dbo.EventAttended");
            DropPrimaryKey("dbo.Individuals");
            AlterColumn("dbo.EventAttended", "IndividualRefId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Events", "PlaceId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Individuals", "Age", c => c.Int());
            AlterColumn("dbo.Individuals", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Individuals", "User_Id");
            DropColumn("dbo.Individuals", "ApplicationUserId");
            DropTable("dbo.Places");
            DropTable("dbo.ApplicationUsers");
            AddPrimaryKey("dbo.EventAttended", new[] { "EventRefId", "IndividualRefId" });
            AddPrimaryKey("dbo.Individuals", "Id");
            RenameColumn(table: "dbo.Events", name: "PlaceId", newName: "Place_Id");
            AddColumn("dbo.Events", "PlaceId", c => c.Int(nullable: false));
            CreateIndex("dbo.EventAttended", "IndividualRefId");
            CreateIndex("dbo.Events", "Place_Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.EventAttended", "IndividualRefId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers", "Id");
            AddForeignKey("dbo.Events", "Place_Id", "dbo.ApplicationUsers", "Id");
            RenameTable(name: "dbo.Individuals", newName: "ApplicationUsers");
        }
    }
}
