namespace MicroUrl.Storage.Stores
{
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IVisitStore
    {
        Task<long> CreateAsync(Visit visit);
    }
}