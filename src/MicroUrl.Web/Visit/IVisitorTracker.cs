namespace MicroUrl.Visit
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IVisitorTracker
    {
        Task SaveVisitAsync(string key, HttpContext context);
    }
}