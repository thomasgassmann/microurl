namespace MicroUrl.Storage
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MicroUrl.Storage.Abstractions;
    using MicroUrl.Storage.Abstractions.CloudDatastore;
    using MicroUrl.Storage.Abstractions.Implementation;
    using MicroUrl.Storage.Abstractions.Shared;
    using MicroUrl.Storage.Abstractions.Shared.Implementation;
    using MicroUrl.Storage.Configuration;

    public static class CollectionExtensions
    {
        public static void AddStorage(this IServiceCollection services, IConfiguration configuration)
        {
            const string SettingsKey = "Storage";
            services.Configure<MicroUrlStorageConfiguration>(configuration.GetSection(SettingsKey).Bind);
        
            services.AddSingleton<IEntityAnalyzer, EntityAnalyzer>();
            services.AddSingleton<IKeyFactory, DefaultKeyFactory>();
            services.AddScoped<IStorageFactory, CloudDatastoreStorageFactory>();
        }
    }
}