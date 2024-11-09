using Smartwyre.Domain.Entities.Enums;
using Smartwyre.Domain.Entities.Model.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Entities.Model.Operation
{
    public class RebateCalculation : BaseEntity
    {
        public string Identifier { get; set; }
        public string RebateIdentifier { get; set; }
        public IncentiveType IncentiveType { get; set; }
        public decimal Amount { get; set; }
    }
}
