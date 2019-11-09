namespace MicroUrl.Stats
{
    using System.Threading.Tasks;

    public interface IStatsService
    {
        Task<MicroUrlStats> GetStatsAsync(string key);
    }
}