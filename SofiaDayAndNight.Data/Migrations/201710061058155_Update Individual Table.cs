namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIndividualTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndividualFriendRequested",
                c => new
                    {
                        IndividualId = c.Guid(nullable: false),
                        RequestedToId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IndividualId, t.RequestedToId })
                .ForeignKey("dbo.Individuals", t => t.IndividualId)
                .ForeignKey("dbo.Individuals", t => t.RequestedToId)
                .Index(t => t.IndividualId)
                .Index(t => t.RequestedToId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualFriendRequested", "RequestedToId", "dbo.Individuals");
            DropForeignKey("dbo.IndividualFriendRequested", "IndividualId", "dbo.Individuals");
            DropIndex("dbo.IndividualFriendRequested", new[] { "RequestedToId" });
            DropIndex("dbo.IndividualFriendRequested", new[] { "IndividualId" });
            DropTable("dbo.IndividualFriendRequested");
        }
    }
}
