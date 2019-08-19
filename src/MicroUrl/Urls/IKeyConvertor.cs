namespace MicroUrl.Urls
{
    public interface IKeyConvertor
    {
        string GetKey(long id);

        long GetId(string key);
    }
}