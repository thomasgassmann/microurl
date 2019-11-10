namespace MicroUrl.Web.Redirects.Implementation
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using MicroUrl.Storage.Dto;
    using MicroUrl.Storage.Stores;
    using MicroUrl.Web.Visit;

    public class RedirectService : IRedirectService
    {
        private readonly IRedirectableStore _redirectableStore;
        private readonly IVisitorTracker _visitorTracker;
        private readonly IClientUrlService _clientUrlService;
        
        public RedirectService(
            IRedirectableStore redirectableStore,
            IVisitorTracker visitorTracker,
            IClientUrlService clientUrlService)
        {
            _redirectableStore = redirectableStore;
            _visitorTracker = visitorTracker;
            _clientUrlService = clientUrlService;
        }
        
        public async Task<string> GetRedirectUrlAndTrackAsync(string key, HttpContext context)
        {
            var redirectable = await _redirectableStore.LoadAsync(key);
            if (redirectable == null)
            {
                return null;
            }

            await _visitorTracker.SaveVisitAsync(key, context);           
            return !redirectable.Enabled ? null : GetRedirectUrl(redirectable);
        }

        private string GetRedirectUrl(Redirectable redirectable)
        {
            if (redirectable is MicroUrl url)
            {
                return url.Url;
            }

            if (redirectable is MicroText text)
            {
                return _clientUrlService.ConstructEditorUrl(text.Key);
            }
            
            throw new ArgumentException(redirectable.GetType().FullName);
        }
    }
}