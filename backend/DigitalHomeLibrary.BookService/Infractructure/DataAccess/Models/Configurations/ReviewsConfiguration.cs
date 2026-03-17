using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class ReviewsConfiguration : IEntityTypeConfiguration<EFReview>
    {
        public void Configure(EntityTypeBuilder<EFReview> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
