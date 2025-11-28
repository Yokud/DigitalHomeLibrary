using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Models.Configurations
{
    public class LanguagesConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(l => l.Id);
        }
    }
}
