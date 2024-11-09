FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["4.WebApi/Smartwyre.WebApi/Smartwyre.WebApi.csproj", "4.WebApi/Smartwyre.WebApi/"]
COPY ["1.Domain/Smartwyre.Domain.Entities/Smartwyre.Domain.Entities.csproj", "1.Domain/Smartwyre.Domain.Entities/"]
COPY ["2.Infraestructure/Smartwyre.Infra.IoC/Smartwyre.Infra.IoC.csproj", "2.Infraestructure/Smartwyre.Infra.IoC/"]
COPY ["3.Application/Smartwyre.Application.Services/Smartwyre.Application.Services.csproj", "3.Application/Smartwyre.Application.Services/"]
COPY ["3.Application/Smartwyre.Application.Contracts/Smartwyre.Application.Contracts.csproj", "3.Application/Smartwyre.Application.Contracts/"]
COPY ["3.Application/Smartwyre.Application.Interfaces/Smartwyre.Application.Interfaces.csproj", "3.Application/Smartwyre.Application.Interfaces/"]
COPY ["1.Domain/Smartwyre.Domain.Services/Smartwyre.Domain.Services.csproj", "1.Domain/Smartwyre.Domain.Services/"]
COPY ["1.Domain/Smartwyre.Domain.Interfaces/Smartwyre.Domain.Interfaces.csproj", "1.Domain/Smartwyre.Domain.Interfaces/"]
COPY ["2.Infraestructure/Smartwyre.Infra.Data/Smartwyre.Infra.Data.csproj", "2.Infraestructure/Smartwyre.Infra.Data/"]
RUN dotnet restore "4.WebApi/Smartwyre.WebApi/Smartwyre.WebApi.csproj"
COPY . .
WORKDIR "/src/4.WebApi/Smartwyre.WebApi"
RUN dotnet build "Smartwyre.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Smartwyre.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Smartwyre.WebApi.dll"]