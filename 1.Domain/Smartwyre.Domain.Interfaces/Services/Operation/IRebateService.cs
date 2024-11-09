using Smartwyre.Domain.Entities.Dto.Operation;
using Smartwyre.Domain.Entities.Model.Authentication;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Services.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Interfaces.Services.Operation
{
    public interface IRebateService : IBaseService<Rebate>
    {
        CalculateRebateResult Calculate(CalculateRebateRequest request, Product product);
    }
}
