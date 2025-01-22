using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Interfaces.Operation
{
    public interface IPropertyTraceApplication
    {
        Task<GenericResponse<IEnumerable<PropertyTrace>>> GetPropertyTraces();

        Task<GenericResponse<PropertyTrace>> GetPropertyTraceById(int Id);

        Task<GenericResponse<PropertyTrace>> AddPropertyTrace(PropertyTrace propertyTrace);

        Task<GenericResponse<PropertyTrace>> UpdatePropertyTrace(PropertyTrace propertyTrace);

        Task<GenericResponse<int>> DeletePropertyTrace(int Id);
    }
}