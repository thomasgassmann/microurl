namespace MicroUrl.Visit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVisitorStorageService
    {
        Task SaveAsync(UrlVisitEntity entity);

        Task<IEnumerable<UrlVisitEntity>> GetVisitorCountAsync(string key, DateTime from, DateTime to);
    }
}