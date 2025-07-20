using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Million.Domain.Entities.Model.Transversal;
using Million.WebApi; // Asegúrate que Program sea public partial class Program
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("qa") // ⚠️ Esto indica a Program.cs que use appsettings.qa.json
            .ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.AddJsonFile("appsettings.qa.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices(services =>
            {
                // Reemplaza el esquema de autenticación por uno de pruebas
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

                services.PostConfigureAll<AuthenticationOptions>(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                });
            });
    }
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim("unique_name", "admin@yopmail.com"),
            new Claim("nameid", "1"),
            new Claim(ClaimTypes.Name, "Test User"),
            new Claim(ClaimTypes.Role, "Admin")
        }, Scheme.Name);

        var principal = new ClaimsPrincipal(identity);
        // 🧪 Simula el usuario que normalmente pondría el JwtMiddleware
        Context.Items["UserName"] = new User
        {
            Id = 1,
            Name = "admin",
            Email = "admin@yopmail.com",
            Password = "123456"
        };
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
