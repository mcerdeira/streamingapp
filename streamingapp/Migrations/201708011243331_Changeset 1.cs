namespace streamingapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeset1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actor", "Nationality_Id", "dbo.Country");
            DropIndex("dbo.Actor", new[] { "Nationality_Id" });
            RenameColumn(table: "dbo.Actor", name: "Nationality_Id", newName: "CountryID");
            AlterColumn("dbo.Actor", "CountryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Actor", "CountryID");
            AddForeignKey("dbo.Actor", "CountryID", "dbo.Country", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actor", "CountryID", "dbo.Country");
            DropIndex("dbo.Actor", new[] { "CountryID" });
            AlterColumn("dbo.Actor", "CountryID", c => c.Int());
            RenameColumn(table: "dbo.Actor", name: "CountryID", newName: "Nationality_Id");
            CreateIndex("dbo.Actor", "Nationality_Id");
            AddForeignKey("dbo.Actor", "Nationality_Id", "dbo.Country", "Id");
        }
    }
}
