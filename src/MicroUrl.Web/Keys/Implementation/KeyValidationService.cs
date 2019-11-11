namespace MicroUrl.Web.Keys.Implementation
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.Extensions.Options;
    using MicroUrl.Web.Configuration;

    public class KeyValidationService : IKeyValidationService
    {
        private readonly IOptions<UrlConfig> _options;
        
        public KeyValidationService(IOptions<UrlConfig> options)
        {
            _options = options;
        }
        
        public bool IsKeyValid(string key)
        {
            return !_options.Value.DisallowedKeys.Any(x =>
                Regex.Match(key, x, RegexOptions.IgnoreCase).Success);
        }
    }
}