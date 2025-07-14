using Million.Domain.Entities.Dto.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Services.Transversal
{
    public interface IClimateService
    {
        /// <summary>  
        /// Gets the current weather for a given location.  
        /// </summary>  
        /// <param name="location">City name or coordinates.</param>  
        /// <returns>CurrentWeatherDto with current weather values.</returns>  
        Task<CurrentWeatherDto> GetCurrentWeatherAsync(string location);

        /// <summary>  
        /// Gets the 5-day forecast for a given location.  
        /// </summary>  
        /// <param name="location">City name or coordinates.</param>  
        /// <returns>List of data with 5-day forecast data.</returns>  
        Task<List<DailyForecastDto>> Get5DayForecastAsync(string location);

        /// <summary>  
        /// Sends weather email to CSV recipients for a given location.  
        /// </summary>  
        /// <param name="location">City name or coordinates.</param>  
        /// <returns>A task representing the asynchronous operation.</returns>  
        Task SendWeatherEmailToCsvRecipientsAsync(string location);
    }
}
