using Million.Application.Interfaces.Operation;
using Million.Application.Services.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Response;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Interfaces.Services.Transversal;
using Million.Domain.Services.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Services.Operation
{
    public class PropertyTraceApplication : BaseApplication<PropertyTrace>, IPropertyTraceApplication
    {
        /// <summary>
        /// Defines the propertyTraceService
        /// </summary>
        private readonly IPropertyTraceService propertyTraceService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyTraceApplication"/> class.
        /// </summary>
        /// <param name="propertyTraceService">The propertyTraceService<see cref="IPropertyTraceService"/></param>
        public PropertyTraceApplication(IBaseService<PropertyTrace> baseService, IPropertyTraceService propertyTraceService) : base(baseService)
        {
            this.propertyTraceService = propertyTraceService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<IEnumerable<PropertyTrace>>> GetPropertyTraces()
        {
            var obj = await this.propertyTraceService.GetPropertyTraces();
            return HelperGeneric<IEnumerable<PropertyTrace>>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<PropertyTrace>> GetPropertyTraceById(int Id)
        {
            var obj = await this.propertyTraceService.GetPropertyTraceById(Id);
            return HelperGeneric<PropertyTrace>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<PropertyTrace>> AddPropertyTrace(PropertyTrace propertyTrace)
        {
            var obj = await this.propertyTraceService.AddPropertyTrace(propertyTrace);
            return HelperGeneric<PropertyTrace>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<PropertyTrace>> UpdatePropertyTrace(PropertyTrace propertyTrace)
        {
            var obj = await this.propertyTraceService.UpdatePropertyTrace(propertyTrace);
            return HelperGeneric<PropertyTrace>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<int>> DeletePropertyTrace(int Id)
        {
            var obj = await this.propertyTraceService.DeletePropertyTrace(Id);
            return HelperGeneric<int>.CastToGenericResponse(Helper.ManageResponse(obj));
        }
    }
}
