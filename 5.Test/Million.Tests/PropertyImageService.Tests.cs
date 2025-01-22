using Microsoft.Extensions.Configuration;
using Moq;
using Million.Domain.Entities.Model.Operation;
using Million.Domain.Interfaces.Repositories.Operation;
using Million.Domain.Interfaces.Repositories.Transversal;
using Million.Domain.Services.Operation;
using System.Threading.Tasks;
using Xunit;
using Million.Domain.Entities.Request;
using System.Net;
using System.Xml.Linq;
using System;

namespace Million.DeveloperTest.Tests;

public class PropertyImageServiceTests
{
    private readonly MockRepository mockRepositories;
    private readonly Mock<IBaseRepository<PropertyImage>> mockBaseRepository;
    private readonly Mock<IPropertyImageRepository> mockPropertyImageRepository;
    private readonly Mock<IRepository<PropertyImage>> mockRepository;
    private readonly Mock<IConfiguration> mockConfiguration;
    private readonly PropertyImageService propertyImageService;

    public PropertyImageServiceTests()
    {
        mockRepositories = new MockRepository(MockBehavior.Default);
        mockBaseRepository = mockRepositories.Create<IBaseRepository<PropertyImage>>();
        mockPropertyImageRepository = mockRepositories.Create<IPropertyImageRepository>();
        mockRepository = mockRepositories.Create<IRepository<PropertyImage>>();
        mockConfiguration = mockRepositories.Create<IConfiguration>();
        propertyImageService = new PropertyImageService(mockPropertyImageRepository.Object, mockRepository.Object, mockConfiguration.Object);
    }

    [Theory]
    [InlineData(1, "imageURL1")]
    [InlineData(1, "http://url")]
    [InlineData(1, "hdshffwhi")]
    public async Task Add_Image_From_Property(int IdProperty, string File)
    {
        Property property = new Property()
        {
            Name = "Home1",
            Address = "Street2",
            Price = 100,
            CodeInternal = 1,
            Year = new DateTime(2025,01,01),
            IdProperty = 1
        };

        PropertyImageRequestDto propertyDto = new PropertyImageRequestDto()
        {
            IdProperty = IdProperty,
            File = File
        };

        PropertyImage propertyRes = new PropertyImage()
        {
            IdProperty = IdProperty,
            Enabled = true,
            IdPropertyImage = 1,
            File = File
        };

        mockRepository
        .Setup(st => st.AddAsync(It.IsAny<PropertyImage>()))
        .ReturnsAsync(propertyRes);

        var result = await propertyImageService.AddPropertyImage(propertyDto);

        Assert.NotNull(result);
    }
}
