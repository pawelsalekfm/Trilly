FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Services/Catalog/CatalogApi/CatalogApi.csproj", "Services/Catalog/CatalogApi/"]
RUN dotnet restore "Services/Catalog/CatalogApi/CatalogApi.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/CatalogApi"
RUN dotnet build "CatalogApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CatalogApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CatalogApi.dll"]