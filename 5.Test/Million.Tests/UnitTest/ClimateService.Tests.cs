using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Interfaces.Transversal;
using Million.Domain.Services.Transversal;
using System.Net.Http;
using Xunit;

namespace Million.DeveloperTest.Tests.UnitTest
{
    public class ClimateServiceTests
    {
        private readonly Mock<IApiClientConsumerStrategyContext> mockApiClientContext;
        private readonly Mock<IConfiguration> mockConfiguration;
        private readonly ClimateService climateService;

        public ClimateServiceTests()
        {
            mockApiClientContext = new Mock<IApiClientConsumerStrategyContext>();
            mockConfiguration = new Mock<IConfiguration>();

            climateService = new ClimateService(
                mockApiClientContext.Object,
                mockConfiguration.Object
            );
        }

        [Fact]
        public async Task GetCurrentWeatherAsync_ReturnsCurrentWeatherDto()
        {
            // Arrange
            var location = "Bogota";
            var apiKey = "fake-api-key";
            var baseUrl = "https://api.openweathermap.org/data/2.5";
            var jsonResponse = @"{
                ""name"": ""Bogota"",
                ""dt"": 1710000000,
                ""main"": {
                    ""temp"": 20.5,
                    ""temp_min"": 18.0,
                    ""temp_max"": 22.0
                },
                ""weather"": [
                    { ""main"": ""Clouds"", ""description"": ""scattered clouds"" }
                ]
            }";

            mockConfiguration.Setup(c => c["OpenWeatherMap:ApiKey"]).Returns(apiKey);
            mockConfiguration.Setup(c => c["OpenWeatherMap:BaseUrl"]).Returns(baseUrl);

            var httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            };

            mockApiClientContext
                .Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponse);

            // Act
            var result = await climateService.GetCurrentWeatherAsync(location);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Bogota", result.Location);
            Assert.Equal(20.5, result.Temperature);
            Assert.Equal(18.0, result.MinTemperature);
            Assert.Equal(22.0, result.MaxTemperature);
            Assert.Equal("Clouds", result.Weather);
            Assert.Equal("scattered clouds", result.Description);
        }

        [Fact]
        public async Task Get5DayForecastAsync_ReturnsListOfDailyForecastDto()
        {
            // Arrange
            var location = "Bogota";
            var apiKey = "fake-api-key";
            var baseUrl = "https://api.openweathermap.org/data/2.5";
            var jsonResponse = @"{
                ""list"": [
                    {
                        ""dt_txt"": ""2025-03-10 12:00:00"",
                        ""main"": { ""temp_min"": 10.0, ""temp_max"": 15.0 }
                    },
                    {
                        ""dt_txt"": ""2025-03-10 15:00:00"",
                        ""main"": { ""temp_min"": 11.0, ""temp_max"": 16.0 }
                    },
                    {
                        ""dt_txt"": ""2025-03-11 12:00:00"",
                        ""main"": { ""temp_min"": 12.0, ""temp_max"": 18.0 }
                    }
                ]
            }";

            mockConfiguration.Setup(c => c["OpenWeatherMap:ApiKey"]).Returns(apiKey);
            mockConfiguration.Setup(c => c["OpenWeatherMap:BaseUrl"]).Returns(baseUrl);

            var httpResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(jsonResponse)
            };

            mockApiClientContext
                .Setup(m => m.GetAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(httpResponse);

            // Act
            var result = await climateService.Get5DayForecastAsync(location);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, d => d.Date == new DateTime(2025, 3, 10) && d.MinTemperature == 10.0 && d.MaxTemperature == 16.0);
            Assert.Contains(result, d => d.Date == new DateTime(2025, 3, 11) && d.MinTemperature == 12.0 && d.MaxTemperature == 18.0);
        }
    }
}
