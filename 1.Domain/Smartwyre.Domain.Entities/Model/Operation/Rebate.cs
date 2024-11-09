using Smartwyre.Domain.Entities.Enums;
using Smartwyre.Domain.Entities.Model.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Entities.Model.Operation
{
    public class Rebate: BaseEntity
    {
        public string Identifier { get; set; }
        public IncentiveType Incentive { get; set; }
        public decimal Amount { get; set; }
        public decimal Percentage { get; set; }
    }
}
