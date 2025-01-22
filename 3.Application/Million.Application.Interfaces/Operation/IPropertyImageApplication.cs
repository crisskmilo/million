using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Entities.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Application.Interfaces.Operation
{
    public interface IPropertyImageApplication
    {
        Task<GenericResponse<IEnumerable<PropertyImage>>> GetPropertyImages();

        Task<GenericResponse<PropertyImage>> GetPropertyImageById(int Id);

        Task<GenericResponse<PropertyImage>> AddPropertyImage(PropertyImageRequestDto propertyImage);

        Task<GenericResponse<PropertyImage>> UpdatePropertyImage(PropertyImage propertyImage);

        Task<GenericResponse<int>> DeletePropertyImage(int Id);
    }
}