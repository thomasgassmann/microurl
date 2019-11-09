namespace MicroUrl.Storage.Abstractions.CloudDatastore
{
    using Google.Cloud.Datastore.V1;

    public class CloudDatastoreEntitySerializer<T>
    {
        public CloudDatastoreEntitySerializer()
        {
            
        }

        public void Serialize(T source, Entity entity)
        {
            
        }

        public void Deserialize(Entity entity, T destination)
        {
            
        }
    }
}