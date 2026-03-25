using DigitalHomeLibrary.ContentService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.ContentService.Infrastructure.DataAccess
{
    public class BookContentDataConfiguration : IEntityTypeConfiguration<BookContentData>
    {
        public void Configure(EntityTypeBuilder<BookContentData> builder)
        {
            builder.HasKey(e => e.BookId);

            builder.Property(e => e.ContentUri).IsRequired();
        }
    }
}
