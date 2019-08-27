namespace MicroUrl.Visit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVisitorStorageService
    {
        Task SaveAsync(UrlVisitEntity entity);

//        IAsyncEnumerable<UrlVisitEntity> GetVisitorCountAsync(string key, DateTime from, DateTime to);
    }
}