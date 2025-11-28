using DigitalHomeLibrary.TrackingBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.Domain.Models.Configurations
{
    public class StatusesConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasKey(s => s.Id);
        }
    }
}
