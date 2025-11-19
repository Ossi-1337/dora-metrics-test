# Build args
ARG DOTNET_VERSION=8.0

# -------- Build stage --------
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy project files first to leverage Docker layer caching
COPY ["src/Customer/Customer.Api.csproj", "Customer/"]
COPY ["src/Shared/Shared.csproj", "Shared/"]

# Restore only the Customer project
RUN dotnet restore "Customer/Customer.Api.csproj"

# Copy the rest of the source tree into /src
COPY src/. .

# Build and publish
WORKDIR /src/Customer
RUN dotnet build "Customer.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -------- Runtime stage --------
ARG DOTNET_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app

COPY --from=build /app/publish ./

ENTRYPOINT ["dotnet", "Customer.Api.dll"]
