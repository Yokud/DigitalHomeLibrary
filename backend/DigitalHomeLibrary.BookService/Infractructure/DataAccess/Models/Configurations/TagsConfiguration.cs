using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class TagsConfiguration : IEntityTypeConfiguration<EFTag>
    {
        public void Configure(EntityTypeBuilder<EFTag> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasMany(t => t.TaggedBooks)
                .WithMany(b => b.Tags);
        }
    }
}
