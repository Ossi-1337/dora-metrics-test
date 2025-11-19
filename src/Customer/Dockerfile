# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files first (restore cache)
COPY Shared/Shared.csproj Shared/
COPY Customer/Customer.Api.csproj Customer/

RUN dotnet restore Customer/Customer.Api.csproj

# Copy the rest of the source, including Shared
COPY Shared/ Shared/
COPY Customer/ Customer/

WORKDIR /src/Customer
RUN dotnet publish Customer.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

# -------- Runtime stage --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# Expose whatever ports you actually use; 8080 is common for ASP.NET in containers
EXPOSE 8080

ENTRYPOINT ["dotnet", "Customer.Api.dll"]
