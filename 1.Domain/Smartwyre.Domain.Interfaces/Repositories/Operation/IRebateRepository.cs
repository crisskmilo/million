using Smartwyre.Domain.Entities.Model.Authentication;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Interfaces.Repositories.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Interfaces.Repositories.Operation
{
    public interface IRebateRepository : IBaseRepository<Rebate>
    {
        Rebate GetRebate(string rebateIdentifier);

        void StoreCalculationResult(Rebate account, decimal rebateAmount);
    }
}
