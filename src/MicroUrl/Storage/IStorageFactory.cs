namespace MicroUrl.Storage
{
    using Google.Cloud.Datastore.V1;

    public interface IStorageFactory
    {
        DatastoreDb GetStorage();
    }
}