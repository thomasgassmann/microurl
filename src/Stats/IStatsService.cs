namespace MicroUrl.Stats
{
    using System.Threading.Tasks;

    public interface IStatsService
    {
        Task<MicroUrlStats> GetStats(string key);
    }
}