namespace MicroUrl.Common.Implementation
{
    using Microsoft.Extensions.Configuration;

    public class EnvConfigurationStore : IEnvConfigurationStore
    {
        private const string AnalyticsIdKey = "ANALYTICS_ID";
        private const string GcpProjectKey = "GCP_PROJECT";

        private readonly IConfiguration _configuration;
        
        public EnvConfigurationStore(IConfiguration configuration) =>
            _configuration = configuration;
        
        public MicroUrlSettings GetMicroUrlSettings() => new MicroUrlSettings
        {
            AnalyticsId = _configuration.GetValue<string>(AnalyticsIdKey)
        };

        public StorageSettings GetStorageSettings() => new StorageSettings
        {
            Project = _configuration.GetValue<string>(GcpProjectKey)
        };
    }
}