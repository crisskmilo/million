using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Interfaces.Operation
{
    public interface IPropertyApplication
    {
        Task<GenericResponse<IEnumerable<Property>>> GetProperties();

        Task<GenericResponse<Property>> GetPropertyById(int Id);

        Task<GenericResponse<Property>> AddProperty(Property property);

        Task<GenericResponse<Property>> UpdateProperty(Property property);

        Task<GenericResponse<int>> DeleteProperty(int Id);

        Task<GenericResponse<Property>> ChangePrice(PropertyPriceRequestDto propertyPriceRequestDto);

        Task<GenericResponse<List<Property>>> Search(SearchDto searchDto);
    }
}