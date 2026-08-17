using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models
{
    public class BookServiceDbContext(DbContextOptions<BookServiceDbContext> options) : DbContext(options)
    {
        public DbSet<BookDbo> Books { get; set; }
        public DbSet<AuthorDbo> Authors { get; set; }
        public DbSet<ReviewDbo> Reviews { get; set; }
        public DbSet<TagDbo> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorsConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewsConfiguration());
            modelBuilder.ApplyConfiguration(new TagsConfiguration());
        }
    }
}
