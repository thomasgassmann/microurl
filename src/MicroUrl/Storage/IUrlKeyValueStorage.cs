namespace MicroUrl.Storage
{
    using System;
    using System.Threading.Tasks;
    using Google.Cloud.Datastore.V1;

    public interface IUrlKeyValueStorage
    {
        Task SaveAsync<T>(T url);

        Task<T> LoadAsync<T>(string key);

        Task<bool> Exists(string key);
    }
}