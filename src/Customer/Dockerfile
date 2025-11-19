# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files (for better restore-layer caching)
COPY ["src/Customer/Customer.Api.csproj", "Customer/"]
COPY ["src/Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "Customer/Customer.Api.csproj"

# Copy the rest of the source, including Shared
COPY src/Customer/ Customer/
COPY src/Shared/ Shared/

WORKDIR /src/Customer
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Expose the default ASP.NET Core ports used in containers
EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "Customer.Api.dll"]
