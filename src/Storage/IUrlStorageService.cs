namespace MicroUrl.Storage
{
    using MicroUrl.Storage.Entities;

    public interface IUrlStorageService : IEntityStorageService<MicroUrlEntity, string>
    {
    }
}