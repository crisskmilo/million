using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Entities.Enums
{
    public enum SupportedIncentiveType
    {
        FixedRateRebate = 1 << 0,
        AmountPerUom = 1 << 1,
        FixedCashAmount = 1 << 2,
    }
}
