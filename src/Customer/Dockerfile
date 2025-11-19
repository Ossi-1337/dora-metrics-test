# Multi-stage Dockerfile for Customer including Shared project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy entire repository into the build container to ensure src/Shared is always present
# This avoids "not found" errors for COPY when build context is partial or .dockerignore excludes paths
COPY . .

# Restore using the Customer project (path is relative to the copied repository)
RUN dotnet restore "src/Customer/Customer.Api.csproj"

# Publish Customer project
RUN dotnet publish "src/Customer/Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Customer.Api.dll"]