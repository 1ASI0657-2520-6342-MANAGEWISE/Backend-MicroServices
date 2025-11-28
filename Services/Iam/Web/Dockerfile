# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo el proyecto primero (para cache más rápida)
COPY Services/Iam/Web/Web.csproj Services/Iam/Web/
RUN dotnet restore Services/Iam/Web/Web.csproj

# Copiamos TODO
COPY . .

# Publicamos
RUN dotnet publish Services/Iam/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiamos los archivos publicados
COPY --from=build /app .

# 👇 IMPORTANTE: COPIAR LOS APPSETTINGS
COPY Services/Iam/Web/appsettings.json .
COPY Services/Iam/Web/appsettings.Development.json .

ENTRYPOINT ["dotnet", "Web.dll"]
