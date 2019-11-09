namespace MicroUrl.Storage.Entities
{
    using MicroUrl.Storage.Abstractions;

    [EntityName("microurl")]
    public class MicroTextEntity : RedirectableEntity
    {
        public string Language { get; set; }

        [ExcludeFromIndexes]
        public string Text { get; set; }
    }
}