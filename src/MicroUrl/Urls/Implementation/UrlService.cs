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
            var key = await _storageService.SaveAsync(new MicroUrlEntity
            {
                Created = Timestamp.FromDateTime(DateTime.UtcNow),
                Enabled = true,
                Url = url,
                Id = -1
            });
            return "empty";
        }
    }
}