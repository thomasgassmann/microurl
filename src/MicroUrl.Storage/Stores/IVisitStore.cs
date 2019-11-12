namespace MicroUrl.Storage.Stores
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MicroUrl.Storage.Dto;

    public interface IVisitStore
    {
        Task<long> CreateAsync(Visit visit);

        IAsyncEnumerable<Visit> GetVisitsOfRedirectableBetween(string redirectableKey, DateTime from, DateTime to);
    }
}