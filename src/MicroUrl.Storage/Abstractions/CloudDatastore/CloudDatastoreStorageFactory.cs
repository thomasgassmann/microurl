namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Options;
    using MicroUrl.Storage.Abstractions.Shared;
    using MicroUrl.Storage.Configuration;

    public class CloudDatastoreStorageFactory : IStorageFactory
    {
        private readonly IOptions<MicroUrlStorageConfiguration> _options;
        private readonly IEntityAnalyzer _entityAnalyzer;
        private readonly IKeyFactory _keyFactory;

        private readonly IDictionary<Type, object> _storageMap = new Dictionary<Type, object>();
        
        public CloudDatastoreStorageFactory(
            IOptions<MicroUrlStorageConfiguration> options,
            IEntityAnalyzer entityAnalyzer,
            IKeyFactory keyFactory)
        {
            _options = options;
            _entityAnalyzer = entityAnalyzer;
            _keyFactory = keyFactory;
        }
        
        public IStorage<T> CreateStorage<T>() where T : class, new()
        {
            if (_storageMap.TryGetValue(typeof(T), out var storage))
            {
                return (IStorage<T>)storage;
            }
            
            var instance = new CloudDatastoreStorage<T>(
                _options,
                _entityAnalyzer,
                _keyFactory);
            _storageMap.Add(typeof(T), instance);
            
            return instance;
        }
    }
}