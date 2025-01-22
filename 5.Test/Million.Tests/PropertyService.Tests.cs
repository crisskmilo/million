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
        /*
        mockPropertyRepository
            .Setup(st => st.GetProperty(propertyIdentifier))
            .Returns(property);

        mockPropertyRepository
            .Setup(st => st.StoreCalculationResult(property,10));*/

        var result = await propertyService.AddProperty(property);

        Assert.InRange(result.IdProperty,1,5);
    }
}
