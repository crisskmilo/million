using Smartwyre.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Entities.Model.Transversal
{
    [Serializable]
    public class Product: BaseEntity
    {
        public string Identifier { get; set; }
        public decimal Price { get; set; }
        public string Uom { get; set; }
        public SupportedIncentiveType SupportedIncentives { get; set; }
    }
}
