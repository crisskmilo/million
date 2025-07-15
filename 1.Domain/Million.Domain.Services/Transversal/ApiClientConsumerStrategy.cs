using Million.Domain.Interfaces.Transversal;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Services.Transversal
{
    public class ApiClientConsumerStrategy : IApiClientConsumerStrategy
    {
        public async Task<HttpResponseMessage> GetAsync(string url, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, string token, string jsonContent)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return await client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> PutAsync(string url, string token, string jsonContent)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return await client.PutAsync(url, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await client.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> PatchAsync(string url, string token, string jsonContent)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = content };
            return await client.SendAsync(request);
        }
    }
}