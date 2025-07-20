using Million.Domain.Entities.Config;
using Million.Infra.IoC;
using Million.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Million.Infra.Data.Repositories.Transversal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.qa.json", optional: true, reloadOnChange: true);

builder.Services.Add(new DependencyInjector().GetServiceCollection());

var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetSection("AppSettings:DefaultConnection").Value;
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Singleton);


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddHttpClient();

builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

AddSwagger(builder.Services);

// Middleware
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = appSettings.Secret,
        ValidAudience = appSettings.Secret,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Secret))
    };
});

var app = builder.Build();

// Configuración de la aplicación
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("qa"))
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

if (!app.Environment.IsEnvironment("qa"))
{
    app.UseMiddleware<JwtMiddleware>();
}
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Million API v1");
});

app.MapGet("/api/values", async context =>
{
    await context.Response.WriteAsync("Api Million is running!!");
});

app.MapControllers();

app.Run();

// Función para agregar Swagger
void AddSwagger(IServiceCollection services)
{
    var configuration = builder.Configuration.GetSection("Swagger");
    var contactSection = configuration.GetSection("Contact");
    var securitySection = configuration.GetSection("Security");

    services.AddSwaggerGen(options =>
    {
        var groupName = configuration["Version"] ?? "v1";
        options.SwaggerDoc(groupName, new OpenApiInfo
        {
            Title = configuration["Title"] ?? $"Million API {groupName}",
            Version = groupName,
            Description = configuration["Description"] ?? "Million API",
            Contact = new OpenApiContact
            {
                Name = contactSection["Name"] ?? "",
                Email = contactSection["Email"] ?? "",
                Url = new Uri(contactSection["Url"] ?? "https://www.linkedin.com/in/")
            }
        });

        var jwtSecurityScheme = new OpenApiSecurityScheme
        {
            Scheme = securitySection["Scheme"] ?? "bearer",
            BearerFormat = securitySection["BearerFormat"] ?? "JWT",
            Name = securitySection["Name"] ?? "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Description = securitySection["Description"] ?? "Put **_ONLY_** your JWT Bearer token on textbox below!",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

        options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            { jwtSecurityScheme, Array.Empty<string>() }
        });
    });
}

public partial class Program { }