
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/LibraryMemberService.DbMigrator/LibraryMemberService.DbMigrator.csproj

RUN dotnet publish src/LibraryMemberService.DbMigrator/LibraryMemberService.DbMigrator.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LibraryMemberService.DbMigrator.dll"]
