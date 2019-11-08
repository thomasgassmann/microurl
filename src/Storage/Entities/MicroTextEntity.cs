namespace MicroUrl.Storage.Entities
{
    public class MicroTextEntity : RedirectableEntity
    {
        public string Language { get; set; }

        public string Text { get; set; }
    }
}