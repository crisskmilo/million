using Microsoft.Extensions.Configuration;
using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Services.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Services.Operation
{
    public class PropertyService : BaseService<Property>, IPropertyService
    {

        /// <summary>
        /// Defines the property Repository
        /// </summary>
        private readonly IPropertyRepository propertyRepository;

        /// <summary>
        /// Defines the repository
        /// </summary>
        private readonly IRepository<Property> repository;

        /// <summary>
        /// Defines the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public PropertyService(IPropertyRepository propertyRepository,
            IRepository<Property> repository,
            IConfiguration configuration)
            : base(propertyRepository)
        {
            this.propertyRepository = propertyRepository;
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<Property> AddProperty(Property property)
        {
            return await this.repository.AddAsync(property);
        }

        public async Task<int> DeleteProperty(int Id)
        {
            return await this.repository.DeleteAsync(Id);
        }

        public async Task<Property> GetPropertyById(int Id)
        {
            return await this.repository.GetByIdAsync(Id);
        }

        public async Task<ICollection<Property>> GetProperties()
        {
            return await this.repository.GetAllAsync();             
        }

        public async Task<Property> UpdateProperty(Property property)
        {
            return await this.repository.UpdateAsync(property);
        }

        public async Task<Property> ChangePrice(PropertyPriceRequestDto propertyPriceRequestDto)
        {
            var propertyAux = await this.GetPropertyById(propertyPriceRequestDto.IdProperty);
            if(propertyAux != null)
            {
                propertyAux.Price = propertyPriceRequestDto.Price;
                return await this.UpdateProperty(propertyAux);
            }
            return null;
        }

        public async Task<List<Property>> Search(SearchDto searchDto)
        {
           return await this.propertyRepository.Search(searchDto);
        }
    }
}
