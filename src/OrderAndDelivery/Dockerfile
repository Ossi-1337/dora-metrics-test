# Multi-stage Dockerfile for a .NET 8 ASP.NET service (OrderAndDelivery)
# Build stage: restore and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj(s) first to enable Docker layer caching for restores.
COPY *.csproj ./

# Restore the specific project
RUN dotnet restore ./OrderAndDelivery.Api.csproj

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish ./OrderAndDelivery.Api.csproj -c Release -o /app/publish --no-restore

# Runtime stage: lightweight ASP.NET image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Configure listening port and copy published output
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
COPY --from=build /app/publish .

# Start the app (adjust DLL name if your assembly name differs)
ENTRYPOINT ["dotnet", "OrderAndDelivery.Api.dll"]