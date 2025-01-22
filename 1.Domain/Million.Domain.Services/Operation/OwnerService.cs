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
    public class OwnerService : BaseService<Owner>, IOwnerService
    {

        /// <summary>
        /// Defines the owner Repository
        /// </summary>
        private readonly IOwnerRepository ownerRepository;

        /// <summary>
        /// Defines the repository
        /// </summary>
        private readonly IRepository<Owner> repository;

        /// <summary>
        /// Defines the configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public OwnerService(IOwnerRepository ownerRepository,
            IRepository<Owner> repository,
            IConfiguration configuration)
            : base(ownerRepository)
        {
            this.ownerRepository = ownerRepository;
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<Owner> AddOwner(Owner owner)
        {
            return await this.repository.AddAsync(owner);
        }

        public async Task<int> DeleteOwner(int Id)
        {
            return await this.repository.DeleteAsync(Id);
        }

        public async Task<Owner> GetOwnerById(int Id)
        {
            return await this.repository.GetByIdAsync(Id);
        }
        public async Task<ICollection<Owner>> GetOwners()
        {
            return await this.repository.GetAllAsync();
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            return await this.repository.UpdateAsync(owner);
        }
    }
}
