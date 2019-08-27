namespace MicroUrl.Visit
{
    using System;
    using System.Threading.Tasks;

    public interface IVisitorStorageService
    {
        Task SaveAsync(UrlVisitEntity entity);

        Task<VisitorNumbers> GetVisitorCount(string key, DateTime from, DateTime to);
    }
}