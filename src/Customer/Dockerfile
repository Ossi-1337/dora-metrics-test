ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy solution and csproj(s) first to maximize cache reuse
COPY *.sln ./
COPY Customer/*.csproj Customer/

# If you use private NuGet feeds, copy nuget.config here:
# COPY nuget.config ./

# Restore using the explicit csproj path (this is the step that failed previously)
RUN dotnet restore "Customer/Customer.csproj"

# Copy the rest of the source and publish (no-restore to use cached restore)
COPY . .
RUN dotnet publish "Customer/Customer.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Customer.dll"]