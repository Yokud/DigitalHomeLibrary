using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using DigitalHomeLibrary.TrackingBooks.Domain.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Models
{
    public class TrackingBooksDbContext : DbContext
    {
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Status> BookStatuses { get; set; }
        DbSet<Tag> Tags { get; set; }

        public TrackingBooksDbContext(DbContextOptions<TrackingBooksDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
        }
    }
}
