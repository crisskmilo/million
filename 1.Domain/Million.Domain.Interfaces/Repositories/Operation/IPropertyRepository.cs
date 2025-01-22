using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Repositories.Operation
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Task<List<Property>> Search(SearchDto searchDto);
    }
}
