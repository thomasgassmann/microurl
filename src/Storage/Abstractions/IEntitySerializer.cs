namespace MicroUrl.Storage.Abstractions
{
    public interface IEntitySerializer<in TSource, in TTarget>
    {
        void Serialize(TSource source, TTarget destination);
        
        void Deserialize(TTarget source, TSource destination);
    }
}