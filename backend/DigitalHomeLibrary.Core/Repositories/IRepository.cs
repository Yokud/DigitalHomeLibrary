namespace DigitalHomeLibrary.Core.Repositories
{
    internal interface IRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void Save();
    }
}
