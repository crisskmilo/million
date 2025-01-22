namespace Million.Domain.Interfaces.Repositories.Transversal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IQueryRepository<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> GetQueryData(string sql, object param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> GetQueryData(string sql);

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteStoreProcedure(string procedure);

        /// <summary>
        /// Executes the store procedure parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteStoreProcedureParams(string procedure, T myObject);

        /// <summary>
        /// Executes the store procedure parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> ExecuteStoreProcedureParams(string procedure, object parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetObjectById(T myObject);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetObjectAll();
    }
}
