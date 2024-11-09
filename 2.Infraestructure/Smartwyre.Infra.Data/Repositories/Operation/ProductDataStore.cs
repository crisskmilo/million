using Dapper;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Repositories.Operation;
using Smartwyre.Domain.Interfaces.Repositories.Transversal;
using Smartwyre.Infra.Data.Repositories.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Smartwyre.Infra.Data.Repositories.Operation
{
    public class ProductDataStore : BaseRepository<Product>, IProductRepository
    {
        public ProductDataStore(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Product GetProduct(string productIdentifier)
        {
            Product entity = null;
            string sql = "SELECT * FROM product WHERE Identifier = '" + productIdentifier + "'";
            using (var db = this.DbFactory.GetConnection())
            {
                entity = db.Query<Product>(sql)?.FirstOrDefault();
            }
            return entity;
        }
    }
}
