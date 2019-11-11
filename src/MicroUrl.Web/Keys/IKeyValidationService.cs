namespace MicroUrl.Web.Keys
{
    public interface IKeyValidationService
    {
        bool IsKeyValid(string key);
    }
}