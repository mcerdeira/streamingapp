namespace streamingapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelbigchange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActorMovies", "ActorId", "dbo.Actor");
            DropForeignKey("dbo.ActorMovies", "MovieId", "dbo.Movie");
            DropIndex("dbo.ActorMovies", new[] { "ActorId" });
            DropIndex("dbo.ActorMovies", new[] { "MovieId" });
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        MovieActorsId = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieActorsId)
                .ForeignKey("dbo.Actor", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movie", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
            DropTable("dbo.ActorMovies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ActorMovies",
                c => new
                    {
                        ActorId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActorId, t.MovieId });
            
            DropForeignKey("dbo.MovieActors", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actor");
            DropIndex("dbo.MovieActors", new[] { "ActorId" });
            DropIndex("dbo.MovieActors", new[] { "MovieId" });
            DropTable("dbo.MovieActors");
            CreateIndex("dbo.ActorMovies", "MovieId");
            CreateIndex("dbo.ActorMovies", "ActorId");
            AddForeignKey("dbo.ActorMovies", "MovieId", "dbo.Movie", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActorMovies", "ActorId", "dbo.Actor", "Id", cascadeDelete: true);
        }
    }
}
