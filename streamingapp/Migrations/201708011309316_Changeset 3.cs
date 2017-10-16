namespace streamingapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changeset3 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieActor", newName: "ActorMovies");
            RenameColumn(table: "dbo.ActorMovies", name: "Movie_Id", newName: "MovieId");
            RenameColumn(table: "dbo.ActorMovies", name: "Actor_Id", newName: "ActorId");
            RenameIndex(table: "dbo.ActorMovies", name: "IX_Actor_Id", newName: "IX_ActorId");
            RenameIndex(table: "dbo.ActorMovies", name: "IX_Movie_Id", newName: "IX_MovieId");
            DropPrimaryKey("dbo.ActorMovies");
            AddPrimaryKey("dbo.ActorMovies", new[] { "ActorId", "MovieId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ActorMovies");
            AddPrimaryKey("dbo.ActorMovies", new[] { "Movie_Id", "Actor_Id" });
            RenameIndex(table: "dbo.ActorMovies", name: "IX_MovieId", newName: "IX_Movie_Id");
            RenameIndex(table: "dbo.ActorMovies", name: "IX_ActorId", newName: "IX_Actor_Id");
            RenameColumn(table: "dbo.ActorMovies", name: "ActorId", newName: "Actor_Id");
            RenameColumn(table: "dbo.ActorMovies", name: "MovieId", newName: "Movie_Id");
            RenameTable(name: "dbo.ActorMovies", newName: "MovieActor");
        }
    }
}
