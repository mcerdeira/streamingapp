using streamingapp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace streamingapp.DAL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
        { }

        public DbSet<Actor> Actor { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Director> Director { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<RelatedMedia> RelatedMedia { get; set; }
        public DbSet<Serie> Serie { get; set; }
        public DbSet<MovieActors> MovieActors { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }        
    }
}