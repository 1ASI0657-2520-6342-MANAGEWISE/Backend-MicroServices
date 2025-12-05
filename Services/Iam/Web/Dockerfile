# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj
COPY Services/Profiles/Web/Web.csproj Services/Profiles/Web/
COPY Services/Profiles/Application/Application.csproj Services/Profiles/Application/
COPY Services/Profiles/Infrastructure/Infrastructure.csproj Services/Profiles/Infrastructure/

RUN dotnet restore Services/Profiles/Web/Web.csproj

# Copiar todo el repo
COPY . .

# Publicar
RUN dotnet publish Services/Profiles/Web/Web.csproj -c Release -o /app

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app .

ENTRYPOINT ["dotnet", "Web.dll"]

