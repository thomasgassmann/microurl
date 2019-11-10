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
        private readonly IMicroUrlKeyGenerator _microUrlKeyGenerator;
        private readonly IKeyFactory _keyFactory;
        private readonly IMapper _mapper;

        public RedirectableStore(
            IStorageFactory storageFactory,
            IKeyFactory keyFactory,
            IMapper mapper,
            IMicroUrlKeyGenerator microUrlKeyGenerator)
        {
            _storageFactory = storageFactory;
            _keyFactory = keyFactory;
            _mapper = mapper;
            _microUrlKeyGenerator = microUrlKeyGenerator;
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

        public async Task<string> SaveAsync(Redirectable redirectable)
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

            if (string.IsNullOrEmpty(redirectable.Key))
            {
                var generatedKey = await _microUrlKeyGenerator.GenerateKeyAsync();
                entityToSave.Key = generatedKey;
                var key = await storage.CreateAsync(entityToSave);
                return key.StringValue;
            }
            else
            {
                var key = await storage.UpdateAsync(entityToSave);
                return key.StringValue;
            }
        }
    }
}