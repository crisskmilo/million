namespace Smartwyre.Application.Interfaces.Transversal
{
    using System.Threading.Tasks;
    using Smartwyre.Domain.Entities.Response;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseApplication<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <returns></returns>
        Task<GenericResponse<T>> ExecuteProcedure(string procedure);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="myObject"></param>
        /// <returns></returns>
        Task<GenericResponse<T>> ExecuteProcedureWithParams(string procedure, T myObject);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="myObject"></param>
        /// <returns></returns>
        Task<GenericResponse<T>> ExecuteInsertProcedure(string procedure, T myObject);

        /// <summary>
        /// Executes the procedure with parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        Task<GenericResponse<T>> ExecuteProcedureWithParams(string procedure, object myObject);
    }
}
