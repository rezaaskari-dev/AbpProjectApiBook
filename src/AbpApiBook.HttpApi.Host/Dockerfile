
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/AbpApiBook.HttpApi.Host/AbpApiBook.HttpApi.Host.csproj

RUN dotnet publish src/AbpApiBook.HttpApi.Host/AbpApiBook.HttpApi.Host.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AbpApiBook.HttpApi.Host.dll"]
