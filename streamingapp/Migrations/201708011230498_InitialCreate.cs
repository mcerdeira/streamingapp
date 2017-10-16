namespace streamingapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        Nationality_Id = c.Int(),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.Nationality_Id)
                .ForeignKey("dbo.Movie", t => t.Movie_Id)
                .Index(t => t.Nationality_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Flaglink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Director",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        BirthDay = c.DateTime(nullable: false),
                        Nationality_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Country", t => t.Nationality_Id)
                .Index(t => t.Nationality_Id);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Producer = c.String(),
                        Rank = c.Int(nullable: false),
                        Director_Id = c.Int(),
                        Media_MediaLink = c.String(nullable: false, maxLength: 128),
                        Serie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Director", t => t.Director_Id)
                .ForeignKey("dbo.RelatedMedia", t => t.Media_MediaLink, cascadeDelete: true)
                .ForeignKey("dbo.Serie", t => t.Serie_Id)
                .Index(t => t.Director_Id)
                .Index(t => t.Media_MediaLink)
                .Index(t => t.Serie_Id);
            
            CreateTable(
                "dbo.RelatedMedia",
                c => new
                    {
                        MediaLink = c.String(nullable: false, maxLength: 128),
                        Format = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MediaLink);
            
            CreateTable(
                "dbo.Serie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie", "Serie_Id", "dbo.Serie");
            DropForeignKey("dbo.Movie", "Media_MediaLink", "dbo.RelatedMedia");
            DropForeignKey("dbo.Movie", "Director_Id", "dbo.Director");
            DropForeignKey("dbo.Actor", "Movie_Id", "dbo.Movie");
            DropForeignKey("dbo.Director", "Nationality_Id", "dbo.Country");
            DropForeignKey("dbo.Actor", "Nationality_Id", "dbo.Country");
            DropIndex("dbo.Movie", new[] { "Serie_Id" });
            DropIndex("dbo.Movie", new[] { "Media_MediaLink" });
            DropIndex("dbo.Movie", new[] { "Director_Id" });
            DropIndex("dbo.Director", new[] { "Nationality_Id" });
            DropIndex("dbo.Actor", new[] { "Movie_Id" });
            DropIndex("dbo.Actor", new[] { "Nationality_Id" });
            DropTable("dbo.Serie");
            DropTable("dbo.RelatedMedia");
            DropTable("dbo.Movie");
            DropTable("dbo.Director");
            DropTable("dbo.Country");
            DropTable("dbo.Actor");
        }
    }
}
