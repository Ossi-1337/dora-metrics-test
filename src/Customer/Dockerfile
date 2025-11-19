ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build

WORKDIR /src

# Copy project files individually to leverage layer caching.
# Ensure you run docker build from the repository root so 'src/...' paths exist in the build context.
COPY src/Customer/Customer.csproj src/Customer/
COPY src/Shared/Shared.csproj src/Shared/

RUN dotnet restore src/Customer/Customer.csproj

# Copy all source code
COPY src/. src/

# Publish only the Customer project (Shared is included as referenced project)
RUN dotnet publish src/Customer/Customer.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Adjust the DLL name if your project produces a different assembly name
ENTRYPOINT ["dotnet", "Customer.dll"]

