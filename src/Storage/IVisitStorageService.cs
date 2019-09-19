namespace MicroUrl.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Entities;
    using MicroUrl.Visit;

    public interface IVisitStorageService : IEntityStorageService<VisitEntity, long>
    {
        Task<IEnumerable<VisitEntity>> GetVisitorCountAsync(string key, DateTime from, DateTime to);
    }
}