ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy project files first (adjust paths if your repo layout differs)
# This reduces restore times by leveraging Docker layer cache.
COPY ["src/Customer/Customer.Api.csproj", "Customer/"]
COPY ["src/Shared/Shared.csproj", "Shared/"]

# Restore only the Customer project (path is relative to WORKDIR)
RUN dotnet restore "Customer/Customer.Api.csproj"

# Copy the rest of the source tree
COPY src/ ./src/

# Build and publish
WORKDIR /src/Customer
RUN dotnet build "Customer.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

ARG DOTNET_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "Customer.Api.dll"]