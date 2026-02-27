using DigitalHomeLibrary.TrackingBooks.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Models.Configurations
{
    public class TagsConfiguration : IEntityTypeConfiguration<TagEntity>
    {
        public void Configure(EntityTypeBuilder<TagEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasMany(t => t.TaggedBooks)
                .WithMany(b => b.Tags);
        }
    }
}
