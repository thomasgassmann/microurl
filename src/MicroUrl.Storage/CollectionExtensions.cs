namespace MicroUrl.Storage
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Abstractions.CloudDatastore;
    using MicroUrl.Storage.Abstractions.Implementation;
    using MicroUrl.Storage.Abstractions.Shared;
    using MicroUrl.Storage.Abstractions.Shared.Implementation;
    using MicroUrl.Storage.Keys;
    using MicroUrl.Storage.Keys.Implementation;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Storage.Stores.Implementation;

    public static class CollectionExtensions
    {
        public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEntityAnalyzer, EntityAnalyzer>();
            services.AddSingleton<IKeyFactory, DefaultKeyFactory>();
            
            services.AddScoped<IStorageFactory, CloudDatastoreStorageFactory>();
            services.AddScoped<IMicroUrlKeyGenerator, MicroUrlKeyGenerator>();

            services.AddScoped<IRedirectableStore, RedirectableStore>();
            services.AddScoped<IMicroTextStore, MicroTextStore>();
            services.AddScoped<IVisitStore, VisitStore>();
            services.AddScoped<IMicroUrlStore, MicroUrlStore>();
        }
    }
}