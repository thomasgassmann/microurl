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

        public async Task<bool> ExistsAsync(string key)
        {
            var storage = _storageFactory.CreateStorage<MicroUrlEntity>();
            var item = await storage.LoadAsync(_keyFactory.CreateFromString(key));
            return item != null;
        }
        
        public async Task<Redirectable> LoadAsync(string key)
        {
            var storage = _storageFactory.CreateStorage<MicroUrlEntity>();
            var loaded = await storage.LoadAsync(_keyFactory.CreateFromString(key));
            if (loaded == null)
            {
                return null;
            }
            
            switch (loaded.Type)
            {
                case MicroUrlEntity.TextType:
                    return _mapper.Map<MicroText>(loaded);
                case MicroUrlEntity.UrlType:
                    return _mapper.Map<MicroUrl>(loaded);
                default:
                    throw new ArgumentException("Invalid entity type");
            }
        }

        public async Task<string> CreateAsync(Redirectable redirectable)
        {
            var storage = _storageFactory.CreateStorage<MicroUrlEntity>();
            var entityToSave = _mapper.Map<MicroUrlEntity>(redirectable);
            var type = redirectable.GetType();
            if (type == typeof(MicroText))
            {
                entityToSave.Type = MicroUrlEntity.TextType;
            }
            else if (type == typeof(MicroUrl))
            {
                entityToSave.Type = MicroUrlEntity.UrlType;
            }
            else
            {
                throw new ArgumentException(type.FullName);
            }
            
            entityToSave.Created = DateTime.Now;
            var key = await storage.CreateAsync(entityToSave);
            return key.StringValue;
        }
    }
}