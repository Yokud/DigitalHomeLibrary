using DigitalHomeLibrary.ContentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.ContentService.Infrastructure.DataAccess
{
    public class ContentServiceDbContext(DbContextOptions<ContentServiceDbContext> options) : DbContext(options)
    {
        public DbSet<BookContentData> BooksContent { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookContentDataConfiguration());
        }
    }
}
