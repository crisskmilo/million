using Smartwyre.Domain.Entities.Model.Authentication;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Repositories.Transversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Interfaces.Repositories.Operation
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Product GetProduct(string productIdentifier);
    }
}
