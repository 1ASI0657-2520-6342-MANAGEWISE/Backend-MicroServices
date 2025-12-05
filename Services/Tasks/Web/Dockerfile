# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj
COPY Services/Tasks/Web/Web.csproj Services/Tasks/Web/
COPY Services/Tasks/Application/Application.csproj Services/Tasks/Application/
COPY Services/Tasks/Infrastructure/Infrastructure.csproj Services/Tasks/Infrastructure/

RUN dotnet restore Services/Tasks/Web/Web.csproj

# Copiar todo el repo
COPY . .

# Publicar
RUN dotnet publish Services/Tasks/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]

