namespace DigitalHomeLibrary.TrackingBooks.Repositories
{
    internal interface IRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        Guid Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void Save();
    }
}
