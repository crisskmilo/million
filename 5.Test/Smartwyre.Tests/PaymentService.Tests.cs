using Microsoft.Extensions.Configuration;
using Moq;
using Smartwyre.Domain.Entities.Dto.Operation;
using Smartwyre.Domain.Entities.Enums;
using Smartwyre.Domain.Entities.Model.Operation;
using Smartwyre.Domain.Entities.Model.Transversal;
using Smartwyre.Domain.Interfaces.Repositories.Operation;
using Smartwyre.Domain.Interfaces.Repositories.Transversal;
using Smartwyre.Domain.Interfaces.Services.Operation;
using Smartwyre.Domain.Services.Operation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    private readonly MockRepository mockRepository;
    private readonly Mock<IBaseRepository<Rebate>> mockBaseRepository;
    private readonly Mock<IRebateRepository> mockRebateRepository;
    private readonly Mock<IConfiguration> mockConfiguration;
    private readonly RebateService rebateService;

    public PaymentServiceTests()
    {
        mockRepository = new MockRepository(MockBehavior.Default);
        mockBaseRepository = mockRepository.Create<IBaseRepository<Rebate>>();
        rebateService = new RebateService(mockRebateRepository.Object, mockConfiguration.Object);
    }

    [Theory]
    [InlineData("123456", "1234", 2)]
    public void CalculateRebate(string rebateIdentifier, string productIdentifier, decimal volume)
    {
        CalculateRebateRequest request = new CalculateRebateRequest()
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        Product product = new Product()
        {
            id=1,
            Identifier = productIdentifier,
            Price = 2,
            Uom = "m",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount    
        };

        Rebate rebate = new Rebate()
        {
            id = 1,
            Identifier = rebateIdentifier,
            Incentive = IncentiveType.AmountPerUom,
            Amount = 5,
            Percentage = 0.2M
        };


        mockRebateRepository
            .Setup(st => st.GetRebate(rebateIdentifier))
            .Returns(rebate);

        mockRebateRepository
            .Setup(st => st.StoreCalculationResult(rebate,10));

        var result = rebateService.Calculate(request, product);

        Assert.True(result.Success);
    }
}
