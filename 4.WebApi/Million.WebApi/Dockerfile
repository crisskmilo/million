FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["4.WebApi/Million.WebApi/Million.WebApi.csproj", "4.WebApi/Million.WebApi/"]
COPY ["1.Domain/Million.Domain.Entities/Million.Domain.Entities.csproj", "1.Domain/Million.Domain.Entities/"]
COPY ["2.Infraestructure/Million.Infra.IoC/Million.Infra.IoC.csproj", "2.Infraestructure/Million.Infra.IoC/"]
COPY ["3.Application/Million.Application.Services/Million.Application.Services.csproj", "3.Application/Million.Application.Services/"]
COPY ["3.Application/Million.Application.Contracts/Million.Application.Contracts.csproj", "3.Application/Million.Application.Contracts/"]
COPY ["3.Application/Million.Application.Interfaces/Million.Application.Interfaces.csproj", "3.Application/Million.Application.Interfaces/"]
COPY ["1.Domain/Million.Domain.Services/Million.Domain.Services.csproj", "1.Domain/Million.Domain.Services/"]
COPY ["1.Domain/Million.Domain.Interfaces/Million.Domain.Interfaces.csproj", "1.Domain/Million.Domain.Interfaces/"]
COPY ["2.Infraestructure/Million.Infra.Data/Million.Infra.Data.csproj", "2.Infraestructure/Million.Infra.Data/"]
RUN dotnet restore "4.WebApi/Million.WebApi/Million.WebApi.csproj"
COPY . .
WORKDIR "/src/4.WebApi/Million.WebApi"
RUN dotnet build "Million.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Million.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80/tcp
ENTRYPOINT ["dotnet", "Million.WebApi.dll"]