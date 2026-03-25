using DigitalHomeLibrary.ContentService.Domain.Entities;
using DigitalHomeLibrary.ContentService.Domain.Repositories;
using DigitalHomeLibrary.ContentService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DigitalHomeLibrary.ContentService.Infrastructure.Repositories
{
    public class EFCoreBookContentDataRepository(ContentServiceDbContext contentServiceDbContext) : IBookContentDataRepository
    {
        readonly ContentServiceDbContext _contentServiceDbContext = contentServiceDbContext;

        public async Task AddBookContentData(BookContentData bookContentData)
        {
            await _contentServiceDbContext.BooksContent.AddAsync(bookContentData);
            await _contentServiceDbContext.SaveChangesAsync();
        }

        public async Task DeleteBookContent(Guid bookId)
        {
            await _contentServiceDbContext.BooksContent.Where(e => e.BookId == bookId).ExecuteDeleteAsync();
            await _contentServiceDbContext.SaveChangesAsync();
        }

        public async Task<BookContentData> GetBookContentData(Guid bookId)
        {
            return await _contentServiceDbContext.BooksContent.FirstAsync(e => e.BookId == bookId);
        }
    }
}
