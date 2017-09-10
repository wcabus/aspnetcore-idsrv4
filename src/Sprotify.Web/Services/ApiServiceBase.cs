using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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
            if (!response.IsSuccessStatusCode)
            {
                var errData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception(errData);
            }

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }

        protected async Task<T> Post<T>(string resource, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(_baseUri + resource, content).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                var errData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception(errData);
            }

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
