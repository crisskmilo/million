using Microsoft.Extensions.Configuration;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Services.Operation
{
    public class PropertyImageService : BaseService<PropertyImage>, IPropertyImageService
    {

        /// <summary>
        /// Defines the propertyImage Repository
        /// </summary>
        private readonly IPropertyImageRepository propertyImageRepository;

        /// <summary>
        /// Defines the repository
        /// </summary>
        private readonly IRepository<PropertyImage> repository;

        /// <summary>
        /// Defines the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public PropertyImageService(IPropertyImageRepository propertyImageRepository,
            IRepository<PropertyImage> repository,
            IConfiguration configuration)
            : base(propertyImageRepository)
        {
            this.propertyImageRepository = propertyImageRepository;
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<PropertyImage> AddPropertyImage(PropertyImageRequestDto propertyImage)
        {
            return await this.repository.AddAsync(new PropertyImage() { 
                IdProperty = propertyImage.IdProperty,
                File = propertyImage.File,
                Enabled = true
            });            
        }

        public async Task<int> DeletePropertyImage(int Id)
        {
            return await this.repository.DeleteAsync(Id);
        }

        public async Task<PropertyImage> GetPropertyImageById(int Id)
        {
            return await this.repository.GetByIdAsync(Id);
        }
        public async Task<ICollection<PropertyImage>> GetPropertyImages()
        {
            return await this.repository.GetAllAsync();
        }

        public async Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage)
        {
            return await this.repository.UpdateAsync(propertyImage);
        }
    }
}
