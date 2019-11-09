namespace MicroUrl.Storage.Entities
{
    using MicroUrl.Storage.Abstractions;

    [EntityName("microurl")]
    public class MicroUrlEntity : RedirectableEntity
    {
        public string Url { get; set; }
    }
}