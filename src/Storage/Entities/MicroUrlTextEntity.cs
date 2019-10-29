namespace MicroUrl.Storage.Entities
{
    public class MicroUrlTextEntity : RedirectableEntity
    {
        public string Language { get; set; }

        public string Text { get; set; }
    }
}