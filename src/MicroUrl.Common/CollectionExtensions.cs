namespace MicroUrl.Common
{
    using Microsoft.Extensions.DependencyInjection;
    using MicroUrl.Common.Implementation;

    public static class CollectionExtensions
    {
        public static void AddCommon(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IEnvConfigurationStore, EnvConfigurationStore>();
        }
    }
}