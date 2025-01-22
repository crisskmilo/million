using Million.Application.Interfaces.Operation;
using Million.Application.Services.Transversal;
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
    public class PropertyImageApplication : BaseApplication<PropertyImage>, IPropertyImageApplication
    {
        /// <summary>
        /// Defines the propertyImageService
        /// </summary>
        private readonly IPropertyImageService propertyImageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyImageApplication"/> class.
        /// </summary>
        /// <param name="propertyImageService">The propertyImageService<see cref="IPropertyImageService"/></param>
        public PropertyImageApplication(IBaseService<PropertyImage> baseService, IPropertyImageService propertyImageService) : base(baseService)
        {
            this.propertyImageService = propertyImageService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<IEnumerable<PropertyImage>>> GetPropertyImages()
        {
            var obj = await this.propertyImageService.GetPropertyImages();
            return HelperGeneric<IEnumerable<PropertyImage>>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<PropertyImage>> GetPropertyImageById(int Id)
        {
            var obj = await this.propertyImageService.GetPropertyImageById(Id);
            return HelperGeneric<PropertyImage>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<PropertyImage>> AddPropertyImage(PropertyImageRequestDto propertyImage)
        {
            var obj = await this.propertyImageService.AddPropertyImage(propertyImage);
            return HelperGeneric<PropertyImage>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<PropertyImage>> UpdatePropertyImage(PropertyImage propertyImage)
        {
            var obj = await this.propertyImageService.UpdatePropertyImage(propertyImage);
            return HelperGeneric<PropertyImage>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<int>> DeletePropertyImage(int Id)
        {
            var obj = await this.propertyImageService.DeletePropertyImage(Id);
            return HelperGeneric<int>.CastToGenericResponse(Helper.ManageResponse(obj));
        }
    }
}

