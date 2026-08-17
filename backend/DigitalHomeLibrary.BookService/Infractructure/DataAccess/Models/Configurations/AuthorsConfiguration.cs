using DigitalHomeLibrary.BookService.Infractructure.DataAccess.DBO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class AuthorsConfiguration : IEntityTypeConfiguration<AuthorDbo>
    {
        public void Configure(EntityTypeBuilder<AuthorDbo> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}
