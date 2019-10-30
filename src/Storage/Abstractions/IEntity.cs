namespace MicroUrl.Storage.Abstractions
{
    public interface IEntity
    {
        IKey DeriveKey();
    }
}