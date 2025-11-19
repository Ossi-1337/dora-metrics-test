# Dockerfile for the Customer project (multi-stage .NET build)
# Adjust DOTNET_VERSION or project file name if needed.

ARG DOTNET_VERSION=7.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# Copy all source files
COPY . .

# Detect project file dynamically, restore and publish it
RUN set -eux; \
    proj=$(ls *.csproj 2>/dev/null | head -n 1); \
    if [ -z "$proj" ]; then echo "No .csproj found in /src"; exit 1; fi; \
    echo "Using project: $proj"; \
    dotnet restore "$proj"; \
    dotnet publish "$proj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Start the first DLL found in /app (avoids hardcoding the DLL name)
ENTRYPOINT ["sh", "-c", "dotnet /app/$(ls /app | grep '\\.dll$' | head -n1)"]