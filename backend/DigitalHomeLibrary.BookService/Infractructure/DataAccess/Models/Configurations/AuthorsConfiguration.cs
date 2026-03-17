using DigitalHomeLibrary.BookService.Infractructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalHomeLibrary.BookService.Infractructure.DataAccess.Models.Configurations
{
    public class AuthorsConfiguration : IEntityTypeConfiguration<EFAuthor>
    {
        public void Configure(EntityTypeBuilder<EFAuthor> builder)
        {
            builder.HasKey(a => a.Id);
        }
    }
}
