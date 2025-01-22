using Million.Application.Interfaces.Operation;
using Million.Application.Services.Transversal;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Entities.Response;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Interfaces.Services.Transversal;
using Million.Domain.Services.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Services.Operation
{
    public class PropertyApplication : BaseApplication<Property>, IPropertyApplication
    {
        /// <summary>
        /// Defines the propertyService
        /// </summary>
        private readonly IPropertyService propertyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyApplication"/> class.
        /// </summary>
        /// <param name="propertyService">The propertyService<see cref="IPropertyService"/></param>
        public PropertyApplication(IBaseService<Property> baseService, IPropertyService propertyService) : base(baseService)
        {
            this.propertyService = propertyService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<IEnumerable<Property>>> GetProperties()
        {
            var obj = await this.propertyService.GetProperties();
            return HelperGeneric<IEnumerable<Property>>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Property>> GetPropertyById(int Id)
        {
            var obj = await this.propertyService.GetPropertyById(Id);
            return HelperGeneric<Property>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Property>> AddProperty(Property property)
        {
            var obj = await this.propertyService.AddProperty(property);
            return HelperGeneric<Property>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Property>> UpdateProperty(Property property)
        {
            var obj = await this.propertyService.UpdateProperty(property);
            return HelperGeneric<Property>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<int>> DeleteProperty(int Id)
        {
            var obj = await this.propertyService.DeleteProperty(Id);
            return HelperGeneric<int>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Property>> ChangePrice(PropertyPriceRequestDto propertyPriceRequestDto)
        {
            var obj = await this.propertyService.ChangePrice(propertyPriceRequestDto);
            return HelperGeneric<Property>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<List<Property>>> Search(SearchDto searchDto)
        {
            var obj = await this.propertyService.Search(searchDto);
            return HelperGeneric<List<Property>>.CastToGenericResponse(Helper.ManageResponse(obj));
        }
    }
}
