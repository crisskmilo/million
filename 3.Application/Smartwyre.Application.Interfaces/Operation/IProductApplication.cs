using Smartwyre.Domain.Entities.Model.Authentication;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Entities.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Application.Interfaces.Operation
{
    public interface IProductApplication
    {
        GenericResponse<Product> GetProduct(string Identifier);
    }
}
