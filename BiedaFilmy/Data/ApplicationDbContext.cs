using BiedaFilmy.Models;
using BiedaFilmy.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BiedaFilmy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Employment> Employments { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<MovieComment> MovieComments { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>().Ignore(m => m.Score);

            builder.Entity<MovieComment>()
                .HasOne<IdentityUser>(c => c.User);
            builder.Entity<Collection>()
                .HasOne<IdentityUser>(c => c.User);


            builder.Entity<Employment>().HasKey(e => new { e.MovieId, e.PersonId });

            builder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Dramat"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Thriller"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Fantasy"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Fantastyka naukowa"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Komedia"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Historyczny"
                }
            );
        }

        
    }
}