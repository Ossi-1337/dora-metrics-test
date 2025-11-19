# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution-wide files (optional but nice for restore)
COPY Mtogo.sln ./
COPY Directory.Build.props ./
COPY global.json ./

# Copy shared project and service project
COPY src/Shared/ src/Shared/
COPY src/Customer/ src/Customer/

# Restore
WORKDIR /src/src/Customer
RUN dotnet restore Customer.Api.csproj

# Build
RUN dotnet publish Customer.Api.csproj -c Release -o /app/publish --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./

# ASP.NET Core defaults to port 8080 in container scenarios, but expose 8080 explicitly
EXPOSE 8080

ENTRYPOINT ["dotnet", "Customer.Api.dll"]