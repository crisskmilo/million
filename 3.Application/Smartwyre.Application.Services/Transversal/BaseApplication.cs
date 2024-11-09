namespace Smartwyre.Application.Services.Transversal
{
    using Smartwyre.Domain.Entities.Response;
    using System.Threading.Tasks;
    using Smartwyre.Application.Interfaces.Transversal;
    using Smartwyre.Domain.Services.Utilities;
    using Smartwyre.Domain.Entities.Model.Transversal;
    using Smartwyre.Domain.Interfaces.Services.Transversal;

    public class BaseApplication<T> : IBaseApplication<T> where T : BaseEntity
    {
        private readonly IBaseService<T> baseService;

        public BaseApplication(IBaseService<T> baseService) {
            this.baseService = baseService;
        }


        /// <summary>
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        public async Task<GenericResponse<T>> ExecuteProcedure(string procedure)
        {
            var list = await this.baseService.ExecuteProcedure(procedure);
            return HelperGeneric<T>.CastToGenericResponse(Util.ManageResponse(list));
        }

        /// <summary>
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual async Task<GenericResponse<T>> ExecuteProcedureWithParams(string procedure, T myObject)
        {
            var list = await this.baseService.ExecuteProcedureWithParams(procedure, myObject);
            return HelperGeneric<T>.CastToGenericResponse(Util.ManageResponse(list));
        }

        /// <summary>
        /// Executes the insert procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public virtual async Task<GenericResponse<T>> ExecuteInsertProcedure(string procedure, T myObject)
        {
            var list = await this.baseService.ExecuteInsertProcedure(procedure, myObject);
            return HelperGeneric<T>.CastToGenericResponse(Util.ManageResponse(list));
        }

        /// <summary>
        /// Executes the procedure with parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public virtual async Task<GenericResponse<T>> ExecuteProcedureWithParams(string procedure, object myObject)
        {
            var list = await this.baseService.ExecuteProcedureWithParams(procedure, myObject);
            return HelperGeneric<T>.CastToGenericResponse(Util.ManageResponse(list));
        }
    }
}
