# Use a small runtime image for final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Replace wildcard copy with explicit JSON-style COPY so docker resolves paths reliably.
# Ensure you run `docker build` from the repository root (the directory that contains Customer.Api/)
# or adjust the source paths below to match your repo layout.
COPY ["Customer.Api/Customer.Api.csproj", "Customer.Api/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# If you have a solution file, you can copy it too:
# COPY ["YourSolution.sln", "."]

# Restore only the projects (faster layer caching)
RUN dotnet restore Customer.Api/Customer.Api.csproj

# Copy the remaining source files
COPY . .

# Publish
WORKDIR /src/Customer.Api
RUN dotnet publish Customer.Api.csproj -c Release -o /app/publish --no-restore

# Final stage: copy publish output
FROM runtime AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Customer.Api.dll"]