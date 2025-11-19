
ARG DOTNET_VERSION=8.0
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src

# copy the entire source so all project files (including Shared) are available
COPY . .

# restore dependencies
RUN dotnet restore "Customer/Customer.csproj"

WORKDIR /src/Customer

# publish the Customer app
RUN dotnet publish "Customer.csproj" -c Release -o /app/publish --no-restore

# final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Customer.dll"]