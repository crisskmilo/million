using Microsoft.Extensions.Configuration;
using Moq;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Domain.Services.Operation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Million.Domain.Entities.Request;
using Million.Domain.Entities.Dto.Transversal;

namespace Million.DeveloperTest.Tests.UnitTest;

public class PropertyServiceTests
{
    private readonly MockRepository mockRepositories;
    private readonly Mock<IBaseRepository<Property>> mockBaseRepository;
    private readonly Mock<IPropertyRepository> mockPropertyRepository;
    private readonly Mock<IRepository<Property>> mockRepository;
    private readonly Mock<IConfiguration> mockConfiguration;
    private readonly PropertyService propertyService;

    public PropertyServiceTests()
    {
        mockRepositories = new MockRepository(MockBehavior.Default);
        mockBaseRepository = mockRepositories.Create<IBaseRepository<Property>>();
        mockPropertyRepository = mockRepositories.Create<IPropertyRepository>();
        mockRepository = mockRepositories.Create<IRepository<Property>>();
        mockConfiguration = mockRepositories.Create<IConfiguration>();
        propertyService = new PropertyService(mockPropertyRepository.Object, mockRepository.Object, mockConfiguration.Object);
    }

    [Theory]
    [InlineData("Prop1", "street 1", 1, 1, "2025-01-01")]
    [InlineData("Prop2", "street 1", 2, 2, "2025-01-01")]
    [InlineData("Prop3", "street 2", 3, 3, "2025-01-01")]
    public async Task Create_Property_Building(string Name, string Address, decimal Price, int CodeInternal, DateTime Year)
    {
        Property property = new Property()
        {
            Name = Name,
            Address = Address,
            Price = Price,
            CodeInternal = CodeInternal,
            Year = Year
        };

        Property propertyRes = new Property()
        {
            Name = Name,
            Address = Address,
            Price = Price,
            CodeInternal = CodeInternal,
            Year = Year,
            IdProperty = 1
        };

        mockRepository
            .Setup(st => st.AddAsync(property))
            .ReturnsAsync(propertyRes);

        var result = await propertyService.AddProperty(property);

        Assert.InRange(result.IdProperty,1,5);
    }

    [Theory]
    [InlineData(1, 100)]
    [InlineData(2, 200)]
    [InlineData(3, 300)]
    public async Task Change_Property_Price(int IdProperty, decimal Price)
    {
        PropertyPriceRequestDto propertyPrice = new PropertyPriceRequestDto()
        {
            IdProperty = IdProperty,
            Price = Price,
        };

        Property propertyReq = new Property()
        {
            Name = "Prop1",
            Address = "street",
            Price = 50,
            CodeInternal = 1,
            Year = new DateTime(2025, 01, 01),
            IdProperty = IdProperty
        };

        Property propertyRes = new Property()
        {
            Name = "Prop1",
            Address = "street",
            Price = Price,
            CodeInternal = 1,
            Year = new DateTime(2025,01,01),
            IdProperty = IdProperty
        };

        mockRepository
            .Setup(st => st.GetByIdAsync(IdProperty))
            .ReturnsAsync(propertyReq);

        mockRepository
            .Setup(st => st.UpdateAsync(propertyReq))
            .ReturnsAsync(propertyRes);

        var result = await propertyService.ChangePrice(propertyPrice);

        Assert.Equal(result.Price, Price);
    }

    [Theory]
    [InlineData(1, 100, "street 2")]
    [InlineData(2, 200, "street 3")]
    [InlineData(3, 300, "street 4")]
    public async Task Update_Property(int IdProperty, decimal Price, string Address)
    {
        Property propertyReq = new Property()
        {
            Name = "Prop1",
            Address = "street",
            Price = 50,
            CodeInternal = 1,
            Year = new DateTime(2025, 01, 01),
            IdProperty = IdProperty
        };

        Property propertyRes = new Property()
        {
            Name = "Prop1",
            Address = Address,
            Price = Price,
            CodeInternal = 1,
            Year = new DateTime(2025, 01, 01),
            IdProperty = IdProperty
        };

        mockRepository
            .Setup(st => st.UpdateAsync(propertyReq))
            .ReturnsAsync(propertyRes);

        var result = await propertyService.UpdateProperty(propertyReq);

        Assert.Equal(result.Price, Price);
    }

    [Theory]
    [InlineData("", "100", "")]
    [InlineData("", "100", "1")]
    [InlineData("Hom", "300", "")]
    public async Task List_Property_With_Filters(string Name, string Price, string Address)
    {
        SearchDto searchDto = new SearchDto()
        {
            Filters = new Dictionary<string, string[]>()
            {
                { "Name", new string[] { Name }},
                { "Price", new string[] { Price } },
                { "Address", new string[] { Address } }
            }
        };

        List<Property> properties = new List<Property>() {
                new Property()
            {
                Name = "Home 4",
                Address = "Avenue 5 Street 1",
                Price = 100,
                CodeInternal = 1,
                Year = new DateTime(2025, 01, 01),
                IdProperty = 1
            },new Property()
            {
                Name = "Luxury Home",
                Address = "Avenue 1 Street 4",
                Price = 100,
                CodeInternal = 1,
                Year = new DateTime(2025, 01, 01),
                IdProperty = 1
            }
        };

        Property propertyAux = new Property()
        {
            Name = "My Home Property 1",
            Address = "Avenue 2 Street 1",
            Price = 100,
            CodeInternal = 1,
            Year = new DateTime(2025, 01, 01),
            IdProperty = 1
        };

        properties.Add(propertyAux);

        mockPropertyRepository
            .Setup(st => st.Search(searchDto))
            .ReturnsAsync(properties);

        var result = await propertyService.Search(searchDto);

        Assert.NotEmpty(result);
    }
}
