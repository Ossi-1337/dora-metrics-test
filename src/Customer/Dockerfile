# Dockerfile for the Customer project (multi-stage .NET build)
# Adjust DOTNET_VERSION or project file name if needed.

ARG DOTNET_VERSION=7.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy everything and restore/build. If your .csproj is named differently,
# replace "Customer.csproj" below with the actual project filename.
COPY . .
RUN dotnet restore "Customer.csproj"
RUN dotnet publish "Customer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Customer.dll"]