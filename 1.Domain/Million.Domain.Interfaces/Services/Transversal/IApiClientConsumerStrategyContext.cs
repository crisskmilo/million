using System.Net.Http;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Transversal
{
    public interface IApiClientConsumerStrategyContext
    {
        Task<HttpResponseMessage> GetAsync(string url, string token);
        Task<HttpResponseMessage> PostAsync(string url, string token, string jsonContent);
        Task<HttpResponseMessage> PutAsync(string url, string token, string jsonContent);
        Task<HttpResponseMessage> DeleteAsync(string url, string token);
        Task<HttpResponseMessage> PatchAsync(string url, string token, string jsonContent);
        void SetStrategy(IApiClientConsumerStrategy strategy);
    }
}