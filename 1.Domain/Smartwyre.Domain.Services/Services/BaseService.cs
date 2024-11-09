namespace Smartwyre.Domain.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Smartwyre.Domain.Entities.Model.Transversal;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;
    using Smartwyre.Domain.Interfaces.Services.Transversal;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Smartwyre.Domain.Interfaces.Interfaces.IBaseService{T}" />
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> baseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T}"/> class.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        public BaseService(IBaseRepository<T> baseRepository) {
            this.baseRepository = baseRepository;
        }

        /// <summary>
        /// Reads procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> ExecuteProcedure(string procedure)
        {
            return this.baseRepository.ExecuteStoreProcedure(procedure);
        }

        /// <summary>
        /// Procedures the with parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public Task<IEnumerable<T>> ExecuteProcedureWithParams(string procedure, T myObject)
        {
            return this.baseRepository.ExecuteStoreProcedureParams(procedure, myObject);
        }

        /// <summary>
        /// Executes the insert procedure.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> ExecuteInsertProcedure(string procedure, T myObject)
        {
            return this.ExecuteProcedureWithParams(procedure, myObject);
        }

        /// <summary>
        /// Executes the procedure with parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public virtual Task<IEnumerable<T>> ExecuteProcedureWithParams(string procedure, object myObject)
        {
            return this.baseRepository.ExecuteStoreProcedureParams(procedure, myObject);
        }
    }
}
