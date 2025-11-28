namespace DigitalHomeLibrary.TrackingBooks.Repositories.Abstract
{
    internal interface IAsyncRepository<T>
    {
        Task<T?> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<Guid> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task SaveAsync();
    }
}
