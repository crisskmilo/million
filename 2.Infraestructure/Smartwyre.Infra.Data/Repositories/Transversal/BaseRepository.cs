namespace Smartwyre.Infra.Data.Repositories.Transversal
{
    using System;
    using System.Data.SqlClient;
    using global::Dapper.Contrib.Extensions;
    using Smartwyre.Domain.Entities.Enums;
    using Newtonsoft.Json;
    using Smartwyre.Domain.Entities.Model.Transversal;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;

    public class BaseRepository<T> : QueryRepository<T>, IBaseRepository<T> where T : BaseEntity
    {
        public BaseRepository(IDbFactory dbFactory)
          : base(dbFactory)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual long Insert(T entity)
        {
            using (var db = this.DbFactory.GetConnection())
            {
               return db.Insert(entity);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            using (var db = this.DbFactory.GetConnection())
            {
                try
                {
                    db.Delete(entity);
                }
                catch (SqlException sqlException)
                {
                    if (sqlException.Number == (int)BaseRepositpryEnum.ERRORFIVEHOUNDREDFORTYSEVEN)
                    {
                        throw new InvalidOperationException("Error, this register has relationships.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            using (var db = this.DbFactory.GetConnection())
            {
                db.Update(entity);
            }
        }
    }
}
