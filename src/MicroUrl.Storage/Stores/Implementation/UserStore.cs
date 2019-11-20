namespace MicroUrl.Storage.Stores.Implementation
{
    using AutoMapper;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Entities;
    using System.Threading.Tasks;

    public class UserStore : IUserStore
    {
        private readonly IStorageFactory _storageFactory;
        private readonly IKeyFactory _keyFactory;
        private readonly IMapper _mapper;

        public UserStore(IStorageFactory storageFactory, IKeyFactory keyFactory, IMapper mapper)
        {
            _storageFactory = storageFactory;
            _keyFactory = keyFactory;
            _mapper = mapper;
        }

        public Task<bool> ExistsAsync(string username)
        {
            var storage = _storageFactory.CreateStorage<UserEntity>();
            return storage.ExistsAsync(_keyFactory.CreateFromString(username));
        }

        public async Task CreateAsync(User user)
        {
            var storage = _storageFactory.CreateStorage<UserEntity>();
            var entity = _mapper.Map<UserEntity>(user);
            _ = await storage.CreateAsync(entity);
        }

        public async Task<User> LoadAsync(string userName)
        {
            var storage = _storageFactory.CreateStorage<UserEntity>();
            var loaded = await storage.LoadAsync(_keyFactory.CreateFromString(userName));
            return _mapper.Map<User>(loaded);
        }
    }
}
