using Microsoft.Extensions.Configuration;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Domain.Interfaces.Services.Operation;
using Million.Domain.Services.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Services.Operation
{
    public class PropertyTraceService : BaseService<PropertyTrace>, IPropertyTraceService
    {

        /// <summary>
        /// Defines the propertyTrace Repository
        /// </summary>
        private readonly IPropertyTraceRepository propertyTraceRepository;

        /// <summary>
        /// Defines the repository
        /// </summary>
        private readonly IRepository<PropertyTrace> repository;

        /// <summary>
        /// Defines the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public PropertyTraceService(IPropertyTraceRepository propertyTraceRepository,
            IRepository<PropertyTrace> repository,
            IConfiguration configuration)
            : base(propertyTraceRepository)
        {
            this.propertyTraceRepository = propertyTraceRepository;
            this.repository = repository;
            this.configuration = configuration;
        }


        public async Task<PropertyTrace> AddPropertyTrace(PropertyTrace propertyTrace)
        {
            return await this.repository.AddAsync(propertyTrace);
        }

        public async Task<int> DeletePropertyTrace(int Id)
        {
            return await this.repository.DeleteAsync(Id);
        }

        public async Task<PropertyTrace> GetPropertyTraceById(int Id)
        {
            return await this.repository.GetByIdAsync(Id);
        }
        public async Task<ICollection<PropertyTrace>> GetPropertyTraces()
        {
            return await this.repository.GetAllAsync();
        }

        public async Task<PropertyTrace> UpdatePropertyTrace(PropertyTrace propertyTrace)
        {
            return await this.repository.UpdateAsync(propertyTrace);
        }
    }
}
