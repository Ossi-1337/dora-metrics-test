# Build args
ARG DOTNET_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# only copy neessary code because each folder is its own service, besides shared
COPY Customer.Api/Customer.Api.csproj ./Customer.Api/
COPY ../Shared ./Shared
# Restore the specific project
RUN dotnet restore ./Customer.Api/Customer.Api.csproj
# Copy the rest of the source and publish
COPY Customer.Api ./Customer.Api
RUN dotnet publish ./Customer.Api/Customer.Api.csproj -c Release -o /app/publish --no-restore
# Runtime stage: lightweight ASP.NET image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
# Configure listening port
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
# Start the app (adjust DLL name if your assembly name differs)
ENTRYPOINT ["dotnet", "Customer.Api.dll"]