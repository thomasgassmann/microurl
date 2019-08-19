namespace MicroUrl.Urls.Implementation
{
    using System;
    using System.Threading.Tasks;
    using Google.Protobuf.WellKnownTypes;

    public class UrlService : IUrlService
    {
        private readonly IUrlStorageService _storageService;

        public UrlService(IUrlStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string> SaveAsync(string url)
        {
            await _storageService.Save(new MicroUrlEntity
            {
                Created = Timestamp.FromDateTime(DateTime.Now),
                Enabled = true,
                Key = "",
                Url = url
            });
            return "";
        }
    }
}