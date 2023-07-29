using Microsoft.EntityFrameworkCore;
using MusicApi.Models;

namespace MusicApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
                     new Song
                     {
                         Id = Guid.NewGuid(),
                         Title = "Willow",
                         Language = "English",
                         Duration = "4:36"
                     },
                     new Song
                     {
                         Id =  Guid.NewGuid(),
                         Title = "In The End",
                         Language = "English",
                         Duration = "3:20"
                     }
                );
        }
    }
}
