using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Response;
using Million.WebApi;

namespace Million.DeveloperTest.Tests.IntegrationTest
{
    public class ClimateControllerE2E : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly string _token;

        public ClimateControllerE2E(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();

            // ✅ Carga el entorno "qa" desde appsettings.qa.json
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.qa.json", optional: false, reloadOnChange: true)
                .Build();

            var secret = config["AppSettings:Secret"];
            _token = JwtTokenGenerator.GenerateToken(secret, "testuser@example.com");
        }

        [Fact]
        public async Task GetCurrentWeather_ReturnsSuccessAndWeatherDto()
        {
            // Arrange
            var location = "Bogota";
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Climate/GetCurrentWeather/{location}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GenericResponse<CurrentWeatherDto>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.NotNull(result.result);
        }

        [Fact]
        public async Task Get5DayForecast_ReturnsSuccessAndForecastList()
        {
            // Arrange
            var location = "Bogota";
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/Climate/Get5DayForecast/{location}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<GenericResponse<List<DailyForecastDto>>>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(result);
            Assert.True(result.isSuccess);
            Assert.NotNull(result.result);
        }
    }
}
