namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updates : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "Individual_Id", newName: "IndividualId");
            RenameColumn(table: "dbo.Events", name: "Organization_Id", newName: "OrganizationId");
            RenameIndex(table: "dbo.Events", name: "IX_Individual_Id", newName: "IX_IndividualId");
            RenameIndex(table: "dbo.Events", name: "IX_Organization_Id", newName: "IX_OrganizationId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Events", name: "IX_OrganizationId", newName: "IX_Organization_Id");
            RenameIndex(table: "dbo.Events", name: "IX_IndividualId", newName: "IX_Individual_Id");
            RenameColumn(table: "dbo.Events", name: "OrganizationId", newName: "Organization_Id");
            RenameColumn(table: "dbo.Events", name: "IndividualId", newName: "Individual_Id");
        }
    }
}
