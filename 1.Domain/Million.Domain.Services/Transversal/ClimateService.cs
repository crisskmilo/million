using Microsoft.Extensions.Configuration;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Interfaces.Services.Transversal;
using Million.Domain.Interfaces.Transversal;
using Million.Domain.Services.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Million.Domain.Services.Transversal
{
    public class ClimateService : IClimateService
    {
        private readonly IApiClientConsumerStrategyContext apiClientContext;
        private readonly IConfiguration configuration;

        public ClimateService(
            IApiClientConsumerStrategyContext apiClientContext,
            IConfiguration configuration)
        {
            this.apiClientContext = apiClientContext;
            this.configuration = configuration;
        }

        public async Task<CurrentWeatherDto> GetCurrentWeatherAsync(string location)
        {
            CurrentWeatherDto currentWeatherDto = null;
            var apiKey = configuration["OpenWeatherMap:ApiKey"];
            var baseUrl = configuration["OpenWeatherMap:BaseUrl"];
            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(baseUrl))
                throw new Exception("OpenWeatherMap configuration is missing.");

            var url = $"{baseUrl}/weather?q={location}&appid={apiKey}&units=metric&lang=es";
            var response = await apiClientContext.GetAsync(url, string.Empty);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenWeatherMap API request failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new Exception("OpenWeatherMap API returned empty response.");
            }
            else
            {
                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                currentWeatherDto = new CurrentWeatherDto
                {
                    Location = root.GetProperty("name").GetString(),
                    Date = DateTimeOffset.FromUnixTimeSeconds(root.GetProperty("dt").GetInt64()).DateTime,
                    Temperature = root.GetProperty("main").GetProperty("temp").GetDouble(),
                    MinTemperature = root.GetProperty("main").GetProperty("temp_min").GetDouble(),
                    MaxTemperature = root.GetProperty("main").GetProperty("temp_max").GetDouble(),
                    Weather = root.GetProperty("weather")[0].GetProperty("main").GetString(),
                    Description = root.GetProperty("weather")[0].GetProperty("description").GetString()
                };
            }

            return currentWeatherDto;
        }

        public async Task<List<DailyForecastDto>> Get5DayForecastAsync(string location)
        {
            var result = new List<DailyForecastDto>();
            var apiKey = configuration["OpenWeatherMap:ApiKey"];
            var baseUrl = configuration["OpenWeatherMap:BaseUrl"];
            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(baseUrl))
                throw new Exception("OpenWeatherMap configuration is missing.");

            var url = $"{baseUrl}/forecast?q={location}&appid={apiKey}&units=metric&lang=es";
            var response = await apiClientContext.GetAsync(url, string.Empty);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"OpenWeatherMap API request failed: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new Exception("OpenWeatherMap API returned empty response.");
            }
            else
            {
                using var doc = JsonDocument.Parse(json);

                if (doc == null)
                {
                    throw new Exception("OpenWeatherMap API returned empty response.");
                }

                var dailyGroups = doc.RootElement.GetProperty("list")
                    .EnumerateArray()
                    .GroupBy(
                        x => DateTime.Parse(x.GetProperty("dt_txt").GetString()).Date
                    );

                foreach (var group in dailyGroups)
                {
                    var min = group.Min(x => x.GetProperty("main").GetProperty("temp_min").GetDouble());
                    var max = group.Max(x => x.GetProperty("main").GetProperty("temp_max").GetDouble());
                    result.Add(new DailyForecastDto
                    {
                        Date = group.Key,
                        MinTemperature = min,
                        MaxTemperature = max
                    });
                }
            }
            return result;
        }

        public async Task SendWeatherEmailToCsvRecipientsAsync(string location)
        {
            // Read resource paths from configuration
            var emailsCsvPath = configuration["Resources:EmailsCsv"];
            var templateHtmlPath = configuration["Resources:ClimateTemplateHtml"];

            if (string.IsNullOrWhiteSpace(emailsCsvPath) || string.IsNullOrWhiteSpace(templateHtmlPath))
                throw new Exception("Resource file paths are not configured in appsettings.json.");

            // 1. Read email addresses from CSV
            var csvContent = FileReader.ReadAllText(emailsCsvPath);
            var recipients = csvContent
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .ToList();

            if (recipients.Count == 0)
                throw new Exception("No email addresses found in the CSV file.");

            // 2. Get weather data
            var currentWeather = await GetCurrentWeatherAsync(location);
            var forecast = await Get5DayForecastAsync(location);

            // 3. Read HTML template
            var template = FileReader.ReadAllText(templateHtmlPath);

            // 4. Replace placeholders in template
            var htmlBody = template
                .Replace("{{Location}}", currentWeather.Location)
                .Replace("{{Date}}", currentWeather.Date.ToString("yyyy-MM-dd HH:mm"))
                .Replace("{{Temperature}}", currentWeather.Temperature.ToString("F1"))
                .Replace("{{MinTemperature}}", currentWeather.MinTemperature.ToString("F1"))
                .Replace("{{MaxTemperature}}", currentWeather.MaxTemperature.ToString("F1"))
                .Replace("{{Weather}}", currentWeather.Weather)
                .Replace("{{Description}}", currentWeather.Description);

            // Build forecast table rows
            var sb = new StringBuilder();
            foreach (var day in forecast)
            {
                sb.AppendLine($"<tr><td>{day.Date:yyyy-MM-dd}</td><td>{day.MinTemperature:F1}</td><td>{day.MaxTemperature:F1}</td></tr>");
            }
            htmlBody = htmlBody.Replace("{{ForecastRows}}", sb.ToString());

            // 5. Send email to each recipient
            var smtpHost = configuration["Smtp:Host"];
            var smtpPort = int.Parse(configuration["Smtp:Port"] ?? "25");
            var smtpUser = configuration["Smtp:User"];
            var smtpPass = configuration["Smtp:Password"];
            var smtpFrom = configuration["Smtp:From"];

            using var smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = bool.TryParse(configuration["Smtp:EnableSsl"], out var enableSsl) ? enableSsl : true
            };

            foreach (var recipient in recipients)
            {
                var mail = new MailMessage
                {
                    From = new MailAddress(smtpFrom),
                    Subject = $"Weather Report for {currentWeather.Location}",
                    Body = htmlBody,
                    IsBodyHtml = true
                };
                mail.To.Add(recipient);

                await smtpClient.SendMailAsync(mail);
            }
        }
    }
}
