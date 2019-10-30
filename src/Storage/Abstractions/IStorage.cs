namespace MicroUrl.Storage.Abstractions
{
    using System.Threading.Tasks;

    public interface IStorage<T> where T : class, IEntity, new()
    {
        Task SaveAsync(T entity);

        Task<T> LoadAsync();
    }
}