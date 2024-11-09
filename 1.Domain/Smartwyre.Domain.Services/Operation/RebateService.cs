using Microsoft.Extensions.Configuration;
using Smartwyre.Domain.Entities.Dto.Operation;
using Smartwyre.Domain.Entities.Enums;
using Smartwyre.Domain.Entities.ErrorHandler;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Repositories.Operation;
using Smartwyre.Domain.Interfaces.Services.Operation;
using Smartwyre.Domain.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.Domain.Services.Operation
{
    public class RebateService : BaseService<Rebate>, IRebateService
    {

        /// <summary>
        /// Defines the rebate Repository
        /// </summary>
        private readonly IRebateRepository rebateRepository;

        /// <summary>
        /// Defines the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public RebateService(IRebateRepository rebateRepository,
            IConfiguration configuration)
            : base(rebateRepository)
        {
            this.rebateRepository = rebateRepository;
            this.configuration = configuration;
        }

        public CalculateRebateResult Calculate(CalculateRebateRequest request, Product product)
        {
            if (product == null) {
                throw new ExceptionGeneric(ExceptionGenericTypes.Validations, "Product not found.");
            }

            Rebate rebate = rebateRepository.GetRebate(request.RebateIdentifier);

            var result = new CalculateRebateResult();

            var rebateAmount = 0m;

            switch (rebate.Incentive)
            {
                case IncentiveType.FixedCashAmount:
                    if (rebate == null)
                    {
                        result.Success = false;
                    }
                    else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
                    {
                        result.Success = false;
                    }
                    else if (rebate.Amount == 0)
                    {
                        result.Success = false;
                    }
                    else
                    {
                        rebateAmount = rebate.Amount;
                        result.Success = true;
                    }
                    break;

                case IncentiveType.FixedRateRebate:
                    if (rebate == null)
                    {
                        result.Success = false;
                    }
                    else if (product == null)
                    {
                        result.Success = false;
                    }
                    else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
                    {
                        result.Success = false;
                    }
                    else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
                    {
                        result.Success = false;
                    }
                    else
                    {
                        rebateAmount += product.Price * rebate.Percentage * request.Volume;
                        result.Success = true;
                    }
                    break;

                case IncentiveType.AmountPerUom:
                    if (rebate == null)
                    {
                        result.Success = false;
                    }
                    else if (product == null)
                    {
                        result.Success = false;
                    }
                    else if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
                    {
                        result.Success = false;
                    }
                    else if (rebate.Amount == 0 || request.Volume == 0)
                    {
                        result.Success = false;
                    }
                    else
                    {
                        rebateAmount += rebate.Amount * request.Volume;
                        result.Success = true;
                    }
                    break;
            }

            if (result.Success)
            {
                rebateRepository.StoreCalculationResult(rebate, rebateAmount);
            }

            return result;
        }
    }
}
