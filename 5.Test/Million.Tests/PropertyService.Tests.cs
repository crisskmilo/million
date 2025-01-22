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
using Newtonsoft.Json.Linq;

namespace Million.DeveloperTest.Tests;

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
            .Setup(st => st.UpdateAsync(propertyRes))
            .ReturnsAsync(propertyRes);

        var result = await propertyService.ChangePrice(propertyPrice);

        Assert.Equal(result.Price, Price);
    }
}
