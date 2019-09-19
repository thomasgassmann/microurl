namespace MicroUrl.Storage
{
    using MicroUrl.Storage.Entities;

    public interface ITextStorageService : IEntityStorageService<MicroUrlTextEntity, string>
    {
    }
}