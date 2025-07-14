using Microsoft.Extensions.Configuration;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Interfaces.Transversal;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Million.Domain.Services.Transversal;

namespace Million.Domain.Services.Operation
{
    public class PropertyImageService : BaseService<PropertyImage>, IPropertyImageService
    {
        private readonly IPropertyImageRepository propertyImageRepository;
        private readonly IRepository<PropertyImage> repository;
        private readonly IConfiguration configuration;
        private readonly IApiClientConsumerStrategyContext apiClientContext;

        public PropertyImageService(
            IPropertyImageRepository propertyImageRepository,
            IRepository<PropertyImage> repository,
            IConfiguration configuration,
            IApiClientConsumerStrategyContext apiClientContext)
            : base(propertyImageRepository)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.repository = repository;
            this.configuration = configuration;
            this.apiClientContext = apiClientContext;
        }

        public async Task<PropertyImage> AddPropertyImage(PropertyImageRequestDto propertyImage)
        {
            return await this.repository.AddAsync(new PropertyImage()
            {
                IdProperty = propertyImage.IdProperty,
                File = propertyImage.File,
                Enabled = true
            });
        }

        public async Task<int> DeletePropertyImage(int Id)
        {
            return await this.repository.DeleteAsync(Id);
        }

        public async Task<PropertyImage> GetPropertyImageById(int Id)
        {
            return await this.repository.GetByIdAsync(Id);
        }

        public async Task<ICollection<PropertyImage>> GetPropertyImages()
        {
            return await this.repository.GetAllAsync();
        }

        public async Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage)
        {
            return await this.repository.UpdateAsync(propertyImage);
        }

        public async Task<PropertyImage> UpdatePropertyImageFromPexels(PropertyImage propertyImage)
        {
            propertyImage.File = await GetHouseImagesFromPexelsAsync();
            return await this.repository.UpdateAsync(propertyImage);
        }

        /// <summary>
        /// Gets a house image from the Pexels API using the strategy pattern.
        /// </summary>
        /// <param name="query">Search term, e.g., "house" or "home".</param>
        /// <returns>image URLs as strings.</returns>
        public async Task<string> GetHouseImagesFromPexelsAsync(string query = "house")
        {
            // Get Pexels API key from configuration
            var apiKey = configuration["Pexels:ApiKey"];
            var pexelsUrl = configuration["Pexels:BaseUrl"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new System.Exception("Pexels API key is not configured.");

            var url = $"{pexelsUrl}/search?query={query}&per_page={1}";

            // Pexels uses the API key in the Authorization header
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", apiKey);

            // Use the strategy context to send the request
            var response = await apiClientContext.GetAsync(url, apiKey);

            if (!response.IsSuccessStatusCode)
                throw new System.Exception($"Pexels API request failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();

            // Pexels response: { photos: [ { src: { original: "...", ... } }, ... ] }
            using var doc = JsonDocument.Parse(json);
            var image = string.Empty;
            if (doc.RootElement.TryGetProperty("photos", out var photos))
            {
                foreach (var photo in photos.EnumerateArray())
                {
                    if (photo.TryGetProperty("src", out var src) &&
                        src.TryGetProperty("original", out var original))
                    {
                        image = original.GetString();
                    }
                }
            }
            return image;
        }
    }
}
