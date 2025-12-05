# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj
COPY Services/Payments/Web/Web.csproj Services/Payments/Web/
COPY Services/Payments/Application/Application.csproj Services/Payments/Application/
COPY Services/Payments/Infrastructure/Infrastructure.csproj Services/Payments/Infrastructure/

RUN dotnet restore Services/Payments/Web/Web.csproj

# Copiar todo el repo
COPY . .

# Publicar
RUN dotnet publish Services/Payments/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]
