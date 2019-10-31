namespace MicroUrl.Storage.Abstractions
{
    using System.Threading.Tasks;

    public interface IStorage<T> where T : class, new()
    {
        Task<IKey> SaveAsync(T entity);

        Task<T> LoadAsync(IKey key);
    }
}