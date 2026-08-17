using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class ReviewsConfiguration : IEntityTypeConfiguration<ReviewDbo>
    {
        public void Configure(EntityTypeBuilder<ReviewDbo> builder)
        {
            builder.HasKey(r => r.Id);
        }
    }
}
