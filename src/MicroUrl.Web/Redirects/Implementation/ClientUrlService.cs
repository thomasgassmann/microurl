namespace MicroUrl.Web.Redirects.Implementation
{
    public class ClientUrlService : IClientUrlService
    {
        private const string EditorTemplate = "/editor/{0}";

        public string ConstructEditorUrl(string key) =>
            string.Format(EditorTemplate, key);
    }
}