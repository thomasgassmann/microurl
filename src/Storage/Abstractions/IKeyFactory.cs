namespace MicroUrl.Storage.Abstractions
{
    public interface IKeyFactory
    {
        IKey CreateFromString(string key);

        IKey CreateNewAutoId();

        IKey CreateFromId(long id);
    }
}