namespace MicroUrl.Storage
{
    using MicroUrl.Storage.Entities;
    using System;
    using System.Collections.Generic;

    public interface IVisitStorageService : IEntityStorageService<VisitEntity, long>
    {
        IAsyncEnumerable<VisitEntity> GetVisitorCountAsync(string key, DateTime from, DateTime to);
    }
}