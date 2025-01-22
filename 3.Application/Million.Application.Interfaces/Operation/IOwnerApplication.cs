using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Interfaces.Operation
{
    public interface IOwnerApplication
    {
        Task<GenericResponse<IEnumerable<Owner>>> GetOwners();

        Task<GenericResponse<Owner>> GetOwnerById(int Id);

        Task<GenericResponse<Owner>> AddOwner(Owner owner);

        Task<GenericResponse<Owner>> UpdateOwner(Owner owner);

        Task<GenericResponse<int>> DeleteOwner(int Id);
    }
}
