namespace MicroUrl.Storage.Stores.Implementation
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class VisitStore : IVisitStore
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IMapper _mapper;
        
        public VisitStore(IStorageFactory storageFactory, IMapper mapper)
        {
            _storageFactory = storageFactory;
            _mapper = mapper;
        }
        
        public async Task<long> CreateAsync(Visit visit)
        {
            var storage = _storageFactory.CreateStorage<VisitEntity>();
            var entity = _mapper.Map<VisitEntity>(visit);
            entity.Created = DateTime.Now;
            var key = await storage.CreateAsync(entity);
            return key.LongValue;
        }
    }
}