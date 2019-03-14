using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerCommunication
{
    public class HttpResourceRetriever : IResourceRetriever
    {
        private readonly HttpClient _httpClient;
        private readonly IDeserializer _deserializer;

        public HttpResourceRetriever(HttpClient httpClient, IDeserializer deserializer)
        {
            _httpClient = httpClient;
            _deserializer = deserializer;
        }

        public async Task<T> RetrieveResource<T>(Uri uri)
        {
            var distributionsResponse = await _httpClient.GetAsync(uri);
            try
            {
                distributionsResponse.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new UnavailableResourceException(uri.ToString(), e);
            }

            var distributionsContent = await distributionsResponse.Content.ReadAsStringAsync();
            return _deserializer.Deserialize<T>(distributionsContent);
        }
    }
}