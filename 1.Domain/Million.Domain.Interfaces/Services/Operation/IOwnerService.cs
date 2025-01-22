using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Services.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Services.Operation
{
    public interface IOwnerService : IBaseService<Owner>
    {
        Task<ICollection<Owner>> GetOwners();
        Task<Owner> GetOwnerById(int Id);
        Task<Owner> AddOwner(Owner owner);
        Task<Owner> UpdateOwner(Owner owner);
        Task<int> DeleteOwner(int Id);
    }
}
