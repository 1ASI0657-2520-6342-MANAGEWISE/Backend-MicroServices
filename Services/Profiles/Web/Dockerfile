# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos solo el proyecto primero (para cache más rápida)
COPY Services/Profiles/Web/Web.csproj Services/Profiles/Web/
RUN dotnet restore Services/Profiles/Web/Web.csproj

# Copiamos TODO
COPY . .

# Publicamos
RUN dotnet publish Services/Profiles/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiamos los archivos publicados
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]
