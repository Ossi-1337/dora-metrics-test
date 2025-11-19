ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src
COPY ["src/Customer/Customer.Api.csproj", "Customer/"]
COPY ["src/Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "Customer/Customer.Api.csproj"

COPY src/Customer/. Customer/
COPY src/Shared/. Shared/

WORKDIR /src/Customer
RUN dotnet publish -c Release -o /app/publish --no-restore

ARG DOTNET_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Customer.dll"]