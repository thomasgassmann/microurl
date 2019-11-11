namespace MicroUrl.Common
{
    public interface IConfigurationStore
    {
        MicroUrlSettings GetMicroUrlSettings();

        StorageSettings GetStorageSettings();
    }
}