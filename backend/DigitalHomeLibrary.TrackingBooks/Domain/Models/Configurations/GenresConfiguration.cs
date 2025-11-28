using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Models.Configurations
{
    public class GenresConfigurator : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);
        }
    }
}
