using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Interfaces.Transversal
{
    public interface IClimateApplication
    {
        /// <summary>
        /// Gets the current weather for a given location.
        /// </summary>
        /// <param name="location">City name or coordinates.</param>
        /// <returns>JSON string with current weather data.</returns>
        Task<GenericResponse<CurrentWeatherDto>> GetCurrentWeatherAsync(string location);

        /// <summary>
        /// Gets the 5-day forecast for a given location.
        /// </summary>
        /// <param name="location">City name or coordinates.</param>
        /// <returns>JSON string with 5-day forecast data.</returns>
        Task<GenericResponse<List<DailyForecastDto>>> Get5DayForecastAsync(string location);

        Task<GenericResponse<string>> SendWeatherEmailToCsvRecipientsAsync(string location);
    }
}
