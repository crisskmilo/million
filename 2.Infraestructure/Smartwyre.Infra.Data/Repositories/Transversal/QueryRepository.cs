namespace Smartwyre.Infra.Data.Repositories.Transversal
{
    using global::Dapper;
    using global::Dapper.Contrib.Extensions;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Smartwyre.Domain.Entities.Model.Transversal;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Smartwyre.Domain.Interfaces.Repositories.IQueryRepository{T}" />
    public class QueryRepository<T>: IQueryRepository<T> where T: BaseEntity
    {
        /// <summary>
        /// The db factory.
        /// </summary>
        protected readonly IDbFactory DbFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRepository{T}"/> class.
        /// </summary>
        /// <param name="dbFactory">The database factory.</param>
        public QueryRepository(IDbFactory dbFactory)
        {
            this.DbFactory = dbFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetQueryData(string sql, object param)
        {
            IEnumerable<T> entity;
            using (var db = this.DbFactory.GetConnection())
            {
                entity = db.Query<T>(sql, param);
            }
            return entity;
        }

        public virtual IEnumerable<T> GetQueryData(string sql)
        {
            IEnumerable<T> entity;
            using (var db = this.DbFactory.GetConnection())
            {
                entity = db.Query<T>(sql);
            }
            return entity;
        }

        /// <summary>
        /// Reads all.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteStoreProcedure(string procedure)
        {
            using (var db = this.DbFactory.GetConnection())
            {
                var response = await db.QueryAsync<T>(procedure,
                    commandType: CommandType.StoredProcedure);
                return response.ToList();
            }
        }

        /// <summary>
        /// Executes the store procedure parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteStoreProcedureParams(string procedure, T myObject)
        {
            var parameters = BuildParameters(myObject);
            using (var db = this.DbFactory.GetConnection())
            {
                var response = await db.QueryAsync<T>(procedure, parameters,
                    commandType: CommandType.StoredProcedure);
                return response.ToList();
            }
        }

        /// <summary>
        /// Executes the store procedure parameters.
        /// </summary>
        /// <param name="procedure">The procedure.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ExecuteStoreProcedureParams(string procedure, object parameters)
        {
            DynamicParameters dynamicParameters = (DynamicParameters)parameters;
            using (var db = this.DbFactory.GetConnection())
            {
                var response = await db.QueryAsync<T>(procedure, dynamicParameters,
                    commandType: CommandType.StoredProcedure);
                return response.ToList();
            }
        }

        /// <summary>
        /// Builds the parameters.
        /// </summary>
        /// <param name="myObject">My object.</param>
        /// <returns></returns>
        private DynamicParameters BuildParameters(T myObject)
        {
            Type types = myObject.GetType();
            List<PropertyInfo> infoProperties = this.GetObjectFields(types);
            DynamicParameters parameters = new DynamicParameters();
            foreach (PropertyInfo propertyInfo in infoProperties)
            {
                parameters.Add(propertyInfo.Name, this.GetValueField(propertyInfo, myObject));
            }
            // parameters.Add("VALOR_RETORNO", "", dbType: DbType.String, direction: ParameterDirection.Output);
            return parameters;
        }

        /// <summary>
        /// Gets the object fields.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <returns></returns>
        public List<PropertyInfo> GetObjectFields(Type types)
        {
            List<PropertyInfo> infoProperties = new List<PropertyInfo>();
            if (types == typeof(object))
            {
                return infoProperties;
            }
            infoProperties.AddRange(this.GetObjectFields(types.BaseType));
            infoProperties.AddRange(types.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            return infoProperties;
        }

        /// <summary>
        /// Gets the value field.
        /// </summary>
        /// <param name="prop">The property.</param>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        private object GetValueField(PropertyInfo prop, object obj)
        {
            var fieldType = prop.PropertyType;
            if (prop.GetValue(obj) != null)
            {
                if (fieldType.IsPrimitive || fieldType.IsSealed)
                {
                    return prop.GetValue(obj);
                }
                else
                {
                    return JsonConvert.SerializeObject(prop.GetValue(obj));
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myObject"></param>
        /// <returns></returns>
        public virtual T GetObjectById(T myObject)
        {
            T entity = null;
            Type type = typeof(T);
            dynamic tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");
            string sql = string.Concat(" SELECT * FROM ", tableattr, " WHERE id = ", myObject.id);
            using (var db = this.DbFactory.GetConnection())
            {
                entity = db.Query<T>(sql)?.FirstOrDefault();
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetObjectAll()
        {
            IEnumerable<T> entity = null;
            Type type = typeof(T);
            dynamic tableattr = type.GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute");
            string sql = string.Concat(" SELECT * FROM ", tableattr);
            using (var db = this.DbFactory.GetConnection())
            {
                entity = db.Query<T>(sql);
            }
            return entity;
        }
    }
}
