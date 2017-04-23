using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sprotify.Web.Services
{
    public abstract class ApiServiceBase
    {
        private readonly HttpClient _client;
        private readonly string _baseUri;

        protected ApiServiceBase(HttpClient client, string baseUri)
        {
            _client = client;
            _baseUri = baseUri;
        }

        protected async Task<T> Get<T>(string resource)
        {
            var response = await _client.GetAsync(_baseUri + resource).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
