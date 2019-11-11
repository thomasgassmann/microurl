namespace MicroUrl.Common
{
    public interface IEnvConfigurationStore
    {
        MicroUrlSettings GetMicroUrlSettings();

        StorageSettings GetStorageSettings();
    }
}