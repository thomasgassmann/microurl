namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using System;
    using System.Collections.Generic;
    using MicroUrl.Common;
    using MicroUrl.Storage.Abstractions.Shared;

    public class CloudDatastoreStorageFactory : IStorageFactory
    {
        private readonly IConfigurationStore _configurationStore;
        private readonly IEntityAnalyzer _entityAnalyzer;
        private readonly IKeyFactory _keyFactory;

        private readonly IDictionary<Type, object> _storageMap = new Dictionary<Type, object>();
        
        public CloudDatastoreStorageFactory(
            IConfigurationStore configurationStore,
            IEntityAnalyzer entityAnalyzer,
            IKeyFactory keyFactory)
        {
            _configurationStore = configurationStore;
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
                _configurationStore,
                _entityAnalyzer,
                _keyFactory);
            _storageMap.Add(typeof(T), instance);
            
            return instance;
        }
    }
}