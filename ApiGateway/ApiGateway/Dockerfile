FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo el csproj primero
COPY ApiGateway/ApiGateway/ApiGateway.csproj ApiGateway/ApiGateway/

# Restaurar dependencias
RUN dotnet restore ApiGateway/ApiGateway/ApiGateway.csproj

# Copiar el resto del código
COPY ApiGateway/ApiGateway/ ApiGateway/ApiGateway/

# Publicar
RUN dotnet publish ApiGateway/ApiGateway/ApiGateway.csproj -c Release -o /app

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
