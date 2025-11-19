
ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR ../src

# copy project files first (for layer caching)
COPY ["Customer/Customer.csproj", "Customer/"]
COPY ["Shared/Shared.csproj", "Shared/"]

# restore dependencies
RUN dotnet restore "Customer/Customer.csproj"

# copy the rest of the source
COPY . .

WORKDIR /src/Customer

# publish the Customer app
RUN dotnet publish "Customer.csproj" -c Release -o /app/publish --no-restore

# final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Customer.dll"]