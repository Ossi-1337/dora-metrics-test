# Dockerfile for the Customer project (multi-stage .NET build)
# Adjust DOTNET_VERSION or project file name if needed.

ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy all source files (ensure Shared projects/folders are present in the build context)
COPY . .

# Detect solution or project, cd into project dir (so relative ProjectReference paths resolve),
# then restore and publish to /app/publish
RUN set -eux; \
    sln=$(find . -maxdepth 4 -name "*.sln" -print -quit); \
    if [ -n "$sln" ]; then \
      echo "Using solution: $sln"; \
      dotnet restore "$sln"; \
      dotnet publish "$sln" -c Release -o /app/publish /p:UseAppHost=false; \
      exit 0; \
    fi; \
    proj_path=$(find . -maxdepth 6 -name "*.csproj" -print -quit); \
    if [ -z "$proj_path" ]; then \
      echo "No .sln or .csproj found in /src"; exit 1; \
    fi; \
    echo "Using project: $proj_path"; \
    proj=$(basename "$proj_path"); \
    proj_dir=$(dirname "$proj_path"); \
    cd "$proj_dir"; \
    dotnet restore "$proj"; \
    dotnet publish "$proj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image must match major runtime (use .NET 8 ASP.NET runtime)
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Start the first DLL found in /app (avoids hardcoding the DLL name)
ENTRYPOINT ["sh", "-c", "dotnet /app/$(ls /app | grep '\\.dll$' | head -n1)"]