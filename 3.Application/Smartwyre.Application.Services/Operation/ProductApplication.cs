using Smartwyre.Application.Interfaces.Transversal;
using Smartwyre.Application.Services.Transversal;
using Smartwyre.Domain.Entities.Model.Authentication;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Services.Transversal;
using Smartwyre.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartwyre.Domain.Interfaces.Services.Operation;
using Smartwyre.Domain.Entities.Response;
using Smartwyre.Domain.Services.Utilities;
using Smartwyre.Application.Interfaces.Operation;

namespace Smartwyre.Application.Services.Operation
{
    public class ProductApplication : BaseApplication<Product>, IProductApplication
    {
        /// <summary>
        /// Defines the productService
        /// </summary>
        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductApplication"/> class.
        /// </summary>
        /// <param name="productService">The productService<see cref="IProductService"/></param>
        public ProductApplication(IBaseService<Product> baseService, IProductService productService) : base(baseService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GenericResponse<Product> GetProduct(string Identifier)
        {
            var obj = this.productService.GetProduct(Identifier);
            return HelperGeneric<Product>.CastToGenericResponse(Helper.ManageResponse(obj));
        }
    }
}
