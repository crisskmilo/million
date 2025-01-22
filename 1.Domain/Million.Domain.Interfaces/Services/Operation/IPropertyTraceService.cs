using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Services.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Services.Operation
{
    public interface IPropertyTraceService : IBaseService<PropertyTrace>
    {
        Task<ICollection<PropertyTrace>> GetPropertyTraces();
        Task<PropertyTrace> GetPropertyTraceById(int Id);
        Task<PropertyTrace> AddPropertyTrace(PropertyTrace propertyTrace);
        Task<PropertyTrace> UpdatePropertyTrace(PropertyTrace propertyTrace);
        Task<int> DeletePropertyTrace(int Id);
    }
}

