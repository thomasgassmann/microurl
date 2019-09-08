namespace MicroUrl.Urls.Implementation
{
    using System.Threading.Tasks;

    public class KeyGenerator : IKeyGenerator
    {
        public KeyGenerator()
        {
            
        }
        
        public Task<string> GetKeyAsync(string customKey = null)
        {
            throw new System.NotImplementedException();
        }
    }
}