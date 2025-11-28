using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Models.Configurations
{
    public class AuthorsConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(a => a.Country)
                .WithMany(c => c.Authors)
                .HasForeignKey(a => a.CountryId);
        }
    }
}
