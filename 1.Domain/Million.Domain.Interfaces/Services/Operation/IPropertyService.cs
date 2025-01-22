using Million.Domain.Entities.Dto.Transversal;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Entities.Request;
using Million.Domain.Interfaces.Services.Transversal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Million.Domain.Interfaces.Services.Operation
{
    public interface IPropertyService : IBaseService<Property>
    {
        Task<ICollection<Property>> GetProperties();
        Task<Property> GetPropertyById(int Id);
        Task<Property> AddProperty(Property property);
        Task<Property> UpdateProperty(Property property);
        Task<int> DeleteProperty(int Id);
        Task<Property> ChangePrice(PropertyPriceRequestDto propertyPriceRequestDto);
        Task<List<Property>> Search(SearchDto searchDto);
    }
}
