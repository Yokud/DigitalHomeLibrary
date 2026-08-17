using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<BookDbo>
    {
        public void Configure(EntityTypeBuilder<BookDbo> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books);

            builder
                .HasMany(b => b.Reviews)
                .WithOne(r => r.ReviewedBook)
                .HasForeignKey(r => r.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(b => b.Status);
        }
    }
}
