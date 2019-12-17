FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Services/Promotor/PromotorApi/PromotorApi.csproj", "Services/Promotor/PromotorApi/"]
RUN dotnet restore "Services/Promotor/PromotorApi/PromotorApi.csproj"
COPY . .
WORKDIR "/src/Services/Promotor/PromotorApi"
RUN dotnet build "PromotorApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PromotorApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PromotorApi.dll"]