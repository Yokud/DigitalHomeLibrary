using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Models.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books);

            builder
                .HasOne(b => b.Status)
                .WithOne(s => s.Book)
                .HasForeignKey<Book>(b => b.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(b => b.Reviews)
                .WithOne(r => r.ReviewedBook)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
