# Build args
ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Context is src/, so no leading "src/" here
COPY ["Customer/Customer.Api.csproj", "Customer/"]
COPY ["Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "Customer/Customer.Api.csproj"

# Copy the rest of src/
COPY . .

WORKDIR /src/Customer
RUN dotnet build "Customer.Api.csproj" -c Release -o /app/build
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

ARG DOTNET_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENTRYPOINT ["dotnet", "Customer.Api.dll"]
