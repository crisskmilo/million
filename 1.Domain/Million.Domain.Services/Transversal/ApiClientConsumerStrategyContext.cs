using Million.Domain.Interfaces.Transversal;
using System.Net.Http;
using System.Threading.Tasks;

namespace Million.Domain.Services.Transversal
{
    public class ApiClientConsumerStrategyContext : IApiClientConsumerStrategyContext
    {
        private IApiClientConsumerStrategy _strategy;

        public ApiClientConsumerStrategyContext(IApiClientConsumerStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IApiClientConsumerStrategy strategy)
        {
            _strategy = strategy;
        }

        public Task<HttpResponseMessage> GetAsync(string url, string token) =>
            _strategy.GetAsync(url, token);

        public Task<HttpResponseMessage> PostAsync(string url, string token, string jsonContent) =>
            _strategy.PostAsync(url, token, jsonContent);

        public Task<HttpResponseMessage> PutAsync(string url, string token, string jsonContent) =>
            _strategy.PutAsync(url, token, jsonContent);

        public Task<HttpResponseMessage> DeleteAsync(string url, string token) =>
            _strategy.DeleteAsync(url, token);

        public Task<HttpResponseMessage> PatchAsync(string url, string token, string jsonContent) =>
            _strategy.PatchAsync(url, token, jsonContent);
    }
}