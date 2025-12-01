using DigitalHomeLibrary.TrackingBooks.Domain.Entities;

namespace DigitalHomeLibrary.TrackingBooks.Repositories.Abstract
{
    public interface IBooksRepository : IAsyncRepository<Book>
    {
    }
}
