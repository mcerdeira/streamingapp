namespace streamingapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeset2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actor", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.Actor", new[] { "Movie_Id" });
            CreateTable(
                "dbo.MovieActor",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("dbo.Movie", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actor", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            DropColumn("dbo.Actor", "Movie_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actor", "Movie_Id", c => c.Int());
            DropForeignKey("dbo.MovieActor", "Actor_Id", "dbo.Actor");
            DropForeignKey("dbo.MovieActor", "Movie_Id", "dbo.Movie");
            DropIndex("dbo.MovieActor", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActor", new[] { "Movie_Id" });
            DropTable("dbo.MovieActor");
            CreateIndex("dbo.Actor", "Movie_Id");
            AddForeignKey("dbo.Actor", "Movie_Id", "dbo.Movie", "Id");
        }
    }
}
