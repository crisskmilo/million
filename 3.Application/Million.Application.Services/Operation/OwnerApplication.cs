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
    public class OwnerApplication : BaseApplication<Owner>, IOwnerApplication
    {
        /// <summary>
        /// Defines the ownerService
        /// </summary>
        private readonly IOwnerService ownerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerApplication"/> class.
        /// </summary>
        /// <param name="ownerService">The ownerService<see cref="IOwnerService"/></param>
        public OwnerApplication(IBaseService<Owner> baseService, IOwnerService ownerService) : base(baseService)
        {
            this.ownerService = ownerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<IEnumerable<Owner>>> GetOwners()
        {
            var obj = await this.ownerService.GetOwners();
            return HelperGeneric<IEnumerable<Owner>>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Owner>> GetOwnerById(int Id)
        {
            var obj = await this.ownerService.GetOwnerById(Id);
            return HelperGeneric<Owner>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Owner>> AddOwner(Owner owner)
        {
            var obj = await this.ownerService.AddOwner(owner);
            return HelperGeneric<Owner>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<Owner>> UpdateOwner(Owner owner)
        {
            var obj = await this.ownerService.UpdateOwner(owner);
            return HelperGeneric<Owner>.CastToGenericResponse(Helper.ManageResponse(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GenericResponse<int>> DeleteOwner(int Id)
        {
            var obj = await this.ownerService.DeleteOwner(Id);
            return HelperGeneric<int>.CastToGenericResponse(Helper.ManageResponse(obj));
        }
    }
}
