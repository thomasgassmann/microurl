namespace MicroUrl.Web.Stats
{
    using System.Threading.Tasks;

    public interface IStatsService
    {
        Task<MicroUrlStats> GetStatsAsync(string key);
    }
}