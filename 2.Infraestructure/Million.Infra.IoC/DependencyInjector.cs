namespace Million.Infra.IoC
{
    using Million.Domain.Interfaces.Services;
    using Million.Domain.Services.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Million.Domain.Interfaces.Repositories.Transversal;
    using Million.Infra.Data.Repositories.Transversal;
    using Million.Domain.Interfaces.Services.Transversal;
    using Million.Domain.Services.Transversal;
    using Million.Application.Interfaces.Transversal;
    using Million.Application.Services.Transversal;
    using Million.Infra.Data.Repositories.Operation;
    using Million.Domain.Interfaces.Repositories.Operation;
    using Million.Domain.Services.Operation;
    using Million.Domain.Interfaces.Services.Operation;
    using Million.Application.Interfaces.Operation;
    using Million.Application.Services.Operation;

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
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IOwnerRepository, OwnerRepository>();
            services.AddSingleton<IPropertyRepository, PropertyRepository>();
            services.AddSingleton<IPropertyImageRepository, PropertyImageRepository>();
            services.AddSingleton<IPropertyTraceRepository, PropertyTraceRepository>();

            services.AddSingleton(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddSingleton<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IOwnerService, OwnerService>();
            services.AddSingleton<IPropertyService, PropertyService>();
            services.AddSingleton<IPropertyImageService, PropertyImageService>();
            services.AddSingleton<IPropertyTraceService, PropertyTraceService>();

            services.AddSingleton(typeof(IBaseApplication<>), typeof(BaseApplication<>));            
            services.AddSingleton<IAuthenticationApplication, AuthenticationApplication>();
            services.AddSingleton<IAuthorizationApplication, AuthorizationApplication>();
            services.AddSingleton<IUserApplication, UserApplication>();
            services.AddSingleton<IOwnerApplication, OwnerApplication>();
            services.AddSingleton<IPropertyApplication, PropertyApplication>();
            services.AddSingleton<IPropertyImageApplication, PropertyImageApplication>();
            services.AddSingleton<IPropertyTraceApplication, PropertyTraceApplication>();

            return services;
        }
    }
}