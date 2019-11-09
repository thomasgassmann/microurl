namespace MicroUrl.Storage.Stores.Implementation
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;

    public class RedirectableStore : IRedirectableStore
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IKeyFactory _keyFactory;
        private readonly IMapper _mapper;

        public RedirectableStore(
            IStorageFactory storageFactory,
            IKeyFactory keyFactory,
            IMapper mapper)
        {
            _storageFactory = storageFactory;
            _keyFactory = keyFactory;
            _mapper = mapper;
        }
        
        public async Task<Redirectable> LoadAsync(string key)
        {
            var storage = _storageFactory.CreateStorage<MicroUrlEntity>();
            var loaded = await storage.LoadAsync(_keyFactory.CreateFromString(key));
            switch (loaded.Type)
            {
                case MicroUrlEntity.TextType:
                    return _mapper.Map<MicroUrl>(loaded);
                case MicroUrlEntity.UrlType:
                    return _mapper.Map<MicroText>(loaded);
                default:
                    throw new ArgumentException("Invalid entity type");
            }
        }
    }
}