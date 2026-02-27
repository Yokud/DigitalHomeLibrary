using DigitalHomeLibrary.TrackingBooks.DataAccess.Entities;
using DigitalHomeLibrary.TrackingBooks.DataAccess.Models.Configurations;
using DigitalHomeLibrary.TrackingBooks.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Models
{
    public class TrackingBooksDbContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<StatusEntity> BookStatuses { get; set; }
        public DbSet<TagEntity> Tags { get; set; }

        public TrackingBooksDbContext(DbContextOptions<TrackingBooksDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorsConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new TagsConfiguration());
            modelBuilder.ApplyConfiguration(new StatusesConfiguration());
        }
    }
}
