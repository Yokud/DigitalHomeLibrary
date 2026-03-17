using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class BooksConfiguration : IEntityTypeConfiguration<EFBook>
    {
        public void Configure(EntityTypeBuilder<EFBook> builder)
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
        }
    }
}
