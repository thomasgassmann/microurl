namespace MicroUrl.Storage.Dto
{
    public class MicroText : RedirectableEntity
    {
        public string Language { get; set; }

        public string Text { get; set; }
    }
}