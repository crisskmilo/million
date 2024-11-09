using Dapper;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Repositories.Operation;
using Smartwyre.Domain.Interfaces.Repositories.Transversal;
using Smartwyre.Infra.Data.Repositories.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Infra.Data.Repositories.Operation
{
    public class RebateDataStore : BaseRepository<Rebate>, IRebateRepository
    {
        public RebateDataStore(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Rebate GetRebate(string rebateIdentifier)
        {
            Rebate entity = null;
            string sql = "SELECT * FROM rebate WHERE Identifier = '" + rebateIdentifier + "'";
            using (var db = this.DbFactory.GetConnection())
            {
                entity = db.Query<Rebate>(sql)?.FirstOrDefault();
            }
            return entity;
        }

        public void StoreCalculationResult(Rebate account, decimal rebateAmount)
        {
           account.Amount = rebateAmount;
           this.Update(account);
        }
    }
}
