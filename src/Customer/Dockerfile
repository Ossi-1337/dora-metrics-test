FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Directory.Build.props", "."]
COPY ["global.json", "."]
COPY ["Mtogo.sln", "."]
COPY ["src/Customer/Customer.Api.csproj", "src/Customer/"]
COPY ["src/Shared/Shared.csproj", "src/Shared/"]

# Restore dependencies
RUN dotnet restore "src/Customer/Customer.Api.csproj"

# Copy source files
COPY ["src/Customer/", "src/Customer/"]
COPY ["src/Shared/", "src/Shared/"]

# Build
WORKDIR "/src/src/Customer"
RUN dotnet build "Customer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customer.Api.dll"]