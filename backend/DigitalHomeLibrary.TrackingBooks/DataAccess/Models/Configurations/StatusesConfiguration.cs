using DigitalHomeLibrary.TrackingBooks.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.TrackingBooks.DataAccess.Models.Configurations
{
    public class StatusesConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder.HasKey(s => s.Id);
        }
    }
}
