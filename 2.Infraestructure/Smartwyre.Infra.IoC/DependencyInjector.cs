namespace Smartwyre.Infra.IoC
{
    using Smartwyre.Domain.Interfaces.Services;
    using Smartwyre.Domain.Services.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Smartwyre.Domain.Interfaces.Repositories.Transversal;
    using Smartwyre.Infra.Data.Repositories.Transversal;
    using Smartwyre.Domain.Interfaces.Services.Transversal;
    using Smartwyre.Domain.Services.Transversal;
    using Smartwyre.Application.Interfaces.Transversal;
    using Smartwyre.Application.Services.Transversal;
    using Smartwyre.Domain.Interfaces.Repositories.Operation;
    using Smartwyre.Infra.Data.Repositories.Operation;
    using Smartwyre.Domain.Services.Operation;
    using Smartwyre.Domain.Interfaces.Services.Operation;
    using Smartwyre.Application.Services.Operation;
    using Smartwyre.Application.Interfaces.Operation;

    public class DependencyInjector
    {
        public DependencyInjector()
        {
        }

        public IServiceCollection GetServiceCollection()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IDbFactory, DbFactory>();

            services.AddSingleton(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IProductRepository, ProductDataStore>();
            services.AddSingleton<IRebateRepository, RebateDataStore>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IRebateService, RebateService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton(typeof(IBaseApplication<>), typeof(BaseApplication<>));
            services.AddSingleton<IUserApplication, UserApplication>();
            services.AddSingleton<IRebateApplication, RebateApplication>();
            services.AddSingleton<IAuthenticationApplication, AuthenticationApplication>();
            services.AddSingleton<IAuthorizationApplication, AuthorizationApplication>();

            return services;
        }
    }
}