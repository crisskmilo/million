using Million.Application.Interfaces.Transversal;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Response;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Interfaces.Services.Transversal;
using Million.Domain.Services.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Services.Transversal
{
    public class ClimateApplication : IClimateApplication
    {
        /// <summary>
        /// Defines the climateService
        /// </summary>
        private readonly IClimateService climateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClimateApplication"/> class.
        /// </summary>
        /// <param name="climateService">The propertyImageService<see cref="IPropertyImageService"/></param>
        public ClimateApplication(IClimateService climateService)
        {
            this.climateService = climateService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<CurrentWeatherDto>> GetCurrentWeatherAsync(string location)
        {
            var obj = await this.climateService.GetCurrentWeatherAsync(location);
            return HelperGeneric<CurrentWeatherDto>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<List<DailyForecastDto>>> Get5DayForecastAsync(string location)
        {
            var obj = await this.climateService.Get5DayForecastAsync(location);
            return HelperGeneric<List<DailyForecastDto>>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<string>> SendWeatherEmailToCsvRecipientsAsync(string location)
        {
            await this.climateService.SendWeatherEmailToCsvRecipientsAsync(location);
            return HelperGeneric<string>.CastToGenericResponse(Helper.ManageResponse("Emails sent successfully."));
        }
    }
}

