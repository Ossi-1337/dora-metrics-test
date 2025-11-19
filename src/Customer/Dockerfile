FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project files first (Customer + Shared) so restore can be cached
COPY ["src/Customer/Customer.Api.csproj", "Customer/"]
COPY ["src/Shared/Shared.csproj", "Shared/"]

RUN dotnet restore "Customer/Customer.Api.csproj"

# Copy the rest of the sources
COPY src/Customer/ Customer/
COPY src/Shared/ Shared/

WORKDIR /src/Customer
RUN dotnet publish "Customer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Customer.Api.dll"]