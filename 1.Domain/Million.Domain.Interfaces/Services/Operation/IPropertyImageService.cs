using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Interfaces.Services.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Services.Operation
{
    public interface IPropertyImageService : IBaseService<PropertyImage>
    {
        Task<ICollection<PropertyImage>> GetPropertyImages();
        Task<PropertyImage> GetPropertyImageById(int Id);
        Task<PropertyImage> AddPropertyImage(PropertyImageRequestDto propertyImage);
        Task<PropertyImage> UpdatePropertyImage(PropertyImage propertyImage);
        Task<int> DeletePropertyImage(int Id);
    }
}