namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IMicroTextStore
    {
        Task<string> CreateAsync(MicroText microText);
        
        Task<MicroText> LoadAsync(string key);
    }
}