using Microsoft.Extensions.Configuration;
using Smartwyre.Domain.Entities.Dto.Operation;
using Smartwyre.Domain.Entities.Enums;
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
    public class ProductService : BaseService<Product>, IProductService
    {

        /// <summary>
        /// Defines the product Repository
        /// </summary>
        private readonly IProductRepository productRepository;

        /// <summary>
        /// Defines the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public ProductService(IProductRepository productRepository,
            IConfiguration configuration)
            : base(productRepository)
        {
            this.productRepository = productRepository;
            this.configuration = configuration;
        }

        public Product GetProduct(string productIdentifier)
        {
            return this.productRepository.GetProduct(productIdentifier);
        }
    }
}
