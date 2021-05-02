using System;
using Microsoft.EntityFrameworkCore;

namespace Movies.Models.Data
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>().HasKey(m => m.ID).IsClustered(false);
            builder.Entity<Movie>().HasIndex(m => m.ClustedID).IsClustered(true);

            builder.Entity<Actor>().HasKey(m => m.ID).IsClustered(false);
            builder.Entity<Actor>().HasIndex(m => m.ClustedID).IsClustered(true);
        }
    }
}
