using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Million.Domain.Entities.Dto.Transversal;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Million.Domain.Entities.Response;

namespace Million.DeveloperTest.Tests.IntegrationTest
{
    public class ClimateControllerE2E : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ClimateControllerE2E(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCurrentWeather_ReturnsSuccessAndWeatherDto()
        {
            // Arrange
            var location = "Bogota";
            var url = $"/api/climate/current?location={location}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GenericResponse<CurrentWeatherDto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.NotNull(result.Entity);
            Assert.Equal(location, result.Entity.Location);
        }

        [Fact]
        public async Task Get5DayForecast_ReturnsSuccessAndForecastList()
        {
            // Arrange
            var location = "Bogota";
            var url = $"/api/climate/forecast?location={location}";

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GenericResponse<List<DailyForecastDto>>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.NotNull(result.Entity);
            Assert.True(result.Entity.Count > 0);
        }
    }
}