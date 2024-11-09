using Smartwyre.Application.Interfaces.Operation;
using Smartwyre.Application.Services.Transversal;
using Smartwyre.Domain.Entities.Dto.Operation;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Entities.Response;
using Smartwyre.Domain.Interfaces.Services.Operation;
using Smartwyre.Domain.Interfaces.Services.Transversal;
using Smartwyre.Domain.Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Application.Services.Operation
{
    public class RebateApplication : BaseApplication<Rebate>, IRebateApplication
    {
        /// <summary>
        /// Defines the rebateService
        /// </summary>
        private readonly IRebateService rebateService;

        private readonly IProductService productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RebateApplication"/> class.
        /// </summary>
        /// <param name="rebateService">The rebateService<see cref="IRebateService"/></param>
        public RebateApplication(IBaseService<Rebate> baseService, IRebateService rebateService,
            IProductService productService) : base(baseService)
        {
            this.rebateService = rebateService;
            this.productService = productService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GenericResponse<CalculateRebateResult> Calculate(CalculateRebateRequest request)
        {
            var product = this.productService.GetProduct(request.ProductIdentifier);
            var obj = this.rebateService.Calculate(request, product);
            return HelperGeneric<CalculateRebateResult>.CastToGenericResponse(Helper.ManageResponse(obj));
        }
    }
}
