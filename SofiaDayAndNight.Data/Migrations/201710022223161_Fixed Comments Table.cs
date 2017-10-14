namespace SofiaDayAndNight.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCommentsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            RenameColumn(table: "dbo.Comments", name: "Author_Id", newName: "IndividualId");
            RenameColumn(table: "dbo.Comments", name: "Image_Id", newName: "ImageId");
            RenameIndex(table: "dbo.Comments", name: "IX_Image_Id", newName: "IX_ImageId");
            AlterColumn("dbo.Comments", "IndividualId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Comments", "IndividualId");
            AddForeignKey("dbo.Comments", "IndividualId", "dbo.Individuals", "ImageId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "IndividualId", "dbo.Individuals");
            DropIndex("dbo.Comments", new[] { "IndividualId" });
            AlterColumn("dbo.Comments", "IndividualId", c => c.String(maxLength: 128));
            RenameIndex(table: "dbo.Comments", name: "IX_ImageId", newName: "IX_Image_Id");
            RenameColumn(table: "dbo.Comments", name: "ImageId", newName: "Image_Id");
            RenameColumn(table: "dbo.Comments", name: "IndividualId", newName: "Author_Id");
            CreateIndex("dbo.Comments", "Author_Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.Users", "Id");
        }
    }
}
