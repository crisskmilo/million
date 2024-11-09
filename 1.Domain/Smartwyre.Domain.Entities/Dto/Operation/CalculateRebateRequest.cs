using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Entities.Dto.Operation
{
    public class CalculateRebateRequest
    {
        public string RebateIdentifier { get; set; }

        public string ProductIdentifier { get; set; }

        public decimal Volume { get; set; }
    }
}
