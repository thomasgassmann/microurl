namespace MicroUrl.Visit
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Urls;

    public interface IVisitorTracker
    {
        Task SaveVisitAsync(MicroUrlEntity entity, HttpContext context);
    }
}