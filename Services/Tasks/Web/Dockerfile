# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo el proyecto primero (para cache más rápida)
COPY Services/Tasks/Web/Web.csproj Services/Tasks/Web/
RUN dotnet restore Services/Tasks/Web/Web.csproj

# Copiamos TODO
COPY . .

# Publicamos
RUN dotnet publish Services/Tasks/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiamos los archivos publicados
COPY --from=build /app .

# 👇 IMPORTANTE: COPIAR LOS APPSETTINGS
COPY Services/Tasks/Web/appsettings.json .
COPY Services/Tasks/Web/appsettings.Development.json .

ENTRYPOINT ["dotnet", "Web.dll"]
