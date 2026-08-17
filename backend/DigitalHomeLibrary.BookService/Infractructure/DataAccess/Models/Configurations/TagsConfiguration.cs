using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class TagsConfiguration : IEntityTypeConfiguration<TagDbo>
    {
        public void Configure(EntityTypeBuilder<TagDbo> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasMany(t => t.TaggedBooks)
                .WithMany(b => b.Tags);
        }
    }
}
