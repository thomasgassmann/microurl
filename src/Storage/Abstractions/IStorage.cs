namespace MicroUrl.Storage.Abstractions
{
    using System.Threading.Tasks;

    public interface IStorage
    {
        Task SaveAsync(IEntity entity);

        Task<IEntity> LoadAsync();
    }
}