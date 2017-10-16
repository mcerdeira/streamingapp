namespace streamingapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelbigchangeII : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Movie", "Director_Id", "dbo.Director");
            DropForeignKey("dbo.Movie", "Media_MediaLink", "dbo.RelatedMedia");
            DropIndex("dbo.Movie", new[] { "Director_Id" });
            DropIndex("dbo.Movie", new[] { "Media_MediaLink" });
            RenameColumn(table: "dbo.Movie", name: "Director_Id", newName: "DirectorId");
            RenameColumn(table: "dbo.Movie", name: "Media_MediaLink", newName: "MediaId");
            DropPrimaryKey("dbo.RelatedMedia");
            AddColumn("dbo.RelatedMedia", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Movie", "DirectorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Movie", "MediaId", c => c.Int(nullable: false));
            AlterColumn("dbo.RelatedMedia", "MediaLink", c => c.String(nullable: false));
            AddPrimaryKey("dbo.RelatedMedia", "Id");
            CreateIndex("dbo.Movie", "MediaId");
            CreateIndex("dbo.Movie", "DirectorId");
            AddForeignKey("dbo.Movie", "DirectorId", "dbo.Director", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Movie", "MediaId", "dbo.RelatedMedia", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie", "MediaId", "dbo.RelatedMedia");
            DropForeignKey("dbo.Movie", "DirectorId", "dbo.Director");
            DropIndex("dbo.Movie", new[] { "DirectorId" });
            DropIndex("dbo.Movie", new[] { "MediaId" });
            DropPrimaryKey("dbo.RelatedMedia");
            AlterColumn("dbo.RelatedMedia", "MediaLink", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Movie", "MediaId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Movie", "DirectorId", c => c.Int());
            DropColumn("dbo.RelatedMedia", "Id");
            AddPrimaryKey("dbo.RelatedMedia", "MediaLink");
            RenameColumn(table: "dbo.Movie", name: "MediaId", newName: "Media_MediaLink");
            RenameColumn(table: "dbo.Movie", name: "DirectorId", newName: "Director_Id");
            CreateIndex("dbo.Movie", "Media_MediaLink");
            CreateIndex("dbo.Movie", "Director_Id");
            AddForeignKey("dbo.Movie", "Media_MediaLink", "dbo.RelatedMedia", "MediaLink", cascadeDelete: true);
            AddForeignKey("dbo.Movie", "Director_Id", "dbo.Director", "Id");
        }
    }
}
