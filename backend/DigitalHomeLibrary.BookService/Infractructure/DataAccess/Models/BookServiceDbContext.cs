using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models
{
    public class BookServiceDbContext(DbContextOptions<BookServiceDbContext> options) : DbContext(options)
    {
        public DbSet<EFBook> Books { get; set; }
        public DbSet<EFAuthor> Authors { get; set; }
        public DbSet<EFReview> Reviews { get; set; }
        public DbSet<EFTag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorsConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new TagsConfiguration());
        }
    }
}
