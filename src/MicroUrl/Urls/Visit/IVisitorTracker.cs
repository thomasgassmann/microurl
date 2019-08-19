namespace MicroUrl.Urls.Visit
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IVisitorTracker
    {
        Task SaveVisitAsync(MicroUrlEntity entity, HttpContext context);
    }
}